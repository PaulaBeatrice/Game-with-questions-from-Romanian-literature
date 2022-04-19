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
    public partial class Recuperare_parola : Form
    {
        public Recuperare_parola()
        {
            InitializeComponent();
        }
        public int idu;
        OleDbConnection conn;
        string Raspuns, parola;
        int intrebare;
        private void Recuperare_parola_Shown(object sender, EventArgs e)
        {
            string cs = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source=grila.accdb";
            conn = new OleDbConnection(cs);
            conn.Open();
            string q = "SELECT * FROM Utilizatori WHERE ID=" + idu;
            OleDbCommand c = new OleDbCommand(q, conn);
            OleDbDataReader dr = c.ExecuteReader();
            while (dr.Read())
            {
                label2.Text = dr[5].ToString();
                Raspuns = dr[6].ToString();
                parola = dr[3].ToString();
            }
            intrebare = int.Parse(label2.Text);
            dr.Close();
            string t = "SELECT * FROM Recuperare_parola WHERE ID=" + intrebare;
            OleDbCommand d = new OleDbCommand(t, conn);
            OleDbDataReader drr = d.ExecuteReader();
            while (drr.Read())
                label1.Text = drr[1].ToString();
            drr.Close();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
            {
                if (textBox1.Text == Raspuns)
                    MessageBox.Show("Parola ta este: " + parola);
                else
                {
                    MessageBox.Show("Răspuns greșit!");
                    textBox1.Text = "";
                }
            }
            else
                MessageBox.Show("Introduceți răspunsul la întrebare!");
        }
    }
}
