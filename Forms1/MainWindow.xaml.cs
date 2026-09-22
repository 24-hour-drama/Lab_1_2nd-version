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

namespace Forms1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            OperationsListBox.Items.Add("Операция 1: Включение");
            OperationsListBox.Items.Add("Операция 2: Настройка яркости");
            OperationsListBox.Items.Add("Операция 3: Выключение");
        }

        // ОСНОВА ДЛЯ ВЫБОРА ОПЕРАЦИИ ИЗ СПИСКА
        private void OperationsListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            /*
            // Проверяем, что пользователь действительно что-то выбрал
            if (OperationsListBox.SelectedItem != null)
            {
                // Получаем выбранный элемент
                var selectedItem = OperationsListBox.SelectedItem;

                // Если в ListBox хранятся просто строки (как мы добавили в конструкторе),
                // то приводим его к строке и выводим в label1.
                // Если у вас там сложные объекты, нужно будет обращаться к их свойствам.
                string operationName = selectedItem.ToString();

                // Выводим название операции в заголовок
                label1.Content = operationName;

                // Сбрасываем старые результаты при выборе новой операции
                labelPreconditionValue.Content = "label4";
                labelPostconditionValue.Content = "label4";
                labelResult.Content = "label5";

                // Сбрасываем цвета квадратиков в серый
                labelPreconditionColor.Background = Brushes.Gray;
                labelPostconditionColor.Background = Brushes.Gray;
            }
            */
        }

        // ОБРАБОТЧИК КНОПКИ "ВЫПОЛНИТЬ"
        private void Run_Click(object sender, RoutedEventArgs e)
        {
            /*
            // + логика проверки предусловия
            bool isPreconditionMet = true;
            bool isPostconditionMet = true;

            if (isPreconditionMet)
            {
                // Если условие выполнено - красим квадратик в ЗЕЛЕНЫЙ
                labelPreconditionColor.Background = Brushes.Green;
                labelPreconditionValue.Content = "Выполнено";
            }
            else
            {
                // Если не выполнено - красим в КРАСНЫЙ
                labelPreconditionColor.Background = Brushes.Red;
                labelPreconditionValue.Content = "Ошибка";
            }

            if (isPostconditionMet)
            {
                // Если условие выполнено - красим квадратик в ЗЕЛЕНЫЙ
                labelPostconditionColor.Background = Brushes.Green;
                labelPostconditionValue.Content = "Выполнено";
            }
            else
            {
                // Если не выполнено - красим в КРАСНЫЙ
                labelPostconditionColor.Background = Brushes.Red;
                labelPostconditionValue.Content = "Не выполнено";
            }

            // Выводим результат
            labelResult.Content = "Операция успешно завершена!";
            */
        }

        // ОБРАБОТЧИК КНОПКИ "КОНТРАКТ"
        private void Contract_Click(object sender, RoutedEventArgs e)
        {
            /*
            MessageBox.Show("Здесь отображается информация о контракте выбранной операции.",
                            "Контракт операции",
                            MessageBoxButton.OK,
                            MessageBoxImage.Information);
            */
        }
    }
}