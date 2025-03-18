using System.Data.SqlClient;
using System.Data;
using System;
using System.Windows.Forms;
using Construction_System;
using System.Configuration;

class config
{
    public SqlConnection con = new SqlConnection();
    SqlCommand cmd = new SqlCommand();

    public config()
    {
        try
        {
            // Initialize connection string or other settings here
            con.ConnectionString = ConfigurationManager.ConnectionStrings["Construction_System.Properties.Settings.POSSALEConnectionString"].ConnectionString;
        }
        catch (Exception ex)
        {
            //ShowMessage($"Error initializing connection: {ex.Message}", "Error");
            MyMessageBox.ShowMessage($"Error initializing connection: {ex}", "", "ເກີດຂໍ້ຜີດພາດ", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    public void openConnection()
    {
        try
        {
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
        }
        catch (Exception ex)
        {
            //ShowMessage($"Error opening connection: {ex.Message}", "Error");
            MyMessageBox.ShowMessage($"Error opening connection: {ex}", "", "ເກີດຂໍ້ຜີດພາດ", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    public void closeConnection()
    {
        try
        {
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
        }
        catch (Exception ex)
        {
            //ShowMessage($"Error closing connection: {ex.Message}", "Error");
            MyMessageBox.ShowMessage($"Error closing connection: {ex}", "", "ເກີດຂໍ້ຜີດພາດ", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void ShowMessage(string message, string content)
    {
        MyMessageBox.ShowMessage(message, "", content, MessageBoxButtons.OK, MessageBoxIcon.None);
    }

    public SqlDataReader getData(string sql)
    {
        try
        {
            openConnection();
            cmd = new SqlCommand(sql, con);
            return cmd.ExecuteReader();
        }
        catch (Exception ex)
        {
            //ShowMessage($"Error getting data: {ex.Message}", "Error");
            MyMessageBox.ShowMessage($"Error getting data: {ex}", "", "ເກີດຂໍ້ຜີດພາດ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return null;
        }
    }

    public void setData(string sql)
    {
        try
        {
            openConnection();
            cmd = new SqlCommand(sql, con);
            long result = cmd.ExecuteNonQuery(); // Execute the command once and store the result

            //// Check if the query is successful or not, if not, show a message box with the error message and rollback the transaction
            //if (result == 0)
            //{
            //    ShowMessage("ບໍ່ສາມາດບັນທຶກຂໍ້ມູນໄດ້", "ຂໍ້ມູນບໍ່ຖືກບັນທຶກ");
            //    //cmd.Transaction.Rollback();
            //}
        }
        catch (Exception ex)
        {
            //ShowMessage($"Error setting data: {ex.Message}", "Error");
            MyMessageBox.ShowMessage($"Error setting data: {ex}", "", "ເກີດຂໍ້ຜີດພາດ", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        finally
        {
            closeConnection();
        }
    }

    public void LoadData(string query, ComboBox comboBox, string valueMember, string displayMember, string defaultText = null)
    {
        //try
        //{
            openConnection();
            SqlDataAdapter adapter = new SqlDataAdapter(query, con);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
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
        closeConnection();
        //}
        //catch (Exception ex)
        //{
        //    //ShowMessage($"Error loading data: {ex.Message}", "Error");
        //    MyMessageBox.ShowMessage($"Error loading data: {ex}", "", "ເກີດຂໍ້ຜີດພາດ", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //}
        //finally
        //{
        //    closeConnection();
        //}
    }

    public void LoadData(string query, DataGridView dataGridView)
    {
        //try
        //{
            openConnection();
            SqlDataAdapter adapter = new SqlDataAdapter(query, con);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            dataGridView.DataSource = dt;
        closeConnection();
        //}
        //catch (Exception ex)
        //{
        //    //ShowMessage($"Error loading data: {ex.Message}", "Error");
        //    MyMessageBox.ShowMessage($"Error loading data: {ex}", "", "ເກີດຂໍ້ຜີດພາດ", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //}
        //finally
        //{
        //    closeConnection();
        //}
    }

    public void LoadDataToDataTable(string query, DataTable dt)
    {
        try
        {
            openConnection();
            SqlDataAdapter adapter = new SqlDataAdapter(query, con);
            adapter.Fill(dt);
        }
        catch (Exception ex)
        {
            //ShowMessage($"Error loading data: {ex.Message}", "Error");
            MyMessageBox.ShowMessage($"Error loading data: {ex}", "", "ເກີດຂໍ້ຜີດພາດ", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        finally
        {
            closeConnection();
        }
    }


    //Yai Create
    public void deleteData(string sql)
    {
        try
        {
            openConnection();
            cmd = new SqlCommand(sql, con);
            long result = cmd.ExecuteNonQuery(); // Execute the command once and store the result

            // Check if the query is successful or not, if not, show a message box with the error message and rollback the transaction
            if (result == 0)
            {
                ShowMessage("ບໍ່ສາມາດລົບຂໍ້ມູນໄດ້", "ຂໍ້ມູນບໍ່ຖືກບັນທຶກ");
                cmd.Transaction.Rollback();
            }
        }
        catch (Exception ex)
        {
            //ShowMessage($"Error setting data: {ex.Message}", "Error");
            MyMessageBox.ShowMessage($"Error setting data: {ex}", "", "ເກີດຂໍ້ຜີດພາດ", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        finally
        {
            closeConnection();
        }
    }

    public void setDataEmp(string sql)
    {
        try
        {
            openConnection();
            cmd = new SqlCommand(sql, con);
            long result = cmd.ExecuteNonQuery(); // Execute the command once and store the result
            MyMessageBox.ShowMessage("ເພິ່ມຂໍ້ມູນສຳເລັດແລ້ວ", "", "ສຳເລັດ", MessageBoxButtons.OK, MessageBoxIcon.None);
            // Check if the query is successful or not, if not, show a message box with the error message and rollback the transaction
            if (result == 0)
            {
                ShowMessage("ບໍ່ສາມາດບັນທຶກຂໍ້ມູນໄດ້", "ຂໍ້ມູນບໍ່ຖືກບັນທຶກ");
                cmd.Transaction.Rollback();
            }
        }
        catch
        {
            //ShowMessage($"Error setting data: {ex.Message}", "Error");
            MyMessageBox.ShowMessage($"ຂໍອະໄພ,ຊື່ຜູ້ໃຊ້ນີ້ມີຢູ່ແລ້ວ ບໍ່ສາມາດບັນທຶກຂໍ້ມູນໄດ້", "", "ເກີດຂໍ້ຜີດພາດ", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        finally
        {
            closeConnection();
        }
    }

    public void updateDataEmp(string sql)
    {
        try
        {
            openConnection();
            cmd = new SqlCommand(sql, con);
            long result = cmd.ExecuteNonQuery(); // Execute the command once and store the result
            MyMessageBox.ShowMessage("ແກ້ໄຂຂໍ້ມູນສຳເລັດແລ້ວ", "", "ສຳເລັດ", MessageBoxButtons.OK, MessageBoxIcon.None);

            // Check if the query is successful or not, if not, show a message box with the error message and rollback the transaction
            if (result == 0)
            {
                ShowMessage("ບໍ່ສາມາດບັນທຶກຂໍ້ມູນໄດ້", "ຂໍ້ມູນບໍ່ຖືກບັນທຶກ");
                cmd.Transaction.Rollback();
            }
        }
        catch
        {
            //ShowMessage($"Error setting data: {ex.Message}", "Error");
            MyMessageBox.ShowMessage($"ຂໍອະໄພ,ຊື່ຜູ້ໃຊ້ນີ້ມີຢູ່ແລ້ວ ບໍ່ສາມາດບັນທຶກຂໍ້ມູນໄດ້", "", "ເກີດຂໍ້ຜີດພາດ", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        finally
        {
            closeConnection();
        }
    }
}
