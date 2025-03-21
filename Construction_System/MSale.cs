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
    public partial class MSale : Form
    {
        private string _empId;
        private readonly config _config = new config();
        public MSale(string empId)
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
                    label4.Text = "ລວມລາຍການທັງໝົດ:  " + dataGridView1.RowCount.ToString("#,###") + "  ລາຍການ";
                    //long totalQty = 0;
                    //for (int i = 0; i < dataGridView1.RowCount; i++)
                    //{
                    //    totalQty += Convert.ToInt64(dataGridView1.Rows[i].Cells["Column24"].Value.ToString());
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

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                (dataGridView1.DataSource as DataTable).DefaultView.RowFilter = string.Format("sellId LIKE '%{0}%' or whoSell LIKE '%{0}%'", textBox1.Text);
                sumQty();
            }
            catch (Exception ex)
            {

                MyMessageBox.ShowMessage("ເກີດຂໍ້ຜີດພາດ " + ex + " ", "", "ເກີດຂໍ້ຜີດພາດ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void dataSaleBill()
        {
            var query = "SELECT [sellId] ,[whoSell] ,[sellDate] ,[totalSell] ,[totalPriceSell] FROM [dbo].[sell] where sellStatus = 'ສຳເລັດ'";
            _config.LoadData(query, dataGridView1);
            sumQty();
        }

        private void MSale_Load(object sender, EventArgs e)
        {
            dataSaleBill();
        }

        private void dataGridView1_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            dataGridView1.ClearSelection();
        }

        private void dataGridView1_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            dataGridView1.ClearSelection();
            dataGridView1.Columns["Column6"].DefaultCellStyle.Format = "#,###";
            dataGridView1.Columns["Column7"].DefaultCellStyle.Format = "#,###";
            dataGridView1.Columns["Column5"].DefaultCellStyle.Format = "dd/MM/yyyy hh:mm:ss tt";
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                MSaleEdit editSaleBill = new MSaleEdit(this, dataGridView1.Rows[e.RowIndex].Cells["Column3"].Value.ToString(), dataGridView1.Rows[e.RowIndex].Cells["Column4"].Value.ToString());
                var senderGrid = (DataGridView)sender;

                if (senderGrid.Columns[e.ColumnIndex] is DataGridViewImageColumn &&
                e.RowIndex >= 0 && dataGridView1.Columns[e.ColumnIndex].HeaderCell.Value.ToString() == "ຍົກເລິກ")
                {
                    //TODO - Button Clicked - Execute Code Here
                    dataGridView1.Rows.RemoveAt(dataGridView1.Rows[e.RowIndex].Index);
                    var query = $"UPDATE [dbo].[sell] SET sellStatus = 'ຍົກເລິກ' WHERE sellId = '{dataGridView1.Rows[e.RowIndex].Cells["Column3"].Value.ToString()}'";
                    _config.setData(query);
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
                    editSaleBill.ShowDialog();

                }
            }
            catch (Exception ex)
            {

                MyMessageBox.ShowMessage("ເກີດຂໍ້ຜີດພາດ " + ex + " ", "", "ເກີດຂໍ້ຜີດພາດ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
