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
    public partial class MOrder : Form
    {
        private string _empId;
        private readonly config _config = new config();

        public MOrder(string empId)
        {
            InitializeComponent();
            label4.Text = "ລວມລາຍການທັງໝົດ:  " + "0" + "  ລາຍການ";
            LoadOrders();
            _empId = empId;
        }

        private void LoadOrders(string filter = "")
        {
            if (filter != "")
            {
                filter = $"AND ({filter})";
            }
            else
            {
                filter = "";
            }
            var query = $"SELECT o.[orderId], e.[empName], o.[orderDate], o.[totalOrder], s.[supplierName], s.[supplierId] " +
                        $"FROM [POSSALE].[dbo].[order] o " +
                        $"INNER JOIN [POSSALE].[dbo].[supplier] s ON o.orderFrom = s.supplierId " +
                        $"INNER JOIN [POSSALE].[dbo].[employee] e ON o.whoOrder = e.empId WHERE o.[orderStatus] = 'ສັ່ງຊື້ແລ້ວ' {filter}";
            _config.LoadData(query, dataGridView1);
        }

        private void MOrder_Load(object sender, EventArgs e)
        {
            //dataOrderBill();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                MOrderEdit editOrderBill = new MOrderEdit(this, _empId, dataGridView1.CurrentRow.Cells["Column3"].Value.ToString(), dataGridView1.CurrentRow.Cells["supId"].Value.ToString() );
                var senderGrid = (DataGridView)sender;

                if (senderGrid.Columns[e.ColumnIndex] is DataGridViewImageColumn &&
                e.RowIndex >= 0 && dataGridView1.Columns[e.ColumnIndex].HeaderCell.Value.ToString() == "ຍົກເລິກ")
                {
                    //TODO - Button Clicked - Execute Code Here
                    dataGridView1.Rows.RemoveAt(dataGridView1.Rows[e.RowIndex].Index);
                    _config.setData($"DELETE FROM [POSSALE].[dbo].[order] WHERE orderId = '{dataGridView1.CurrentRow.Cells["Column3"].Value.ToString()}'");
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

        private void dataGridView1_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            dataGridView1.ClearSelection();
            dataGridView1.Columns["Column6"].DefaultCellStyle.Format = "#,###";
            sumQty();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                var filter = $"WHERE e.[empName] LIKE '%{textBox1.Text}%' OR s.[supplierName] LIKE '%{textBox1.Text}%' OR o.[orderDate] LIKE '%{textBox1.Text}%' OR o.[totalOrder] LIKE '%{textBox1.Text}%'";
                LoadOrders(filter);
                sumQty();
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowMessage("ເກີດຂໍ້ຜີດພາດ " + ex + " ", "", "ເກີດຂໍ້ຜີດພາດ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}
