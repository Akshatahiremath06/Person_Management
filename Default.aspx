<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master"
AutoEventWireup="true" CodeBehind="Default.aspx.cs"
Inherits="PersonManag._Default" %>

<asp:Content ID="BodyContent"
    ContentPlaceHolderID="MainContent"
    runat="server">

<h2>Person Registration Form</h2>

<asp:HiddenField ID="hfPersonId" runat="server" />

Name:
<asp:TextBox ID="txtName" runat="server"></asp:TextBox>
<br /><br />

Address:
<asp:TextBox ID="txtAddress" runat="server"></asp:TextBox>
<br /><br />

Gender:
<asp:RadioButtonList ID="rblGender" runat="server">
    <asp:ListItem>Male</asp:ListItem>
    <asp:ListItem>Female</asp:ListItem>
</asp:RadioButtonList>
<br />

DOB:
<asp:TextBox ID="txtDOB" runat="server" TextMode="Date"></asp:TextBox>
<br /><br />

State:
<asp:DropDownList ID="ddlState"
    runat="server"
    AutoPostBack="true"
    OnSelectedIndexChanged="ddlState_SelectedIndexChanged">
</asp:DropDownList>
<br /><br />

District:
<asp:DropDownList ID="ddlDistrict" runat="server"
    AutoPostBack="true" OnSelectedIndexChanged="ddlState_SelectedIndexChanged">
</asp:DropDownList>
<br /><br />

Occupation:
<asp:DropDownList ID="ddlOccupation" runat="server"></asp:DropDownList>
<br /><br />

Languages:
<asp:CheckBoxList ID="cblLanguages" runat="server"></asp:CheckBoxList>
<br /><br />

Marital Status:
<asp:DropDownList ID="ddlMarital" runat="server">
    <asp:ListItem>Single</asp:ListItem>
    <asp:ListItem>Married</asp:ListItem>
</asp:DropDownList>
<br /><br />

Spouse Name:
<asp:TextBox ID="txtSpouse" runat="server"></asp:TextBox>
<br /><br />

Has License:
<asp:CheckBox ID="chkLicense" runat="server" />
<br /><br />

License Number:
<asp:TextBox ID="txtLicenseNo" runat="server"></asp:TextBox>
<br /><br />

Profile Photo:
<asp:FileUpload ID="fuPhoto" runat="server" />
<br /><br />

<asp:Button ID="btnSave"
    runat="server"
    Text="Save Person"
    OnClick="btnSave_Click" />

<br /><br />

<asp:GridView ID="gvPersons" runat="server"
    AutoGenerateColumns="False"
    DataKeyNames="PersonId"
    OnRowEditing="gvPersons_RowEditing"
    OnRowUpdating="gvPersons_RowUpdating"
    OnRowDeleting="gvPersons_RowDeleting">

    <Columns>
        <asp:BoundField DataField="PersonId" HeaderText="ID" ReadOnly="True" />
        <asp:BoundField DataField="Name" HeaderText="Name" />
        <asp:BoundField DataField="Address" HeaderText="Address" />
        <asp:BoundField DataField="StateName" HeaderText="State" />
        <asp:BoundField DataField="DistrictName" HeaderText="District" />
        <asp:BoundField DataField="OccupationName" HeaderText="Occupation" />
        <asp:CommandField ShowEditButton="True" ShowDeleteButton="True" />
    </Columns>

</asp:GridView>

</asp:Content>

