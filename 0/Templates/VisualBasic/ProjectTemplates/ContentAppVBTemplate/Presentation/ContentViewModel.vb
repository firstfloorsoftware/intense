Namespace Presentation
    Public Class ContentViewModel
        Inherits NotifyPropertyChanged

        Private _isPaneOpen As Boolean
        Private _isPanePinned As Boolean
        Private _showPaneCommand As ICommand
        Private _hidePaneCommand As ICommand
        Private _pinPaneCommand As ICommand

        Public Sub New()
            _showPaneCommand = New Command(Sub() IsPaneOpen = True)
            _hidePaneCommand = New Command(Sub() IsPaneOpen = False)
            _pinPaneCommand = New Command(Sub() IsPanePinned = True)
        End Sub

        Public ReadOnly Property ShowPaneCommand As ICommand
            Get
                Return _showPaneCommand
            End Get
        End Property

        Public ReadOnly Property HidePaneCommand As ICommand
            Get
                Return _hidePaneCommand
            End Get
        End Property

        Public ReadOnly Property PinPaneCommand As ICommand
            Get
                Return _pinPaneCommand
            End Get
        End Property
        Public Property IsPaneOpen As Boolean
            Get
                Return _isPaneOpen
            End Get
            Set
                If SetProperty(_isPaneOpen, Value) And Not Value Then
                    ' unpin pane when closed
                    IsPanePinned = False
                End If
            End Set
        End Property

        Public Property IsPanePinned As Boolean
            Get
                Return _isPanePinned
            End Get
            Set
                If SetProperty(_isPanePinned, Value) Then
                    OnPropertyChanged("PaneDisplayMode")
                End If
            End Set
        End Property

        Public ReadOnly Property PaneDisplayMode As SplitViewDisplayMode
            Get
                If _isPanePinned Then
                    Return SplitViewDisplayMode.Inline
                End If
                Return SplitViewDisplayMode.Overlay
            End Get
        End Property
    End Class
End Namespace



