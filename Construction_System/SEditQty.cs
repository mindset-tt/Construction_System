using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Services.Description;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using static Construction_System.POSSALEDataSet;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Construction_System
{
    public partial class SEditQty : Form
    {
        Sale sale; MSaleEdit mSale;
        private bool isSale = false; private bool isMSale = false;
        private string _qtyInStore;
        private bool _fromDataGrid1 = false;
        //private DataRow[] _deleteRow;
        public SEditQty(Sale sale, MSaleEdit mSale, string qtyInStore, bool fromDataGrid1)
        {
            InitializeComponent();
            isSale = sale != null;
            isMSale = mSale != null;
            this.sale = sale;
            this.mSale = mSale;
            _fromDataGrid1 = fromDataGrid1;
            _qtyInStore = qtyInStore;
            textBox2.Select();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        bool isCollapsed = true;
        //Sale saleData = new Sale();
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(textBox2.Text) || textBox2.Text == "0")
                {
                    MyMessageBox.ShowMessage("ທ່ານໄດ້ປ້ອນຂໍ້ມູນຈຳນວນຖືກຕ້ອງແລ້ວ ຫຼື ບໍ່?", "", "ກວດສອບ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                long inputQty = Convert.ToInt64(textBox2.Text);
                long qtyInStore = Convert.ToInt64(_qtyInStore);

                if (inputQty > qtyInStore)
                {
                    MyMessageBox.ShowMessage("ຈຳນວນທີ່ເພີ່ມຫຼາຍກວ່າຈຳນວນທີ່ມີໃນຮ້ານ", "", "ຄໍາຖາມ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (label1.Text.Contains("ຂາຍສິນຄ້າ"))
                {
                    if (isSale)
                    {
                        foreach (DataGridViewRow row in sale.dataGridView2.Rows)
                        {
                            if (row.Cells["id2"].Value.ToString() == lblId.Text)
                            {
                                long currentQty = Convert.ToInt64(row.Cells["Column24"].Value);
                                long totalQty = !_fromDataGrid1 ? inputQty : inputQty + currentQty;

                                if (totalQty > qtyInStore)
                                {
                                    MyMessageBox.ShowMessage("ຈຳນວນທີ່ເພີ່ມຫຼາຍກວ່າຈຳນວນທີ່ມີໃນຮ້ານ", "", "ຄໍາຖາມ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    return;
                                }

                                sale.updateQty(totalQty, Convert.ToInt64(lblPrice.Text) * totalQty, lblId.Text);
                                isCollapsed = false;
                                _fromDataGrid1 = false;
                                break;
                            }
                        }

                        if (isCollapsed)
                        {
                            sale.dataGridView2.Rows.Add(Construction_System.Properties.Resources.bin,
                                                        Construction_System.Properties.Resources.pencil, textBox1.Text,
                                                        inputQty, lblUnit.Text,
                                                        Convert.ToInt64(lblPrice.Text) * inputQty, lblId.Text, _qtyInStore, Convert.ToInt64(_qtyInStore) - inputQty, Convert.ToInt64(lblPrice.Text));
                        }

                        MyMessageBox.ShowMessage("ເພີ່ມຂໍ້ມູນສຳເລັດແລ້ວ", "", "ສຳເລັດ", MessageBoxButtons.OK, MessageBoxIcon.None);
                    }
                    else
                    {
                        DataTable orderTable = mSale.dataGridView1.DataSource as DataTable;
                        // ✅ Check if requested import quantity exceeds available store quantity
                        if (long.Parse(textBox2.Text) > long.Parse(_qtyInStore))
                        {
                            MyMessageBox.ShowMessage("ຈຳນວນທີ່ເພີ່ມຫຼາຍກວ່າຈຳນວນທີ່ມີໃນຮ້ານ", "", "ຄໍາເຕືອນ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }

                        // ✅ Ensure DataTable is available as DataSource of dataGridView2
                        if (!(mSale.dataGridView2.DataSource is DataTable dataTable))
                        {
                            MyMessageBox.ShowMessage("ບໍ່ພົບ DataTable", "", "ຜິດພາດ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        bool rowFound = false;
                        string currentProdId = lblId.Text.Trim();

                        long getQty = 0;

                        // ✅ Try to update if the product already exists in the import grid
                        foreach (DataGridViewRow row in mSale.dataGridView2.Rows)
                        {
                            if (row.IsNewRow) continue;

                            string prodId = row.Cells["id2"].Value?.ToString()?.Trim() ?? string.Empty;
                            if (prodId == currentProdId)
                            {
                                
                                getQty = Convert.ToInt64(row.Cells["Column24"].Value);
                                row.Cells["Column24"].Value = Convert.ToInt64(textBox2.Text); // importQty
                                row.Cells["Column26"].Value = Convert.ToInt64(textBox2.Text) * Convert.ToInt64(lblPrice.Text); // totalPrice
                                rowFound = true;
                                break;
                            }
                        }

                        foreach (DataColumn col in dataTable.Columns)
                        {
                            Console.WriteLine(col.ColumnName);
                        }

                        // ✅ If product not found, insert new row into DataTable
                        if (!rowFound)
                        {
                            DataRow newRow = dataTable.NewRow();
                            newRow["prodID"] = lblId.Text;
                            newRow["prodName"] = textBox1.Text;
                            //newRow["sellQtyss"] = Convert.ToInt64(textBox2.Text) + Convert.ToInt64(_qtyInStore);
                            newRow["sellQtyss"] = Convert.ToInt64(_qtyInStore);
                            newRow["sellQty"] = Convert.ToInt64(textBox2.Text);
                            newRow["unitName"] = lblUnit.Text;
                            newRow["totalPrice"] = Convert.ToInt64(lblPrice.Text) * Convert.ToInt64(textBox2.Text);
                            newRow["prodPrice"] = Convert.ToInt64(lblPrice.Text);
                            dataTable.Rows.Add(newRow);
                        }

                        // ✅ Recalculate total imported qty for this product
                        long totalImportedQty = 0;
                        foreach (DataGridViewRow row in mSale.dataGridView2.Rows)
                        {
                            if (row.IsNewRow) continue;

                            string prodId = row.Cells["id2"].Value?.ToString()?.Trim();
                            if (!string.IsNullOrEmpty(prodId) && prodId == currentProdId)
                            {
                                totalImportedQty += Convert.ToInt64(row.Cells["Column24"].Value);
                            }
                        }
                        
                        // ✅ Update dataGridView1 (order grid) - loop backward for safe deletion
                        for (int i = mSale.dataGridView1.Rows.Count - 1; i >= 0; i--)
                        {
                            DataGridViewRow orderRow = mSale.dataGridView1.Rows[i];
                            if (orderRow.IsNewRow) continue;

                            string productId = orderRow.Cells["id1"].Value?.ToString();
                            if (productId == currentProdId)
                            {
                                long orderQty = long.Parse(orderRow.Cells["Column13"].Value.ToString().Replace(",", "")) + getQty;

                                if (totalImportedQty >= orderQty)
                                {
                                    // ✅ Fully imported, remove from order grid
                                    //mSale.dataGridView1.Rows.RemoveAt(i);

                                    orderRow.Cells["Column13"].Value = 0;

                                    // just make that row invisible
                                    //orderRow.Visible = false;
                                }
                                else
                                {
                                    // ✅ Update remaining order qty
                                    orderRow.Cells["Column13"].Value = orderQty - totalImportedQty;
                                }
                                break; // ✅ Done updating
                            }
                        }

                        MyMessageBox.ShowMessage("ແກ້ໄຂສຳເລັດ", "", "ສຳເລັດ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Close();
                    }

                }
                else if (label1.Text.Contains("ແກ້ໄຂຈຳນວນຂາຍສິນຄ້າ"))
                {
                    sale.updateQty(inputQty, (Convert.ToInt64(lblPrice.Text) / Convert.ToInt64(lblQtyEdit.Text)) * inputQty, lblId.Text);
                    MyMessageBox.ShowMessage("ແກ້ໄຂຂໍ້ມູນສຳເລັດແລ້ວ", "", "ສຳເລັດ", MessageBoxButtons.OK, MessageBoxIcon.None);
                }
                else if (label1.Text.Contains("ຈຳນວນຂາຍສິນຄ້າ"))
                {
                    mSale.dataGridView2.Rows.Add(Construction_System.Properties.Resources.bin,
                                                 Construction_System.Properties.Resources.pencil, textBox1.Text,
                                                 inputQty, lblUnit.Text,
                                                 Convert.ToInt64(lblPrice.Text) * inputQty, lblId.Text);
                    MyMessageBox.ShowMessage("ເພີ່ມຂໍ້ມູນສຳເລັດແລ້ວ", "", "ສຳເລັດ", MessageBoxButtons.OK, MessageBoxIcon.None);
                }
                else if (label1.Text.Contains("ແກ້ໄຂຈຳນວນຂາຍສິນຄ້າ"))
                {
                    //mSale.updateQtyMS(inputQty, (Convert.ToInt64(lblPrice.Text)/Convert.ToInt64(lblQtyEdit.Text)) * inputQty);
                    mSale.updateQty(inputQty, Convert.ToInt64(lblPrice.Text) / Convert.ToInt64(lblQtyEdit.Text) * inputQty);
                    MyMessageBox.ShowMessage("ແກ້ໄຂຂໍ້ມູນສຳເລັດແລ້ວ", "", "ສຳເລັດ", MessageBoxButtons.OK, MessageBoxIcon.None);
                }
                else
                {
                    MyMessageBox.ShowMessage("ຂໍອະໄພ, ລະບົບຂັດຂ້ອງ!", "", "ຜິດພາດ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                this.Close();
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowMessage($"ເກີດຂໍ້ຜີດພາດ {ex.Message}", "", "ເກີດຂໍ້ຜິດພາດ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
}
