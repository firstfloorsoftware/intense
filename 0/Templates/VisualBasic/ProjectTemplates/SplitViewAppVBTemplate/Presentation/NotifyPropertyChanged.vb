Namespace Presentation
    Public Class NotifyPropertyChanged
        Implements INotifyPropertyChanged

        Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged

        Protected Sub OnPropertyChanged(<CallerMemberName> Optional propertyName As String = Nothing)
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(propertyName))
        End Sub

        Public Function SetProperty(Of T)(ByRef storage As T, value As T, <CallerMemberName> Optional propertyName As String = Nothing) As Boolean
            If Not Object.Equals(storage, value) Then
                storage = value
                OnPropertyChanged(propertyName)
                Return True
            End If
            Return False
        End Function
    End Class
End Namespace

