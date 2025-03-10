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
    public partial class MSupAddProduct : Form
    {
        MSupplier msupplier;
        public MSupAddProduct(MSupplier msupplier)
        {
            InitializeComponent();
            this.msupplier = msupplier;
        }

        private readonly config _config = new config();
        string query = "";

        private void LoadProducts(string filter = "WHERE p.[cancel] = 'yes'")
        {
            query = $"SELECT p.[prodID], p.[prodName], t.[typeName], u.[unitName] FROM " +
                "[POSSALE].[dbo].[product] p " +
                "INNER JOIN [POSSALE].[dbo].[type] t ON p.typeId = t.typeId " +
                "INNER JOIN [POSSALE].[dbo].[unit] u ON p.unitId = u.unitId " +
                filter;
            _config.LoadData(query, dataGridView1);
        }


        private void productSupplier(string filter = "")
        {
            var query = "SELECT p.[prodID], CAST(p.[prodName] AS NVARCHAR(MAX)) as [prodName], t.[typeName], u.[unitName] " +
             "FROM [POSSALE].[dbo].[supplierDetail] sp " +
             "INNER JOIN [POSSALE].[dbo].[product] p ON sp.prodId = p.prodID " +
             "INNER JOIN [POSSALE].[dbo].[supplier] s ON s.supplierId = sp.supplierId " +
             "INNER JOIN [POSSALE].[dbo].[type] t ON p.typeId = t.typeId " +
             "INNER JOIN [POSSALE].[dbo].[unit] u ON p.unitId = u.unitId " +
             $"WHERE p.[cancel] = 'yes' and sp.[supplierId] = '{msupplier.supplierIdAddPro}' " +
             "GROUP BY p.[prodID], CAST(p.[prodName] AS NVARCHAR(MAX)), p.[prodQty], p.[prodPrice], t.[typeName], u.[unitName]";
            _config.LoadData(query, dataGridView2);
        }

        private void MSupAddProduct_Load(object sender, EventArgs e)
        {
            LoadProducts();
            productSupplier();
            //MyMessageBox.ShowMessage("ເກີດຂໍ້ຜີດພາດ " + msupplier.supplierIdAddPro + " ", "", "ເກີດຂໍ້ຜີດພາດ", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                (dataGridView1.DataSource as DataTable).DefaultView.RowFilter = string.Format("prodName LIKE '%{0}%' or unit LIKE '%{0}%' " +
                    "or typeName LIKE '%{0}%' or unitName LIKE '%{0}%'", textBox1.Text);
            }
            catch (Exception ex)
            {

                MyMessageBox.ShowMessage("ເກີດຂໍ້ຜີດພາດ " + ex + " ", "", "ເກີດຂໍ້ຜີດພາດ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridView1_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            dataGridView1.ClearSelection();
        }

        private void dataGridView1_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            dataGridView1.ClearSelection();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            query = $"INSERT INTO [POSSALE].[dbo].[supplierDetail] ([supplierId], [prodId]) " +
                    $"VALUES ('{msupplier.supplierIdAddPro}', '{dataGridView1.CurrentRow.Cells["id1"].Value}')";
            _config.setData(query);
            MyMessageBox.ShowMessage("ເພິ່ມຂໍ້ມູນສຳເລັດແລ້ວ", "", "ສຳເລັດ", MessageBoxButtons.OK, MessageBoxIcon.None);
            LoadProducts();
            productSupplier();
        }

        private void dataGridView2_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            dataGridView2.ClearSelection();
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            query = $"DELETE FROM [POSSALE].[dbo].[supplierDetail] WHERE [prodId] = '{dataGridView2.CurrentRow.Cells["id2"].Value}' " +
                $"and [supplierId] = '{msupplier.supplierIdAddPro}'";
            _config.deleteData(query);
            MyMessageBox.ShowMessage("ລົບຂໍ້ມູນສຳເລັດແລ້ວ", "", "ສຳເລັດ", MessageBoxButtons.OK, MessageBoxIcon.None);
            LoadProducts();
            productSupplier();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
