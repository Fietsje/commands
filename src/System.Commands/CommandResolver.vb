Imports Microsoft.Extensions.Logging

Public Class CommandResolver
    Implements ICommandResolver
    Implements IDisposable

    ''' <summary>
    ''' A dictionary to hold command type mappings to their factory functions.
    ''' </summary>
    ''' <remarks>
    ''' This dictionary is used to resolve commands by their type.
    ''' </remarks>
    Private ReadOnly mappings As New Dictionary(Of Type, Func(Of ICommand))()
    Private ReadOnly logger As ILogger

    ''' <summary>
    ''' A list to hold disposable resources.
    ''' </summary>
    ''' <remarks>
    ''' This list is used to keep track of disposable resources that need to be cleaned up when the resolver is disposed.
    ''' </summary>
    Private ReadOnly disposables As New List(Of IDisposable)()

    ''' <summary>
    ''' Initializes a new instance of the <see cref="CommandResolver"/> class.
    ''' </summary>
    Public Sub New(Optional logger As ILogger = Nothing)
        Me.logger = logger
    End Sub

    ''' <summary>
    ''' Indicates whether the object has been disposed.
    ''' </summary>
    ''' <remarks>
    ''' This property is used to check if the object has already been disposed to prevent multiple disposals.
    ''' </remarks>
    Public ReadOnly Property IsDisposed As Boolean

    ''' <summary>
    ''' Clears the command resolver.
    ''' </summary>
    ''' <remarks>
    ''' This method should clear any cached or stored commands in the resolver.
    ''' It can be used to reset the state of the resolver, for example, when reloading commands or changing contexts.
    ''' </remarks>
    Public Sub Clear() Implements ICommandResolver.Clear
        mappings.Clear()
    End Sub

    ''' <summary>
    ''' Resolves a command by its type.
    ''' </summary>
    ''' <typeparam name="TCommand">The type of the command to resolve.</typeparam>
    ''' <returns>The command instance associated with the specified type, or a new instance if not found.</returns>
    ''' <remarks>
    ''' If the command is not registered, a new instance will be created and registered.
    ''' </remarks>
    Public Function Resolve(Of TCommand As {Class, ICommand, New})() As TCommand Implements ICommandResolver.Resolve
        If mappings.ContainsKey(GetType(TCommand)) Then
            Return DirectCast(mappings(GetType(TCommand))(), TCommand)
        End If

        ' If the command is not registered, create a new instance, but do not register it.
        Return New TCommand()
    End Function

    ''' <summary>
    ''' Registers a command factory for a specific command type.
    ''' </summary>
    ''' <typeparam name="TCommand">The type of the command to register.</typeparam>
    ''' <param name="factory">A method to create a new TCommand object.</param>
    ''' <returns>An object that can be used to un-register</returns>
    ''' <remarks>
    ''' If the command is already registered, an exception will be thrown.
    ''' </remarks>
    Public Overridable Function RegisterCommand(Of TCommand As {Class, ICommand, New})(factory As Func(Of TCommand)) As IDisposable Implements ICommandResolver.RegisterCommand
        Return RegisterCommand(GetType(TCommand), factory)
    End Function

    ''' <summary>
    ''' Registers a command factory for a specific command type with a parent type.
    ''' </summary>
    ''' <typeparam name="TCommand">The type of the command to register.</typeparam>
    ''' <param name="commandType">The type of the command that the command is associated with.</param>
    ''' <param name="factory">A method to create a new TCommand object.</param>
    ''' <returns>An object that can be used to un-register</returns>
    ''' <remarks>
    ''' If the command is already registered, an exception will be thrown.
    ''' </remarks>
    Public Overridable Function RegisterCommand(Of TCommand As {Class, ICommand, New})(commandType As Type, factory As Func(Of TCommand)) As IDisposable Implements ICommandResolver.RegisterCommand
        ArgumentNullException.ThrowIfNull(commandType, NameOf(commandType))
        ArgumentNullException.ThrowIfNull(factory, NameOf(factory))

        If Not GetType(ICommand).IsAssignableFrom(commandType) Then
            logger?.LogError("The type '{Name}' must implement the ICommand interface.", commandType.Name)
            Throw New ArgumentException($"The type '{commandType.Name}' must implement the ICommand interface.", NameOf(commandType))
        End If

        If mappings.ContainsKey(commandType) Then
            logger?.LogError("Command type '{Name}' is already registered.", commandType.Name)
            Throw New InvalidOperationException($"Command type '{commandType.Name}' is already registered.")
        End If

        logger?.LogDebug("Registering command type '{Name}' with factory.", commandType.Name)
        mappings(commandType) = Function() factory()

        Dim unregister As New CommandUnResolver(mappings, commandType, logger)
        disposables.Add(unregister)
        Return unregister
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
            Me.disposables.ForEach(Sub(d) d.Dispose())
            Me.disposables.Clear()
        End If
    End Sub

    ''' <summary>
    ''' Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
    ''' </summary>
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code. Put cleanup code in 'Dispose(disposing As Boolean)' method
        Dispose(disposing:=True)
        GC.SuppressFinalize(Me)
    End Sub
End Class
