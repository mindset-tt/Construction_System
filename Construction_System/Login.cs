using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Construction_System
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
            timer1.Start();
            //txtbUesername.Select();
            this.ActiveControl = txtbUesername;
            txtbUesername.Focus();

            txtbUesername.Text = "YAI";
            txtbPassword.Text = "12345678";
        }

        config config = new config();

        Main form1 = new Main();

        private void btnLogin_Click(object sender, EventArgs e)
        {
            //try
            //{
            config.openConnection();
            SqlDataReader dr = config.getData("select * from employee where empId = '" + txtbUesername.Text + "' and empPass = '" + txtbPassword.Text + "'");
            if (dr.Read())
            {
                MyMessageBox.ShowMessage("ເຂົ້າສູ່ລະບົບສຳເລັດ!", "", "ຍິນດີຕ້ອນຮັບ", MessageBoxButtons.OK, MessageBoxIcon.None);
                //form1.label2.Text = dr["empName"].ToString();
                form1.EMPNAME = dr["empName"].ToString();
                form1.EMPID = dr["empId"].ToString();
                form1.EMPPAss = dr["empPass"].ToString();

                if (dr["empRole"].ToString() == "User")
                {
                    form1.btnOrder.Visible = false;
                    form1.btnImport.Visible = false;
                    form1.btnReport.Visible = false;
                    form1.btnMaData.Visible = false;
                }
                form1.Show();
                txtbUesername.Text = "";
                txtbPassword.Text = "";
                this.Hide();
            }
            else
            {
                MyMessageBox.ShowMessage("ຂໍອະໄພ, ຊື່ຜູ້ໃຊ້ ຫຼື ລະຫັດຜ່ານ ບໍ່ຖຶກຕ້ອງ!", "", "ຜິດພາດ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            config.closeConnection();


            //if (txtbUesername.Text == "Yai" && txtbPassword.Text == "1234")
            //{
            //    MyMessageBox.ShowMessage("ເຂົ້າສູ້ລະບົບສຳເລັດ!", "", "ຍິນດີຕ້ອນຮັບ", MessageBoxButtons.OK, MessageBoxIcon.None);
            //    form1.label2.Text = "Admin";
            //    form1.Show();
            //    txtbUesername.Text = "";
            //    txtbPassword.Text = "";
            //    //form1.label2.Text = "Admin";
            //    this.Hide();
            //    //Login login = new Login();
            //    //login.Close();
            //    //this.Close();
            //}
            //else if (txtbUesername.Text == "Yai2" && txtbPassword.Text == "1234")
            //{
            //    MyMessageBox.ShowMessage("ເຂົ້າສູ້ລະບົບສຳເລັດ!", "", "ຍິນດີຕ້ອນຮັບ", MessageBoxButtons.OK, MessageBoxIcon.None);
            //    form1.btnOrder.Visible = false;
            //    form1.btnImport.Visible = false;
            //    form1.btnReport.Visible = false;
            //    form1.btnMaData.Visible = false;
            //    form1.label2.Text = "User";
            //    form1.Show();
            //    txtbUesername.Text = "";
            //    txtbPassword.Text = "";
            //}
            //else if (txtbUesername.Text == "" && txtbPassword.Text == "")
            //{
            //    MyMessageBox.ShowMessage("ຂໍອະໄພ, ກະລຸນາປ້ອນ ຊື່ຜູ້ໃຊ້ ຫຼື ລະຫັດຜ່ານ", "", "ກວດສອບ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //}
            //else
            //{
            //    MyMessageBox.ShowMessage("ຂໍອະໄພ, ຊື່ຜູ້ໃຊ້ ຫຼື ລະຫັດຜ່ານ ບໍ່ຖຶກຕ້ອງ!", "", "ຜິດພາດ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
        //}
            //catch (Exception ex)
            //{
            //    MyMessageBox.ShowMessage("ເກີດຂໍ້ຜີດພາດ " + ex + " ", "", "ເກີດຂໍ້ຜີດພາດ", MessageBoxButtons.OK, MessageBoxIcon.Error);

            //}
        }

        private void btnCloseLogin_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }


        private void timer1_Tick(object sender, EventArgs e)
        {
            //label4.Text = DateTime.Now.ToString();
            label4.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
        }

        private void btnLogin_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    btnLogin.PerformClick();
                }
            }
            catch (Exception ex)
            {

                MyMessageBox.ShowMessage("ເກີດຂໍ້ຜີດພາດ " + ex + " ", "", "ເກີດຂໍ້ຜີດພາດ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void textbPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnLogin.PerformClick();
            }
        }

        private void textbUesername_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtbPassword.Focus();
                e.SuppressKeyPress = true;
            }
        }
    }
}
