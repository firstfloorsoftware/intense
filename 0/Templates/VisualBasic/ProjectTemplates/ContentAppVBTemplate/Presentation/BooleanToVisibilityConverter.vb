Namespace Presentation

    Public Class BooleanToVisibilityConverter
        Implements IValueConverter

        Private _inverse As Boolean
        Public Property Inverse As Boolean
            Get
                Return _inverse
            End Get
            Set(value As Boolean)
                _inverse = value
            End Set
        End Property

        Public Function Convert(value As Object, targetType As Type, parameter As Object, language As String) As Object Implements IValueConverter.Convert
            Dim bValue = DirectCast(value, Boolean)
            If Inverse Then
                bValue = Not bValue
            End If
            If bValue Then
                Return Visibility.Visible
            End If
            Return Visibility.Collapsed
        End Function

        Public Function ConvertBack(value As Object, targetType As Type, parameter As Object, language As String) As Object Implements IValueConverter.ConvertBack
            Dim result As Boolean = value = Visibility.Visible
            If Inverse Then
                result = Not result
            End If

            Return result
        End Function
    End Class
End Namespace
