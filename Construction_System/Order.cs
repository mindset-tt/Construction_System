using CrystalDecisions.CrystalReports.Engine;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
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

        //private void LoadData()
        //{
        //    LoadSuppliers();
        //    LoadProducts();
        //}

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
                    MyMessageBox.ShowMessage("ກະລຸນາເລືອກຂໍ້ມູນຜູ້ສະໜອງ", "", "ແຈ້ງເຕືອນ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    dataGridView1.DataSource = null;
                }
                else
                {
                    var filter = string.IsNullOrEmpty(textBox1.Text) && (comboBox1.SelectedValue.ToString() != "-1")
                            ? $"WHERE sp.supplierId LIKE '%{comboBox1.SelectedValue}%' AND p.cancel = 'no' "
                            : string.IsNullOrEmpty(textBox1.Text) || comboBox1.Text == "ກະລຸນາເລືອກ"
                            ? "WHERE p.cancel = 'none'"
                            : $"WHERE (p.prodID LIKE '%{textBox1.Text}%' OR p.prodName LIKE '%{textBox1.Text}%' " +
                            $"OR p.prodQty LIKE '%{textBox1.Text}%' OR p.prodPrice LIKE '%{textBox1.Text}%' " +
                            $"OR p.typeId LIKE '%{textBox1.Text}%' OR p.unitId LIKE '%{textBox1.Text}%') AND " +
                            $"sp.supplierId LIKE '%{comboBox1.SelectedValue}%' AND p.cancel = 'no' ";
                    LoadProducts(filter);
                }
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowMessage("ເກີດຂໍ້ຜີດພາດ " + ex + " ", "", "ເກີດຂໍ້ຜີດພາດ", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(comboBox1.Text) || comboBox1.Text == "ກະລຸນາເລືອກ")
                {
                    MyMessageBox.ShowMessage("ກະລຸນາເລືອກຂໍ້ມູນຜູ້ສະໜອງ", "", "ແຈ້ງເຕືອນ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    var filter = "WHERE p.cancel = 'none'";
                    dataGridView2.Rows.Clear();
                    //dataGridView1.DataSource = null;
                    label2.Text = "0   ອັນ";
                    LoadProducts(filter);
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
                MyMessageBox.ShowMessage("ເກີດຂໍ້ຜີດພາດ " + ex + " ", "", "ເກີດຂໍ້ຜີດພາດ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void sumQty()
        {
            try
            {
                if (dataGridView2.Rows.Count != 0)
                {
                    var totalQty = dataGridView2.Rows.Cast<DataGridViewRow>()
                        .Sum(row => Convert.ToInt64(row.Cells["Column24"].Value));
                    label2.Text = $"{totalQty:#,###}   ອັນ";
                }
                else
                {
                    label2.Text = "0" + "   ອັນ";
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
                    var editQty = new OrEditQty(this, null, dataGridView1.Rows[e.RowIndex].Cells["Column8"].Value.ToString(), true, false, null)
                    {
                        label1 = { Text = " ເພີ່ມຈໍານວນສັ່ງຊື້ສິນຄ້າ", Image = Construction_System.Properties.Resources.add_button },
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
                        var editQty = new OrEditQty(this, null, null, true, true, null)
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

        public long selectRowOr;
        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            selectRowOr = e.RowIndex;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedValue.ToString() == "-1")
            {
                MyMessageBox.ShowMessage("ກະລຸນາເລືອກຂໍ້ມູນຜູ້ສະໜອງ", "", "ແຈ້ງເຕືອນ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (dataGridView2.RowCount == 0)
            {
                MyMessageBox.ShowMessage("ຂໍອະໄພ, ກະລຸນາເພີ່ມລາຍການສິນຄ້າກ່ອນສັ່ງຊື້ສິນຄ້າ", "", "ເກີດຂໍ້ຜີດພາດ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            foreach (DataGridViewRow row in dataGridView2.Rows)
            {
                // Check idSup in dataGridView2 is content in comboBox1.SelectedValue
                if (!row.Cells["idSup"].Value.ToString().Contains(comboBox1.SelectedValue.ToString()))
                {
                    MyMessageBox.ShowMessage("ຂໍອະໄພ, ຂໍ້ມູນຜູ້ສະໜອງບໍ່ຖືກຕ້ອງ ", "", "ເກີດຂໍ້ຜີດພາດ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            // Get latest order id
            var query = "SELECT TOP 1 orderId FROM [POSSALE].[dbo].[order] ORDER BY orderId DESC";
            var dr = _config.getData(query);

            dr.Read();

            // If order id is null, set it to OD0001
            var orderId = dr.HasRows ? $"OD{long.Parse(dr["orderId"].ToString().Substring(2)) + 1:D4}" : "OD0001";
            dr.Close();
            _config.closeConnection();

            // Get only number on label2
            var totalOrder = long.Parse(label2.Text.Split(' ')[0].Replace(",", ""));
            _config.setData("INSERT INTO [POSSALE].[dbo].[order] ([orderId], [whoOrder], [orderDate], [totalOrder], [orderFrom], [orderStatus]) " +
                           $"VALUES ('{orderId}','{_empId}', GETDATE(), {totalOrder}, '{comboBox1.SelectedValue}', 'ສັ່ງຊື້ແລ້ວ')");

            foreach (DataGridViewRow row in dataGridView2.Rows)
            {
                var productId = row.Cells["id2"].Value.ToString();
                var checkProductQuery = $"SELECT COUNT(*) FROM [POSSALE].[dbo].[product] WHERE prodID = '{productId}'";

                var dr2 = _config.getData(checkProductQuery);
                dr2.Read();
                Console.WriteLine(dr2[0]);
                var productExists = Convert.ToInt64(dr2[0]) > 0;
                dr2.Close();

                if (!productExists)
                {
                    MyMessageBox.ShowMessage($"ຂໍອະໄພ, ເກີດຂໍ້ຜີດພາດ Product ID {productId} does not exist in the product table.", "", "ເກີດຂໍ້ຜີດພາດ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    _config.closeConnection();
                    return;
                }

                _config.closeConnection();

                _config.setData("INSERT INTO [POSSALE].[dbo].[orderDetail] ([orderId], [productId], [orderQty]) " +
                               $"VALUES ('{orderId}', '{productId}', {row.Cells["Column24"].Value})");
            }
            MyMessageBox.ShowMessage("ການສັ່ງຊື້ສິນຄ້າສຳເລັດ", "", "ສຳເລັດ", MessageBoxButtons.OK, MessageBoxIcon.None);

            //dataGridView2.DataSource = null;
            //clear the combobox
            //LoadProducts();
            FM_Bill fM_Bill = new FM_Bill();
            OrderBill orderBill = new OrderBill();
            orderBill.Refresh();
            orderBill.SetParameterValue("OrderID", orderId);
            fM_Bill.crystalReportViewer1.Refresh();
            fM_Bill.crystalReportViewer1.ReportSource = orderBill;
            fM_Bill.ShowDialog();
            label2.Text = "0   ອັນ";
            comboBox1.SelectedIndex = 0;
            dataGridView2.Rows.Clear();
            //dataGridView1.Rows.Clear();
        }
    }
}
