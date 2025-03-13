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
    public partial class RepOrder : Form
    {
        private string _empId;
        public RepOrder(string empId)
        {
            InitializeComponent();
            _empId = empId;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ReportOrder Report = new ReportOrder();
            if (comboBox2.Text == "ທັງໝົດ")
            {
                Report.SetParameterValue("StartDate", dateTimePicker1.Text);
                Report.SetParameterValue("EndDate", dateTimePicker2.Text);
                Report.SetParameterValue("SupID", "");
                Report.SetParameterValue("SupName", comboBox2.Text);
                Report.SetParameterValue("SupplierID", "%");
                Report.SetParameterValue("Date1", dateTimePicker1.Text + " 00:00:00");
                Report.SetParameterValue("Date2", dateTimePicker2.Text + " 23:59:59");
                crystalReportViewer1.Refresh();
                crystalReportViewer1.ReportSource = Report;
            }
            else
            {
                Report.SetParameterValue("SupplierID", comboBox2.SelectedValue);
                Report.SetParameterValue("Date1", dateTimePicker1.Text + " 00:00:00");
                Report.SetParameterValue("Date2", dateTimePicker2.Text + " 23:59:59");
                Report.SetParameterValue("StartDate", dateTimePicker1.Text);
                Report.SetParameterValue("EndDate", dateTimePicker2.Text);
                Report.SetParameterValue("SupID", comboBox2.SelectedValue);
                Report.SetParameterValue("SupName", comboBox2.Text);
                crystalReportViewer1.Refresh();
                crystalReportViewer1.ReportSource = Report;
            }
            //crystalReportViewer1.ToolPanelView = CrystalDecisions.Windows.Forms.ToolPanelViewType.None;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.crystalReportViewer1.PrintReport();
        }

        private void RepOrder_Load(object sender, EventArgs e)
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
