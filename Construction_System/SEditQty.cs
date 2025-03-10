using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Construction_System
{
    public partial class SEditQty : Form
    {
        Sale sale; MSaleEdit mSale;
        private string _qtyInStore;
        public SEditQty(Sale sale, MSaleEdit mSale, string qtyInStore)
        {
            InitializeComponent();
            this.sale = sale;
            this.mSale = mSale;
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

                int inputQty = Convert.ToInt32(textBox2.Text);
                int qtyInStore = Convert.ToInt32(_qtyInStore);

                if (inputQty > qtyInStore)
                {
                    MyMessageBox.ShowMessage("ຈຳນວນທີ່ເພີ່ມຫຼາຍກວ່າຈຳນວນທີ່ມີໃນຮ້ານ", "", "ຄໍາຖາມ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (label1.Text.Contains("ຂາຍສິນຄ້າ"))
                {
                    foreach (DataGridViewRow row in sale.dataGridView2.Rows)
                    {
                        if (row.Cells["id2"].Value.ToString() == lblId.Text)
                        {
                            int currentQty = Convert.ToInt32(row.Cells["Column24"].Value);
                            int totalQty = inputQty + currentQty;

                            if (totalQty > qtyInStore)
                            {
                                MyMessageBox.ShowMessage("ຈຳນວນທີ່ເພີ່ມຫຼາຍກວ່າຈຳນວນທີ່ມີໃນຮ້ານ", "", "ຄໍາຖາມ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }

                            sale.updateQty(totalQty, Convert.ToInt32(lblPrice.Text) * totalQty);
                            isCollapsed = false;
                            break;
                        }
                    }

                    if (isCollapsed)
                    {
                        sale.dataGridView2.Rows.Add(Construction_System.Properties.Resources.bin,
                                                    Construction_System.Properties.Resources.pencil, textBox1.Text,
                                                    inputQty, lblUnit.Text,
                                                    Convert.ToInt32(lblPrice.Text) * inputQty, lblId.Text, _qtyInStore);
                    }

                    MyMessageBox.ShowMessage("ເພີ່ມຂໍ້ມູນສຳເລັດແລ້ວ", "", "ສຳເລັດ", MessageBoxButtons.OK, MessageBoxIcon.None);
                }
                else if (label1.Text.Contains("ແກ້ໄຂຈຳນວນຂາຍສິນຄ້າ"))
                {
                    sale.updateQty(inputQty, (Convert.ToInt32(lblPrice.Text) / Convert.ToInt32(lblQtyEdit.Text)) * inputQty);
                    MyMessageBox.ShowMessage("ແກ້ໄຂຂໍ້ມູນສຳເລັດແລ້ວ", "", "ສຳເລັດ", MessageBoxButtons.OK, MessageBoxIcon.None);
                }
                else if (label1.Text.Contains("ຈຳນວນຂາຍສິນຄ້າ"))
                {
                    mSale.dataGridView2.Rows.Add(Construction_System.Properties.Resources.bin,
                                                 Construction_System.Properties.Resources.pencil, textBox1.Text,
                                                 inputQty, lblUnit.Text,
                                                 Convert.ToInt32(lblPrice.Text) * inputQty, lblId.Text);
                    MyMessageBox.ShowMessage("ເພີ່ມຂໍ້ມູນສຳເລັດແລ້ວ", "", "ສຳເລັດ", MessageBoxButtons.OK, MessageBoxIcon.None);
                }
                else if (label1.Text.Contains("ແກ້ໄຂຈຳນວນຂາຍສິນຄ້າ"))
                {
                    mSale.updateQtyMS(inputQty, (Convert.ToInt32(lblPrice.Text) / Convert.ToInt32(lblQtyEdit.Text)) * inputQty);
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
