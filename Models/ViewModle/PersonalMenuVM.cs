using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _24dh112968_MyStore_lap1.Models.ViewModle
{
    public class PersonalMenuVM
    {
        public bool IsLoggedIn { get; set; }
        public string UserName { get; set; }
        public int CartCount { get; set; }
    }
}