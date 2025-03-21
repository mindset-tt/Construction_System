using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Construction_System
{
    public partial class MProduct : Form
    {
        private string _empId;
        public MProduct(string empId)
        {
            InitializeComponent();
            _empId = empId;
        }

        public void sumQty()
        {
            try
            {
                if (dataGridView1.Rows.Count != 0)
                {
                    label4.Text = dataGridView1.RowCount.ToString("#,###") + "    ລາຍການ";
                }
                else
                {
                    label4.Text = "0" + "    ລາຍການ";
                }
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowMessage("ເກີດຂໍ້ຜີດພາດ " + ex + " ", "", "ເກີດຂໍ້ຜີດພາດ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public bool Checkcellclick = false;
        private readonly config _config = new config();
        string query = "";

        private void LoadProducts(string filter = "WHERE p.[cancel] = 'no' ")
        {
            query = $"SELECT p.[prodID],p.[prodName], p.[prodQty], u.[unitName], t.[typeName], p.[prodPrice], p.[prodSellPrice]" +
                "FROM [POSSALE].[dbo].[product] p " +
                "INNER JOIN [POSSALE].[dbo].[unit] u ON p.unitId = u.unitId " +
                "INNER JOIN [POSSALE].[dbo].[type] t ON p.typeId = t.typeId " +
                filter + "ORDER BY p.[prodQty] ASC";
            _config.LoadData(query, dataGridView1);
            if (dataGridView1.RowCount <= 0)
            {
                MyMessageBox.ShowMessage("ຂໍອະໄພ, ຍັງບໍ່ມີຂໍ້ມູນໃດໆ. ກະລຸນາເພີ່ມຂໍ້ມູນ", "", "ແຈ້ງເຕືອນ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox5.Text = "";
            textBox4.Text = "";
            //comboBox1.Text = "ກະລຸນາເລືອກ";
            //comboBox2.Text = "ກະລຸນາເລືອກ";
            LoadUnit();
            LoadType();
            sumQty();
        }

        private void LoadUnit()
        {
            var query = "SELECT [unitId], [unitName] FROM [POSSALE].[dbo].[unit] ORDER BY [unitName] ASC";
            _config.LoadData(query, comboBox1, "unitId", "unitName", "ກະລຸນາເລືອກ *");
        }

        private void LoadType()
        {
            var query = "SELECT [typeId], [typeName] FROM [POSSALE].[dbo].[type] ORDER BY [typeName] ASC";
            _config.LoadData(query, comboBox2, "typeId", "typeName", "ກະລຸນາເລືອກ *");
        }

        private void MProduct_Load(object sender, EventArgs e)
        {
            LoadProducts();
        }

        private void dataGridView1_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            dataGridView1.ClearSelection();
        }

        private void dataGridView1_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            dataGridView1.Columns["Column4"].DefaultCellStyle.Format = "#,###";
            dataGridView1.Columns["Column7"].DefaultCellStyle.Format = "#,###";
            dataGridView1.Columns["Column8"].DefaultCellStyle.Format = "#,###";
            dataGridView1.ClearSelection();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox2.Text == "" || textBox3.Text == "" || textBox4.Text == "" || 
                textBox5.Text == "" || comboBox1.Text == "ກະລຸນາເລືອກ *" || comboBox2.Text == "ກະລຸນາເລືອກ *")
            {
                MyMessageBox.ShowMessage("ຂໍອະໄພ, ກະລຸນາປ້ອນຂໍ້ມູນໃຫ້ຄົບຖ້ວນ", "", "ຜິດພາດ", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            else
            {
                var query = "SELECT TOP 1 prodID FROM [POSSALE].[dbo].[product] ORDER BY prodID DESC";
                var dr = _config.getData(query);
                dr.Read();

                // If order id is null, set it to OD0001
                var prodID = dr.HasRows ? $"P{long.Parse(dr["prodID"].ToString().Substring(1)) + 1:D5}" : "P00001";
                dr.Close();
                _config.closeConnection();

                _config.setData("INSERT INTO [POSSALE].[dbo].[product] ([prodID], [prodName], [prodQty], " +
                    "[unitId], [typeId],[prodPrice],[prodSellPrice], [cancel]) " +
                    $"VALUES ('{prodID}','{textBox2.Text}','{textBox3.Text}', '{comboBox1.SelectedValue}', '{comboBox2.SelectedValue}', " +
                    $"'{textBox4.Text}', '{textBox5.Text}', 'no')");
                _config.setData(query);
                LoadProducts();
                MyMessageBox.ShowMessage("ເພິ່ມຂໍ້ມູນສຳເລັດແລ້ວ", "", "ສຳເລັດ", MessageBoxButtons.OK, MessageBoxIcon.None);
                textBox2.Select();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox2.Text == "" || textBox3.Text == "" || textBox4.Text == "" ||
                textBox5.Text == "" || comboBox1.Text == "ກະລຸນາເລືອກ *" || comboBox2.Text == "ກະລຸນາເລືອກ *")
            {
                MyMessageBox.ShowMessage("ຂໍອະໄພ, ກະລຸນາປ້ອນຂໍ້ມູນໃຫ້ຄົບຖ້ວນ", "", "ຜິດພາດ", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            else if (Checkcellclick == false)
            {
                MyMessageBox.ShowMessage("ຂໍອະໄພ, ກະລຸນາເລຶອກຂໍ້ມູນທີ່ຕ້ອງການແກ້ໄຂ", "", "ຜິດພາດ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                DialogResult dialog = MyMessageBox.ShowMessage("ທ່ານຕ້ອງການແກ້ໄຂຂໍ້ມູນ ແທ້ ຫຼື ບໍ່", "", "ກວດສອບ", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialog == DialogResult.Yes)
                {
                    query = $"UPDATE [POSSALE].[dbo].[product] SET prodName = '{textBox2.Text}',prodQty = '{textBox3.Text}', " +
                        $"unitId = '{comboBox1.SelectedValue}', typeId = '{comboBox2.SelectedValue}', prodPrice = '{textBox4.Text}', " +
                        $"prodSellPrice = '{textBox5.Text}' WHERE prodID = '{dataGridView1.CurrentRow.Cells["id1"].Value}'";
                    _config.setData(query);
                    LoadProducts();
                    MyMessageBox.ShowMessage("ແກ້ໄຂຂໍ້ມູນສຳເລັດແລ້ວ", "", "ສຳເລັດ", MessageBoxButtons.OK, MessageBoxIcon.None);
                }
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var senderGrid = (DataGridView)sender;
            if (senderGrid.Columns[e.ColumnIndex] is DataGridViewImageColumn &&
            e.RowIndex >= 0 && dataGridView1.Columns[e.ColumnIndex].HeaderCell.Value.ToString() == "ລົບ")
            {
                //TODO - Button Clicked - Execute Code Here
                DialogResult dialog = MyMessageBox.ShowMessage("ທ່ານຕ້ອງການລົບຂໍ້ມູນ ແທ້ ຫຼື ບໍ່", "", "ກວດສອບ", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialog == DialogResult.Yes)
                {
                    query = $"UPDATE [POSSALE].[dbo].[product] SET [cancel] = 'yes'" +
                                $" WHERE [prodID] = '{dataGridView1.CurrentRow.Cells["id1"].Value}'";
                    _config.setData(query);
                    LoadProducts();
                    MyMessageBox.ShowMessage("ລົບຂໍ້ມູນສຳເລັດແລ້ວ", "", "ສຳເລັດ", MessageBoxButtons.OK, MessageBoxIcon.None);
                }
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            Checkcellclick = true;
            textBox2.Text = dataGridView1.CurrentRow.Cells["Column3"].Value.ToString();
            textBox3.Text = dataGridView1.CurrentRow.Cells["Column4"].Value.ToString();
            comboBox1.Text = dataGridView1.CurrentRow.Cells["Column5"].Value.ToString();
            comboBox2.Text = dataGridView1.CurrentRow.Cells["Column6"].Value.ToString();
            textBox4.Text = dataGridView1.CurrentRow.Cells["Column7"].Value.ToString();
            textBox5.Text = dataGridView1.CurrentRow.Cells["Column8"].Value.ToString();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                (dataGridView1.DataSource as DataTable).DefaultView.RowFilter = string.Format("prodName LIKE '%{0}%' " +
                    "or typeName LIKE '%{0}%' or unitName LIKE '%{0}%'", textBox1.Text);
                sumQty();
            }
            catch (Exception ex)
            {

                MyMessageBox.ShowMessage("ເກີດຂໍ້ຜີດພາດ " + ex + " ", "", "ເກີດຂໍ້ຜີດພາດ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void textBox5_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
}
