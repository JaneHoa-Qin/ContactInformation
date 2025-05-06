<%@ Page Title="Contact" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Contact.aspx.cs" Inherits="ContactInfo.Contact" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <asp:GridView ID="gridService" runat="server" AutoGenerateColumns="false" GridLines="Both"
        DataKeyNames="id"
        CssClass="styled-gridview"
        AllowPaging="True"
        AllowCustomPaging="True"
        EnableViewState="true"
        PageSize="2"
        OnPageIndexChanging="gridService_PageIndexChanging"
        OnRowEditing="gridService_RowEditing"
        OnRowUpdating="gridService_RowUpdating"
        OnRowDeleting="gridService_RowDeleting"
        OnRowCancelingEdit="gridService_RowCancelingEdit">
        <Columns>
            <%-- <asp:BoundField DataField="id" HeaderText="ID" ReadOnly="true" />--%>
            <asp:TemplateField HeaderText="First Name">               
                <ItemTemplate>
                    <%# Eval("FirstName") %>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="txtFirstName" runat="server" Text='<%# Bind("FirstName") %>' />
                </EditItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Last Name">
                <ItemTemplate>
                    <%# Eval("LastName") %>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="txtLastName" runat="server" Text='<%# Bind("LastName") %>' />
                </EditItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Address1">
                <ItemTemplate>
                    <%# Eval("Address1") %>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="txtAddress1" runat="server" Text='<%# Bind("Address1") %>' />
                </EditItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Address2">
                <ItemTemplate>
                    <%# Eval("Address2") %>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="txtAddress2" runat="server" Text='<%# Bind("Address2") %>' />
                </EditItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="State">
                <ItemTemplate>
                    <%# Eval("State") %>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="txtState" runat="server" Text='<%# Bind("State") %>' />
                </EditItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Country">
                <ItemTemplate>
                    <%# Eval("Country") %>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="txtCountry" runat="server" Text='<%# Bind("Country") %>' />
                </EditItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Email">
                <ItemTemplate>
                    <%# Eval("Email") %>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="txtEmail" runat="server" Text='<%# Bind("Email") %>' />
                </EditItemTemplate>
            </asp:TemplateField>
            <asp:CommandField ShowEditButton="true" ShowDeleteButton="true" />
        </Columns>
    </asp:GridView>
    <hr />

    <h8>Add New Contact</h8>
    <div class="add-section">
        <asp:TextBox ID="txtNewFirstName" runat="server" Placeholder="First Name" CssClass="add-input" />
        <asp:TextBox ID="txtNewLastName" runat="server" Placeholder="Last Name" CssClass="add-input" />
        <asp:TextBox ID="txtNewAddress1" runat="server" Placeholder="Address1" CssClass="add-input" />
        <asp:TextBox ID="txtNewAddress2" runat="server" Placeholder="Address2" CssClass="add-input" />
        <asp:TextBox ID="txtNewState" runat="server" Placeholder="State" CssClass="add-input" />
        <asp:TextBox ID="txtNewCountry" runat="server" Placeholder="Country" CssClass="add-input" />
        <asp:TextBox ID="txtNewEmail" runat="server" Placeholder="Email" CssClass="add-input" />
        <asp:Button ID="btnAddNew" runat="server" Text="Add Contact" OnClick="btnAddNew_Click" CssClass="add-button" />
    </div>
</asp:Content>
