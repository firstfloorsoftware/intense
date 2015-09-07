Namespace Presentation
    Public Class Command
        Implements ICommand

        Public Event CanExecuteChanged As EventHandler Implements ICommand.CanExecuteChanged

        Private _execute As Action
        Private _canExecute As Func(Of Boolean)

        Public Sub New(execute As Action, Optional canExecute As Func(Of Boolean) = Nothing)
            _execute = execute
            _canExecute = If(canExecute Is Nothing, Function() True, canExecute)
        End Sub

        Public Sub OnCanExecuteChanged()
            RaiseEvent CanExecuteChanged(Me, EventArgs.Empty)
        End Sub

        Public Function CanExecute(parameter As Object) As Boolean Implements ICommand.CanExecute
            Return _canExecute()
        End Function

        Public Sub Execute(parameter As Object) Implements ICommand.Execute
            If CanExecute(parameter) Then
                _execute()
            End If
        End Sub
    End Class
End Namespace
