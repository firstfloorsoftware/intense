Imports Intense.Presentation

Namespace Presentation
    Public Class DisplayableTheme
        Inherits Displayable

        Private _theme As ApplicationTheme

        Public Sub New(displayName As String, theme As ApplicationTheme)
            Me.DisplayName = displayName
            _theme = theme
        End Sub

        Public ReadOnly Property Theme As ApplicationTheme
            Get
                Return _theme
            End Get
        End Property
    End Class
End Namespace
