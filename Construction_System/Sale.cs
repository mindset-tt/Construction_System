using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Construction_System
{
    public partial class Sale : Form
    {
        private string _empId;
        public Sale(string empId)
        {
            InitializeComponent();
            label2.Text = "0 ກີບ";
            _empId = empId;
        }

        private void Sale_Load(object sender, EventArgs e)
        {
            try
            {
                //#Create Data Table
                //DataSet data = new DataSet("Contruction_System");
                //DataTable table = new DataTable("Sale2");
                //table.Columns.Add("id", typeof(int));
                //table.Columns.Add("name");
                //table.Columns.Add("qty");
                //table.Columns.Add("unit");
                //table.Columns.Add("price");
                //table.Rows.Add(1, "ຕະປູແປດ", "1", "ໂລ", "70,000");
                //table.Rows.Add(2, "ຕະປູຫ້າ", "2", "ໂລ", "70,000");
                //table.Rows.Add(3, "ຕະປູຫົກ", "3", "ໂລ", "105,000");
                //table.Rows.Add(4, "ກະເບື່ອງ", "10", "ແຜ່ນ", "560,000");
                //table.Rows.Add(5, "ຫຼົບກະເບື່ອງ", "4", "ແຜ່ນ", "240,000");
                //table.Rows.Add(6, "ຊັງກະສີແຜ່ນລຽບ", "12", "ແຜ່ນ", "680,000");
                //table.Rows.Add(7, "ຊັງກະສີແຜ່ນລ່ອງ", "20", "ແຜ່ນ", "1,320,000");
                //table.Rows.Add(8, "ສາຍໄຟ 2.5", "3", "ກໍ່", "850,000");
                //table.Rows.Add(9, "ຄ້ອນຕີໃຫຍ່", "1", "ອັນ", "120,000");
                //table.Rows.Add(10, "ຄ້ອນຕີນ້ອຍ", "2", "ອັນ", "140,000");
                //table.Rows.Add(11, "ຄ້ອນປອນ", "1", "ອັນ", "150,000");
                //table.Rows.Add(12, "ຕູ້ຈອດຫຼັ້ກ", "1", "ເຄື່ອງ", "570,000");
                //table.Rows.Add(52, "ໄຟບີຕັດເຫຼັກ", "2", "ເຄື່ອງ", "2,700,000");
                //table.Rows.Add(63, "ໝືນຕັດເຫຼັກ", "1", "ເຄື່ອງ", "350,000");
                //table.Rows.Add(72, "ສະຫວ່ານໄຟຟ້າ", "2", "ເຄື່ອງ", "2,620,000");
                //table.Rows.Add(85, "ສະຫວ່ານແບັກເຕີລີ້", "2", "ເຄື່ອງ", "1,250,000");
                //table.Rows.Add(85, "ສະຫວ່ານແບັກເຕີລີ້", "2", "ເຄື່ອງ", "1,250,000");
                //table.Rows.Add(85, "ສະຫວ່ານແບັກເຕີລີ້", "2", "ເຄື່ອງ", "1,250,000");
                //data.Tables.Add(table);
                ////dataGridView1.DataSource = table;
                //dataGridView2.DataSource = table;


                DataSet data1 = new DataSet("Contruction_System");
                DataTable table1 = new DataTable("Sale1");
                table1.Columns.Add("id", typeof(int));
                table1.Columns.Add("name");
                table1.Columns.Add("qty", typeof(int));
                table1.Columns.Add("unit");
                table1.Columns.Add("type");
                table1.Columns.Add("price", typeof(int));
                table1.Rows.Add(1, "ຕະປູແປດ", 1, "ໂລ", "ເຫຼັກ", 70000);
                table1.Rows.Add(2, "ຕະປູຫ້າ", 2, "ໂລ", "ເຫຼັກ", 70000);
                table1.Rows.Add(3, "ຕະປູຫົກ", 3, "ໂລ", "ເຫຼັກ", 105000);
                table1.Rows.Add(4, "ກະເບື່ອງ", 10, "ແຜ່ນ", "ຫຼັງຄາ", 560000);
                table1.Rows.Add(5, "ຫຼົບກະເບື່ອງ", 4, "ແຜ່ນ", "ຫຼັງຄາ", 240000);
                table1.Rows.Add(6, "ຊັງກະສີແຜ່ນລຽບ", 12, "ແຜ່ນ", "ຫຼັງຄາ", 680000);
                table1.Rows.Add(7, "ຊັງກະສີແຜ່ນລ່ອງ", 20, "ແຜ່ນ", "ຫຼັງຄາ", 1320000);
                table1.Rows.Add(8, "ສາຍໄຟ 2.5", 3, "ກໍ່", "ໄຟຟ້າ", 850000);
                table1.Rows.Add(9, "ຄ້ອນຕີໃຫຍ່", 1, "ອັນ", "ເຄື່ອງມື", 120000);
                table1.Rows.Add(10, "ຄ້ອນຕີນ້ອຍ", 2, "ອັນ", "ເຄື່ອງມື", 140000);
                table1.Rows.Add(11, "ຄ້ອນປອນ", 1, "ອັນ", "ເຄື່ອງມື", 150000);
                table1.Rows.Add(12, "ຕູ້ຈອດຫຼັກ", 1, "ເຄື່ອງ", "ໄຟຟ້າ", 570000);
                table1.Rows.Add(52, "ໄຟບີຕັດເຫຼັກ", 2, "ເຄື່ອງ", "ໄຟຟ້າ", 2700000);
                table1.Rows.Add(63, "ໝືນຕັດເຫຼັກ", 1, "ເຄື່ອງ", "ໄຟຟ້າ", 350000);
                table1.Rows.Add(72, "ສະຫວ່ານໄຟຟ້າ", 2, "ເຄື່ອງ", "ໄຟຟ້າ", 2620000);
                table1.Rows.Add(85, "ສະຫວ່ານແບັກເຕີລີ້", 2, "ເຄື່ອງ", "ໄຟຟ້າ", 1250000);
                table1.Rows.Add(85, "ສະຫວ່ານແບັກເຕີລີ້", 2, "ເຄື່ອງ", "ໄຟຟ້າ", 1250000);
                table1.Rows.Add(85, "ສະຫວ່ານແບັກເຕີລີ້", 2, "ເຄື່ອງ", "ໄຟຟ້າ", 1250000);
                data1.Tables.Add(table1);
                dataGridView1.DataSource = table1;
            }
            catch (Exception ex)
            {

                MyMessageBox.ShowMessage("ເກີດຂໍ້ຜີດພາດ " + ex + " ", "", "ເກີດຂໍ້ຜີດພາດ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridView2_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            dataGridView2.ClearSelection();
        }

        //SEditQty editQty = new SEditQty();
        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                SEditQty editQty = new SEditQty(this, null);
                var senderGrid = (DataGridView)sender;

                if (senderGrid.Columns[e.ColumnIndex] is DataGridViewImageColumn &&
                e.RowIndex >= 0 && dataGridView2.Columns[e.ColumnIndex].HeaderCell.Value.ToString() == "ລົບ")
                {
                    //TODO - Button Clicked - Execute Code Here
                    dataGridView2.Rows.RemoveAt(dataGridView2.Rows[e.RowIndex].Index);
                    sumQty();
                }

                if (senderGrid.Columns[e.ColumnIndex] is DataGridViewImageColumn &&
                    e.RowIndex >= 0 && dataGridView2.Columns[e.ColumnIndex].HeaderCell.Value.ToString() == "ແກ້ໄຂ")
                {
                    //TODO - Button Clicked - Execute Code Here
                    //MyMessageBox.ShowMessage(dataGridView2.Rows[e.RowIndex].Cells[3].Value.ToString(), "", "ຍິນດີຕ້ອນຮັບ", MessageBoxButtons.OK, MessageBoxIcon.None);
                    editQty.label1.Text = "ແກ້ໄຂຈຳນວນຂາຍສິນຄ້າ";
                    editQty.label1.Image = Construction_System.Properties.Resources.pencil;
                    editQty.button1.Text = "ແກ້ໄຂ";
                    editQty.textBox1.Text = dataGridView2.Rows[e.RowIndex].Cells["Column23"].Value.ToString();
                    editQty.textBox2.Text = dataGridView2.Rows[e.RowIndex].Cells["Column24"].Value.ToString();
                    editQty.lblQtyEdit.Text = dataGridView2.Rows[e.RowIndex].Cells["Column24"].Value.ToString();
                    editQty.lblPrice.Text = dataGridView2.Rows[e.RowIndex].Cells["Column26"].Value.ToString();
                    //editQty.lblId.Text = dataGridView2.Rows[e.RowIndex].Cells["id2"].Value.ToString();
                    editQty.ShowDialog();
                }
            }
            catch (Exception ex)
            {

                MyMessageBox.ShowMessage("ເກີດຂໍ້ຜີດພາດ " + ex + " ", "", "ເກີດຂໍ້ຜີດພາດ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void updateQty(int qty, int price)
        {
            DataGridViewRow rowUp = new DataGridViewRow();
            rowUp = dataGridView2.Rows[selectRowSale];
            rowUp.Cells["Column24"].Value = qty;
            rowUp.Cells["Column26"].Value = price;
        }

        public int selectRowSale;
        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            selectRowSale = e.RowIndex;
        }

        private void sumQty()
        {
            try
            {
                if (dataGridView2.Rows.Count >= -1)
                {
                    int totalPrice = 0;
                    for (int i = 0; i < dataGridView2.RowCount; i++)
                    {
                        totalPrice += Convert.ToInt32(dataGridView2.Rows[i].Cells["Column26"].Value.ToString());
                    }
                    label2.Text = totalPrice.ToString("#,###") + " ກີບ";
                }
                else
                {
                    label2.Text = "0" + " ກີບ";
                }
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowMessage("ເກີດຂໍ້ຜີດພາດ " + ex + " ", "", "ເກີດຂໍ້ຜີດພາດ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                (dataGridView1.DataSource as DataTable).DefaultView.RowFilter = string.Format("name LIKE '%{0}%' or unit LIKE '%{0}%' or type LIKE '%{0}%'", textBox1.Text);
            }
            catch (Exception ex)
            {

                MyMessageBox.ShowMessage("ເກີດຂໍ້ຜີດພາດ " + ex + " ", "", "ເກີດຂໍ້ຜີດພາດ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }


        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                SEditQty editQty = new SEditQty(this, null);
                var senderGrid1 = (DataGridView)sender;
                if (senderGrid1.Columns[e.ColumnIndex] is DataGridViewImageColumn &&
                    e.RowIndex >= 0)
                {
                    //TODO - Button Clicked - Execute Code Here
                    //MyMessageBox.ShowMessage(dataGridView2.Rows[e.RowIndex].Cells[3].Value.ToString(), "", "ຍິນດີຕ້ອນຮັບ", MessageBoxButtons.OK, MessageBoxIcon.None);
                    editQty.label1.Text = " ເພີ່ມຈຳນວນຂາຍສິນຄ້າ";
                    editQty.label1.Image = Construction_System.Properties.Resources.add_button;
                    editQty.button1.Text = "ເພີ່ມ";
                    editQty.textBox1.Text = dataGridView1.Rows[e.RowIndex].Cells["Column12"].Value.ToString();
                    editQty.lblUnit.Text = dataGridView1.Rows[e.RowIndex].Cells["Column14"].Value.ToString();
                    editQty.lblPrice.Text = dataGridView1.Rows[e.RowIndex].Cells["Column16"].Value.ToString();
                    editQty.lblId.Text = dataGridView1.Rows[e.RowIndex].Cells["id1"].Value.ToString();
                    editQty.ShowDialog();
                    dataGridView1.ClearSelection();
                    //dataGridView2.Columns["Column24"].DefaultCellStyle.Format = "#,###";
                    //dataGridView2.Columns["Column26"].DefaultCellStyle.Format = "#,###";
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
            dataGridView1.Columns["Column13"].DefaultCellStyle.Format = "#,###";
            dataGridView1.Columns["Column16"].DefaultCellStyle.Format = "#,###";
            dataGridView1.ClearSelection();
        }

        private void dataGridView2_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            dataGridView2.Columns["Column24"].DefaultCellStyle.Format = "#,###";
            dataGridView2.Columns["Column26"].DefaultCellStyle.Format = "#,###";
            sumQty();
            dataGridView2.ClearSelection();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FM_Bill fM_Bill = new FM_Bill();
            fM_Bill.ShowDialog();
        }
    }
}
