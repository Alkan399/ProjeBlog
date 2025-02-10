using ProjeBlog.Models;
using ProjeBlog.RepositoryPattern.Base;
using ProjeBlog.RepositoryPattern.Interfaces;
using System;
using System.Collections.Generic;

namespace ProjeBlog.Services
{
    public interface IStatisticsService<T> where T :BaseEntity
    {
        public int GetCountStatisticsOfADateByDay(T repository, DateTime date);
        public int GetCountStatisticsBetweenDatesByDay(T repository, DateTime firstDate, DateTime secondDate);
    }
}
