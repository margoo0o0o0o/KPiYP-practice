using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace TouristVoucher
{
    public partial class AgentsWindow : Window
    {
        private List<TourAgent> agents;
        private MainWindow mainWindow;

        public AgentsWindow(List<TourAgent> agents, MainWindow mainWindow)
        {
            InitializeComponent();
            this.agents = agents;
            this.mainWindow = mainWindow;
            dgAgents.ItemsSource = agents;
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            var editWindow = new AgentEditWindow(null);
            editWindow.Owner = this;
            if (editWindow.ShowDialog() == true)
            {
                agents.Add(editWindow.Agent);
                dgAgents.Items.Refresh();
                mainWindow.UpdateAllData();
                mainWindow.SaveAllData();
            }
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (dgAgents.SelectedItem is TourAgent selectedAgent)
            {
                var editWindow = new AgentEditWindow(selectedAgent);
                editWindow.Owner = this;
                if (editWindow.ShowDialog() == true)
                {
                    selectedAgent.FullName = editWindow.Agent.FullName;
                    selectedAgent.InsuranceNumber = editWindow.Agent.InsuranceNumber;
                    selectedAgent.INN = editWindow.Agent.INN;
                    selectedAgent.PassportNumber = editWindow.Agent.PassportNumber;
                    selectedAgent.VisaExpiry = editWindow.Agent.VisaExpiry;

                    dgAgents.Items.Refresh();
                    mainWindow.UpdateAllData();
                    mainWindow.SaveAllData();
                }
            }
            else
            {
                MessageBox.Show("Выберите агента для редактирования", "Внимание",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (dgAgents.SelectedItem is TourAgent selectedAgent)
            {
                if (MessageBox.Show($"Удалить агента {selectedAgent.FullName}?\n\nВнимание! Если у этого агента есть поездки, они будут удалены!",
                    "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    agents.Remove(selectedAgent);
                    dgAgents.Items.Refresh();
                    mainWindow.UpdateAllData();
                    mainWindow.SaveAllData();
                }
            }
            else
            {
                MessageBox.Show("Выберите агента для удаления", "Внимание",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}