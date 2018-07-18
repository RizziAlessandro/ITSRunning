using System;
using System.Collections.Generic;
using System.Text;
using ITSRunningDbRepository.Models;

namespace ITSRunningDbRepository
{
    public interface IRaceRepository : IRepositoryBase<Race, int>
    {
        IEnumerable<Race> GetListById(int id);
        IEnumerable<Race> GetRaceEnabled();
        void EnableDisableRace(int id, bool value);
    }
}
