using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Windows.Forms;
using SimplePainterNamespace.Properties;

[assembly: CLSCompliant(true)]

namespace SimplePainterNamespace
{
    /// <summary>
    ///     Класс, представляющий собой основное окно программы.
    ///     Наследуется из класса Form
    /// </summary>
    public partial class AdvancedEditor : Form
    {
        #region Constructor

        /// <summary>
        ///     Конструктор формы
        ///     Инициализирует компоненты
        ///     Инициализация превью форму
        ///     Создает 2 tooltip
        ///     Получает список всех фильтров для пункта меню на основе массива FiltersNames
        /// </summary>
        public AdvancedEditor()
        {
            InitializeComponent();
            var origtooltip = new ToolTip();
            origtooltip.SetToolTip(DefaultPicture, "Оригинальное изображение");
            var edittooltip = new ToolTip();
            edittooltip.SetToolTip(EditedPicture, "Обработанное изображение");
            //Инициализация превью форму
            _previewForm = new ImagePreviewForm();
            //Получение списка всех фильтров для пункта меню на основе массива FiltersNames
            int i = 1;
            foreach (string x in _filtersNames)
            {
                var node = new TreeNode {Text = x};
                node.Name = node + i.ToString(CultureInfo.InvariantCulture);
                FiltersList.Nodes.Add(node);
                var filter = new ToolStripMenuItem();
                filter.Name = filter + i.ToString(CultureInfo.InvariantCulture);
                filter.Text = x;
                i++;
                filter.Click += (sender, e) =>
                    {
                        _currentFilter = filter.Text;
                        StarterBody(_currentFilter);
                    };
                FiltersMenuList.DropDownItems.Add(filter);
            }


            DragDrop += Form_DragDrop;
            DragEnter += Form_DragEnter;
            KeyDown += Form_KeyDown;
        }

        #endregion

        #region Variables

        /// <summary>
        ///     Информация о текущем фильтре
        /// </summary>
        private static string _currentFilter;

        /// <summary>
        ///     Путь до последнего открытого изображения
        /// </summary>
        private static string _imagefile;


        /// <summary>
        ///     Входное изображение
        /// </summary>
        private static Bitmap _inputpicture;

        /// <summary>
        ///     Измененное изображение
        /// </summary>
        private static Bitmap _outputpicture;

        private readonly Dictionary<string, Func<Bitmap, Bitmap>> _filters =
            new Dictionary<string, Func<Bitmap, Bitmap>>
                {
                    {"Медианный фильтр", ColorDiapasoneFilters.MedianFilter},
                    {"Перестановка цветов", ColorDiapasoneFilters.ChangeColors},
                    {"Тень", x => ColorDiapasoneFilters.Shadow(x)},
                    {"Пикселизация", x => ColorDiapasoneFilters.Pixelate(x)},
                    {"Усредняющий фильтр", x => Convolution.StartConvolution(x, PredefinedKernels.MeanRemoval)},
                    {"Сепия", ColorDiapasoneFilters.Sepia},
                    {"Инвертирование", ColorDiapasoneFilters.Invert},
                    {"Оттенки серого", ColorDiapasoneFilters.Grayscale},
                    {"Шум", x => ColorDiapasoneFilters.Noise(x)},
                    {"Шум \"Salt and Pepper\"", ColorDiapasoneFilters.NoiseSaltAndPepper},
                    {"Ночной режим", ColorDiapasoneFilters.Nightvision},
                    {"Только красный", ColorDiapasoneFilters.Armageddon},
                    {"Только синий", ColorDiapasoneFilters.OnlyBlue},
                    {"Гауссово размытие", x => Convolution.StartConvolution(x, PredefinedKernels.GaussianBlur)},
                    {"Резкость", x => Convolution.StartConvolution(x, PredefinedKernels.Sharpen)},
                    {"Обнаружить границы", x => Convolution.StartConvolution(x, PredefinedKernels.EdgeDetect)},
                    {"Выделить границы", x => Convolution.StartConvolution(x, PredefinedKernels.EdgeUp)},
                    {"Неон", x => Convolution.StartConvolution(x, PredefinedKernels.Neon)},
                    {"Придать рельеф", x => Convolution.StartConvolution(x, PredefinedKernels.Relief)},
                    {
                        "Проработать контраст с его увеличением",
                        x => Convolution.StartConvolution(x, PredefinedKernels.KontrastCheck)
                    },
                    {"Случайный выбор", x => ColorDiapasoneFilters.RandomSelect(x)},
                    {"Рассеивание", x => ColorDiapasoneFilters.Dispersal(x)},
                    {"Сдвиг", ColorDiapasoneFilters.Shift},
                    {"Вертикальный сдвиг", ColorDiapasoneFilters.ShiftVertical},
                    {"Отражение", ColorDiapasoneFilters.Mirroring},
                    {"Фильтр Лапласа", x => Convolution.StartConvolution(x, PredefinedKernels.Laplasian)},
                    {"Фильтр Собела (вертикаль)", x => Convolution.StartConvolution(x, PredefinedKernels.Sobel1)},
                    {"Фильтр Собела (горизонталь)", x => Convolution.StartConvolution(x, PredefinedKernels.Sobel2)},
                    {"Вертикальное отражение", x => ColorDiapasoneFilters.VerticalMap(_inputpicture)}
                };

