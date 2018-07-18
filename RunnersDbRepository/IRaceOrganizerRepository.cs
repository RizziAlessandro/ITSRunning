using System;
using System.Collections.Generic;
using System.Text;
using ITSRunningDbRepository.Models;

namespace ITSRunningDbRepository
{
    public interface IRaceOrganizerRepository : IRepositoryBase<RaceOrganizer, int>
    {
        int GetIdByUsername(string username);
    }
}
