using System;
using EmployeeManager.Data.Enums;
using Newtonsoft.Json;

namespace EmployeeManager.Data
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public Gender Gender { get; set; }
        public UserActivityStatus Status { get; set; }
        
        [JsonProperty("created_at")]
        public DateTime Created { get; set; }
        
        [JsonProperty("updated_at")]
        public DateTime Updated { get; set; }
    }
}
