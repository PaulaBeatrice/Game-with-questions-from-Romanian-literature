using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Atestat_JocCulturaGenerala_Romana_
{
    public partial class teste : Form
    {
        public teste()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            panel2.Controls.Clear();
            adaugare_test c = new adaugare_test();
            panel2.Controls.Add(c);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            panel2.Controls.Clear();
            afisare_teste c = new afisare_teste();
            panel2.Controls.Add(c);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            panel2.Controls.Clear();
            modificare_teste m = new modificare_teste();
            panel2.Controls.Add(m);
        }
    }
}
