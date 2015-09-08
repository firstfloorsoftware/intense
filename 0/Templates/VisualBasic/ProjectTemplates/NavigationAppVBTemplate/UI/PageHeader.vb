Namespace UI
    ''' <summary>
    ''' The page header control with support for search.
    ''' </summary>
    Public Class PageHeader
        Inherits Control

        Public Shared ReadOnly FrameProperty As DependencyProperty = DependencyProperty.Register("Frame", GetType(Frame), GetType(PageHeader), New PropertyMetadata(Nothing, AddressOf OnFrameChanged))
        Public Shared ReadOnly IconProperty As DependencyProperty = DependencyProperty.Register("Icon", GetType(String), GetType(PageHeader), Nothing)
        Public Shared ReadOnly IsSearchBoxVisibleProperty As DependencyProperty = DependencyProperty.Register("IsSearchBoxVisible", GetType(Boolean), GetType(PageHeader), New PropertyMetadata(True))
        Public Shared ReadOnly SearchTermProperty As DependencyProperty = DependencyProperty.Register("SearchTerm", GetType(String), GetType(PageHeader), Nothing)
        Public Shared ReadOnly TitleProperty As DependencyProperty = DependencyProperty.Register("Title", GetType(String), GetType(PageHeader), Nothing)

        Private _iconButton As Button

        Public Sub New()
            DefaultStyleKey = GetType(PageHeader)
        End Sub

        Private Shared Sub OnFrameChanged(o As DependencyObject, args As DependencyPropertyChangedEventArgs)
            TryCast(o, PageHeader).OnFrameChanged()
        End Sub

        Private Sub OnFrameChanged()
            UpdateIconButtonState()
        End Sub

        Protected Overrides Sub OnApplyTemplate()
            MyBase.OnApplyTemplate()

            If Not _iconButton Is Nothing Then
                RemoveHandler _iconButton.Click, AddressOf OnIconButtonClick
            End If

            _iconButton = TryCast(GetTemplateChild("IconButton"), Button)
            If Not _iconButton Is Nothing Then
                AddHandler _iconButton.Click, AddressOf OnIconButtonClick
            End If
        End Sub

        Private Sub OnIconButtonClick(sender As Object, e As RoutedEventArgs)
            If Frame Is Nothing Then Return

            ' attemp to navigate back to the first element in the frame's navigation stack
            While Frame.CanGoBack
                Frame.GoBack()
            End While
        End Sub

        Private Sub UpdateIconButtonState()
            If _iconButton Is Nothing Or Frame Is Nothing Then Return

            _iconButton.IsEnabled = Frame.CanGoBack
        End Sub

        Public Property Frame As Frame
            Get
                Return GetValue(FrameProperty)
            End Get
            Set(value As Frame)
                SetValue(FrameProperty, value)
            End Set
        End Property

        Public Property IsSearchBoxVisible As Boolean
            Get
                Return GetValue(IsSearchBoxVisibleProperty)
            End Get
            Set(value As Boolean)
                SetValue(IsSearchBoxVisibleProperty, value)
            End Set
        End Property

        Public Property Icon As String
            Get
                Return GetValue(IconProperty)
            End Get
            Set(value As String)
                SetValue(IconProperty, value)
            End Set
        End Property

        Public Property SearchTerm As String
            Get
                Return GetValue(SearchTermProperty)
            End Get
            Set(value As String)
                SetValue(SearchTermProperty, value)
            End Set
        End Property

        Public Property Title As String
            Get
                Return GetValue(TitleProperty)
            End Get
            Set(value As String)
                SetValue(TitleProperty, value)
            End Set
        End Property
    End Class
End Namespace

