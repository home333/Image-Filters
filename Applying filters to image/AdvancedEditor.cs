using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;
using System.Collections.Generic;

[assembly: CLSCompliant(true)]
namespace SimplePainterNamespace
{

    /// <summary>
    /// Класс, представляющий собой основное окно программы.
    /// Наследуется из класса Form
    /// </summary>
    public partial class AdvancedEditor : Form
    {
        #region Constructor
        /// <summary>
        /// Конструктор формы
        /// Инициализирует компоненты
        /// Инициализация превью форму
        /// Создает 2 tooltip
        /// Получает список всех фильтров для пункта меню на основе массива FiltersNames
        /// </summary>
        public AdvancedEditor()
        {
            InitializeComponent();
            ToolTip origtooltip = new ToolTip();
            origtooltip.SetToolTip(DefaultPicture, "Оригинальное изображение");
            ToolTip edittooltip = new ToolTip();
            edittooltip.SetToolTip(EditedPicture, "Обработанное изображение");
            //Инициализация превью форму
            PreviewForm = new ImagePreviewForm() ;
            //Получение списка всех фильтров для пункта меню на основе массива FiltersNames
            int i=1;
            foreach (string x in FiltersNames)
            {
                TreeNode node = new TreeNode();
                node.Text = x;
                node.Name = node + i.ToString();
                FiltersList.Nodes.Add(node);
                ToolStripMenuItem filter = new ToolStripMenuItem();
                filter.Name = filter + i.ToString();
                filter.Text = x;
                i++;
                filter.Click += (sender, e) => { CurrentFilter = filter.Text;  StarterBody(CurrentFilter); };
                FiltersMenuList.DropDownItems.Add(filter);
            }
          
            

            this.DragDrop += this.Form_DragDrop;
            this.DragEnter += this.Form_DragEnter;
            this.KeyDown += Form_KeyDown;
           
        }
        #endregion

        #region Variables
        /// <summary>
        /// Информация о текущем фильтре
        /// </summary>
        private static string CurrentFilter;
        /// <summary>
        /// Путь до последнего открытого изображения
        /// </summary>
        private static string imagefile;

        /// <summary>
        /// Массив имен текущих фильтров
        /// На его основе формируются другие различные части программы
        /// </summary>
        private string[] FiltersNames ={ "Шум","Шум \"Salt and Pepper\"","Резкость","Медианный фильтр","Усредняющий фильтр","Фильтр Собела (вертикаль)","Фильтр Собела (горизонталь)","Фильтр Лапласа",
                                          "Гауссово размытие","Выделить границы","Обнаружить границы", 
                                          "Проработать контраст с его увеличением", "Неон","Придать рельеф",
                                          "Перестановка цветов","Тень", "Пикселизация","Сепия", "Инвертирование","Оттенки серого",
                                          "Ночной режим", "Только красный", "Только синий",   "Случайный выбор","Рассеивание", 
                                          "Сдвиг","Вертикальный сдвиг","Отражение","Вертикальное отражение",
                                          
                                            };


