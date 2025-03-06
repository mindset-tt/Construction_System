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
    public partial class MSupAddProduct : Form
    {
        MSupplier msupplier;
        public MSupAddProduct(MSupplier msupplier)
        {
            InitializeComponent();
            this.msupplier = msupplier;
        }

        private void allProduct()
        {
            DataSet data1 = new DataSet("Contruction_System");
            DataTable table1 = new DataTable("Sale1");
            table1.Columns.Add("id", typeof(int));
            table1.Columns.Add("name");
            table1.Columns.Add("unit");
            table1.Columns.Add("type");
            table1.Rows.Add(1, "ຕະປູແປດ", "ໂລ", "ເຫຼັກ");
            table1.Rows.Add(2, "ຕະປູຫ້າ", "ໂລ", "ເຫຼັກ");
            table1.Rows.Add(3, "ຕະປູຫົກ", "ໂລ", "ເຫຼັກ");
            table1.Rows.Add(4, "ກະເບື່ອງ", "ແຜ່ນ", "ຫຼັງຄາ");
            table1.Rows.Add(5, "ຫຼົບກະເບື່ອງ", "ແຜ່ນ", "ຫຼັງຄາ");
            table1.Rows.Add(6, "ຊັງກະສີແຜ່ນລຽບ", "ແຜ່ນ", "ຫຼັງຄາ");
            table1.Rows.Add(7, "ຊັງກະສີແຜ່ນລ່ອງ", "ແຜ່ນ", "ຫຼັງຄາ");
            table1.Rows.Add(8, "ສາຍໄຟ 2.5", "ກໍ່", "ໄຟຟ້າ");
            table1.Rows.Add(9, "ຄ້ອນຕີໃຫຍ່", "ອັນ", "ເຄື່ອງມື");
            table1.Rows.Add(10, "ຄ້ອນຕີນ້ອຍ", "ອັນ", "ເຄື່ອງມື");
            table1.Rows.Add(11, "ຄ້ອນປອນ", "ອັນ", "ເຄື່ອງມື");
            table1.Rows.Add(12, "ຕູ້ຈອດຫຼັກ", "ເຄື່ອງ", "ໄຟຟ້າ");
            table1.Rows.Add(52, "ໄຟບີຕັດເຫຼັກ", "ເຄື່ອງ", "ໄຟຟ້າ");
            table1.Rows.Add(63, "ໝືນຕັດເຫຼັກ", "ເຄື່ອງ", "ໄຟຟ້າ");
            table1.Rows.Add(72, "ສະຫວ່ານໄຟຟ້າ", "ເຄື່ອງ", "ໄຟຟ້າ");
            table1.Rows.Add(85, "ສະຫວ່ານແບັກເຕີລີ້", "ເຄື່ອງ", "ໄຟຟ້າ");
            table1.Rows.Add(85, "ສະຫວ່ານແບັກເຕີລີ້", "ເຄື່ອງ", "ໄຟຟ້າ");
            table1.Rows.Add(85, "ສະຫວ່ານແບັກເຕີລີ້", "ເຄື່ອງ", "ໄຟຟ້າ");
            data1.Tables.Add(table1);
            dataGridView1.DataSource = table1;
        }

        private void productSupplier()
        {
            DataSet data1 = new DataSet("Contruction_System");
            DataTable table1 = new DataTable("Sale1");
            table1.Columns.Add("id", typeof(int));
            table1.Columns.Add("name");
            table1.Columns.Add("unit");
            table1.Columns.Add("type");
            table1.Rows.Add(4, "ກະເບື່ອງ", "ແຜ່ນ", "ຫຼັງຄາ");
            table1.Rows.Add(9, "ຄ້ອນຕີໃຫຍ່", "ອັນ", "ເຄື່ອງມື");
            table1.Rows.Add(10, "ຄ້ອນຕີນ້ອຍ", "ອັນ", "ເຄື່ອງມື");
            table1.Rows.Add(11, "ຄ້ອນປອນ", "ອັນ", "ເຄື່ອງມື");
            table1.Rows.Add(12, "ຕູ້ຈອດຫຼັກ", "ເຄື່ອງ", "ໄຟຟ້າ");
            table1.Rows.Add(52, "ໄຟບີຕັດເຫຼັກ", "ເຄື່ອງ", "ໄຟຟ້າ");
            table1.Rows.Add(63, "ໝືນຕັດເຫຼັກ", "ເຄື່ອງ", "ໄຟຟ້າ");
            table1.Rows.Add(72, "ສະຫວ່ານໄຟຟ້າ", "ເຄື່ອງ", "ໄຟຟ້າ");
            table1.Rows.Add(85, "ສະຫວ່ານແບັກເຕີລີ້", "ເຄື່ອງ", "ໄຟຟ້າ");
            data1.Tables.Add(table1);
            dataGridView2.DataSource = table1;
        }

        private void MSupAddProduct_Load(object sender, EventArgs e)
        {
            allProduct();
            productSupplier();
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

        private void dataGridView1_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            dataGridView1.ClearSelection();
        }

        private void dataGridView1_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            dataGridView1.ClearSelection();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            MyMessageBox.ShowMessage("ເພີ່ມຂໍ້ມູນສຳເລັດແລ້ວ", "", "ສຳເລັດ", MessageBoxButtons.OK, MessageBoxIcon.None);
        }

        private void dataGridView2_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            dataGridView2.ClearSelection();
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            MyMessageBox.ShowMessage("ລົບຂໍ້ມູນສຳເລັດແລ້ວ", "", "ສຳເລັດ", MessageBoxButtons.OK, MessageBoxIcon.None);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
