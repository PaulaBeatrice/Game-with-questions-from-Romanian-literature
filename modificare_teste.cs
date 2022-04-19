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
    public partial class modificare_teste : UserControl
    {
        public modificare_teste()
        {
            InitializeComponent();
        }
        OleDbConnection conn;
        private void incarca_teste()
        {
            string q = "SELECT * FROM teste ORDER BY Titlu";
            OleDbCommand c = new OleDbCommand(q, conn);
            OleDbDataReader dr = c.ExecuteReader();
            listBox1.Items.Clear();
            while (dr.Read())
            {
                listBox1.Items.Add(dr[0].ToString() + " ) " + dr[1].ToString());
            }
            dr.Close();
        }

        private void incarca_intrebari(int idt)
        {
            string qt = "SELECT Intrebari FROM teste WHERE ID=" + idt;
            string qi = "";
            OleDbCommand ci = new OleDbCommand(qt, conn);
            OleDbDataReader dr = ci.ExecuteReader();
            while (dr.Read())
                qi = dr[0].ToString();
            dr.Close();
            qi = qi.Replace(' ', ',');
            string q = "SELECT * FROM Intrebari WHERE ID NOT IN (" + qi + ") ORDER BY ID";
            OleDbCommand c = new OleDbCommand(q, conn);
            dr = c.ExecuteReader();
            checkedListBox2.Items.Clear();
            while (dr.Read())
                checkedListBox2.Items.Add(dr[0].ToString() + " : " + dr[2].ToString());
            dr.Close();
        }

        private void modificare_teste_Load(object sender, EventArgs e)
        {
            string cs = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source=grila.accdb";
            conn = new OleDbConnection(cs);
            conn.Open();
            incarca_teste();
        }
       

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex != -1)
            {
                string[] S = listBox1.SelectedItem.ToString().Split();
                int id = int.Parse(S[0]);
                string q = "SELECT * FROM teste WHERE ID=" + id;
                OleDbCommand c = new OleDbCommand(q, conn);
                OleDbDataReader dr = c.ExecuteReader();
                while (dr.Read())
                {
                    string[] SI = dr[2].ToString().Split();
                    checkedListBox1.Items.Clear();
                    foreach (string idi in SI)
                    if (idi != "")
                        {
                            string qi = "SELECT * FROM Intrebari WHERE ID=" + idi;
                            OleDbCommand ci = new OleDbCommand(qi, conn);
                            OleDbDataReader dri = ci.ExecuteReader();
                            while (dri.Read())
                            {
                                checkedListBox1.Items.Add(dri[0].ToString() + " : " + dri[2].ToString());
                            }
                            for (int i = 0; i < checkedListBox1.Items.Count; i++)
                                checkedListBox1.SetItemChecked(i, true);
                            dri.Close();
                        }
                    if (dr[3].ToString() == "Ușor")
                        radioButton1.Checked = true;
                    if (dr[3].ToString() == "Mediu")
                        radioButton2.Checked = true;
                    if (dr[3].ToString() == "Dificil")
                        radioButton3.Checked = true;
                    if (dr[3].ToString() == "Opera")
                        radioButton4.Checked = true;
                    if (dr[3].ToString() == "Complex")
                        radioButton5.Checked = true;
                    textBox1.Text = dr[1].ToString();
                }
                dr.Close();
                incarca_intrebari(id);
            }
        }

        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            label3.Text = checkedListBox1.SelectedItem.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string titlu = textBox1.Text;
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
            if (titlu != "" && (checkedListBox1.CheckedItems.Count != 0 || checkedListBox2.CheckedItems.Count != 0) && tip != "")
            {
                int pp = 0;
                string[] r = listBox1.SelectedItem.ToString().Split();
                string t = "SELECT * FROM teste WHERE Titlu='" + titlu + "'";
                OleDbCommand p = new OleDbCommand(t, conn);
                OleDbDataReader drrr = p.ExecuteReader();
                if (drrr.Read())
                    pp = int.Parse(drrr[0].ToString());
                if (pp == 0 || pp == int.Parse(r[0]))
                {
                    string id_i = "";
                    foreach (string s in checkedListBox1.CheckedItems)
                    {
                        string[] S = s.Split();
                        id_i = id_i + S[0] + " ";
                    }
                    foreach (string s in checkedListBox2.CheckedItems)
                    {
                        string[] S = s.Split();
                        id_i = id_i + S[0] + " ";
                    }
                    string[] Si = listBox1.SelectedItem.ToString().Split();
                    int idt = int.Parse(Si[0]);
                    string q = "UPDATE teste SET Titlu='" + titlu + "', Intrebari='" + id_i + "', Tip='" + tip + "' WHERE ID=" + idt;
                    OleDbCommand c = new OleDbCommand(q, conn);
                    c.ExecuteNonQuery();
                    MessageBox.Show("Testul a fost modificat!");
                    checkedListBox1.Items.Clear();
                    textBox1.Text = "";
                    radioButton1.Checked = false;
                    radioButton2.Checked = false;
                    radioButton3.Checked = false;
                    radioButton4.Checked = false;
                    radioButton5.Checked = false;
                    label3.Text = "";
                    label7.Text = "";
                    incarca_teste();
                    checkedListBox1.Items.Clear();
                    checkedListBox2.Items.Clear();
                }
                else
                {
                    MessageBox.Show("Titlul testului a fost deja folosit!");
                    textBox1.Text = "";
                }
            }
            else
            {
                if(listBox1.SelectedIndex != -1)
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
                    if(checkedListBox1.CheckedItems.Count + checkedListBox2.CheckedItems.Count == 0)
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
                            de_completat = de_completat + S[i] ;
                            if (cnt == 2)
                                de_completat = de_completat + " și ";
                            if (cnt > 1 && cnt != 2)
                                de_completat = de_completat + ", ";
                            cnt--;
                        }
                    }
                    MessageBox.Show(de_completat + "!");
                }
                else
                {
                    MessageBox.Show("Alegeți un test!");
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex != -1)
            {
                string[] S = listBox1.SelectedItem.ToString().Split();
                int id = int.Parse(S[0]);
                string q = "DELETE FROM teste WHERE ID=" + id;
                OleDbCommand c = new OleDbCommand(q, conn);
                c.ExecuteNonQuery();
                listBox1.Items.RemoveAt(listBox1.SelectedIndex);
                checkedListBox1.Items.Clear();
                checkedListBox2.Items.Clear();
                textBox1.Text = "";
                radioButton1.Checked = false;
                radioButton2.Checked = false;
                radioButton3.Checked = false;
                radioButton4.Checked = false;
                radioButton5.Checked = false;
                label3.Text = "";
                label7.Text = "";
                MessageBox.Show("Testul a fost șters!");
            }
            else MessageBox.Show("Alege un test!");
        }

        private void checkedListBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            label7.Text = checkedListBox2.SelectedItem.ToString();
        }
    }
}
