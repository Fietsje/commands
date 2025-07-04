Imports Microsoft.Extensions.Logging

''' <summary>
''' Commandconext interface for executing commands within a specific context.
''' This interface defines methods to execute a command and check if it can be executed.
''' </summary>
''' <remarks>
''' Implement this interface to create custom commandcontexts that can execute commands in a specific context.
''' </remarks>
Public Interface ICommandContext
    Inherits IDisposable

    ''' <summary>
    ''' Gets a value indicating whether the command context is disposed.
    ''' This property returns a boolean indicating if the context has been disposed of.
    ''' It can be used to prevent operations on a disposed context.
    ''' </summary>
    ''' <returns>True, if this object is disposed, otherwise: False</returns>
    ReadOnly Property IsDisposed As Boolean

    ''' <summary>
    ''' Gets the logger for this command context.
    ''' This property provides access to a logger instance that can be used for logging within the context.
    ''' </summary>
    ''' <returns>An ILogger instance for logging purposes.</returns>
    ''' <remarks>
    ''' The logger can be used to log messages, warnings, or errors related to command execution.
    ''' </remarks>
    ReadOnly Property Logger As ILogger

    ''' <summary>
    ''' Gets the command resolver for this command context.
    ''' This property provides access to a command resolver that can resolve commands within the context.
    ''' </summary>
    ''' <returns>An ICommandResolver instance for resolving commands.</returns>
    ''' <remarks>
    ''' The resolver can be used to find and instantiate commands based on their types or names.
    ''' </remarks>
    ReadOnly Property Resolver As ICommandResolver

    ''' <summary>
    ''' Executes the specified command in the current context.
    ''' This method should execute the command and return the command instance after execution.
    ''' It allows for executing commands with the context's specific settings or state.
    ''' </summary>
    ''' <typeparam name="TCommand">The type of the command to execute</typeparam>
    ''' <param name="command">The command to execute</param>
    ''' <returns>The command that was executed or NULL (Nothing in VB)</returns>
    Function Execute(Of TCommand As ICommand)(ByRef command As TCommand) As TCommand

End Interface
