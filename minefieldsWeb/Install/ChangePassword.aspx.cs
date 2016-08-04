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

        protected void Unnamed1_Click(object sender, EventArgs e)
        {
            var configuration = WebConfigurationManager.OpenWebConfiguration("~");
            var section = (ConnectionStringsSection)configuration.GetSection("connectionStrings");
            var connectionString = string.Format(section.ConnectionStrings["MinefieldConnection"].ConnectionString, Install.ServerName, TxtPassword.Text);
            section.ConnectionStrings["MinefieldConnection"].ConnectionString = connectionString;
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