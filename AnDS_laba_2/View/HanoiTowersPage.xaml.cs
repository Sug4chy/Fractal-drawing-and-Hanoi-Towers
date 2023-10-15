using AnDS_laba_2.ViewModel;

namespace AnDS_laba_2.View;

public partial class HanoiTowersPage
{
    public HanoiTowersPage()
    {
        InitializeComponent();
        DataContext = new HanoiTowersViewModel();
    }
}