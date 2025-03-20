// ✅ Full, fixed, and longegrated version of MImportEdit with ImEditQty longegration
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using System;

namespace Construction_System
{
    public partial class MImportEdit : Form
    {
        MImport mImport;
        private readonly config _config = new config();
        private string _empId;
        private string _order;
        private string _importId;

        public MImportEdit(MImport mImport, string order, string empId, string importId)
        {
            InitializeComponent();
            this.mImport = mImport;
            label3.Text = "0 ກີບ";
            _empId = empId;
            _order = order;
            _importId = importId;
        }

        private void btnClose_Click(object sender, EventArgs e) => this.Close();

        private void sumQty()
        {
            try
            {
                long totalPrice = dataGridView2.Rows.Cast<DataGridViewRow>()
                    .Where(r => r.Cells["Column26"].Value != null)
                    .Sum(r => Convert.ToInt64(r.Cells["Column26"].Value));

                label3.Text = totalPrice.ToString("#,###") + " ກີບ";
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowMessage($"ເກີດຂໍ້ຜີດພາດ {ex}", "", "ເກີດຂໍ້ຜີດພາດ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadOrderDetailId(bool isFirstTime)
        {
            var query = $"SELECT p.[prodId], p.[prodName], o.[orderQty], o.[orderQty] as [orderQtys], o.[orderQty] as [orderQtyss], " +
                        "u.[unitName], p.[prodPrice] as [prodPriceOrder], t.[typeName] " +
                        "FROM [POSSALE].[dbo].[product] p " +
                        "INNER JOIN [POSSALE].[dbo].[orderDetail] o ON p.prodId = o.productId " +
                        "INNER JOIN [POSSALE].[dbo].[unit] u ON p.unitId = u.unitId " +
                        "INNER JOIN [POSSALE].[dbo].[type] t ON p.typeId = t.typeId " +
                        $"WHERE o.orderId = '{_order}'";

            _config.LoadData(query, dataGridView1);

            if (!isFirstTime)
            {
                Dictionary<string, long> importedQtyDict = new Dictionary<string, long>();
                foreach (DataGridViewRow row in dataGridView2.Rows)
                {
                    string productId = row.Cells["id2"].Value.ToString();
                    long importQty = long.Parse(row.Cells["Column24"].Value.ToString());
                    if (importedQtyDict.ContainsKey(productId))
                        importedQtyDict[productId] += importQty;
                    else
                        importedQtyDict[productId] = importQty;
                }

                for (int i = dataGridView1.RowCount - 1; i >= 0; i--)
                {
                    string prodId = dataGridView1.Rows[i].Cells["id1"].Value.ToString();
                    long orderQty = long.Parse(dataGridView1.Rows[i].Cells["Column13"].Value.ToString());

                    if (importedQtyDict.TryGetValue(prodId, out long importedQty))
                    {
                        if (importedQty >= orderQty)
                        {
                            dataGridView1.Rows.RemoveAt(i);
                        }
                        else
                        {
                            dataGridView1.Rows[i].Cells["Column13"].Value = orderQty - importedQty;
                        }
                    }
                }
            }
        }

        private void LoadImportDetailId()
        {
            var query = $"SELECT p.[prodId], p.[prodName], i.[importQty], i.[difFromOrder] as [importQtys], " +
                        "i.[importQty] as [importQtyss], u.[unitName], (p.[prodPrice] * i.[importQty]) as [totalPrice], p.[prodPrice] " +
                        "FROM [POSSALE].[dbo].[product] p " +
                        "INNER JOIN [POSSALE].[dbo].[importDetail] i ON p.prodId = i.product " +
                        "INNER JOIN [POSSALE].[dbo].[unit] u ON p.unitId = u.unitId " +
                        $"WHERE i.importId = '{_importId}'";

            _config.LoadData(query, dataGridView2);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                (dataGridView1.DataSource as DataTable).DefaultView.RowFilter = $"prodName LIKE '%{textBox1.Text}%' OR unitName LIKE '%{textBox1.Text}%'";
        }
            catch (Exception ex)
            {
                MyMessageBox.ShowMessage($"ເກີດຂໍ້ຜີດພາດ {ex}", "", "ເກີດຂໍ້ຜີດພາດ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
}

        private void MImportEdit_Load(object sender, EventArgs e)
        {
            LoadOrderDetailId(true);
            LoadImportDetailId();
            LoadOrderDetailId(false); // Re-apply to adjust based on loaded import
        }

        private void dataGridView1_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            dataGridView1.ClearSelection();
        }

        private void dataGridView1_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            dataGridView1.Columns["Column13"].DefaultCellStyle.Format = "#,###";
            dataGridView1.ClearSelection();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0) return;
                if (dataGridView1.Columns[e.ColumnIndex] is DataGridViewImageColumn)
                {
                    var editQty = new ImEditQty(null, this,
                        dataGridView1.Rows[e.RowIndex].Cells["prodPriceOrder"].Value.ToString(),
                        dataGridView1.Rows[e.RowIndex].Cells["orderQtys"].Value.ToString(),
                        dataGridView1.Rows[e.RowIndex].Cells["orderQtyss"].Value.ToString()
                    );

                    editQty.label1.Text = " ຈັດການເພີ່ມຈຳນວນນຳເຂົ້າສິນຄ້າ";
                    editQty.textBox1.Text = dataGridView1.Rows[e.RowIndex].Cells["Column12"].Value.ToString();
                    editQty.textBox2.Text = dataGridView1.Rows[e.RowIndex].Cells["Column13"].Value.ToString();
                    editQty.lblUnit.Text = dataGridView1.Rows[e.RowIndex].Cells["Column14"].Value.ToString();
                    editQty.lblId.Text = dataGridView1.Rows[e.RowIndex].Cells["id1"].Value.ToString();
                    //editQty.label5.Text = _order;
                    editQty.ShowDialog();
                    dataGridView1.ClearSelection();
                }
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowMessage($"ເກີດຂໍ້ຜີດພາດ {ex}", "", "ເກີດຂໍ້ຜີດພາດ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void updateQty(long qty, long price)
        {
            if (selectRowIm >= 0 && selectRowIm < dataGridView2.Rows.Count)
            {
                var rowUp = dataGridView2.Rows[selectRowIm];
                rowUp.Cells["Column24"].Value = qty;
                rowUp.Cells["Column26"].Value = price;
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
                if (e.RowIndex < 0) return;

                var senderGrid = (DataGridView)sender;
                if (senderGrid.Columns[e.ColumnIndex] is DataGridViewImageColumn)
                {
                    string action = dataGridView2.Columns[e.ColumnIndex].HeaderText;

                    if (action == "ລົບ")
                    {
                        string productId = dataGridView2.Rows[e.RowIndex].Cells["id2"].Value.ToString();
                        long deleteQty = long.Parse(dataGridView2.Rows[e.RowIndex].Cells["Column24"].Value.ToString());

                        dataGridView2.Rows.RemoveAt(e.RowIndex);
                        sumQty();

                        // Return product back to order grid if necessary
                        bool found = false;
                        foreach (DataGridViewRow row in dataGridView1.Rows)
                        {
                            if (row.Cells["id1"].Value.ToString() == productId)
                            {
                                long currentQty = long.Parse(row.Cells["Column13"].Value.ToString());
                                row.Cells["Column13"].Value = currentQty + deleteQty;
                                found = true;
                                break;
                            }
                        }
                        if (!found)
                            LoadOrderDetailId(false);
                    }
                    else if (action == "ແກ້ໄຂ")
                    {
                        ImEditQty editQty = new ImEditQty(null, this,
                            dataGridView2.Rows[e.RowIndex].Cells["prodPrice"].Value.ToString(),
                            dataGridView2.Rows[e.RowIndex].Cells["importQtys"].Value.ToString(),
                            dataGridView2.Rows[e.RowIndex].Cells["importQtyss"].Value.ToString()
                        );

                        editQty.label1.Text = "ຈັດການແກ້ໄຂຈຳນວນນຳເຂົ້າສິນຄ້າ";
                        editQty.textBox1.Text = dataGridView2.Rows[e.RowIndex].Cells["Column23"].Value.ToString();
                        editQty.textBox2.Text = dataGridView2.Rows[e.RowIndex].Cells["Column24"].Value.ToString();
                        //editQty.textBox3.Text = dataGridView2.Rows[e.RowIndex].Cells["Column26"].Value.ToString();
                        editQty.lblId.Text = dataGridView2.Rows[e.RowIndex].Cells["id2"].Value.ToString();
                        editQty.ShowDialog();
                    }
                }
        }
            catch (Exception ex)
            {
                MyMessageBox.ShowMessage($"ເກີດຂໍ້ຜີດພາດ {ex}", "", "ເກີດຂໍ້ຜີດພາດ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                var item = label3.Text.Split(' ')[0].Replace(",", "");
                if (item == "" || item == "0")
                {
                    MyMessageBox.ShowMessage("ກະລຸນາເພີ່ມຈຳນວນນຳເຂົ້າສິນຄ້າກ່ອນ", "", "ກວດສອບ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                _config.setData($"DELETE FROM [POSSALE].[dbo].[importDetail] WHERE importId = '{_importId}'");

                foreach (DataGridViewRow row in dataGridView2.Rows)
                {
                    string query = $"INSERT INTO [POSSALE].[dbo].[importDetail] ([importId], [product], [importQty], [difFromOrder], [price], [totalPrice]) " +
                                   $"VALUES ('{_importId}', '{row.Cells["id2"].Value}', '{row.Cells["Column24"].Value}', '{row.Cells["importQtys"].Value}', '{row.Cells["prodPrice"].Value}', '{row.Cells["Column26"].Value}')";
                    _config.setData(query);
                }

                _config.setData($"UPDATE i " +
                $"SET i.totalImport = ( " +
                $"    SELECT SUM(d.importQty * p.prodPrice) " +
                $"    FROM [POSSALE].[dbo].[importDetail] d " +
                $"    INNER JOIN [POSSALE].[dbo].[product] p ON p.prodId = d.product " +
                $"    WHERE d.importId = i.importId " +
                $") " +
                $"FROM [POSSALE].[dbo].[import] i " +
                $"WHERE i.importId = '{_importId}';");


                //foreach (DataGridViewRow row in dataGridView2.Rows)
                //{
                //    //string query = $"UPDATE [POSSALE].[dbo].[orderDetail] SET [orderQty] = [orderQty] - {row.Cells["Column24"].Value} " +
                //    //               $"WHERE productId = '{row.Cells["id2"].Value}' AND orderId = '{_order}'";
                //    //_config.setData(query);
                //}

                //_config.setData($"UPDATE [POSSALE].[dbo].[order] SET [orderStatus] = 'ອະນຸມັດ' " +
                //                $"WHERE orderId = '{_order}' AND totalOrder = (SELECT totalImport FROM [POSSALE].[dbo].[import] WHERE importId = '{_importId}')");

                MyMessageBox.ShowMessage("ບັນທຶກການນຳເຂົ້າສິນຄ້າສຳເລັດແລ້ວ", "", "ສຳເລັດ", MessageBoxButtons.OK, MessageBoxIcon.None);
                FM_Bill fM_Bill = new FM_Bill();
                ImportBill importBill = new ImportBill();
                importBill.Refresh();
                importBill.SetParameterValue("importId", _importId);
                fM_Bill.crystalReportViewer1.ReportSource = importBill;
                fM_Bill.ShowDialog();
                this.Close();
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowMessage($"ເກີດຂໍ້ຜີດພາດ {ex}", "", "ເກີດຂໍ້ຜີດພາດ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}