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
using System.Windows.Shapes;
using TestDemo.Data;
using TestDemo.Data.Entity;

namespace TestDemo
{
    public partial class PassTest : Window
    {
        private DataContext context;
        private Guid id;
        private int number;
        private int totalQuestions;
        private int answeredQuestions;
        public PassTest(Test test)
        {
            InitializeComponent();
            context = new DataContext();
            id = test.Id;
            number = test.QuestionCount;
            TestName.Text = $"Назва тесту: {test.NameTest}     ID тесту: {test.Id}     Кількість питань: {test.QuestionCount}";
            totalQuestions = test.QuestionCount;
            answeredQuestions = 0;
            AnswerProgressBar.Maximum = totalQuestions;
            DisplayQuestionsAndAnswers();
            UpdateStatusBar();
        }
        private void UpdateStatusBar()
        {
            // Оновлення прогресбару та текстового статусу
            var progressPercentage = (double)answeredQuestions / totalQuestions * 100;
            AnswerProgressBar.Value = progressPercentage;
            AnswerStatusText.Text = $"Відповіді: {answeredQuestions}/{totalQuestions}";
        }
        private void MoveToNextQuestion()
        {
            // Перевірити, чи є ще питання
            if (QuestionsListBox.SelectedIndex < totalQuestions - 1)
            {
                // Перейти до наступного питання
                QuestionsListBox.SelectedIndex++;
            }
            else
            {
                // Якщо це останнє питання, можна виконати додаткові дії, наприклад, завершення тесту
                MessageBox.Show("Це останнє питання. Тест завершено!", "Результат", MessageBoxButton.OK, MessageBoxImage.Information);
            }

            // Оновлення статусбару при переході до наступного питання
            UpdateStatusBar();
        }
        private void DisplayQuestionsAndAnswers()
        {
            // Отримати список питань для тесту за його Id
            var questions = context.Questionts.Where(q => q.IdTest == id).ToList();

            foreach (var question in questions)
            {
                // Вивести питання
                QuestionsListBox.Items.Add(question.QuestionTest);
            }

            // Додати обробник події для вибору питання в ListBox
            QuestionsListBox.SelectionChanged += QuestionsListBox_SelectionChanged;
        }

        private void QuestionsListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Очистити попередні відповіді
            AnswersTextBox.Text = string.Empty;

            // Викликати метод для деактивації чекбоксів
            DeactivateCheckboxes();

            // Отримати вибране питання
            var selectedQuestionIndex = QuestionsListBox.SelectedIndex;

            if (selectedQuestionIndex >= 0)
            {
                var selectedQuestion = context.Questionts
                    .Where(q => q.IdTest == id)
                    .OrderBy(q => q.NumberQuestion)
                    .Skip(selectedQuestionIndex)
                    .FirstOrDefault();

                // Вивести відповіді для вибраного питання
                if (selectedQuestion != null)
                {
                    var answers = new List<string>
            {
                $"1. {selectedQuestion.Answer1} ({(selectedQuestion.AnswerBool1 ? "Правильно" : "Неправильно")})",
                $"2. {selectedQuestion.Answer2} ({(selectedQuestion.AnswerBool2 ? "Правильно" : "Неправильно")})"
            };

                    if (!string.IsNullOrWhiteSpace(selectedQuestion.Answer3))
                    {
                        answers.Add($"3. {selectedQuestion.Answer3} ({(selectedQuestion.AnswerBool3 ? "Правильно" : "Неправильно")})");
                        Answer3CheckBox.IsEnabled = true;
                    }
                    else
                    {
                        Answer3CheckBox.IsEnabled = false;
                    }

                    if (!string.IsNullOrWhiteSpace(selectedQuestion.Answer4))
                    {
                        answers.Add($"4. {selectedQuestion.Answer4} ({(selectedQuestion.AnswerBool4 ? "Правильно" : "Неправильно")})");
                        Answer4CheckBox.IsEnabled = true;
                    }
                    else
                    {
                        Answer4CheckBox.IsEnabled = false;
                    }

                    if (!string.IsNullOrWhiteSpace(selectedQuestion.Answer5))
                    {
                        answers.Add($"5. {selectedQuestion.Answer5} ({(selectedQuestion.AnswerBool5 ? "Правильно" : "Неправильно")})");
                        Answer5CheckBox.IsEnabled = true;
                    }
                    else
                    {
                        Answer5CheckBox.IsEnabled = false;
                    }

                    var answersText = string.Join("\n", answers);
                    AnswersTextBox.Text = $"{selectedQuestion.NumberQuestion}. {selectedQuestion.QuestionTest}\n{answersText}\n\n";
                }
            }
        }

        // Метод для деактивації чекбоксів
        private void DeactivateCheckboxes()
        {
            Answer1CheckBox.IsChecked = false;
            Answer2CheckBox.IsChecked = false;
            Answer3CheckBox.IsEnabled = false;
            Answer3CheckBox.IsChecked = false;
            Answer4CheckBox.IsEnabled = false;
            Answer4CheckBox.IsChecked = false;
            Answer5CheckBox.IsEnabled = false;
            Answer5CheckBox.IsChecked = false;
        }


        private void SubmitAnswersButton_Click(object sender, RoutedEventArgs e)
        {
            // Отримати вибране питання
            var selectedQuestionIndex = QuestionsListBox.SelectedIndex;

            if (selectedQuestionIndex >= 0)
            {
                var selectedQuestion = context.Questionts
                    .Where(q => q.IdTest == id)
                    .OrderBy(q => q.NumberQuestion)
                    .Skip(selectedQuestionIndex)
                    .FirstOrDefault();

                // Отримати відповіді користувача
                var userAnswer1 = Answer1CheckBox.IsChecked ?? false;
                var userAnswer2 = Answer2CheckBox.IsChecked ?? false;
                var userAnswer3 = Answer3CheckBox.IsChecked ?? false;
                var userAnswer4 = Answer4CheckBox.IsChecked ?? false;
                var userAnswer5 = Answer5CheckBox.IsChecked ?? false;

                // Перевірити, чи коректні відповіді користувача
                var correctAnswers = new List<bool>
                {
                  userAnswer1 == selectedQuestion?.AnswerBool1,
                  userAnswer2 == selectedQuestion?.AnswerBool2,
                  userAnswer3 == selectedQuestion?.AnswerBool3,
                  userAnswer4 == selectedQuestion?.AnswerBool4,
                  userAnswer5 == selectedQuestion?.AnswerBool5
                 };

                // Визначити, чи всі відповіді коректні
                var allCorrect = correctAnswers.All(answer => answer);

                // Вивести повідомлення
                if (allCorrect)
                {
                    MessageBox.Show("Правильно! Відповіді вірні.", "Результат", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("Неправильно. Будь ласка, перевірте відповіді.", "Результат", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                // Оновлення статусбару після відповіді
                answeredQuestions++;
                UpdateStatusBar();

                // Деактивація чекбоксів для даного питання
                DeactivateCheckboxes();

                // Перехід до наступного питання
                MoveToNextQuestion();
            }
        }

    }
}

