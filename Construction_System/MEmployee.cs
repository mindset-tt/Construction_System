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
        public string _empId;
        public string _EMPPAss;
        public MEmployee(string empId, string eMPPAss)
        {
            InitializeComponent();
            _empId = empId;
            _EMPPAss = eMPPAss;
        }

        public void sumQty()
        {
            try
            {
                if (dataGridView1.Rows.Count != 0)
                {
                    label4.Text = dataGridView1.RowCount.ToString("#,###") + "    ລາຍການ";
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

        public bool Checkcellclick = false;
        private readonly config _config = new config();
        string query = "";
        public void LoadData(string filter = "WHERE [empStatus] = 'active' ")
        {
            Checkcellclick = false;
            query = $"SELECT [empName], [empGender], [empTel], [empAdress], [empRole], [empId], [empPass] FROM employee "+ 
                filter +"ORDER BY [empRole] ASC";
            _config.LoadData(query, dataGridView1);
            if (dataGridView1.RowCount <= 0)
            {
                MyMessageBox.ShowMessage("ຂໍອະໄພ, ຍັງບໍ່ມີຂໍ້ມູນໃດໆ. ກະລຸນາເພີ່ມຂໍ້ມູນ", "", "ແຈ້ງເຕືອນ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            textBox6.Text = "";
            comboBox1.Text = "ກະລຸນາເລືອກ *";
            comboBox2.Text = "ກະລຸນາເລືອກ *";
            sumQty();
        }

        private void MEmployee_Load(object sender, EventArgs e)
        {
            LoadData();
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
                if (textBox2.Text == "" || textBox3.Text == "" || textBox4.Text == "" || textBox5.Text == "" || textBox6.Text == ""
                     || comboBox1.Text == "ກະລຸນາເລືອກ *" || comboBox2.Text == "ກະລຸນາເລືອກ *")
                {
                    MyMessageBox.ShowMessage("ຂໍອະໄພ, ກະລຸນາປ້ອນຂໍ້ມູນໃຫ້ຄົບຖ້ວນ", "", "ຜິດພາດ", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
                else
                {
                    mEmpEdit.label1.Text = " ເພີ່ມຂໍ້ມູນພະນັກງານ";
                    mEmpEdit.label1.Image = Construction_System.Properties.Resources.add_button;
                    mEmpEdit.label1.Size = new System.Drawing.Size(154, 26);
                    mEmpEdit.button1.Text = "ເພີ່ມ";
                    mEmpEdit.ShowDialog();
                }
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
                (dataGridView1.DataSource as DataTable).DefaultView.RowFilter = string.Format("empName LIKE '%{0}%' " +
                    "or empId LIKE '%{0}%' or empTel LIKE '%{0}%' or empRole LIKE '%{0}%' " +
                    "or empAdress LIKE '%{0}%' or empGender LIKE '%{0}%'", textBox1.Text);
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
            Checkcellclick = true;
            textBox2.Text = dataGridView1.CurrentRow.Cells["Column3"].Value.ToString();
            textBox3.Text = dataGridView1.CurrentRow.Cells["Column6"].Value.ToString();
            comboBox1.Text = dataGridView1.CurrentRow.Cells["Column4"].Value.ToString();
            textBox4.Text = dataGridView1.CurrentRow.Cells["Column5"].Value.ToString();
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

        private void checkBox1_CheckedChanged_1(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                textBox6.UseSystemPasswordChar = false;
            }
            else
            {
                textBox6.UseSystemPasswordChar = true;
            }
        }
    }
}
