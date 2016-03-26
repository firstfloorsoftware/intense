Imports $safeprojectname$.Presentation

''' <summary>
''' An empty page that can be used on its own or navigated to within a Frame.
''' </summary>
Public NotInheritable Class SettingsPage
    Inherits Page

    Private _viewModel As SettingsViewModel = New SettingsViewModel()

    Public ReadOnly Property ViewModel As SettingsViewModel
        Get
            Return _viewModel
        End Get
    End Property

End Class
