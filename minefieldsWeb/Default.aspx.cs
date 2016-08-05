using minefieldsWeb.Install.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace minefieldsWeb
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var installed = Config.ConfigSection.Isntalled;
            var userName = Config.ConfigSection.UserName;
            var currUserName = HttpContext.Current.User.Identity.Name;

            installedDiv.Visible = installed;
            notInstalledDiv.Visible = !installed;
            administrationDiv.Visible = userName == currUserName;            
        }
    }
}