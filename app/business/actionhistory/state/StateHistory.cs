using app.business.state;
using app.business.util;
using System.Collections.Generic;

namespace app.business.actionhistory.state
{
    /// <summary>
    /// Represents full snapshots of the business models state, whenever it is altered. 
    /// </summary>
    internal class StateHistory : IActionHistory<ApplicationState>
    {
        /// <summary>
        /// A reference to the application state. 
        /// </summary>
        private ApplicationState _state;

        /// <summary>
        /// The history of state snapshots. 
        /// </summary>
        public LimitedStack<ApplicationState> Reversible { get; protected set; }

        /// <summary>
        /// The history of reversed state snapshots. In other words, these are the snapshots that have been "undone". 
        /// </summary>
        public Stack<ApplicationState> Reversed { get; protected set; }

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

        public StateHistory(ApplicationState state)
        {
            _state = state;
            Reversible = new LimitedStack<ApplicationState>(32);
            Reversed = new Stack<ApplicationState>();
        }

        /// <summary>
        /// Pushes a new snapshot of the ApplicationState to the stack. 
        /// </summary>
        public void Push()
        {
            ApplicationState cloned = (ApplicationState)_state.Clone();
            Reversible.Push(cloned);
        }

        /// <summary>
        /// Reverses the last made state and returns it. Returns null, if there is no state to reverse. 
        /// </summary>
        /// <returns>The state that was "undone". </returns>
        public ApplicationState Undo()
        {
            if (Reversible.Count == 0)
                return null;

            var state = Reversible.Pop();
            _state.Apply(state);
            Reversed.Push(state);

            return state;
        }

        /// <summary>
        /// Re-applies the last state that was reversed and returns it. Returns null, if there is no state to re-apply. 
        /// </summary>
        /// <returns>The state that was re-applied. </returns>
        public ApplicationState Redo()
        {
            if (Reversed.Count == 0)
                return null;

            var state = Reversed.Pop();
            Reversible.Push(state);

            return state;
        }

        /// <summary>
        /// Clears all history, without reversing or re-applying anything. 
        /// </summary>
        public void Clear()
        {
            Reversible.Clear();
            Reversed.Clear();
        }

        /// <summary>
        /// Clears all reversible history. 
        /// </summary>
        public void ClearReversible()
        {
            Reversible.Clear();
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
