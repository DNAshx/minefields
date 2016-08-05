using System;
using System.Collections;
using System.Text;
using System.Configuration;
using System.Xml;

namespace minefieldsWeb.Models
{
    public class InstallSection : ConfigurationSection
    {
        // Create a "installed" attribute.
        [ConfigurationProperty("installed", DefaultValue = "false", IsRequired = false)]
        public Boolean Isntalled
        {
            get
            {
                return (Boolean)this["installed"];
            }
            set
            {
                this["installed"] = value;
            }
        }

        // Create a "UserName" attribute.
        [ConfigurationProperty("userName", DefaultValue = "", IsRequired = false)]
        public string UserName
        {
            get
            {
                return (string)this["userName"];
            }
            set
            {
                this["userName"] = value;
            }
        }
    }
}