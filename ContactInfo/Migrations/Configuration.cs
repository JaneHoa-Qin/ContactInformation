namespace ContactInfo.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using System.Net;


    internal sealed class Configuration : DbMigrationsConfiguration<ContactInfo.DataLayer.ContactContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(ContactInfo.DataLayer.ContactContext context)
        {
            GetContact().ForEach(c => context.Contacts.Add(c));
        }

        private static List<Model.Contact> GetContact()
        {
            var contacts = new List<Model.Contact>
            {
                new Model.Contact
                {
                    id =1,
                    FirstName = "John",
                    LastName = "Peter",
                    Address1 = "4821 Mackenzie",
                    Address2 = "CDN",
                    State = "Quebec",
                    Country = "Canada",
                    Email="jane.janehoa@gmail.com",
                   
                }
            };
            return contacts;
        }
    }
}
