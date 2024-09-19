using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPISVR_Managements.ViewModel
{
    public static class KhmerCalendarHelper
    {
        private static readonly Dictionary<int, string> KhmerMonthNames = new Dictionary<int, string>
    {
        { 1, "មករា" },   // January
        { 2, "កម្ភះ" },  // February
        { 3, "មីនា" },   // March
        { 4, "មេសា" },   // April
        { 5, "ឧសភា" },   // May
        { 6, "មិថុនា" }, // June
        { 7, "កក្កដា" },  // July
        { 8, "សីហា" },   // August
        { 9, "កញ្ញា" },  // September
        { 10, "តុលា" },  // October
        { 11, "វិច្ឆិកា" }, // November
        { 12, "ធ្នូ" }    // December
    };

        public static string GetKhmerMonthName(int month)
        {
            return KhmerMonthNames.TryGetValue(month, out var monthName) ? monthName : "";
        }
    }
}
