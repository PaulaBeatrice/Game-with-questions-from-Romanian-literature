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
using WMPLib;


namespace Atestat_JocCulturaGenerala_Romana_
{
    public partial class Joc : Form
    {
        List<intrebare> LI;
        OleDbConnection conn;
        int poz;
        string tip;
        public int idu;
        int idt;
        int stop = 0, opreste = 0;
        private void incarca_teste()
        {
            tip = comboBox2.Text;
            string q = "SELECT * FROM teste WHERE Tip='" +  tip + "'";
            OleDbCommand c = new OleDbCommand(q, conn);
            OleDbDataReader dr = c.ExecuteReader();
            comboBox1.Items.Clear();
            while (dr.Read())
            {
                string[] S = dr[2].ToString().Split();
                comboBox1.Items.Add(dr[0].ToString() + " ) " + dr[1].ToString());
            }
            dr.Close();
        }

        private void retine_raspuns()
        {
            string r = "";
            if (checkBox1.Checked) r = r + "A";
            if (checkBox2.Checked) r = r + "B";
            if (checkBox3.Checked) r = r + "C";
            if (checkBox4.Checked) r = r + "D";
            LI[poz].Raspuns = r;
        }

        private void afisare_intrebare()
        {
            timer1.Start();
            textBox1.Text = LI[poz].Enunt;
            textBox2.Text = LI[poz].A;
            textBox3.Text = LI[poz].B;
            textBox4.Text = LI[poz].C;
            textBox5.Text = LI[poz].D;
            string corect = LI[poz].Raspuns;
            if (corect.IndexOf('A') != -1) checkBox1.Checked = true;
            else checkBox1.Checked = false;
            if (corect.IndexOf('B') != -1) checkBox2.Checked = true;
            else checkBox2.Checked = false;
            if (corect.IndexOf('C') != -1) checkBox3.Checked = true;
            else checkBox3.Checked = false;
            if (corect.IndexOf('D') != -1) checkBox4.Checked = true;
            else checkBox4.Checked = false;
            muzica.controls.stop();
            muzica.controls.play();
            if (button15.Visible == true || opreste == 1)
                muzica.controls.stop();
        }
        WindowsMediaPlayer muzica = new WindowsMediaPlayer();
        WindowsMediaPlayer muz = new WindowsMediaPlayer();
        public Joc()
        {
            InitializeComponent();
            muzica.URL = "Joc.mp3";
            muz.URL = "final.mp3";
        }

