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
        MOrder mOrder1;
        private string _idSup;
        private bool _isOrderPage;
        private bool _isDataGrid2 = false;
        private string _orderId;
        public OrEditQty(Order order, MOrderEdit mOrder, string idSup, bool isOrderPage, bool isDataGrid1, string orderId)
        {
            InitializeComponent();
            this.order = order;
            this.mOrder = mOrder;
            _isDataGrid2 = isDataGrid1;
            textBox2.Select();
            _isOrderPage = isOrderPage;
            _idSup = idSup;
            _orderId = orderId;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(textBox2.Text) || textBox2.Text == "0")
                {
                    MyMessageBox.ShowMessage("ຂໍອະໄພ, ທ່ານໄດ້ປ້ອນຂໍ້ມູນຈຳນວນຖືກຕ້ອງແລ້ວ ຫຼື ບໍ່?", "", "ກວດສອບ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                long inputQty = Convert.ToInt64(textBox2.Text);
                bool isCollapsed = true;

                if (label1.Text.Contains("ສັ່ງຊື້ສິນຄ້າ"))
                {
                    if (_isOrderPage)
                    {
                        foreach (DataGridViewRow row in order.dataGridView2.Rows)
                        {
                            if (row.Cells["id2"].Value.ToString() == lblId.Text)
                            {
                                long currentQty = Convert.ToInt64(row.Cells["Column24"].Value);
                                order.updateQty(_isDataGrid2 ? inputQty : inputQty + currentQty, lblId.Text);
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
                    }
                    else
                    {
                       foreach (DataGridViewRow row in mOrder.dataGridView2.Rows)
                        {
                            if (row.Cells["id2"].Value.ToString() == lblId.Text)
                            {
                                long currentQty = Convert.ToInt64(row.Cells["Column24"].Value);
                                mOrder.updateQty(_isDataGrid2 ? inputQty : inputQty + currentQty, lblId.Text);
                                isCollapsed = false;
                                break;
                            }
                        }
                        if (isCollapsed)
                        {
                            //mOrder.dataGridView2.Rows.Add(Construction_System.Properties.Resources.bin,
                            //                             Construction_System.Properties.Resources.pencil,
                            //                             textBox1.Text, inputQty, lblUnit.Text, lblId.Text, _idSup, _orderId);
                            var data = new List<string>
                            {
                                Construction_System.Properties.Resources.bin.ToString(),
                                Construction_System.Properties.Resources.pencil.ToString(),
                                textBox1.Text,
                                inputQty.ToString(),
                                lblUnit.Text,
                                lblId.Text,
                                _idSup,
                                _orderId
                            };
                            mOrder.AddProductToDataGridView(data);
                        }

                        
                    }

                    MyMessageBox.ShowMessage("ເພີ່ມຂໍ້ມູນສຳເລັດແລ້ວ", "", "ສຳເລັດ", MessageBoxButtons.OK, MessageBoxIcon.None);
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
