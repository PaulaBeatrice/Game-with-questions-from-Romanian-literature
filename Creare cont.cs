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
    public partial class Creeare_cont_elev : Form
    {
        public Creeare_cont_elev()
        {
            InitializeComponent();
        }
        OleDbConnection conn;
        List<int> LID;
        private void Creeare_cont_elev_Shown(object sender, EventArgs e)
        {
            string cs = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source=grila.accdb";
            conn = new OleDbConnection(cs);
            conn.Open();
            LID = new List<int>();
            string q = "SELECT * FROM Recuperare_parola";
            OleDbCommand c = new OleDbCommand(q, conn);
            OleDbDataReader dr = c.ExecuteReader();
            while (dr.Read())
            {
                comboBox1.Items.Add(dr[0].ToString() + " - " + dr[1].ToString());
                LID.Add(int.Parse(dr[0].ToString()));
            }
            dr.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string utilizator = textBox1.Text;
            string nume = textBox2.Text;
            string parola = textBox3.Text;
            string tip = "";
            int ok = 0;
            string cod = "";
            if (radioButton1.Checked == true)
                tip = "elev";
            if (radioButton2.Checked == true)
            {
                tip = "admin";
                ok = 1;
            }
            if (ok == 1)
                cod = textBox4.Text;
            int id_i = comboBox1.SelectedIndex;
            string raspuns = textBox5.Text;
            if (utilizator != "" && nume != "" && parola != "" && tip != "" && (radioButton1.Checked == true || (radioButton2.Checked == true && cod != "")) && id_i != -1 && raspuns != "")
            {
                int bun = 1;
                if ((radioButton2.Checked == true && cod == "PB101002") || radioButton1.Checked == true)
                {
                    int nr = id_i + 1;
                    int pp = 0;
                    string t = "SELECT * FROM Utilizatori WHERE Utilizator='" + utilizator + "'";
                    OleDbCommand p = new OleDbCommand(t, conn);
                    OleDbDataReader drrr = p.ExecuteReader();
                    if (drrr.Read())
                        pp = int.Parse(drrr[0].ToString());
                    if (pp == 0)
                    {
                        string q = "INSERT INTO Utilizatori(Utilizator, Nume_prenume, Parola, Tip, Id_recuperare_parola, raspuns_recuperare_parola) VALUES ";
                        q = q + "('" + utilizator + "','" + nume + "','" + parola + "','" + tip + "'," + nr + ",'" + raspuns + "')";
                        OleDbCommand c = new OleDbCommand(q, conn);
                        c.ExecuteNonQuery();
                        MessageBox.Show("Contul tău a fost salvat!");
                        textBox1.Text = "";
                        textBox2.Text = "";
                        textBox3.Text = "";
                        textBox5.Text = "";
                        checkBox1.Checked = false;
                        radioButton1.Checked = false;
                        radioButton2.Checked = false;
                        label6.Visible = false;
                        textBox4.Text = "";
                        textBox4.Visible = false;
                        comboBox1.SelectedIndex = -1;
                    }
                    else
                    {
                        MessageBox.Show("Numele de utlizator există deja!");
                        textBox1.Text = "";
                    }                   
                }
                else bun = 0;
                if (bun == 0)
                    MessageBox.Show("Codul introdus nu este corect!");
            }
            else
            {
                string de_completat = "Completați ";
                int cnt = 0;
                string incomplet = "";
                if (utilizator == "")
                {
                    cnt++;
                    incomplet = incomplet + "numele ";
                }
                if (nume == "")
                {
                    cnt++;
                    incomplet = incomplet + "nume ";
                }
                if (parola == "")
                {
                    cnt++;
                    incomplet = incomplet + "parola ";
                }
                if (tip == "")
                {
                    cnt++;
                    incomplet = incomplet + "tipul ";
                }
                if (id_i == -1)
                {
                    cnt++;
                    incomplet = incomplet + "întrebarea ";
                }
                if (raspuns == "")
                {
                    cnt++;
                    incomplet = incomplet + "răspunsul ";
                }
                string[] S = incomplet.Split();
                for (int i = 0; i < S.Length; i++)
                {
                    if (S[i] == "numele")
                    {
                        de_completat = de_completat + S[i] + " de utilizator";
                        if (cnt == 2)
                            de_completat = de_completat + " și ";
                        if (cnt > 1 && cnt != 2)
                            de_completat = de_completat + ", ";
                        cnt--;
                    }
                    if (S[i] == "nume")
                    {
                        de_completat = de_completat + S[i] + "le și prenumele dumneavoastră";
                        if (cnt == 2)
                            de_completat = de_completat + " și ";
                        if (cnt > 1 && cnt != 2)
                            de_completat = de_completat + ", ";
                        cnt--;
                    }
                    if (S[i] == "parola")
                    {
                        de_completat = de_completat + S[i] + " contului";
                        if (cnt == 2)
                            de_completat = de_completat + " și ";
                        if (cnt > 1 && cnt != 2)
                            de_completat = de_completat + ", ";
                        cnt--;
                    }
                    if (S[i] == "tipul")
                    {
                        de_completat = de_completat + S[i] + " utilizatorului și codul (dacă este cazul)";
                        if (cnt == 2)
                            de_completat = de_completat + " și ";
                        if (cnt > 1 && cnt != 2)
                            de_completat = de_completat + ", ";
                        cnt--;
                    }
                    if (S[i] == "întrebarea")
                    {
                        de_completat = de_completat + S[i] + " pentru recuperarea parolei";
                        if (cnt == 2)
                            de_completat = de_completat + " și ";
                        if (cnt > 1 && cnt != 2)
                            de_completat = de_completat + ", ";
                        cnt--;
                    }
                    if (S[i] == "răspunsul")
                    {
                        de_completat = de_completat + S[i] + " la întrebarea aleasă";
                        if (cnt == 2)
                            de_completat = de_completat + " și ";
                        if (cnt > 1 && cnt != 2)
                            de_completat = de_completat + ", ";
                        cnt--;
                    }
                }
                MessageBox.Show(de_completat + "!");
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            label6.Visible = true;
            textBox4.Visible = true;
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            label6.Visible = false;
            textBox4.Visible = false;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
                textBox3.PasswordChar = '\0';
            if (checkBox1.Checked == false)
                textBox3.PasswordChar = '*';
        }
    }
}
