using minefieldsWeb.DAL;
using minefieldsWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace minefieldsWeb.User
{
    public partial class AddUser : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void BtnAddUser_Click(object sender, EventArgs e)
        {
            using (var db = new MinefieldsContext())
            {
                db.Users.Add(new UserDb() { UserName = TxtUserName.Text, IsAllowed = true, Admin = false});
                db.SaveChanges();
            }
        }
    }
}