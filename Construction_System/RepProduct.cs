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
    public partial class RepProduct : Form
    {
        private string _empId;
        public RepProduct(string empId)
        {
            InitializeComponent();
            _empId = empId;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ReportProduct Report = new ReportProduct();
            if (comboBox2.Text == "ທັງໝົດ")
            {
                Report.SetParameterValue("SupID", "%");
                Report.SetParameterValue("SupName", comboBox2.Text);
                crystalReportViewer1.Refresh();
                crystalReportViewer1.ReportSource = Report;
            }
            else
            {
                Report.SetParameterValue("SupID", comboBox2.SelectedValue.ToString());
                Report.SetParameterValue("SupName", comboBox2.Text);
                crystalReportViewer1.Refresh();
                crystalReportViewer1.ReportSource = Report;
            }
            crystalReportViewer1.ToolPanelView = CrystalDecisions.Windows.Forms.ToolPanelViewType.None;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.crystalReportViewer1.PrintReport();
        }

        private void RepProduct_Load(object sender, EventArgs e)
        {
            LoadSupplier();
            comboBox2.Text = "ທັງໝົດ";
        }

        private readonly config _config = new config();
        private void LoadSupplier()
        {
            var query = "SELECT [supplierId], [supplierName] FROM [POSSALE].[dbo].[supplier] ORDER BY [supplierName] ASC";
            _config.LoadData(query, comboBox2, "supplierId", "supplierName", "ທັງໝົດ");
        }
    }
}
