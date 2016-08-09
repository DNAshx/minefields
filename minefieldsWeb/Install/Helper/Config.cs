using minefieldsWeb.DAL;
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
        public const string IS_DISABLE_NAME = "IsDisable";
        public static InstallSection ConfigSection { get { return GetConfig(); }}
        public static bool Installed
        {
            get { return ConfigSection != null ? ConfigSection.Installed : false; }
        }

        public static bool IsAdmin
        {
            get
            {
                var currUserName = "";
                if (HttpContext.Current.User != null)
                    currUserName = HttpContext.Current.User.Identity.Name;
                return ConfigSection != null ? currUserName == ConfigSection.UserName : false;
            }
        }

        public static bool IsDisabled
        {
            get
            {
                return GetDisabled();
            }
        }
        public static InstallSection GetConfig()
        {
            var configuration = WebConfigurationManager.OpenWebConfiguration("~");
            if (configuration != null)
            {
                return (InstallSection)configuration.GetSection("InstallingGroup/install");
            }
            return null;
        }
        
        public static bool GetDisabled()
        {
            using (var db = new MinefieldsContext())
            {
                var property = db.Properties.First(x => x.Name == IS_DISABLE_NAME);
                if (property != null)
                {
                    return bool.Parse(property.Value);
                }
            }
            return false;
        }
    }
}