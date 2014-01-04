using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using SecureApp;

namespace exam
{
    class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            string path = @"Software\WWSoft\Protection";
            Secure scr = new Secure();
            bool logic = scr.Algorithm("admin123#", path);
            if (logic == true)
            {
                Application.Run(new frm_mdi_parent());
            }
        }
    }
}
