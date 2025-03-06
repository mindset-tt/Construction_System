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
    public partial class RepIncome : Form
    {
        private string _empId;
        public RepIncome(string empId)
        {
            InitializeComponent();
            _empId = empId;
        }
    }
}
