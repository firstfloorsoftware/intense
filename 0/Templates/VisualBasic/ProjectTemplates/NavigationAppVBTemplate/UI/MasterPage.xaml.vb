Imports $safeprojectname$.Presentation
Imports Windows.UI.Xaml.Media.Animation

Namespace UI
    ''' <summary>
    ''' Represents the master navigation page.
    ''' </summary>
    Public NotInheritable Class MasterPage
        Inherits NavigationPage

        Public Const BasicNavigationListViewStyleKey As String = "BasicNavigationListViewStyle"
        Public Const NarrowNavigationListViewStyle As String = "NarrowNavigationListViewStyle"
        Public Const WideNavigationListViewStyle As String = "WideNavigationListViewStyle"

        Public Shared ReadOnly NavigationListViewStyleProperty As DependencyProperty = DependencyProperty.Register("NavigationListViewStyle", GetType(Style), GetType(NavigationPage), Nothing)

        Protected Overrides Sub OnNavigationItemChanged(oldValue As NavigationItem, newValue As NavigationItem)
            MyBase.OnNavigationItemChanged(oldValue, newValue)

            SelectedItem = Nothing
            UpdateListViewStyle()
        End Sub

        Protected Overrides Sub OnWindowStateChanged(oldValue As String, newValue As String)
            If Frame Is Nothing Or NavigationItem Is Nothing Then Return

            ' when state changes from narrow to wide and navigation item is a master-detail candidate, navigate to master-detail
            If oldValue = WindowStateNarrow And newValue = WindowStateWide And IsMasterDetailCandidate(NavigationItem) Then
                ' navigate without transition
                Frame.Navigate(GetType(MasterDetailPage), NavigationItem, New SuppressNavigationTransitionInfo())

                ' and clear the most recent backstack entry
                Frame.BackStack.RemoveAt(Frame.BackStackDepth - 1)
            Else
                UpdateListViewStyle()
            End If

        End Sub

        Private Sub UpdateListViewStyle()
            Dim styleKey As String = WideNavigationListViewStyle

            If WindowState = WindowStateNarrow Then
                styleKey = BasicNavigationListViewStyleKey

                If Not NavigationItem Is Nothing AndAlso (NavigationItem.IsRoot() Or NavigationItem.HasGrandchildren()) Then
                    styleKey = NarrowNavigationListViewStyle
                End If
            End If

            NavigationListViewStyle = TryCast(Application.Current.Resources.Item(styleKey), Style)
        End Sub

        Public Property NavigationListViewStyle As Style
            Get
                Return GetValue(NavigationListViewStyleProperty)
            End Get
            Set(value As Style)
                SetValue(NavigationListViewStyleProperty, value)
            End Set
        End Property

    End Class
End Namespace