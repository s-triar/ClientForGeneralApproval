using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace cobamvc.Models
{
    public class RequestData
    {

        public string nik {get;set;}
        public string apiName {get;set;}
        public string id {get;set;}
        public string projectName {get;set;}
        public string urlProject {get;set;}
        public string category {get;set;}
        public string title {get;set;}
        public string subTitle {get;set;}
        public string status {get;set;}
        public string detail {get;set;}
        public string urlAction {get;set;}
         public NotifConfig dataNotif { get; set; }
    }

    public class NotifConfig
    {
        public string title { get; set; }
        public string message { get; set; }
    }
}