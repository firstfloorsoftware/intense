Namespace Presentation
    Public Class ShellViewModel
        Inherits NotifyPropertyChanged

        Private _menuItems As ObservableCollection(Of MenuItem) = New ObservableCollection(Of MenuItem)
        Private _selectedMenuItem As MenuItem
        Private _isSplitViewPaneOpen As Boolean
        Private _toggleSplitViewPaneCommand As ICommand

        Public Sub New()
            _toggleSplitViewPaneCommand = New Command(Sub() IsSplitViewPaneOpen = Not IsSplitViewPaneOpen)
        End Sub

        Public ReadOnly Property ToggleSplitViewPaneCommand As ICommand
            Get
                Return _toggleSplitViewPaneCommand
            End Get
        End Property

        Public Property IsSplitViewPaneOpen As Boolean
            Get
                Return _isSplitViewPaneOpen
            End Get
            Set
                SetProperty(_isSplitViewPaneOpen, Value)
            End Set
        End Property

        Public Property SelectedMenuItem As MenuItem
            Get
                Return _selectedMenuItem

            End Get
            Set
                If SetProperty(_selectedMenuItem, Value) Then
                    OnPropertyChanged("SelectedPageType")

                    ' auto-close split view pane
                    IsSplitViewPaneOpen = False
                End If
            End Set
        End Property

        Public Property SelectedPageType As Type
            Get
                If _selectedMenuItem Is Nothing Then
                    Return Nothing
                End If
                Return _selectedMenuItem.PageType
            End Get
            Set
                ' select associated menu item
                SelectedMenuItem = _menuItems.FirstOrDefault(Function(m) m.PageType.Equals(Value))
            End Set
        End Property

        Public ReadOnly Property MenuItems As ObservableCollection(Of MenuItem)
            Get
                Return _menuItems
            End Get
        End Property

    End Class
End Namespace



