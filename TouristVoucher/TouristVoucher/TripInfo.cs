using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TouristVoucher
{
    public class TripInfo
    {
        // Информация о поездке
        public string AgentName { get; set; }
        public string GroupNumber { get; set; }
        public DateTime TripDate { get; set; }
        public string ContractNumber { get; set; }
        public int TripDuration { get; set; }

        // Информация о группе
        public int PeopleCount { get; set; }
        public string AgeCategory { get; set; }

        // Информация о турагенте
        public string InsuranceNumber { get; set; }
        public string INN { get; set; }
        public string PassportNumber { get; set; }
        public DateTime VisaExpiry { get; set; }

        // Для отображения в DataGrid (вычисляемые свойства)
        public string TripDateText => TripDate.ToString("dd.MM.yyyy");
        public string ReturnDateText => TripDate.AddDays(TripDuration).ToString("dd.MM.yyyy");
        public string VisaExpiryText => VisaExpiry.ToString("dd.MM.yyyy");
        public string VisaStatus => VisaExpiry > DateTime.Now ? "✓ Действительна" : "✗ Просрочена";
        public string TripPeriod => $"{TripDateText} - {ReturnDateText}";
    }
}
