using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace exercise.Models
{
    public class PutRequest 
    {
        public string buildingCode { get; set; }

        public string description { get; set; }

        public CurrentStatus? currentStatus { get; set; }
    }
}
