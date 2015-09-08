Namespace UI.Converters
    ''' <summary>
    ''' Converts a boolean to and from a visibility value.
    ''' </summary>
    Public Class BooleanToVisibilityConverter
        Inherits ValueConverter(Of Boolean, Object)

        Private _inverse As Boolean

        ''' <summary>
        ''' Determines whether an inverse conversion should take place.
        ''' </summary>
        ''' <returns></returns>
        Public Property Inverse
            Get
                Return _inverse
            End Get
            Set(value)
                _inverse = value
            End Set
        End Property

        Public Overrides Function Convert(value As Boolean, parameter As Object, language As String) As Object
            If Inverse Then
                value = Not value
            End If
            Return If(value, Visibility.Visible, Visibility.Collapsed)
        End Function

        Public Overrides Function ConvertBack(value As Object, parameter As Object, language As String) As Boolean
            Dim result As Boolean = value = Visibility.Visible
            If Inverse Then
                result = Not result
            End If

            Return result
        End Function
    End Class

End Namespace
