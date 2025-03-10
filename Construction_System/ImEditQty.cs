using System;
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

        public ImEditQty(Import import, MImportEdit mImport, string price, string originalQty, string difFromOrder)
        {
            InitializeComponent();
            _import = import;
            _mImport = mImport;
            _price = price;
            _originalQty = originalQty;
            _difFromOrder = difFromOrder;
            textBox2.Select();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            _import.updateQty(Convert.ToInt32(textBox2.Text), Convert.ToInt32(textBox3.Text), int.Parse(_originalQty) - Convert.ToInt32(textBox2.Text));
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            _import.updateQty(Convert.ToInt32(textBox2.Text), Convert.ToInt32(textBox3.Text), int.Parse(_originalQty) - Convert.ToInt32(textBox2.Text));
            this.Close();
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            // Intentionally left blank
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox2.Text) || int.Parse(textBox2.Text) < 0)
            {
                MyMessageBox.ShowMessage("ກະລຸນາປ້ອນຈຳນວນສິນຄ້າ", "", "ຄໍາຖາມ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            textBox3.Text = (Convert.ToInt32(textBox2.Text) * Convert.ToInt32(_price)).ToString();
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox2.Text) || textBox2.Text == "0" || string.IsNullOrEmpty(textBox3.Text) || textBox3.Text == "0")
            {
                MyMessageBox.ShowMessage("ທ່ານໄດ້ປ້ອນຂໍ້ມູນຖືກຕ້ອງຄົບຖ້ວນແລ້ວ ຫຼື ບໍ່?", "", "ກວດສອບ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            switch (label1.Text)
            {
                case "ແກ້ໄຂຂໍ້ມູນນຳເຂົ້າສິນຄ້າ":
                    Console.WriteLine("original", _originalQty);
                    if(int.Parse(textBox2.Text) > int.Parse(_originalQty))
                    {
                        MyMessageBox.ShowMessage("ຈຳນວນທີ່ເພີ່ມຫຼາຍກວ່າຈຳນວນທີ່ສັ່ງຊື້", "", "ຄໍາຖາມ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    _import.updateQty(Convert.ToInt32(textBox2.Text), Convert.ToInt32(textBox3.Text), int.Parse(_originalQty) - Convert.ToInt32(textBox2.Text));
                    MyMessageBox.ShowMessage("ແກ້ໄຂຂໍ້ມູນສຳເລັດແລ້ວ", "", "ສຳເລັດ", MessageBoxButtons.OK, MessageBoxIcon.None);
                    Close();
                    break;

                case " ຈັດການເພີ່ມຈຳນວນນຳເຂົ້າສິນຄ້າ":
                    UpdateDataGridView();
                    MyMessageBox.ShowMessage("ເພີ່ມຂໍ້ມູນສຳເລັດແລ້ວ", "", "ສຳເລັດ", MessageBoxButtons.OK, MessageBoxIcon.None);
                    Close();
                    break;

                case "ຈັດການແກ້ໄຂຈຳນວນນຳເຂົ້າສິນຄ້າ":
                    if(int.Parse(textBox2.Text) > int.Parse(_originalQty))
                    {
                        MyMessageBox.ShowMessage("ຈຳນວນທີ່ເພີ່ມຫຼາຍກວ່າຈຳນວນທີ່ສັ່ງຊື້", "", "ຄໍາຖາມ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    _mImport.updateQty(Convert.ToInt32(textBox2.Text), Convert.ToInt32(textBox3.Text));
                    MyMessageBox.ShowMessage("ແກ້ໄຂຂໍ້ມູນສຳເລັດແລ້ວ", "", "ສຳເລັດ", MessageBoxButtons.OK, MessageBoxIcon.None);
                    Close();
                    break;

                default:
                    MyMessageBox.ShowMessage("ຂໍອະໄພ, ລະບົບຂັດຂ້ອງ!", "", "ຜິດພາດ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
            }
        }

        private void UpdateDataGridView()
        {
            if (_mImport.dataGridView2.Rows.Count == 0) return;

            foreach (DataGridViewRow row in _mImport.dataGridView2.Rows)
            {
                if (row.Cells["id1"].Value.ToString() == lblId.Text)
                {
                    row.Cells["Column24"].Value = Convert.ToInt32(textBox2.Text);
                    row.Cells["Column26"].Value = Convert.ToInt32(textBox3.Text);
                    row.Cells["difFromOrder"].Value = int.Parse(_originalQty) - Convert.ToInt32(textBox2.Text);
                }
            }
        }
    }
}
