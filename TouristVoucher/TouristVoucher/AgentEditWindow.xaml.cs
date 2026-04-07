using System;
using System.Linq;
using System.Windows;

namespace TouristVoucher
{
    public partial class AgentEditWindow : Window
    {
        public TourAgent Agent { get; set; }
        private bool isEditMode;

        public AgentEditWindow(TourAgent agent)
        {
            InitializeComponent();

            if (agent != null)
            {
                // Режим редактирования
                isEditMode = true;
                Agent = agent;
                LoadAgentData();
                this.Title = "Редактирование агента";
            }
            else
            {
                // Режим добавления
                isEditMode = false;
                Agent = new TourAgent();
                this.Title = "Добавление агента";
            }
        }

        private void LoadAgentData()
        {
            txtFullName.Text = Agent.FullName;
            txtInsurance.Text = Agent.InsuranceNumber;
            txtINN.Text = Agent.INN;
            txtPassport.Text = Agent.PassportNumber;
            dpVisaExpiry.SelectedDate = Agent.VisaExpiry;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Проверка ФИО (не пустое, не менее 5 символов, только буквы и пробелы)
                if (string.IsNullOrWhiteSpace(txtFullName.Text))
                {
                    MessageBox.Show("Введите ФИО", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                if (txtFullName.Text.Length < 5)
                {
                    MessageBox.Show("ФИО должно содержать минимум 5 символов", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                if (!txtFullName.Text.All(c => char.IsLetter(c) || c == ' ' || c == '-'))
                {
                    MessageBox.Show("ФИО должно содержать только буквы, пробелы и дефисы", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                // Проверка страхового свидетельства
                if (string.IsNullOrWhiteSpace(txtInsurance.Text))
                {
                    MessageBox.Show("Введите номер страхового свидетельства", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                if (txtInsurance.Text.Length < 5)
                {
                    MessageBox.Show("Номер страхового свидетельства должен содержать минимум 5 символов", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                // Проверка ИНН
                if (string.IsNullOrWhiteSpace(txtINN.Text))
                {
                    MessageBox.Show("Введите ИНН", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                if (txtINN.Text.Length != 10 && txtINN.Text.Length != 12)
                {
                    MessageBox.Show("ИНН должен содержать 10 или 12 цифр", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                if (!txtINN.Text.All(char.IsDigit))
                {
                    MessageBox.Show("ИНН должен содержать только цифры", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                // Проверка паспорта
                if (string.IsNullOrWhiteSpace(txtPassport.Text))
                {
                    MessageBox.Show("Введите номер паспорта", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                if (txtPassport.Text.Length < 8)
                {
                    MessageBox.Show("Номер паспорта должен содержать минимум 8 символов", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                // Проверка срока визы (просто проверяем что выбран, без проверки на прошлое)
                if (dpVisaExpiry.SelectedDate == null)
                {
                    MessageBox.Show("Выберите срок визы", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                // Сохраняем данные
                Agent.FullName = txtFullName.Text;
                Agent.InsuranceNumber = txtInsurance.Text;
                Agent.INN = txtINN.Text;
                Agent.PassportNumber = txtPassport.Text;
                Agent.VisaExpiry = dpVisaExpiry.SelectedDate.Value;

                DialogResult = true;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}