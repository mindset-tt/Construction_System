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
                    //int totalQty = 0;
                    //for (int i = 0; i < dataGridView1.RowCount; i++)
                    //{
                    //    totalQty += Convert.ToInt32(dataGridView1.Rows[i].Cells["Column24"].Value.ToString());
                    //}
                    //label2.Text = totalQty.ToString("#,###") + "   ອັນ";
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

        private void MProduct_Load(object sender, EventArgs e)
        {
            DataSet data1 = new DataSet("Contruction_System");
            DataTable table1 = new DataTable("Sale1");
            table1.Columns.Add("id", typeof(int));
            table1.Columns.Add("prodname");
            table1.Columns.Add("qty", typeof(int));
            table1.Columns.Add("unit");
            table1.Columns.Add("type");
            table1.Columns.Add("priceOrder", typeof(int));
            table1.Columns.Add("priceSell", typeof(int));
            table1.Rows.Add(1, "ຕະປູແປດ", 1, "ໂລ", "ເຫຼັກ", 70000, 80000);
            table1.Rows.Add(2, "ຕະປູຫ້າ", 2, "ໂລ", "ເຫຼັກ", 70000, 80000);
            table1.Rows.Add(3, "ຕະປູຫົກ", 3, "ໂລ", "ເຫຼັກ", 105000, 1105000);
            table1.Rows.Add(4, "ກະເບື່ອງ", 10, "ແຜ່ນ", "ຫຼັງຄາ", 560000, 590000);
            table1.Rows.Add(5, "ຫຼົບກະເບື່ອງ", 4, "ແຜ່ນ", "ຫຼັງຄາ", 240000, 280000);
            table1.Rows.Add(6, "ຊັງກະສີແຜ່ນລຽບ", 12, "ແຜ່ນ", "ຫຼັງຄາ", 680000, 700000);
            table1.Rows.Add(7, "ຊັງກະສີແຜ່ນລ່ອງ", 20, "ແຜ່ນ", "ຫຼັງຄາ", 1320000, 240000);
            table1.Rows.Add(8, "ສາຍໄຟ 2.5", 3, "ກໍ່", "ໄຟຟ້າ", 850000, 900000);
            table1.Rows.Add(9, "ຄ້ອນຕີໃຫຍ່", 1, "ອັນ", "ເຄື່ອງມື", 120000, 140000);
            table1.Rows.Add(10, "ຄ້ອນຕີນ້ອຍ", 2, "ອັນ", "ເຄື່ອງມື", 140000, 180000);
            table1.Rows.Add(11, "ຄ້ອນປອນ", 1, "ອັນ", "ເຄື່ອງມື", 150000, 190000);
            table1.Rows.Add(12, "ຕູ້ຈອດຫຼັກ", 1, "ເຄື່ອງ", "ໄຟຟ້າ", 570000, 610000);
            table1.Rows.Add(52, "ໄຟບີຕັດເຫຼັກ", 2, "ເຄື່ອງ", "ໄຟຟ້າ", 2700000, 2800000);
            table1.Rows.Add(63, "ໝືນຕັດເຫຼັກ", 1, "ເຄື່ອງ", "ໄຟຟ້າ", 350000, 380000);
            table1.Rows.Add(72, "ສະຫວ່ານໄຟຟ້າ", 2, "ເຄື່ອງ", "ໄຟຟ້າ", 2620000, 2800000);
            table1.Rows.Add(85, "ສະຫວ່ານແບັກເຕີລີ້", 2, "ເຄື່ອງ", "ໄຟຟ້າ", 1250000, 2240000);
            table1.Rows.Add(85, "ສະຫວ່ານແບັກເຕີລີ້", 2, "ເຄື່ອງ", "ໄຟຟ້າ", 1250000, 2240000);
            table1.Rows.Add(85, "ສະຫວ່ານແບັກເຕີລີ້", 2, "ເຄື່ອງ", "ໄຟຟ້າ", 1250000, 2240000);
            data1.Tables.Add(table1);
            dataGridView1.DataSource = table1;
        }

        private void dataGridView1_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            dataGridView1.ClearSelection();
            sumQty();
        }

        private void dataGridView1_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            dataGridView1.Columns["Column4"].DefaultCellStyle.Format = "#,###";
            dataGridView1.Columns["Column7"].DefaultCellStyle.Format = "#,###";
            dataGridView1.Columns["Column8"].DefaultCellStyle.Format = "#,###";
            dataGridView1.ClearSelection();
            sumQty();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
