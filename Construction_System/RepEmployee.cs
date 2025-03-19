using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Construction_System
{
    public partial class RepEmployee : Form
    {
        private string _empId;
        public RepEmployee(string empId)
        {
            InitializeComponent();
            _empId = empId;
        }

        config config = new config();

        private void button1_Click(object sender, EventArgs e)
        {
            ReportEmployee report = new ReportEmployee();
            report.Refresh();

            switch (comboBox2.Text)
            {
                case "ທັງໝົດ":

                    report.SetParameterValue("emp1", "Admin");
                    report.SetParameterValue("emp2", "User");
                    crystalReportViewer1.Refresh();
                    crystalReportViewer1.ReportSource = report;
                    break;

                case "Admin":

                    report.SetParameterValue("emp1", "Admin");
                    report.SetParameterValue("emp2", "Admin");
                    crystalReportViewer1.Refresh();
                    crystalReportViewer1.ReportSource = report;
                    break;

                case "User":

                    report.SetParameterValue("emp1", "User");
                    report.SetParameterValue("emp2", "User");
                    crystalReportViewer1.Refresh();
                    crystalReportViewer1.ReportSource = report;
                    break;

                default:
                    MyMessageBox.ShowMessage("ຂໍອະໄພ, ລະບົບຂັດຂ້ອງ!", "", "ຜິດພາດ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
            }
            crystalReportViewer1.ToolPanelView = CrystalDecisions.Windows.Forms.ToolPanelViewType.None;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.crystalReportViewer1.PrintReport();
        }

        private void RepEmployee_Load(object sender, EventArgs e)
        {
            comboBox2.Text = "ທັງໝົດ";
        }
    }
}
