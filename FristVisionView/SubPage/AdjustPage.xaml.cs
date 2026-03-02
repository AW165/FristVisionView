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
using FirstVisionView.Card;

namespace FirstVisionView
{
    /// <summary>
    ///  参数调整界面
    /// </summary>
    public partial class AdjustPage : UserControl
    {
        public AdjustPage()
        {
            InitializeComponent();
        }
        private List<Border> AllCards = new List<Border>();
        private List<Border> SelectionCards = new List<Border>();
        private bool _isSelecting;//拉框
        private Point _SelectionStratPoint;
        private Point _RightButtonDown;
        private Point _CardOffset;//存储移动的距离
        private bool _isSelectingCard;//卡片是否被选择
        private bool _IsDraggingCard;//是否拖拽
        private FrameworkElement _CurrentClickCard; //存储当前点击的卡片
        private int Distance_Threshold = 5; //存储最小移动的距离
        private bool _CanvasRightButtonDown;
        private bool _CanvasRightButtonUp;
        private bool _CardRightButtonDown;
        private bool _CardRightButtonUp;
        //鼠标左键按下
        private void Canvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            //获取鼠标指针当前的位置并赋值给起始点
            _SelectionStratPoint = e.GetPosition(ParamentCanvas);
            //设置选择中的状态
            _isSelecting = true;
            //设置矩形框的长和宽为0
            SelectionBox.Width = 0;
            SelectionBox.Height = 0;
            //设置矩形框生成的起始点
            Canvas.SetLeft(SelectionBox,_SelectionStratPoint.X);
            Canvas.SetTop(SelectionBox,_SelectionStratPoint.Y);
            //设置这个框为可见
            SelectionBox.Visibility = Visibility.Visible;
            ParamentCanvas.CaptureMouse();
        }
        //鼠标左键抬起
        private void Canvas_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            //设置这个框为不可见
            SelectionBox.Visibility = Visibility.Collapsed;
            //设置选择状态结束
            _isSelecting = false;
            ParamentCanvas.ReleaseMouseCapture();
        }
        //鼠标移动
        private void Canvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (_isSelecting)
            {
                Point CurrentPoint = e.GetPosition(ParamentCanvas);
                double X = Math.Min(_SelectionStratPoint.X,CurrentPoint.X);
                double Y = Math.Min(_SelectionStratPoint.Y,CurrentPoint.Y);
                double width= Math.Abs(CurrentPoint.X - _SelectionStratPoint.X);
                double height = Math.Abs(CurrentPoint.Y - _SelectionStratPoint.Y);
                Canvas.SetLeft(SelectionBox,X);
                Canvas.SetTop(SelectionBox,Y);
                SelectionBox.Width = width;
                SelectionBox.Height = height;

            }
        }
        
      

        private void Card_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var card = sender as FrameworkElement;
            if (card == null)
            {
                return;
            }
            _CurrentClickCard = card;
            _IsDraggingCard = true;
            _CardOffset = e.GetPosition(card);
            _SelectionStratPoint = e.GetPosition(ParamentCanvas);
            e.Handled = true;
            card.CaptureMouse();
        }
        private void Card_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (_IsDraggingCard == false)
            {
                return;
            }
            e.Handled = true;
            _IsDraggingCard = false;
            _CurrentClickCard.ReleaseMouseCapture();
        }
        private void Card_MouseMove(object sender, MouseEventArgs e)
        {

            e.Handled = true;
            if (_IsDraggingCard)
            {

                Point currentPoint = e.GetPosition(ParamentCanvas);
                var disx = Math.Abs(currentPoint.X - _SelectionStratPoint.X);
                var disy = Math.Abs(currentPoint.Y - _SelectionStratPoint.Y);
                if (disx > Distance_Threshold || disy > Distance_Threshold)
                {
                    double x = currentPoint.X - _CardOffset.X;
                    double y = currentPoint.Y - _CardOffset.Y;
                    Canvas.SetLeft(_CurrentClickCard, x);
                    Canvas.SetTop(_CurrentClickCard, y);
                }
                
            }
        }

        private void ParamentCanvas_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            _CanvasRightButtonDown = true;
        }

        private void ParamentCanvas_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (_CanvasRightButtonDown)
            {
                AddCard.IsOpen = true;
                _CanvasRightButtonDown = false;

            }

        }

        private void AddNewCard(object sender, RoutedEventArgs e)
        {
            e.Handled = true;
            Card.ToolCard NewCard = new Card.ToolCard();
            Canvas.SetTop(NewCard, 25);
            Canvas.SetLeft(NewCard, 25);
            NewCard.MouseLeftButtonDown += Card_MouseLeftButtonDown;
            NewCard.MouseLeftButtonUp += Card_MouseLeftButtonUp;
            NewCard.MouseMove += Card_MouseMove;
            NewCard.MouseRightButtonDown += Card_MouseRightButtonDown;
            NewCard.MouseRightButtonUp += Card_MouseRightButtonUp; 
            ParamentCanvas.Children.Add(NewCard);
        }

        private void Card_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
            if (_CardRightButtonDown)
            {
                DeleteCard.IsOpen = true;
                _CanvasRightButtonDown = false;
            }
        }

        private void Card_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
            if (_CurrentClickCard == null) { return; }
            _CardRightButtonDown = true;
            
        }

        private void DeleCard(object sender, RoutedEventArgs e)
        {
            var card = _CurrentClickCard;
            if (card == null) { return; }
            ParamentCanvas.Children.Remove(card);
            DeleteCard.IsOpen = false;
        }
    }
}
