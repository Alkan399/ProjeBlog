using ProjeBlog.Models;
using ProjeBlog.RepositoryPattern.Base;
using ProjeBlog.RepositoryPattern.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjeBlog.Services
{
    public class StatisticsService<T> : IStatisticsService<T> where T : BaseEntity
    {
        IRepository<T> _repository;
        public StatisticsService(IRepository<T> repository)
        {
            _repository = repository;
        }
        public int GetCountStatisticsOfADateByDay(T repository, DateTime date)
        {
            int listStatistics = _repository.GetAll().Where(x => x.CreatedDate.Date == date.Date).GroupBy(y => y.CreatedDate.Date).Count();
            return listStatistics;
        }
        public int GetCountStatisticsBetweenDatesByDay(T repository, DateTime firstDate, DateTime secondDate)
        {
            int listStatistics = _repository.GetAll().Where(x => x.CreatedDate.Date <= firstDate && x.CreatedDate >= secondDate).GroupBy(y => y.CreatedDate.Date).Count();
            return listStatistics;
        }


    }
}
