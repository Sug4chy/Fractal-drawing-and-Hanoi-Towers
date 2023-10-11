using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using AnDS_laba_2.Model;

namespace AnDS_laba_2.ViewModel;

public sealed class MainViewModel : INotifyPropertyChanged
{
    private readonly MainWindow _mainWindow = null!;
    private readonly BinaryTreeFractal _fractal = new();
    private bool _mode;
    
    public ICommand DrawFractalCommand { get; init; } = null!;
    public ICommand EnableFractalModeCommand { get; init; } = null!;
    public ICommand EnableHanoiTowerModeCommand { get; init; } = null!;

    public MainViewModel() { }

    public MainViewModel(MainWindow mainWindow)
    {
        _mainWindow = mainWindow;
        DrawFractalCommand = new RelayCommand(DrawBinaryTree);
        EnableFractalModeCommand = new RelayCommand(_ => { _mode = false; }, _ => _mode);
        EnableHanoiTowerModeCommand = new RelayCommand(_ => { _mode = true; }, _ => !_mode);
    }

    private async void DrawBinaryTree(object? o)
    {
        _mainWindow.Canvas.Children.Clear();
        await Task.Run(() =>
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                _fractal.Draw(_mainWindow.Canvas, 17);
            });
        });
    }
    
    public event PropertyChangedEventHandler? PropertyChanged;

    private void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}