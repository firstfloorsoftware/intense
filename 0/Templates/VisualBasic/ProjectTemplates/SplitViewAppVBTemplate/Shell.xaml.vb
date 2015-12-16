Imports Intense.Presentation
Imports $safeprojectname$.Presentation
    
Public NotInheritable Class Shell
    Inherits UserControl

    Private _viewModel As ShellViewModel

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        _viewModel = New ShellViewModel
        _viewModel.TopItems.Add(New NavigationItem With {.Icon = "", .DisplayName = "Welcome", .PageType = GetType(WelcomePage)})
        _viewModel.TopItems.Add(New NavigationItem With {.Icon = "", .DisplayName = "Page 1", .PageType = GetType(Page1)})
        _viewModel.TopItems.Add(New NavigationItem With {.Icon = "", .DisplayName = "Page 2", .PageType = GetType(Page2)})
        _viewModel.TopItems.Add(New NavigationItem With {.Icon = "", .DisplayName = "Page 3", .PageType = GetType(Page3)})

        _viewModel.BottomItems.Add(New NavigationItem With {.Icon = "", .DisplayName = "Settings", .PageType = GetType(SettingsPage)})

        ' selected the first menu item
        _viewModel.SelectedItem = _viewModel.TopItems.First
    End Sub

    Public ReadOnly Property ViewModel As ShellViewModel
        Get
            Return _viewModel
        End Get
    End Property

    Public ReadOnly Property RootFrame As Frame
        Get
            Return Me.Frame
        End Get
    End Property

End Class
