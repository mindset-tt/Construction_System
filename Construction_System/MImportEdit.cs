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
            label3.Text = "0" + " ກີບ";
            _empId = empId;
            _order = order;
            _importId = importId;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
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
                    label3.Text = totalPrice.ToString("#,###") + " ກີບ";
                }
                else
                {
                    label3.Text = "0" + " ກີບ";
                }
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowMessage("ເກີດຂໍ້ຜີດພາດ " + ex + " ", "", "ເກີດຂໍ້ຜີດພາດ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadOrderDetailId(bool isFristTime)
        {
            var query = $"SELECT p.[prodId], p.[prodName], o.[orderQty], o.[orderQty] as [orderQtys], o.[orderQty] as [orderQtyss], u.[unitName], " + $"p.[prodPrice] as [prodPriceOrder], t.[typeName] " + $"FROM [POSSALE].[dbo].[product] p " + $"INNER JOIN [POSSALE].[dbo].[orderDetail] o ON p.prodId = o.productId " + $"INNER JOIN [POSSALE].[dbo].[unit] u ON p.unitId = u.unitId INNER JOIN [POSSALE].[dbo].[type] t ON p.typeId = t.typeId "
                + $"WHERE o.orderId = '{_order}'";
            _config.LoadData(query, dataGridView1);

            if (!isFristTime)
            {
                //check the product that already import from order
                for (int i = 0; i < dataGridView1.RowCount; i++)
                {
                    for (int j = 0; j < dataGridView2.RowCount; j++)
                    {
                        if (dataGridView1.Rows[i].Cells["id1"].Value.ToString() == dataGridView2.Rows[j].Cells["id2"].Value.ToString())
                        {
                            //check the qty that already import from order if it's more than the qty that order just show the qty that not import yet
                            if (int.Parse(dataGridView1.Rows[i].Cells["Column13"].Value.ToString()) == int.Parse(dataGridView2.Rows[j].Cells["Column24"].Value.ToString()))
                            {
                                //remove the product that already import from order
                                dataGridView1.Rows.RemoveAt(i);
                            }
                            else
                            {
                                dataGridView1.Rows[i].Cells["Column13"].Value = int.Parse(dataGridView1.Rows[i].Cells["Column13"].Value.ToString()) - int.Parse(dataGridView2.Rows[j].Cells["Column24"].Value.ToString());
                            }
                        }
                    }
                }
            }
        }

        private void LoadImportDetailId()
        {
            var query = $"SELECT p.[prodId], p.[prodName], i.[importQty], i.[difFromOrder] as [importQtys], i.[importQty] as [importQtyss], u.[unitName], " + $"(p.[prodPrice] * i.[importQty]) as [totalPrice], p.[prodPrice] " + $"FROM [POSSALE].[dbo].[product] p " + $"INNER JOIN [POSSALE].[dbo].[importDetail] i ON p.prodId = i.product " + $"INNER JOIN [POSSALE].[dbo].[unit] u ON p.unitId = u.unitId "
                + $"WHERE i.importId = '{_importId}'";
            _config.LoadData(query, dataGridView2);
        }



        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                (dataGridView1.DataSource as DataTable).DefaultView.RowFilter = string.Format("name LIKE '%{0}%' " +
                    "or unit LIKE '%{0}%' or type LIKE '%{0}%'", textBox1.Text);
            }
            catch (Exception ex)
            {

                MyMessageBox.ShowMessage("ເກີດຂໍ້ຜີດພາດ " + ex + " ", "", "ເກີດຂໍ້ຜີດພາດ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void MImportEdit_Load(object sender, EventArgs e)
        {
            //productImport2();
            LoadOrderDetailId(true);
            LoadImportDetailId();

            //check the product that already import from order
            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                for (int j = 0; j < dataGridView2.RowCount; j++)
                {
                    if (dataGridView1.Rows[i].Cells["id1"].Value.ToString() == dataGridView2.Rows[j].Cells["id2"].Value.ToString())
                    {
                        //check the qty that already import from order if it's more than the qty that order just show the qty that not import yet
                        if (int.Parse(dataGridView1.Rows[i].Cells["Column13"].Value.ToString()) == int.Parse(dataGridView2.Rows[j].Cells["Column24"].Value.ToString()))
                        {
                            //remove the product that already import from order
                            dataGridView1.Rows.RemoveAt(i);
                        }
                        else
                        {
                            dataGridView1.Rows[i].Cells["Column13"].Value = int.Parse(dataGridView1.Rows[i].Cells["Column13"].Value.ToString()) - int.Parse(dataGridView2.Rows[j].Cells["Column24"].Value.ToString());
                        }
                    }
                }
            }
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
                int qty = 0;
                if (dataGridView2.Rows.Count != 0)
                {
                    for (int i = 0; i < dataGridView2.RowCount; i++)
                    {
                        if (dataGridView1.Rows[e.RowIndex].Cells["id1"].Value.ToString() == dataGridView2.Rows[i].Cells["id2"].Value.ToString())
                        {
                            qty = int.Parse(dataGridView2.Rows[i].Cells["Column24"].Value.ToString());
                        }
                    }
                }

                qty = int.Parse(dataGridView1.Rows[e.RowIndex].Cells["Column13"].Value.ToString()) - qty;

                ImEditQty editQty = new ImEditQty(null, this, dataGridView2.Rows[e.RowIndex].Cells["prodPriceOrder"].Value.ToString(), dataGridView2.Rows[e.RowIndex].Cells["difFromOrder"].Value.ToString(), dataGridView2.Rows[e.RowIndex].Cells["orderQtyss"].Value.ToString());
                var senderGrid1 = (DataGridView)sender;
                if (senderGrid1.Columns[e.ColumnIndex] is DataGridViewImageColumn &&
                    e.RowIndex >= 0)
                {
                    //TODO - Button Clicked - Execute Code Here
                    //MyMessageBox.ShowMessage(dataGridView2.Rows[e.RowIndex].Cells[3].Value.ToString(), "", "ຍິນດີຕ້ອນຮັບ", MessageBoxButtons.OK, MessageBoxIcon.None);
                    editQty.label1.Text = " ຈັດການເພີ່ມຈຳນວນນຳເຂົ້າສິນຄ້າ";
                    editQty.label1.Image = Construction_System.Properties.Resources.add_button;
                    editQty.label1.Size = new System.Drawing.Size(232, 26);
                    editQty.button1.Text = "ເພີ່ມ";
                    editQty.textBox1.Text = dataGridView1.Rows[e.RowIndex].Cells["Column12"].Value.ToString();
                    editQty.lblUnit.Text = dataGridView1.Rows[e.RowIndex].Cells["Column14"].Value.ToString();
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

        public void updateQty(int Qty, int price)
        {
            DataGridViewRow rowUp = new DataGridViewRow();
            rowUp = dataGridView2.Rows[selectRowIm];
            rowUp.Cells["Column24"].Value = Qty;
            rowUp.Cells["Column26"].Value = price;
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
                ImEditQty editQty = new ImEditQty(null, this, dataGridView2.Rows[e.RowIndex].Cells["prodPrice"].Value.ToString(), dataGridView2.Rows[e.RowIndex].Cells["importQtys"].Value.ToString(), dataGridView2.Rows[e.RowIndex].Cells["importQtys"].Value.ToString());
                var senderGrid = (DataGridView)sender;

                if (senderGrid.Columns[e.ColumnIndex] is DataGridViewImageColumn &&
                e.RowIndex >= 0 && dataGridView2.Columns[e.ColumnIndex].HeaderCell.Value.ToString() == "ລົບ")
                {
                    //get the delete qty from gridview2
                    int deleteQty = int.Parse(dataGridView2.Rows[e.RowIndex].Cells["Column24"].Value.ToString());

                    //ແນະນຳໃຫ້ update ຈຳນວນ ແລະ ລາຄາຜ່ານ Database
                    //TODO - Button Clicked - Execute Code Here
                    dataGridView2.Rows.RemoveAt(dataGridView2.Rows[e.RowIndex].Index);
                    sumQty();

                    // make the product that already import from order show again if it's remove
                    if (dataGridView1.Rows.Count == 0)
                    {
                        LoadOrderDetailId(false);
                    }
                    else
                    {

                        for (int i = 0; i < dataGridView1.RowCount; i++)
                        {
                            if (dataGridView2.Rows.Count == 0)
                            {
                                LoadOrderDetailId(false);
                                break;
                            }

                            if (dataGridView1.Rows[i].Cells["id1"].Value.ToString() == dataGridView2.Rows[e.RowIndex].Cells["id2"].Value.ToString())
                            {
                                dataGridView1.Rows[i].Cells["Column13"].Value = int.Parse(dataGridView1.Rows[i].Cells["Column13"].Value.ToString()) + deleteQty;
                            }
                        }
                    }
                    
                }

                if (senderGrid.Columns[e.ColumnIndex] is DataGridViewImageColumn &&
                    e.RowIndex >= 0 && dataGridView2.Columns[e.ColumnIndex].HeaderCell.Value.ToString() == "ແກ້ໄຂ")
                {
                    //TODO - Button Clicked - Execute Code Here
                    editQty.label1.Text = "ຈັດການແກ້ໄຂຈຳນວນນຳເຂົ້າສິນຄ້າ";
                    editQty.label1.Image = Construction_System.Properties.Resources.add_button;
                    editQty.label1.Size = new System.Drawing.Size(232, 26);
                    editQty.button1.Text = "ແກ້ໄຂ";
                    editQty.textBox1.Text = dataGridView2.Rows[e.RowIndex].Cells["Column23"].Value.ToString();
                    editQty.textBox2.Text = dataGridView2.Rows[e.RowIndex].Cells["Column24"].Value.ToString();
                    editQty.textBox3.Text = dataGridView2.Rows[e.RowIndex].Cells["Column26"].Value.ToString();
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

        private void button1_Click(object sender, EventArgs e)
        {
            //FM_Bill fM_Bill = new FM_Bill();
            //fM_Bill.ShowDialog();

            // update the import qty and price to database
            try
            {
                // delete the import detail that already import from order
                var query = $"DELETE FROM [POSSALE].[dbo].[importDetail] WHERE importId = '{_importId}'";
                _config.setData(query);

                foreach (DataGridViewRow row in dataGridView2.Rows)
                {
                    // insert the import detail that already import from order
                    var query1 = $"INSERT INTO [POSSALE].[dbo].[importDetail] ([importId], [product], [importQty], [difFromOrder]) " +
                        $"VALUES ('{_importId}', '{row.Cells["id2"].Value}', '{row.Cells["Column24"].Value}', '{row.Cells["importQtys"].Value}')";
                    _config.setData(query1);
                }

                // update the import total price to database
                var query2 = $"UPDATE [POSSALE].[dbo].[import] SET " +
                    $"[totalImport] = (SELECT SUM(importQty * (SELECT prodPrice FROM [POSSALE].[dbo].[product] WHERE prodId = product)) " +
                    $"FROM [POSSALE].[dbo].[importDetail] WHERE importId = '{_importId}') WHERE importId = '{_importId}'";
                _config.setData(query2);

                // update the order status to 'ອະນຸມັດ' if the import total price is equal to the order total price
                var query3 = $"UPDATE [POSSALE].[dbo].[order] SET [orderStatus] = 'ອະນຸມັດ' " +
                    $"WHERE orderId = '{_order}' AND totalOrder = (SELECT totalImport FROM [POSSALE].[dbo].[import] WHERE importId = '{_importId}')";
                _config.setData(query3);

                MyMessageBox.ShowMessage("ບັນທຶກການນຳເຂົ້າສິນຄ້າສຳເລັດແລ້ວ", "", "ສຳເລັດ", MessageBoxButtons.OK, MessageBoxIcon.None);
                FM_Bill fM_Bill = new FM_Bill();
                ImportBill importBill = new ImportBill();
                importBill.SetParameterValue("importId", _importId);
                fM_Bill.crystalReportViewer1.ReportSource = importBill;
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
