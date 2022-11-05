using app.business.state;
using app.business.util;
using System;
using System.Collections.Generic;

namespace app.business.actionhistory.state
{
    /// <summary>
    /// Represents full snapshots of the business model state, whenever it is altered. 
    /// </summary>
    /// <typeparam name="T">Data type of the concrete application state type. </typeparam>
    public class SnapshotHistory<T> : IUndoHistory<T>
        where T : AbstractApplicationState<T>
    {
        /// <summary>
        /// A reference to the application state. 
        /// </summary>
        private T _state;

        /// <summary>
        /// A copy of the initial state. 
        /// </summary>
        private T _initialState;

        /// <summary>
        /// The history of state snapshots. 
        /// </summary>
        public LimitedStack<T> Reversible { get; protected set; }

        /// <summary>
        /// The history of reversed state snapshots. In other words, these are the snapshots that have been "undone". 
        /// </summary>
        public Stack<T> Reversed { get; protected set; }

        /// <summary>
        /// The number of commands to keep in history at most. Oldest entries will be discarded, 
        /// once the limit has been reached.
        /// <br></br>
        /// Default 32. 
        /// </summary>
        public int MaxHistoryEntryCount
        {
            get { return Reversible.Capacity; }
            set { Reversible.Capacity = value; }
        }

        public SnapshotHistory(T state)
        {
            _state = state;
            _initialState = state.Clone();
            Reversible = new LimitedStack<T>(32);
            Reversed = new Stack<T>();
        }

        /// <summary>
        /// Pushes a new snapshot of the ApplicationState to the stack. 
        /// <br></br>
        /// A new snapshot is meant to be taken after every alteration of the model state. 
        /// </summary>
        public void TakeSnapshot()
        {
            T cloned = _state.Clone();
            Reversible.Push(cloned);
        }

        /// <summary>
        /// Reverses the last made state and returns it. Returns null, if there is no state to reverse. 
        /// </summary>
        /// <returns>The state that was "undone". </returns>
        public T Undo()
        {
            if (Reversible.Count > 0)
            {
                var popped = Reversible.Pop();

                if (Reversible.Count > 0)
                {
                    T toApply = Reversible.Peek();
                    _state.Apply(toApply);
                }
                else
                {
                    T toApply = _initialState;
                    _state.Apply(toApply);
                }

                Reversed.Push(popped);

                return popped;
            }
            else
            {
                _state.Apply(_initialState);

                return default;
            }
        }

        /// <summary>
        /// Re-applies the last state that was reversed and returns it. Returns null, if there is no state to re-apply. 
        /// </summary>
        /// <returns>The state that was re-applied. </returns>
        public T Redo()
        {
            if (Reversed.Count == 0)
                return default;

            var toApply = Reversed.Pop();
            _state.Apply(toApply);
            Reversible.Push(toApply);

            return toApply;
        }

        /// <summary>
        /// Clears all history, without reversing or re-applying anything. 
        /// </summary>
        public void Clear()
        {
            Reversible.Clear();
            Reversed.Clear();
            _initialState = _state.Clone();
        }

        /// <summary>
        /// Clears all reversible history. 
        /// </summary>
        public void ClearReversible()
        {
            Reversible.Clear();
            _initialState = _state.Clone();
        }

        /// <summary>
        /// Clears all reversed history. 
        /// </summary>
        public void ClearReversed()
        {
            Reversed.Clear();
        }
    }
}