        private void Nivel_usor_Shown(object sender, EventArgs e)
        {
            string cs = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source=grila.accdb";
            conn = new OleDbConnection(cs);
            conn.Open();
            incarca_teste();
            button11.Height = 116;
            button11.Width = 425;
            button12.Height = 116;
            button12.Width = 425;
        }
        int timp = 20;
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            timp = 20;
            opreste = 0;
            if (comboBox1.SelectedIndex != -1)
            {
                string[] S = comboBox1.SelectedItem.ToString().Split();
                int id = int.Parse(S[0]);
                idt = id;
                muzica.controls.play();
                string q = "SELECT * FROM teste WHERE ID=" + id;
                OleDbCommand c = new OleDbCommand(q, conn);
                OleDbDataReader dr = c.ExecuteReader();
                LI = new List<intrebare>();
                while (dr.Read())
                {
                    string[] SI = dr[2].ToString().Split();
                    foreach (string s in SI)
                        if (s != "")
                        {
                            int id_i = int.Parse(s);
                            string qi = "SELECT * FROM intrebari WHERE ID=" + id_i;
                            OleDbCommand ci = new OleDbCommand(qi, conn);
                            OleDbDataReader dri = ci.ExecuteReader();
                            while (dri.Read())
                            {
                                int idi = int.Parse(dri[0].ToString());
                                string enunti = dri[2].ToString();
                                string ai = dri[3].ToString();
                                string bi = dri[4].ToString();
                                string Ci = dri[5].ToString();
                                string di = dri[6].ToString();
                                string corecti = dri[7].ToString();
                                string dificultatei = dri[8].ToString();
                                intrebare I = new intrebare(idi, enunti, ai, bi, Ci, di, corecti, dificultatei);
                                LI.Add(I);
                            }
                            dri.Close();

                        }
                    textBox1.Text = dr[1].ToString();
                }
                dr.Close();
                textBox1.Enabled = false;
                textBox2.Enabled = false;
                textBox3.Enabled = false;
                textBox4.Enabled = false;
                textBox5.Enabled = false;
                poz = 0;
                afisare_intrebare();
                textBox1.Visible = true;
                textBox2.Visible = true;
                textBox3.Visible = true;
                textBox4.Visible = true;
                textBox5.Visible = true;
                checkBox1.Visible = true;
                checkBox2.Visible = true;
                checkBox3.Visible = true;
                checkBox4.Visible = true;
                button1.Visible = true;
                button2.Visible = true;
                button5.Visible = true;
                button6.Visible = true;
                label2.Visible = true;
                label4.Visible = true;
                comboBox1.Enabled = false;
                comboBox2.Enabled = false;
                button13.Visible = true;
                button14.Visible = true;
                button15.Visible = true;
                button13.Enabled = true;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            retine_raspuns();
            poz--;
            if (poz < 0) poz = 0;
            afisare_intrebare();
        }
        private void button2_Click(object sender, EventArgs e)
        {
                timer1.Stop();
                timp = 20;
                retine_raspuns();
                poz++;
                int gata = 0;
                if (poz == LI.Count)
                {
                    poz--;
                    gata = 1;
                }
                if(gata == 1 && stop == 0)
                {
                    stop = 1;
                    retine_raspuns();
                    timer1.Stop();
                    int c = 0;
                    int total = 0;
                    foreach (intrebare I in LI)
                    {
                        if (I.Dificultate == "usor")
                        {
                            total = total + 100;
                            if (I.Corect == I.Raspuns) 
                                c = c + 100;
                        }
                        if (I.Dificultate == "mediu")
                        {
                            total = total + 200;
                            if (I.Corect == I.Raspuns)
                                c = c + 200;
                        }
                        if (I.Dificultate == "dificil")
                        {
                            total = total + 300;
                            if (I.Corect == I.Raspuns)
                                c = c + 300;
                        }
                    }
                opreste = 1;
                muzica.controls.stop();
                label7.Text = c.ToString();
                label3.Text = "Jocul s-a încheiat!!!Ai obținut ";
                label3.Text = label3.Text + c.ToString() + " din " + total + " puncte! Felicitări!!! Dorești să înregistrezi scorul obținut?";
                label3.Visible = true;
                label3.BringToFront();
                button9.Visible = true;
                button9.BringToFront();
                button3.Visible = true;
                button4.Visible = true;
                button3.BringToFront();
                button4.BringToFront();
                button2.Enabled = false;
                button1.Enabled = false;
                button5.Enabled = false;
                button6.Enabled = false;
                button13.Enabled = false;
                label4.Text = "";
                label2.Visible = false;
                label4.Visible = false;
                label3.Width = 324;
                label3.Height = 571;
                label6.Width = 575;
                label6.Height = 369;
                button7.Width = 92;
                button7.Height = 68;
                button8.Width = 92;
                button8.Height = 68;
                button3.Width = 83;
                button3.Height = 67;
                button4.Width = 83;
                button4.Height = 67;
                button9.Width = 295;
                button9.Height = 65;
                if (button14.Visible == true)
                    muz.controls.play();
            }
                if(opreste == 0)
                afisare_intrebare();            
        }

