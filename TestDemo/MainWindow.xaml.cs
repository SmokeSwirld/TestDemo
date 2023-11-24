using System.Collections.ObjectModel;
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
using TestDemo.Data;
using TestDemo.Data.Entity;

namespace TestDemo
{
    public partial class MainWindow : Window
    {
        private DataContext context;
        public MainWindow()
        {
            InitializeComponent();
            context = new DataContext();
            UpdateTests();

        }

        private void NewTest_Click(object sender, RoutedEventArgs e)
        {
            var newTest = new Test
            {
                Id = Guid.NewGuid(),
                NameTest = TestName.Text,
                CreatedAt = DateTime.Now,
                QuestionCount = 0
            };

            context.Tests.Add(newTest);
            context.SaveChanges();

            var secondWindow = new NewTest(newTest);
            secondWindow.Show();
        }
        private void Update_Click(object sender, RoutedEventArgs e)// видалення пустих тестів
        {
            var testsWithoutQuestions = context.Tests
               .Where(test => !context.Questionts.Any(q => q.IdTest == test.Id))
               .ToList();

            foreach (var testWithoutQuestions in testsWithoutQuestions)
            {
                // Видалити тесту
                context.Tests.Remove(testWithoutQuestions);
            }

            context.SaveChanges();
            UpdateTests();
        }

        private void UpdateTests()
        {
           
            // Оновлюємо DataGrid
            var tests = context.Tests.ToList();
            dataGrid.ItemsSource = tests;

            // Додаємо стовпець кнопки
            var takeTestColumn = new DataGridTemplateColumn();
            takeTestColumn.Header = "Дії";

            var buttonFactory = new FrameworkElementFactory(typeof(Button));
            buttonFactory.SetValue(ContentProperty, "Пройти тест");
            buttonFactory.AddHandler(Button.ClickEvent, new RoutedEventHandler(TakeTestButton_Click));
            takeTestColumn.CellTemplate = new DataTemplate { VisualTree = buttonFactory };
            dataGrid.Columns.Add(takeTestColumn);

            // Прив'язуємо дані до стовпця кнопки
            foreach (var test in tests)
            {
                var cellContent = new FrameworkElementFactory(typeof(Button));
                cellContent.SetValue(ContentProperty, "   Пройти тест   ");
                cellContent.SetValue(Button.BackgroundProperty, Brushes.LightSkyBlue);
                cellContent.AddHandler(Button.ClickEvent, new RoutedEventHandler(TakeTestButton_Click));
                takeTestColumn.CellTemplate.VisualTree = cellContent;
            }
        }

        private void TakeTestButton_Click(object sender, RoutedEventArgs e)
        {

            var button = sender as Button;
            var test = button?.DataContext as Test;

            if (test != null)
            {
                StartTest(test);
            }
        }

        private void StartTest(Test test)
        {

            MessageBox.Show($"Починаємо тест '{test.NameTest}'!");

            // Створити новий екземпляр форми PassTest
            PassTest passTestForm = new PassTest(test);

            // Відобразити форму PassTest
            passTestForm.ShowDialog();


        }


    }
}