Friend Class ConsoleLoggerScope
    Implements IDisposable

    Private disposedValue As Boolean
    Private sources As IList(Of ConsoleLoggerScope)()

    ''' <summary>
    ''' Gets or sets the state to log.
    ''' </summary>
    Public ReadOnly Property State As Object

    ''' <summary>
    ''' Initializes a new instance of the <see cref="ConsoleLoggerScope"/> class.
    ''' </summary>
    Sub New(source As IList(Of ConsoleLoggerScope)(), state As Object)
        ' Initialize any resources if needed
        Me.sources = source
        Me.State = state
    End Sub

    ''' <summary>
    ''' Releases unmanaged and - optionally - managed resources.
    ''' </summary>
    ''' <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then
                ' TODO: dispose managed state (managed objects)
            End If

            ' TODO: free unmanaged resources (unmanaged objects) and override finalizer
            ' TODO: set large fields to null
            disposedValue = True
            TryCast(sources, IList(Of ConsoleLoggerScope)).Remove(Me)
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
