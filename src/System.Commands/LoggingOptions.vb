''' <summary>
''' Represents options for logging within the command context.
''' This class can be extended to include specific logging configurations or settings.
''' </summary>
Public Class LoggingOptions

    ''' <summary>
    ''' Gets or sets the style of friendly names used in logging.
    ''' This property determines how type names are formatted in log messages.
    ''' </summary>
    ''' <value>
    ''' A <see cref="FriendlyNameStyle"/> value indicating the style of friendly names.
    ''' </value>
    ''' <remarks>
    ''' The default value is <see cref="FriendlyNameStyle.VB"/>, which formats names in Visual Basic style.
    ''' Other styles may be added in the future for different programming languages or conventions.
    ''' </remarks>
    Public Property NameStyle As FriendlyNameStyle = FriendlyNameStyle.VB

End Class
