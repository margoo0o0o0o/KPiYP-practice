using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TouristVoucher
{
    public class TripSchedule
    {
        public string AgentName { get; set; }
        public string GroupNumber { get; set; }
        public DateTime TripDate { get; set; }
        public string ContractNumber { get; set; }
        public int TripDuration { get; set; }

        // Для отображения в DataGrid
        public string TripDateText => TripDate.ToString("dd.MM.yyyy");
        public string ReturnDateText => TripDate.AddDays(TripDuration).ToString("dd.MM.yyyy");
        public string TripDurationText => $"{TripDuration} дней";
    }
}