        private Dictionary<string, string> _filtersinformations 
            = new Dictionary<string,string>{
                    {"Медианный фильтр", "Эффективный фильтр подавления шумов на изображениях."  },
            {"Перестановка цветов", "Смещение цветовых компонент каждого пикселя изображения вправо." },
            {"Тень","Создание новой копии изображения с допольнительной площадью, эмулирующей отбрасывание тени от изображения"}, 
            {"Пикселизация", "Эмуляция шумового фильтра при котором все изображение не просто теряет свою четкость, а оформляется в виде усредненных пикселей оригинального изображения с определенным размером"},
            {"Усредняющий фильтр", "Малоэффективный фильтр подавления небольших шумов на изображении. Происходит значительное потеря четкости"},
            {"Сепия","Эффект, применяемый над любым изображением, позволяет представить изображение в старом стиле. Иначе говоря окрашивает изображения в желто-коричневые оттенки" },
            {"Инвертирование","Обращает на противоположные цвета в спектре все пиксели изображения" },
            {"Оттенки серого", "Фильтр исскуственной потери цветности изображений. В результате на изображении все цвета представляются в серых тонах." },
            {"Шум","Фильтр исскуственной деформации изображений. На случайные места на изображении наносятся новые цветовые пиксели, цвет которых выбирается автоматически"},
            {"Шум \"Salt and Pepper\"","Фильтр эмуляции деформации изображений. В отличие от обычного шума вместо случайного шума из всего диапазона цветов используется только черный и белый цвета." },
            {"Ночной режим", "Обработка изображения при которой выполняется потеря всех цветовых компонент кроме зеленой."},
            {"Только красный", "Обработка изображения при которой выполняется потеря всех цветовых компонент кроме красной."},
            {"Только синий", "Обработка изображения при которой выполняется потеря всех цветовых компонент кроме синей."},
            {"Гауссово размытие","Обработка изображения с помощью нормализованной матрицы , сформированной по закону Гаусса. Данный фильтр позволяет производить мягкую потерю резкости." },
            {"Резкость", "Фильтр обработки изображений для частичного восстановления контуров объектов. "},
            {"Обнаружить границы", "Фильтр незначительного выделения границ. Неэфективен на изображениях с большим количеством объектов."},
            {"Выделить границы", "Фильтр выделения границ любых объектов на изображении. Для качественного результата необходимо применение фильтра резкости"},
            {"Неон", "Комбинированный фильтр обнаружения границ. Выделение границ происходит с красивым эффектом различных цветов"},
            {"Придать рельеф", "Фильтр выделения краев с последующим вдавливанием. В результате получается эффект рельефа местности."},
            {"Проработать контраст с его увеличением", "Усиление цветности изображения по среднему значению окружающих пикселей с одновременной корректировкой контрастности"},
            {"Случайный выбор", "Обрабатывается каждый пиксель изображения с его изменением в зависимости от случайного цвета в неком радиусе от редактируемого пикселя"},
            {"Рассеивание", "Обрабатывается каждый пиксель изображения с его изменением в зависимости от случайного цвета в неком радиусе от редактируемого пикселя. В отличие от фильтра \"Случайный выбор\"все цвета сохраняются" },
            {"Сдвиг", "Сдвиг наборов пикселей в различные стороны в зависимости от четности строк. "},
            {"Вертикальный сдвиг",  "Сдвиг наборов пикселей в различные стороны в зависимости от четности столбцов. "},
            {"Отражение","Эмуляция получения изображения в зеркале"},
            {"Фильтр Лапласа", "Высококачественный фильтр определения границ изображения." },
            {"Фильтр Собела (вертикаль)", "Выделяет горизонтальные края отдельно на изображении. Для правильной работы требует изображения в оттенках серого"  },
            {"Фильтр Собела (горизонталь)", "Выделяет вертикальный края отдельно на изображении. Для правильной работы требует изображения в оттенках серого"  },
            {"Вертикальное отражение", "Эмуляция получения изображения в зеркале с использованием вертикальных координат"}
        
        };
        private Dictionary<string, Func<Bitmap,Bitmap>> _filters =
        new Dictionary<string, Func<Bitmap,Bitmap>>
        {
            {"Медианный фильтр", (x) => ColorDiapasoneFilters.MedianFilter(x) },
            {"Перестановка цветов", (x)=> ColorDiapasoneFilters.ChangeColors(x) },
            {"Тень", (x)=> ColorDiapasoneFilters.Shadow(x)}, 
            {"Пикселизация", (x) => ColorDiapasoneFilters.Pixelate(x)},
            {"Усредняющий фильтр", (x)=> Convolution.StartConvolution(x,PredefinedKernels.MeanRemoval)},
            {"Сепия", (x)=> ColorDiapasoneFilters.Sepia(x)},
            {"Инвертирование", (x) => ColorDiapasoneFilters.Invert(x)},
            {"Оттенки серого", (x)=> ColorDiapasoneFilters.Grayscale(x)},
            {"Шум", (x)=>ColorDiapasoneFilters.Noise(x)},
            {"Шум \"Salt and Pepper\"", (x)=>ColorDiapasoneFilters.NoiseSaltAndPepper(x) },
            {"Ночной режим", (x) => ColorDiapasoneFilters.Nightvision(x)},
            {"Только красный", (x)=>ColorDiapasoneFilters.Armageddon(x)},
            {"Только синий", (x)=> ColorDiapasoneFilters.OnlyBlue(x)},
            {"Гауссово размытие", (x)=> Convolution.StartConvolution(x, PredefinedKernels.GaussianBlur)},
            {"Резкость", (x) => Convolution.StartConvolution(x, PredefinedKernels.Sharpen)},
            {"Обнаружить границы", (x)=> Convolution.StartConvolution(x, PredefinedKernels.EdgeDetect)},
            {"Выделить границы", (x)=>Convolution.StartConvolution(x, PredefinedKernels.EdgeUp)},
            {"Неон", (x) => Convolution.StartConvolution(x, PredefinedKernels.Neon)},
            {"Придать рельеф", (x) => Convolution.StartConvolution(x, PredefinedKernels.Relief)},
            {"Проработать контраст с его увеличением", (x) => Convolution.StartConvolution(x, PredefinedKernels.KontrastCheck)},
            {"Случайный выбор", (x) => ColorDiapasoneFilters.RandomSelect(x, 100)},
            {"Рассеивание", (x)=> ColorDiapasoneFilters.Dispersal(x, 100)},
            {"Сдвиг", (x)=> ColorDiapasoneFilters.Shift(x)},
            {"Вертикальный сдвиг", (x) => ColorDiapasoneFilters.ShiftVertical(x)},
            {"Отражение", (x)=> ColorDiapasoneFilters.Mirroring(x)},
            {"Фильтр Лапласа", (x)=> Convolution.StartConvolution(x, PredefinedKernels.Laplasian)},
            {"Фильтр Собела (вертикаль)", (x)=> Convolution.StartConvolution(x, PredefinedKernels.Sobel1) },
            {"Фильтр Собела (горизонталь)", (x)=> Convolution.StartConvolution(x, PredefinedKernels.Sobel2) },
            {"Вертикальное отражение", (x) => ColorDiapasoneFilters.VerticalMap(inputpicture)}
      
       
        };


