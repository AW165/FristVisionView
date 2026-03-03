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
using System.Windows.Media.Effects;
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
        private List<ToolCard> AllCards = new List<ToolCard>();
        private List<ToolCard> SelectionCards = new List<ToolCard>();
        private bool _isCanvasSelecting;//拉框
        private Point _SelectionStratPoint;
        private Point _CardOffset;//存储移动的距离
        private bool _isSelectingCard;//卡片是否被选择
        private bool _IsDraggingCard;//是否拖拽
        private ToolCard _CurrentClickCard; //存储当前点击的卡片
        private int Distance_Threshold = 5; //存储最小移动的距离
        private bool _CanvasRightButtonDown;
        //==============================画布事件区域===========================
        //鼠标左键按下
        private void Canvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (_CurrentClickCard != null)
            { //判断当前是否有选中的卡片，有则从卡片中释放鼠标
                _CurrentClickCard.ReleaseMouseCapture();
                ResetCardEffect(_CurrentClickCard);//取消高亮
            }       
            _isSelectingCard = false;//复位卡片选择状态
            //获取鼠标指针当前的位置并赋值给起始点
            _SelectionStratPoint = e.GetPosition(ParamentCanvas);
            //设置选择中的状态
            _isCanvasSelecting = true;
            //设置矩形框的长和宽为0
            SelectionBox.Width = 0;
            SelectionBox.Height = 0;
            //设置矩形框生成的起始点
            Canvas.SetLeft(SelectionBox,_SelectionStratPoint.X);
            Canvas.SetTop(SelectionBox,_SelectionStratPoint.Y);
            //设置这个框为可见
            SelectionBox.Visibility = Visibility.Visible;
            //捕获鼠标，后续事件都接收到Canvas上
            ParamentCanvas.CaptureMouse();
        }
        //鼠标左键抬起
        private void Canvas_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            //设置这个框为不可见
            SelectionBox.Visibility = Visibility.Collapsed;
            //设置选择状态结束
            _isCanvasSelecting = false;
            //释放鼠标
            ParamentCanvas.ReleaseMouseCapture();
        }
        //鼠标移动
        private void Canvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (_isCanvasSelecting)//如果在Canvas上左键长按未松开，则开始绘框
            {
                Point CurrentPoint = e.GetPosition(ParamentCanvas);//获取鼠标在Canvs的坐标
                double X = Math.Min(_SelectionStratPoint.X,CurrentPoint.X);//得到矩形左上角
                double Y = Math.Min(_SelectionStratPoint.Y,CurrentPoint.Y);
                double width= Math.Abs(CurrentPoint.X - _SelectionStratPoint.X);//得到长和宽
                double height = Math.Abs(CurrentPoint.Y - _SelectionStratPoint.Y);
                Canvas.SetLeft(SelectionBox,X);//设置矩形左上角
                Canvas.SetTop(SelectionBox,Y);
                SelectionBox.Width = width;//设置矩形长和宽
                SelectionBox.Height = height;
                foreach (var card in AllCards)
                {
                    double CardLeft = Canvas.GetLeft(card);
                    double CardTop = Canvas.GetTop(card);
                    
                    
                }

            }
        }
        //Canvas上鼠标右键抬起事件
        private void Canvas_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (_isSelectingCard == true)//如果有选择中的卡片则显示删除菜单，否则显示添加菜单
            {
                DeleteCard.IsOpen = true;
                return;
            }
            AddCard.IsOpen = true;//打开添加菜单
        }
        //===============================卡片事件区域=========================

        //卡片上鼠标左键按下事件
        private void Card_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (_CurrentClickCard != null)//判断当前有无卡片被选择，有则取消之前卡片的高亮
            {
                ResetCardEffect(_CurrentClickCard);    
            }
            var card = sender as ToolCard;//得到当前的卡片
            if (card == null)
            {
                return;
            }
            _CurrentClickCard = card;//记录当前点击的卡片
            _IsDraggingCard = true;//拖拽开启
            _isSelectingCard = true;//标记已选择了卡片
            _CardOffset = e.GetPosition(card);//获取鼠标位于Card的内部坐标
            _SelectionStratPoint = e.GetPosition(ParamentCanvas);//获取鼠标位于Canvas的坐标
            HighlightCard(_CurrentClickCard);//设置当前卡片高亮
            e.Handled = true;//禁止冒泡
            card.CaptureMouse();//捕获鼠标
        }
        //卡片上鼠标左键抬起事件
        private void Card_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (_IsDraggingCard == false)
            {
                return;
            }
            
            e.Handled = true;
            //拖拽结束
            _IsDraggingCard = false;
            //释放鼠标
            _CurrentClickCard.ReleaseMouseCapture();
        }
        //卡片上鼠标移动事件
        private void Card_MouseMove(object sender, MouseEventArgs e)
        {

            e.Handled = true;
            if (_IsDraggingCard)//如果当前在卡片上长按鼠标，则进入拖动
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
   
        //卡片上右键按下
        private void Card_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
            var card = sender as ToolCard;
            if (_CurrentClickCard != null && card != _CurrentClickCard) 
            {
                ResetCardEffect(_CurrentClickCard);
                
            } 
            if (card != null)
            {
                _CurrentClickCard = card;
                HighlightCard(card);//设置当前卡片高亮
                _isSelectingCard = true;
            }
           
        }
        //卡片上右键抬起
        private void Card_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
            DeleteCard.IsOpen = true;
        }
        //=========================菜单事件===========================

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
            AllCards.Add(NewCard);
            AddCard.IsOpen = false;

        }



        private void DeleCard(object sender, RoutedEventArgs e)
        {
            if (_CurrentClickCard == null) { return; }
            ParamentCanvas.Children.Remove(_CurrentClickCard);
            AllCards.Remove(_CurrentClickCard);
            DeleteCard.IsOpen = false;//关闭删除菜单
            ClearSelection();
        }
        /// <summary>
        /// 取消高亮
        /// </summary>
        private void ResetCardEffect(ToolCard card)
        {
            if (card == null) return;
            card.Effect = new DropShadowEffect
            {
                BlurRadius = 10,
                Color = (Color)ColorConverter.ConvertFromString("#20000000"),
                Opacity = 0.5,
                ShadowDepth = 1,
            };
        }

        /// <summary>
        /// 设置高亮
        /// </summary>
        private void HighlightCard(ToolCard card)
        {
            if (card == null) return;
            card.Effect = new DropShadowEffect
            {
                BlurRadius = 10,
                Color = Colors.DodgerBlue,
                Opacity = 0.8,
                ShadowDepth = 0
            };
        }
        /// <summary>
        /// 清除选中状态
        /// </summary>
        private void ClearSelection()
        {
            ResetCardEffect(_CurrentClickCard);
            _CurrentClickCard?.ReleaseMouseCapture();
            _CurrentClickCard = null;
            _isSelectingCard = false;
            _IsDraggingCard = false;
        }
    }
}
