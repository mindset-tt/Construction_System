using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Construction_System
{
    public partial class MType : Form
    {
        private string _empId;
        public MType(string empId)
        {
            InitializeComponent();
            _empId = empId;
        }

        private void sumQty()
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

        private void dataGridView1_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            dataGridView1.ClearSelection();
        }

        private void dataGridView1_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            dataGridView1.Rows[e.RowIndex].Cells["Column0"].Value = (e.RowIndex + 1).ToString();
            dataGridView1.ClearSelection();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //try
            //{
            //    var senderGrid = (DataGridView)sender;

            //    if (senderGrid.Columns[e.ColumnIndex] is DataGridViewImageColumn &&
            //    e.RowIndex >= 0)
            //    {
            //        //TODO - Button Clicked - Execute Code Here
            //        dataGridView1.Rows.RemoveAt(dataGridView1.Rows[e.RowIndex].Index);
            //        sumQty();
            //        MyMessageBox.ShowMessage("ລົບຂໍ້ມູນສຳເລັດແລ້ວ", "", "ສຳເລັດ", MessageBoxButtons.OK, MessageBoxIcon.None);
            //    }
            //}
            //catch (Exception ex)
            //{

            //    MyMessageBox.ShowMessage("ເກີດຂໍ້ຜີດພາດ " + ex + " ", "", "ເກີດຂໍ້ຜີດພາດ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                (dataGridView1.DataSource as DataTable).DefaultView.RowFilter = string.Format("typeName LIKE '%{0}%'", textBox1.Text);
                sumQty();
            }
            catch (Exception ex)
            {

                MyMessageBox.ShowMessage("ເກີດຂໍ້ຜີດພາດ " + ex + " ", "", "ເກີດຂໍ້ຜີດພາດ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        bool Checkcellclick = false;
        private readonly config _config = new config();
        string query = "";
        private void LoadProducts(string filter = "")
        {
            Checkcellclick = false;
            query = $"SELECT [typeId], [typeName] FROM type ORDER BY [typeName] ASC" + filter;
            _config.LoadData(query, dataGridView1);
            if (dataGridView1.RowCount <= 0)
            {
                MyMessageBox.ShowMessage("ຂໍອະໄພ, ຍັງບໍ່ມີຂໍ້ມູນໃດໆ. ກະລຸນາເພີ່ມຂໍ້ມູນ", "", "ແຈ້ງເຕືອນ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            textBox1.Text = "";
            textBox2.Text = "";
            sumQty();
        }

        private void MType_Load(object sender, EventArgs e)
        {
            LoadProducts();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox2.Text == "")
            {
                MyMessageBox.ShowMessage("ຂໍອະໄພ, ກະລຸນາປ້ອນຂໍ້ມູນໃຫ້ຄົບຖ້ວນ", "", "ຜິດພາດ", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            else
            {
                query = $"INSERT INTO [POSSALE].[dbo].[type] ([typeName]) " +
                    $"VALUES ('{textBox2.Text}')";
                _config.setData(query);
                MyMessageBox.ShowMessage("ເພິ່ມຂໍ້ມູນສຳເລັດແລ້ວ", "", "ສຳເລັດ", MessageBoxButtons.OK, MessageBoxIcon.None);
                LoadProducts();
                textBox2.Select();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox2.Text == "")
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
                    query = $"UPDATE [POSSALE].[dbo].[type] SET typeName = '{textBox2.Text}' WHERE typeId = {dataGridView1.CurrentRow.Cells["id1"].Value}";
                    _config.setData(query);
                    LoadProducts();
                    MyMessageBox.ShowMessage("ແກ້ໄຂຂໍ້ມູນສຳເລັດແລ້ວ", "", "ສຳເລັດ", MessageBoxButtons.OK, MessageBoxIcon.None);
                }
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            Checkcellclick = true;
            textBox2.Text = dataGridView1.CurrentRow.Cells["Column2"].Value.ToString();
        }
    }
}
