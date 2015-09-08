Imports $safeprojectname$.Presentation

Namespace UI.Converters
    ''' <summary>
    ''' Converts a <see cref="NavigationItem"/> instance to object and vice versa.
    ''' </summary>
    Public Class NavigationItemToObjectConverter
        Inherits ValueConverter(Of NavigationItem, Object)

        Public Overrides Function Convert(value As NavigationItem, parameter As Object, language As String) As Object
            Return value
        End Function

        Public Overrides Function ConvertBack(value As Object, parameter As Object, language As String) As NavigationItem
            Return TryCast(value, NavigationItem)
        End Function
    End Class
End Namespace

