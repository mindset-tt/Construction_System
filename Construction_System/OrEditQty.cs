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
    public partial class OrEditQty : Form
    {
        Order order;
        MOrderEdit mOrder;
        private string _idSup;
        private bool _isOrderPage;
        public OrEditQty(Order order, MOrderEdit mOrder, string idSup, bool isOrderPage)
        {
            InitializeComponent();
            this.order = order;
            this.mOrder = mOrder;
            textBox2.Select();
            _isOrderPage = isOrderPage;
            _idSup = idSup;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        bool isCollapsed;
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
                bool isCollapsed = true;

                if (label1.Text.Contains("ສັ່ງຊື້ສິນຄ້າ"))
                {
                    foreach (DataGridViewRow row in order.dataGridView2.Rows)
                    {
                        if (row.Cells["id2"].Value.ToString() == lblId.Text)
                        {
                            int currentQty = Convert.ToInt32(row.Cells["Column24"].Value);
                            order.updateQty(inputQty + currentQty, lblId.Text);
                            isCollapsed = false;
                            break;
                        }
                    }

                    if (isCollapsed)
                    {
                        order.dataGridView2.Rows.Add(Construction_System.Properties.Resources.bin,
                                                     Construction_System.Properties.Resources.pencil,
                                                     textBox1.Text, inputQty, lblUnit.Text, lblId.Text, _idSup);
                    }

                    MyMessageBox.ShowMessage("ເພີ່ມຂໍ້ມູນສຳເລັດແລ້ວ", "", "ສຳເລັດ", MessageBoxButtons.OK, MessageBoxIcon.None);
                }
                else if (label1.Text.Contains("ແກ້ໄຂຈຳນວນສັ່ງຊື້ສິນຄ້າ") && _isOrderPage)
                {
                    Console.WriteLine("label text", lblId.Text);
                    //Console.WriteLine("data in grid", row.Cells["id2"].Value.ToString());
                    order.updateQty(inputQty, lblId.Text);
                    MyMessageBox.ShowMessage("ແກ້ໄຂຂໍ້ມູນສຳເລັດແລ້ວ", "", "ສຳເລັດ", MessageBoxButtons.OK, MessageBoxIcon.None);
                }
                else if (label1.Text.Contains("ຈຳນວນສັ່ງຊື້ສິນຄ້າ"))
                {
                    mOrder.dataGridView2.Rows.Add(Construction_System.Properties.Resources.bin,
                                                  Construction_System.Properties.Resources.pencil,
                                                  textBox1.Text, inputQty, lblUnit.Text, lblId.Text);
                    MyMessageBox.ShowMessage("ເພີ່ມຂໍ້ມູນສຳເລັດແລ້ວ", "", "ສຳເລັດ", MessageBoxButtons.OK, MessageBoxIcon.None);
                }
                else if (label1.Text.Contains("ແກ້ໄຂຈຳນວນສັ່ງຊື້ສິນຄ້າ") && !_isOrderPage)
                {
                    mOrder.updateQtyMOr(inputQty);
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
