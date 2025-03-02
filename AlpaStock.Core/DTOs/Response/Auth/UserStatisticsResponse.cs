using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlpaStock.Core.DTOs.Response.Auth
{
    public class UserStatisticsResponse
    {
        public List<UserStatistics> UserStatistics { get; set; }
        public int CurrentYearTotalUserCount { get; set; }
        public int TotalUserCount { get; set; }
    }
}
