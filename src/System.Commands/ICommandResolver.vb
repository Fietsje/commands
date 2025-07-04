''' <summary>
''' Command resolver interface for resolving commands.
''' This interface defines methods to resolve commands based on their names or types.
''' </summary>
''' <remarks>
''' Implement this interface to create custom command resolvers that can resolve commands in a specific context.
''' </remarks>
Public Interface ICommandResolver
    ''' <summary>
    ''' Clears the command resolver.
    ''' This method should clear any cached or stored commands in the resolver.
    ''' It can be used to reset the state of the resolver, for example, when reloading commands or changing contexts.
    ''' </summary>
    Sub Clear()

    ''' <summary>
    ''' Resolves a command by its type.
    ''' </summary>
    ''' <typeparam name="TCommand">The type of the command to resolve.</typeparam>
    ''' <returns>The command instance associated with the specified type, or a new instance if not found.</returns>
    Function Resolve(Of TCommand As {Class, ICommand, New})() As TCommand

    ''' <summary>
    ''' Registers a command factory for a specific command type.
    ''' This method allows you to register a factory function that creates instances of the specified command type.
    ''' The factory will be used to create new instances of the command when it is resolved.
    ''' </summary>
    ''' <typeparam name="TCommand">The type of the command to register.</typeparam>
    ''' <param name="factory">A method to create a new TCommand object.</param>
    ''' <returns>An object that can be used to un-register</returns>
    Function RegisterCommand(Of TCommand As {Class, ICommand, New})(factory As Func(Of TCommand)) As IDisposable

    ''' <summary>
    ''' Registers a command factory for a specific command type.
    ''' This method allows you to register a factory function that creates instances of the specified command type.
    ''' The factory will be used to create new instances of the command when it is resolved.
    ''' </summary>
    ''' <typeparam name="TCommand">The type of the command to register.</typeparam>
    ''' <param name="parentType"> The type of the parent command that the command is associated with.</param>
    ''' <param name="factory">A method to create a new TCommand object.</param>
    ''' <returns>An object that can be used to un-register</returns>
    Function RegisterCommand(Of TCommand As {Class, ICommand, New})(parentType As Type, factory As Func(Of TCommand)) As IDisposable

End Interface
