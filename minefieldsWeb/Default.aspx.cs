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
            if (!Page.IsPostBack)
            {
                var installed = Config.Installed;

                PanelInstalled.Visible = installed;
                PanelInstall.Visible = !installed;
                PanelAddUser.Visible = installed && Config.IsAdmin && !Config.IsDisabled;
                PanelDisabled.Visible = Config.IsDisabled;
            }
        }       
    }
}