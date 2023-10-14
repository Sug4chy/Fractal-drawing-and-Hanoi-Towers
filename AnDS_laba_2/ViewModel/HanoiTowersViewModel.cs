using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace AnDS_laba_2.ViewModel;

public sealed class HanoiTowersViewModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;

    private void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}