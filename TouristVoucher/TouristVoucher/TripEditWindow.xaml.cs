using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace TouristVoucher
{
    public partial class TripEditWindow : Window
    {
        public TripSchedule Trip { get; set; }
        private List<TourAgent> agents;
        private List<TourGroup> groups;

        public TripEditWindow(TripSchedule trip, List<TourAgent> agents, List<TourGroup> groups)
        {
            InitializeComponent();
            this.agents = agents;
            this.groups = groups;

            // Заполняем выпадающие списки
            cboAgent.ItemsSource = agents;
            cboGroup.ItemsSource = groups;

            if (trip != null)
            {
                // Режим редактирования
                Trip = trip;
                LoadTripData();
                this.Title = "Редактирование поездки";
            }
            else
            {
                // Режим добавления
                Trip = new TripSchedule();
                this.Title = "Добавление поездки";
            }
        }

        private void LoadTripData()
        {
            // Выбираем агента
            for (int i = 0; i < agents.Count; i++)
            {
                if (agents[i].FullName == Trip.AgentName)
                {
                    cboAgent.SelectedIndex = i;
                    break;
                }
            }

            // Выбираем группу
            for (int i = 0; i < groups.Count; i++)
            {
                if (groups[i].GroupNumber == Trip.GroupNumber)
                {
                    cboGroup.SelectedIndex = i;
                    break;
                }
            }

            // Заполняем остальные поля
            dpTripDate.SelectedDate = Trip.TripDate;
            txtDuration.Text = Trip.TripDuration.ToString();
            txtContract.Text = Trip.ContractNumber;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Проверка выбора турагента
                if (cboAgent.SelectedItem == null)
                {
                    MessageBox.Show("Выберите турагента", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                // Проверка выбора группы
                if (cboGroup.SelectedItem == null)
                {
                    MessageBox.Show("Выберите группу", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                // Проверка даты поездки
                if (dpTripDate.SelectedDate == null)
                {
                    MessageBox.Show("Выберите дату поездки", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                // Проверка длительности
                if (string.IsNullOrWhiteSpace(txtDuration.Text))
                {
                    MessageBox.Show("Введите длительность поездки", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                int duration;
                if (!int.TryParse(txtDuration.Text, out duration))
                {
                    MessageBox.Show("Введите корректную длительность (целое число)", "Ошибка",
                        MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                if (duration <= 0)
                {
                    MessageBox.Show("Длительность поездки должна быть больше 0", "Ошибка",
                        MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                if (duration > 30)
                {
                    MessageBox.Show("Длительность поездки не может превышать 30 дней", "Ошибка",
                        MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                // Проверка номера договора (не пустой, минимум 5 символов, буквы/цифры/дефис)
                if (string.IsNullOrWhiteSpace(txtContract.Text))
                {
                    MessageBox.Show("Введите номер договора", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                if (txtContract.Text.Length < 5)
                {
                    MessageBox.Show("Номер договора должен содержать минимум 5 символов", "Ошибка",
                        MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                if (!txtContract.Text.All(c => char.IsLetterOrDigit(c) || c == '-' || c == '/'))
                {
                    MessageBox.Show("Номер договора должен содержать только буквы, цифры, дефисы и слеши", "Ошибка",
                        MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                var selectedAgent = cboAgent.SelectedItem as TourAgent;
                var selectedGroup = cboGroup.SelectedItem as TourGroup;

                Trip.AgentName = selectedAgent.FullName;
                Trip.GroupNumber = selectedGroup.GroupNumber;
                Trip.TripDate = dpTripDate.SelectedDate.Value;
                Trip.TripDuration = duration;
                Trip.ContractNumber = txtContract.Text;

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