        /// <summary>
        /// Входное изображение
        /// </summary>
        private static Bitmap inputpicture;

        /// <summary>
        /// Измененное изображение
        /// </summary>
        private static Bitmap outputpicture;
      
        /// <summary>
        /// Свойство-флаг для обработчика события HideShowLeft_Click
        /// </summary>
        private bool ColumnHide = false;

   
        /// <summary>
        /// Объявление PreviewForm для отображения миниатюр текущих изображений
        /// </summary>
        private ImagePreviewForm PreviewForm;

        /// <summary>
        /// Флаг PreviewForm. 
        /// False - Hide()
        /// True - Show()
        /// </summary>
        private bool PreviewOpened = false;
        #endregion

        #region clearing/disposing/closing methods

        /// <summary>
        /// Обработчик завершения программы при нажатии на пункт меню "Выход"
        /// </summary>
        private void ExitMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void DeletePictureMenuItem_Click(object sender, EventArgs e)
        {
            outputpicture = null;
            EditedPicture.Image = null;
            PreviewForm.DelOutput();
        }

        private void DisableAllButtons()
        {
            StartButton.Enabled = false;
            FiltersList.Enabled = false;
            ReloadOriginal.Enabled = false;
            RezolutionTools.Enabled = false;
            RandomFilter.Enabled = false;
            StatsImage.Enabled = false;
        }

        private void EnableAllButtons()
        {
            StartButton.Enabled = true;
            FiltersList.Enabled = true;
            ReloadOriginal.Enabled = true;

            RezolutionTools.Enabled = true;
            RandomFilter.Enabled = true;
            StatsImage.Enabled = true;
        }

