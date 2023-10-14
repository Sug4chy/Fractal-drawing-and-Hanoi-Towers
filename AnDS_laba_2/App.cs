using System.Windows;
using AnDS_laba_2.View;

namespace AnDS_laba_2;

public class App : Application
{
    private readonly MainWindow _mainWindow;

    public App(MainWindow mainWindow)
    {
        _mainWindow = mainWindow;
    }

    protected override void OnStartup(StartupEventArgs e)
    {
        _mainWindow.Show();
        base.OnStartup(e);
    }
}