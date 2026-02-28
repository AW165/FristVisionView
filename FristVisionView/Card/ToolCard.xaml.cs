using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FirstVisionView.Card
{
    /// <summary>
    /// ToolCard.xaml 的交互逻辑
    /// </summary>
    public partial class ToolCard : UserControl
    {
        public ToolCard()
        {
            InitializeComponent();
        }
        public void SetSelected(bool selected)
        {
            ToolBorder.BorderBrush = selected ? Brushes.CornflowerBlue: Brushes.Transparent;
        }
    }
}
