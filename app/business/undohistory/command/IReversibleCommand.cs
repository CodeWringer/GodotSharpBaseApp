namespace app.business.actionhistory.command
{
    /// <summary>
    /// Represents a reversible alteration of the business model state. 
    /// </summary>
    public interface IReversibleCommand
    {
        /// <summary>
        /// Executes the action to alter the state. 
        /// </summary>
        void Invoke();

        /// <summary>
        /// Executes the inverse action to "undo" the alteration of the state. 
        /// </summary>
        void InvokeReverse();
    }
}
