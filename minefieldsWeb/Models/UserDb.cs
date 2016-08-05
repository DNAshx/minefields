using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace minefieldsWeb.Models
{
    [Table ("Users")]
    public class UserDb
    {
        [Key]
        public int UserId { get; set; }
        public string UserName { get; set; }
        public bool IsAllowed { get; set; }
        public bool Admin { get; set; }
    }
}