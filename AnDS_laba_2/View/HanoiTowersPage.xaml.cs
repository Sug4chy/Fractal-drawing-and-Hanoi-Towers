using System.Runtime.CompilerServices;
using AnDS_laba_2.ViewModel;

namespace AnDS_laba_2.View;

public partial class HanoiTowersPage
{
    public HanoiTowersPage()
    {
        InitializeComponent();
        DataContext = new HanoiTowersViewModel(this);
    }

    public void StartDoing(HanoiTowersMode mode, int ringsCount)
    {
        (DataContext as HanoiTowersViewModel)!.Start(mode, ringsCount);
    }
}