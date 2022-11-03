using app.business.actionhistory.commandhistory;
using app.business.state;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace app.business.actionhistory.state
{
    /// <summary>
    /// Represents full snapshots of the business models state, whenever it is altered. 
    /// </summary>
    internal class StateHistory
    {
        /// <summary>
        /// A reference to the application state. 
        /// </summary>
        private ApplicationState _state;

        /// <summary>
        /// The history of state snapshots. 
        /// </summary>
        public Stack<ApplicationState> ReversibleStates { get; protected set; }

        /// <summary>
        /// The history of reversed state snapshots. In other words, these are the snapshots that have been "undone". 
        /// </summary>
        public Stack<ApplicationState> ReversedStates { get; protected set; }

        /// <summary>
        /// The number of snapshots to keep in history at most. Oldest entries will be discarded, 
        /// once the limit has been reached.
        /// </summary>
        public int MaxHistoryEntryCount { get; set; }

        public StateHistory(ApplicationState state)
        {
            _state = state;
            ReversibleStates = new Stack<ApplicationState>();
            MaxHistoryEntryCount = 32;
        }

        /// <summary>
        /// 
        /// </summary>
        public void Push()
        {
            // 1. Deep copy the state object.
            // 2. Discard oldest entry, if necessary. 
            // 3. Add the copy of the state to history. 
        }

        /// <summary>
        /// Returns the last state snapshot made, or null, if there is no history. 
        /// </summary>
        /// <returns>The last state snapshot or null. </returns>
        public ApplicationState Pop()
        {
            if (ReversibleStates.Count == 0)
                return null;

            var state = ReversibleStates.Pop();
            ReversedStates.Push(state);

            return state;
        }

        /// <summary>
        /// Clears all history, without reversing or re-applying anything. 
        /// </summary>
        public void Clear()
        {
            ReversibleStates.Clear();
            ReversedStates.Clear();
        }

        /// <summary>
        /// Clears all reversible history. 
        /// </summary>
        public void ClearReversibleStates()
        {
            ReversibleStates.Clear();
        }

        /// <summary>
        /// Clears all reversed history. 
        /// </summary>
        public void ClearReversedStates()
        {
            ReversedStates.Clear();
        }
    }
}
