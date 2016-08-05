using minefieldsWeb.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace minefieldsWeb.DAL
{
    public class MinefieldsContext : DbContext
    {
        public DbSet<UserDb> Users { get; set; }
    }
}