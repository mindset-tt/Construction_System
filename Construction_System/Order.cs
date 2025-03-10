using CrystalDecisions.CrystalReports.Engine;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;

namespace Construction_System
{
    public partial class Order : Form
    {
        private readonly config _config = new config();
        private string _empId;

        public Order(string empID)
        {
            InitializeComponent();
            label2.Text = "0   ອັນ";
            //LoadData();
            LoadSuppliers();
            _empId = empID;
        }

        private void LoadData()
        {
            LoadSuppliers();
            LoadProducts();
        }

        private void LoadSuppliers()
        {
            var query = "SELECT sp.[supplierId], s.[supplierName] FROM [POSSALE].[dbo].[supplierDetail] sp " +
                        "INNER JOIN [POSSALE].[dbo].[supplier] s ON s.supplierId = sp.supplierId " +
                        "GROUP BY sp.[supplierId], s.[supplierName]";
            _config.LoadData(query, comboBox1, "supplierId", "supplierName", "ກະລຸນາເລືອກ");
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
            filter +
            "GROUP BY p.[prodID], CAST(p.[prodName] AS NVARCHAR(MAX)), p.[prodQty], p.[prodPrice], t.[typeName], u.[unitName]";
            _config.LoadData(query, dataGridView1);
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(comboBox1.Text))
                {
                    //ShowMessage("ກະລຸນາເລືອກຂໍ້ມູນຜູ້ສະໜອງ");
                    ShowMessage("ກະລຸນາເລືອກຂໍ້ມູນຜູ້ສະໜອງ", "ຂໍ້ຜິດພາດ");
                    dataGridView1.DataSource = null;
                }
                else
                {
                    var filter = string.IsNullOrEmpty(textBox1.Text) && (comboBox1.SelectedValue.ToString() != "-1")
                            ? $"WHERE sp.supplierId LIKE '%{comboBox1.SelectedValue}%' AND p.cancel = 'no'"
                            : string.IsNullOrEmpty(textBox1.Text) || comboBox1.Text == "ກະລຸນາເລືອກ"
                            ? "WHERE p.cancel = 'yes'"
                            : $"WHERE (p.prodID LIKE '%{textBox1.Text}%' OR p.prodName LIKE '%{textBox1.Text}%' " +
                            $"OR p.prodQty LIKE '%{textBox1.Text}%' OR p.prodPrice LIKE '%{textBox1.Text}%' " +
                            $"OR p.typeId LIKE '%{textBox1.Text}%' OR p.unitId LIKE '%{textBox1.Text}%') AND " +
                            $"sp.supplierId LIKE '%{comboBox1.SelectedValue}%' AND p.cancel = 'yes'";
                    LoadProducts(filter);
                }
            }
            catch (Exception ex)
            {
                ShowMessage($"ສິ່ງທີ່ຄົ້ນຫາບໍ່ຖືກຕ້ອງ {ex}", "ຂໍ້ຜິດພາດ");
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(comboBox1.Text) || comboBox1.Text == "ກະລຸນາເລືອກ")
                {
                    ShowMessage("ກະລຸນາເລືອກຂໍ້ມູນຜູ້ສະໜອງ", "ຂໍ້ຜິດພາດ");
                    dataGridView2.Rows.Clear();
                    label2.Text = "0   ອັນ";
                    LoadProducts();
                }
                else
                {
                    var filter = $"WHERE sp.supplierId LIKE '%{comboBox1.SelectedValue}%'";
                    dataGridView2.Rows.Clear();
                    label2.Text = "0   ອັນ";
                    LoadProducts(filter);
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

        private void sumQty()
        {
            try
            {
                var totalQty = dataGridView2.Rows.Cast<DataGridViewRow>()
                    .Sum(row => Convert.ToInt32(row.Cells["Column24"].Value));
                label2.Text = $"{totalQty:#,###}   ອັນ";
            }
            catch (Exception ex)
            {
                //ShowMessage($"ເກີດຂໍ້ຜີດພາດ {ex}");
                ShowMessage($"ເກີດຂໍ້ຜີດພາດ {ex}", "ຂໍ້ຜິດພາດ");
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
        }

        private void dataGridView2_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            dataGridView1.ClearSelection();
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
                if (dataGridView1.Columns[e.ColumnIndex] is DataGridViewImageColumn && e.RowIndex >= 0)
                {
                    var editQty = new OrEditQty(this, null, dataGridView1.Rows[e.RowIndex].Cells["Column8"].Value.ToString(), true)
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
                ShowMessage($"ເກີດຂໍ້ຜີດພາດ {ex}", "ຂໍ້ຜິດພາດ");
            }
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
                        sumQty();
                    }
                    else if (dataGridView2.Columns[e.ColumnIndex].HeaderCell.Value.ToString() == "ແກ້ໄຂ")
                    {
                        var editQty = new OrEditQty(this, null, null, true)
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
                ShowMessage($"ເກີດຂໍ້ຜີດພາດ {ex}", "ຂໍ້ຜິດພາດ");
            }
        }

