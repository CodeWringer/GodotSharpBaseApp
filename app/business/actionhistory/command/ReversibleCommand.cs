using System;

namespace app.business.actionhistory.commandhistory
{
    /// <summary>
    /// Represents a reversible alteration of the business model state. 
    /// </summary>
    internal class ReversibleCommand
    {
        /// <summary>
        /// Represents an action a user made to alter the business model state. 
        /// </summary>
        public Action Action { get; protected set; }

        /// <summary>
        /// Represents the inverse to the action, to "undo" it. 
        /// </summary>
        public Action ReverseAction { get; protected set; }

        public ReversibleCommand(Action action, Action reverseAction)
        {
            Action = action;
            ReverseAction = reverseAction;
        }

        /// <summary>
        /// Executes the action to alter the state. 
        /// </summary>
        public void Invoke()
        {
            Action();
        }

        /// <summary>
        /// Executes the inverse action to "undo" the alteration of the state. 
        /// </summary>
        public void InvokeReverse()
        {
            ReverseAction();
        }
    }
}
