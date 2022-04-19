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
    public partial class adaugare_intrebare : UserControl
    {
        public adaugare_intrebare()
        {
            InitializeComponent();
        }

        OleDbConnection conn;
        List<int> LID;
        private void button1_Click(object sender, EventArgs e)
        {
            if(comboBox1.SelectedIndex != - 1)
            {
                int id_op = LID[comboBox1.SelectedIndex];
                string enunt = textBox1.Text;
                string A = textBox2.Text;
                string B = textBox3.Text;
                string C = textBox4.Text;
                string D = textBox5.Text;
                string corect = "";
                if (checkBox1.Checked)
                    corect = corect + "A";
                if (checkBox2.Checked)
                    corect = corect + "B";
                if (checkBox3 .Checked)
                    corect = corect + "C";
                if (checkBox4.Checked)
                    corect = corect + "D";
                string nivel = "";
                if (radioButton2.Checked)
                    nivel = "usor";
                if (radioButton3.Checked)
                    nivel = "mediu";
                if (radioButton7.Checked)
                    nivel = "dificil";
                if (enunt != "" && A != "" && B != "" && C != "" && D != "" && corect != "" && nivel != "")
                {
                    string q = "INSERT INTO Intrebari (Id_opera, Enunt, A, B, C, D, Corect, Dificultate) VALUES ";
                    q = q + "(" + id_op + ", '" + enunt + "', '" + A + "', '" + B + "', '" + C + "', '" + D + "', '" + corect + "', '" + nivel + "')";
                    OleDbCommand c = new OleDbCommand(q, conn);
                    c.ExecuteNonQuery();
                    MessageBox.Show("Întrebarea a fost salvată!");
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
                    comboBox1.SelectedIndex = -1;
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
                    string[] S = incomplet.Split();
                    for (int i = 0; i < S.Length; i++)
                    {
                        if (S[i] == "enunțul")
                        {
                            de_completat = de_completat + S[i] + " problemei";
                            if (cnt == 2)
                                de_completat = de_completat + " și ";
                            if (cnt > 1 && cnt != 2)
                                de_completat = de_completat + ", ";
                            cnt--;
                        }
                        if (S[i] == "A")
                        {
                            de_completat = de_completat + "varianta " + S[i];
                            if (cnt == 2)
                                de_completat = de_completat + " și ";
                            if (cnt > 1 && cnt != 2)
                                de_completat = de_completat + ", ";
                            cnt--;
                        }
                        if (S[i] == "B")
                        {
                            de_completat = de_completat + "varianta " + S[i];
                            if (cnt == 2)
                                de_completat = de_completat + " și ";
                            if (cnt > 1 && cnt != 2)
                                de_completat = de_completat + ", ";
                            cnt--;
                        }
                        if (S[i] == "C")
                        {
                            de_completat = de_completat + "varianta " + S[i];
                            if (cnt == 2)
                                de_completat = de_completat + " și ";
                            if (cnt > 1 && cnt != 2)
                                de_completat = de_completat + ", ";
                            cnt--;
                        }
                        if (S[i] == "D")
                        {
                            de_completat = de_completat + "varianta " + S[i];
                            if (cnt == 2)
                                de_completat = de_completat + " și ";
                            if (cnt > 1 && cnt != 2)
                                de_completat = de_completat + ", ";
                            cnt--;
                        }
                        if (S[i] == "corect")
                        {
                            de_completat = de_completat + "răspunsul " + S[i];
                            if (cnt == 2)
                                de_completat = de_completat + " și ";
                            if (cnt > 1 && cnt != 2)
                                de_completat = de_completat + ", ";
                            cnt--;
                        }
                        if (S[i] == "nivelul")
                        {
                            de_completat = de_completat + S[i] + " de dificultate";
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
            else MessageBox.Show("Selectați un capitol!");
        }

        private void adaugare_intrebare_Load(object sender, EventArgs e)
        {
            string cs = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source=grila.accdb";
            conn = new OleDbConnection(cs);
            conn.Open();
            LID = new List<int>();
            string q = "SELECT * FROM Categorie ORDER BY Opera";
            OleDbCommand c = new OleDbCommand(q, conn);
            OleDbDataReader dr = c.ExecuteReader();
            while(dr.Read())
            {
                comboBox1.Items.Add(dr[1].ToString() + " - " + dr[2].ToString());
                LID.Add(int.Parse(dr[0].ToString()));
            }
            dr.Close();
        }
    }
}
