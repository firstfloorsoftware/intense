Imports Intense.Presentation

Namespace Presentation
    Public Class ShellViewModel
        Inherits NotifyPropertyChanged

        Private _topItems As NavigationItemCollection = New NavigationItemCollection
        Private _selectedTopItem As NavigationItem
        Private _bottomItems As NavigationItemCollection = New NavigationItemCollection
        Private _selectedBottomItem As NavigationItem
        Private _isSplitViewPaneOpen As Boolean
        Private _toggleSplitViewPaneCommand As ICommand

        Public Sub New()
            _toggleSplitViewPaneCommand = New RelayCommand(Sub() IsSplitViewPaneOpen = Not IsSplitViewPaneOpen)

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
                Me.Set(_isSplitViewPaneOpen, Value)
            End Set
        End Property

        Public Property SelectedTopItem As NavigationItem
            Get
                Return _selectedTopItem

            End Get
            Set
                If Me.Set(_selectedTopItem, Value) And Not Value Is Nothing Then
                    OnSelectedMenuItemChanged(True)
                End If
            End Set
        End Property

        Public Property SelectedBottomItem As NavigationItem
            Get
                Return _selectedBottomItem

            End Get
            Set
                If Me.Set(_selectedBottomItem, Value) And Not Value Is Nothing Then
                    OnSelectedMenuItemChanged(False)
                End If
            End Set
        End Property

        Public Property SelectedItem As NavigationItem
            Get
                If Not _selectedTopItem Is Nothing Then
                    Return _selectedTopItem
                End If
                Return _selectedBottomItem
            End Get
            Set
                SelectedTopItem = _topItems.FirstOrDefault(Function(m) m.Equals(Value))
                SelectedBottomItem = _bottomItems.FirstOrDefault(Function(m) m.Equals(Value))
            End Set
        End Property

        Public Property SelectedPageType As Type
            Get
                Dim item = SelectedItem
                If Not item Is Nothing Then
                    Return item.PageType
                End If
                Return Nothing
            End Get
            Set
                ' select associated menu item
                SelectedTopItem = _topItems.FirstOrDefault(Function(m) m.PageType.Equals(Value))
                SelectedBottomItem = _bottomItems.FirstOrDefault(Function(m) m.PageType.Equals(Value))
            End Set
        End Property

        Public ReadOnly Property TopItems As NavigationItemCollection
            Get
                Return _topItems
            End Get
        End Property

        Public ReadOnly Property BottomItems As NavigationItemCollection
            Get
                Return _bottomItems
            End Get
        End Property

        Private Sub OnSelectedMenuItemChanged(top As Boolean)
            If top Then
                SelectedBottomItem = Nothing
            Else
                SelectedTopItem = Nothing
            End If
            OnPropertyChanged("SelectedItem")
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



