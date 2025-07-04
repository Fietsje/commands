''' <summary>
''' Basic class for implementing commands.
''' This class implements the ICommand interface and provides a base for command execution.
''' It includes properties for the command name, exception number, and exception message.
''' </summary>
Public MustInherit Class Command
    Implements ICommand

    ''' <summary>
    ''' Gets the name of the command.
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property Name As String Implements ICommand.Name

    ''' <summary>
    ''' Gets an exception number.
    ''' It can be used to identify specific exceptions or errors that occur during command execution.
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property ExceptionNumber As Integer Implements ICommand.ExceptionNumber

    ''' <summary>
    ''' Gets an exception message.
    ''' It provides additional context about the error for debugging or user feedback.
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property ExceptionMessage As String Implements ICommand.ExceptionMessage

    ''' <summary>
    ''' Executes the command in the given context.
    ''' This method should contain the logic to execute the command's action.
    ''' It is called when the command is confirmed to be executable.
    ''' </summary>
    ''' <param name="context"></param>
    Public MustOverride Sub Execute(context As ICommandContext) Implements ICommand.Execute

    ''' <summary>
    ''' Checks if the command can be executed in the given context.
    ''' This method should return a boolean indicating whether the command can be executed.
    ''' It allows for pre-execution validation of the command's conditions.
    ''' </summary>
    ''' <param name="context"></param>
    ''' <returns></returns>
    Public MustOverride Function CanExecute(context As ICommandContext) As Boolean Implements ICommand.CanExecute
End Class
