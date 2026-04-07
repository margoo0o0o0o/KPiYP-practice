using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace TouristVoucher
{
    public partial class GroupsWindow : Window
    {
        private List<TourGroup> groups;
        private MainWindow mainWindow;

        public GroupsWindow(List<TourGroup> groups, MainWindow mainWindow)
        {
            InitializeComponent();
            this.groups = groups;
            this.mainWindow = mainWindow;
            dgGroups.ItemsSource = groups;
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            var editWindow = new GroupEditWindow(null);
            editWindow.Owner = this;
            if (editWindow.ShowDialog() == true)
            {
                groups.Add(editWindow.Group);
                dgGroups.Items.Refresh();
                mainWindow.UpdateAllData();
                mainWindow.SaveAllData();
            }
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (dgGroups.SelectedItem is TourGroup selectedGroup)
            {
                var editWindow = new GroupEditWindow(selectedGroup);
                editWindow.Owner = this;
                if (editWindow.ShowDialog() == true)
                {
                    selectedGroup.GroupNumber = editWindow.Group.GroupNumber;
                    selectedGroup.PeopleCount = editWindow.Group.PeopleCount;
                    selectedGroup.AgeCategory = editWindow.Group.AgeCategory;

                    dgGroups.Items.Refresh();
                    mainWindow.UpdateAllData();
                    mainWindow.SaveAllData();
                }
            }
            else
            {
                MessageBox.Show("Выберите группу для редактирования", "Внимание",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (dgGroups.SelectedItem is TourGroup selectedGroup)
            {
                if (MessageBox.Show($"Удалить группу {selectedGroup.GroupNumber}?\n\nВнимание! Если у этой группы есть поездки, они будут удалены!",
                    "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    groups.Remove(selectedGroup);
                    dgGroups.Items.Refresh();
                    mainWindow.UpdateAllData();
                    mainWindow.SaveAllData();
                }
            }
            else
            {
                MessageBox.Show("Выберите группу для удаления", "Внимание",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}