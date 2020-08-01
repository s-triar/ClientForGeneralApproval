using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace cobamvc.Models
{
    public class Decision
    {
        [Required]
        public string Nik { get; set; }
        [Required]
        public string ApiName { get; set; }
        [Required]
        public string Id { get; set; }
        [Required]
        public string Data { get; set; }
        [Required]
        public string Link { get; set; }
    }

    public class DecisionData
    {
        public bool Data { get; set; }
        public string DataForm { get; set; }
    }

    public class DecisionDataForm
    {
        public string Name { get; set; }
        public dynamic Value { get; set; }
    }
}