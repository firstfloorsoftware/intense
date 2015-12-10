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
        If backgroundVisual Is Nothing Or Host Is Nothing Then
            Return
        End If

        backgroundVisual.Size = New Vector2(Host.ActualWidth, Host.ActualHeight)
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
        target.Offset = New Vector3(250.0F, 250.0F, 0.0F)
        target.CenterPoint = New Vector3(75.0F, 75.0F, 0.0F)
        root.Children.InsertAtTop(target)

        ' animate square
        Animate(target)

        UpdateSize()
    End Sub

    Private Sub Animate(visual As Visual)
        Dim easing = compositor.CreateCubicBezierEasingFunction(New Vector2(0.5F, 0.1F), New Vector2(0.5F, 0.75F))
        Dim animation = compositor.CreateScalarKeyFrameAnimation()

        animation.InsertKeyFrame(0.00F, 0.00F, easing)
        animation.InsertKeyFrame(1.0F, 360.0F, easing)

        animation.Duration = TimeSpan.FromMilliseconds(2000)
        animation.IterationBehavior = AnimationIterationBehavior.Forever

        visual.StartAnimation("RotationAngleinDegrees", animation)
    End Sub

    Private Sub Host_SizeChanged(sender As Object, e As SizeChangedEventArgs)
        UpdateSize()
    End Sub
End Class
