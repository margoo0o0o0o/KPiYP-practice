using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace TouristVoucher
{
    public partial class ChartWindow : Window
    {
        public ChartWindow(List<TripInfo> trips)
        {
            InitializeComponent();
            LoadChart(trips);
        }

        private void LoadChart(List<TripInfo> trips)
        {
            // Группируем поездки по месяцам
            var monthlyStats = trips
                .GroupBy(t => new { t.TripDate.Year, t.TripDate.Month })
                .Select(g => new
                {
                    Month = GetMonthName(g.Key.Month) + " " + g.Key.Year,
                    Count = g.Count()
                })
                .OrderBy(x => x.Month)
                .ToList();

            if (monthlyStats.Count == 0)
            {
                MessageBox.Show("Нет данных для отображения диаграммы",
                    "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                Close();
                return;
            }

            ColumnSeries.ItemsSource = monthlyStats;
        }

        private string GetMonthName(int month)
        {
            string[] months = { "Янв", "Фев", "Мар", "Апр", "Май", "Июн", "Июл", "Авг", "Сен", "Окт", "Ноя", "Дек" };
            return months[month - 1];
        }
    }
}