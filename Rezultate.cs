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
    public partial class Rezultate : Form
    {
        public Rezultate()
        {
            InitializeComponent();
        }
        public int idu;
        OleDbConnection conn;

        string nume_by_id(int id)
        {
            string q = "SELECT Nume_prenume FROM Utilizatori WHERE ID=" + id;
            OleDbCommand c = new OleDbCommand(q, conn);
            OleDbDataReader dr = c.ExecuteReader();
            while (dr.Read())
                return dr[0].ToString();
            return "";
        }

        private void Rezultate_Shown(object sender, EventArgs e)
        {
            string cs = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source=grila.accdb";
            conn = new OleDbConnection(cs);
            conn.Open();
            string q;
            if (idu != 0)
                q = "SELECT Utilizatori.Nume_prenume, Scor.Id_test,  Scor.Scor, Scor.Data FROM Utilizatori INNER JOIN Scor ON Utilizatori.ID=Scor.Idu WHERE Idu=" + idu;
            else
                q = "SELECT Utilizatori.Nume_prenume,  Scor.Id_test,  Scor.Scor, Scor.Data FROM Utilizatori INNER JOIN Scor ON Utilizatori.ID=Scor.Idu";
            OleDbCommand c = new OleDbCommand(q, conn);
            OleDbDataAdapter da = new OleDbDataAdapter(c);
            DataTable t = new DataTable();
            da.Fill(t);
            dataGridView1.DataSource = t;
        }
    }
}
