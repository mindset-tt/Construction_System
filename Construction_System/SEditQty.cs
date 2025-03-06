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
        public SEditQty(Sale sale, MSaleEdit mSale)
        {
            InitializeComponent();
            this.sale = sale;
            this.mSale = mSale;
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

        //Sale saleData = new Sale();
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {

                if (label1.Text == " ເພີ່ມຈຳນວນຂາຍສິນຄ້າ")
                {
                    if (textBox2.Text == "" || textBox2.Text == "0")
                    {
                        MyMessageBox.ShowMessage("ທ່ານໄດ້ປ້ອນຂໍ້ມູນຈຳນວນຖືກຕ້ອງແລ້ວ ຫຼື ບໍ່?", "", "ກວດສອບ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else
                    {
                        sale.dataGridView2.Rows.Add(Construction_System.Properties.Resources.bin,
                            Construction_System.Properties.Resources.pencil, textBox1.Text,
                            Convert.ToInt32(textBox2.Text), lblUnit.Text,
                            Convert.ToInt32(lblPrice.Text)*Convert.ToInt32(textBox2.Text), lblId.Text);
                        MyMessageBox.ShowMessage("ເພີ່ມຂໍ້ມູນສຳເລັດແລ້ວ", "", "ສຳເລັດ", MessageBoxButtons.OK, MessageBoxIcon.None);
                        this.Close();
                    }

                }
                else if (label1.Text == "ແກ້ໄຂຈຳນວນຂາຍສິນຄ້າ")
                {
                    if (textBox2.Text == "" || textBox2.Text == "0")
                    {
                        MyMessageBox.ShowMessage("ທ່ານໄດ້ປ້ອນຂໍ້ມູນຈຳນວນຖືກຕ້ອງແລ້ວ ຫຼື ບໍ່?", "", "ກວດສອບ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else
                    {
                        sale.updateQty(Convert.ToInt32(textBox2.Text),
                         (Convert.ToInt32(lblPrice.Text) / Convert.ToInt32(lblQtyEdit.Text)) * Convert.ToInt32(textBox2.Text));
                        MyMessageBox.ShowMessage("ແກ້ໄຂຂໍ້ມູນສຳເລັດແລ້ວ", "", "ສຳເລັດ", MessageBoxButtons.OK, MessageBoxIcon.None);
                        this.Close();
                    }
                    
                }
                else if (label1.Text == " ຈັດການເພີ່ມຈຳນວນຂາຍສິນຄ້າ")
                {
                    if (textBox2.Text == "" || textBox2.Text == "0")
                    {
                        MyMessageBox.ShowMessage("ທ່ານໄດ້ປ້ອນຂໍ້ມູນຈຳນວນຖືກຕ້ອງແລ້ວ ຫຼື ບໍ່?", "", "ກວດສອບ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else
                    {
                        //ແນະນຳໃຫ້ update ຈຳນວນ ແລະ ລາຄາຜ່ານ Database
                        mSale.dataGridView2.Rows.Add(Construction_System.Properties.Resources.bin,
                            Construction_System.Properties.Resources.pencil, textBox1.Text,
                            Convert.ToInt32(textBox2.Text), lblUnit.Text,
                            Convert.ToInt32(lblPrice.Text) * Convert.ToInt32(textBox2.Text), lblId.Text);
                        MyMessageBox.ShowMessage("ເພີ່ມຂໍ້ມູນສຳເລັດແລ້ວ", "", "ສຳເລັດ", MessageBoxButtons.OK, MessageBoxIcon.None);
                        this.Close();
                    }
                }
                else if (label1.Text == "ຈັດການແກ້ໄຂຈຳນວນຂາຍສິນຄ້າ")
                {
                    if (textBox2.Text == "" || textBox2.Text == "0")
                    {
                        MyMessageBox.ShowMessage("ທ່ານໄດ້ປ້ອນຂໍ້ມູນຈຳນວນຖືກຕ້ອງແລ້ວ ຫຼື ບໍ່?", "", "ກວດສອບ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else
                    {
                        //ແນະນຳໃຫ້ update ຈຳນວນ ແລະ ລາຄາຜ່ານ Database
                        mSale.updateQtyMS(Convert.ToInt32(textBox2.Text),
                            (Convert.ToInt32(lblPrice.Text) / Convert.ToInt32(lblQtyEdit.Text)) * Convert.ToInt32(textBox2.Text));
                        MyMessageBox.ShowMessage("ແກ້ໄຂຂໍ້ມູນສຳເລັດແລ້ວ", "", "ສຳເລັດ", MessageBoxButtons.OK, MessageBoxIcon.None);
                        this.Close();
                    }
                    
                }
                else
                {
                    MyMessageBox.ShowMessage("ຂໍອະໄພ, ລະບົບຂັດຂ້ອງ!", "", "ຜິດພາດ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
            catch
            {

                //MyMessageBox.ShowMessage("ເກີດຂໍ້ຜີດພາດ " + ex + " ", "", "ເກີດຂໍ້ຜີດພາດ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                MyMessageBox.ShowMessage("ທ່ານໄດ້ປ້ອນຂໍ້ມູນຈຳນວນຖືກຕ້ອງແລ້ວ ຫຼື ບໍ່?", "", "ກວດສອບ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
