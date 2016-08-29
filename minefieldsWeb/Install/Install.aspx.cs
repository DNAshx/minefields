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
        public static string ConnectionString
        {
            get; private set;
        }
        public static string ServerName
        { get; private set; }
        #endregion
        #region CONSTANTS
        private const string CREATE_DATABASE_CMD = @"CREATE DATABASE [Minefields]";
        private const string CHECK_DB = @"SELECT 1 FROM sys.databases WHERE name = 'Minefields'";
        private const string CREATE_USERS_TABLE = @"USE [Minefields]
CREATE TABLE [Users](
	[UserId] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [nchar](50) NOT NULL,
	[IsAllowed] [bit] NOT NULL,
    [Admin] [bit] NOT NULL,
    CONSTRAINT PK_Users_UserId PRIMARY KEY CLUSTERED (UserId)
) ON [PRIMARY]";
        private const string INSERT_USERS_TABLE = @"declare @user varchar(50) = SYSTEM_USER
 INSERT INTO Users
 SELECT @user, 1, 1";
        private const string CREATE_USER_CMD = @"USE [master]
CREATE LOGIN [Minefields] WITH PASSWORD=N'Pa$$w0rd!1', DEFAULT_DATABASE=[Minefields], CHECK_EXPIRATION=OFF, CHECK_POLICY=OFF
ALTER SERVER ROLE [securityadmin] ADD MEMBER [Minefields]
USE [Minefields]

CREATE USER [Minefields] FOR LOGIN [Minefields];

EXEC sp_addrolemember 'db_owner', 'Minefields'
";
        
        private const string DB_NAME = "Minefields";
        #endregion CONSTANTS

        #region members
        private bool _alreadyInstalled;
        #endregion members

        #region Page Load
        protected void Page_Load(object sender, EventArgs e)
        {
            TxtLogin.Text = Context.User.Identity.Name;
        }
        #endregion Page Load

        #region Methods
        protected void InstallBtn_Click(object sender, EventArgs e)
        {
            try
            {
                ServerName = TxtServerName.Text;                
                var loginName = TxtLogin.Text;
                var password = TxtPassword.Text;
                if (ChkBxTrustConnection.Checked)
                {
                    ConnectionString = string.Format(@"Server={0};persist security info=True;Integrated Security=SSPI;", ServerName);//Database={1};
                }
                else
                {
                    ConnectionString = string.Format(@"Server={0};User Id={1};Password={2};", ServerName, loginName, password);//Database={1};
                }
                
                if (CheckIsInstall())
                {                    
                    InstallTool();
                    //check for errors
                    if (!TxtError.Visible)
                        Response.Redirect("~/Install/ChangePassword");
                }
                else
                {
                    TxtError.Visible = true;
                    TxtError.Text += _alreadyInstalled ? "" : "\nTool wasn't installed";                    
                    //TxtError.Height = new Unit(100, UnitType.Pixel);
                }

                //TxtError.Width = new Unit(500, UnitType.Pixel);
                TxtLogin.Text = "";
                TxtPassword.Text = "";
            }
            catch(Exception ex)
            {
                TxtError.Visible = true;
                TxtError.Text = ex.Message;
            }
            TxtError.Columns = TxtError.Text.Length;
        }

        protected void ChkBxTrustConnection_CheckedChanged(object sender, EventArgs e)
        {
            if (((CheckBox)sender).Checked)
            {
                TxtLogin.Enabled = false;
                TxtPassword.Enabled = false;
            }
            else
            {
                TxtLogin.Enabled = true;
                TxtPassword.Enabled = true;
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

                    //create DB as it is not exists
                    var sqlCmd = new SqlCommand(CREATE_DATABASE_CMD, conn);
                    sqlCmd.ExecuteNonQuery();
                }
                catch (SqlException ex)
                {
                    TxtError.Visible = true;
                    TxtError.Text = ex.Message;
                } 
            }
            ConnectionString = ConnectionString.Insert(ConnectionString.IndexOf(';') + 1, "Database=Minefields;");
            conn = new SqlConnection(ConnectionString);
            using (conn)
            {
                try
                {
                    conn.Open();
                    //change CREATE LOGIN and DB USER
                    var sqlCmd = new SqlCommand(CREATE_USER_CMD, conn);
                    sqlCmd.ExecuteNonQuery();

                    ////create Users table
                    //sqlCmd = new SqlCommand(CREATE_USERS_TABLE, conn);
                    //sqlCmd.ExecuteNonQuery();

                    ////Add User to the table
                    //sqlCmd = new SqlCommand(INSERT_USERS_TABLE, conn);
                    //sqlCmd.ExecuteNonQuery();
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
                        var sqlCmd = new SqlCommand(CHECK_DB, conn);
                        var reader = sqlCmd.ExecuteReader();
                        while (reader.Read())
                        {
                            var val = 0;
                            if (int.TryParse(reader[0].ToString(), out val) && val == 1)
                            {
                                TxtError.Text = "Tool already isntalled.";
                                _alreadyInstalled = true;
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