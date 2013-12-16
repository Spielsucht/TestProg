using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Emotiv
{
    static class Program
    {
        /// <summary>
        /// Der Haupteinstiegspunkt für die Anwendung.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            MainWindow mw = new MainWindow();
            Model mo = new Model();
            IController co = new Coordinator(mw, mo);
            Application.Run(mw);
        }
    }
}
