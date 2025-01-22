using ProjeBlog.Enums;
using System;
using System.ComponentModel;

namespace ProjeBlog.Models
{
    public class Statistics: BaseEntity
    {
        public string StatisticName { get; set; }
        public string StatisticDescription { get; set; }
        public StatisticsTypeEnum StatisticType { get; set; } = StatisticsTypeEnum.None;
        public int Count { get; set; } = 0;
    }
}
