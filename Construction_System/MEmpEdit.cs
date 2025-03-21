using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Services.Description;
using System.Windows.Forms;

namespace Construction_System
{
    public partial class MEmpEdit : Form
    {
        MEmployee mEmployee;
        public MEmpEdit(MEmployee mEmployee)
        {
            InitializeComponent();
            this.mEmployee = mEmployee;
        }

        config config = new config();
        private readonly config _config = new config();
        string query = "";
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                switch (label1.Text)
                {
                    case " ເພີ່ມຂໍ້ມູນພະນັກງານ":

                        if (textBox1.Text == mEmployee._empId && textBox2.Text == mEmployee._EMPPAss)
                        {
                            query = "INSERT INTO [POSSALE].[dbo].[employee] ([empName], [empGender], [empTel], [empAdress], " +
                                      "[empRole], [empId], [empPass], [empStatus]) " +
                                      $"VALUES ('{mEmployee.textBox2.Text}', '{mEmployee.comboBox1.Text}', '{mEmployee.textBox4.Text}', " +
                                      $"'{mEmployee.textBox3.Text}', '{mEmployee.comboBox2.Text}', '{mEmployee.textBox5.Text}', " +
                                      $"'{mEmployee.textBox6.Text}', 'active')";
                            _config.setDataEmp(query);
                            mEmployee.LoadData();
                            this.Close();
                        }
                        else
                        {
                            MyMessageBox.ShowMessage("ຂໍອະໄພ, ຊື່ຜູ້ໃຊ້ ຫຼື ລະຫັດຜ່ານ ບໍ່ຖຶກຕ້ອງ!", "", "ຜິດພາດ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        //Close();
                        break;

                    case "ແກ້ໄຂຂໍ້ມູນພະນັກງານ":

                        if (textBox1.Text == mEmployee._empId && textBox2.Text == mEmployee._EMPPAss)
                        {
                            query = $"UPDATE [POSSALE].[dbo].[employee] SET empName = '{mEmployee.textBox2.Text}', " +
                                $"empGender = '{mEmployee.comboBox1.Text}', " +
                                $"empTel = '{mEmployee.textBox4.Text}', " +
                                $"empAdress = '{mEmployee.textBox3.Text}', " +
                                $"empRole = '{mEmployee.comboBox2.Text}', " +
                                $"empId = '{mEmployee.textBox5.Text}', " +
                                $"empPass = '{mEmployee.textBox6.Text}' " +
                                $"WHERE empId = '{mEmployee.dataGridView1.CurrentRow.Cells["Column8"].Value}'";
                            _config.updateDataEmp(query);

                            if (mEmployee.emID == mEmployee._empId)
                            {
                                if (mEmployee.emID != mEmployee.textBox5.Text || mEmployee.emPass != mEmployee._EMPPAss)
                                {

                                    DialogResult dialog = MyMessageBox.ShowMessage("ຂໍອະໄພ, ລະບົບກຳລັງດຳເນີນການເລີ່ມຕົ້ນເຂົ້າສູ່ລະບົບອີກຄັ້ງ!", "", "ແຈ້ງເຕືອນ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    if (dialog == DialogResult.OK)
                                    {
                                        Application.Restart();
                                    }
                                }
                            }
                            mEmployee.LoadData();
                            this.Close();
                        }
                        else
                        {
                            MyMessageBox.ShowMessage("ຂໍອະໄພ, ຊື່ຜູ້ໃຊ້ ຫຼື ລະຫັດຜ່ານ ບໍ່ຖຶກຕ້ອງ!", "", "ຜິດພາດ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        //Close();
                        break;

                    case " ລົບຂໍ້ມູນພະນັກງານ":

                        if (textBox1.Text == mEmployee._empId && textBox2.Text == mEmployee._EMPPAss)
                        {
                            query = $"UPDATE [POSSALE].[dbo].[employee] SET [empStatus] = 'exit'" +
                                $" WHERE [empId] = '{mEmployee.dataGridView1.CurrentRow.Cells["Column8"].Value}'";
                            _config.setData(query);
                            MyMessageBox.ShowMessage("ລົບຂໍ້ມູນສຳເລັດແລ້ວ", "", "ສຳເລັດ", MessageBoxButtons.OK, MessageBoxIcon.None);

                            if (mEmployee.emID == mEmployee._empId)
                            {
                                DialogResult dialog = MyMessageBox.ShowMessage("ຂໍອະໄພ, ລະບົບກຳລັງດຳເນີນການເລີ່ມຕົ້ນເຂົ້າສູ່ລະບົບອີກຄັ້ງ!", "", "ແຈ້ງເຕືອນ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                if (dialog == DialogResult.OK)
                                {
                                    Application.Restart();
                                }
                            }
                            mEmployee.LoadData();
                            this.Close();
                        }
                        else
                        {
                            MyMessageBox.ShowMessage("ຂໍອະໄພ, ຊື່ຜູ້ໃຊ້ ຫຼື ລະຫັດຜ່ານ ບໍ່ຖຶກຕ້ອງ!", "", "ຜິດພາດ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        //Close();
                        break;

                    default:
                        MyMessageBox.ShowMessage("ຂໍອະໄພ, ລະບົບຂັດຂ້ອງ!", "", "ຜິດພາດ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                }
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowMessage("ເກີດຂໍ້ຜີດພາດ " + ex + " ", "", "ເກີດຂໍ້ຜີດພາດ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void MEmpEdit_Load(object sender, EventArgs e)
        {
            textBox1.Text = mEmployee._empId;
            textBox2.Select();
        }
    }
}
