using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Demo.MicroService.AuthenticationCenter.Utility
{
    public class CurrentUserModel
    {
        public Guid TSysUserID { get; set; }
        public string UserName { get; set; }
        public string Mail { get; set; }
        public string Mobile { get; set; }
        public Guid MTenantID { get; set; }
    }
}