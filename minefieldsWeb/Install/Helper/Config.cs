using minefieldsWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace minefieldsWeb.Install.Helper
{
    public static class Config
    {
        public static InstallSection ConfigSection { get { return GetConfig(); }}
        public static bool Installed
        {
            get { return ConfigSection.Installed; }
        }

        public static bool IsAdmin
        {
            get
            {
                var currUserName = HttpContext.Current.User.Identity.Name;
                return currUserName == ConfigSection.UserName;
            }
        }
        public static InstallSection GetConfig()
        {
            var configuration = WebConfigurationManager.OpenWebConfiguration("~");
            return (InstallSection)configuration.GetSection("InstallingGroup/install");
        }
    }
}