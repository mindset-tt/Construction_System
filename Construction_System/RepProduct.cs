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
    public partial class RepProduct : Form
    {
        private string _empId;
        public RepProduct(string empId)
        {
            InitializeComponent();
            _empId = empId;
        }
    }
}
