using System.Drawing;
using System.Drawing.Drawing2D;
using System.Threading;
using System.Windows.Forms;

namespace SimplePainterNamespace
{
    /// <summary>
    ///     Форма скриншот региона(области)
    /// </summary>
    public partial class ScreenShotRegion : Form
    {
        /// <summary>
        ///     "Стирающая кисть"
        /// </summary>
        private readonly Pen _eraserPen = new Pen(Color.FromArgb(byte.MaxValue, byte.MaxValue, 192), 1f);

        /// <summary>
        ///     Основная кисть отрисовки прямоугольника
        /// </summary>
        private readonly Pen _myPen = new Pen(Color.DarkRed, 1);

        private readonly Graphics g;

        /// <summary>
        ///     Текущая нижняя координата
        /// </summary>
        private Point _currentBottomRight;

        /// <summary>
        ///     Текущая координата лево верх
        /// </summary>
        private Point _currentTopLeft;

        /// <summary>
        ///     Флаг нажатия левой кнопки мыши
        /// </summary>
        private bool _leftButtonDown;

        /// <summary>
        ///     Начальная точка
        /// </summary>
        private Point _startPoint;

        /// <summary>
        ///     Конструктор
        /// </summary>
        public ScreenShotRegion()
        {
            InitializeComponent();
            BackColor = Color.FromArgb(byte.MaxValue, byte.MaxValue, 192);
            MouseDown += M_Click;
            MouseUp += M_Up;
            MouseMove += M_Move;
            _myPen.DashStyle = DashStyle.Dash;
            g = CreateGraphics();
            KeyUp += K_KeyUp;
        }

        /// <summary>
        ///     Результат выполнения
        /// </summary>
        public Bitmap ScreenShot { get; private set; }

        /// <summary>
        ///     Обработчик нажатия левой кнопки мыши
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void M_Click(object sender, MouseEventArgs e)
        {
            g.Clear(Color.FromArgb(byte.MaxValue, byte.MaxValue, 192));
            _leftButtonDown = true;
            _startPoint = new Point(MousePosition.X, MousePosition.Y);
        }

        /// <summary>
        ///     Обработчик конца нажатия кнопки мыши
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void M_Up(object sender, MouseEventArgs e)
        {
            _leftButtonDown = false;
            TakeScreenShot();
        }

        /// <summary>
        ///     Обработчик перемещения мыши
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void M_Move(object sender, MouseEventArgs e)
        {
            if (!_leftButtonDown)
            {
                return;
            }
            g.DrawRectangle(_eraserPen, _currentTopLeft.X, _currentTopLeft.Y, _currentBottomRight.X - _currentTopLeft.X,
                            _currentBottomRight.Y - _currentTopLeft.Y);
            if (Cursor.Position.X < _startPoint.X)
            {
                _currentTopLeft.X = Cursor.Position.X;
                _currentBottomRight.X = _startPoint.X;
            }
            else
            {
                _currentTopLeft.X = _startPoint.X;
                _currentBottomRight.X = Cursor.Position.X;
            }
            if (Cursor.Position.Y < _startPoint.Y)
            {
                _currentTopLeft.Y = Cursor.Position.Y;
                _currentBottomRight.Y = _startPoint.Y;
            }
            else
            {
                _currentTopLeft.Y = _startPoint.Y;
                _currentBottomRight.Y = Cursor.Position.Y;
            }
            g.DrawRectangle(_myPen, _currentTopLeft.X, _currentTopLeft.Y, _currentBottomRight.X - _currentTopLeft.X,
                            _currentBottomRight.Y - _currentTopLeft.Y);
        }

        /// <summary>
        ///     Обработчик захвата изображения из области
        /// </summary>
        private void TakeScreenShot()
        {
            Hide();
            Thread.Sleep(byte.MaxValue);
            var sourcePoint = new Point(_currentTopLeft.X, _currentTopLeft.Y);
            var selectionRectangle = new Rectangle(_currentTopLeft.X, _currentTopLeft.Y,
                                                   _currentBottomRight.X - _currentTopLeft.X,
                                                   _currentBottomRight.Y - _currentTopLeft.Y);
            var bitmap = new Bitmap(selectionRectangle.Width, selectionRectangle.Height);
            using (Graphics graphics = Graphics.FromImage(bitmap))
            {
                graphics.CopyFromScreen(sourcePoint, Point.Empty, selectionRectangle.Size);
            }
            ScreenshotScreen.ScreenshotedRegion = bitmap;
        }

        /// <summary>
        ///     Обработка ESC
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