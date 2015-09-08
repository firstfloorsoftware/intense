Imports Windows.UI.Xaml.Markup

Namespace Presentation
    ''' <summary>
    ''' Represents a single item in a navigation hierarchy.
    ''' </summary>
    <ContentProperty(Name:="Items")>
    Public Class NavigationItem
        Inherits NotifyPropertyChanged

        Private _displayName As String
        Private _icon As String
        Private _description As String
        Private _pageType As Type
        Private _pageParameter As Object

        Private _items As NavigationItemCollection
        Private _parent As NavigationItem

        Public Sub New()
            _items = New NavigationItemCollection(Me)
        End Sub

        ''' <summary>
        ''' Gets or sets the display name.
        ''' </summary>
        ''' <returns></returns>
        Public Property DisplayName As String
            Get
                Return _displayName
            End Get
            Set(value As String)
                If (SetProperty(_displayName, value)) Then
                    OnPropertyChanged("DisplayNameUppercase")
                End If
            End Set
        End Property

        ''' <summary>
        ''' Get the uppercase variant of the display name.
        ''' </summary>
        ''' <returns></returns>
        Public ReadOnly Property DisplayNameUppercase As String
            Get
                Return If(_displayName, "").ToUpper()
            End Get
        End Property

        ''' <summary>
        ''' Gets or sets the icon.
        ''' </summary>
        ''' <returns></returns>
        Public Property Icon As String
            Get
                Return _icon
            End Get
            Set(value As String)
                SetProperty(_icon, value)
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets a description of the item.
        ''' </summary>
        ''' <returns></returns>
        Public Property Description As String
            Get
                Return _description
            End Get
            Set(value As String)
                SetProperty(_description, value)
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets the page type associated with the item.
        ''' </summary>
        ''' <returns></returns>
        Public Property PageType As Type
            Get
                Return _pageType
            End Get
            Set(value As Type)
                SetProperty(_pageType, value)
            End Set
        End Property

        ''' <summary>
        ''' Gets or set the parameter object that used when navigating to the page specified by <see cref="PageType"/>.
        ''' </summary>
        ''' <returns></returns>
        Public Property PageParameter As Object
            Get
                Return _pageParameter
            End Get
            Set(value As Object)
                SetProperty(_pageParameter, value)
            End Set
        End Property

        ''' <summary>
        ''' Gets the child navigation items.
        ''' </summary>
        ''' <returns></returns>
        Public ReadOnly Property Items As NavigationItemCollection
            Get
                Return _items
            End Get
        End Property

        ''' <summary>
        ''' Gets the parent navigation item.
        ''' </summary>
        ''' <returns></returns>
        Public Property Parent As NavigationItem
            Get
                Return _parent
            End Get
            Friend Set
                SetProperty(_parent, Value)
            End Set
        End Property
    End Class
End Namespace