using CrystalDecisions.CrystalReports.Engine;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace Construction_System
{
    public partial class MSaleEdit : Form
    {
        MSale msale;
        private readonly config _config = new config();
        private string _empId;
        private string _sellId;
        private string query;
        public int selectRowSale;
        public int selectRowIm;
        public DataRow[] deleteRows;
        List<string> addDataList = new List<string>();
        List<string> addDataQty = new List<string>();
        public MSaleEdit(MSale msale, string sellId, string empId)
        {
            InitializeComponent();
            this.msale = msale;
            _empId = empId;
            _sellId = sellId;
            label3.Text = "0 ກີບ";
        }

        // Example method to delete a row and store it
        public void DeleteRowAndStore(int rowIndex)
        {
            if (dataGridView1.DataSource is DataTable dt && rowIndex >= 0)
            {
                // Store deleted row before removing
                deleteRows = new DataRow[] { dt.Rows[rowIndex] };
                dt.Rows.RemoveAt(rowIndex);
            }
        }

        private void MSaleEdit_Load(object sender, EventArgs e)
        {
            LoadProducts();
            LoadSaleDetail();
        }

        private void LoadProducts(string filter = "WHERE p.[cancel] = 'no' AND p.prodQty > 0")
        {
            query = $"SELECT p.[prodID], p.[prodName], p.[prodQty], p.[prodPrice], t.[typeName], u.[unitName], p.[prodQty] as [saleQtys]" +
                    $"FROM [POSSALE].[dbo].[product] p " +
                    $"INNER JOIN [POSSALE].[dbo].[type] t ON p.typeId = t.typeId " +
                    $"INNER JOIN [POSSALE].[dbo].[unit] u ON p.unitId = u.unitId " +
                    filter;
            _config.LoadData(query, dataGridView1);
        }

        private void LoadSaleDetail()
        {
            var query = $"SELECT p.[prodID], p.[prodName], s.[sellQty], (s.[sellQty] + p.[prodQty]) as [sellQtyss], " +
                        $"u.[unitName], (p.[prodPrice] * s.[sellQty]) as [totalPrice], p.[prodPrice] " +
                        $"FROM [POSSALE].[dbo].[product] p " +  
                        $"INNER JOIN [POSSALE].[dbo].[sellDetail] s ON p.prodID = s.product " +
                        $"INNER JOIN [POSSALE].[dbo].[unit] u ON p.unitId = u.unitId " +
                        $"WHERE s.sellId = '{_sellId}'";
            _config.LoadData(query, dataGridView2);
        }

        public void sumQty()
        {
            try
            {
                long totalPrice = 0;
                foreach (DataGridViewRow row in dataGridView2.Rows)
                {
                    if (!row.IsNewRow)
                        totalPrice += Convert.ToInt64(row.Cells["Column26"].Value);
                }
                label3.Text = totalPrice.ToString("#,###") + " ກີບ";
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowMessage("ເກີດຂໍ້ຜີດພາດ " + ex, "", "ຜິດພາດ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void updateQty(long qty, long price)
        {
            if (selectRowSale >= 0)
            {
                dataGridView2.Rows[selectRowSale].Cells["Column24"].Value = qty;
                dataGridView2.Rows[selectRowSale].Cells["Column26"].Value = price;
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                (dataGridView1.DataSource as DataTable).DefaultView.RowFilter = string.Format("prodName LIKE '%{0}%' OR prodID LIKE '%{0}%' OR typeName LIKE '%{0}%' OR unitName LIKE '%{0}%'", textBox1.Text);
                sumQty();
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowMessage("ເກີດຂໍ້ຜີດພາດ " + ex, "", "ຜິດພາດ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dataGridView1.Columns[e.ColumnIndex] is DataGridViewImageColumn && e.RowIndex >= 0)
                {
                    SEditQty editQty = new SEditQty(null, this, dataGridView1.Rows[e.RowIndex].Cells["saleQtys"].Value.ToString(), true);
                    editQty.label1.Text = "ເພີ່ມຈຳນວນຂາຍສິນຄ້າ";
                    editQty.label1.Image = Construction_System.Properties.Resources.add_button;
                    editQty.button1.Text = "ເພີ່ມ";
                    editQty.textBox1.Text = dataGridView1.Rows[e.RowIndex].Cells["Column12"].Value.ToString();
                    editQty.lblUnit.Text = dataGridView1.Rows[e.RowIndex].Cells["Column14"].Value.ToString();
                    editQty.lblPrice.Text = dataGridView1.Rows[e.RowIndex].Cells["Column16"].Value.ToString();
                    editQty.lblId.Text = dataGridView1.Rows[e.RowIndex].Cells["id1"].Value.ToString();
                    editQty.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowMessage("ເກີດຂໍ້ຜີດພາດ " + ex, "", "ຜິດພາດ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridView1_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e) => dataGridView1.ClearSelection();

        private void dataGridView1_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            dataGridView1.Columns["Column13"].DefaultCellStyle.Format = "#,###";
            dataGridView1.Columns["Column16"].DefaultCellStyle.Format = "#,###";
            dataGridView1.ClearSelection();
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e) => selectRowSale = e.RowIndex;

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                var senderGrid = (DataGridView)sender;
                if (senderGrid.Columns[e.ColumnIndex] is DataGridViewImageColumn && e.RowIndex >= 0)
                {
                    // Delete logic
                    if (dataGridView2.Columns[e.ColumnIndex].HeaderCell.Value.ToString() == "ລົບ")
                    {
                        long deleteQty = Convert.ToInt64(dataGridView2.Rows[e.RowIndex].Cells["Column24"].Value);
                        string productId = dataGridView2.Rows[e.RowIndex].Cells["id2"].Value.ToString();
                        dataGridView2.Rows.RemoveAt(e.RowIndex);
                        sumQty();

                        foreach (DataGridViewRow row in dataGridView1.Rows)
                        {
                            if (row.Cells["id1"].Value.ToString() == productId)
                            {
                                long currentQty = long.Parse(row.Cells["Column13"].Value.ToString());
                                row.Cells["Column13"].Value = currentQty + deleteQty;
                                break;
                            }
                        }
                    }
                    // Edit logic
                    else if (dataGridView2.Columns[e.ColumnIndex].HeaderCell.Value.ToString() == "ແກ້ໄຂ")
                    {
                        SEditQty editQty = new SEditQty(null, this, dataGridView2.Rows[e.RowIndex].Cells["sellQtyss"].Value.ToString(), false);
                        editQty.label1.Text = "ແກ້ໄຂຈຳນວນຂາຍສິນຄ້າ";
                        editQty.label1.Image = Construction_System.Properties.Resources.pencil;
                        editQty.button1.Text = "ແກ້ໄຂ";
                        editQty.textBox1.Text = dataGridView2.Rows[e.RowIndex].Cells["Column23"].Value.ToString();
                        editQty.textBox2.Text = dataGridView2.Rows[e.RowIndex].Cells["Column24"].Value.ToString();
                        editQty.lblQtyEdit.Text = dataGridView2.Rows[e.RowIndex].Cells["Column24"].Value.ToString();
                        editQty.lblPrice.Text = (long.Parse(dataGridView2.Rows[e.RowIndex].Cells["Column26"].Value.ToString()) / long.Parse(dataGridView2.Rows[e.RowIndex].Cells["Column24"].Value.ToString())).ToString();
                        editQty.lblId.Text = dataGridView2.Rows[e.RowIndex].Cells["id2"].Value.ToString();
                        editQty.ShowDialog();
                    }
                }
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowMessage("ເກີດຂໍ້ຜີດພາດ " + ex, "", "ຜິດພາດ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridView2_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e) => dataGridView2.ClearSelection();

        private void dataGridView2_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            dataGridView2.Columns["Column24"].DefaultCellStyle.Format = "#,###";
            dataGridView2.Columns["Column26"].DefaultCellStyle.Format = "#,###";
            sumQty();
            dataGridView2.ClearSelection();
        }

        private void btnClose_Click(object sender, EventArgs e) => Close();

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                var item = label3.Text.Split(' ')[0].Replace(",", "");
                if (item == "" || item == "0")
                { 
                    var query = $"SELECT p.[prodID], p.[prodName], s.[sellQty], (s.[sellQty] + p.[prodQty]) as [sellQtyss], " +
                       $"u.[unitName], (p.[prodPrice] * s.[sellQty]) as [totalPrice], p.[prodPrice] " +
                       $"FROM [POSSALE].[dbo].[product] p " +
                       $"INNER JOIN [POSSALE].[dbo].[sellDetail] s ON p.prodID = s.product " +
                       $"INNER JOIN [POSSALE].[dbo].[unit] u ON p.unitId = u.unitId " +
                       $"WHERE s.sellId = '{_sellId}'";
                    var dr1 = _config.getData(query);
                    //dr1.Read();
                    while (dr1.Read())
                    {
                        addDataList.Add(dr1["prodID"].ToString());
                        addDataQty.Add(dr1["sellQty"].ToString());
                    }
                    dr1.Close();

                    string[] addData = addDataList.ToArray();  // Convert List to string[]
                    string[] addQty = addDataQty.ToArray();  // Convert List to string[]

                    for (int i = 0; i < addData.Length; i++)
                    {
                        _config.setData($"UPDATE [POSSALE].[dbo].[product] SET prodQty = prodQty + {addQty[i]} WHERE prodID = '{addData[i]}'");
                    }

                    _config.setData($"DELETE FROM [POSSALE].[dbo].[sellDetail] WHERE sellId = '{_sellId}'");
                    MyMessageBox.ShowMessage("ຍົກເລິກການຂາຍສິນຄ້າສຳເລັດແລ້ວ", "", "ສຳເລັດ", MessageBoxButtons.OK, MessageBoxIcon.None);
                    Close();
                    return;
                }

               


                _config.setData($"DELETE FROM [POSSALE].[dbo].[sellDetail] WHERE sellId = '{_sellId}'");
                long getQty = 0;

                foreach (DataGridViewRow row in dataGridView2.Rows)
                {
                    if (row.IsNewRow) continue;

                    string productId = row.Cells["id2"].Value.ToString();
                    long sellQty = long.Parse(row.Cells["Column24"].Value.ToString());
                    long totalPrice = long.Parse(row.Cells["Column26"].Value.ToString());
                    long pricePerUnit = totalPrice / sellQty;

                    _config.setData($"INSERT INTO [POSSALE].[dbo].[sellDetail] ([sellId], [product], [price], [sellQty], [totalPrice]) " +
                                    $"VALUES ('{_sellId}', '{productId}', {pricePerUnit}, {sellQty}, {totalPrice})");

                    long qtyFromProduct = 0;
                    foreach (DataGridViewRow prodRow in dataGridView1.Rows)
                    {
                        if (prodRow.Cells["id1"].Value.ToString() == productId)
                        {
                            qtyFromProduct = long.Parse(prodRow.Cells["Column13"].Value.ToString()) + sellQty;
                            break;
                        }
                    }

                    _config.setData($"UPDATE [POSSALE].[dbo].[product] SET prodQty = {qtyFromProduct - sellQty} WHERE prodID = '{productId}'");
                    getQty += sellQty;
                }

                long totalSellPrice = long.Parse(label3.Text.Split(' ')[0].Replace(",", ""));
                _config.setData($"UPDATE [POSSALE].[dbo].[sell] SET totalSell = {getQty}, totalPriceSell = {totalSellPrice} WHERE sellId = '{_sellId}'");

                MyMessageBox.ShowMessage("ບັນທຶກການແກ້ໄຂຂໍ້ມູນການຂາຍສິນຄ້າສຳເລັດແລ້ວ", "", "ສຳເລັດ", MessageBoxButtons.OK, MessageBoxIcon.None);

                FM_Bill fM_Bill = new FM_Bill();
                SaleBill saleBill = new SaleBill();
                saleBill.Refresh();
                saleBill.SetParameterValue("Sellid", _sellId);
                fM_Bill.crystalReportViewer1.Refresh();
                fM_Bill.crystalReportViewer1.ReportSource = saleBill;
                fM_Bill.ShowDialog();
                Close();
                msale.dataSaleBill();
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowMessage("ເກີດຜິດພາດ " + ex, "", "ຜິດພາດ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
