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
    Private root As ContainerVisual
    Private backgroundVisual As SpriteVisual
    Private target As SpriteVisual

    Private Sub UpdateSize()
        If Host Is Nothing Then Return
        If Not backgroundVisual Is Nothing Then
            backgroundVisual.Size = New Vector2(Host.ActualWidth, Host.ActualHeight)
        End If
        If Not target Is Nothing Then
            target.Offset = New Vector3(Host.ActualWidth / 2 - 75, Host.ActualHeight / 2 - 75, 0.0F)
        End If
    End Sub

    Private Sub Host_Loaded(sender As Object, e As RoutedEventArgs)
        Dim visual = ElementCompositionPreview.GetElementVisual(Host)
        compositor = visual.Compositor

        ' create root container
        root = compositor.CreateContainerVisual()
        ElementCompositionPreview.SetElementChildVisual(Host, root)

        ' create background
        backgroundVisual = compositor.CreateSpriteVisual()
        backgroundVisual.Brush = compositor.CreateColorBrush(Colors.LightGreen)
        root.Children.InsertAtBottom(backgroundVisual)

        ' create green square
        target = compositor.CreateSpriteVisual()
        target.Brush = compositor.CreateColorBrush(Colors.Green)
        target.Size = New Vector2(150.0F, 150.0F)
        target.CenterPoint = New Vector3(75.0F, 75.0F, 0.0F)
        root.Children.InsertAtTop(target)

        UpdateSize()
    End Sub

    Private Sub Host_SizeChanged(sender As Object, e As SizeChangedEventArgs)
        UpdateSize()
    End Sub

    Private Sub Start_Click(sender As Object, e As RoutedEventArgs)
        Dim easing = compositor.CreateCubicBezierEasingFunction(New Vector2(0.5F, 0.1F), New Vector2(0.5F, 0.75F))
        Dim animation = compositor.CreateScalarKeyFrameAnimation()

        animation.InsertKeyFrame(0.00F, 0.00F, easing)
        animation.InsertKeyFrame(1.0F, 360.0F, easing)

        animation.Duration = TimeSpan.FromMilliseconds(2000)
        animation.IterationBehavior = AnimationIterationBehavior.Forever

        target.StartAnimation("RotationAngleinDegrees", animation)
    End Sub

    Private Sub Stop_Click(sender As Object, e As RoutedEventArgs)
        target.StopAnimation("RotationAngleinDegrees")
    End Sub
End Class