        /// <summary>
        ///     Массив имен текущих фильтров
        ///     На его основе формируются другие различные части программы
        /// </summary>
        private readonly string[] _filtersNames =
            {
                "Шум", "Шум \"Salt and Pepper\"", "Резкость", "Медианный фильтр", "Усредняющий фильтр",
                "Фильтр Собела (вертикаль)", "Фильтр Собела (горизонталь)", "Фильтр Лапласа",
                "Гауссово размытие", "Выделить границы", "Обнаружить границы",
                "Проработать контраст с его увеличением", "Неон", "Придать рельеф",
                "Перестановка цветов", "Тень", "Пикселизация", "Сепия", "Инвертирование", "Оттенки серого",
                "Ночной режим", "Только красный", "Только синий", "Случайный выбор", "Рассеивание",
                "Сдвиг", "Вертикальный сдвиг", "Отражение", "Вертикальное отражение"
            };


        private readonly Dictionary<string, string> _filtersinformations
            = new Dictionary<string, string>
                {
                    {"Медианный фильтр", "Эффективный фильтр подавления шумов на изображениях."},
                    {"Перестановка цветов", "Смещение цветовых компонент каждого пикселя изображения вправо."},
                    {
                        "Тень",
                        "Создание новой копии изображения с допольнительной площадью, эмулирующей отбрасывание тени от изображения"
                    },
                    {
                        "Пикселизация",
                        "Эмуляция шумового фильтра при котором все изображение не просто теряет свою четкость, а оформляется в виде усредненных пикселей оригинального изображения с определенным размером"
                    },
                    {
                        "Усредняющий фильтр",
                        "Малоэффективный фильтр подавления небольших шумов на изображении. Происходит значительное потеря четкости"
                    },
                    {
                        "Сепия",
                        "Эффект, применяемый над любым изображением, позволяет представить изображение в старом стиле. Иначе говоря окрашивает изображения в желто-коричневые оттенки"
                    },
                    {"Инвертирование", "Обращает на противоположные цвета в спектре все пиксели изображения"},
                    {
                        "Оттенки серого",
                        "Фильтр исскуственной потери цветности изображений. В результате на изображении все цвета представляются в серых тонах."
                    },
                    {
                        "Шум",
                        "Фильтр исскуственной деформации изображений. На случайные места на изображении наносятся новые цветовые пиксели, цвет которых выбирается автоматически"
                    },
                    {
                        "Шум \"Salt and Pepper\"",
                        "Фильтр эмуляции деформации изображений. В отличие от обычного шума вместо случайного шума из всего диапазона цветов используется только черный и белый цвета."
                    },
                    {
                        "Ночной режим",
                        "Обработка изображения при которой выполняется потеря всех цветовых компонент кроме зеленой."
                    },
                    {
                        "Только красный",
                        "Обработка изображения при которой выполняется потеря всех цветовых компонент кроме красной."
                    },
                    {
                        "Только синий",
                        "Обработка изображения при которой выполняется потеря всех цветовых компонент кроме синей."
                    },
                    {
                        "Гауссово размытие",
                        "Обработка изображения с помощью нормализованной матрицы , сформированной по закону Гаусса. Данный фильтр позволяет производить мягкую потерю резкости."
                    },
                    {"Резкость", "Фильтр обработки изображений для частичного восстановления контуров объектов. "},
                    {
                        "Обнаружить границы",
                        "Фильтр незначительного выделения границ. Неэфективен на изображениях с большим количеством объектов."
                    },
                    {
                        "Выделить границы",
                        "Фильтр выделения границ любых объектов на изображении. Для качественного результата необходимо применение фильтра резкости"
                    },
                    {
                        "Неон",
                        "Комбинированный фильтр обнаружения границ. Выделение границ происходит с красивым эффектом различных цветов"
                    },
                    {
                        "Придать рельеф",
                        "Фильтр выделения краев с последующим вдавливанием. В результате получается эффект рельефа местности."
                    },
                    {
                        "Проработать контраст с его увеличением",
                        "Усиление цветности изображения по среднему значению окружающих пикселей с одновременной корректировкой контрастности"
                    },
                    {
                        "Случайный выбор",
                        "Обрабатывается каждый пиксель изображения с его изменением в зависимости от случайного цвета в неком радиусе от редактируемого пикселя"
                    },
                    {
                        "Рассеивание",
                        "Обрабатывается каждый пиксель изображения с его изменением в зависимости от случайного цвета в неком радиусе от редактируемого пикселя. В отличие от фильтра \"Случайный выбор\"все цвета сохраняются"
                    },
                    {"Сдвиг", "Сдвиг наборов пикселей в различные стороны в зависимости от четности строк. "},
                    {
                        "Вертикальный сдвиг",
                        "Сдвиг наборов пикселей в различные стороны в зависимости от четности столбцов. "
                    },
                    {"Отражение", "Эмуляция получения изображения в зеркале"},
                    {"Фильтр Лапласа", "Высококачественный фильтр определения границ изображения."},
                    {
                        "Фильтр Собела (вертикаль)",
                        "Выделяет горизонтальные края отдельно на изображении. Для правильной работы требует изображения в оттенках серого"
                    },
                    {
                        "Фильтр Собела (горизонталь)",
                        "Выделяет вертикальный края отдельно на изображении. Для правильной работы требует изображения в оттенках серого"
                    },
                    {
                        "Вертикальное отражение",
                        "Эмуляция получения изображения в зеркале с использованием вертикальных координат"
                    }
                };


