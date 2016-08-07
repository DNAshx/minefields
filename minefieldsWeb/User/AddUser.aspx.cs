using minefieldsWeb.DAL;
using minefieldsWeb.Models;
using System;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.DirectoryServices.AccountManagement;
using minefieldsWeb.Install.Helper;
using System.Web;

namespace minefieldsWeb.User
{    
    public partial class AddUser : System.Web.UI.Page
    {
       
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (!Config.IsAdmin || !Config.Installed)
                {
                    Response.Redirect("~/");
                }
                BindData();
            }
        }


        #region Events
        protected void BtnAddUser_Click(object sender, EventArgs e)
        {
            var userName = TxtUserName.Text;

            if (DoesUserExist(userName))
            {
                using (var db = new MinefieldsContext())
                {
                    if (db.Users.Any(x => x.UserName == userName))
                    {
                        TxtUserName.Text = "";
                        LblError.Text = "User which you're trying to add, already exists.";
                        LblError.Visible = true;
                        return;
                    }
                    db.Users.Add(new UserDb() { UserName = userName, IsAllowed = true, Admin = false });
                    db.SaveChanges();
                }
                BindData();
                LblError.Visible = false;
            }
            else
            {
                LblError.Text = "User hasn't been found. Check Login you entering.";
                LblError.Visible = true;
            }
            TxtUserName.Text = "";
        }

        protected void BtnApply_Click(object sender, EventArgs e)
        {
            using (var db = new MinefieldsContext())
            {
                foreach (DataGridItem item in DataGridUsers.Items)
                {
                    var userId = int.Parse(item.Cells[0].Text);
                    var user = db.Users.FirstOrDefault(x => x.UserId == userId);
                    if (user != null)
                    {
                                                
                        user.IsAllowed = ((CheckBox)item.FindControl("ChkBxIsAllowed")).Checked;
                        user.Admin = ((CheckBox)item.FindControl("ChkBxAdmin")).Checked; ;
                    }
                }
                db.SaveChanges();
            }
        }

        protected void Grid_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            DataGridUsers.CurrentPageIndex = e.NewPageIndex;
            BindData();
        }


        protected void Grid_DeleteCommand(object source, DataGridCommandEventArgs e)
        {
            using (var db = new MinefieldsContext())
            {
                var UserId = (int)DataGridUsers.DataKeys[(int)e.Item.ItemIndex];
                db.Users.Remove(db.Users.FirstOrDefault(x => x.UserId == UserId));
                db.SaveChanges();
            }
            LblError.Visible = true;
            BindData();
        }
        #endregion Events

        #region helper
        private void BindData()
        {            
            using (var db = new MinefieldsContext())
            {
                DataGridUsers.DataSource = db.Users.ToList();
                DataGridUsers.DataBind();
            }                       
        }

        private bool DoesUserExist(string userName)
        {
            var details = userName.Split('\\');
            if (details.Count() > 1)
            {
                try
                {
                    using (var domainContext = new PrincipalContext(ContextType.Domain, details[0]))
                    {
                        using (var foundUser = UserPrincipal.FindByIdentity(domainContext, IdentityType.SamAccountName, userName))
                        {
                            LblError.Visible = false;
                            return foundUser != null;
                        }
                    }
                }
                //trying to get local machine user
                //skip this error if it will be appear
                catch (PrincipalServerDownException)
                {                                        
                }
                try
                {
                    using (var domainContext = new PrincipalContext(ContextType.Machine, details[0]))
                    {
                        using (var foundUser = UserPrincipal.FindByIdentity(domainContext, IdentityType.SamAccountName, userName))
                        {
                            LblError.Visible = false;
                            return foundUser != null;
                        }
                    }
                }
                catch(Exception ex)
                {
                    LblError.Text = ex.Message;
                    LblError.Visible = true;
                }

            }
            else
            {
                LblError.Text = "Error format. Please, check format of user name you entering. (DOMAIN\\UserName)";
                LblError.Visible = true;
            }
            return false;
        }
        #endregion helper        
    }
}