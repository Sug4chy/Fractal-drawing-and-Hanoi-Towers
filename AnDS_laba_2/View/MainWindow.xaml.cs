using AnDS_laba_2.ViewModel;

namespace AnDS_laba_2.View;

public partial class MainWindow
{
    public MainWindow()
    {
        InitializeComponent();
        DataContext = new MainViewModel(this);
    }
}