using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Services.Description;
using System.Windows.Forms;

namespace Construction_System
{
    public partial class Import : Form
    {
        private readonly config _config = new config();
        private string _empId;

        public Import(string empId)
        {
            InitializeComponent();
            label2.Text = "0 ກີບ";
            LoadData();
            _empId = empId;
        }

        private void LoadData()
        {
            LoadProducts();
        }

        private void LoadProducts(string filter = "")
        {
            var query = "SELECT o.[orderId], o.[orderDate], o.[totalOrder], s.[supplierName] " +
                        "FROM [POSSALE].[dbo].[order] o " +
                        "INNER JOIN [POSSALE].[dbo].[supplier] s ON o.orderFrom = s.supplierId " + filter;
            _config.LoadData(query, dataGridView1);
        }

        private void sumQty()
        {
            try
            {
                var totalPrice = dataGridView2.Rows.Cast<DataGridViewRow>()
                    .Sum(row => Convert.ToInt32(row.Cells["Column26"].Value));
                label2.Text = $"{totalPrice:#,###} ກີບ";
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
            dataGridView2.Columns["Column24"].DefaultCellStyle.Format = "#,###";
            dataGridView2.Columns["Column26"].DefaultCellStyle.Format = "#,###";
            sumQty();
            dataGridView2.ClearSelection();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                var filter = $"WHERE o.[orderId] LIKE '%{textBox1.Text}%' OR s.[supplierName] LIKE '%{textBox1.Text}%' " +
                             $"OR o.[totalOrder] LIKE '%{textBox1.Text}%' OR o.[orderDate] LIKE '%{textBox1.Text}%'";
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
                if (dataGridView2.RowCount != 0)
                {
                    dataGridView2.Rows.Clear();
                }

                var query = $"SELECT p.[prodId], p.[prodName], o.[orderQty], u.[unitName], " +
                            $"(p.[prodPrice] * o.[orderQty]) as [totalPrice], p.[prodPrice] " +
                            $"FROM [POSSALE].[dbo].[product] p " +
                            $"INNER JOIN [POSSALE].[dbo].[orderDetail] o ON p.prodId = o.productId " +
                            $"INNER JOIN [POSSALE].[dbo].[unit] u ON p.unitId = u.unitId " +
                            $"WHERE o.orderId = '{dataGridView1.Rows[e.RowIndex].Cells["id1"].Value}'";
                _config.LoadData(query, dataGridView2);
                sumQty();
                ShowMessage("ເພີ່ມຂໍ້ມູນສຳເລັດແລ້ວ", "ສຳເລັດ");
            }
            catch (Exception ex)
            {
                ShowMessage($"ເກີດຂໍ້ຜີດພາດ {ex}", "ຂໍ້ຜິດພາດ");
            }
        }

        public void updateQty(int qty, int price, int dif)
        {
            var row = dataGridView2.Rows[selectRowIm];
            MessageBox.Show(qty.ToString());
            MessageBox.Show(price.ToString());
            MessageBox.Show(dif.ToString());
            //check totalPrice cell is readonly or not


            row.Cells["Column24"].Value = qty;
            //row.Cells["Column26"].Value = price;
            row.Cells["difFromOrder"].Value = dif;
        }

        public int selectRowIm;
        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            selectRowIm = e.RowIndex;
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //try
            //{
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
                        var editQty = new ImEditQty(this, null, dataGridView2.Rows[e.RowIndex].Cells["price"].Value.ToString(), dataGridView2.Rows[e.RowIndex].Cells["difFromOrder"].Value.ToString(), dataGridView2.Rows[e.RowIndex].Cells["originalOrder"].Value.ToString())
                        {
                            textBox1 = { Text = dataGridView2.Rows[e.RowIndex].Cells["Column23"].Value.ToString() },
                            textBox2 = { Text = dataGridView2.Rows[e.RowIndex].Cells["Column24"].Value.ToString() },
                            textBox3 = { Text = dataGridView2.Rows[e.RowIndex].Cells["Column26"].Value.ToString() }
                        };
                        editQty.ShowDialog();
                    }
                }
            //}
            //catch (Exception ex)
            //{
            //    ShowMessage($"ເກີດຂໍ້ຜີດພາດ {ex}", "ຂໍ້ຜິດພາດ");
            //}
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (dataGridView2.RowCount == 0)
            {
                ShowMessage("ທ່ານຍັງບໍ່ໄດ້ເພີ່ມສິນຄ້າໃນການນຳເຂົ້າສິນຄ້າ", "ຂໍ້ຜິດພາດ");
            }
            else
            {
                // Get latest order id
                var query = "SELECT TOP 1 [importId] FROM [POSSALE].[dbo].[import] ORDER BY [importId] DESC";
                var dr = _config.getData(query);

                dr.Read();

                // If order id is null, set it to OD0001
                var importID = dr.HasRows ? $"IM{int.Parse(dr["importId"].ToString().Substring(2)) + 1:D4}" : "IM0001";
                dr.Close();
                _config.closeConnection();

                // Get only number on label2
                var totalOrder = int.Parse(label2.Text.Split(' ')[0].Replace(",", ""));

                // Insert data to import table:  SELECT TOP (1000) [importId]
                //,[whoImport]
                //,[importDate]
                //,[totalImport]
                //,[importFromOrder]
                //,[importStatus]
                //,[totalPriceImport]
                //          FROM[POSSALE].[dbo].[import]
                query = $"INSERT INTO [POSSALE].[dbo].[import] ([importId], [whoImport], [importDate], [totalImport], [importFromOrder], [importStatus], [totalPriceImport]) " +
                        $"VALUES ('{importID}', '{_empId}', '{DateTime.Now:yyyy-MM-dd}', {dataGridView2.RowCount}, '{dataGridView1.CurrentRow.Cells["id1"].Value}', 'ອະນຸມັດ', {totalOrder})";
                _config.setData(query);
                // Insert data to importDetail table
                foreach (DataGridViewRow row in dataGridView2.Rows)
                {
                    // Insert data to importDetail table:  SELECT TOP (1000) [importId]
                    //,[product]
                    //,[importQty]
                    //,[price]
                    //,[totalPrice]
                    //,[difFromOrder]
                    //              FROM[POSSALE].[dbo].[importDetail]
                    query = $"INSERT INTO [POSSALE].[dbo].[importDetail] ([importId], [product], [importQty], [price], [totalPrice], [difFromOrder]) " +
                            $"VALUES ('{importID}', '{row.Cells["Column22"].Value}', {row.Cells["Column24"].Value}, {row.Cells["price"].Value}, {row.Cells["Column26"].Value}, {row.Cells["difFromOrder"].Value})";
                    _config.setData(query);
                }
                // Update order status to 'ອະນຸມັດ'
                query = $"UPDATE [POSSALE].[dbo].[order] SET [orderStatus] = 'ອະນຸມັດ' WHERE [orderId] = '{dataGridView1.Rows[0].Cells["id1"].Value}'";
                _config.setData(query);
                ShowMessage("ການນຳເຂົ້າສິນຄ້າສຳເລັດ", "ສຳເລັດ");
                LoadProducts();
            }

            // FM_Bill fM_Bill = new FM_Bill();
            // fM_Bill.ShowDialog();
        }
    }
}
