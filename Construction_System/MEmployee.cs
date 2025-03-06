using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Construction_System
{
    public partial class MEmployee : Form
    {
        private string _empId;
        public MEmployee(string empId)
        {
            InitializeComponent();
            _empId = empId;
        }

        public void sumQty()
        {
            try
            {
                if (dataGridView1.Rows.Count != 0)
                {
                    label4.Text = dataGridView1.RowCount.ToString("#,###") + "    ລາຍການ";
                    //int totalQty = 0;
                    //for (int i = 0; i < dataGridView1.RowCount; i++)
                    //{
                    //    totalQty += Convert.ToInt32(dataGridView1.Rows[i].Cells["Column24"].Value.ToString());
                    //}
                    //label2.Text = totalQty.ToString("#,###") + "   ອັນ";
                }
                else
                {
                    label4.Text = "0" + "    ລາຍການ";
                }
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowMessage("ເກີດຂໍ້ຜີດພາດ " + ex + " ", "", "ເກີດຂໍ້ຜີດພາດ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void MEmployee_Load(object sender, EventArgs e)
        {
            DataSet data1 = new DataSet("Contruction_System");
            DataTable table1 = new DataTable("employee");
            table1.Columns.Add("id", typeof(int));
            table1.Columns.Add("emName");
            table1.Columns.Add("empSurnmae");
            table1.Columns.Add("sex");
            table1.Columns.Add("number");
            table1.Columns.Add("username");
            table1.Columns.Add("password");
            table1.Columns.Add("root");
            table1.Rows.Add(1, "ໃຫຍ່", "ສູງສົ່ງ", "ຊາຍ", "5933658", "Yai1", "1234", "Admin");
            table1.Rows.Add(2, "ນ້ອຍ", "ໃຈໃຫຍ່", "ຊາຍ", "5933658", "Noy", "5678", "Admin");
            table1.Rows.Add(3, "ສີໄພ", "ຕາງອນ", "ຍິງ", "5933658", "Seephai", "91011", "User");
            table1.Rows.Add(4, "ຕຸກຕາ", "ໝອນທອງ", "ຍິງ", "5933658", "Tookta", "112211", "User");
            table1.Rows.Add(5, "ພູທອນ", "ບ້ານນອກ", "ຊາຍ", "5933658", "Phouthone", "332244", "User");
            table1.Rows.Add(6, "ເພັດສະໝອນ", "ອອນຊອນ", "ຊາຍ", "5933658", "Phetsamone", "886644", "User");
            data1.Tables.Add(table1);
            dataGridView1.DataSource = table1;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBox1.Checked)
            {
                textBox6.UseSystemPasswordChar = false;
            }
            else
            {
                textBox6.UseSystemPasswordChar = true;
            }
            
        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MEmpEdit mEmpEdit = new MEmpEdit(this);
            try
            {
                mEmpEdit.label1.Text = "ແກ້ໄຂຂໍ້ມູນພະນັກງານ";
                mEmpEdit.label1.Image = Construction_System.Properties.Resources.pencil;
                mEmpEdit.label1.Size = new System.Drawing.Size(154, 26);
                mEmpEdit.button1.Text = "ແກ້ໄຂ";
                mEmpEdit.ShowDialog();
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowMessage("ເກີດຂໍ້ຜີດພາດ " + ex + " ", "", "ເກີດຂໍ້ຜີດພາດ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //textBox2.Text = Convert.ToDouble(textBox4.Text).ToString("###");
            MEmpEdit mEmpEdit = new MEmpEdit(this);
            try
            {
                mEmpEdit.label1.Text = " ເພີ່ມຂໍ້ມູນພະນັກງານ";
                mEmpEdit.label1.Image = Construction_System.Properties.Resources.add_button;
                mEmpEdit.label1.Size = new System.Drawing.Size(154, 26);
                mEmpEdit.button1.Text = "ເພີ່ມ";
                mEmpEdit.ShowDialog();
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
            dataGridView1.ClearSelection();
            sumQty();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                MEmpEdit mEmpEdit = new MEmpEdit(this);
                var senderGrid = (DataGridView)sender;

                if (senderGrid.Columns[e.ColumnIndex] is DataGridViewImageColumn &&
                e.RowIndex >= 0)
                {
                    //TODO - Button Clicked - Execute Code Here
                    mEmpEdit.label1.Text = " ລົບຂໍ້ມູນພະນັກງານ";
                    mEmpEdit.label1.Image = Construction_System.Properties.Resources.bin;
                    mEmpEdit.label1.Size = new System.Drawing.Size(154, 26);
                    mEmpEdit.button1.Text = "ລົບ";
                    mEmpEdit.ShowDialog();

                    //dataGridView1.Rows.RemoveAt(dataGridView1.Rows[e.RowIndex].Index);
                    //sumQty();
                    //MyMessageBox.ShowMessage("ລົບຂໍ້ມູນສຳເລັດແລ້ວ", "", "ສຳເລັດ", MessageBoxButtons.OK, MessageBoxIcon.None);
                }
            }
            catch (Exception ex)
            {

                MyMessageBox.ShowMessage("ເກີດຂໍ້ຜີດພາດ " + ex + " ", "", "ເກີດຂໍ້ຜີດພາດ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                (dataGridView1.DataSource as DataTable).DefaultView.RowFilter = string.Format("emName LIKE '%{0}%' " +
                    "or empSurnmae LIKE '%{0}%' or number LIKE '%{0}%' or username LIKE '%{0}%' " +
                    "or sex LIKE '%{0}%'", textBox1.Text);
                sumQty();
            }
            catch (Exception ex)
            {

                MyMessageBox.ShowMessage("ເກີດຂໍ້ຜີດພາດ " + ex + " ", "", "ເກີດຂໍ້ຜີດພາດ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == 7 && e.Value != null)
            {
                e.Value = new String('●', e.Value.ToString().Length);
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox2.Text = dataGridView1.CurrentRow.Cells["Column3"].Value.ToString();
            textBox3.Text = dataGridView1.CurrentRow.Cells["Column4"].Value.ToString();
            comboBox1.Text = dataGridView1.CurrentRow.Cells["Column5"].Value.ToString();
            textBox4.Text = dataGridView1.CurrentRow.Cells["Column6"].Value.ToString();
            textBox5.Text = dataGridView1.CurrentRow.Cells["Column8"].Value.ToString();
            textBox6.Text = dataGridView1.CurrentRow.Cells["Column9"].Value.ToString();
            comboBox2.Text = dataGridView1.CurrentRow.Cells["Column7"].Value.ToString();
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            //string s = textBox4.Text;
            //if (s.Length >= 4)
            //{
            //    double sAsD = double.Parse(s);
            //    textBox4.Text = string.Format("{0:#,###}", sAsD).ToString();
            //}
            //if (textBox4.Text.Length > 1)
            //    textBox4.SelectionStart = textBox4.Text.Length;
            //textBox4.SelectionLength = 0;
        }
    }
}
