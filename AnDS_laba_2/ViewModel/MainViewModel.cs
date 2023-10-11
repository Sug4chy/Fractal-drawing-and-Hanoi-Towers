using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace AnDS_laba_2.ViewModel;

public sealed class MainViewModel : INotifyPropertyChanged
{
    private readonly MainWindow _mainWindow = null!;
    private bool _mode = false;
    
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

    private void DrawBinaryTree(object? o)
    {
        
    }
    
    public event PropertyChangedEventHandler? PropertyChanged;

    private void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}