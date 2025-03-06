using System.Data.SqlClient;
using System.Data;
using System.Windows.Forms;
using Construction_System;
using System.Configuration;

class config
{
    public SqlConnection con = new SqlConnection();
    SqlCommand cmd = new SqlCommand();
    SqlDataReader dr;


    public config()
    {
        // Initialize connection string or other settings here
        con.ConnectionString = ConfigurationManager.ConnectionStrings["Construction_System.Properties.Settings.POSSALEConnectionString"].ConnectionString;
    }

    public void openConnection()
    {
        if (con.State == ConnectionState.Closed)
        {
            con.Open();
        }
    }

    public void closeConnection()
    {
        if (con.State == ConnectionState.Open)
        {
            con.Close();
        }
    }
    private void ShowMessage(string message, string content)
    {
        MyMessageBox.ShowMessage(message, "", content, MessageBoxButtons.OK, MessageBoxIcon.None);
    }
    public SqlDataReader getData(string sql)
    {
        openConnection();
        cmd = new SqlCommand(sql, con);
        return cmd.ExecuteReader();
    }

    public void setData(string sql)
    {
        openConnection();
        cmd = new SqlCommand(sql, con);
        int result = cmd.ExecuteNonQuery(); // Execute the command once and store the result

        // Check if the query is successful or not, if not, show a message box with the error message and rollback the transaction
        if (result == 0)
        {
            ShowMessage("ບໍ່ສາມາດບັນທຶກຂໍ້ມູນໄດ້", "ຂໍ້ມູນບໍ່ຖືກບັນທຶກ");
            cmd.Transaction.Rollback();
        }
        closeConnection();
    }


    public void LoadData(string query, ComboBox comboBox, string valueMember, string displayMember, string defaultText = null)
    {
        openConnection();
        SqlDataReader dr = getData(query);
        DataTable dt = new DataTable();
        if (dr.HasRows)
        {
            dt.Load(dr);
            if (!string.IsNullOrEmpty(defaultText))
            {
                DataRow row = dt.NewRow();
                row[valueMember] = -1;
                row[displayMember] = defaultText;
                dt.Rows.InsertAt(row, 0);
            }
            comboBox.DataSource = dt;
            comboBox.ValueMember = valueMember;
            comboBox.DisplayMember = displayMember;
            comboBox.SelectedIndex = 0;
        }
        closeConnection();
    }

    public void LoadData(string query, DataGridView dataGridView)
    {
        openConnection();
        SqlDataReader dr = getData(query);
        DataTable dt = new DataTable();
        if (dr.HasRows)
        {
            dt.Load(dr);
            dataGridView.DataSource = dt;
        }
        else
        {
            //clear the data grid view has one row with no data
            dt.Rows.Clear();
            dataGridView.DataSource = dt;
        }
        closeConnection();
    }
}
