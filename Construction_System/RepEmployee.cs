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
            ReportEmployee reportEmployee = new ReportEmployee();
            crystalReportViewer1.Refresh();
            config.openConnection();
            SqlDataReader dr = config.getData("select * from employee where empStatus = 'active'");
            while (dr.Read())
            {
                reportEmployee.SetParameterValue("empName", dr["empName"].ToString());
                reportEmployee.SetParameterValue("empID", dr["empID"].ToString());
            }
            //foreach (DataRow record in dr.)

            crystalReportViewer1.ReportSource = reportEmployee;

            //IEnumerable<IDataRecord> GetFromReader(IDataReader reader)
            //{
            //    while (reader.Read()) yield return reader;
            //}

            //foreach (IDataRecord record in GetFromReader(dr))
            //{
            //    reportEmployee.SetParameterValue("empID", record["empID"].ToString());
            //    crystalReportViewer1.ReportSource = reportEmployee;
            //}


            //else
            //{
            //    //MyMessageBox.ShowMessage("ຂໍອະໄພ, ຊື່ຜູ້ໃຊ້ ຫຼື ລະຫັດຜ່ານ ບໍ່ຖຶກຕ້ອງ!", "", "ຜິດພາດ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    MyMessageBox.ShowMessage("ຂໍອະໄພ, ລະບົບຂັດຂ້ອງ!", "", "ຜິດພາດ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
            config.closeConnection();
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }
    }
}
