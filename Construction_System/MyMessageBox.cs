using Construction_System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Construction_System
{
    internal class MyMessageBox
    {
        public static System.Windows.Forms.DialogResult ShowMessage(string message, string caption, string tital, System.Windows.Forms.MessageBoxButtons button, System.Windows.Forms.MessageBoxIcon icon)
        {
            System.Windows.Forms.DialogResult dlgResult = System.Windows.Forms.DialogResult.None;
            switch (button)
            {
                case System.Windows.Forms.MessageBoxButtons.OK:
                    using (FM_Ok form_Msb_Ok = new FM_Ok())
                    {
                        form_Msb_Ok.Text = caption;
                        form_Msb_Ok.Message = message;
                        form_Msb_Ok.Title = tital;
                        switch (icon)
                        {
                            case System.Windows.Forms.MessageBoxIcon.None:
                                form_Msb_Ok.MessageIcon = Construction_System.Properties.Resources.accept__1_;
                                break;
                            case System.Windows.Forms.MessageBoxIcon.Question:
                                form_Msb_Ok.MessageIcon = Construction_System.Properties.Resources.question;
                                break;
                            case System.Windows.Forms.MessageBoxIcon.Warning:
                                form_Msb_Ok.MessageIcon = Construction_System.Properties.Resources.caution__1_1;
                                break;
                            case System.Windows.Forms.MessageBoxIcon.Error:
                                form_Msb_Ok.MessageIcon = Construction_System.Properties.Resources.close;
                                break;
                        }
                        dlgResult = form_Msb_Ok.ShowDialog();
                    }
                    break;

                case System.Windows.Forms.MessageBoxButtons.YesNo:
                    using (FM_YN form_Msb_YN = new FM_YN())
                    {
                        form_Msb_YN.Text = caption;
                        form_Msb_YN.Message = message;
                        form_Msb_YN.Title = tital;
                        switch (icon)
                        {
                            case System.Windows.Forms.MessageBoxIcon.Information:
                                form_Msb_YN.MessageIcon = Construction_System.Properties.Resources.accept__1_;
                                break;
                            case System.Windows.Forms.MessageBoxIcon.Question:
                                form_Msb_YN.MessageIcon = Construction_System.Properties.Resources.question;
                                break;
                            case System.Windows.Forms.MessageBoxIcon.Error:
                                form_Msb_YN.MessageIcon = Construction_System.Properties.Resources.caution__1_1;
                                break;
                        }
                        dlgResult = form_Msb_YN.ShowDialog();
                    }
                    break;
            }
            return dlgResult;
        }
    }
}