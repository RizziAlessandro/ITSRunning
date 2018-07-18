using System;
using System.Collections.Generic;
using System.Text;

namespace ITSRunningDbRepository.Models
{
    public class Runner
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Username { get; set; }
        public int Gender { get; set; }
        public string PhotoUri { get; set; }
    }
}
