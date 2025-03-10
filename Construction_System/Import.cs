using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace Construction_System
{
    public partial class Import : Form
    {
        private readonly config _config = new config();
        private readonly string _empId;
        private string _editQty;
        private string _totalPrice;
        private string _dif;
        private string _originalOrder;

        public Import(string empId)
        {
            InitializeComponent();
            label2.Text = "0 ກີບ";
            _empId = empId;
            LoadData();
        }

        private void LoadData()
        {
            LoadProducts();
        }

        private void LoadProducts(string filter = "")
        {
            if (filter != "")
            {
                filter = $"AND ({filter})";
            }
            else
            {
                filter = "";
            }
            var query = $"SELECT o.[orderId], o.[orderDate], o.[totalOrder], s.[supplierName] " +
                        $"FROM [POSSALE].[dbo].[order] o " +
                        $"INNER JOIN [POSSALE].[dbo].[supplier] s ON o.orderFrom = s.supplierId WHERE o.[orderStatus] = 'ສັ່ງຊື້ແລ້ວ' {filter}";
            _config.LoadData(query, dataGridView1);
        }

        private void SumQty()
        {
            try
            {
                if (dataGridView2.Columns.Contains("Column26"))
                {
                    var totalPrice = dataGridView2.Rows.Cast<DataGridViewRow>()
                        .Sum(row => Convert.ToInt32(row.Cells["Column26"].Value));
                    label2.Text = $"{totalPrice:#,###} ກີບ";
                }
                else
                {
                    // Handle the case where the column does not exist
                    label2.Text = "0 ກີບ";
                }
            }
            catch (Exception ex)
            {
                ShowMessage($"ເກີດຂໍ້ຜີດພາດ {ex}", "ຂໍ້ຜິດພາດ");
            }
        }

        private void ShowMessage(string message, string content)
        {
            MyMessageBox.ShowMessage(message, "", content, MessageBoxButtons.OK, MessageBoxIcon.None);
        }

        private void dataGridView1_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            dataGridView1.ClearSelection();
        }

        private void dataGridView1_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            dataGridView1.Columns["Column14"].DefaultCellStyle.Format = "#,###";
        }

        private void dataGridView2_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            dataGridView2.ClearSelection();
        }

        private void dataGridView2_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            if (dataGridView2.Columns["Column24"] != null)
            {
                dataGridView2.Columns["Column24"].DefaultCellStyle.Format = "#,###";
            }
            if (dataGridView2.Columns["Column26"] != null)
            {
                dataGridView2.Columns["Column26"].DefaultCellStyle.Format = "#,###";
            }
            SumQty();
            dataGridView2.ClearSelection();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string filter = "";
                if (textBox1.Text != "")
                {
                    filter = $"o.[orderId] LIKE '%{textBox1.Text}%' OR s.[supplierName] LIKE '%{textBox1.Text}%' " +
                                 $"OR o.[totalOrder] LIKE '%{textBox1.Text}%' OR o.[orderDate] LIKE '%{textBox1.Text}%'";
                }
                LoadProducts(filter);
            }
            catch (Exception ex)
            {
                ShowMessage($"ເກີດຂໍ້ຜີດພາດ {ex}", "ຂໍ້ຜິດພາດ");
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {

                var query = $"SELECT p.[prodId], p.[prodName], o.[orderQty], o.[orderQty] as [orderQtys], o.[orderQty] as [orderQtyss], u.[unitName], " +
                            $"(p.[prodPrice] * o.[orderQty]) as [totalPrice], p.[prodPrice] " +
                            $"FROM [POSSALE].[dbo].[product] p " +
                            $"INNER JOIN [POSSALE].[dbo].[orderDetail] o ON p.prodId = o.productId " +
                            $"INNER JOIN [POSSALE].[dbo].[unit] u ON p.unitId = u.unitId " +
                            $"WHERE o.orderId = '{dataGridView1.Rows[e.RowIndex].Cells["id1"].Value}'";
                _config.LoadData(query, dataGridView2);
                SumQty();
                ShowMessage("ເພີ່ມຂໍ້ມູນສຳເລັດແລ້ວ", "ສຳເລັດ");
            }
            catch (Exception ex)
            {
                ShowMessage($"ເກີດຂໍ້ຜີດພາດ {ex}", "ຂໍ້ຜິດພາດ");
            }
        }

        public void updateQty(int qty, int price, int dif, string proId)
        {
            //dataGridView2.Rows[selectRowOr].Cells["Column24"].Value = qty;
            foreach (DataGridViewRow row in dataGridView2.Rows)
            {
                if (row.Cells["id2"].Value.ToString() == proId)
                {
                    row.Cells["Column24"].Value = qty;
                    row.Cells["Column26"].Value = price;
                    row.Cells["difFromOrder"].Value = qty - int.Parse(_originalOrder);
                }
            }
        }

        public int selectRowIm;
        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            selectRowIm = e.RowIndex;
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                var senderGrid = (DataGridView)sender;
                if (senderGrid.Columns[e.ColumnIndex] is DataGridViewImageColumn && e.RowIndex >= 0)
                {
                    if (dataGridView2.Columns[e.ColumnIndex].HeaderCell.Value.ToString() == "ລົບ")
                    {
                        dataGridView2.Rows.RemoveAt(e.RowIndex);
                        SumQty();
                    }
                    else if (dataGridView2.Columns[e.ColumnIndex].HeaderCell.Value.ToString() == "ແກ້ໄຂ")
                    {
                        var editQty = new ImEditQty(this, null, dataGridView2.Rows[e.RowIndex].Cells["price"].Value.ToString(), dataGridView2.Rows[e.RowIndex].Cells["difFromOrder"].Value.ToString(), dataGridView2.Rows[e.RowIndex].Cells["originalOrder"].Value.ToString())
                        {
                            textBox1 = { Text = dataGridView2.Rows[e.RowIndex].Cells["Column23"].Value.ToString() },
                            textBox2 = { Text = dataGridView2.Rows[e.RowIndex].Cells["Column24"].Value.ToString() },
                            textBox3 = { Text = dataGridView2.Rows[e.RowIndex].Cells["Column26"].Value.ToString() },
                            lblId = { Text = dataGridView2.Rows[e.RowIndex].Cells["id2"].Value.ToString() },
                        };
                        _originalOrder = dataGridView2.Rows[e.RowIndex].Cells["originalOrder"].Value.ToString();
                        editQty.ShowDialog();

                        //dataGridView2.Rows[e.RowIndex].Cells["Column24"].Value = _editQty;
                        //dataGridView2.Rows[e.RowIndex].Cells["Column26"].Value = _totalPrice;
                        //dataGridView2.Rows[e.RowIndex].Cells["difFromOrder"].Value = _dif;
                    }
                }
            }
            catch (Exception ex)
            {
                ShowMessage($"ເກີດຂໍ້ຜີດພາດ {ex}", "ຂໍ້ຜິດພາດ");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (dataGridView2.RowCount == 0)
            {
                ShowMessage("ທ່ານຍັງບໍ່ໄດ້ເພີ່ມສິນຄ້າໃນການນຳເຂົ້າສິນຄ້າ", "ຂໍ້ຜິດພາດ");
                return;
            }

            try
            {
                string query;
                query = "SELECT TOP 1 [importId] FROM [POSSALE].[dbo].[import] ORDER BY [importId] DESC";
                var dr = _config.getData(query);
                dr.Read();

                var importID = dr.HasRows ? $"IM{int.Parse(dr["importId"].ToString().Substring(2)) + 1:D4}" : "IM0001";
                dr.Close();
                _config.closeConnection();

                var totalOrder = int.Parse(label2.Text.Split(' ')[0].Replace(",", ""));

                query = $"INSERT INTO [POSSALE].[dbo].[import] ([importId], [whoImport], [importDate], [totalImport], [importFromOrder], [importStatus], [totalPriceImport]) " +
                        $"VALUES ('{importID}', '{_empId}', '{DateTime.Now:yyyy-MM-dd}', {dataGridView2.RowCount}, '{dataGridView1.CurrentRow.Cells["id1"].Value}', 'ອະນຸມັດ', {totalOrder})";
                _config.setData(query);
                int getQty = 0;
                foreach (DataGridViewRow row in dataGridView2.Rows)
                {
                    int CalculateDif() => int.Parse(row.Cells["difFromOrder"].Value.ToString()) - int.Parse(row.Cells["Column24"].Value.ToString());
                    int dif = CalculateDif() != 0 ? int.Parse(row.Cells["difFromOrder"].Value.ToString()) : CalculateDif();
                    
                    query = $"INSERT INTO [POSSALE].[dbo].[importDetail] ([importId], [product], [importQty], [price], [totalPrice], [difFromOrder]) " +
                                  $"VALUES ('{importID}', '{row.Cells["id2"].Value}', {row.Cells["Column24"].Value}, {row.Cells["price"].Value}, {row.Cells["Column26"].Value}, {dif})";
                    _config.setData(query);

                    // get the current stock of the product
                    query = $"SELECT [prodQty] FROM [POSSALE].[dbo].[product] WHERE [prodId] = '{row.Cells["id2"].Value}'";
                    dr = _config.getData(query);
                    dr.Read();
                    var currentStock = int.Parse(dr["prodQty"].ToString());
                    dr.Close();
                    _config.closeConnection();

                    // update the stock of the product
                    query = $"UPDATE [POSSALE].[dbo].[product] SET [prodQty] = {currentStock + int.Parse(row.Cells["Column24"].Value.ToString())} WHERE [prodId] = '{row.Cells["id2"].Value}'";
                    _config.setData(query);
                    getQty += int.Parse(row.Cells["Column24"].Value.ToString());

                }
                //update totalImport in import table
                query = $"UPDATE [POSSALE].[dbo].[import] SET [totalImport] = {getQty} WHERE [importId] = '{importID}'";
                _config.setData(query);

                //update the order status to 'ອະນຸມັດ'
                query = $"UPDATE [POSSALE].[dbo].[order] SET [orderStatus] = 'ອະນຸມັດ' WHERE [orderId] = '{dataGridView1.CurrentRow.Cells["id1"].Value}'";
                _config.setData(query);

                ShowMessage("ການນຳເຂົ້າສິນຄ້າສຳເລັດ", "ສຳເລັດ");

                //clear the datagridview
                this.dataGridView2.DataSource = null;
                this.dataGridView2.Rows.Clear();
                label2.Text = "0   ອັນ";

                FM_Bill bill = new FM_Bill();
                ImportBill importBill = new ImportBill();
                importBill.SetParameterValue("importId", importID);
                bill.crystalReportViewer1.ReportSource = importBill;
                bill.ShowDialog();
                LoadProducts();
            }
            catch (Exception ex)
            {
                ShowMessage($"ເກີດຂໍ້ຜີດພາດ {ex}", "ຂໍ້ຜິດພາດ");
            }
        }
    }
}
