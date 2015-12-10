Imports System.Numerics
Imports Windows.UI
Imports Windows.UI.Composition
Imports Windows.UI.Xaml.Hosting

''' <summary>
''' A page with a container visual having a solid color background.
''' </summary>
Public NotInheritable Class MainPage
    Inherits Page

    Private compositor As Compositor
    Private root As Visual
    Private backgroundVisual As SpriteVisual


    Private Sub UpdateSize()
        If backgroundVisual Is Nothing Or Host Is Nothing Then
            Return
        End If

        backgroundVisual.Size = New Vector2(Host.ActualWidth, Host.ActualHeight)
    End Sub

    Private Sub Host_Loaded(sender As Object, e As RoutedEventArgs)
        root = ElementCompositionPreview.GetElementVisual(Host)
        compositor = root.Compositor

        backgroundVisual = compositor.CreateSpriteVisual()
        backgroundVisual.Brush = compositor.CreateColorBrush(Colors.LightGreen)
        ElementCompositionPreview.SetElementChildVisual(Host, backgroundVisual)

        UpdateSize()
    End Sub

    Private Sub Host_SizeChanged(sender As Object, e As SizeChangedEventArgs)
        UpdateSize()
    End Sub
End Class
