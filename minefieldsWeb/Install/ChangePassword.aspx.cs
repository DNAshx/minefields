using minefieldsWeb.Install.Helper;
using minefieldsWeb.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace minefieldsWeb.Install
{
    public partial class ChangePassword : System.Web.UI.Page
    {
        private string _scriptChangePassword = @"ALTER LOGIN [Minefields] WITH PASSWORD=N'{0}'";
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void BtnSave_Click(object sender, EventArgs e)
        {
            //save to configuration new connectionString
            var configuration = WebConfigurationManager.OpenWebConfiguration("~");
            var section = (ConnectionStringsSection)configuration.GetSection("connectionStrings");
            var connectionString = string.Format(section.ConnectionStrings["MinefieldsContext"].ConnectionString, Install.ServerName, TxtPassword.Text);
            section.ConnectionStrings["MinefieldsContext"].ConnectionString = connectionString;            

            //save to configuration Installed flag widh admin name
            var installConfig = (InstallSection)configuration.GetSection("InstallingGroup/install");
            installConfig.Isntalled = true;
            installConfig.UserName = HttpContext.Current.User.Identity.Name;

            configuration.Save();

            _scriptChangePassword = string.Format(_scriptChangePassword, TxtPassword.Text);

            #region ChangePAssword
            var conn = new SqlConnection(Install.ConnectionString);

            using (conn)
            {
                try
                {
                    conn.Open();                    
                    var sqlCmd = new SqlCommand(_scriptChangePassword, conn);
                    sqlCmd.ExecuteNonQuery();                    
                }
                catch (SqlException ex)
                {
                    TxtError.Visible = true;
                    TxtError.Text = ex.Message;
                }
            }
            #endregion

            Response.Redirect("~/Install/Success");
        }
    }
}