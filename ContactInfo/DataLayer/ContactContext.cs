#region
//05/05/2025 (Jane Qin) Create 
#endregion
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ContactInfo.DataLayer
{
    public class ContactContext:DbContext
    {
        public ContactContext() : base("ContactInfoDB")
        {
        }
        public DbSet<Model.Contact> Contacts { get; set; }
      
    }
}