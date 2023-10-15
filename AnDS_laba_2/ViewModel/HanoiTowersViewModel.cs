using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Shapes;
using AnDS_laba_2.Model;
using AnDS_laba_2.View;

namespace AnDS_laba_2.ViewModel;

public sealed class HanoiTowersViewModel : INotifyPropertyChanged
{
    private readonly HanoiTowersPage _page = null!;
    private readonly List<(int from, int to)> _steps = new();
    private List<string> _stepStrings = null!;
    private bool _canDoNextStep;
    private int _currentStep;
    private HanoiTowersMode _mode;
    private int _ringsCount;

    public bool IsDoing { get; private set; }

    private bool CanDoNextStep
    {
        get => _canDoNextStep;
        set
        {
            _canDoNextStep = value;
            OnPropertyChanged();
        }
    }

    public ICommand NextStepCommand { get; init; } = null!;

    public HanoiTowersViewModel(HanoiTowersPage page)
    {
        _page = page;
        NextStepCommand = new RelayCommand(NextStep, 
            _ => CanDoNextStep && _mode == HanoiTowersMode.NotAuto);
    }

    public HanoiTowersViewModel() { }

    private void Init()
    {
        _steps.Clear();
        _currentStep = 0;
        HanoiTower.DrawRings(_ringsCount, 700.0 / 3 + 50, _page.Pole1);
        HanoiTower.MoveDisks(1, 2, 3, _ringsCount, _steps);
        _stepStrings = _steps.Select(step => $"{step.from} => {step.to}").ToList();
    }

    private void NextStep(object? o)
        => NextStepAsync();

    private async Task NextStepAsync()
    {
        try
        {
            if (!IsDoing)
            {
                IsDoing = true;
            }
            CanDoNextStep = false;
            var step = _steps[_currentStep];
            _stepStrings.Add($"{step.from} => {step.to}");
            _currentStep++;
            var ring = GetRingFrom(step.from);
            await MoveRing(step.from, step.to, ring);
            CanDoNextStep = true;
        }
        catch (Exception)
        {
            MessageBox.Show("Algorithm is already completed!");
            IsDoing = false;
        }
    }

    private Rectangle GetRingFrom(int poleNumber)
    {
        Rectangle ring = null!;
        switch (poleNumber)
        {
            case 1:
                ring = _page.Pole1.Children[0] as Rectangle ?? throw new Exception();
                _page.Pole1.Children.Remove(ring);
                return ring;
            case 2:
                ring = _page.Pole2.Children[0] as Rectangle ?? throw new Exception();
                _page.Pole2.Children.Remove(ring);
                return ring;
            case 3:
                ring = _page.Pole3.Children[0] as Rectangle ?? throw new Exception();
                _page.Pole3.Children.Remove(ring);
                return ring;
            default:
                return ring;
        }
    }

    private async Task MoveRing(int from, int to, UIElement ring)
    {
        switch (from)
        {
            case 1:
                Grid.SetColumn(ring, 1);
                Grid.SetColumnSpan(ring, 3);
                break;
            case 2:
                Grid.SetColumn(ring, 5);
                Grid.SetColumnSpan(ring, 3);
                break;
            case 3:
                Grid.SetColumn(ring, 9);
                Grid.SetColumnSpan(ring, 3);
                break;
        }

        Grid.SetRow(ring, 0);
        _page.Grid.Children.Add(ring);
        await Task.Delay(500);
        
        _page.Grid.Children.Remove(ring);
        switch (to)
        {
            case 1:
                Grid.SetColumn(ring, 1);
                Grid.SetColumnSpan(ring, 3);
                break;
            case 2:
                Grid.SetColumn(ring, 5);
                Grid.SetColumnSpan(ring, 3);
                break;
            case 3:
                Grid.SetColumn(ring, 9);
                Grid.SetColumnSpan(ring, 3);
                break;
        }

        _page.Grid.Children.Add(ring);
        await Task.Delay(500);
        
        _page.Grid.Children.Remove(ring);
        switch (to)
        {
            case 1:
                _page.Pole1.Children.Insert(0, ring);
                break;
            case 2:
                _page.Pole2.Children.Insert(0, ring);
                break;
            case 3:
                _page.Pole3.Children.Insert(0, ring);
                break;
        }
    }

    private async void Auto()
    {
        IsDoing = true;
        foreach (var _ in _steps)
        {
            await NextStepAsync();
            await Task.Delay(500);
        }

        IsDoing = false;
    }

    public void Start(HanoiTowersMode mode, int ringsCount)
    {
        _page.Pole1.Children.Clear();
        _page.Pole2.Children.Clear();
        _page.Pole3.Children.Clear();
        _mode = mode;
        _ringsCount = ringsCount;
        Init();
        if (_mode == HanoiTowersMode.Auto)
        {
            Auto();
        }
        else
        {
            CanDoNextStep = true;
        }
    }
    
    public event PropertyChangedEventHandler? PropertyChanged;

    private void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}