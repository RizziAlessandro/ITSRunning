using System;
using System.Collections.Generic;
using System.Text;
using ITSRunningDbRepository.Models;

namespace ITSRunningDbRepository
{
    public interface IActivityRepository : IRepositoryBase<Activity, int>
    {
        int InsertAndReturnId(Activity value);
        IEnumerable<Activity> GetListByIdRunner(int id);
    }
}
