Public NotInheritable Class LoremPage
    Inherits Page

    Private _title As String

    Public ReadOnly Property Title As String
        Get
            Return _title
        End Get
    End Property

    Protected Overrides Sub OnNavigatedTo(e As NavigationEventArgs)
        MyBase.OnNavigatedTo(e)

        _title = TryCast(e.Parameter, String)
    End Sub
End Class
