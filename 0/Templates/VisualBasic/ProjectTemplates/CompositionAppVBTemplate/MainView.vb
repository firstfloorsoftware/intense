Imports System.Numerics
Imports Windows.ApplicationModel.Core
Imports Windows.UI
Imports Windows.UI.Composition
Imports Windows.UI.Core

Public Class MainView
    Implements IFrameworkView

    Private view As CoreApplicationView
    Private window As CoreWindow
    Private compositor As Compositor
    Private compositionTarget As CompositionTarget
    Private root As ContainerVisual
    Private background As SpriteVisual
    Private target As SpriteVisual

    Private Sub InitializeComposition()
        ' setup compositor And root visual
        compositor = New Compositor()
        root = compositor.CreateContainerVisual()

        ' associate with the CoreWindow
        compositionTarget = compositor.CreateTargetForCurrentView()
        compositionTarget.Root = root

        ' add a solid color background
        background = compositor.CreateSpriteVisual()
        background.Brush = compositor.CreateColorBrush(Colors.LightGreen)
        root.Children.InsertAtBottom(background)

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

    Private Sub UpdateSize()
        If (background Is Nothing Or window Is Nothing) Then
            Return
        End If

        background.Size = New Vector2(window.Bounds.Width, window.Bounds.Height)
    End Sub

    Private Sub OnWindowSizeChanged(sender As CoreWindow, args As WindowSizeChangedEventArgs)
        UpdateSize()
    End Sub

    Public Sub Initialize(applicationView As CoreApplicationView) Implements IFrameworkView.Initialize
        view = applicationView
    End Sub

    Public Sub Load(entryPoint As String) Implements IFrameworkView.Load
        ' not used
    End Sub

    Public Sub Run() Implements IFrameworkView.Run
        window.Activate()
        window.Dispatcher.ProcessEvents(CoreProcessEventsOption.ProcessUntilQuit)
    End Sub

    Public Sub SetWindow(window As CoreWindow) Implements IFrameworkView.SetWindow
        Me.window = window
        AddHandler window.SizeChanged, AddressOf OnWindowSizeChanged

        InitializeComposition()
    End Sub

    Public Sub Uninitialize() Implements IFrameworkView.Uninitialize
        window = Nothing
        view = Nothing
    End Sub
End Class
