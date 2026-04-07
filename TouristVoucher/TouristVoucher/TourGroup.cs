using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TouristVoucher
{
    public class TourGroup
    {
        public string GroupNumber { get; set; }
        public int PeopleCount { get; set; }
        public string AgeCategory { get; set; }

        // Для отображения в DataGrid
        public string PeopleCountText => $"{PeopleCount} чел.";
    }
}
