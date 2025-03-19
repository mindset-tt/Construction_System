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
    public partial class MOrderEdit : Form
    {
        MOrder mOrder;
        private readonly config _config = new config();
        private string _empId;
        private string _order;
        private string _supplierId;
        public MOrderEdit(MOrder mOrder, string empId, string Order, string supplierId)
        {
            InitializeComponent();
            this.mOrder = mOrder;
            _empId = empId;
            _order = Order;
            _supplierId = supplierId;
            LoadProducts();
            LoadOrderDetail();
            label3.Text = "0" + "   ອັນ";

        }

        private void LoadProducts(string filter = "")
        {
            var query = "SELECT p.[prodID], CAST(p.[prodName] AS NVARCHAR(MAX)) as [prodName], p.[prodQty], p.[prodPrice], t.[typeName], u.[unitName], " +
            "STRING_AGG(sp.[supplierId], ',') AS [supplierId] " +
            "FROM [POSSALE].[dbo].[supplierDetail] sp " +
            "INNER JOIN [POSSALE].[dbo].[product] p ON sp.prodId = p.prodID " +
            "INNER JOIN [POSSALE].[dbo].[supplier] s ON s.supplierId = sp.supplierId " +
            "INNER JOIN [POSSALE].[dbo].[type] t ON p.typeId = t.typeId " +
            "INNER JOIN [POSSALE].[dbo].[unit] u ON p.unitId = u.unitId " +
            $"WHERE sp.supplierId = '{_supplierId}' AND p.cancel = 'no' AND p.prodQty > 0" +
            "GROUP BY p.[prodID], CAST(p.[prodName] AS NVARCHAR(MAX)), p.[prodQty], p.[prodPrice], t.[typeName], u.[unitName]";
            _config.LoadData(query, dataGridView1);

        }

        public void AddProductToDataGridView(List<string> viewRow)
        {
            DataTable dataTable = (DataTable) dataGridView2.DataSource;
            DataRow newRow = dataTable.NewRow();
            newRow["orderId"] = viewRow[7];
            newRow["prodName"] = viewRow[2];
            newRow["productId"] = viewRow[5];
            newRow["orderQty"] = viewRow[3];
            newRow["unitName"] = viewRow[4];
            dataTable.Rows.Add(newRow);
        }

        private void LoadOrderDetail(string filter = "")
        {
            // get value from this insert  _config.setData("INSERT INTO [POSSALE].[dbo].[orderDetail] ([orderId], [productId], [orderQty]) " +
            //$"VALUES ('{orderId}', '{productId}', {row.Cells["Column24"].Value})");
            var query = $"SELECT [orderId], [prodName] ,[productId], [orderQty], [unitName] FROM [dbo].[orderDetail] " +
                $"inner join [dbo].[product] on [dbo].[orderDetail].[productId] = [dbo].[product].[prodID] " +
                $"inner join [dbo].[type] on [dbo].[product].[typeId] = [dbo].[type].[typeId] " +
                $"inner join [dbo].[unit] on [dbo].[product].[unitId] = [dbo].[unit].[unitId] " +
                $"WHERE [orderId] = '{_order}'";
            _config.LoadData(query, dataGridView2);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public void updateQty(long qty, string proId)
        {
            //dataGridView2.Rows[selectRowOr].Cells["Column24"].Value = qty;
            foreach (DataGridViewRow row in dataGridView2.Rows)
            {
                if (row.Cells["id2"].Value.ToString() == proId)
                {
                    row.Cells["Column24"].Value = qty;
                }
            }
        }

        private void sumQty()
        {
            try
            {
                if (dataGridView2.Rows.Count >= -1)
                {
                    long totalQty = 0;
                    foreach (DataGridViewRow row in dataGridView2.Rows)
                    {
                        totalQty += Convert.ToInt64(row.Cells["Column24"].Value);
                    }
                    label3.Text = totalQty.ToString("#,###") + "   ອັນ";
                }
                else
                {
                    label3.Text = "0" + "   ອັນ";
                }
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

        private void dataGridView1_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            dataGridView1.ClearSelection();
        }

        private void dataGridView1_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            dataGridView1.ClearSelection();
            dataGridView1.Columns["Column13"].DefaultCellStyle.Format = "#,###";
            dataGridView1.Columns["Column16"].DefaultCellStyle.Format = "#,###";
        }

        private void dataGridView2_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            dataGridView2.Columns["Column24"].DefaultCellStyle.Format = "#,###";
            sumQty();
            dataGridView2.ClearSelection();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                //OrEditQty editQty = new OrEditQty(null, this, null, false, false);
                //var senderGrid1 = (DataGridView)sender;
                //if (senderGrid1.Columns[e.ColumnIndex] is DataGridViewImageColumn &&
                //    e.RowIndex >= 0)
                //{
                //    //TODO - Button Clicked - Execute Code Here
                //    //MyMessageBox.ShowMessage(dataGridView2.Rows[e.RowIndex].Cells[3].Value.ToString(), "", "ຍິນດີຕ້ອນຮັບ", MessageBoxButtons.OK, MessageBoxIcon.None);
                //    editQty.label1.Text = " ຈັດການເພີ່ມຈຳນວນສັ່ງຊື້ສິນຄ້າ";
                //    editQty.label1.Image = Construction_System.Properties.Resources.add_button;
                //    editQty.label1.Size = new System.Drawing.Size(216, 26);
                //    editQty.button1.Text = "ເພີ່ມ";
                //    editQty.textBox1.Text = dataGridView1.Rows[e.RowIndex].Cells["Column12"].Value.ToString();
                //    editQty.lblUnit.Text = dataGridView1.Rows[e.RowIndex].Cells["Column14"].Value.ToString();
                //    //editQty.lblPrice.Text = dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString();
                //    editQty.lblId.Text = dataGridView1.Rows[e.RowIndex].Cells["id1"].Value.ToString();
                //    editQty.ShowDialog();
                //    dataGridView1.ClearSelection();
                //    //dataGridView2.Columns["Column24"].DefaultCellStyle.Format = "#,###";
                //    //dataGridView2.Columns["Column26"].DefaultCellStyle.Format = "#,###";
                //}
                if (dataGridView1.Columns[e.ColumnIndex] is DataGridViewImageColumn && e.RowIndex >= 0)
                {
                    var editQty = new OrEditQty(null, this, dataGridView1.Rows[e.RowIndex].Cells["Column8"].Value.ToString(), false, false, _order)
                    {
                        label1 = { Text = " ເພີ່ມຈຳນວນສັ່ງຊື້ສິນຄ້າ", Image = Construction_System.Properties.Resources.add_button },
                        button1 = { Text = "ເພີ່ມ" },
                        textBox1 = { Text = dataGridView1.Rows[e.RowIndex].Cells["Column12"].Value.ToString() },
                        lblUnit = { Text = dataGridView1.Rows[e.RowIndex].Cells["Column14"].Value.ToString() },
                        lblId = { Text = dataGridView1.Rows[e.RowIndex].Cells["id1"].Value.ToString() }
                    };
                    editQty.ShowDialog();
                    dataGridView1.ClearSelection();
                }

            }
            catch (Exception ex)
            {

                MyMessageBox.ShowMessage("ເກີດຂໍ້ຜີດພາດ " + ex + " ", "", "ເກີດຂໍ້ຜີດພາດ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                //OrEditQty editQty = new OrEditQty(null, this, null, false, false);
                //var senderGrid = (DataGridView)sender;

                //if (senderGrid.Columns[e.ColumnIndex] is DataGridViewImageColumn &&
                //e.RowIndex >= 0 && dataGridView2.Columns[e.ColumnIndex].HeaderCell.Value.ToString() == "ລົບ")
                //{
                //    //TODO - Button Clicked - Execute Code Here
                //    dataGridView2.Rows.RemoveAt(dataGridView2.Rows[e.RowIndex].Index);
                //    //MyMessageBox.ShowMessage("ເພີ່ມຂໍ້ມູນສຳເລັດແລ້ວ", "", "ສຳເລັດ", MessageBoxButtons.OK, MessageBoxIcon.None);
                //}

                //if (senderGrid.Columns[e.ColumnIndex] is DataGridViewImageColumn &&
                //    e.RowIndex >= 0 && dataGridView2.Columns[e.ColumnIndex].HeaderCell.Value.ToString() == "ແກ້ໄຂ")
                //{
                //    //TODO - Button Clicked - Execute Code Here
                //    //MyMessageBox.ShowMessage(dataGridView2.Rows[e.RowIndex].Cells[3].Value.ToString(), "", "ຍິນດີຕ້ອນຮັບ", MessageBoxButtons.OK, MessageBoxIcon.None);
                //    editQty.label1.Text = "ຈັດການແກ້ໄຂຈຳນວນສັ່ງຊື້ສິນຄ້າ";
                //    editQty.label1.Image = Construction_System.Properties.Resources.pencil;
                //    editQty.label1.Size = new System.Drawing.Size(216, 26);
                //    editQty.button1.Text = "ແກ້ໄຂ";
                //    editQty.textBox1.Text = dataGridView2.Rows[e.RowIndex].Cells["Column23"].Value.ToString();
                //    editQty.textBox2.Text = dataGridView2.Rows[e.RowIndex].Cells["Column24"].Value.ToString();
                //    //editQty.lblId.Text = dataGridView2.Rows[e.RowIndex].Cells["id2"].Value.ToString();
                //    editQty.ShowDialog();


                //}

                var senderGrid = (DataGridView)sender;
                if (senderGrid.Columns[e.ColumnIndex] is DataGridViewImageColumn && e.RowIndex >= 0)
                {
                    if (dataGridView2.Columns[e.ColumnIndex].HeaderCell.Value.ToString() == "ລົບ")
                    {
                        dataGridView2.Rows.RemoveAt(e.RowIndex);
                        sumQty();
                    }
                    else if (dataGridView2.Columns[e.ColumnIndex].HeaderCell.Value.ToString() == "ແກ້ໄຂ")
                    {
                        var editQty = new OrEditQty(null, this, null, false, true, _order)
                        {
                            label1 = { Text = "ແກ້ໄຂຈຳນວນສັ່ງຊື້ສິນຄ້າ", Image = Construction_System.Properties.Resources.pencil },
                            button1 = { Text = "ແກ້ໄຂ" },
                            textBox1 = { Text = dataGridView2.Rows[e.RowIndex].Cells["Column23"].Value.ToString() },
                            textBox2 = { Text = dataGridView2.Rows[e.RowIndex].Cells["Column24"].Value.ToString() },
                            lblId = { Text = dataGridView2.Rows[e.RowIndex].Cells["id2"].Value.ToString() }
                        };
                        editQty.ShowDialog();
                    }
                }
            }
            catch (Exception ex)
            {

                MyMessageBox.ShowMessage("ເກີດຂໍ້ຜີດພາດ " + ex + " ", "", "ເກີດຂໍ້ຜີດພາດ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public long selectRowOr;
        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            selectRowOr = e.RowIndex;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                //dataProduct();
                (dataGridView1.DataSource as DataTable).DefaultView.RowFilter = string.Format("unitName LIKE '%{0}%' or prodID LIKE '%{0}%' or prodName LIKE '%{0}%'", textBox1.Text);

            }
            catch (Exception ex)
            {
                MyMessageBox.ShowMessage("ສິ່ງທີ່ຄົ້ນຫາບໍ່ຖືກຕ້ອງ " + ex + " ", "", "ເກີດຂໍ້ຜີດພາດ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //FM_Bill fM_Bill = new FM_Bill();
            //fM_Bill.ShowDialog();

            //update order
            try
            {
                var query = "";

                //delete all order detail first
                query = $"DELETE FROM [POSSALE].[dbo].[orderDetail] WHERE [orderId] = '{_order}'";
                _config.setData(query);

                foreach (DataGridViewRow row in dataGridView2.Rows)
                {
                    query = $"INSERT INTO [POSSALE].[dbo].[orderDetail] ([orderId], [productId], [orderQty]) " +
                        $"VALUES ('{_order}', '{row.Cells["id2"].Value}', {row.Cells["Column24"].Value})";
                    _config.setData(query);
                }
                var totalOrder = label3.Text.Replace("   ອັນ", "");
                query = $"UPDATE [POSSALE].[dbo].[order] SET [totalOrder] = {totalOrder} WHERE [orderId] = '{_order}'";
                _config.setData(query);

                MyMessageBox.ShowMessage("ປ່ຽນຈຳນວນສັ່ງຊື້ສິນຄ້າສຳເລັດແລ້ວ", "", "ສຳເລັດ", MessageBoxButtons.OK, MessageBoxIcon.None);

                FM_Bill fM_Bill = new FM_Bill();
                OrderBill orderBill = new OrderBill();
                orderBill.Refresh();
                orderBill.SetParameterValue("orderId", _order);
                fM_Bill.crystalReportViewer1.ReportSource = orderBill;
                fM_Bill.ShowDialog();
                this.Close();
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowMessage("ເກີດຂໍ້ຜີດພາດ " + ex + " ", "", "ເກີດຂໍ້ຜີດພາດ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
