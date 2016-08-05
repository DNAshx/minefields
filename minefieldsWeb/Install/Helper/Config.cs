using minefieldsWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace minefieldsWeb.Install.Helper
{
    public static class Config
    {
        public static InstallSection ConfigSection { get { return GetConfig(); }}
        public static InstallSection GetConfig()
        {
            return (InstallSection)System.Configuration.ConfigurationManager.GetSection("InstallingGroup/install");
        }
    }
}