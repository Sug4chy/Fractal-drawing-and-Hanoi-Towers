using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using AnDS_laba_2.Model;
using AnDS_laba_2.View;

namespace AnDS_laba_2.ViewModel;

public sealed class MainViewModel : INotifyPropertyChanged
{
    private readonly MainWindow _mainWindow = null!;
    private readonly BinaryTreeFractal _fractal = new();
    private bool _mode;
    private int _sliderValue = 1;
    private bool _isDrawing;

    public int SliderValue
    {
        get => _sliderValue;
        set
        {
            _sliderValue = value;
            OnPropertyChanged();
        }
    }

    private bool IsDrawing
    {
        get => _isDrawing;
        set
        {
            _isDrawing = value;
            OnPropertyChanged();
        }
    }
    
    public ICommand DrawFractalCommand { get; init; } = null!;
    public ICommand EnableFractalModeCommand { get; init; } = null!;
    public ICommand EnableHanoiTowerModeCommand { get; init; } = null!;

    public MainViewModel() { }

    public MainViewModel(MainWindow mainWindow)
    {
        _mainWindow = mainWindow;
        DrawFractalCommand = new RelayCommand(DrawBinaryTree, _ => !IsDrawing);
        EnableFractalModeCommand = new RelayCommand(EnableFractalMode, _ => _mode);
        EnableHanoiTowerModeCommand = new RelayCommand(EnableHanoiTowerMode, _ => !_mode);
    }

    private void EnableFractalMode(object? o)
    {
        _mainWindow.FractalPanel.Visibility = Visibility.Visible;
        _mainWindow.Canvas.Visibility = Visibility.Visible;
        _mode = false;
    }

    private void EnableHanoiTowerMode(object? o)
    {
        _mainWindow.FractalPanel.Visibility = Visibility.Collapsed;
        _mainWindow.Canvas.Visibility = Visibility.Collapsed;
        _mode = true;
    }

    private async void DrawBinaryTree(object? o)
    {
        _mainWindow.Canvas.Children.Clear();
        IsDrawing = true;
        await Task.Run(() =>
        {
            Application.Current.Dispatcher.Invoke(async () =>
            {
                await _fractal.Draw(_mainWindow.Canvas, _sliderValue);
                IsDrawing = false;
            });
        });
    }
    
    public event PropertyChangedEventHandler? PropertyChanged;

    private void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}