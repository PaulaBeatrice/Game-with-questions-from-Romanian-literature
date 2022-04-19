using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;

namespace Atestat_JocCulturaGenerala_Romana_
{
    public partial class afisare_intrebare : UserControl
    {
        public afisare_intrebare()
        {
            InitializeComponent();
        }
        OleDbConnection conn;

        private void afisare_intrebare_Load(object sender, EventArgs e)
        {
            string cs = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source=grila.accdb";
            conn = new OleDbConnection(cs);
            conn.Open();
            string q = "SELECT * FROM intrebari";
            OleDbCommand c = new OleDbCommand(q, conn);
            OleDbDataAdapter da = new OleDbDataAdapter(c);
            DataTable t = new DataTable();
            da.Fill(t);
            dataGridView1.DataSource = t;
        }
    }
}
