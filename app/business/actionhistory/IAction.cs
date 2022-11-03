namespace app.business.actionhistory
{
    /// <summary>
    /// Represents a command/action a user can make to alter the business model state. 
    /// </summary>
    internal interface IAction
    {
        /// <summary>
        /// Invokes the command/action and alters the business model state. 
        /// </summary>
        void Invoke();
    }
}
