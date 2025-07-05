Imports Microsoft.Extensions.Logging

Public Class CommandContext
    Implements ICommandContext

    ''' <summary>
    ''' Gets a value indicating whether the command context is disposed.
    ''' This property returns a boolean indicating if the context has been disposed of.
    ''' It can be used to prevent operations on a disposed context.
    ''' </summary>
    Public ReadOnly Property IsDisposed As Boolean Implements ICommandContext.IsDisposed

    ''' <summary>
    ''' Gets the logger for this command context.
    ''' This property provides access to a logger instance that can be used for logging within the context.
    ''' </summary>
    ''' <remarks>
    ''' The logger can be used to log messages, warnings, or errors related to command execution.
    ''' </remarks>
    Public ReadOnly Property Logger As ILogger Implements ICommandContext.Logger

    ''' <summary>
    ''' Gets the logging options for this command context.
    ''' This property provides access to logging options that can be used to configure how commands are logged.
    ''' </summary>
    ''' <remarks>
    ''' The logging options can include settings such as the style of friendly names used in log messages.
    ''' </remarks>
    Public ReadOnly Property LoggingOptions As LoggingOptions = New LoggingOptions()

    ''' <summary>
    ''' Gets the command resolver for this command context.
    ''' This property provides access to a command resolver that can resolve commands within the context.
    ''' </summary>
    ''' <remarks>
    ''' The resolver can be used to find and instantiate commands based on their types or names.
    ''' </remarks>
    Public ReadOnly Property Resolver As ICommandResolver Implements ICommandContext.Resolver

    ''' <summary>
    ''' Initializes a new instance of the <see cref="CommandContext"/> class.
    ''' </summary>
    ''' <param name="logger">The logger.</param>
    ''' <param name="resolver">The resolver.</param>
    Sub New(Optional logger As ILogger = Nothing, Optional resolver As ICommandResolver = Nothing)
        _Logger = logger
        _Resolver = IIf(resolver IsNot Nothing, resolver, New CommandResolver(logger))
        _IsDisposed = False
    End Sub

    ''' <summary>
    ''' Executes the specified command in the current context.
    ''' This method should execute the command and return the command instance after execution.
    ''' It allows for executing commands with the context's specific settings or state.
    ''' </summary>
    ''' <typeparam name="TCommand">The type of the command to execute</typeparam>
    ''' <param name="command">The command to execute</param>
    ''' <returns>
    ''' The command that was executed or NULL (Nothing in VB)
    ''' </returns>
    Public Overridable Function Execute(Of TCommand As ICommand)(ByVal command As TCommand) As TCommand Implements ICommandContext.Execute
        ObjectDisposedException.ThrowIf(IsDisposed, Me)
        If command Is Nothing Then Return command

        Dim friendlyName As String = GetFriendlyName(command.GetType(), LoggingOptions.NameStyle)
        Dim fullCommandName = IIf(String.IsNullOrEmpty(command.Name), $"(Type = {friendlyName})", $"{command.Name} (Type = {friendlyName})")


        Logger?.LogDebug("Executing {CommandName}", fullCommandName)

        Try
            If command.CanExecute(Me) Then
                command.Execute(Me)
            Else
                Logger?.LogWarning("Could not execute {CommandName} due to: '{ExceptionMessage}'", fullCommandName, command.ExceptionMessage)
            End If
        Catch ex As Exception
            Logger?.LogError(ex, "An error occurred while executing {CommandName}", fullCommandName)
            Throw
        Finally
            Logger?.LogDebug("Finished executing {CommandName}", fullCommandName)
        End Try

        Return command
    End Function

    ''' <summary>
    ''' Releases unmanaged and - optionally - managed resources.
    ''' </summary>
    ''' <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not IsDisposed Then
            If disposing Then
                ' TODO: dispose managed state (managed objects)
            End If

            ' TODO: free unmanaged resources (unmanaged objects) and override finalizer
            ' TODO: set large fields to null
            _IsDisposed = True
        End If
    End Sub

    ' ' TODO: override finalizer only if 'Dispose(disposing As Boolean)' has code to free unmanaged resources
    ' Protected Overrides Sub Finalize()
    '     ' Do not change this code. Put cleanup code in 'Dispose(disposing As Boolean)' method
    '     Dispose(disposing:=False)
    '     MyBase.Finalize()
    ' End Sub

    ''' <summary>
    ''' Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
    ''' </summary>
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code. Put cleanup code in 'Dispose(disposing As Boolean)' method
        Dispose(disposing:=True)
        GC.SuppressFinalize(Me)
    End Sub
End Class
