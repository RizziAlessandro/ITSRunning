using System;
using System.Collections.Generic;
using System.Text;
using ITSRunningDbRepository.Models;

namespace ITSRunningDbRepository
{
    public interface IRunnerRepository : IRepositoryBase<Runner, int>
    {
        int GetIdByUsername(string username);
    }
}
