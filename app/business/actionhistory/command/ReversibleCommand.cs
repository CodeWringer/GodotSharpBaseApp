using System.Collections.Generic;

namespace app.business.actionhistory.command
{
    /// <summary>
    /// Represents a generic and general reversible command. 
    /// </summary>
    /// <typeparam name="T">Data type of the business state to work with. </typeparam>
    public class ReversibleCommand<T> : IReversibleCommand
    {
        /// <summary>
        /// An invocable delegate function that receives the state object and 
        /// working data dictionary of the command as arguments. 
        /// </summary>
        /// <param name="state">The state to alter. </param>
        /// <param name="workingData">A dictionary of data to pass between the action and its inverse action. </param>
        public delegate void InvokeDelegate(T state, Dictionary<string, object> workingData);

        /// <summary>
        /// The state to alter. 
        /// </summary>
        protected T _state;

        /// <summary>
        /// A dictionary of data to pass between the action and its inverse action. 
        /// </summary>
        protected Dictionary<string, object> _workingData;

        /// <summary>
        /// The function to execute to alter the state. 
        /// </summary>
        protected InvokeDelegate _invokeDelegate { get; set; }

        /// <summary>
        /// The function to execute to "undo" the alteration of the state. 
        /// </summary>
        protected InvokeDelegate _invokeReverseDelegate { get; set; }

        public ReversibleCommand(T state, InvokeDelegate action, InvokeDelegate inverseAction)
        {
            this._state = state;
            this._workingData = new Dictionary<string, object>();
            this._invokeDelegate = action;
            this._invokeReverseDelegate = inverseAction;
        }

        public void Invoke()
        {
            this._invokeDelegate(this._state, this._workingData);
        }

        public void InvokeReverse()
        {
            this._invokeReverseDelegate(this._state, this._workingData);
        }
    }
}
