Imports Microsoft.VisualStudio.TestTools.UnitTesting

<TestClass> Public Class CommandContextTests

    <TestMethod>
    <TestCategory("CommandContext")>
    Public Sub Execute_WithNoCommand_ReturnsNothing()
        ' Arrange
        Dim context As New CommandContext()

        ' Act
        Dim result = context.Execute(Of ICommand)(Nothing)

        ' Assert
        Assert.IsNull(result, "Expected Execute to return Nothing when no command is provided.")
    End Sub

End Class