        /// <summary>
        ///     Объявление _previewForm для отображения миниатюр текущих изображений
        /// </summary>
        private readonly ImagePreviewForm _previewForm;

        /// <summary>
        ///     Свойство-флаг для обработчика события HideShowLeft_Click
        /// </summary>
        private bool _columnHide;

        /// <summary>
        ///     Флаг _previewForm.
        ///     False - Hide()
        ///     True - Show()
        /// </summary>
        private bool _previewOpened;

        #endregion

        #region clearing/disposing/closing methods

        /// <summary>
        ///     Обработчик завершения программы при нажатии на пункт меню "Выход"
        /// </summary>
        private void ExitMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void DeletePictureMenuItem_Click(object sender, EventArgs e)
        {
            _outputpicture = null;
            EditedPicture.Image = null;
            _previewForm.DelOutput();
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
        ///     Метод очистки рабочей области
        ///     inputpicture = null;
        ///     EditedPicture.Image = null;
        ///     outputpicture = null;
        ///     DefaultPicture.Image = null;
        ///     FiltersList.Enabled = false;
        ///     StartButton.Enabled = false;
        ///     + _previewForm.CleanAllPictures();
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CloseAll_Click(object sender, EventArgs e)
        {
            _inputpicture = null;
            EditedPicture.Image = null;
            _outputpicture = null;
            DefaultPicture.Image = null;
            DisableAllButtons();
            _previewForm.CleanAllPictures();
        }

        /// <summary>
        ///     Обработчик закрытия изображения
        /// </summary>
        /// <param name="sender">заголовок отправителя</param>
        /// <param name="e">данные о событии EventArgs</param>
        private void CloseImage_Click(object sender, EventArgs e)
        {
            //Стираем всё после закрытия
            _inputpicture = null;
            EditedPicture.Image = null;
            _outputpicture = null;
            DefaultPicture.Image = null;
            DisableAllButtons();
            _previewForm.CleanAllPictures();
        }

        #endregion

        #region IDragDrop

        private void Form_DragDrop(object sender, DragEventArgs e)
        {
            if (_inputpicture != null)
            {
                MessageBox.Show("", "", MessageBoxButtons.OK, MessageBoxIcon.Question);
            }
            var file = (string) e.Data.GetData(DataFormats.FileDrop);
            try
            {
                if (!_columnHide)
                {
                    UpdateOriginalPicture((Bitmap) Image.FromFile(file));
                }
                else
                {
                    UpdateOriginalPicture((Bitmap) Image.FromFile(file));
                    _outputpicture = _inputpicture;
                    EditedPicture.Image = _outputpicture;
                }
            }
                //catch (Exception) { MessageBox.Show("Возникла ошибка при загрузке данного файла"); }
            finally
            {
                _imagefile = file;
            }
        }

        private void Form_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.V)
            {
                if (Clipboard.ContainsImage())
                {
                    UpdateOriginalPicture((Bitmap) Clipboard.GetImage());
                    EnableAllButtons();
                }
            }

