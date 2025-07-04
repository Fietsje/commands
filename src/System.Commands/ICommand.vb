''' <summary>
''' Command interface for executing actions within a specific context.
''' This interface defines methods to execute a command and check if it can be executed.
''' </summary>
''' <remarks> Implement this interface to create custom commands that can be executed in a specific context. </remarks>
Public Interface ICommand
    ''' <summary>
    ''' Gets the name of the command.
    ''' This property should return a unique name for the command.
    ''' </summary>
    ''' <remarks>
    ''' The name is used to identify the command in logs or user interfaces.
    ''' </remarks>
    ''' <returns>A string representing the name of the command.</returns>
    ReadOnly Property Name As String

    ''' <summary>
    ''' Gets an exception number.
    ''' This property should return an integer that represents an error code or status.
    ''' It can be used to identify specific exceptions or errors that occur during command execution.
    ''' </summary>
    ''' <remarks>
    ''' The exception number can be used for logging or debugging purposes.</remarks>
    ''' <returns>An exception number that can be used for logging or debugging purposes.</returns>
    ReadOnly Property ExceptionNumber As Integer

    ''' <summary>
    ''' Gets an exception message.
    ''' This property should return a string that describes the error or exception that occurred during command execution.
    ''' It provides additional context about the error for debugging or user feedback.
    ''' </summary>
    ''' <remarks>
    ''' The exception message can be used to inform users about what went wrong and how to resolve it.</remarks>
    ''' <returns>An exception message that can be used to inform users about what went wrong and how to resolve it.</returns>
    ReadOnly Property ExceptionMessage As String

    ''' <summary>
    ''' Checks if the command can be executed in the given context.
    ''' This method should return a boolean indicating whether the command can be executed.
    ''' It allows for pre-execution validation of the command's conditions.
    ''' </summary>
    ''' <remarks>
    ''' Implement this method to define the conditions under which the command can be executed.</remarks>
    ''' <param name="context"></param>
    ''' <returns>True, if the command can be executed, otherwise: False.</returns>
    Function CanExecute(context As ICommandContext) As Boolean

    ''' <summary>
    ''' Performs the command execution in the given context.
    ''' This method should contain the logic to execute the command's action.
    ''' It is called when the command is confirmed to be executable.
    ''' </summary>
    ''' <param name="context">The command context to execute on.</param>
    ''' <remarks>
    ''' Implement this method to define what happens when the command is executed.</remarks>
    Sub Execute(context As ICommandContext)
End Interface
