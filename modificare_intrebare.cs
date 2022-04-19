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
    public partial class modificare_intrebare : UserControl
    {
        public modificare_intrebare()
        {
            InitializeComponent();
        }
        OleDbConnection conn;

        private void button1_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex != -1)
            {
                string[] S = listBox1.SelectedItem.ToString().Split();
                int id = int.Parse(S[0]);
                string q = "DELETE FROM intrebari WHERE ID=" + id;
                OleDbCommand c = new OleDbCommand(q, conn);
                c.ExecuteNonQuery();
                listBox1.Items.RemoveAt(listBox1.SelectedIndex);
                listBox1.SelectedIndex = -1;
                comboBox1.SelectedIndex = -1;
                textBox1.Text = "";
                textBox2.Text = "";
                textBox3.Text = "";
                textBox4.Text = "";
                textBox5.Text = "";
                radioButton2.Checked = false;
                radioButton3.Checked = false;
                radioButton7.Checked = false;
                checkBox1.Checked = false;
                checkBox2.Checked = false;
                checkBox3.Checked = false;
                checkBox4.Checked = false;
                MessageBox.Show("Întrebarea a fost ștearsă!");
                string t = "SELECT * FROM teste ORDER BY ID";
                OleDbCommand cc = new OleDbCommand(t, conn);
                OleDbDataReader DR = cc.ExecuteReader();
                while (DR.Read())
                {
                    string[] Q;
                    string w = "", titlu = "", tip = "";
                    string idd = DR[0].ToString();
                    titlu = DR[1].ToString();
                    Q = DR[2].ToString().Split();
                    tip = DR[3].ToString();
                    for (int i = 0; i < Q.Length; i++)
                        if(Q[i] != "")
                        if (int.Parse(Q[i]) != id)
                            w = w + Q[i] + " ";
                    string x = "UPDATE teste SET Titlu='" + titlu + "', Intrebari='" + w + "', Tip='" + tip + "' WHERE ID=" + int.Parse(idd);
                    OleDbCommand C = new OleDbCommand(x, conn);
                    C.ExecuteNonQuery();
                }
                DR.Close();
            }
            else
                MessageBox.Show("Alegeți o întrebare!");
        }
        string get_opera_by_id(int id)
        {
            string q = "SELECT * FROM categorie WHERE ID=" + id;
            OleDbCommand c = new OleDbCommand(q, conn);
            OleDbDataReader dr = c.ExecuteReader();
            while (dr.Read())
            {
                return dr[1].ToString();
            }
            return "";
        }
        private void incarca_intrebari()
        {
            string q = "SELECT * FROM intrebari ORDER BY ID";
            OleDbCommand c = new OleDbCommand(q, conn);
            OleDbDataReader dr = c.ExecuteReader();
            listBox1.Items.Clear();
            while (dr.Read())
            {
                listBox1.Items.Add(dr[0].ToString() + " : " + dr[2].ToString());
            }
            dr.Close();
        }


        private void modificare_intrebare_Load(object sender, EventArgs e)
        {
            string cs = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source=grila.accdb";
            conn = new OleDbConnection(cs);
            conn.Open();
            incarca_intrebari();
            string q = "SELECT * FROM Categorie";
            OleDbCommand c = new OleDbCommand(q, conn);
            OleDbDataReader dr = c.ExecuteReader();
            comboBox1.Items.Clear();
            while (dr.Read())
            {
                comboBox1.Items.Add(dr[0].ToString() + " ) " +dr[1].ToString());
            }
            dr.Close();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex != -1)
            {
                string[] S = listBox1.SelectedItem.ToString().Split();
                int id = int.Parse(S[0]);
                S = comboBox1.Text.Split();
                int id_op = int.Parse(S[0]);
                string enunt = textBox1.Text;
                string A = textBox2.Text;
                string B = textBox3.Text;
                string C = textBox4.Text;
                string D = textBox5.Text;
                string corect = "";
                string nivel = "";
                if (checkBox1.Checked)
                    corect = corect + "A";
                if (checkBox2.Checked)
                    corect = corect + "B";
                if (checkBox3.Checked)
                    corect = corect + "C";
                if (checkBox4.Checked)
                    corect = corect + "D";
                if (radioButton7.Checked)
                    nivel = "dificil";
                if (radioButton3.Checked)
                    nivel = "mediu";
                if (radioButton2.Checked)
                    nivel = "usor";
                if (enunt != "" && A != "" && B != "" && C != "" && D != "" && corect != "" && nivel != "")
                {
                    string q = "UPDATE intrebari SET ";
                    q = q + "Id_opera=" + id_op + ", ";
                    q = q + "Enunt='" + enunt + "', ";
                    q = q + "A='" + A + "', ";
                    q = q + "B='" + B + "', ";
                    q = q + "C='" + C + "', ";
                    q = q + "D='" + D + "', ";
                    q = q + "Corect='" + corect + "', ";
                    q = q + "Dificultate='" + nivel + "' ";
                    q = q + " WHERE ID=" + id;
                    OleDbCommand c = new OleDbCommand(q, conn);
                    c.ExecuteNonQuery();
                    MessageBox.Show("Modificările au fost salvate!");
                    textBox1.Text = "";
                    textBox2.Text = "";
                    textBox3.Text = "";
                    textBox4.Text = "";
                    textBox5.Text = "";
                    checkBox1.Checked = false;
                    checkBox2.Checked = false;
                    checkBox3.Checked = false;
                    checkBox4.Checked = false;
                    radioButton2.Checked = false;
                    radioButton3.Checked = false;
                    radioButton7.Checked = false;
                    comboBox1.Text = "";
                    incarca_intrebari();
                }
                else
                {
                    string de_completat = "Completați ";
                    int cnt = 0;
                    string incomplet = "";
                    if (enunt == "")
                    {
                        cnt++;
                        incomplet = incomplet + "enunțul ";
                    }
                    if (A == "")
                    {
                        cnt++;
                        incomplet = incomplet + "A ";
                    }
                    if (B == "")
                    {
                        cnt++;
                        incomplet = incomplet + "B ";
                    }
                    if (C == "")
                    {
                        cnt++;
                        incomplet = incomplet + "C ";
                    }
                    if (D == "")
                    {
                        cnt++;
                        incomplet = incomplet + "D ";
                    }
                    if (corect == "")
                    {
                        cnt++;
                        incomplet = incomplet + "corect ";
                    }
                    if (nivel == "")
                    {
                        cnt++;
                        incomplet = incomplet + "nivelul ";
                    }
                    string[] M = incomplet.Split();
                    for (int i = 0; i < M.Length; i++)
                    {
                        if (M[i] == "enunțul")
                        {
                            de_completat = de_completat + M[i] + " problemei";
                            if (cnt == 2)
                                de_completat = de_completat + " și ";
                            if (cnt > 1 && cnt != 2)
                                de_completat = de_completat + ", ";
                            cnt--;
                        }
                        if (M[i] == "A")
                        {
                            de_completat = de_completat + "varianta " + M[i];
                            if (cnt == 2)
                                de_completat = de_completat + " și ";
                            if (cnt > 1 && cnt != 2)
                                de_completat = de_completat + ", ";
                            cnt--;
                        }
                        if (M[i] == "B")
                        {
                            de_completat = de_completat + "varianta " + M[i];
                            if (cnt == 2)
                                de_completat = de_completat + " și ";
                            if (cnt > 1 && cnt != 2)
                                de_completat = de_completat + ", ";
                            cnt--;
                        }
                        if (M[i] == "C")
                        {
                            de_completat = de_completat + "varianta " + M[i];
                            if (cnt == 2)
                                de_completat = de_completat + " și ";
                            if (cnt > 1 && cnt != 2)
                                de_completat = de_completat + ", ";
                            cnt--;
                        }
                        if (M[i] == "D")
                        {
                            de_completat = de_completat + "varianta " + M[i];
                            if (cnt == 2)
                                de_completat = de_completat + " și ";
                            if (cnt > 1 && cnt != 2)
                                de_completat = de_completat + ", ";
                            cnt--;
                        }
                        if (M[i] == "corect")
                        {
                            de_completat = de_completat + "răspunsul " + M[i];
                            if (cnt == 2)
                                de_completat = de_completat + " și ";
                            if (cnt > 1 && cnt != 2)
                                de_completat = de_completat + ", ";
                            cnt--;
                        }
                        if (M[i] == "nivelul")
                        {
                            de_completat = de_completat + M[i] + " de dificultate";
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
            else MessageBox.Show("Alegeți o întrebare!");
        }

        private void listBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex != -1)
            {
                string[] S = listBox1.SelectedItem.ToString().Split();
                int id = int.Parse(S[0]);
                string q = "SELECT * FROM intrebari WHERE ID=" + id;
                OleDbCommand c = new OleDbCommand(q, conn);
                OleDbDataReader dr = c.ExecuteReader();
                while (dr.Read())
                {
                    comboBox1.Text = dr[1].ToString() + " ) " + get_opera_by_id(int.Parse(dr[1].ToString()));
                    textBox1.Text = dr[2].ToString();
                    textBox2.Text = dr[3].ToString();
                    textBox3.Text = dr[4].ToString();
                    textBox4.Text = dr[5].ToString();
                    textBox5.Text = dr[6].ToString();
                    string corect = dr[7].ToString();
                    string nivel = dr[8].ToString();
                    if (corect.IndexOf('A') != -1) checkBox1.Checked = true;
                    else checkBox1.Checked = false;
                    if (corect.IndexOf('B') != -1) checkBox2.Checked = true;
                    else checkBox2.Checked = false;
                    if (corect.IndexOf('C') != -1) checkBox3.Checked = true;
                    else checkBox3.Checked = false;
                    if (corect.IndexOf('D') != -1) checkBox4.Checked = true;
                    else checkBox4.Checked = false;
                    if (nivel == "usor")
                        radioButton2.Checked = true;
                    if (nivel == "mediu")
                        radioButton3.Checked = true;
                    if (nivel == "dificil")
                        radioButton7.Checked = true;
                }

            }
        }
    }
}
