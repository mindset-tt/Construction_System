using System;
using System.Windows.Forms;

namespace Construction_System
{
    public partial class Main : Form
    {
        public string EMPID = "";
        public string EMPPAss = "";
        public string EMPNAME = "";
        public Main()
        {
            InitializeComponent();
            timer1.Start();
            hideSubMenu();
            openChildForm(new Sale(EMPID));
        }

        private void hideSubMenu()
        {
            panelReport.Visible = false;
            panelData.Visible = false;
        }

        public void showSubMenu(Panel subMenu)
        {
            if (subMenu.Visible == false)
            {
                hideSubMenu();
                subMenu.Visible = true;
            }
            else
                subMenu.Visible = false;
        }

        private void btnSale_Click(object sender, EventArgs e)
        {
            hideSubMenu();
            openChildForm(new Sale(EMPID));
        }

        private void btnOrder_Click(object sender, EventArgs e)
        {
            hideSubMenu();
            openChildForm(new Order(EMPID));
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            hideSubMenu();
            openChildForm(new Import(EMPID));
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            if (MyMessageBox.ShowMessage("ທ່ານຕ້ອງການອອກຈາກລະບົບແທ້ ຫຼື ບໍ່?", "", "ອອກຈາກລະບົບ", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Application.Restart();
            }
        }

        private Form activeForm = null;
        public void openChildForm(Form childForm)
        {
            if (activeForm != null) activeForm.Close();
            activeForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            panelForm.Controls.Add(childForm);
            panelForm.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
        }

        private void btnMaData_Click(object sender, EventArgs e)
        {
            showSubMenu(panelData);
        }

        private void btnReport_Click(object sender, EventArgs e)
        {
            showSubMenu(panelReport);
        }

        private void btnCloseApp_Click(object sender, EventArgs e)
        {
            if (MyMessageBox.ShowMessage("ທ່ານຕ້ອງການອອກຈາກລະບົບແທ້ ຫຼື ບໍ່?", "", "ອອກຈາກລະບົບ", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Application.Restart();
            }
        }

        bool maxi = true;
        private void btnMaximize()
        {
            if (maxi == false)
            {
                btnLayer.Image = Construction_System.Properties.Resources.maximize;
                maxi = true;
            }
            else
            {
                btnLayer.Image = Construction_System.Properties.Resources.layers;
                maxi = false;
            }
        }

        private void btnLayer_Click(object sender, EventArgs e)
        {
            btnMaximize();
            if (WindowState == FormWindowState.Normal)
            {
                WindowState = FormWindowState.Maximized;
            }
            else
            {
                WindowState = FormWindowState.Normal;
                //this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            }
        }

        private void btnMinus_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label1.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
        }

        private void btnMaType_Click(object sender, EventArgs e)
        {
            openChildForm(new MType(EMPID));
        }

        private void btnMaUnit_Click(object sender, EventArgs e)
        {
            openChildForm(new MUnit(EMPID));
        }

        private void btnMaSupr_Click(object sender, EventArgs e)
        {
            openChildForm(new MSupplier(EMPID));
        }

        private void btnMaSale_Click(object sender, EventArgs e)
        {
            openChildForm(new MSale(EMPID));
        }

        private void btnMaOrder_Click(object sender, EventArgs e)
        {
            openChildForm(new MOrder(EMPID));
        }

        private void btnMaImp_Click(object sender, EventArgs e)
        {
            openChildForm(new MImport(EMPID));
        }

        private void btnMaEmp_Click(object sender, EventArgs e)
        {
            openChildForm(new MEmployee(EMPID, EMPPAss));
        }

        private void btnMaProd_Click(object sender, EventArgs e)
        {
            openChildForm(new MProduct(EMPID));
        }

        private void btnReEmp_Click(object sender, EventArgs e)
        {
            openChildForm(new RepEmployee(EMPID));
        }

        private void btnProd_Click(object sender, EventArgs e)
        {
            openChildForm(new RepProduct(EMPID));
        }

        private void btnReSale_Click(object sender, EventArgs e)
        {
            openChildForm(new RepSale(EMPID));
        }

        private void btnReInc_Click(object sender, EventArgs e)
        {
            openChildForm(new RepIncome(EMPNAME));
        }

        private void btnReWaInc_Click(object sender, EventArgs e)
        {
            openChildForm(new RepWincome(EMPNAME));
        }

        private void btnReOrder_Click(object sender, EventArgs e)
        {
            openChildForm(new RepOrder(EMPID));
        }

        private void btnReIm_Click(object sender, EventArgs e)
        {
            openChildForm(new RepImport(EMPID));
        }

        private void Main_Load(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Maximized;
            label2.Text = EMPNAME;
        }
    }
}

