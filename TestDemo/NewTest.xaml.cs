using System;
using System.Collections.Generic;
using System.Globalization;
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
using System.Windows.Shapes;
using TestDemo.Data;
using TestDemo.Data.Entity;

namespace TestDemo
{

    public partial class NewTest : Window
    {
        private List<DataGrid> dataGrids = new List<DataGrid>();
        
        private DataContext context;

        private Guid id;
        private int number;
        public NewTest(Test test)
        {
            InitializeComponent();
            context = new DataContext();

            TestName.Text = test.NameTest;
            id = test.Id;
            RefreshDataGrid();
            
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(((RadioButton)sender).Content.ToString(), out int answerCount))
            {
                AnswerCheckBox3.IsEnabled = answerCount >= 3;
                AnswerCheckBox3.IsChecked = answerCount >= 3;
                AnswerTextBox3.IsEnabled = answerCount >= 3;

                AnswerCheckBox4.IsEnabled = answerCount >= 4;
                AnswerCheckBox4.IsChecked = answerCount >= 4;
                AnswerTextBox4.IsEnabled = answerCount >= 4;

                AnswerCheckBox5.IsEnabled = answerCount >= 5;
                AnswerCheckBox5.IsChecked = answerCount >= 5;
                AnswerTextBox5.IsEnabled = answerCount >= 5;
            }
        }


        private void SaveTest_Click(object sender, RoutedEventArgs e)
        {
            if (AnswersPanel != null)
            {
               
                using (var context = new DataContext())
                {
                   

                    var newQuestion = new Question
                    {
                        QuestionId = Guid.NewGuid(),
                        QuestionTest = TestNameTextBox.Text,
                    };

                    if (!string.IsNullOrWhiteSpace(AnswerTextBox1.Text))
                    {
                        newQuestion.Answer1 = AnswerTextBox1.Text;
                        newQuestion.AnswerBool1 = AnswerCheckBox1.IsChecked ?? false;
                    }

                    if (!string.IsNullOrWhiteSpace(AnswerTextBox2.Text))
                    {
                        newQuestion.Answer2 = AnswerTextBox2.Text;
                        newQuestion.AnswerBool2 = AnswerCheckBox2.IsChecked ?? false;
                    }

                    if (AnswerCheckBox3.IsEnabled && !string.IsNullOrWhiteSpace(AnswerTextBox3.Text))
                    {
                        newQuestion.Answer3 = AnswerTextBox3.Text;
                        newQuestion.AnswerBool3 = AnswerCheckBox3.IsChecked ?? false;
                    }

                    if (AnswerCheckBox4.IsEnabled && !string.IsNullOrWhiteSpace(AnswerTextBox4.Text))
                    {
                        newQuestion.Answer4 = AnswerTextBox4.Text;
                        newQuestion.AnswerBool4 = AnswerCheckBox4.IsChecked ?? false;
                    }

                    if (AnswerCheckBox5.IsEnabled && !string.IsNullOrWhiteSpace(AnswerTextBox5.Text))
                    {
                        newQuestion.Answer5 = AnswerTextBox5.Text;
                        newQuestion.AnswerBool5 = AnswerCheckBox5.IsChecked ?? false;
                    }
                    newQuestion.IdTest = id;
                    number++;
                    newQuestion.NumberQuestion = number;
                    context.Questionts.Add(newQuestion);
                    context.SaveChanges();
                    // Оновити QuestionCount в класі Test
                    var testToUpdate = context.Tests.FirstOrDefault(t => t.Id == id);

                    if (testToUpdate != null)
                    {
                        testToUpdate.QuestionCount++;
                        context.SaveChanges();
                    }
                    RefreshDataGrid();

                }
            }
        }

        private void RefreshDataGrid()
        {
            using (var readContext = new DataContext())
            {
                // Отримуємо унікальні IdTest з бази даних
                var uniqueIdTests = readContext.Questionts.Select(q => q.IdTest).Distinct();

                // Очищуємо попередні DataGrid
                MainStackPanel.Children.Clear();

                // Створюємо та оновлюємо DataGrid для кожного унікального IdTest
                foreach (var idTest in uniqueIdTests)
                {
                    using (var writeContext = new DataContext())
                    {
                        // Отримуємо ім'я тесту
                        var testName = writeContext.Tests.FirstOrDefault(t => t.Id == idTest)?.NameTest;

                        // Створити Expander
                        var expander = new Expander
                        {
                            Header = $"Назва тесту: {testName}",
                            IsExpanded = false // Залежно від вашого вибору
                        };

                        // Створюємо DataGrid
                        var dataGrid = new DataGrid
                        {
                            AutoGenerateColumns = true,
                            ItemsSource = writeContext.Questionts.Where(q => q.IdTest == idTest).ToList()
                        };
                        // Створюємо стовпець для кнопок редагування і видалення
                        var editColumn = new DataGridTemplateColumn();
                        var deleteColumn = new DataGridTemplateColumn();

                        // Додазмо кнопку редагування
                        var editButtonFactory = new FrameworkElementFactory(typeof(Button));
                        editButtonFactory.SetValue(ContentProperty, "Редагувати");
                        editButtonFactory.AddHandler(Button.ClickEvent, new RoutedEventHandler(EditButton_Click));
                        editButtonFactory.SetValue(Button.BackgroundProperty, Brushes.LightSkyBlue);
                        editColumn.CellTemplate = new DataTemplate { VisualTree = editButtonFactory };

                        // Додазмо кнопку видалення
                        var deleteButtonFactory = new FrameworkElementFactory(typeof(Button));
                        deleteButtonFactory.SetValue(ContentProperty, "Видалити");
                        deleteButtonFactory.AddHandler(Button.ClickEvent, new RoutedEventHandler(DeleteButton_Click));
                        deleteButtonFactory.SetValue(Button.BackgroundProperty, Brushes.LightSkyBlue);
                        deleteColumn.CellTemplate = new DataTemplate { VisualTree = deleteButtonFactory };

                        // Додаємо стовпці до DataGrid
                        dataGrid.Columns.Add(editColumn);
                        dataGrid.Columns.Add(deleteColumn);
                        // Додати DataGrid до Expander
                        expander.Content = dataGrid;

                        // Додаємо Expander до StackPanel
                        MainStackPanel.Children.Add(expander);
                    }
                }
            }
        }
        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            // Обробник події для кнопки редагування
            // Отримайте дані рядка, який потрібно редагувати, і викличте необхідні дії
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.DataContext is Question question)
            {
                // Викликаємо метод видалення, передаючи дані питання
                DeleteQuestion(question);
            }
            
        }
        private void DeleteQuestion(Question question)
        {
            // Код для видалення питання

            using (var dbContext = new DataContext())
            {
                // Знаходимо питання в базі даних за його ідентифікатором 
                var questionToDelete = dbContext.Questionts.FirstOrDefault(q => q.QuestionId == question.QuestionId);

                if (questionToDelete != null)
                {
                    // Видаляємо питання з бази даних
                    dbContext.Questionts.Remove(questionToDelete);
                    dbContext.SaveChanges();

                    // Оновлюємо DataGrid після видалення
                    RefreshDataGrid();
                }
                else
                {
                    // Повідомлення, якщо не вдалося знайти питання для видалення
                    MessageBox.Show("Питання не знайдено для видалення.");
                }
            }
        }
    }
}
