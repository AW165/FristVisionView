using System.Security.Permissions;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
namespace FirstVisionView;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private DatePage _DatePage;
    private AdjustPage _AdjustPage;
    private SettingPage _SettingPage;
    public MainWindow()
    {
        InitializeComponent();
        _AdjustPage = new AdjustPage();
        Page.Content = _AdjustPage;
        _DatePage = new DatePage();
        _SettingPage = new SettingPage();
        
    }

    private void Button_Click(object sender, RoutedEventArgs e)
    {
        if (_DatePage != null) { Page.Content = _DatePage; return; }
        _DatePage = new DatePage();
        Page.Content = _DatePage;

    }

    private void Button_Click_1(object sender, RoutedEventArgs e)
    {
        if (_AdjustPage != null) {Page.Content = _AdjustPage; ; return; }
        _AdjustPage = new AdjustPage();
        Page.Content = _AdjustPage;
    }

    private void Setting_Click(object sender, RoutedEventArgs e)
    {
        if (_SettingPage != null) { Page.Content = _SettingPage; ; return; }
        _SettingPage = new SettingPage();
        Page.Content = _SettingPage;
    }
}