        public void updateQty(int qty, string proId)
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

        public int selectRowOr;
        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            selectRowOr = e.RowIndex;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedValue.ToString() == "-1")
            {
                ShowMessage("ກະລຸນາເລືອກຜູ້ສະໜອງ", "ຂໍ້ຜິດພາດ");
                return;
            }

            if (dataGridView2.RowCount == 0)
            {
                ShowMessage("ທ່ານຍັງບໍ່ໄດ້ເພີ່ມສິນຄ້າໃນການສິ່ງຊື້", "ຂໍ້ຜິດພາດ");
                return;
            }

            foreach (DataGridViewRow row in dataGridView2.Rows)
            {
                // Check idSup in dataGridView2 is content in comboBox1.SelectedValue
                if (!row.Cells["idSup"].Value.ToString().Contains(comboBox1.SelectedValue.ToString()))
                {
                    ShowMessage("ຜູ້ສະໜອງບໍ່ຖືກຕ້ອງ", "ຂໍ້ຜິດພາດ");
                    return;
                }
            }

            // Get latest order id
            var query = "SELECT TOP 1 orderId FROM [POSSALE].[dbo].[order] ORDER BY orderId DESC";
            var dr = _config.getData(query);

            dr.Read();

            // If order id is null, set it to OD0001
            var orderId = dr.HasRows ? $"OD{int.Parse(dr["orderId"].ToString().Substring(2)) + 1:D4}" : "OD0001";
            dr.Close();
            _config.closeConnection();

            // Get only number on label2
            var totalOrder = int.Parse(label2.Text.Split(' ')[0].Replace(",", ""));
            _config.setData("INSERT INTO [POSSALE].[dbo].[order] ([orderId], [whoOrder], [orderDate], [totalOrder], [orderFrom], [orderStatus]) " +
                           $"VALUES ('{orderId}','{_empId}', GETDATE(), {totalOrder}, '{comboBox1.SelectedValue}', 'ສິ່ງຊື້ແລ້ວ')");

            foreach (DataGridViewRow row in dataGridView2.Rows)
            {
                var productId = row.Cells["id2"].Value.ToString();
                var checkProductQuery = $"SELECT COUNT(*) FROM [POSSALE].[dbo].[product] WHERE prodID = '{productId}'";

                // With the following code:
                _config.openConnection();
                var cmd = new SqlCommand(checkProductQuery, _config.con);
                var productExists = (int)cmd.ExecuteScalar() > 0;

                if (!productExists)
                {
                    ShowMessage($"Product ID {productId} does not exist in the product table.", "Error");
                    _config.closeConnection();
                    return;
                }

                _config.closeConnection();

                _config.setData("INSERT INTO [POSSALE].[dbo].[orderDetail] ([orderId], [productId], [orderQty]) " +
                               $"VALUES ('{orderId}', '{productId}', {row.Cells["Column24"].Value})");
            }
            ShowMessage("ສິ່ງຊື້ສຳເລັດແລ້ວ", "ສຳເລັດ");
            
            dataGridView2.Rows.Clear();
            //dataGridView2.DataSource = null;
            label2.Text = "0   ອັນ";
            //clear the combobox
            comboBox1.SelectedIndex = 0;
            LoadProducts();
            FM_Bill fM_Bill = new FM_Bill();
            OrderBill orderBill = new OrderBill();
            orderBill.SetParameterValue("orderId", orderId);
            fM_Bill.crystalReportViewer1.ReportSource = orderBill;
            fM_Bill.ShowDialog();
        }
    }
}
