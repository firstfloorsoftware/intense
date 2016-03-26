Imports System.Collections.Immutable
Imports Intense.Presentation
Imports Intense.UI

Namespace Presentation
    Public Class SettingsViewModel
        Inherits NotifyPropertyChanged

        Private _themes As IReadOnlyList(Of DisplayableTheme)
        Private _selectedTheme As DisplayableTheme
        Private _brushes As IReadOnlyList(Of SolidColorBrush)
        Private _selectedBrush As SolidColorBrush
        Private _useSystemAccentColor As Boolean

        Public Sub New()
            _brushes = AccentColors.Windows10.Select(Function(c)
                                                         Return New SolidColorBrush(c)
                                                     End Function).ToImmutableList()
            _themes = ImmutableList.Create(
                New DisplayableTheme("Dark", ApplicationTheme.Dark),
                New DisplayableTheme("Light", ApplicationTheme.Light))

            Dim manager = AppearanceManager.GetForCurrentView()
            _selectedTheme = _themes.FirstOrDefault(Function(t)
                                                        Return t.Theme = manager.Theme
                                                    End Function)

            If (AppearanceManager.AccentColor Is Nothing) Then
                _useSystemAccentColor = True
            Else
                _selectedBrush = _brushes.FirstOrDefault(Function(b)
                                                             Return b.Color = AppearanceManager.AccentColor
                                                         End Function)
            End If
        End Sub

        Public ReadOnly Property Brushes As IReadOnlyList(Of SolidColorBrush)
            Get
                Return _brushes
            End Get
        End Property

        Public Property SelectedBrush As SolidColorBrush
            Get
                Return _selectedBrush
            End Get
            Set
                If Me.Set(_selectedBrush, Value) And Not _useSystemAccentColor And Not Value Is Nothing Then
                    AppearanceManager.AccentColor = Value.Color
                End If
            End Set
        End Property

        Public ReadOnly Property Themes As IReadOnlyList(Of DisplayableTheme)
            Get
                Return _themes
            End Get
        End Property

        Public Property SelectedTheme As DisplayableTheme
            Get
                Return _selectedTheme
            End Get
            Set
                If Me.Set(_selectedTheme, Value) And Not Value Is Nothing Then
                    AppearanceManager.GetForCurrentView().Theme = Value.Theme
                End If
            End Set
        End Property

        Public Property UseSystemAccentColor As Boolean
            Get
                Return _useSystemAccentColor
            End Get
            Set
                If Me.Set(_useSystemAccentColor, Value) Then
                    If (Value) Then
                        AppearanceManager.AccentColor = Nothing
                    ElseIf Not _selectedBrush Is Nothing Then
                        AppearanceManager.AccentColor = _selectedBrush.Color
                    End If
                End If
            End Set
        End Property

    End Class
End Namespace



