using System;
using System.Collections.Generic;
using System.Windows;

namespace TouristVoucher
{
    public partial class TripsWindow : Window
    {
        private List<TripSchedule> schedule;
        private List<TourAgent> agents;
        private List<TourGroup> groups;
        private MainWindow mainWindow;

        public TripsWindow(List<TripSchedule> schedule, List<TourAgent> agents, List<TourGroup> groups, MainWindow mainWindow)
        {
            InitializeComponent();
            this.schedule = schedule;
            this.agents = agents;
            this.groups = groups;
            this.mainWindow = mainWindow;
            dgTrips.ItemsSource = schedule;
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            var editWindow = new TripEditWindow(null, agents, groups);
            editWindow.Owner = this;
            if (editWindow.ShowDialog() == true)
            {
                schedule.Add(editWindow.Trip);
                dgTrips.Items.Refresh();
                mainWindow.UpdateAllData();
                mainWindow.SaveAllData();
            }
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (dgTrips.SelectedItem is TripSchedule selectedTrip)
            {
                var editWindow = new TripEditWindow(selectedTrip, agents, groups);
                editWindow.Owner = this;
                if (editWindow.ShowDialog() == true)
                {
                    selectedTrip.AgentName = editWindow.Trip.AgentName;
                    selectedTrip.GroupNumber = editWindow.Trip.GroupNumber;
                    selectedTrip.TripDate = editWindow.Trip.TripDate;
                    selectedTrip.ContractNumber = editWindow.Trip.ContractNumber;
                    selectedTrip.TripDuration = editWindow.Trip.TripDuration;

                    dgTrips.Items.Refresh();
                    mainWindow.UpdateAllData();
                    mainWindow.SaveAllData();
                }
            }
            else
            {
                MessageBox.Show("Выберите поездку для редактирования", "Внимание",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (dgTrips.SelectedItem is TripSchedule selectedTrip)
            {
                if (MessageBox.Show($"Удалить поездку?\n\nАгент: {selectedTrip.AgentName}\nГруппа: {selectedTrip.GroupNumber}\nДата: {selectedTrip.TripDate:dd.MM.yyyy}",
                    "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    schedule.Remove(selectedTrip);
                    dgTrips.Items.Refresh();
                    mainWindow.UpdateAllData();
                    mainWindow.SaveAllData();
                }
            }
            else
            {
                MessageBox.Show("Выберите поездку для удаления", "Внимание",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}