Imports $safeprojectname$.Presentation

Public NotInheritable Class Shell
    Inherits UserControl

    Private _viewModel As ShellViewModel

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        _viewModel = New ShellViewModel
        _viewModel.MenuItems.Add(New MenuItem With {.Icon = "", .Title = "Welcome", .PageType = GetType(WelcomePage)})
        _viewModel.MenuItems.Add(New MenuItem With {.Icon = "", .Title = "Page 1", .PageType = GetType(Page1)})
        _viewModel.MenuItems.Add(New MenuItem With {.Icon = "", .Title = "Page 2", .PageType = GetType(Page2)})
        _viewModel.MenuItems.Add(New MenuItem With {.Icon = "", .Title = "Page 3", .PageType = GetType(Page3)})

        ' selected the first menu item
        _viewModel.SelectedMenuItem = _viewModel.MenuItems.First
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
