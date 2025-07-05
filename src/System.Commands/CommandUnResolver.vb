Imports Microsoft.Extensions.Logging

Friend Class CommandUnResolver
    Implements IDisposable

    Private disposedValue As Boolean
    Private ReadOnly source As Dictionary(Of Type, Func(Of ICommand))
    Private ReadOnly command As ICommand
    Private ReadOnly logger As ILogger
    ''' <summary>
    ''' Gets a value indicating whether the command un-resolver is disposed.
    ''' </summary>
    ''' <returns><c>true</c> if the command un-resolver is disposed; otherwise, <c>false</c>.</returns>

    ''' <summary>
    ''' Initializes a new instance of the <see cref="CommandUnResolver"/> class.
    ''' </summary>
    ''' <param name="source">The source dictionary containing command factories.</param>
    ''' <param name="command">The command to be unregistered.</param>
    Sub New(source As Dictionary(Of Type, Func(Of ICommand)), command As ICommand, Optional logger As ILogger = Nothing)
        ArgumentNullException.ThrowIfNull(source, NameOf(source))
        ArgumentNullException.ThrowIfNull(command, NameOf(command))
        Me.source = source
        Me.command = command
        Me.logger = logger
    End Sub

    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then
                ' TODO: dispose managed state (managed objects)
            End If

            ' TODO: free unmanaged resources (unmanaged objects) and override finalizer
            ' TODO: set large fields to null
            disposedValue = True
            Dim keyToRemove As ICommand = source.FirstOrDefault(Function(kvp) kvp.Key.GetType() = command.GetType()).Key
            If keyToRemove IsNot Nothing Then
                source.Remove(keyToRemove)
            End If
        End If
    End Sub

    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code. Put cleanup code in 'Dispose(disposing As Boolean)' method
        Dispose(disposing:=True)
        GC.SuppressFinalize(Me)
    End Sub
End Class
