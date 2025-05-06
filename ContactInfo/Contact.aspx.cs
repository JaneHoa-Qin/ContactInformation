#region
//05/05/2025 (Jane Qin) Create 
//05/05/2025 (Jane Qin) btnAddNew_Click: Reload the last page of contacts after adding a new one, change the load function to LoadContactsWithPaging
//05/06/2025 (Jane Qin) Create server-side paging for the GridView, LoadContactsWithPaging/gridService_PageIndexChanging
//05/06/2025 (Jane Qin) gridService_RowEditing/gridService_RowUpdating/gridService_RowDeleting/gridService_RowCancelingEdit: change the load function to LoadContactsWithPaging
//05/06/2025 (Jane Qin) Sorting by FirstName, added Email validation client-side
#endregion

using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ContactInfo.DataLayer;
using System.Linq.Dynamic.Core;
using System.ComponentModel.DataAnnotations;
using ContactInfo.Model;

namespace ContactInfo
{
    public partial class Contact : Page
    {
        private readonly Repository _repository = new Repository(new ContactContext());
        private int pageSize = 2;
        #region
        //05/05/2025 (Jane Qin) Create 
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadContactsWithPaging(0);
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
        //05/06/2025 (Jane Qin) Create server-side paging for the GridView
        #endregion
        private void LoadContactsWithPaging(int pageIndex)
        {
            int totalRecords;
            var data = _repository.GetContacts(
                pageIndex,
                gridService.PageSize,
                SortColumn,
                SortDirection,
                out totalRecords);

            int maxPageIndex = (int)Math.Ceiling((double)totalRecords / gridService.PageSize) - 1;
            if (pageIndex > maxPageIndex && maxPageIndex >= 0)
                pageIndex = maxPageIndex;

            gridService.DataSource = data;
            gridService.VirtualItemCount = totalRecords;
            gridService.PageIndex = pageIndex;
            gridService.DataBind();
        }

        #region
        //05/06/2025 (Jane Qin) Create server-side paging for the GridView
        #endregion
        protected void gridService_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            LoadContactsWithPaging(e.NewPageIndex);
        }
        #region
        //05/05/2025 (Jane Qin) Create  function to activate TextBox of the selected row 
        //05/06/2025 (Jane Qin) change the load function to LoadContactsWithPaging
        #endregion
        protected void gridService_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gridService.EditIndex = e.NewEditIndex;
            LoadContactsWithPaging(gridService.PageIndex);
        }

        #region
        //05/05/2025 (Jane Qin) Create  function to edit the selected record
        //05/06/2025 (Jane Qin) change the load function to LoadContactsWithPaging
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
            LoadContactsWithPaging(gridService.PageIndex);
        }

        #region
        //05/05/2025 (Jane Qin) Create  function to delete the selected record
        //05/06/2025 (Jane Qin) change the load function to LoadContactsWithPaging
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
            LoadContactsWithPaging(0);
        }

        #region
        //05/05/2025 (Jane Qin) Create  function to cancel the editing
        //05/06/2025 (Jane Qin) change the load function to LoadContactsWithPaging
        #endregion
        protected void gridService_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gridService.EditIndex = -1;
            LoadContactsWithPaging(gridService.PageIndex);
        }

        #region
        //05/05/2025 (Jane Qin) Create  function to add new record
        //05/06/2025 (Jane Qin) Reload the last page of contacts after adding a new one, change the load function to LoadContactsWithPaging
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
           

            // Reload the last page of contacts after adding a new one
            int totalRecords = _repository.GetTotalContactsCount();
            int lastPageIndex = (int)Math.Floor((double)(totalRecords - 1) / pageSize);
            LoadContactsWithPaging(lastPageIndex);

            // Clear the input fields
            txtNewFirstName.Text = string.Empty;
            txtNewLastName.Text = string.Empty;
            txtNewAddress1.Text = string.Empty;
            txtNewAddress2.Text = string.Empty;
            txtNewState.Text = string.Empty;
            txtNewCountry.Text = string.Empty;
            txtNewEmail.Text = string.Empty;
        }

        #region
        //05/06/2025 (Jane Qin) Sorting by FirstName
        protected string SortColumn
        {
            get => ViewState["SortColumn"] as string ?? "FirstName";
            set => ViewState["SortColumn"] = value;
        }

        protected string SortDirection
        {
            get => ViewState["SortDirection"] as string ?? "ASC";
            set => ViewState["SortDirection"] = value;
        }

        protected string GetSortIcon(string column, string displayText)
        {
            if (SortColumn != column)
                return displayText;

            string arrow = SortDirection == "ASC" ? " ▲" : " ▼";
            return displayText + arrow;
        }

        protected void gridService_Sorting(object sender, GridViewSortEventArgs e)
        {
            if (SortColumn == e.SortExpression)
            {
                SortDirection = SortDirection == "ASC" ? "DESC" : "ASC";
            }
            else
            {
                SortColumn = e.SortExpression;
                SortDirection = "ASC";
            }

            LoadContactsWithPaging(gridService.PageIndex);
        }

        #endregion

    }
}