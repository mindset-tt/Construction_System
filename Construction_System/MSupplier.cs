using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Construction_System
{
    public partial class MSupplier : Form
    {
        private string _empId;
        public MSupplier(string empId)
        {
            InitializeComponent();
            _empId = empId;
        }

        bool Checkcellclick = false;
        private readonly config _config = new config();
        string query = "";
        private void LoadProducts(string filter = "")
        {
            Checkcellclick = false;
            query = $"SELECT [supplierId], [supplierName], [supplierAddress], [supplierTel] FROM supplier ORDER BY [supplierId] ASC" + filter;
            _config.LoadData(query, dataGridView1);
            if (dataGridView1.RowCount <= 0)
            {
                MyMessageBox.ShowMessage("ຂໍອະໄພ, ຍັງບໍ່ມີຂໍ້ມູນໃດໆ. ກະລຸນາເພີ່ມຂໍ້ມູນ", "", "ແຈ້ງເຕືອນ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            sumQty();
        }

        private void MSupplier_Load(object sender, EventArgs e)
        {
            LoadProducts();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                (dataGridView1.DataSource as DataTable).DefaultView.RowFilter = string.Format("supplierName LIKE '%{0}%' " +
                    "or supplierTel LIKE '%{0}%' or supplierId LIKE '%{0}%'", textBox1.Text);
                sumQty();
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
            //dataGridView1.Columns["Column3"].DefaultCellStyle.Format = "#,###";
            dataGridView1.ClearSelection();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                MSupAddProduct editAddProduct = new MSupAddProduct(this);
                var senderGrid = (DataGridView)sender;

                //if (senderGrid.Columns[e.ColumnIndex] is DataGridViewImageColumn &&
                //e.RowIndex >= 0 && dataGridView1.Columns[e.ColumnIndex].HeaderCell.Value.ToString() == "ລົບ")
                //{
                //    //TODO - Button Clicked - Execute Code Here
                //    dataGridView1.Rows.RemoveAt(dataGridView1.Rows[e.RowIndex].Index);
                //    sumQty();
                //    MyMessageBox.ShowMessage("ລົບຂໍ້ມູນສຳເລັດແລ້ວ", "", "ສຳເລັດ", MessageBoxButtons.OK, MessageBoxIcon.None);
                //}

                if (senderGrid.Columns[e.ColumnIndex] is DataGridViewImageColumn &&
                    e.RowIndex >= 0 && dataGridView1.Columns[e.ColumnIndex].HeaderCell.Value.ToString() == "ເພີ່ມສິນຄ້າ")
                {
                    //TODO - Button Clicked - Execute Code Here
                    //MyMessageBox.ShowMessage(dataGridView2.Rows[e.RowIndex].Cells[3].Value.ToString(), "", "ຍິນດີຕ້ອນຮັບ", MessageBoxButtons.OK, MessageBoxIcon.None);
                    //editQty.label1.Text = "ແກ້ໄຂຈຳນວນຂາຍສິນຄ້າ";
                    //editQty.label1.Image = Construction_System.Properties.Resources.pencil;
                    //editQty.button1.Text = "ແກ້ໄຂ";
                    //editQty.textBox1.Text = dataGridView2.Rows[e.RowIndex].Cells["Column23"].Value.ToString();
                    //editQty.textBox2.Text = dataGridView2.Rows[e.RowIndex].Cells["Column24"].Value.ToString();
                    //editQty.lblQtyEdit.Text = dataGridView2.Rows[e.RowIndex].Cells["Column24"].Value.ToString();
                    //editQty.lblPrice.Text = dataGridView2.Rows[e.RowIndex].Cells["Column26"].Value.ToString();
                    ////editQty.lblId.Text = dataGridView2.Rows[e.RowIndex].Cells["id2"].Value.ToString();
                    editAddProduct.ShowDialog();
                }
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

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox2.Text == "" || textBox3.Text == "" || textBox4.Text == "")
            {
                MyMessageBox.ShowMessage("ຂໍອະໄພ, ກະລຸນາປ້ອນຂໍ້ມູນໃຫ້ຄົບຖ້ວນ", "", "ຜິດພາດ", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            else
            {
                var query = "SELECT TOP 1 supplierId FROM [POSSALE].[dbo].[supplier] ORDER BY supplierId DESC";
                var dr = _config.getData(query);
                dr.Read();

                // If order id is null, set it to OD0001
                var supplierId = dr.HasRows ? $"SP{long.Parse(dr["supplierId"].ToString().Substring(2)) + 1:D3}" : "SP001";
                dr.Close();
                _config.closeConnection();

                _config.setData("INSERT INTO [POSSALE].[dbo].[supplier] ([supplierId], [supplierName], [supplierAddress], [supplierTel]) " +
                          $"VALUES ('{supplierId}','{textBox2.Text}','{textBox4.Text}', '{textBox3.Text}')");
                _config.setData(query);
                MyMessageBox.ShowMessage("ເພິ່ມຂໍ້ມູນສຳເລັດແລ້ວ", "", "ສຳເລັດ", MessageBoxButtons.OK, MessageBoxIcon.None);
                LoadProducts();
                textBox2.Select();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox2.Text == "" || textBox3.Text == "" || textBox4.Text == "")
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
                    query = $"UPDATE [POSSALE].[dbo].[supplier] SET supplierName = '{textBox2.Text}',supplierTel = '{textBox3.Text}', " +
                        $"supplierAddress = '{textBox4.Text}' WHERE supplierId = '{dataGridView1.CurrentRow.Cells["id1"].Value}'";
                    _config.setData(query);
                    LoadProducts();
                    MyMessageBox.ShowMessage("ແກ້ໄຂຂໍ້ມູນສຳເລັດແລ້ວ", "", "ສຳເລັດ", MessageBoxButtons.OK, MessageBoxIcon.None);
                }
            }
        }

        public string supplierIdAddPro;
        public string spName;
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            Checkcellclick = true;
            spName = dataGridView1.CurrentRow.Cells["Column2"].Value.ToString();
            supplierIdAddPro = dataGridView1.CurrentRow.Cells["id1"].Value.ToString();
            textBox2.Text = dataGridView1.CurrentRow.Cells["Column2"].Value.ToString();
            textBox3.Text = dataGridView1.CurrentRow.Cells["Column3"].Value.ToString();
            textBox4.Text = dataGridView1.CurrentRow.Cells["Column4"].Value.ToString();
        }
    }
}
