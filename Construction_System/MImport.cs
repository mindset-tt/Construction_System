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
    public partial class MImport : Form
    {
        private string _empId;
        public MImport(string empId)
        {
            InitializeComponent();
            _empId = empId;
        }

        private void dataImportBill()
        {
            DataSet data1 = new DataSet("Contruction_System");
            DataTable table1 = new DataTable("Order1");
            table1.Columns.Add("idSale", typeof(int));
            table1.Columns.Add("empName");
            table1.Columns.Add("datetime");
            table1.Columns.Add("supName");
            table1.Columns.Add("qty", typeof(int));
            table1.Rows.Add(25, "ນ້ອຍ", "20/02/2025", "ເອສຊີຈີ (SCG)", 15);
            table1.Rows.Add(45, "ນ້ອຍ", "19/02/2025", "ຊີເອສຊີ (CSC)", 14);
            table1.Rows.Add(54, "ລີຊາ", "21/02/2025", "ສຸວັນນີ", 12);
            table1.Rows.Add(67, "ນ້ອຍ", "19/02/2025", "ຊີເອສຊີ (CSC)", 18);
            table1.Rows.Add(68, "ນ້ອຍ", "19/02/2025", "ຊີເອສຊີ (CSC)", 140);
            data1.Tables.Add(table1);
            dataGridView1.DataSource = table1;
        }

        private void sumQty()
        {
            try
            {
                if (dataGridView1.Rows.Count != 0)
                {
                    label4.Text = "ລວມລາຍການທັງໝົດ:  " + dataGridView1.RowCount.ToString("#,###") + "  ລາຍການ";
                    //int totalQty = 0;
                    //for (int i = 0; i < dataGridView1.RowCount; i++)
                    //{
                    //    totalQty += Convert.ToInt32(dataGridView1.Rows[i].Cells["Column24"].Value.ToString());
                    //}
                    //label2.Text = totalQty.ToString("#,###") + "   ອັນ";
                }
                else
                {
                    label4.Text = "ລວມລາຍການທັງໝົດ:  " + "0" + "  ລາຍການ";
                }
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowMessage("ເກີດຂໍ້ຜີດພາດ " + ex + " ", "", "ເກີດຂໍ້ຜີດພາດ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void MImport_Load(object sender, EventArgs e)
        {
            dataImportBill();
            sumQty();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                (dataGridView1.DataSource as DataTable).DefaultView.RowFilter = string.Format("empName LIKE '%{0}%' " +
                    "or datetime LIKE '%{0}%' or supName LIKE '%{0}%'", textBox1.Text);
                sumQty();
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
                MImportEdit editOrderBill = new MImportEdit(this);
                var senderGrid = (DataGridView)sender;

                if (senderGrid.Columns[e.ColumnIndex] is DataGridViewImageColumn &&
                e.RowIndex >= 0 && dataGridView1.Columns[e.ColumnIndex].HeaderCell.Value.ToString() == "ຍົກເລິກ")
                {
                    //TODO - Button Clicked - Execute Code Here
                    dataGridView1.Rows.RemoveAt(dataGridView1.Rows[e.RowIndex].Index);
                    sumQty();
                    MyMessageBox.ShowMessage("ຍົກເລິກໃບບິນສຳເລັດແລ້ວ", "", "ສຳເລັດ", MessageBoxButtons.OK, MessageBoxIcon.None);
                }

                if (senderGrid.Columns[e.ColumnIndex] is DataGridViewImageColumn &&
                    e.RowIndex >= 0 && dataGridView1.Columns[e.ColumnIndex].HeaderCell.Value.ToString() == "ແກ້ໄຂ")
                {
                    //TODO - Button Clicked - Execute Code Here

                    //editSaleBill.textBox1.Text = dataGridView2.Rows[e.RowIndex].Cells["Column23"].Value.ToString();
                    //editSaleBill.textBox2.Text = dataGridView2.Rows[e.RowIndex].Cells["Column24"].Value.ToString();
                    //editSaleBill.textBox3.Text = dataGridView2.Rows[e.RowIndex].Cells["Column26"].Value.ToString();
                    //editQty.lblId.Text = dataGridView2.Rows[e.RowIndex].Cells["id2"].Value.ToString();
                    editOrderBill.ShowDialog();

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
            dataGridView1.ClearSelection();
            dataGridView1.Columns["Column6"].DefaultCellStyle.Format = "#,###";
        }
    }
}
