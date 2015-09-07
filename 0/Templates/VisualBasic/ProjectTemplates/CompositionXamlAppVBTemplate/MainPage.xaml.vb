Imports System.Numerics
Imports Windows.UI
Imports Windows.UI.Composition
Imports Windows.UI.Xaml.Hosting

''' <summary>
''' A page with a container visual having a solid color background.
''' </summary>
Public NotInheritable Class MainPage
    Inherits Page

    Private container As ContainerVisual
    Private backgroundVisual As SolidColorVisual
    Private compositor As Compositor

    Private Sub UpdateSize()
        If backgroundVisual Is Nothing Or Host Is Nothing Then
            Return
        End If

        backgroundVisual.Size = New Vector2(Host.ActualWidth, Host.ActualHeight)
    End Sub

    Private Sub Host_Loaded(sender As Object, e As RoutedEventArgs)
        container = TryCast(ElementCompositionPreview.GetContainerVisual(Host), ContainerVisual)
        compositor = container.Compositor

        backgroundVisual = compositor.CreateSolidColorVisual()
        backgroundVisual.Color = Colors.LightGreen

        container.Children.InsertAtBottom(backgroundVisual)
        UpdateSize()
    End Sub

    Private Sub Host_Unloaded(sender As Object, e As RoutedEventArgs)
        backgroundVisual.Dispose()
        container.Dispose()
        compositor.Dispose()
    End Sub

    Private Sub Host_SizeChanged(sender As Object, e As SizeChangedEventArgs)
        UpdateSize()
    End Sub
End Class
