Imports $safeprojectname$.Presentation

Public NotInheritable Class Shell
    Inherits UserControl

    Private _viewModel As ContentViewModel

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        _viewModel = New ContentViewModel
    End Sub

    Public ReadOnly Property ViewModel As ContentViewModel
        Get
            Return _viewModel
        End Get
    End Property

End Class
