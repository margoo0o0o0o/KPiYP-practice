using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TouristVoucher
{
    public class TourAgent
    {
        public string FullName { get; set; }
        public string InsuranceNumber { get; set; }
        public string INN { get; set; }
        public string PassportNumber { get; set; }
        public DateTime VisaExpiry { get; set; }

        // Для отображения в DataGrid
        public string VisaExpiryText => VisaExpiry.ToString("dd.MM.yyyy");
        public bool IsVisaValid => VisaExpiry > DateTime.Now;
        public string VisaStatus => IsVisaValid ? "Действительна" : "Просрочена";
    }
}
