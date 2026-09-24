using Library;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Forms1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        NightlightState newNightlight;
        NightlightController controller = new NightlightController();
        CheckBox chk_power; //Создаём ссылку заранее
        Slider sld_brightness;
        Label lbl_actuall_brightness;
        string pre_txt = "";
        string post_txt = "";
        public MainWindow()
        {
            InitializeComponent();

            OperationsListBox.Items.Add("Операция 1: Включение");
            OperationsListBox.Items.Add("Операция 2: Настройка яркости");
            OperationsListBox.Items.Add("Операция 3: Выключение");

            newNightlight = new NightlightState(); //Создаём новый объект для новой формы
        }

        // ОСНОВА ДЛЯ ВЫБОРА ОПЕРАЦИИ ИЗ СПИСКА
        private void OperationsListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Проверяем, что пользователь действительно что-то выбрал
            if (OperationsListBox.SelectedItem != null)
            {
                //Очистка дочерних элементов перед созданием новых (удаление детей, ха-ха)
                FuncControlsPanel.Children.Clear();

                // Получаем выбранный элемент
                var selectedItem = OperationsListBox.SelectedItem;

                // Если в ListBox хранятся просто строки (как мы добавили в конструкторе),
                // то приводим его к строке и выводим в label1.
                // Если у вас там сложные объекты, нужно будет обращаться к их свойствам.
                string operationName = selectedItem.ToString();

                // Выводим название операции в заголовок
                label1.Content = operationName;

                // Сбрасываем старые результаты при выборе новой операции
                labelPreconditionValue.Content = "";
                labelPostconditionValue.Content = "";
                labelResult.Content = "";

                // Сбрасываем цвета квадратиков в серый
                labelPreconditionColor.Background = Brushes.Gray;
                labelPostconditionColor.Background = Brushes.Gray;

                //Создание дочерних элементов при выборе опр. действия
                switch (OperationsListBox.SelectedIndex)
                {
                    case 0:
                        pre_txt = "Питание есть (chk_power.IsChecked = true)\n" +
                            "Свет выключен (NightlightState.isOn = false)";
                        post_txt = "Свет включен (NightlightState.isOn = true)\n" +
                            "Если яркость была 0 (выключен) – яркость 50 (NightlightState.Brightness = 50)\n" +
                            "Если яркость была настроена ранее — значение сохраняется (NightlightState.Brightness не изменяется)";
                        ControlsForOn();
                        break;
                    case 1:
                        pre_txt = "Свет включен (NightlightState.isOn = true)\n" +
                            "Новая яркость не равна старой (newBrightness != oldBrightness)";
                        post_txt = "Яркость изменена (NightlightState.Brightness = newBrightness)\n" +
                            "Если яркость была настроена на 0 – выключение (NightlightState.isOn = false)\n" +
                            "Если яркость была настроена не на 0 — состояние сохраняется (NightlightState.isOn = true)";
                        ControlsForSet();
                        break;
                    case 2:
                        pre_txt = "Свет включен (NightlightState.isOn = true)\n";
                        post_txt = "Свет выключен (NightlightState.isOn = false)\n" +
                            "Сохранение настроенной яркости (NightlightState.Brightness не изменяется)";
                        ControlsForOff();
                        break;
                    default:
                        break;
                }
            }
        }

        /// <summary>
        /// Создание элементов управления для функции включения
        /// </summary>
        private void ControlsForOn()
        {
            chk_power = new CheckBox
            {
                Content = "Питание есть", //Корректировка чекбокса
                FontSize = 14
            };
            
            FuncControlsPanel.Children.Add(chk_power);

            Label lbl_is_off = new Label
            {
                Content = "Свет должен быть выключен",
                FontSize = 14
            };
            FuncControlsPanel.Children.Add(lbl_is_off);
        }

        /// <summary>
        /// Реакция лейбла на движение ползунка
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void sld_brightness_set(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            var slider = (Slider)sender;
            var parent = slider.Parent as Panel;
            if (parent == null) return;

            lbl_actuall_brightness.Content = $"{e.NewValue:0}%";
        }

        /// <summary>
        /// Создание элементов управления для функции настройки яркости
        /// </summary>
        private void ControlsForSet()
        {
            Label lbl_is_on = new Label
            {
                Content = "Свет должен быть включен",
                FontSize = 14
            };
            FuncControlsPanel.Children.Add(lbl_is_on);

            var row = new Grid { Margin = new Thickness(0, 4, 0, 4) };
            row.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
            row.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            row.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });

            Label lbl_brightness = new Label
            {
                Content = "Новая яркость:",
                FontSize = 14,
                VerticalAlignment = VerticalAlignment.Center,
                Margin = new Thickness(0, 0, 8, 0)
            };
            Grid.SetColumn(lbl_brightness, 0);
            row.Children.Add(lbl_brightness);

            sld_brightness = new Slider
            {
                Minimum = 0,
                Maximum = 100,
                Value = newNightlight.Brightness,
                TickFrequency = 1,
                IsSnapToTickEnabled = true,
                VerticalAlignment = VerticalAlignment.Center
            };
            Grid.SetColumn(sld_brightness, 1);
            row.Children.Add(sld_brightness);

            lbl_actuall_brightness = new Label
            {
                Content = $"{newNightlight.Brightness}%",
                FontSize = 14,
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Right,
                MinWidth = 48,
                Margin = new Thickness(8, 0, 0, 0)
            };
            Grid.SetColumn(lbl_actuall_brightness, 2);
            row.Children.Add(lbl_actuall_brightness);

            sld_brightness.ValueChanged += sld_brightness_set;

            FuncControlsPanel.Children.Add(row);
        }

        /// <summary>
        /// Создание элементов управления для функции выключения
        /// </summary>
        private void ControlsForOff()
        {
            Label lbl_is_on = new Label
            {
                Content = "Свет должен быть включен",
                FontSize = 14
            };
            FuncControlsPanel.Children.Add(lbl_is_on);
        }

        /// <summary>
        /// ОБРАБОТЧИК КНОПКИ "ВЫПОЛНИТЬ"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Run_Click(object sender, RoutedEventArgs e)
        {
            switch (OperationsListBox.SelectedIndex)
            {
                case 0:
                    Run_On();
                    break;
                case 1:
                    Run_Set();
                    break;
                case 2:
                    Run_Off();
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Обработчик для функции включения
        /// </summary>
        private void Run_On()
        {
            //Проверка Pre
            bool havePower = chk_power.IsChecked == true;
            int oldBrightness = newNightlight.Brightness;
            if (!havePower)
            {
                PreDontWork("нет питания");
                PostDontWork("свет не был вкюлчён");
                return;
            }
            else if (newNightlight.isOn)
            {
                PreDontWork("свет уже включен");
                PostDontWork("свет не был вкюлчён");
                return;
            }
            else PreWork();

            //Настройка значений
            try
            {
                controller.TurnOn(newNightlight);
            }
            catch (Exception ex)
            {
                PreDontWork(ex.Message);
                PostDontWork(ex.Message);
                return;
            }

            //Проверка Post
            if (!newNightlight.isOn) PostDontWork("свет не был вкюлчён");
            else PostWork();
        }

        /// <summary>
        /// Обработчик для функции изменения яркости
        /// </summary>
        private void Run_Set()
        {
            //Проверка Pre
            int newBrightness = (int)sld_brightness.Value;
            int oldBrightness = newNightlight.Brightness;
            if (!newNightlight.isOn)
            {
                PreDontWork("свет выключен");
                PostDontWork("яркость не была настроена");
                return;
            }
            else if (newBrightness == oldBrightness)
            {
                PreDontWork("новая яркость равна старой");
                PostDontWork("яркость не была настроена");
                return;
            }
            else PreWork();

            //Настройка значений
            try
            { controller.SetBrightness(newNightlight, newBrightness); }
            catch (Exception ex)
            {
                PreDontWork(ex.Message);
                PostDontWork(ex.Message);
                return;
            }

            //Проверка Post
            if (newBrightness != newNightlight.Brightness) PostDontWork("яркость не была настроена");
            else PostWork();
        }

        /// <summary>
        /// Обработчик для функции включения
        /// </summary>
        private void Run_Off()
        {
            //Проверка Pre
            if (!newNightlight.isOn)
            {
                PreDontWork("свет уже выключен");
                PostDontWork("свет не был выкюлчён");
                return;
            }
            else PreWork();

            //Настройка значений
            try
            { controller.TurnOff(newNightlight); }
            catch (Exception ex)
            {
                PreDontWork(ex.Message);
                PostDontWork(ex.Message);
                return;
            }

            //Проверка Post
            if (newNightlight.isOn) PostDontWork("свет не был выкюлчён");
            else PostWork();
        }

        /// <summary>
        /// При невыполненном Pre
        /// </summary>
        /// <param name="msg">Сообщение об ошибке</param>
        private void PreDontWork(string msg)
        {
            labelPreconditionColor.Background = Brushes.Red;
            labelPreconditionValue.Content = $"Ошибка: {msg}.";
        }

        /// <summary>
        /// При невыполненном Post
        /// </summary>
        /// <param name="msg"></param>
        private void PostDontWork(string msg)
        {
            labelPostconditionColor.Background = Brushes.Red;
            labelPostconditionValue.Content = $"Ошибка: {msg}.";
            labelResult.Content = "Операция была отменена.";
        }

        /// <summary>
        /// При выполненном Pre
        /// </summary>
        private void PreWork()
        {
            labelPreconditionColor.Background = Brushes.Green;
            labelPreconditionValue.Content = $"Выполнено.";
        }

        /// <summary>
        /// При выполненном Post
        /// </summary>
        private void PostWork()
        {
            labelPostconditionColor.Background = Brushes.Green;
            labelPostconditionValue.Content = $"Выполнено.";
            labelResult.Content = "Операция успешно завершена!";
        }

        // ОБРАБОТЧИК КНОПКИ "КОНТРАКТ"
        private void Contract_Click(object sender, RoutedEventArgs e)
        {
            if (OperationsListBox.SelectedItem != null)
                MessageBox.Show($"{OperationsListBox.SelectedItem.ToString()}\n\n\n" +
                    $"pre:\n{pre_txt}\n\npost:\n{post_txt}",
                            $"Контракт операции {OperationsListBox.SelectedItem.ToString()}",
                            MessageBoxButton.OK,
                            MessageBoxImage.Information);
            else MessageBox.Show($"Пожалуйста, выберете сначала операцию",
                            $"Операция не выбрана",
                            MessageBoxButton.OK,
                            MessageBoxImage.Warning);
        }
    }
}