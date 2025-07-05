Imports Microsoft.VisualStudio.TestTools.UnitTesting

<TestClass> Public Class ExtensionMethodTests

    <DataRow(GetType(String), "String", FriendlyNameStyle.VB, DisplayName:="Simple String in VB")>
    <DataRow(GetType(String), "String", FriendlyNameStyle.CSharp, DisplayName:="Simple String in CSharp")>
    <DataRow(GetType(Func(Of String)), "Func(Of String)", FriendlyNameStyle.VB, DisplayName:="Single generic type in VB")>
    <DataRow(GetType(Func(Of String)), "Func<String>", FriendlyNameStyle.CSharp, DisplayName:="Single generic type in CSharp")>
    <DataRow(GetType(Func(Of String, Integer, DateTime, Boolean)), "Func(Of String, Int32, DateTime, Boolean)", FriendlyNameStyle.VB, DisplayName:="Multiple generic types in VB")>
    <DataRow(GetType(Func(Of String, Integer, DateTime, Boolean)), "Func<String, Int32, DateTime, Boolean>", FriendlyNameStyle.CSharp, DisplayName:="Multiple generic types in CSharp")>
    <DataRow(GetType(Func(Of String, Integer, DateTime, Boolean, Func(Of String))), "Func(Of String, Int32, DateTime, Boolean, Func(Of String))", FriendlyNameStyle.VB, DisplayName:="Nested generic types in VB")>
    <DataRow(GetType(Func(Of String, Integer, DateTime, Boolean, Func(Of String))), "Func<String, Int32, DateTime, Boolean, Func<String>>", FriendlyNameStyle.CSharp, DisplayName:="Nested generic types in CSharp")>
    <DataRow(GetType(Action(Of String, Action(Of Integer, Action(Of DateTime)))), "Action(Of String, Action(Of Int32, Action(Of DateTime)))", FriendlyNameStyle.VB, DisplayName:="Deep nested generic types in VB")>
    <DataRow(GetType(Action(Of String, Action(Of Integer, Action(Of DateTime)))), "Action<String, Action<Int32, Action<DateTime>>>", FriendlyNameStyle.CSharp, DisplayName:="Deep nested generic types in CSharp")>
    <DataTestMethod>
    <TestCategory("Get friendly name")>
    Public Sub GetFriendlyName_WithExistingType_ThenReturnsFriendlyName(type As Type, output As String, mode As FriendlyNameStyle)
        ' Arrange

        ' Act
        Dim result As String = CommandExtensions.GetFriendlyName(type, mode)

        ' Assert
        Assert.AreEqual(output, result)
    End Sub

End Class