            if (e.Control && e.KeyCode == Keys.C)
            {
                if (_outputpicture != null)
                {
                    Clipboard.SetImage(_outputpicture);
                }
            }
        }

        private void Form_DragEnter(object sender, DragEventArgs e)
        {
            // Если данный изображение или файл -> разрешить
            if (e.Data.GetDataPresent(DataFormats.Bitmap) || e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        #endregion

        #region filters information

        /// <summary>
        ///     Обработчик изменения выбора фильтра
        ///     Меняет описание о текущем фильтре
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FiltersList_AfterSelect(object sender, TreeViewEventArgs e)
        {
            _currentFilter = FiltersList.SelectedNode.Text;
            SetCurrentOperationsInfo(_currentFilter);
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
        ///     Метод сохранения картинки в определенном формате
        /// </summary>
        /// <param name="flag">
        ///     1 - BMP
        ///     2- JPEG
        ///     3- PNG
        /// </param>
        private void SaveImage(int flag)
        {
            ImageFormat imageformat = null;

            switch (flag)
            {
                case 1:
                    imageformat = new ImageFormat(ImageFormat.Bmp.Guid);
                    break;
                case 2:
                    imageformat = new ImageFormat(ImageFormat.Jpeg.Guid);
                    break;
                case 3:
                    imageformat = new ImageFormat(ImageFormat.Png.Guid);
                    break;
            }

            //Сохранение картинки
            if (EditedPicture.Image != null)
            {
                using (var dialog = new SaveFileDialog())
                {
                    dialog.Title = "Выберите место сохранения изображения";
                    dialog.InitialDirectory = Application.StartupPath;
                    dialog.Filter = "Image file | *.*";
                    DialogResult dlgrslt = dialog.ShowDialog();
                    if (dlgrslt == DialogResult.OK)
                    {
                        var img = (Image) _outputpicture;
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
        {
//открываем файл
            using (var dialog = new OpenFileDialog())
            {
                dialog.Title = "Откройте необходимое изображение";
                dialog.InitialDirectory = Application.StartupPath;
                //-----------------------------------------------------
                //-----------------------------------------------------
                //Получение всех поддерживаемых форматов изображений
                ImageCodecInfo[] codecs = ImageCodecInfo.GetImageEncoders();
                const string sep = "|";
                dialog.Filter = "Все файлы (*.*)|*.*";
                foreach (ImageCodecInfo c in codecs)
                {
                    string codecName = c.CodecName.Substring(8).Replace("Codec", "Files").Trim();
                    dialog.Filter = String.Format("{0}{1}{2} ({3})|{3}", dialog.Filter, sep, codecName,
                                                  c.FilenameExtension);
                }

                //-----------------------------------------------------
                //-----------------------------------------------------
                DialogResult dlgrslt = dialog.ShowDialog();
                if (dlgrslt == DialogResult.OK)
                {
                    if (dialog.FileName != null)
                    {
                        _imagefile = dialog.FileName;
                        UpdateOriginalPicture((Bitmap) Image.FromFile(dialog.FileName));
                        EnableAllButtons();
                        if (_columnHide)
                        {
                            _outputpicture = _inputpicture;
                            EditedPicture.Image = _outputpicture;
                        }
                        else
                        {
                            EditedPicture.Image = null;
                            _outputpicture = null;
                        }

                        _previewForm.SetDefaultBitmap(
                            (Bitmap)
                            _inputpicture.GetThumbnailImage(GetThumbnailSize(_inputpicture).Width,
                                                            GetThumbnailSize(_inputpicture).Height, null, IntPtr.Zero));
                    }
                }
            }
        }

        #endregion

        #region ExtendedTools/Methods

        private void AboutDialog_Click(object sender, EventArgs e)
        {
            var prog = new AboutProgram();
            prog.ShowDialog();
            prog.Dispose();
        }

        /// <summary>
        ///     Метод повторной загрузки последнего изображения
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ReloadOriginal_Click(object sender, EventArgs e)
        {
            if (_imagefile != null)
            {
                UpdateOriginalPicture((Bitmap) Image.FromFile(_imagefile));
                _previewForm.SetDefaultBitmap(_inputpicture);
                if (_columnHide)
                {
                    _outputpicture = _inputpicture;
                    EditedPicture.Image = _outputpicture;
                }
            }
            else
            {
                MessageBox.Show(
                    "Изображение невозможно перезагрузить. \nВозможные причины:\n1) Вы еще не загружали изображение\n2) Произошла программная ошибка и ссылка была потеряна",
                    "Нет изображения", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void HideShowLeft_Click(object sender, EventArgs e)
        {
            if (!_columnHide)
            {
                ToolLayout.ColumnStyles[0].Width = 0F;
                _columnHide = true;
                FiltersMenuList.Visible = true;
                HideShowLeft.Image = Resources.show;
                if (_outputpicture == null)
                {
                    _outputpicture = _inputpicture;
                    EditedPicture.Image = _outputpicture;
                }
                Somethings.Visible = false;
                ToolLayout.RowStyles[2].SizeType = SizeType.Absolute;
                ToolLayout.RowStyles[2].Height = 34;
            }
            else
            {
                _columnHide = false;
                ToolLayout.ColumnStyles[0].Width = 50F;
                FiltersMenuList.Visible = false;
                HideShowLeft.Image = Resources.hide;
                Somethings.Visible = true;
                ToolLayout.RowStyles[2].SizeType = SizeType.Percent;
                ToolLayout.RowStyles[2].Height = 33;
            }
        }

        /// <summary>
        ///     Применить случайный фильтр для изображения
        ///     Использует  StarterBody и массив FiltersNames
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RandomFilter_Click(object sender, EventArgs e)
        {
            if (_inputpicture != null || _outputpicture != null)
            {
                var rand = new Random();
                StarterBody(_filtersNames[rand.Next(0, _filtersNames.Length - 1)]);
            }
        }

        /// <summary>
        ///     Показать форму ChangeResolution для изменения размеров
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RezolutionTools_Click(object sender, EventArgs e)
        {
            if (_inputpicture != null)
            {
                if (_outputpicture == null)
                {
                    var rez = new ChangeResolution(_inputpicture);
                    rez.ShowDialog();
                    _outputpicture = rez.ReturnImage;
                    rez.Dispose();
                }
                else
                {
                    var rez = new ChangeResolution(_outputpicture);
                    rez.ShowDialog();
                    _outputpicture = rez.ReturnImage;
                    rez.Dispose();
                }
                EditedPicture.Image = _outputpicture;
                _previewForm.SetBitmap(_outputpicture);
            }
        }

        private void StatsImage_Click(object sender, EventArgs e)
        {
            if (_inputpicture != null)
            {
                MessageBox.Show("Имя файла: " + Path.GetFileName(_imagefile) + "\n" +
                                "Путь к файлу: " + Path.GetDirectoryName(_imagefile) + "\n" +
                                "Формат пикселей изображения: " + _inputpicture.PixelFormat.ToString() + "\n" +
                                "Горизонтальное разрешение: " + _inputpicture.PhysicalDimension.Width.ToString() + "\n" +
                                "Вертикальное разрешение: " + _inputpicture.PhysicalDimension.Height.ToString() + "\n" +
                                "Вертикальный DPI: " + _inputpicture.VerticalResolution.ToString() + "\n" +
                                "Горизонтальный DPI: " + _inputpicture.HorizontalResolution.ToString() + "\n" +
                                "Общее количество пикселей: " +
                                (_inputpicture.PhysicalDimension.Width*_inputpicture.PhysicalDimension.Height).ToString() +
                                "\n" +
                                "Дополнительные данные: " + _inputpicture.Tag);
            }
        }

        private void TextTools_Click(object sender, EventArgs e)
        {
            AddTextForm eff;
            if (_outputpicture != null)
            {
                eff = new AddTextForm(_outputpicture);
                eff.ShowDialog();
                _outputpicture = eff.GetBitmap;
                EditedPicture.Image = _outputpicture;
            }
            else
            {
                eff = new AddTextForm(_inputpicture);
                eff.ShowDialog();
                _outputpicture = eff.GetBitmap;
                EditedPicture.Image = _outputpicture;
            }
        }

        /// <summary>
        ///     !PreviewOpened: Открыть форму _previewForm, перечитать выходное изображение в качестве превью
        ///     PreviewOpened: скрыть форму
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ZoomView_Click(object sender, EventArgs e)
        {
            if (!_previewOpened)
            {
                _previewOpened = true;
                if (_outputpicture != null)
                {
                    Size thumbnailSize = GetThumbnailSize(_outputpicture);
                    _previewForm.SetBitmap(
                        (Bitmap)
                        _outputpicture.GetThumbnailImage(thumbnailSize.Width, thumbnailSize.Height, null, IntPtr.Zero));
                }
                _previewForm.Show();
            }
            else
            {
                _previewOpened = false;
                _previewForm.Hide();
            }
        }

        private static Size GetThumbnailSize(Image original)
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
                factor = (double) maxPixels/originalWidth;
            }
            else
            {
                factor = (double) maxPixels/originalHeight;
            }

            // Return thumbnail size.
            return new Size((int) (originalWidth*factor), (int) (originalHeight*factor));
        }

        #endregion

        #region invokes methods

        /// <summary>
        ///     Зарезервированный метод для изменения текстовой метки из другого потока
        ///     Необходимо исправить EditedPicture на правильную текстовую метку
        /// </summary>
        private void UpdateLabel()
        {
            if (EditedPicture.InvokeRequired)
            {
                BeginInvoke(new MethodInvoker(UpdateLabel));
            }
            else
            {
                EditedPicture.Image = _outputpicture;
            }
        }

        /// <summary>
        ///     Обновление объектов оригинального изображения
        /// </summary>
        /// <param name="img"></param>
        public void UpdateOriginalPicture(Bitmap img)
        {
            _inputpicture = img;
            DefaultPicture.Image = _inputpicture;
        }

/*
        /// <summary>
        ///  Обновление объектов измененного изображения
        /// </summary>
        /// <param name="img"></param>
        private void UpdateEditedPicture(Bitmap img)
        {
            _outputpicture = img;
           EditedPicture.Image = _outputpicture;
        }
*/

        /// <summary>
        ///     Вспомогательный класс для отображения измененного изображения в окне формы
        ///     выполняет InvokeRequest
        /// </summary>
        private void UpdateRefEditedPicture()
        {
            if (EditedPicture.InvokeRequired)
            {
                // It's on outputarr different thread, so use Invoke.
                BeginInvoke(new MethodInvoker(UpdateRefEditedPicture));
            }
            else
            {
                // It's on the same thread, no need for Invoke
                EditedPicture.Image = _outputpicture;
            }
        }

        #endregion

        #region RunFilters init

        /// <summary>
        ///     Обработчик нажатия кнопки
        ///     Вызывает StarterBody
        ///     Использует FiltersList.SelectedNode.Text
        /// </summary>
        /// <param name="sender">заголовок отправителя</param>
        /// <param name="e"> данные о событии EventArgs</param>
        private void StartButton_Click(object sender, EventArgs e)
        {
            StarterBody(_currentFilter);
        }

        private void StarterBody(string filterArg)
        {
            try
            {
                string caption = Text;
                _currentFilter = filterArg;
                StartButton.Text = "Выполнение операции...";
                StartButton.Enabled = false;
                Text = "Подождите... Работаю ^__^";
                var wtch = new Stopwatch();
                wtch.Start();
                UseBw();
                wtch.Stop();
                if (_previewOpened)
                    _previewForm.SetBitmap(
                        (Bitmap)
                        _outputpicture.GetThumbnailImage(GetThumbnailSize(_outputpicture).Width,
                                                         GetThumbnailSize(_outputpicture).Height, null, IntPtr.Zero));
                TimeElapsed.Text = wtch.Elapsed.TotalMilliseconds + " ms";
                Text = caption;
                StartButton.Text = "Применить фильтр";
                StartButton.Enabled = true;
            }
            finally
            {
                StartButton.BackColor = Color.Green;
                var tmr = new Timer {Interval = 1500};
                tmr.Tick += (sender, e) => { StartButton.BackColor = SystemColors.Control; };
                tmr.Start();
            }
        }

        /// <summary>
        ///     Запуск выполнения обработки изображения в отдельном потоке
        ///     Определяет действия для выполнения и события обработчика
        ///     worker.DoWork +=    worker_DoWork;
        ///     worker.RunWorkerCompleted += worker_RunWorkerCompleted;
        /// </summary>
        private void UseBw()
        {
            using (var worker = new BackgroundWorker())
            {
                worker.DoWork += worker_DoWork;
                worker.RunWorkerCompleted += (sender, e) => UpdateRefEditedPicture();
                worker.RunWorkerAsync();
                while (worker.IsBusy)
                {
                    Application.DoEvents();
                }
            }
        }

        private void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            _outputpicture = PerformOperation(_currentFilter, _outputpicture ?? _inputpicture);
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

/*
        private void Screenshot_Click(object sender, EventArgs e)
        {
            Hide();
            var tts = new ScreenShotRegion();
            tts.Show();
            tts.Dispose();
        }
*/

        private void PrimaryScreenScreenShotItem_Click(object sender, EventArgs e)
        {
            var screenshottimer = new Timer {Interval = 300};
            WindowState = FormWindowState.Minimized;
            screenshottimer.Tick += (omg, tmrs) =>
                {
                    screenshottimer.Stop();
                    UpdateOriginalPicture(ScreenshotScreen.ScreenShotPrimaryScreen());


                    WindowState = FormWindowState.Normal;
                };
            screenshottimer.Start();
            EnableAllButtons();
        }

        private void AllScreenScreenShotMenuItem_Click(object sender, EventArgs e)
        {
            var screenshottimer = new Timer {Interval = 300};
            WindowState = FormWindowState.Minimized;
            screenshottimer.Tick += (omg1, tmrs1) =>
                {
                    screenshottimer.Stop();
                    UpdateOriginalPicture(ScreenshotScreen.AllScreenScreenShot());


                    WindowState = FormWindowState.Normal;
                };
            screenshottimer.Start();
            EnableAllButtons();
        }

        private void ActiveWindowsScreenshot_Click(object sender, EventArgs e)
        {
            var screenshottimer = new Timer {Interval = 3000};
            WindowState = FormWindowState.Minimized;
            screenshottimer.Tick += (omg2, tmrs2) =>
                {
                    screenshottimer.Stop();
                    UpdateOriginalPicture(ScreenshotScreen.CaptureActiveWindow(NativeMethods.GetForegroundWindow()));


                    WindowState = FormWindowState.Normal;
                };
            screenshottimer.Start();
            EnableAllButtons();
        }

        private void ScreenRegion_Click(object sender, EventArgs e)
        {
            var tts = new ScreenShotRegion();
            tts.ShowDialog();
            UpdateOriginalPicture(ScreenshotScreen.ScreenshotedRegion);

            tts.Dispose();
            EnableAllButtons();
        }

        #endregion
    }
}