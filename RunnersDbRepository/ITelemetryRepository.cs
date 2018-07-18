using System;
using System.Collections.Generic;
using System.Text;
using ITSRunningDbRepository.Models;

namespace ITSRunningDbRepository
{
    public interface ITelemetryRepository : IRepositoryBase <Telemetry, int>
    {
        IEnumerable<Telemetry> GetFirstAndLastTelemetry(int id);
    }
}
