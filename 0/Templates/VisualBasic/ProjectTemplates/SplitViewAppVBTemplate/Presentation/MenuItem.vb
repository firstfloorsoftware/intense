Namespace Presentation
    Public Class MenuItem
        Inherits NotifyPropertyChanged

        Private _icon As String
        Private _title As String
        Private _pageType As Type

        Public Property Icon As String
            Get
                Return _icon
            End Get
            Set
                SetProperty(_icon, Value)
            End Set
        End Property

        Public Property Title As String
            Get
                Return _title
            End Get
            Set
                SetProperty(_title, Value)
            End Set
        End Property

        Public Property PageType As Type
            Get
                Return _pageType
            End Get
            Set
                SetProperty(_pageType, Value)
            End Set
        End Property

    End Class
End Namespace

