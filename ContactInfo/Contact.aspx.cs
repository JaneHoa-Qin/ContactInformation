#region
  //05/05/2025 (Jane Qin) Create 
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ContactInfo.DataLayer;

namespace ContactInfo
{
    public partial class Contact : Page
    {
        private readonly Repository _repository = new Repository(new ContactContext());

        #region
        //05/05/2025 (Jane Qin) Create 
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadContacts();
            }
        }

        #region
        //05/05/2025 (Jane Qin)Create  function to bind the data to GridView
        #endregion
        private void LoadContacts()
        {
            var contacts = _repository.GetContacts();
            gridService.DataSource = contacts;
            gridService.DataBind();
        }

        #region
        //05/05/2025 (Jane Qin) Create  function to activate TextBox of the selected row 
        #endregion
        protected void gridService_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gridService.EditIndex = e.NewEditIndex;
            LoadContacts();
        }

        #region
        //05/05/2025 (Jane Qin) Create  function to edit the selected record
        #endregion
        protected void gridService_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow row = gridService.Rows[e.RowIndex];
            int id = Convert.ToInt32(gridService.DataKeys[e.RowIndex].Value);
            string firstName = ((TextBox)row.FindControl("txtFirstName")).Text;
            string lastName = ((TextBox)row.FindControl("txtLastName")).Text;
            string address1 = ((TextBox)row.FindControl("txtAddress1")).Text;
            string address2 = ((TextBox)row.FindControl("txtAddress2")).Text;
            string state = ((TextBox)row.FindControl("txtState")).Text;
            string country = ((TextBox)row.FindControl("txtCountry")).Text;
            string email = ((TextBox)row.FindControl("txtEmail")).Text;

            var contact = new Model.Contact
            {
                id = id,
                FirstName = firstName,
                LastName = lastName,
                Address1 = address1,
                Address2 = address2,
                State = state,
                Country = country,
                Email = email
            };

            _repository.UpdateContact(contact); // Ensure UpdateContact exists in Repository
            _repository.Save();
            gridService.EditIndex = -1;
            LoadContacts();
        }

        #region
        //05/05/2025 (Jane Qin) Create  function to delete the selected record
        #endregion
        protected void gridService_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int id = Convert.ToInt32(gridService.DataKeys[e.RowIndex].Value);
            var contact = _repository.GetContactById(id);
            if (contact != null)
            {
                _repository.DeleteContact(contact);
                _repository.Save();
            }
            LoadContacts();
        }

        #region
        //05/05/2025 (Jane Qin) Create  function to cancel the editing
        #endregion
        protected void gridService_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gridService.EditIndex = -1;
            LoadContacts();
        }

        #region
        //05/05/2025 (Jane Qin) Create  function to add new record
        #endregion
        protected void btnAddNew_Click(object sender, EventArgs e)
        {
            string firstName = txtNewFirstName.Text;
            string lastName = txtNewLastName.Text;
            string address1 = txtNewAddress1.Text;
            string address2 = txtNewAddress2.Text;
            string state = txtNewState.Text;
            string country = txtNewCountry.Text;
            string email = txtNewEmail.Text;

            var newContact = new Model.Contact
            {
                FirstName = firstName,
                LastName = lastName,
                Address1 = address1,
                Address2 = address2,
                State = state,
                Country = country,
                Email = email
            };

            _repository.AddContact(newContact);
            _repository.Save();
            LoadContacts();

            // Clear the input fields
            txtNewFirstName.Text = string.Empty;
            txtNewLastName.Text = string.Empty;
            txtNewAddress1.Text = string.Empty;
            txtNewAddress2.Text = string.Empty;
            txtNewState.Text = string.Empty;
            txtNewCountry.Text = string.Empty;
            txtNewEmail.Text = string.Empty;
        }

    }
}