        /// <summary>
        /// Метод очистки рабочей области
        /// inputpicture = null;
        /// EditedPicture.Image = null;
        /// outputpicture = null;
        ///DefaultPicture.Image = null;
        ///FiltersList.Enabled = false;
        ///StartButton.Enabled = false;
        ///+ PreviewForm.CleanAllPictures();
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CloseAll_Click(object sender, EventArgs e)
        {
            inputpicture = null;
            EditedPicture.Image = null;
            outputpicture = null;
            DefaultPicture.Image = null;
            DisableAllButtons();
            PreviewForm.CleanAllPictures();
        }

        /// <summary>
        ///  Обработчик закрытия изображения
        /// </summary>
        /// <param name="sender">заголовок отправителя</param>
        /// <param name="e">данные о событии EventArgs</param>
        private void CloseImage_Click(object sender, EventArgs e)
        {
            //Стираем всё после закрытия
            inputpicture = null;
            EditedPicture.Image = null;
            outputpicture = null;
            DefaultPicture.Image = null;
            DisableAllButtons();
            PreviewForm.CleanAllPictures();
        }
        #endregion

        #region IDragDrop
        private void Form_DragDrop(object sender, DragEventArgs e)
        {
            if (inputpicture != null)
            {
                MessageBox.Show("", "", MessageBoxButtons.OK, MessageBoxIcon.Question);
            }
            string file = (string)e.Data.GetData(DataFormats.FileDrop);
            try
            {
                if (!ColumnHide)
                {
                    UpdateOriginalPicture((Bitmap)Image.FromFile(file));
                }
                else
                {
                    UpdateOriginalPicture((Bitmap)Image.FromFile(file));
                    outputpicture = inputpicture;
                    EditedPicture.Image = outputpicture;
                }
            }
            //catch (Exception) { MessageBox.Show("Возникла ошибка при загрузке данного файла"); }
            finally { imagefile = file; }
        }
        private void Form_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.V)
            {
                if (Clipboard.ContainsImage())
                {
                    UpdateOriginalPicture((Bitmap)Clipboard.GetImage());
                    EnableAllButtons();
                }
            }

