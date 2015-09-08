Imports $safeprojectname$.Presentation

Namespace UI
    ''' <summary>
    ''' Represents the base implementation of a page featuring a list of navigation items.
    ''' </summary>
    Public MustInherit Class NavigationPage
        Inherits Page

        Public Const WindowStateWide As String = "WideState"
        Public Const WindowStateNarrow As String = "NarrowState"

        Public Shared ReadOnly NavigationItemProperty As DependencyProperty = DependencyProperty.Register("NavigationItem", GetType(NavigationItem), GetType(NavigationPage), New PropertyMetadata(Nothing, AddressOf OnNavigationItemChanged))
        Public Shared ReadOnly RootItemProperty As DependencyProperty = DependencyProperty.Register("RootItem", GetType(NavigationItem), GetType(NavigationPage), Nothing)
        Public Shared ReadOnly SelectedItemProperty As DependencyProperty = DependencyProperty.Register("SelectedItem", GetType(NavigationItem), GetType(NavigationPage), New PropertyMetadata(Nothing, AddressOf OnSelectedItemChanged))
        Public Shared ReadOnly WindowStateProperty As DependencyProperty = DependencyProperty.Register("WindowState", GetType(String), GetType(NavigationPage), New PropertyMetadata(Nothing, AddressOf OnWindowStateChanged))


        Private Shared Sub OnNavigationItemChanged(o As DependencyObject, args As DependencyPropertyChangedEventArgs)
            Dim page As NavigationPage = TryCast(o, NavigationPage)

            ' delay invoke changed handler to ensure any dependent bindings are finalized
            Dim scheduler As TaskScheduler = TaskScheduler.FromCurrentSynchronizationContext
            Task.Delay(10).ContinueWith(Sub(t) page.OnNavigationItemChanged(TryCast(args.OldValue, NavigationItem), TryCast(args.NewValue, NavigationItem)), scheduler)
        End Sub

        Protected Overridable Sub OnNavigationItemChanged(oldValue As NavigationItem, newValue As NavigationItem)
            ' update root
            If newValue Is Nothing Then
                RootItem = Nothing
            Else
                RootItem = newValue.GetAncestorsAndSelf().LastOrDefault()
            End If
        End Sub

        Private Shared Sub OnSelectedItemChanged(o As DependencyObject, args As DependencyPropertyChangedEventArgs)
            Dim page As NavigationPage = TryCast(o, NavigationPage)
            If Not page.NavigationItem Is Nothing Then
                page.OnSelectedItemChanged(TryCast(args.OldValue, NavigationItem), TryCast(args.NewValue, NavigationItem))
            End If
        End Sub

        Protected Overridable Sub OnSelectedItemChanged(oldValue As NavigationItem, newValue As NavigationItem)
            If Frame Is Nothing Then Return

            If Not newValue Is Nothing Then
                ' navigate to selected item
                Dim pageType As Type = GetType(MasterPage)
                If IsMasterDetailCandidate(newValue) Then
                    pageType = GetType(MasterDetailPage)
                End If

                Frame.Navigate(pageType, newValue)
            End If
        End Sub

        Private Shared Sub OnWindowStateChanged(o As DependencyObject, args As DependencyPropertyChangedEventArgs)
            Dim page As NavigationPage = TryCast(o, NavigationPage)
            page.OnWindowStateChanged(TryCast(args.OldValue, String), TryCast(args.NewValue, String))
        End Sub

        Protected Overridable Sub OnWindowStateChanged(oldValue As String, newValue As String)

        End Sub

        Protected Function IsMasterDetailCandidate(item As NavigationItem) As Boolean
            Return Not item.IsRoot() And (item.IsLeaf Or (Not item.HasGrandchildren() And WindowState = WindowStateWide))
        End Function

        Protected Overrides Sub OnNavigatedTo(e As NavigationEventArgs)
            MyBase.OnNavigatedTo(e)

            NavigationItem = TryCast(e.Parameter, NavigationItem)
        End Sub

        Protected Overrides Sub OnNavigatedFrom(e As NavigationEventArgs)
            MyBase.OnNavigatedFrom(e)

            NavigationItem = Nothing
        End Sub

        ''' <summary>
        ''' Gets or sets the navigation item associated with this instance.
        ''' </summary>
        ''' <returns></returns>
        Public Property NavigationItem As NavigationItem
            Get
                Return GetValue(NavigationItemProperty)
            End Get
            Set(value As NavigationItem)
                SetValue(NavigationItemProperty, value)
            End Set
        End Property

        ''' <summary>
        ''' Gets the root item.
        ''' </summary>
        ''' <returns></returns>
        Public Property RootItem As NavigationItem
            Get
                Return GetValue(RootItemProperty)
            End Get
            Private Set(value As NavigationItem)
                SetValue(RootItemProperty, value)
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets the selected item associated with this instance.
        ''' </summary>
        ''' <returns></returns>
        Public Property SelectedItem As NavigationItem
            Get
                Return GetValue(SelectedItemProperty)
            End Get
            Set(value As NavigationItem)
                SetValue(SelectedItemProperty, value)
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets the window state.
        ''' </summary>
        ''' <returns></returns>
        Public Property WindowState As String
            Get
                Return GetValue(WindowStateProperty)
            End Get
            Set(value As String)
                SetValue(WindowStateProperty, value)
            End Set
        End Property
    End Class
End Namespace
