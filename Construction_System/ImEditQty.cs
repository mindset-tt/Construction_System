using System;
using System.Data;
using System.Windows.Forms;

namespace Construction_System
{
    public partial class ImEditQty : Form
    {
        private readonly Import _import;
        private readonly MImportEdit _mImport;
        private readonly string _price;
        private readonly string _originalQty;
        private readonly string _difFromOrder;
        private readonly config _config = new config();

        public ImEditQty(Import import, MImportEdit mImport, string price, string difFromOrder, string originalQty)
        {
            InitializeComponent();
            _import = import;
            _mImport = mImport;
            _price = price;
            _originalQty = originalQty;
            _difFromOrder = difFromOrder;
            textBox2.Select();
        }

        private void pictureBox2_Click(object sender, EventArgs e) => this.Close();
        private void button2_Click(object sender, EventArgs e) => this.Close();

        private void textBox3_TextChanged(object sender, EventArgs e) { }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox2.Text) || long.Parse(textBox2.Text) < 0)
            {
                MyMessageBox.ShowMessage("ກະລຸນາປ້ອນຈຳນວນສິນຄ້າ", "", "ຄໍາຖາມ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            textBox3.Text = (Convert.ToInt64(textBox2.Text) * Convert.ToInt64(_price)).ToString();
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
                e.Handled = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox2.Text) || textBox2.Text == "0" || string.IsNullOrEmpty(textBox3.Text) || textBox3.Text == "0")
            {
                MyMessageBox.ShowMessage("ກວດສອບຂໍ້ມູນທີ່ປ້ອນ", "", "ຄໍາເຕືອນ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            switch (label1.Text.Trim())
            {
                case "ແກ້ໄຂຂໍ້ມູນນຳເຂົ້າສິນຄ້າ":
                    if (long.Parse(textBox2.Text) > long.Parse(_originalQty))
                    {
                        MyMessageBox.ShowMessage("ຈຳນວນເພີ່ມເກີນຈຳນວນທີ່ສັ່ງ", "", "ຄໍາເຕືອນ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    _import.updateQty(Convert.ToInt64(textBox2.Text), Convert.ToInt64(textBox3.Text), long.Parse(_originalQty) - Convert.ToInt64(textBox2.Text), lblId.Text);
                    MyMessageBox.ShowMessage("ແກ້ໄຂສຳເລັດ", "", "ສຳເລັດ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Close();
                    break;

                case "ຈັດການເພີ່ມຈຳນວນນຳເຂົ້າສິນຄ້າ":
                    UpdateDataGridView();
                    MyMessageBox.ShowMessage("ເພີ່ມຂໍ້ມູນສຳເລັດ", "", "ສຳເລັດ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Close();
                    break;

                case "ຈັດການແກ້ໄຂຈຳນວນນຳເຂົ້າສິນຄ້າ":
                    if (long.Parse(textBox2.Text) > long.Parse(_originalQty))
                    {
                        MyMessageBox.ShowMessage("ຈຳນວນເພີ່ມເກີນຈຳນວນທີ່ສັ່ງ", "", "ຄໍາເຕືອນ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    // ✅ Recalculate total imported qty for this product
                    long totalImportedQty = 0;
                    foreach (DataGridViewRow row in _mImport.dataGridView2.Rows)
                    {
                        if (row.IsNewRow) continue;
                        string prodId = row.Cells["id2"].Value?.ToString()?.Trim();
                        if (!string.IsNullOrEmpty(prodId) && prodId == lblId.Text.Trim())
                        {
                            // If it's the row being edited, use the new value from textBox2
                            if (row.Cells["id2"].Value.ToString() == lblId.Text.Trim())
                            {
                                totalImportedQty += Convert.ToInt64(textBox2.Text);
                            }
                            else
                            {
                                totalImportedQty += Convert.ToInt64(row.Cells["Column24"].Value);
                            }
                        }
                    }

                    // ✅ Update dataGridView1 (order) - loop backwards
                    for (int i = _mImport.dataGridView1.Rows.Count - 1; i >= 0; i--)
                    {
                        DataGridViewRow orderRow = _mImport.dataGridView1.Rows[i];
                        if (orderRow.IsNewRow) continue;

                        string productId = orderRow.Cells["id1"].Value?.ToString();
                        if (productId == lblId.Text)
                        {
                            long orderQty = long.Parse(orderRow.Cells["Column13"].Value.ToString().Replace(",", ""));

                            if (totalImportedQty >= orderQty)
                            {
                                // ✅ Fully imported, remove from order grid
                                _mImport.dataGridView1.Rows.RemoveAt(i);
                            }
                            else
                            {
                                // ✅ Update remaining order qty
                                orderRow.Cells["Column13"].Value = orderQty - totalImportedQty;
                            }
                            break; // ✅ Done
                        }
                    }

                    // ✅ Update selected row in dataGridView2 (the import grid)
                    if (_mImport.selectRowIm >= 0 && _mImport.selectRowIm < _mImport.dataGridView2.Rows.Count)
                    {
                        DataGridViewRow updateRow = _mImport.dataGridView2.Rows[_mImport.selectRowIm];
                        updateRow.Cells["Column24"].Value = textBox2.Text; // importQty
                        updateRow.Cells["Column26"].Value = textBox3.Text; // totalPrice
                    }

                    MyMessageBox.ShowMessage("ແກ້ໄຂສຳເລັດ", "", "ສຳເລັດ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Close();
                    break;

                default:
                    MyMessageBox.ShowMessage("ລະບົບຂັດຂ້ອງ", "", "ຜິດພາດ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
            }
        }

        private void UpdateDataGridView()
        {
            // ✅ Use strong reference to DataTable (use _mImport's DataTable if stored)
            if (!(_mImport.dataGridView2.DataSource is DataTable dataTable))
            {
                MyMessageBox.ShowMessage("ບໍ່ພົບ DataTable", "", "ຜິດພາດ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            bool rowFound = false;
            foreach (DataGridViewRow row in _mImport.dataGridView2.Rows)
            {
                if (row.IsNewRow) continue; // ✅ Skip the last empty new row

                string prodId = row.Cells["id2"].Value?.ToString().Trim() ?? string.Empty;
                string currentLblId = lblId.Text.Trim();

                Console.WriteLine($"[DEBUG] prodId: '{prodId}' | lblId: '{currentLblId}'");

                if (!string.IsNullOrEmpty(prodId) && prodId == currentLblId)
                {
                    row.Cells["Column24"].Value = Convert.ToInt64(textBox2.Text); // importQty column
                    row.Cells["Column26"].Value = Convert.ToInt64(textBox3.Text); // totalPrice column
                    rowFound = true;
                    break; // ✅ Found, stop loop
                }
            }



            if (!rowFound)
            {
                DataRow newRow = dataTable.NewRow();
                newRow["prodId"] = lblId.Text;
                newRow["prodName"] = textBox1.Text;
                newRow["importQty"] = Convert.ToInt64(textBox2.Text);
                newRow["importQtys"] = Convert.ToInt64(_originalQty) - Convert.ToInt64(textBox2.Text);
                newRow["importQtyss"] = Convert.ToInt64(_originalQty);
                newRow["unitName"] = lblUnit.Text;
                newRow["totalPrice"] = Convert.ToInt64(textBox3.Text);
                newRow["prodPrice"] = Convert.ToInt64(_price);
                dataTable.Rows.Add(newRow);
            }

            // ✅ Recalculate total imported qty for this product
            long totalImportedQty = 0;
            foreach (DataGridViewRow row in _mImport.dataGridView2.Rows)
            {
                if (row.IsNewRow) continue; // ✅ Skip the placeholder new row

                string prodId = row.Cells["id2"].Value?.ToString()?.Trim();
                if (!string.IsNullOrEmpty(prodId) && prodId == lblId.Text.Trim())
                {
                    totalImportedQty += Convert.ToInt64(row.Cells["Column24"].Value);
                }
            }

            // ✅ Update dataGridView1 - loop backward to avoid accessing deleted rows
            for (int i = _mImport.dataGridView1.Rows.Count - 1; i >= 0; i--)
            {
                DataGridViewRow orderRow = _mImport.dataGridView1.Rows[i];
                if (orderRow.IsNewRow) continue;

                string productId = orderRow.Cells["id1"].Value?.ToString();
                if (productId == lblId.Text)
                {
                    long orderQty = long.Parse(orderRow.Cells["Column13"].Value.ToString().Replace(",", ""));

                    if (totalImportedQty >= orderQty)
                    {
                        _mImport.dataGridView1.Rows.RemoveAt(i);
                        break;
                    }
                    else
                    {
                        orderRow.Cells["Column13"].Value = orderQty - totalImportedQty;
                        break;
                    }
                }
            }
        }
    }
}
