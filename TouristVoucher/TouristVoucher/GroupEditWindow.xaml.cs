using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace TouristVoucher
{
    public partial class GroupEditWindow : Window
    {
        public TourGroup Group { get; set; }

        public GroupEditWindow(TourGroup group)
        {
            InitializeComponent();

            if (group != null)
            {
                Group = group;
                LoadGroupData();
                this.Title = "Редактирование группы";
            }
            else
            {
                Group = new TourGroup();
                this.Title = "Добавление группы";
            }
        }

        private void LoadGroupData()
        {
            txtGroupNumber.Text = Group.GroupNumber;
            txtPeopleCount.Text = Group.PeopleCount.ToString();

            // Устанавливаем выбранную категорию в ComboBox
            for (int i = 0; i < cboAgeCategory.Items.Count; i++)
            {
                var item = cboAgeCategory.Items[i] as ComboBoxItem;
                if (item != null && item.Content.ToString() == Group.AgeCategory)
                {
                    cboAgeCategory.SelectedIndex = i;
                    break;
                }
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Проверка номера группы (не пустой, только буквы, цифры и дефис)
                if (string.IsNullOrWhiteSpace(txtGroupNumber.Text))
                {
                    MessageBox.Show("Введите номер группы", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                if (txtGroupNumber.Text.Length < 3)
                {
                    MessageBox.Show("Номер группы должен содержать минимум 3 символа", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                if (!txtGroupNumber.Text.All(c => char.IsLetterOrDigit(c) || c == '-'))
                {
                    MessageBox.Show("Номер группы должен содержать только буквы, цифры и дефисы", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                // Проверка количества человек
                if (string.IsNullOrWhiteSpace(txtPeopleCount.Text))
                {
                    MessageBox.Show("Введите количество человек", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                int peopleCount;
                if (!int.TryParse(txtPeopleCount.Text, out peopleCount))
                {
                    MessageBox.Show("Введите корректное количество человек (целое число)", "Ошибка",
                        MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                if (peopleCount <= 0)
                {
                    MessageBox.Show("Количество человек должно быть больше 0", "Ошибка",
                        MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                if (peopleCount > 50)
                {
                    MessageBox.Show("Количество человек не может превышать 50", "Ошибка",
                        MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                // Проверка возрастной категории
                if (cboAgeCategory.SelectedItem == null)
                {
                    MessageBox.Show("Выберите возрастную категорию", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                var selectedCategory = cboAgeCategory.SelectedItem as ComboBoxItem;
                string ageCategory = selectedCategory.Content.ToString();

                // Сохраняем данные
                Group.GroupNumber = txtGroupNumber.Text;
                Group.PeopleCount = peopleCount;
                Group.AgeCategory = ageCategory;

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