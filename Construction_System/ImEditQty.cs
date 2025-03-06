using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Construction_System
{
    public partial class ImEditQty : Form
    {
        Import import;
        MImportEdit mImport;
        private string _price, _originalQty, _difFromOrder;
        public ImEditQty(Import import, MImportEdit mImport, string price, string originalQty, string difFromOrder)
        {
            InitializeComponent();
            this.import = import;
            this.mImport = mImport;
            _price = price;
            _originalQty = originalQty;
            _difFromOrder = difFromOrder;
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

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox2.Text) || int.Parse(textBox2.Text) < 0)
            {
                MyMessageBox.ShowMessage("ກະລຸນາປ້ອນຈຳນວນສິນຄ້າ", "", "ຄໍາຖາມ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            {
                textBox3.Text = (Convert.ToInt32(textBox2.Text) * Convert.ToInt32(_price)).ToString();
            }
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
            //textBox2.Text = string.Format("{#,###}", double.Parse(textBox2.Text));
            //textBox3.Text = string.Format("{#,###}", double.Parse(textBox3.Text));
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //try
            //{
            if (label1.Text == "ແກ້ໄຂຂໍ້ມູນນຳເຂົ້າສິນຄ້າ")
            {
                if (textBox2.Text == "" || textBox2.Text == "0" || textBox3.Text == "" || textBox3.Text == "0")
                {
                    MyMessageBox.ShowMessage("ທ່ານໄດ້ປ້ອນຂໍ້ມູນຖືກຕ້ອງຄົບຖ້ວນແລ້ວ ຫຼື ບໍ່?", "", "ກວດສອບ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    import.updateQty(Convert.ToInt32(textBox2.Text), Convert.ToInt32(textBox3.Text), (int.Parse(_originalQty) - Convert.ToInt32(textBox2.Text)));
                    
                    //if (mImport.dataGridView2.Rows.Count != 0)
                    //{
                    //    for (int i = 0; i < mImport.dataGridView2.Rows.Count; i++)
                    //    {

                    //        if (mImport.dataGridView2.Rows[i].Cells["id1"].Value.ToString() == lblId.Text)
                    //        {
                    //            mImport.dataGridView2.Rows[i].Cells["Column24"].Value = Convert.ToInt32(textBox2.Text);
                    //            mImport.dataGridView2.Rows[i].Cells["Column26"].Value = Convert.ToInt32(textBox3.Text);
                    //            mImport.dataGridView2.Rows[i].Cells["difFromOrder"].Value = int.Parse(_originalQty) - Convert.ToInt32(textBox2.Text);
                    //        }
                    //    }
                    //}
                    MyMessageBox.ShowMessage("ແກ້ໄຂຂໍ້ມູນສຳເລັດແລ້ວ", "", "ສຳເລັດ", MessageBoxButtons.OK, MessageBoxIcon.None);
                    this.Close();
                }
            }
            else if (label1.Text == " ຈັດການເພີ່ມຈຳນວນນຳເຂົ້າສິນຄ້າ")
            {
                if (textBox2.Text == "" || textBox2.Text == "0" || textBox3.Text == "" || textBox3.Text == "0")
                {
                    MyMessageBox.ShowMessage("ທ່ານໄດ້ປ້ອນຂໍ້ມູນຖືກຕ້ອງຄົບຖ້ວນແລ້ວ ຫຼື ບໍ່?", "", "ກວດສອບ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    //ແນະນຳໃຫ້ update ຈຳນວນ ແລະ ລາຄາຜ່ານ Database
                    //mImport.dataGridView2.Rows.Add(Construction_System.Properties.Resources.bin,
                    //    Construction_System.Properties.Resources.pencil, textBox1.Text,
                    //    Convert.ToInt32(textBox2.Text), lblUnit.Text,
                    //    Convert.ToInt32(textBox3.Text), lblId.Text, _price, int.Parse(_originalQty) - Convert.ToInt32(textBox2.Text), _originalQty);

                    if (mImport.dataGridView2.Rows.Count != 0)
                    {
                        for (int i = 0; i < mImport.dataGridView2.Rows.Count; i++)
                        {
                            if (mImport.dataGridView2.Rows[i].Cells["id1"].Value.ToString() == lblId.Text)
                            {
                                mImport.dataGridView2.Rows[i].Cells["Column24"].Value = Convert.ToInt32(textBox2.Text);
                                mImport.dataGridView2.Rows[i].Cells["Column26"].Value = Convert.ToInt32(textBox3.Text);
                                mImport.dataGridView2.Rows[i].Cells["difFromOrder"].Value = int.Parse(_originalQty) - Convert.ToInt32(textBox2.Text);
                            }
                        }
                    }

                    MyMessageBox.ShowMessage("ເພີ່ມຂໍ້ມູນສຳເລັດແລ້ວ", "", "ສຳເລັດ", MessageBoxButtons.OK, MessageBoxIcon.None);
                    this.Close();
                    //import.updateQty(Convert.ToInt32(textBox2.Text), Convert.ToInt32(textBox3.Text));
                    //MyMessageBox.ShowMessage("ແກ້ໄຂຂໍ້ມູນສຳເລັດແລ້ວ", "", "ສຳເລັດ", MessageBoxButtons.OK, MessageBoxIcon.None);
                    this.Close();
                }
            }
            else if (label1.Text == "ຈັດການແກ້ໄຂຈຳນວນນຳເຂົ້າສິນຄ້າ")
            {
                //ແນະນຳໃຫ້ update ຈຳນວນ ແລະ ລາຄາຜ່ານ Database
                mImport.updateQty(Convert.ToInt32(textBox2.Text), Convert.ToInt32(textBox3.Text));
                MyMessageBox.ShowMessage("ແກ້ໄຂຂໍ້ມູນສຳເລັດແລ້ວ", "", "ສຳເລັດ", MessageBoxButtons.OK, MessageBoxIcon.None);
                this.Close();
            }
            else
            {
                MyMessageBox.ShowMessage("ຂໍອະໄພ, ລະບົບຂັດຂ້ອງ!", "", "ຜິດພາດ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            //}
            //catch
            //{
            //    MessageBox.Show("adadsadad");
            //    //MyMessageBox.ShowMessage("ເກີດຂໍ້ຜີດພາດ " + ex + " ", "", "ເກີດຂໍ້ຜີດພາດ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    MyMessageBox.ShowMessage("ທ່ານໄດ້ປ້ອນຂໍ້ມູນຖືກຕ້ອງຄົບຖ້ວນແລ້ວ ຫຼື ບໍ່?", "", "ກວດສອບ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //}
        }
    }
}
