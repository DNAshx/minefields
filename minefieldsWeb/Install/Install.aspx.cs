using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace minefieldsWeb.Install
{
    public partial class Install : System.Web.UI.Page
    {
        #region property
        public string ConnectionString
        {
            get; private set;
        }
        #endregion
        #region CONSTANTS
        private const string CREATE_SCHEMA_CMD = @" CREATE SCHEMA [Minefields] AUTHORIZATION [dbo]";
        private const string CHECK_SCHEME = @"SELECT  1
FROM    information_schema.schemata
WHERE   schema_name = 'Minefields'";
        private const string CREATE_USERS_TABLE = @"CREATE TABLE [Minefields].[Users](
	[UserId] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [nchar](50) NOT NULL,
	[IsAllowed] [bit] NOT NULL
) ON [PRIMARY]";
        private const string CREATE_USER_CMD = @"declare @user varchar(50) = SYSTEM_USER
declare @sql varchar(2000) = 'CREATE USER [Minefield] for LOGIN [' + @user + '] WITH DEFAULT_SCHEMA=[Minefields];'
EXEC(@sql) ";
        #endregion CONSTANTS

        #region members

        #endregion members

        #region Page Load
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        #endregion Page Load

        #region Methods
        protected void InstallBtn_Click(object sender, EventArgs e)
        {
            try
            {
                ConnectionString = TxtConnString.Text;
                if (CheckIsInstall())
                {
                    InstallTool();
                    Response.Redirect("/../Success");
                }
                else
                {
                    TxtError.Visible = true;
                    TxtError.Text += "/nTool wasn't installed";
                    TxtError.Height = new Unit(100, UnitType.Pixel);
                }
                TxtError.Width = new Unit(500, UnitType.Pixel);
                TxtConnString.Text = "";
            }
            catch(Exception ex)
            {
                TxtError.Visible = true;
                TxtError.Text = ex.Message;
            }
        }
        #endregion Methods

        #region helpers
        private void InstallTool()
        {
            var conn = new SqlConnection(ConnectionString);

            using (conn)
            {
                try
                {
                    conn.Open();

                    //create scheme as it is not exists
                    var sqlCmd = new SqlCommand(CREATE_SCHEMA_CMD, conn);
                    sqlCmd.ExecuteNonQuery();

                    //change CREATE USER FOR current [LOGIN]
                    sqlCmd = new SqlCommand(CREATE_USER_CMD, conn);
                    sqlCmd.ExecuteNonQuery();

                    //create Users table
                    sqlCmd = new SqlCommand(CREATE_USERS_TABLE, conn);
                    sqlCmd.ExecuteReader();
                }
                catch (SqlException ex)
                {
                    TxtError.Visible = true;
                    TxtError.Text = ex.Message;
                }
            }
        }

        private bool CheckIsInstall()
        {
            if (!String.IsNullOrEmpty(ConnectionString))
            {
                var conn = new SqlConnection(ConnectionString);
                if (conn.Database == null)
                {
                    TxtError.Text = "Database should be in connection string.";
                    TxtError.Visible = true;
                    return false;
                }
                using (conn)
                {
                    try
                    {
                        conn.Open();

                        //running commad to check is Scheme exists
                        var sqlCmd = new SqlCommand(CHECK_SCHEME, conn);
                        var reader = sqlCmd.ExecuteReader();
                        while (reader.Read())
                        {
                            var val = 0;
                            if (int.TryParse(reader[0].ToString(), out val) && val == 1)
                            {
                                TxtError.Text = "Tool already isntalled.";
                                return false;
                            }
                        }
                    }
                    catch (SqlException ex)
                    {
                        TxtError.Text = ex.Message;
                        TxtError.Visible = true;
                        return false;
                    }
                }
            }
            TxtError.Visible = false;
            return true;
        }
        #endregion helpers
    }
}