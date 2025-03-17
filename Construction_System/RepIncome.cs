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
    public partial class RepIncome : Form
    {
        private string _EMPNAME;
        public RepIncome(string EMPNAME)
        {
            InitializeComponent();
            _EMPNAME = EMPNAME;
        }


        private void button1_Click(object sender, EventArgs e)
        {
            ReportIncome Report = new ReportIncome();
            Report.SetParameterValue("StartDate", dateTimePicker1.Text);
            Report.SetParameterValue("EndDate", dateTimePicker2.Text);
            Report.SetParameterValue("EmpName", _EMPNAME);
            Report.SetParameterValue("Date1", dateTimePicker1.Text + " 00:00:00");
            Report.SetParameterValue("Date2", dateTimePicker2.Text + " 23:59:59");
            crystalReportViewer1.Refresh();
            crystalReportViewer1.ReportSource = Report;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.crystalReportViewer1.PrintReport();
        }
    }
}
