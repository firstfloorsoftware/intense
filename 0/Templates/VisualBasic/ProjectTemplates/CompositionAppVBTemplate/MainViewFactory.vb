Imports Windows.ApplicationModel.Core

Public Class MainViewFactory
    Implements IFrameworkViewSource

    Public Function CreateView() As IFrameworkView Implements IFrameworkViewSource.CreateView
        Return New MainView
    End Function

    Shared Sub Main()
        CoreApplication.Run(New MainViewFactory())
    End Sub
End Class
