Imports Microsoft.Extensions.Logging

Public Class ConsoleLogger
    Implements ILogger

    Private scopes As New List(Of ConsoleLoggerScope)

    Public Sub Log(Of TState)(logLevel As LogLevel, eventId As EventId, state As TState, exception As Exception, formatter As Func(Of TState, Exception, String)) Implements ILogger.Log
        Throw New NotImplementedException()
    End Sub

    Public Function IsEnabled(logLevel As LogLevel) As Boolean Implements ILogger.IsEnabled
        Return True
    End Function

    Public Function BeginScope(Of TState)(state As TState) As IDisposable Implements ILogger.BeginScope
        Dim scope = New ConsoleLoggerScope(scopes, state)
        scopes.Add(scope)

        Return scope
    End Function
End Class
