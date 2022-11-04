using app.business.util;
using System.Collections.Generic;

namespace app.business.actionhistory.command
{
    /// <summary>
    /// Represents the history of actions taken to alter the business model state. 
    /// <br></br>
    /// Keeps two histories: one for the actions taken and which could be "undone" and one for the actions that <b>have been</b> "undone". 
    /// <br></br>
    /// This represents the command-pattern for an undo history. This undo system is lightest on memory, but is fairly heavy  
    /// in terms of implementation and maintenance. 
    /// </summary>
    public class CommandHistory : IActionHistory<IReversibleCommand>
    {
        /// <summary>
        /// The history of commands that could be "undone". 
        /// </summary>
        public LimitedStack<IReversibleCommand> Reversible { get; protected set; }

        /// <summary>
        /// The history of commands that have been "undone". 
        /// </summary>
        public Stack<IReversibleCommand> Reversed { get; protected set; }

        /// <summary>
        /// The number of commands to keep in history at most. Oldest entries will be discarded, 
        /// once the limit has been reached.
        /// <br></br>
        /// Default 100. 
        /// </summary>
        public int MaxHistoryEntryCount
        {
            get { return Reversible.Capacity; }
            set { Reversible.Capacity = value; }
        }

        public CommandHistory()
        {
            Reversible = new LimitedStack<IReversibleCommand>(100);
            Reversed = new Stack<IReversibleCommand>();
        }

        /// <summary>
        /// Executes the given command and "memorizes" it. 
        /// <br></br>
        /// Clears the history of reversed actions. This is done, because every time a completely new command is invoked, 
        /// a new "timeline" is begun, meaning it no longer makes sense to re-apply commands on 
        /// business model state that may no longer be "correct" from the view point of the command. 
        /// E. g.: Add an item and then move it. This represents 2 commands. The move is then "undone". 
        /// Then, the item is removed, as a new command. At this point, re-applying the "undone" move command
        /// targets an item that no longer exists and is therefore no longer valid. 
        /// </summary>
        /// <param name="command">The command to execute and memorize. </param>
        public void InvokeAndPush(IReversibleCommand command)
        {
            command.Invoke();
            Reversible.Push(command);
            ClearReversed();
        }

        /// <summary>
        /// Reverses the last made command and returns it. Returns null, if there is no command to reverse. 
        /// </summary>
        /// <returns>The command that was "undone". </returns>
        public IReversibleCommand Undo()
        {
            if (Reversible.Count == 0)
                return null;

            var popped = Reversible.Pop();
            popped.InvokeReverse();
            Reversed.Push(popped);

            return popped;
        }

        /// <summary>
        /// Re-applies the last command that was reversed and returns it. Returns null, if there is no command to re-apply. 
        /// </summary>
        /// <returns>The command that was re-applied. </returns>
        public IReversibleCommand Redo()
        {
            if (Reversed.Count == 0)
                return null;

            var command = Reversed.Pop();
            command.Invoke();
            Reversible.Push(command);

            return command;
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
