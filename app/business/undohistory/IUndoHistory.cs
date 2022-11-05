namespace app.business.actionhistory
{
    /// <summary>
    /// Represents a history of undoable/redoable commands or states. 
    /// </summary>
    /// <typeparam name="T">The data type of commands/states this undo history works with. </typeparam>
    public interface IUndoHistory<T>
    {
        /// <summary>
        /// Reverses the last made command/state and returns it. Returns null, if there is nothing reverse. 
        /// </summary>
        /// <returns>The command/state that was "undone". </returns>
        T Undo();

        /// <summary>
        /// Re-applies the last command/state that was reversed and returns it. Returns null, if there is nothing to re-apply. 
        /// </summary>
        /// <returns>The command/state that was re-applied. </returns>
        T Redo();

        /// <summary>
        /// Clears all history, without reversing or re-applying anything. 
        /// </summary>
        void Clear();

        /// <summary>
        /// Clears all reversible history, without reversing or re-applying anything. 
        /// </summary>
        void ClearReversible();

        /// <summary>
        /// Clears all reversed history, without reversing or re-applying anything. 
        /// </summary>
        void ClearReversed();
    }
}
