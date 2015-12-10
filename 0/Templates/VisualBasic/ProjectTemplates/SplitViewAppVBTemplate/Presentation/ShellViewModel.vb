Namespace Presentation
    Public Class ShellViewModel
        Inherits NotifyPropertyChanged

        Private _menuItems As ObservableCollection(Of MenuItem) = New ObservableCollection(Of MenuItem)
        Private _selectedMenuItem As MenuItem
        Private _bottomMenuItems As ObservableCollection(Of MenuItem) = New ObservableCollection(Of MenuItem)
        Private _selectedBottomMenuItem As MenuItem
        Private _isSplitViewPaneOpen As Boolean
        Private _toggleSplitViewPaneCommand As ICommand

        Public Sub New()
            _toggleSplitViewPaneCommand = New Command(Sub() IsSplitViewPaneOpen = Not IsSplitViewPaneOpen)

            ' open splitview pane in wide state
            IsSplitViewPaneOpen = IsWideState()
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
                If SetProperty(_selectedMenuItem, Value) And Not Value Is Nothing Then
                    OnSelectedMenuItemChanged(True)
                End If
            End Set
        End Property

        Public Property SelectedBottomMenuItem As MenuItem
            Get
                Return _selectedBottomMenuItem

            End Get
            Set
                If SetProperty(_selectedBottomMenuItem, Value) And Not Value Is Nothing Then
                    OnSelectedMenuItemChanged(False)
                End If
            End Set
        End Property

        Public Property SelectedPageType As Type
            Get
                If Not _selectedMenuItem Is Nothing Then
                    Return _selectedMenuItem.PageType
                End If
                If Not _selectedBottomMenuItem Is Nothing Then
                    Return _selectedBottomMenuItem.PageType
                End If
                Return Nothing
            End Get
            Set
                ' select associated menu item
                SelectedMenuItem = _menuItems.FirstOrDefault(Function(m) m.PageType.Equals(Value))
                SelectedBottomMenuItem = _bottomMenuItems.FirstOrDefault(Function(m) m.PageType.Equals(Value))
            End Set
        End Property

        Public ReadOnly Property MenuItems As ObservableCollection(Of MenuItem)
            Get
                Return _menuItems
            End Get
        End Property

        Public ReadOnly Property BottomMenuItems As ObservableCollection(Of MenuItem)
            Get
                Return _bottomMenuItems
            End Get
        End Property

        Private Sub OnSelectedMenuItemChanged(top As Boolean)
            If top Then
                SelectedBottomMenuItem = Nothing
            Else
                SelectedMenuItem = Nothing
            End If
            OnPropertyChanged("SelectedPageType")

            ' auto-close split view pane (only when not in widestate)
            If Not IsWideState() Then
                IsSplitViewPaneOpen = False
            End If
        End Sub

        ' a helper determining whether we are in a wide window state
        ' mvvm purists probably don't appreciate this approach
        Private Function IsWideState() As Boolean
            Return Window.Current.Bounds.Width >= 1024
        End Function

    End Class
End Namespace



