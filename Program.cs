using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PhotoRed
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения. 
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var mainForm = new MainForm();

            mainForm.AddFilter(new LightningFilter());
            mainForm.AddFilter(new GrayScaleFilter());
            mainForm.AddFilter(new GammaCorrectionFilter());

            Application.Run(mainForm);
        }
    }
}
