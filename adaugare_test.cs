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
    public partial class adaugare_test : UserControl
    {
        public adaugare_test()
        {
            InitializeComponent();
        }
        OleDbConnection conn;
        List<int> LID;
        private void button1_Click(object sender, EventArgs e)
        {
            string titlu = textBox1.Text;
            int nrintrebari = checkedListBox1.CheckedItems.Count;
            string tip = "";
            if (radioButton1.Checked)
                tip = "Ușor";
            if (radioButton2.Checked)
                tip = "Mediu";
            if (radioButton3.Checked)
                tip = "Dificil";
            if (radioButton4.Checked)
                tip = "Opera";
            if (radioButton5.Checked)
                tip = "Complex";
            if (nrintrebari != 0 && titlu != "" && tip != "")
            {
                int pp = 0;
                string t = "SELECT * FROM teste WHERE Titlu='" + titlu + "'";
                OleDbCommand p = new OleDbCommand(t, conn);
                OleDbDataReader drrr = p.ExecuteReader();
                if (drrr.Read())
                    pp = int.Parse(drrr[0].ToString());
                if (pp == 0)
                {
                    string id_q = "";
                    foreach (string s in checkedListBox1.CheckedItems)
                    {
                        string[] S = s.Split();
                        id_q = id_q + S[0] + " ";
                    }
                    string q = "INSERT INTO teste (Titlu, Intrebari, Tip) VALUES ";
                    q = q + "('" + titlu + "', '" + id_q + "','" + tip + "')";
                    OleDbCommand c = new OleDbCommand(q, conn);
                    c.ExecuteNonQuery();
                    MessageBox.Show("Testul a fost salvat");
                    textBox1.Text = "";
                    radioButton1.Checked = false;
                    radioButton2.Checked = false;
                    radioButton3.Checked = false;
                    radioButton4.Checked = false;
                    radioButton5.Checked = false;
                    label3.Text = "";
                    comboBox1.Text = "";
                    comboBox1.SelectedIndex = -1;
                    comboBox2.SelectedIndex = -1;
                    comboBox2.Text = "";
                    incarca_intrebari();
                }
                else
                {
                    MessageBox.Show("Titlul testului a fost deja folosit!");
                    textBox1.Text = "";
                }
                    
            }
            else
                {
                    string de_completat = "Completați ";
                    int cnt = 0;
                    string incomplet = "";
                    if (titlu == "")
                    {
                        cnt++;
                        incomplet = incomplet + "titlul ";
                    }
                    if (tip == "")
                    {
                        cnt++;
                        incomplet = incomplet + "tipul ";
                    }
                    if (checkedListBox1.CheckedItems.Count == 0)
                    {
                        cnt++;
                        incomplet = incomplet + "întrebările ";
                    }
                    string[] S = incomplet.Split();
                    for (int i = 0; i < S.Length; i++)
                    {
                        if (S[i] == "titlul")
                        {
                            de_completat = de_completat + S[i] + " testului";
                            if (cnt == 2)
                                de_completat = de_completat + " și ";
                            if (cnt > 1 && cnt != 2)
                                de_completat = de_completat + ", ";
                            cnt--;
                        }
                        if (S[i] == "tipul")
                        {
                            de_completat = de_completat + S[i] + " testului";
                            if (cnt == 2)
                                de_completat = de_completat + " și ";
                            if (cnt > 1 && cnt != 2)
                                de_completat = de_completat + ", ";
                            cnt--;
                        }
                        if (S[i] == "întrebările")
                        {
                            de_completat = de_completat + S[i];
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
        private void incarca_intrebari()
        {
            string q = "SELECT * FROM intrebari";
            OleDbCommand c = new OleDbCommand(q, conn);
            OleDbDataReader dr = c.ExecuteReader();
            checkedListBox1.Items.Clear();
            while (dr.Read())
                checkedListBox1.Items.Add(dr[0].ToString() + " : " + dr[2].ToString());
            dr.Close();
        }

        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            label3.Text = checkedListBox1.SelectedItem.ToString();
        }

        private void adaugare_test_Load(object sender, EventArgs e)
        {
            string cs = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source=grila.accdb";
            conn = new OleDbConnection(cs);
            conn.Open();
            incarca_intrebari();
            LID = new List<int>();
            string q = "SELECT * FROM Categorie ORDER BY ID";
            OleDbCommand c = new OleDbCommand(q, conn);
            OleDbDataReader dr = c.ExecuteReader();
            while (dr.Read())
            {
                comboBox1.Items.Add(dr[0].ToString() + " ) " + dr[1].ToString());
                LID.Add(int.Parse(dr[0].ToString()));
            }
            dr.Close();
        }
        string[] o;
        string n;
        private void button2_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex != -1 || comboBox2.SelectedIndex != -1)
            {
                if(comboBox1.SelectedIndex != -1 && comboBox2.SelectedIndex != -1)
                {
                    o = comboBox1.Text.Split() ;
                    string nr = o[0];
                    n = comboBox2.Text;
                    if (comboBox2.Text == "Ușor")
                        n = "usor";
                    if (comboBox2.Text == "Mediu")
                        n = "mediu";
                    if (comboBox2.Text == "Dificil")
                        n = "dificil";
                    string q = "SELECT * FROM intrebari WHERE Id_opera='" + nr + "' AND Dificultate='" + n + "'";
                    OleDbCommand c = new OleDbCommand(q, conn);
                    OleDbDataReader dr = c.ExecuteReader();
                    checkedListBox1.Items.Clear();
                    while (dr.Read())
                        checkedListBox1.Items.Add(dr[0].ToString() + " : " + dr[2].ToString());
                    dr.Close();
                    label3.Text = "";
                    comboBox1.SelectedIndex = -1;
                    comboBox2.SelectedIndex = -1;
                }
                else
                {
                    if (comboBox1.SelectedIndex != -1)
                    {
                        o = comboBox1.Text.Split();
                        string nr = o[0];
                        string q = "SELECT * FROM intrebari WHERE Id_opera='" + nr + "'";
                        OleDbCommand c = new OleDbCommand(q, conn);
                        OleDbDataReader dr = c.ExecuteReader();
                        checkedListBox1.Items.Clear();
                        while (dr.Read())
                            checkedListBox1.Items.Add(dr[0].ToString() + " : " + dr[2].ToString());
                        dr.Close();
                        label3.Text = "";
                        comboBox1.SelectedIndex = -1;
                        comboBox2.SelectedIndex = -1;
                    }
                    if (comboBox2.SelectedIndex != -1)
                    {
                        n = comboBox2.Text;
                        if (comboBox2.Text == "Ușor")
                            n = "usor";
                        if (comboBox2.Text == "Mediu")
                            n = "mediu";
                        if (comboBox2.Text == "Dificil")
                            n = "dificil";
                        string q = "SELECT * FROM intrebari WHERE Dificultate='" + n + "'";
                        OleDbCommand c = new OleDbCommand(q, conn);
                        OleDbDataReader dr = c.ExecuteReader();
                        checkedListBox1.Items.Clear();
                        while (dr.Read())
                            checkedListBox1.Items.Add(dr[0].ToString() + " : " + dr[2].ToString());
                        dr.Close();
                        label3.Text = "";
                        comboBox1.SelectedIndex = -1;
                        comboBox2.SelectedIndex = -1;
                    }
                }
            }
            else
                MessageBox.Show("Alegeți o categorie!");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string titlu = "";
            titlu = textBox2.Text;
            int nrintrebari = 0;
            string tip = "Complex";
            nrintrebari = (int)numericUpDown1.Value;
            if (titlu != "" && nrintrebari != 0)
            {
                int pp = 0;
                string t = "SELECT * FROM teste WHERE Titlu='" + titlu + "'";
                OleDbCommand p = new OleDbCommand(t, conn);
                OleDbDataReader drrr = p.ExecuteReader();
                if (drrr.Read())
                    pp = int.Parse(drrr[0].ToString());
                if(pp == 0)
                {
                    string id_q = "";
                    string qr = "SELECT TOP " + nrintrebari + " ID FROM intrebari ORDER BY rnd(ID)";
                    OleDbCommand cr = new OleDbCommand(qr, conn);
                    OleDbDataReader dr = cr.ExecuteReader();
                    while (dr.Read())
                        id_q = id_q + dr[0].ToString() + " ";
                    dr.Close();
                    string q = "INSERT INTO teste (Titlu, Intrebari, Tip ) VALUES ";
                    q = q + "('" + titlu + "', '" + id_q + "', '" + tip + "')";
                    OleDbCommand c = new OleDbCommand(q, conn);
                    c.ExecuteNonQuery();
                    MessageBox.Show("Testul a fost salvat!");
                    textBox2.Text = "";
                    numericUpDown1.Value = 0;
                }
                else
                {
                    MessageBox.Show("Titlul testului a fost deja folosit!");
                    textBox2.Text = "";
                }
            }
            else
            {
                if (titlu == "" && nrintrebari == 0)
                    MessageBox.Show("Completați titlul testului și introduceți numărul de întrebări!");
                else
                {
                    if (titlu == "")
                        MessageBox.Show("Completați titlul testului!");
                    if (nrintrebari == 0)
                        MessageBox.Show("Introduceți numărul de întrebări!");
                }
            }
        }
    }
}
