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
    public partial class intrebari : Form
    {
        public intrebari()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            panel2.Controls.Clear();
            adaugare_intrebare i1 = new adaugare_intrebare();
            panel2.Controls.Add(i1);
        }

        private void intrebari_Load(object sender, EventArgs e)
        {
            button1.BackColor = Color.Transparent;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            panel2.Controls.Clear();
            afisare_intrebare i1 = new afisare_intrebare();
            panel2.Controls.Add(i1);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            panel2.Controls.Clear();
            modificare_intrebare i1 = new modificare_intrebare();
            panel2.Controls.Add(i1);
        }
    }
}
