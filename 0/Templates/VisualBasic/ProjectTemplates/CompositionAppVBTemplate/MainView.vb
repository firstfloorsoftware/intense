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
    Private rootVisual As ContainerVisual
    Private background As SolidColorVisual

    Private Sub InitializeComposition()
        ' setup compositor And root visual
        compositor = New Compositor()
        rootVisual = compositor.CreateContainerVisual()

        ' associate with the CoreWindow
        compositionTarget = compositor.CreateTargetForCurrentView()
        compositionTarget.Root = rootVisual

        ' add a solid color background
        background = compositor.CreateSolidColorVisual()
        background.Color = Colors.LightGreen
        rootVisual.Children.InsertAtBottom(background)

        UpdateSize()
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
