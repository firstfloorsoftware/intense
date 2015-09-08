Imports $safeprojectname$.Presentation

Namespace UI
    ''' <summary>
    ''' Represents the master-detail navigation page.
    ''' </summary>
    Public NotInheritable Class MasterDetailPage
        Inherits NavigationPage

        Public Const LayoutStateMasterDetail As String = "MasterDetailState"
        Public Const LayoutStateDetail As String = "DetailState"

        Private lastSelectedItem As NavigationItem

        Protected Overrides Sub OnNavigatingFrom(e As NavigatingCancelEventArgs)
            MyBase.OnNavigatingFrom(e)

            ' make sure the content frame is cleared

            If Not ContentFrame.SourcePageType Is Nothing And Not ContentFrame.SourcePageType.Equals(GetType(Page)) Then
                ContentFrame.SourcePageType = GetType(Page)
            End If

            ContentFrame.BackStack.Clear()
            ContentFrame.ForwardStack.Clear()
            lastSelectedItem = Nothing
        End Sub

        Protected Overrides Sub OnSelectedItemChanged(oldValue As NavigationItem, newValue As NavigationItem)
            If newValue Is Nothing Or lastSelectedItem Is newValue Then Return

            lastSelectedItem = newValue

            If Not newValue.Equals(NavigationItem) And (Not newValue.IsLeaf() Or WindowState = WindowStateNarrow) Then
                ' navigate to selected item
                MyBase.OnSelectedItemChanged(oldValue, newValue)
            Else
                ' navigate to content and supply the PageParameter
                Dim pageType As Type = If(newValue.PageType, GetType(Page))
                If Not pageType.Equals(GetType(Page)) Or Not pageType.Equals(ContentFrame.SourcePageType) Then
                    ContentFrame.Navigate(pageType, newValue.PageParameter)
                End If
            End If
        End Sub

        Protected Overrides Sub OnNavigationItemChanged(oldValue As NavigationItem, newValue As NavigationItem)
            MyBase.OnNavigationItemChanged(oldValue, newValue)

            If Frame Is Nothing Or newValue Is Nothing Then
                SelectedItem = Nothing
                Return
            End If

            Dim layoutStateName As String = LayoutStateMasterDetail

            If newValue.IsLeaf() Then
                ' select navigation item itself
                SelectedItem = newValue

                layoutStateName = LayoutStateDetail
            ElseIf SelectedItem Is Nothing Then
                ' auto-select first child
                SelectedItem = newValue.Items.FirstOrDefault()
            End If

            VisualStateManager.GoToState(Me, layoutStateName, False)
        End Sub

        Protected Overrides Sub OnWindowStateChanged(oldValue As String, newValue As String)
            If Frame Is Nothing Or NavigationItem Is Nothing Then Return

            Dim layoutStateName As String = Nothing

            If newValue = WindowStateWide Then
                layoutStateName = LayoutStateMasterDetail

                If NavigationItem.IsLeaf() Then
                    If oldValue = WindowStateNarrow And IsMasterDetailCandidate(NavigationItem.Parent) Then
                        ' if from narrow and leaf, auto-select parent
                        Dim navItem As NavigationItem = NavigationItem
                        NavigationItem = NavigationItem.Parent
                        SelectedItem = navItem

                        ' remove narrow master from backstack
                        Dim entry As PageStackEntry = Frame.BackStack.FirstOrDefault(Function(e) e.Parameter.Equals(navItem.Parent))
                        If Not entry Is Nothing Then
                            Frame.BackStack.Remove(entry)
                        End If

                        Return
                    End If

                    ' show leaf always in detail
                    layoutStateName = LayoutStateDetail
                End If
            ElseIf newValue = WindowStateNarrow Then
                layoutStateName = LayoutStateDetail

                'if from wide, auto-select to selected child
                If oldValue = WindowStateWide And Not NavigationItem.Equals(SelectedItem) And Not SelectedItem Is Nothing AndAlso SelectedItem.IsLeaf() Then
                    Dim parent As NavigationItem = NavigationItem
                    NavigationItem = SelectedItem

                    ' add narrow master to backstack
                    Frame.BackStack.Add(New PageStackEntry(GetType(MasterPage), parent, Nothing))

                    Return
                End If
            End If

            If Not layoutStateName Is Nothing Then
                VisualStateManager.GoToState(Me, layoutStateName, False)
            End If
        End Sub

        Private Sub OnContentFrameNavigated(sender As Object, e As NavigationEventArgs)
            ' try sync selected item
            Dim item As NavigationItem = NavigationItem.Items.FirstOrDefault(Function(i) e.SourcePageType.Equals(i.PageType) And i.PageParameter = e.Parameter)

            If Not item Is Nothing Then
                SelectedItem = item
            End If
        End Sub
    End Class
End Namespace

