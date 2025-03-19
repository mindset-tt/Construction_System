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
    public partial class RepSale : Form
    {
        private string _empId;
        public RepSale(string empId)
        {
            InitializeComponent();
            _empId = empId;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ReportSale Report = new ReportSale();
            Report.Refresh();
            if (comboBox2.Text == "ທັງໝົດ")
            {
                Report.SetParameterValue("empName", comboBox2.Text);
                Report.SetParameterValue("StartDate", dateTimePicker1.Text);
                Report.SetParameterValue("EndDate", dateTimePicker2.Text);
                Report.SetParameterValue("empID", "%");
                Report.SetParameterValue("Date1", dateTimePicker1.Text + " 00:00:00");
                Report.SetParameterValue("Date2", dateTimePicker2.Text + " 23:59:59");
                //dateTimePicker1.CustomFormat = "dd/MM/yyyy";
                //dateTimePicker2.CustomFormat = "dd/MM/yyyy";
                crystalReportViewer1.Refresh();
                crystalReportViewer1.ReportSource = Report;
            }
            else
            {
                Report.SetParameterValue("empName", comboBox2.Text);
                Report.SetParameterValue("StartDate", dateTimePicker1.Text);
                Report.SetParameterValue("EndDate", dateTimePicker2.Text);
                Report.SetParameterValue("empID", comboBox2.SelectedValue);
                Report.SetParameterValue("Date1", dateTimePicker1.Text + " 00:00:00");
                Report.SetParameterValue("Date2", dateTimePicker2.Text + " 23:59:59");
                crystalReportViewer1.Refresh();
                crystalReportViewer1.ReportSource = Report;
            }
            //crystalReportViewer1.ToolPanelView = CrystalDecisions.Windows.Forms.ToolPanelViewType.None;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.crystalReportViewer1.PrintReport();
        }

        private void RepSale_Load(object sender, EventArgs e)
        {
            LoadSupplier();
        }

        private readonly config _config = new config();
        private void LoadSupplier()
        {
            var query = "SELECT [empId], [empName] FROM [POSSALE].[dbo].[employee] WHERE [empStatus] = 'active' ORDER BY [empName] ASC";
            _config.LoadData(query, comboBox2, "empId", "empName", "ທັງໝົດ");
        }
    }
}