            if (e.Control && e.KeyCode == Keys.C)
            {
                if (outputpicture != null)
                {
                    Clipboard.SetImage(outputpicture);
                }
            }
        }
        private void Form_DragEnter(object sender, DragEventArgs e)
        {

            // Если данный изображение или файл -> разрешить
            if (e.Data.GetDataPresent(DataFormats.Bitmap) || e.Data.GetDataPresent(DataFormats.FileDrop))
            { e.Effect = DragDropEffects.Copy; }
            else
            { e.Effect = DragDropEffects.None; }
        }
        #endregion

        #region filters information

        /// <summary>
        /// Обработчик изменения выбора фильтра
        /// Меняет описание о текущем фильтре
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FiltersList_AfterSelect(object sender, TreeViewEventArgs e)
        {
            CurrentFilter = FiltersList.SelectedNode.Text;
            SetCurrentOperationsInfo(CurrentFilter);
        }

        private void SetCurrentOperationsInfo(string op)
        {
            if (!_filtersinformations.ContainsKey(op))
                return;
                //throw new ArgumentException(string.Format("Operation {0} is invalid", op), "op");
            information.Text = _filtersinformations[op];
        }
       
        #endregion

        #region SaveImage
        /// <summary>
        /// Метод сохранения картинки в определенном формате
        /// </summary>
        /// <param name="flag">1 - BMP
        /// 2- JPEG
        /// 3- PNG</param>
        private void SaveImage(int flag)
        {
            System.Drawing.Imaging.ImageFormat imageformat = null;

            switch (flag)
            {
                case 1: imageformat = new System.Drawing.Imaging.ImageFormat(System.Drawing.Imaging.ImageFormat.Bmp.Guid); break;
                case 2: imageformat = new System.Drawing.Imaging.ImageFormat(System.Drawing.Imaging.ImageFormat.Jpeg.Guid); break;
                case 3: imageformat = new System.Drawing.Imaging.ImageFormat(System.Drawing.Imaging.ImageFormat.Png.Guid); break;
            }

            //Сохранение картинки
            if (EditedPicture.Image != null)
            {
                using (SaveFileDialog dialog = new SaveFileDialog())
                {
                    dialog.Title = "Выберите место сохранения изображения";
                    dialog.InitialDirectory = Application.StartupPath;
                    dialog.Filter = "Image file | *.*";
                    var dlgrslt = dialog.ShowDialog();
                    if (dlgrslt == DialogResult.OK)
                    {
                        Image img = (Image)outputpicture;
                        img.Save(dialog.FileName, imageformat);
                    }
                }
            }
        }

        private void bMPToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveImage(1);
        }

        private void JPGToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveImage(2);
        }

        private void PNGToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveImage(3);
        }
        #endregion

        #region OpenImage
        private void OpenImage_Click(object sender, EventArgs e)
        {//открываем файл
            using (OpenFileDialog dialog = new OpenFileDialog())
            {
                dialog.Title = "Откройте необходимое изображение";
                dialog.InitialDirectory = Application.StartupPath;
                //-----------------------------------------------------
                //-----------------------------------------------------
                //Получение всех поддерживаемых форматов изображений
                ImageCodecInfo[] codecs = ImageCodecInfo.GetImageEncoders();
                string sep = "|";
                dialog.Filter = "Все файлы (*.*)|*.*";
                foreach (var c in codecs)
                {
                    string codecName = c.CodecName.Substring(8).Replace("Codec", "Files").Trim();
                    dialog.Filter = String.Format("{0}{1}{2} ({3})|{3}", dialog.Filter, sep, codecName, c.FilenameExtension);
                    
                }
                                
                //-----------------------------------------------------
                //-----------------------------------------------------
                var dlgrslt = dialog.ShowDialog();
                if (dlgrslt == System.Windows.Forms.DialogResult.OK)
                {
                    if (dialog.FileName != null)
                    {
                        imagefile = dialog.FileName;
                        UpdateOriginalPicture((Bitmap)Bitmap.FromFile(dialog.FileName));
                        EnableAllButtons();
                        if (ColumnHide)
                        {
                            outputpicture = inputpicture;
                            EditedPicture.Image = outputpicture;
                        }
                        else
                        {
                            EditedPicture.Image = null;
                            outputpicture = null;
                        }

                        PreviewForm.SetDefaultBitmap((Bitmap)inputpicture.GetThumbnailImage(GetThumbnailSize(inputpicture).Width, GetThumbnailSize(inputpicture).Height, null, IntPtr.Zero));

                    }
                };
            }
        }
        #endregion

        #region ExtendedTools/Methods

        private void AboutDialog_Click(object sender, EventArgs e)
        {
            AboutProgram prog = new AboutProgram();
            prog.ShowDialog();
            prog.Dispose();
        }

        /// <summary>
        /// Метод повторной загрузки последнего изображения
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ReloadOriginal_Click(object sender, EventArgs e)
        {
            if (imagefile != null)
            {
                UpdateOriginalPicture((Bitmap)Bitmap.FromFile(imagefile));
                PreviewForm.SetDefaultBitmap(inputpicture);
                if (ColumnHide)
                {
                    outputpicture = inputpicture;
                    EditedPicture.Image = outputpicture;
                }
            }
            else
            {
                MessageBox.Show("Изображение невозможно перезагрузить. \nВозможные причины:\n1) Вы еще не загружали изображение\n2) Произошла программная ошибка и ссылка была потеряна", "Нет изображения", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void HideShowLeft_Click(object sender, EventArgs e)
        {
            if (!ColumnHide)
            {
                ToolLayout.ColumnStyles[0].Width = 0F;
                ColumnHide = true;
                FiltersMenuList.Visible = true;
                HideShowLeft.Image = global::SimplePainterNamespace.Properties.Resources.show;
                if (outputpicture == null)
                {
                    outputpicture = inputpicture;
                    EditedPicture.Image = outputpicture;
                }
                Somethings.Visible = false;
                ToolLayout.RowStyles[2].SizeType = SizeType.Absolute;
                ToolLayout.RowStyles[2].Height = 34;
            }
            else
            {
                ColumnHide = false;
                ToolLayout.ColumnStyles[0].Width = 50F;
                FiltersMenuList.Visible = false;
                HideShowLeft.Image = global::SimplePainterNamespace.Properties.Resources.hide;
                Somethings.Visible = true;
                ToolLayout.RowStyles[2].SizeType = SizeType.Percent;
                ToolLayout.RowStyles[2].Height = 33;
            }
        }

        /// <summary>
        /// Применить случайный фильтр для изображения
        /// Использует  StarterBody и массив FiltersNames
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RandomFilter_Click(object sender, EventArgs e)
        {
            if (inputpicture != null || outputpicture != null)
            {
                Random rand = new Random();
                StarterBody(FiltersNames[rand.Next(0, FiltersNames.Length - 1)]);
            }
        }
        
        /// <summary>
        /// Показать форму ChangeResolution для изменения размеров
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RezolutionTools_Click(object sender, EventArgs e)
        {
            if (inputpicture != null)
            {
                if (outputpicture == null) { ChangeResolution rez = new ChangeResolution(inputpicture); rez.ShowDialog(); outputpicture = rez.ReturnImage; rez.Dispose(); }
                else { ChangeResolution rez = new ChangeResolution(outputpicture); rez.ShowDialog(); outputpicture = rez.ReturnImage; rez.Dispose(); }
                EditedPicture.Image = outputpicture;
                PreviewForm.SetBitmap(outputpicture);
                
            }
        }

        private void StatsImage_Click(object sender, EventArgs e)
        {
            if (inputpicture != null)
            {
                MessageBox.Show("Имя файла: " + System.IO.Path.GetFileName(imagefile).ToString() + "\n" +
               "Путь к файлу: " + System.IO.Path.GetDirectoryName(imagefile).ToString() + "\n" +
               "Формат пикселей изображения: " + inputpicture.PixelFormat.ToString() + "\n" +
               "Горизонтальное разрешение: " + inputpicture.PhysicalDimension.Width.ToString() + "\n" +
               "Вертикальное разрешение: " + inputpicture.PhysicalDimension.Height.ToString() + "\n" +
               "Вертикальный DPI: " + inputpicture.VerticalResolution.ToString() + "\n" +
               "Горизонтальный DPI: " + inputpicture.HorizontalResolution.ToString() + "\n" +
               "Общее количество пикселей: " + (inputpicture.PhysicalDimension.Width * inputpicture.PhysicalDimension.Height).ToString() + "\n" +
               "Дополнительные данные: " + inputpicture.Tag);
            }
        }

        private void TextTools_Click(object sender, EventArgs e)
        {
            AddTextForm eff;
            if (outputpicture != null)
            {
                eff = new AddTextForm(outputpicture);
                eff.ShowDialog();
                outputpicture = eff.GetBitmap;
                EditedPicture.Image = outputpicture;

            }
            else
            {
                eff = new AddTextForm(inputpicture);
                eff.ShowDialog();
                outputpicture = eff.GetBitmap;
                EditedPicture.Image = outputpicture;
            }


        }

        /// <summary>
        /// !PreviewOpened: Открыть форму PreviewForm, перечитать выходное изображение в качестве превью
        /// PreviewOpened: скрыть форму
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ZoomView_Click(object sender, EventArgs e)
        {
            if (!PreviewOpened)
            {
                PreviewOpened = true;
                if (outputpicture != null)
                {
                    Size thumbnailSize = GetThumbnailSize(outputpicture);
                    PreviewForm.SetBitmap((Bitmap)outputpicture.GetThumbnailImage(thumbnailSize.Width, thumbnailSize.Height, null, IntPtr.Zero));
                }
                PreviewForm.Show();
            }
            else
            {
                PreviewOpened = false;
                PreviewForm.Hide();
            }
        }

        static Size GetThumbnailSize(Image original)
        {

            // Maximum size of any dimension.
            const int maxPixels = 400;

            // Width and height.
            int originalWidth = original.Width;
            int originalHeight = original.Height;

            // Compute best factor to scale entire image based on larger dimension.
            double factor;
            if (originalWidth > originalHeight)
            {
                factor = (double)maxPixels / originalWidth;
            }
            else
            {
                factor = (double)maxPixels / originalHeight;
            }

            // Return thumbnail size.
            return new Size((int)(originalWidth * factor), (int)(originalHeight * factor));
        }


        #endregion

        #region invokes methods
        /// <summary>
        /// Зарезервированный метод для изменения текстовой метки из другого потока
        /// Необходимо исправить EditedPicture на правильную текстовую метку
        /// </summary>
        /// <param name="s">Входная строка для изменения </param>
        private void UpdateLabel(string s)
        {
            if (EditedPicture.InvokeRequired)
            {
                this.BeginInvoke(new MethodInvoker(() => UpdateLabel(s)));
            }
            else
            {
                EditedPicture.Image = outputpicture;
            }
        }

        /// <summary>
        /// Обновление объектов оригинального изображения
        /// </summary>
        /// <param name="img"></param>
        public void UpdateOriginalPicture(Bitmap img)
        {
            inputpicture = img;
            DefaultPicture.Image = inputpicture;
        }
        /// <summary>
        ///  Обновление объектов измененного изображения
        /// </summary>
        /// <param name="img"></param>
        private void UpdateEditedPicture(Bitmap img)
        {
            outputpicture = img;
           EditedPicture.Image = outputpicture;
        }
        /// <summary>
        /// Вспомогательный класс для отображения измененного изображения в окне формы
        /// выполняет InvokeRequest
        /// </summary>
        private void UpdateRefEditedPicture()
        {
            if (EditedPicture.InvokeRequired)
            {
                // It's on outputarr different thread, so use Invoke.
                this.BeginInvoke(new MethodInvoker(() => UpdateRefEditedPicture()));
            }
            else
            {
                // It's on the same thread, no need for Invoke
                EditedPicture.Image = outputpicture;
            }
        }
        #endregion

        #region RunFilters init

        /// <summary>
        /// Обработчик нажатия кнопки
        /// Вызывает StarterBody
        /// Использует FiltersList.SelectedNode.Text
        /// </summary>
        /// <param name="sender">заголовок отправителя</param>
        /// <param name="e"> данные о событии EventArgs</param>
        private void StartButton_Click(object sender, EventArgs e)
        { StarterBody(CurrentFilter); }

        private void StarterBody(string filter_arg)
        {
            try
            {
                string caption = this.Text;
                CurrentFilter = filter_arg;
                StartButton.Text = "Выполнение операции...";
                StartButton.Enabled = false;
                this.Text = "Подождите... Работаю ^__^";
                System.Diagnostics.Stopwatch wtch = new System.Diagnostics.Stopwatch();
                wtch.Start();
                this.UseBW();
                wtch.Stop();
                if (PreviewOpened) PreviewForm.SetBitmap((Bitmap)outputpicture.GetThumbnailImage(GetThumbnailSize(outputpicture).Width, GetThumbnailSize(outputpicture).Height, null, IntPtr.Zero));
                TimeElapsed.Text = wtch.Elapsed.TotalMilliseconds + " ms";
                this.Text = caption;
                StartButton.Text = "Применить фильтр";
                StartButton.Enabled = true; ;

            }
            finally { StartButton.BackColor = Color.Green; Timer tmr = new Timer(); tmr.Interval = 1500; tmr.Tick += (sender, e) => { StartButton.BackColor = SystemColors.Control; }; tmr.Start(); }

        }

        /// <summary>
        /// Запуск выполнения обработки изображения в отдельном потоке
        /// Определяет действия для выполнения и события обработчика
        ///  worker.DoWork +=    worker_DoWork;
        ///  worker.RunWorkerCompleted += worker_RunWorkerCompleted;
        /// </summary>
        private void UseBW()
        {

            using (BackgroundWorker worker = new BackgroundWorker())
            {
                worker.DoWork += worker_DoWork;
                worker.RunWorkerCompleted += (sender, e) => { UpdateRefEditedPicture(); };
                worker.RunWorkerAsync();
                while (worker.IsBusy)
                {
                    Application.DoEvents();
                }
            }
        }

        void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            if (outputpicture != null)
            { outputpicture =  PerformOperation(CurrentFilter, outputpicture); }
            else
            { outputpicture = PerformOperation(CurrentFilter, inputpicture); }

        }

        private Bitmap PerformOperation(string op, Bitmap x)
        {
            if (!_filters.ContainsKey(op))
                throw new ArgumentException(string.Format("Operation {0} is invalid", op), "op");
            return _filters[op](x);
        }
        #endregion

        #region Picture View Modes
        private void NormalViewMode_Click(object sender, EventArgs e)
        {
            NormalViewMode.CheckState = CheckState.Checked;
            ZoomViewMode.CheckState = CheckState.Unchecked;
            DefaultPicture.Dock = DockStyle.None;
            EditedPicture.Dock = DockStyle.None;
            DefaultPicture.SizeMode = PictureBoxSizeMode.AutoSize;
            EditedPicture.SizeMode = PictureBoxSizeMode.AutoSize;

        }
        
        private void ZoomViewMode_Click(object sender, EventArgs e)
        {
                ZoomViewMode.CheckState = CheckState.Checked;
                NormalViewMode.CheckState = CheckState.Unchecked;
                DefaultPicture.Dock = DockStyle.Fill;
                EditedPicture.Dock = DockStyle.Fill;
                DefaultPicture.SizeMode = PictureBoxSizeMode.Zoom;
                EditedPicture.SizeMode = PictureBoxSizeMode.Zoom;
            
        }
        #endregion

        #region screenshot menu
        private void Screenshot_Click(object sender, EventArgs e)
        {
            this.Hide();
            ScreenShotRegion tts = new ScreenShotRegion();
            tts.Show();
            tts.Dispose();
        }

        private void PrimaryScreenScreenShotItem_Click(object sender, EventArgs e)
        {
            Timer screenshottimer = new Timer();
            screenshottimer.Interval = 300;
           this.WindowState = FormWindowState.Minimized;
           screenshottimer.Tick += (omg, tmrs) =>
           {
               screenshottimer.Stop();
             UpdateOriginalPicture(ScreenshotScreen.ScreenShotPrimaryScreen());
             
             
             this.WindowState = FormWindowState.Normal;
            };
            screenshottimer.Start();
            EnableAllButtons();
           
        }

        private void AllScreenScreenShotMenuItem_Click(object sender, EventArgs e)
        {
            Timer screenshottimer = new Timer();
            screenshottimer.Interval = 300;
            this.WindowState = FormWindowState.Minimized;
            screenshottimer.Tick += (omg1, tmrs1) =>
            {
                screenshottimer.Stop();
                UpdateOriginalPicture(ScreenshotScreen.AllScreenScreenShot());
               
                
                this.WindowState = FormWindowState.Normal;
            };
            screenshottimer.Start();
            EnableAllButtons();
        }

        private void ActiveWindowsScreenshot_Click(object sender, EventArgs e)
        {

            Timer screenshottimer = new Timer();
            screenshottimer.Interval = 3000;
            this.WindowState = FormWindowState.Minimized;
            screenshottimer.Tick += (omg2, tmrs2) =>
            {
                screenshottimer.Stop();
                 UpdateOriginalPicture(ScreenshotScreen.CaptureActiveWindow(NativeMethods.GetForegroundWindow()));
              
                
                this.WindowState = FormWindowState.Normal;
            };
            screenshottimer.Start();
            EnableAllButtons();
           
        }

        private void ScreenRegion_Click(object sender, EventArgs e)
        {
                 ScreenShotRegion tts = new ScreenShotRegion();
                 tts.ShowDialog();
                 UpdateOriginalPicture((Bitmap)ScreenshotScreen.ScreenshotedRegion);
               
                 tts.Dispose();
                 EnableAllButtons();
        }
        #endregion


    }
}