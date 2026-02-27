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

namespace FirstVisionView
{
    /// <summary>
    /// HomePage.xaml 的交互逻辑
    /// </summary>
    public partial class AdjustPage : UserControl
    {
        public AdjustPage()
        {
            InitializeComponent();
        }
        private bool _isSelecting;
        private Point _SelectionStratPoint;
        private Point _SelectionEndPoint;
        //鼠标左键按下
        private void Canvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            //获取鼠标指针当前的位置并赋值给起始点
            _SelectionStratPoint = e.GetPosition(ParamCanvas);
            //设置选择中的状态
            _isSelecting = true;
            //设置矩形框的长和宽为0
            SelectionBox.Width = 0;
            SelectionBox.Height = 0;
            //设置矩形框生成的起始点
            Canvas.SetLeft(SelectionBox,_SelectionStratPoint.X);
            Canvas.SetLeft(SelectionBox,_SelectionStratPoint.Y);
            //设置这个框为可见
            SelectionBox.Visibility = Visibility.Visible;
        }
        //鼠标左键抬起
        private void Canvas_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            //设置这个框为不可见
            SelectionBox.Visibility = Visibility.Collapsed;
            //设置选择状态结束
            _isSelecting = false;
        }
        //鼠标移动
        private void Canvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (_isSelecting)
            {
                Point CurrentPoint = e.GetPosition(ParamCanvas);
                double X = Math.Min(_SelectionStratPoint.X,CurrentPoint.X);
                double Y = Math.Min(_SelectionStratPoint.Y,CurrentPoint.Y);
                double weight = Math.Abs(CurrentPoint.X - _SelectionStratPoint.X);
                double height = Math.Abs(CurrentPoint.Y - _SelectionStratPoint.Y);
                Canvas.SetLeft(SelectionBox,X);
                Canvas.SetRight(SelectionBox,Y);
                SelectionBox.Width = Width;
                SelectionBox.Height = Height;
            }
        }
    }
}
