using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using AnDS_laba_2.Model;
using AnDS_laba_2.View;

namespace AnDS_laba_2.ViewModel;

public sealed class MainViewModel : INotifyPropertyChanged
{
    private readonly MainWindow _mainWindow = null!;
    private HanoiTowersPage _page = null!;
    private bool _mode;
    private int _sliderValue = 1;
    private bool _isDrawing;
    private int _ringsCountValue;
    private bool _checkBoxValue = true;

    public ObservableCollection<string> StepsAsStrings
    {
        get
        {
            var steps = new List<(int from, int to)>();
            HanoiTower.MoveDisks(1, 2, 3, _ringsCountValue, steps);
            return new ObservableCollection<string>(
                steps.Select(step => $"{step.from} => {step.to}"));
        }
    }

    public bool CheckBoxValue
    {
        get => _checkBoxValue;
        set
        {
            _checkBoxValue = value;
            OnPropertyChanged();
        }
    }

    public int RingsCountValue
    {
        get => _ringsCountValue;
        set
        {
            _ringsCountValue = value;
            OnPropertyChanged();
        }
    }

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
    public ICommand StartTowersCommand { get; private set; } = null!;

    public MainViewModel() { }

    public MainViewModel(MainWindow mainWindow)
    {
        _mainWindow = mainWindow;
        DrawFractalCommand = new RelayCommand(DrawBinaryTree, _ => !IsDrawing);
        EnableFractalModeCommand = new RelayCommand(EnableFractalMode, _ => _mode);
        EnableHanoiTowerModeCommand = new RelayCommand(EnableHanoiTowerMode, _ => !_mode);
        StartTowersCommand = new RelayCommand(StartTowers);
    }

    private void EnableFractalMode(object? o)
    {
        _mainWindow.Frame.Content = null;
        _page = null!;
        _mainWindow.Frame.Visibility = Visibility.Collapsed;
        _mainWindow.TowersPanel.Visibility = Visibility.Collapsed;
        _mainWindow.TowersSteps.Visibility = Visibility.Collapsed;
        
        _mainWindow.FractalPanel.Visibility = Visibility.Visible;
        _mainWindow.Canvas.Visibility = Visibility.Visible;
        
        _mode = false;
    }

    private void EnableHanoiTowerMode(object? o)
    {
        _mainWindow.Frame.Visibility = Visibility.Visible;
        _page = new HanoiTowersPage();
        _mainWindow.Frame.Content = _page;
        _mainWindow.TowersPanel.Visibility = Visibility.Visible;
        _mainWindow.TowersSteps.Visibility = Visibility.Visible;
        
        _mainWindow.FractalPanel.Visibility = Visibility.Collapsed;
        _mainWindow.Canvas.Visibility = Visibility.Collapsed;
        
        _mode = true;
        StartTowersCommand = new RelayCommand(StartTowers, 
            _ => !IsDrawing || !(_page.DataContext as HanoiTowersViewModel)!.IsDoing);
    }

    private async void DrawBinaryTree(object? o)
    {
        _mainWindow.Canvas.Children.Clear();
        IsDrawing = true;
        await Task.Run(() =>
        {
            Application.Current.Dispatcher.Invoke(async () =>
            {
                await BinaryTreeFractal.Draw(_mainWindow.Canvas, _sliderValue);
                IsDrawing = false;
            });
        });
    }

    private void StartTowers(object? o)
    {
        _mainWindow.TowersSteps.ItemsSource = StepsAsStrings;
        var mode = CheckBoxValue ? HanoiTowersMode.Auto : HanoiTowersMode.NotAuto;
        _page.StartDoing(mode, RingsCountValue);
    }
    
    public event PropertyChangedEventHandler? PropertyChanged;

    private void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}