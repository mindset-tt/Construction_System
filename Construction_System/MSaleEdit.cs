using CrystalDecisions.CrystalReports.Engine;
using System;
using System.Collections;
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
    public partial class MSaleEdit : Form
    {
        MSale msale;
        private readonly config _config = new config();
        private string _empId;
        private string _sellId;
        private string query;
        public MSaleEdit(MSale msale, string sellId, string empId)
        {
            InitializeComponent();
            this.msale = msale;
            _empId = empId;
            _sellId = sellId;
            label3.Text = "0 ກີບ";
        }

        public void updateQty(int qty, int price, string id)
        {
            //DataGridViewRow rowUp = new DataGridViewRow();
            //rowUp = dataGridView2.Rows[selectRowSale];
            //rowUp.Cells["Column24"].Value = qty;
            //rowUp.Cells["Column26"].Value = price;
            foreach (DataGridViewRow row in dataGridView2.Rows)
            {
                if (row.Cells["id2"].Value.ToString() == id)
                {
                    row.Cells["Column24"].Value = qty;
                    row.Cells["Column26"].Value = price;
                }
            }

        }

        private void LoadProducts(string filter = "WHERE p.[cancel] = 'no' ")
        {
           query = $"SELECT p.[prodID],p.[prodName], p.[prodQty], p.[prodPrice], t.[typeName], u.[unitName]" +
                "FROM " +
                "[POSSALE].[dbo].[product] p " +
                "INNER JOIN [POSSALE].[dbo].[type] t ON p.typeId = t.typeId " +
                "INNER JOIN [POSSALE].[dbo].[unit] u ON p.unitId = u.unitId " +
                filter;
            _config.LoadData(query, dataGridView1);
        }

        private void MSaleEdit_Load(object sender, EventArgs e)
        {
            LoadProducts();
        }

        private void sumQty()
        {
            try
            {
                if (dataGridView2.Rows.Count >= -1)
                {
                    int totalPrice = 0;
                    for (int i = 0; i < dataGridView2.RowCount; i++)
                    {
                        totalPrice += Convert.ToInt32(dataGridView2.Rows[i].Cells["Column26"].Value.ToString());
                    }
                    label2.Text = totalPrice.ToString("#,###") + " ກີບ";
                }
                else
                {
                    label2.Text = "0" + " ກີບ";
                }
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
                SEditQty editQty = new SEditQty(null, this, dataGridView1.CurrentRow.Cells["column13"].Value.ToString(), true);
                var senderGrid1 = (DataGridView)sender;
                if (senderGrid1.Columns[e.ColumnIndex] is DataGridViewImageColumn &&
                    e.RowIndex >= 0)
                {
                    //TODO - Button Clicked - Execute Code Here
                    //MyMessageBox.ShowMessage(dataGridView2.Rows[e.RowIndex].Cells[3].Value.ToString(), "", "ຍິນດີຕ້ອນຮັບ", MessageBoxButtons.OK, MessageBoxIcon.None);
                    editQty.label1.Text = " ເພີ່ມຈຳນວນຂາຍສິນຄ້າ";
                    editQty.label1.Image = Construction_System.Properties.Resources.add_button;
                    editQty.button1.Text = "ເພີ່ມ";
                    editQty.textBox1.Text = dataGridView1.Rows[e.RowIndex].Cells["Column12"].Value.ToString();
                    editQty.lblUnit.Text = dataGridView1.Rows[e.RowIndex].Cells["Column14"].Value.ToString();
                    editQty.lblPrice.Text = dataGridView1.Rows[e.RowIndex].Cells["Column16"].Value.ToString();
                    editQty.lblId.Text = dataGridView1.Rows[e.RowIndex].Cells["id1"].Value.ToString();
                    editQty.ShowDialog();
                    dataGridView1.ClearSelection();
                    //dataGridView2.Columns["Column24"].DefaultCellStyle.Format = "#,###";
                    //dataGridView2.Columns["Column26"].DefaultCellStyle.Format = "#,###";
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
            dataGridView1.ClearSelection();
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                SEditQty editQty = new SEditQty(null, this, dataGridView1.CurrentRow.Cells["column13"].Value.ToString(), false);
                var senderGrid = (DataGridView)sender;

                if (senderGrid.Columns[e.ColumnIndex] is DataGridViewImageColumn &&
                e.RowIndex >= 0 && dataGridView2.Columns[e.ColumnIndex].HeaderCell.Value.ToString() == "ລົບ")
                {
                    int deleteQty = int.Parse(dataGridView2.Rows[e.RowIndex].Cells["Column24"].Value.ToString());
                    //TODO - Button Clicked - Execute Code Here
                    dataGridView2.Rows.RemoveAt(dataGridView2.Rows[e.RowIndex].Index);

                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        if (row.Cells["id1"].Value.ToString() == dataGridView2.Rows[e.RowIndex].Cells["id2"].Value.ToString())
                        {
                            int qty = int.Parse(row.Cells["Column13"].Value.ToString());
                            row.Cells["Column13"].Value = qty + deleteQty;
                        }
                    }

                    sumQty();
                }

                if (senderGrid.Columns[e.ColumnIndex] is DataGridViewImageColumn &&
                    e.RowIndex >= 0 && dataGridView2.Columns[e.ColumnIndex].HeaderCell.Value.ToString() == "ແກ້ໄຂ")
                {
                    //TODO - Button Clicked - Execute Code Here
                    editQty.label1.Text = "ແກ້ໄຂຈຳນວນຂາຍສິນຄ້າ";
                    editQty.label1.Image = Construction_System.Properties.Resources.pencil;
                    editQty.button1.Text = "ແກ້ໄຂ";
                    editQty.textBox1.Text = dataGridView2.Rows[e.RowIndex].Cells["Column23"].Value.ToString();
                    editQty.textBox2.Text = dataGridView2.Rows[e.RowIndex].Cells["Column24"].Value.ToString();
                    editQty.lblQtyEdit.Text = dataGridView2.Rows[e.RowIndex].Cells["Column24"].Value.ToString();
                    editQty.lblPrice.Text = dataGridView2.Rows[e.RowIndex].Cells["Column26"].Value.ToString();
                    editQty.lblId.Text = dataGridView2.Rows[e.RowIndex].Cells["id2"].Value.ToString();
                    editQty.ShowDialog();
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

        private void dataGridView2_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            dataGridView2.Columns["Column24"].DefaultCellStyle.Format = "#,###";
            dataGridView2.Columns["Column26"].DefaultCellStyle.Format = "#,###";
            sumQty();
            dataGridView2.ClearSelection();
        }

        public void updateQtyMS(int qty, int price)
        {
            DataGridViewRow rowUp = new DataGridViewRow();
            rowUp = dataGridView2.Rows[selectRowSale];
            rowUp.Cells["Column24"].Value = qty;
            rowUp.Cells["Column26"].Value = price;
        }

        public int selectRowSale;

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            selectRowSale = e.RowIndex;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                //(dataGridView1.DataSource as DataTable).DefaultView.RowFilter = string.Format("name LIKE '%{0}%' or unit LIKE '%{0}%' or type LIKE '%{0}%'", textBox1.Text);
                var filter = string.IsNullOrEmpty(textBox1.Text)
                            ? "WHERE cancel = 'no' and p.prodQty > 0"
                            : $"WHERE (p.prodName LIKE '%{textBox1.Text}%' OR p.prodQty LIKE '%{textBox1.Text}%' OR p.prodPrice LIKE '%{textBox1.Text}%' OR t.typeName LIKE '%{textBox1.Text}%' OR u.unitName LIKE '%{textBox1.Text}%') AND (cancel = 'no' and p.prodQty > 0)";
                LoadProducts(filter);
            }
            catch (Exception ex)
            {

                MyMessageBox.ShowMessage("ເກີດຂໍ້ຜີດພາດ " + ex + " ", "", "ເກີດຂໍ້ຜີດພາດ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try {
                //delete all data in sell detail
                _config.setData($"DELETE FROM [POSSALE].[dbo].[selldetail] WHERE sellId = '{_sellId}'");
                //insert new data
                int getQty = 0;

                foreach (DataGridViewRow row in dataGridView2.Rows)
                {
                    //query = $"SELECT prodQty FROM [POSSALE].[dbo].[product] WHERE prodID = '{row.Cells["id2"].Value}'";
                    //var dr1 = _config.getData(query);
                    //dr1.Read();
                    int qtyFromProduct = 0;
                    //dr1.Close();
                    //_config.closeConnection();

                    foreach (DataGridViewRow row1 in dataGridView1.Rows)
                    {
                        if (row1.Cells["id1"].Value.ToString() == row.Cells["id2"].Value.ToString())
                        {
                            qtyFromProduct = int.Parse(row1.Cells["Column13"].Value.ToString());
                        }
                    }

                    query = $"INSERT INTO [POSSALE].[dbo].[sellDetail] ([sellId], [product], [price], [sellQty], [totalPrice]) " +
                        $"VALUES ('{_sellId}', '{row.Cells["id2"].Value}', {int.Parse(row.Cells["Column26"].Value.ToString()) / int.Parse(row.Cells["Column24"].Value.ToString())}, {row.Cells["Column24"].Value}, {int.Parse(row.Cells["Column26"].Value.ToString())})";
                    _config.setData(query);

                    query = $"UPDATE [POSSALE].[dbo].[product] SET prodQty = {qtyFromProduct - int.Parse(row.Cells["Column24"].Value.ToString())} WHERE prodID = '{row.Cells["id2"].Value}'";
                    _config.setData(query);

                    getQty += int.Parse(row.Cells["Column24"].Value.ToString());
                }

                //update total sell in sell table
                query = $"UPDATE [POSSALE].[dbo].[sell] SET totalSell = {getQty}, totalPriceSell = {int.Parse(label2.Text.Split(' ')[0].Replace(",", ""))} WHERE sellId = '{_sellId}'";
                _config.setData(query);

                MyMessageBox.ShowMessage("ບັນທຶກການຂາຍສິນຄ້າສຳເລັດແລ້ວ", "", "ສຳເລັດ", MessageBoxButtons.OK, MessageBoxIcon.None);

                FM_Bill fM_Bill = new FM_Bill();
                SaleBill orderBill = new SaleBill();
                orderBill.SetParameterValue("Sellid", _sellId);
                fM_Bill.crystalReportViewer1.Refresh();
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
