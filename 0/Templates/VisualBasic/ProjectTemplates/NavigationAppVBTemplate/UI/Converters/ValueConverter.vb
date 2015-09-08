Namespace UI.Converters
    ''' <summary>
    ''' The generic base implementation of a value converter.
    ''' </summary>
    Public Class ValueConverter(Of TSource, TTarget)
        Implements IValueConverter

        Public Function Convert(value As Object, targetType As Type, parameter As Object, language As String) As Object Implements IValueConverter.Convert
            Return Convert(DirectCast(value, TSource), parameter, language)
        End Function

        Public Function ConvertBack(value As Object, targetType As Type, parameter As Object, language As String) As Object Implements IValueConverter.ConvertBack
            Return ConvertBack(DirectCast(value, TTarget), parameter, language)
        End Function

        Public Overridable Function Convert(value As TSource, parameter As Object, language As String) As TTarget
            Throw New NotSupportedException
        End Function

        Public Overridable Function ConvertBack(value As TTarget, parameter As Object, language As String) As TSource
            Throw New NotSupportedException
        End Function
    End Class
End Namespace

