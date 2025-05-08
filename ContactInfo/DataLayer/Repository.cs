#region
//05/05/2025 (Jane Qin) Create 
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Linq.Dynamic.Core;

namespace ContactInfo.DataLayer
{
    public class Repository
    {
        private readonly ContactContext _dbContext;

        #region
        //05/05/2025 (Jane Qin) DI ContactContext 
        #endregion
        public Repository(ContactContext dbContext)
        {
            _dbContext = dbContext;
        }

        #region
        //05/05/2025 (Jane Qin) Create get all contacts
        #endregion
        public List<Model.Contact> GetContacts()
        {
            return _dbContext.Contacts
                .OrderBy(c => c.FirstName)
                .ThenBy(c => c.LastName).ToList();
        }

        public List<Model.Contact> GetContacts(int pageIndex, int pageSize, string sortColumn, string sortDirection, out int totalRecords)
        {
            var query = _dbContext.Contacts
                        .Where(c => !string.IsNullOrEmpty(c.FirstName)); // filter out blanks if needed

            totalRecords = query.Count();

            if (!string.IsNullOrEmpty(sortColumn))
            {
                string orderBy = $"{sortColumn} {sortDirection}";
                query = query.OrderBy(orderBy);
            }

            // Assume pageSize = 10, pageIndex = 2
            //Skip(pageIndex * pageSize) → Skip(2 * 10) → Skip 20, Take(pageSize) → Take(10)
            // Skip 20 records and take the next 10 records
            return query
                .Skip(pageIndex * pageSize)
                .Take(pageSize)
                .ToList();
        }

        #region
        //05/06/2025 (Jane Qin) Create get all contacts
        #endregion
        public int GetTotalContactsCount()
        {
            return _dbContext.Contacts
                .Where(c => !string.IsNullOrEmpty(c.FirstName)) // filter out empty first names
                .Count();
        }

        #region
        //05/05/2025 (Jane Qin) Create add new contact
        #endregion
        public void AddContact(Model.Contact contact)
        {
            _dbContext.Contacts.Add(contact);
        }

        #region
        //05/05/2025 (Jane Qin) Create get contact by id
        #endregion
        public Model.Contact GetContactById(int id)
        {
            return _dbContext.Contacts.FirstOrDefault(c => c.id ==id);
        }

        #region
        //05/05/2025 (Jane Qin) Create update contact
        #endregion
        public void UpdateContact(Model.Contact contact)
        {
            var existingContact = _dbContext.Contacts.Find(contact.id);
            if (existingContact != null)
            {
                existingContact.FirstName = contact.FirstName;
                existingContact.LastName = contact.LastName;
                existingContact.Address1 = contact.Address1;
                existingContact.Address2 = contact.Address2;
                existingContact.State = contact.State;
                existingContact.Country = contact.Country;
                existingContact.Email = contact.Email;
            }
        }

        #region
        //05/05/2025 (Jane Qin) Create delete contact
        #endregion
        public void DeleteContact(Model.Contact contact)
        {
            _dbContext.Contacts.Remove(contact);
        }
        public bool Save()
        {
            return (_dbContext.SaveChanges() >= 0);
        }
    }
}