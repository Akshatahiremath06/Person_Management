using MySql.Data.MySqlClient;
using System;
using System.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

namespace PersonManag
{
    public partial class _Default : Page
    {
        string conStr = ConfigurationManager.ConnectionStrings["MySqlConn"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadStates();
                LoadOccupations();
                LoadLanguages();
                LoadPersons();
            }
        }

        void LoadStates()
        {
            using (MySqlConnection con = new MySqlConnection(conStr))
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT StateId, StateName FROM States", con);
                ddlState.DataSource = cmd.ExecuteReader();
                ddlState.DataTextField = "StateName";
                ddlState.DataValueField = "StateId";
                ddlState.DataBind();
                ddlState.Items.Insert(0, new ListItem("Select State", "0"));
            }
        }

        void LoadDistricts(int stateId)
        {

            ddlDistrict.Items.Clear();

            using (MySqlConnection con = new MySqlConnection(conStr))
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT DistrictId, DistrictName FROM Districts WHERE StateId=@sid", con);
                cmd.Parameters.AddWithValue("@sid", stateId);
                ddlDistrict.DataSource = cmd.ExecuteReader();
                ddlDistrict.DataTextField = "DistrictName";
                ddlDistrict.DataValueField = "DistrictId";
                ddlDistrict.DataBind();
            }
        }

        void LoadOccupations()
        {
            using (MySqlConnection con = new MySqlConnection(conStr))
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT OccupationId, OccupationName FROM Occupations", con);
                ddlOccupation.DataSource = cmd.ExecuteReader();
                ddlOccupation.DataTextField = "OccupationName";
                ddlOccupation.DataValueField = "OccupationId";
                ddlOccupation.DataBind();
            }
        }

        void LoadLanguages()
        {
            using (MySqlConnection con = new MySqlConnection(conStr))
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT LanguageId, LanguageName FROM Languages", con);
                cblLanguages.DataSource = cmd.ExecuteReader();
                cblLanguages.DataTextField = "LanguageName";
                cblLanguages.DataValueField = "LanguageId";
                cblLanguages.DataBind();
            }
        }

        protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadDistricts(Convert.ToInt32(ddlState.SelectedValue));
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(hfPersonId.Value))
            {
                UpdatePerson();
            }
            else
            {
                InsertPerson();
            }
        }

        void InsertPerson()
        {
            int newId = 0;
            string photoPath = "";

            if (fuPhoto.HasFile)
            {
                string folder = Server.MapPath("~/Uploads/Profile/");
                if (!Directory.Exists(folder))
                    Directory.CreateDirectory(folder);

                photoPath = "~/Uploads/Profile/" + fuPhoto.FileName;
                fuPhoto.SaveAs(Server.MapPath(photoPath));
            }

            using (MySqlConnection con = new MySqlConnection(conStr))
            {
                con.Open();
                string query = @"INSERT INTO Persons
                (Name, Address, Gender, DOB, StateId, DistrictId, OccupationId,
                 MaritalStatus, SpouseName, HasLicense, LicenseNumber, ProfilePhoto)
                 VALUES
                (@Name,@Address,@Gender,@DOB,@StateId,@DistrictId,@OccupationId,
                 @MaritalStatus,@SpouseName,@HasLicense,@LicenseNumber,@ProfilePhoto);
                 SELECT LAST_INSERT_ID();";

                MySqlCommand cmd = new MySqlCommand(query, con);

                cmd.Parameters.AddWithValue("@Name", txtName.Text);
                cmd.Parameters.AddWithValue("@Address", txtAddress.Text);
                cmd.Parameters.AddWithValue("@Gender", rblGender.SelectedValue);
                cmd.Parameters.AddWithValue("@DOB", txtDOB.Text);
                cmd.Parameters.AddWithValue("@StateId", ddlState.SelectedValue);
                cmd.Parameters.AddWithValue("@DistrictId", ddlDistrict.SelectedValue);
                cmd.Parameters.AddWithValue("@OccupationId", ddlOccupation.SelectedValue);
                cmd.Parameters.AddWithValue("@MaritalStatus", ddlMarital.SelectedValue);
                cmd.Parameters.AddWithValue("@SpouseName", txtSpouse.Text);
                cmd.Parameters.AddWithValue("@HasLicense", chkLicense.Checked);
                cmd.Parameters.AddWithValue("@LicenseNumber", txtLicenseNo.Text);
                cmd.Parameters.AddWithValue("@ProfilePhoto", photoPath);

                newId = Convert.ToInt32(cmd.ExecuteScalar());
            }

            SaveLanguages(newId);
            LoadPersons();
        }

        void UpdatePerson()
        {
            using (MySqlConnection con = new MySqlConnection(conStr))
            {
                con.Open();
                string query = @"UPDATE Persons SET
                Name=@Name, Address=@Address, Gender=@Gender, DOB=@DOB,
                StateId=@StateId, DistrictId=@DistrictId, OccupationId=@OccupationId,
                MaritalStatus=@MaritalStatus, SpouseName=@SpouseName,
                HasLicense=@HasLicense, LicenseNumber=@LicenseNumber
                WHERE PersonId=@PersonId";

                MySqlCommand cmd = new MySqlCommand(query, con);

                cmd.Parameters.AddWithValue("@Name", txtName.Text);
                cmd.Parameters.AddWithValue("@Address", txtAddress.Text);
                cmd.Parameters.AddWithValue("@Gender", rblGender.SelectedValue);
                cmd.Parameters.AddWithValue("@DOB", txtDOB.Text);
                cmd.Parameters.AddWithValue("@StateId", ddlState.SelectedValue);
                cmd.Parameters.AddWithValue("@DistrictId", ddlDistrict.SelectedValue);
                cmd.Parameters.AddWithValue("@OccupationId", ddlOccupation.SelectedValue);
                cmd.Parameters.AddWithValue("@MaritalStatus", ddlMarital.SelectedValue);
                cmd.Parameters.AddWithValue("@SpouseName", txtSpouse.Text);
                cmd.Parameters.AddWithValue("@HasLicense", chkLicense.Checked);
                cmd.Parameters.AddWithValue("@LicenseNumber", txtLicenseNo.Text);
                cmd.Parameters.AddWithValue("@PersonId", hfPersonId.Value);

                cmd.ExecuteNonQuery();

                MySqlCommand del = new MySqlCommand("DELETE FROM PersonLanguages WHERE PersonId=@PersonId", con);
                del.Parameters.AddWithValue("@PersonId", hfPersonId.Value);
                del.ExecuteNonQuery();
            }

            SaveLanguages(Convert.ToInt32(hfPersonId.Value));
            hfPersonId.Value = "";
            btnSave.Text = "Save Person";
            LoadPersons();
        }

        void SaveLanguages(int personId)
        {
            using (MySqlConnection con = new MySqlConnection(conStr))
            {
                con.Open();
                foreach (ListItem item in cblLanguages.Items)
                {
                    if (item.Selected)
                    {
                        MySqlCommand cmd = new MySqlCommand(
                        "INSERT INTO PersonLanguages (PersonId, LanguageId) VALUES (@pid,@lid)", con);

                        cmd.Parameters.AddWithValue("@pid", personId);
                        cmd.Parameters.AddWithValue("@lid", item.Value);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }

        void LoadPersons()
        {
            using (MySqlConnection con = new MySqlConnection(conStr))
            {
                con.Open();
                string query = @"SELECT p.PersonId, p.Name, p.Address,
                s.StateName, d.DistrictName, o.OccupationName
                FROM Persons p
                JOIN States s ON p.StateId=s.StateId
                JOIN Districts d ON p.DistrictId=d.DistrictId
                JOIN Occupations o ON p.OccupationId=o.OccupationId";

                MySqlCommand cmd = new MySqlCommand(query, con);
                gvPersons.DataSource = cmd.ExecuteReader();
                gvPersons.DataBind();
            }
        }

        protected void gvPersons_RowEditing(object sender, GridViewEditEventArgs e)
        {
            int id = Convert.ToInt32(gvPersons.DataKeys[e.NewEditIndex].Value);
            hfPersonId.Value = id.ToString();
            btnSave.Text = "Update Person";
        }
        protected void gvPersons_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            int id = Convert.ToInt32(gvPersons.DataKeys[e.RowIndex].Value);

            GridViewRow row = gvPersons.Rows[e.RowIndex];
            string name = ((TextBox)row.Cells[1].Controls[0]).Text;
            string address = ((TextBox)row.Cells[2].Controls[0]).Text;
            // similarly get other fields...

            using (MySqlConnection con = new MySqlConnection(conStr))
            {
                con.Open();
                string query = @"UPDATE Persons SET Name=@Name, Address=@Address WHERE PersonId=@PersonId";
                MySqlCommand cmd = new MySqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Name", name);
                cmd.Parameters.AddWithValue("@Address", address);
                cmd.Parameters.AddWithValue("@PersonId", id);
                cmd.ExecuteNonQuery();
            }

            gvPersons.EditIndex = -1; // exit edit mode
            LoadPersons();
        }


        protected void gvPersons_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int id = Convert.ToInt32(gvPersons.DataKeys[e.RowIndex].Value);

            using (MySqlConnection con = new MySqlConnection(conStr))
            {
                con.Open();
                MySqlCommand cmd1 = new MySqlCommand("DELETE FROM PersonLanguages WHERE PersonId=@id", con);
                cmd1.Parameters.AddWithValue("@id", id);
                cmd1.ExecuteNonQuery();

                MySqlCommand cmd2 = new MySqlCommand("DELETE FROM Persons WHERE PersonId=@id", con);
                cmd2.Parameters.AddWithValue("@id", id);
                cmd2.ExecuteNonQuery();
            }

            LoadPersons();
        }
    }
}