        private void button6_Click(object sender, EventArgs e)
        {
            timp = timp + 10;
            button6.Enabled = false;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBox1.Checked == true )
            {
                textBox2.BackColor = Color.Goldenrod;
            }
            else
                textBox2.BackColor = Color.Black;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timp--;
            if(timp <= 9)
                label4.Text = "00:0" + timp.ToString();
            else
                label4.Text = "00:" + timp.ToString();
            if(timp == 0)
            {
                timer1.Stop();
                retine_raspuns();
                timp = 20;
                poz++;
                if (poz == LI.Count)
                {
                    poz--;
                    retine_raspuns();
                    timer1.Stop();
                    int c = 0;
                    int total = 0;
                    foreach (intrebare I in LI)
                    {
                        if (I.Dificultate == "usor")
                        {
                            total = total + 100;
                            if (I.Corect == I.Raspuns)
                                c = c + 100;
                        }
                        if (I.Dificultate == "mediu")
                        {
                            total = total + 200;
                            if (I.Corect == I.Raspuns)
                                c = c + 200;
                        }
                        if (I.Dificultate == "dificil")
                        {
                            total = total + 300;
                            if (I.Corect == I.Raspuns)
                                c = c + 300;
                        }
                    }
                    opreste = 1;
                    muzica.controls.stop();
                    label7.Text = c.ToString();
                    muz.controls.play();
                    label3.Text = "Jocul s-a încheiat!!!Ai obținut ";
                    label3.Text = label3.Text + c.ToString() + " din " + total + " puncte! Felicitări!!! Dorești să înregistrezi scorul obținut?";
                    label3.Visible = true;
                    label3.BringToFront();
                    button9.Visible = true;
                    button9.BringToFront();
                    button3.Visible = true;
                    button4.Visible = true;
                    button3.BringToFront();
                    button4.BringToFront();
                    button2.Enabled = false;
                    button1.Enabled = false;
                    button13.Enabled = false;
                    button5.Enabled = false;
                    button6.Enabled = false;
                    label2.Visible = false;
                    label4.Visible = false;
                    button13.Enabled = false;
                    label3.Width = 324;
                    label3.Height = 571;
                    label6.Width = 575;
                    label6.Height = 369;
                    button7.Width = 92;
                    button7.Height = 68;
                    button8.Width = 92;
                    button8.Height = 68;
                    button3.Width = 83;
                    button3.Height = 67;
                    button4.Width = 83;
                    button4.Height = 67;
                    button9.Width = 295;
                    button9.Height = 65;
                    if (button14.Visible == true)
                        muz.controls.play();
                }
                if(opreste == 0)
                afisare_intrebare();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            string corect = LI[poz].Corect;
            int eliminat = 0;
            for(int i = 0; i < corect.Length; i++)
            {
                if(corect[i] != 'A' && eliminat == 0)
                {
                    eliminat = 1;
                    textBox2.Text = "";
                }
                if (corect[i] != 'B' && eliminat == 0)
                {
                    eliminat = 1;
                    textBox3.Text = "";
                }
                if (corect[i] != 'C' && eliminat == 0)
                {
                    eliminat = 1;
                    textBox4.Text = "";
                }
                if (corect[i] != 'D' && eliminat == 0)
                {
                    eliminat = 1;
                    textBox5.Text = "";
                }
            }
            button5.Enabled = false;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            label6.Visible = true;
            button7.Visible = true;
            button8.Visible = true;
            label6.BringToFront();
            button7.BringToFront();
            button8.BringToFront();
            button3.Visible = false;
            button4.Visible = false;
            button9.Visible = false;
            label3.Visible = false;
            label6.Text = "Dorești să începi alt joc?";
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            string q = "INSERT INTO Scor (Idu, Scor , Id_test, Data) VALUES";
            q = q + "(" + idu + ", " + int.Parse(label7.Text) + "," + idt + ",'" + DateTime.Now.ToShortDateString() + "')";
            OleDbCommand cc = new OleDbCommand(q, conn);
            cc.ExecuteNonQuery();
            label6.Visible = true;
            button7.Visible = true;
            button8.Visible = true;
            label6.BringToFront();
            button7.BringToFront();
            button8.BringToFront();
            button3.Visible = false;
            button9.Visible = false;
            button4.Visible = false;
            label3.Visible = false;
            label6.Text = "Scorul tău a fost înregistrat! \n Dorești să începi alt joc?";
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            incarca_teste();
            comboBox1.Visible = true;
            label1.Visible = true;
            comboBox1.Text = "";
            muz.controls.stop();
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked == true)
            {
                textBox3.BackColor = Color.Goldenrod;
            }
            else
                textBox3.BackColor = Color.Black;
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox3.Checked == true)
            {
                textBox4.BackColor = Color.Goldenrod;
            }
            else
                textBox4.BackColor = Color.Black;
        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox4.Checked == true)
            {
                textBox5.BackColor = Color.Goldenrod;
            }
            else
                textBox5.BackColor = Color.Black;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string corect = LI[poz].Corect;
            MessageBox.Show("Răspuns corect: " + corect);
            button1.Enabled = false;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            button14.Visible = false;
            button15.Visible = false;
            textBox1.Visible = false ;
            textBox2.Visible = false;
            textBox3.Visible = false;
            textBox4.Visible = false;
            textBox5.Visible = false;
            checkBox1.Visible = false;
            checkBox2.Visible = false;
            checkBox3.Visible = false;
            checkBox4.Visible = false;
            button1.Visible = false;
            button2.Visible = false;
            button5.Visible = false;
            button6.Visible = false;
            label2.Visible = false;
            label4.Visible = false;
            comboBox1.Visible = false;
            label1.Visible = false;
            label6.Visible = false;
            button7.Visible = false;
            button8.Visible = false;
            comboBox1.Text = "";
            comboBox2.Text = "";
            button1.Enabled = true;
            button2.Enabled = true;
            button5.Enabled = true;
            button6.Enabled = true;
            comboBox1.Enabled = true;
            comboBox2.Enabled = true;
            button9.Visible = false;
            label3.Text = "";
            button13.Visible = false;
            stop = 0;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void button9_Click(object sender, EventArgs e)
        {
            label8.Visible = true;
            label8.BringToFront();
            label8.Text = "";
            foreach (intrebare I in LI)
                if (I.Corect != I.Raspuns)
                {
                    string corect = I.Corect;
                    int lungime = corect.Length;
                    label8.Text = label8.Text + I.Enunt + " Răspuns corect: ";
                    if (corect.IndexOf('A') != -1)
                    {
                        label8.Text = label8.Text + "A)" + I.A;
                        if (lungime > 1)
                        {
                            label8.Text = label8.Text + ",";
                            lungime--;
                        }
                        label8.Text = label8.Text + " ";
                    }
                    if (corect.IndexOf('B') != -1)
                    {
                        label8.Text = label8.Text + "B)" + I.B;
                        if (lungime > 1)
                        {
                            label8.Text = label8.Text + ",";
                            lungime--;
                        }
                        label8.Text = label8.Text + " ";
                    }
                    if (corect.IndexOf('C') != -1)
                    {
                        label8.Text = label8.Text + "C)" +  I.C;
                        if (lungime > 1)
                        {
                            label8.Text = label8.Text + ",";
                            lungime--;
                        }
                        label8.Text = label8.Text + " ";
                    }
                    if (corect.IndexOf('D') != -1)
                    {
                        label8.Text = label8.Text + "D)" + I.D;
                        if (lungime > 1)
                        {
                            label8.Text = label8.Text + ",";
                            lungime--;
                        }
                        label8.Text = label8.Text + " ";
                    }
                    label8.Text = label8.Text + "\n";
                    label8.Text = label8.Text + "\n";
                }
            label8.Width = 1139;
            label8.Height = 650;
            button10.Visible = true;
            button10.BringToFront();
            button10.Width = 35;
            button10.Height = 35;
        }

        private void button10_Click(object sender, EventArgs e)
        {
            label8.Visible = false;
            button10.Visible = false;
        }
        private void button11_Click(object sender, EventArgs e)
        {
            Regulile_jocului r = new Regulile_jocului();
            r.ShowDialog();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            button11.Visible = false;
            button12.Visible = false;
            label5.Visible = true;
            comboBox2.Visible = true;
        }

        private void button13_Click(object sender, EventArgs e)
        {
            textBox1.Visible = false;
            textBox2.Visible = false;
            textBox3.Visible = false;
            textBox4.Visible = false;
            textBox5.Visible = false;
            checkBox1.Visible = false;
            checkBox2.Visible = false;
            checkBox3.Visible = false;
            checkBox4.Visible = false;
            button1.Visible = false;
            button2.Visible = false;
            button5.Visible = false;
            button6.Visible = false;
            label2.Visible = false;
            label4.Visible = false;
            comboBox1.Visible = false;
            label1.Visible = false;
            label6.Visible = false;
            button7.Visible = false;
            button8.Visible = false;
            comboBox1.Text = "";
            comboBox2.Text = "";
            button1.Enabled = true;
            button2.Enabled = true;
            button5.Enabled = true;
            button6.Enabled = true;
            comboBox1.Enabled = true;
            comboBox2.Enabled = true;
            button9.Visible = false;
            label3.Text = "";
            stop = 0;
            button13.Visible = false;
            muzica.controls.stop();
            button14.Visible = false;
            button15.Visible = false;
            opreste = 1;
        }

        private void Joc_Load(object sender, EventArgs e)
        {
            muzica.controls.stop();
            muz.controls.stop();
        }

        private void button14_Click(object sender, EventArgs e)
        {
            muzica.controls.stop();
            button14.Visible = false;
            button15.Visible = true;
            if (opreste == 1)
                muzica.controls.stop();
        }

        private void Joc_FormClosing(object sender, FormClosingEventArgs e)
        {
            muzica.controls.stop();
            muz.controls.stop();
        }

        private void button15_Click(object sender, EventArgs e)
        {
            muzica.controls.play();
            button14.Visible = true;
            button15.Visible = false;
            if (opreste == 1)
                muzica.controls.stop();
        }
    }
}
