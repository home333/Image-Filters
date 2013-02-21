using System.Drawing;
using System.Drawing.Drawing2D;
using System.Threading;
using System.Windows.Forms;

namespace SimplePainterNamespace
{
    /// <summary>
    /// Форма скриншот региона(области)
    /// </summary>
    public partial class ScreenShotRegion : Form
    {
        /// <summary>
        /// Начальная точка
        /// </summary>
        private Point StartPoint = new Point();
        /// <summary>
        /// Текущая координата лево верх
        /// </summary>
        private Point CurrentTopLeft = new Point();
        /// <summary>
        /// Текущая нижняя координата
        /// </summary>
        private Point CurrentBottomRight = new Point();
        /// <summary>
        /// Основная кисть отрисовки прямоугольника
        /// </summary>
        private Pen MyPen = new Pen(Color.DarkRed, 1);
        /// <summary>
        /// "Стирающая кисть"
        /// </summary>
        private Pen EraserPen = new Pen(Color.FromArgb((int)byte.MaxValue, (int)byte.MaxValue, 192), 1f);
        /// <summary>
        /// Флаг нажатия левой кнопки мыши
        /// </summary>
        private bool LeftButtonDown;
        /// <summary>
        /// Результат выполнения
        /// </summary>
        public Bitmap ScreenShot {get; private set;}
        private Graphics g;

        /// <summary>
        /// Конструктор
        /// </summary>
        public ScreenShotRegion()
        {
            InitializeComponent();
            BackColor = Color.FromArgb((int)byte.MaxValue, (int)byte.MaxValue, 192);
            MouseDown += new MouseEventHandler(M_Click);
            MouseUp += new MouseEventHandler(M_Up);
            MouseMove += new MouseEventHandler(M_Move);
            MyPen.DashStyle = DashStyle.Dash;
            g = CreateGraphics();
            KeyUp += new KeyEventHandler(K_KeyUp);
        }

        /// <summary>
        /// Обработчик нажатия левой кнопки мыши
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void M_Click(object sender, MouseEventArgs e)
        {
            g.Clear(Color.FromArgb((int)byte.MaxValue, (int)byte.MaxValue,192));
            LeftButtonDown = true;
            StartPoint = new Point(Control.MousePosition.X, Control.MousePosition.Y);
        }

        /// <summary>
        /// Обработчик конца нажатия кнопки мыши
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void M_Up(object sender, MouseEventArgs e)
        {
            LeftButtonDown = false;
            TakeScreenShot();
            
        }

        /// <summary>
        /// Обработчик перемещения мыши
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void M_Move(object sender, MouseEventArgs e)
        {
            if (!LeftButtonDown)
            {
                return;
            }
            g.DrawRectangle(EraserPen, CurrentTopLeft.X, CurrentTopLeft.Y, CurrentBottomRight.X - CurrentTopLeft.X, CurrentBottomRight.Y - CurrentTopLeft.Y);
            if (Cursor.Position.X < StartPoint.X)
            {
                CurrentTopLeft.X = Cursor.Position.X;
                CurrentBottomRight.X = StartPoint.X;
            }
            else
            {
                CurrentTopLeft.X = StartPoint.X;
                CurrentBottomRight.X = Cursor.Position.X;
            }
            if (Cursor.Position.Y < StartPoint.Y)
            {
                CurrentTopLeft.Y = Cursor.Position.Y;
                CurrentBottomRight.Y = StartPoint.Y;
            }
            else
            {
                CurrentTopLeft.Y = StartPoint.Y;
                CurrentBottomRight.Y = Cursor.Position.Y;
            }
            g.DrawRectangle(MyPen, CurrentTopLeft.X, CurrentTopLeft.Y, CurrentBottomRight.X - CurrentTopLeft.X, CurrentBottomRight.Y - CurrentTopLeft.Y);
        }

        /// <summary>
        /// Обработчик захвата изображения из области 
        /// </summary>
        private void TakeScreenShot()
        {
            Hide();
            Thread.Sleep((int)byte.MaxValue);
            Point sourcePoint = new Point(CurrentTopLeft.X, CurrentTopLeft.Y);
            Rectangle selectionRectangle = new Rectangle(CurrentTopLeft.X, CurrentTopLeft.Y, CurrentBottomRight.X - CurrentTopLeft.X, CurrentBottomRight.Y - CurrentTopLeft.Y);
            Bitmap bitmap = new Bitmap(selectionRectangle.Width, selectionRectangle.Height);
            using (Graphics graphics = Graphics.FromImage(bitmap))
            {
                graphics.CopyFromScreen(sourcePoint, Point.Empty, selectionRectangle.Size);

            }
            ScreenshotScreen.ScreenshotedRegion = bitmap;
        }
        /// <summary>
        /// Обработка ESC
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void K_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Escape)
                return;
     
        }
    }
}
