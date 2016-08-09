using minefieldsWeb.DAL;
using minefieldsWeb.Install.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace minefieldsWeb.Install
{
    public partial class Enabled : System.Web.UI.Page
    {
        private bool _isDisabled = false;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (!Config.IsAdmin || !Config.Installed)
                {
                    Response.Redirect("~/");
                }
                using (var db = new MinefieldsContext())
                {
                    //trying to get IsDisable property
                    var property = db.Properties.FirstOrDefault(x => x.Name == Config.IS_DISABLE_NAME);
                    if (property != null)
                    {
                        _isDisabled = bool.Parse(property.Value.ToString());
                    }
                    else //if property not exists we create it with false value (just installed tool
                    {
                        db.Properties.Add(new Models.PropertyDb() { Name = Config.IS_DISABLE_NAME, Value = false.ToString() });
                        db.SaveChanges();
                    }
                }
                BtnEnableDisable.Text = _isDisabled ? "Enable" : "Disable";
            }
            
        }

        protected void BtnEnableDisable_Click(object sender, EventArgs e)
        {
            using (var db = new MinefieldsContext())
            {
                var property = db.Properties.FirstOrDefault(x => x.Name == Config.IS_DISABLE_NAME);
                if (property != null)
                {
                    _isDisabled = !bool.Parse(property.Value);
                    property.Value = (_isDisabled).ToString();                    
                    BtnEnableDisable.Text = _isDisabled ? "Enable" : "Disable";
                    db.SaveChanges();
                }
            }            
        }
    }
}