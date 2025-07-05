Public Module CommandExtensions
    ''' <summary>
    ''' Gets a human-readably typename of the Specified type.
    ''' </summary>
    ''' <param name="type">The type to get the name of.</param>
    ''' <param name="style">Optional coding style.</param>
    ''' <returns>A <c>String</c> containing a friendly name</returns>
    Function GetFriendlyName(ByVal type As Type, Optional style As FriendlyNameStyle = FriendlyNameStyle.VB) As String
        If type Is Nothing Then Return String.Empty
        Dim builder As New Text.StringBuilder()

        If type.IsGenericType Then
            Dim name = type.Name.Substring(0, type.Name.IndexOf("`"c))
            builder.Append(name)

            If FriendlyNameStyle.VB = style Then
                builder.Append("(Of ")
            Else
                builder.Append("<")
            End If

            Dim counter As Integer = 0
            For Each arg In type.GetGenericArguments()
                If counter > 0 Then builder.Append(", ")
                builder.Append(GetFriendlyName(arg, style))
                counter += 1
            Next

            If FriendlyNameStyle.VB = style Then
                builder.Append(")")
            Else
                builder.Append(">")
            End If

        Else
            builder.Append(type.Name)
        End If

        Return builder.ToString()
    End Function

    Public Enum FriendlyNameStyle
        VB
        CSharp
    End Enum
End Module
