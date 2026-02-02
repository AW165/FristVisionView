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
    public MainWindow()
    {
        InitializeComponent();
        ToolsButtonClick = new RelayCommand(() => IsToolsPopupOpen = !IsToolsPopupOpen);
    }

    private void Button_Click(object sender, RoutedEventArgs e)
    {
        FilePopup.IsOpen = true;
        
    }

    private void imgDisplay_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {

    }

    private void imgDisplay_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
    {

    }

    private void imgDisplay_MouseMove(object sender, MouseEventArgs e)
    {

    }

    private void imgDisplay_MouseWheel(object sender, MouseWheelEventArgs e)
    {

    }

    public ICommand ToolsButtonClick {  get; }
}
