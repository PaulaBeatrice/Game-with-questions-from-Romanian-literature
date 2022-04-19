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
    public partial class Form1 : Form
    {
        OleDbConnection conn;
        int idu;
        WindowsMediaPlayer muzica = new WindowsMediaPlayer();
        public Form1()
        {
            InitializeComponent();
            muzica.URL = "start.mp3";

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            muzica.controls.stop();
            timer1.Stop();
            if (c == 79)
                c = 0;
            intrebari f1 = new intrebari();
            f1.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            muzica.controls.stop();
            timer1.Stop();
            if (c == 79)
                c = 0;
            teste t1 = new teste();
            t1.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            muzica.controls.stop();
            if (c == 79)
                c = 0;
            timer1.Stop();
            Joc n = new Joc();
            n.idu = idu;
            n.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            muzica.controls.stop();
            timer1.Stop();
            if (c == 79)
                c = 0;
            teorie t = new teorie();
            t.ShowDialog();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            muzica.controls.play();
            timer1.Start();
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            string cs = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source=grila.accdb";
            conn = new OleDbConnection(cs);
            conn.Open();
            panel2.Visible = false;
            panel3.Visible = false;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            button5_Click(sender, e);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            panel1.Visible = true;
            panel2.Visible = false;
            panel3.Visible = false;
            textBox1.Text = "";
            textBox2.Text = "";
            this.Text = "Teste grila";
        }

        private void button8_Click(object sender, EventArgs e)
        {
            Rezultate r = new Rezultate();
            r.idu = 0;
            r.ShowDialog();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            Rezultate r = new Rezultate();
            r.idu = idu;
            r.ShowDialog();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            string u = textBox1.Text;
            string p = textBox2.Text;
            string q = "SELECT * FROM Utilizatori WHERE Utilizator='" + u + "' AND Parola='" + p + "'";
            OleDbCommand c = new OleDbCommand(q, conn);
            OleDbDataReader dr = c.ExecuteReader();
            if (dr.Read())
            {
                string np = dr[2].ToString();
                string tip = dr[4].ToString();
                if (tip == "admin")
                {

                    this.Text = "Bine ai venit " + np + "!";
                    panel2.Visible = true;
                    panel1.Visible = false;
                    idu = int.Parse(dr[0].ToString());
                    panel2.Location = new Point(580, 400);
                }
                else
                {

                    this.Text = "Bine ai venit " + np + "!";
                    panel3.Visible = true;
                    panel1.Visible = false;
                    idu = int.Parse(dr[0].ToString());
                    panel3.Location = new Point(580, 400);
                }
            }
            else
                MessageBox.Show("Date de logare greșite!");
        }
        private void button10_Click(object sender, EventArgs e)
        {
            Creeare_cont_elev c = new Creeare_cont_elev();
            c.ShowDialog();
        }

        private void label3_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
            {
                idu = 0;
                string u = textBox1.Text;
                string q = "SELECT * FROM Utilizatori WHERE Utilizator='" + u + "'";
                OleDbCommand c = new OleDbCommand(q, conn);
                OleDbDataReader dr = c.ExecuteReader();
                if (dr.Read())
                {
                        idu = int.Parse(dr[0].ToString());
                }
                if(idu != 0)
                {
                    Recuperare_parola r = new Recuperare_parola();
                    r.idu = idu;
                    r.ShowDialog();
                }
               else
                {
                    MessageBox.Show("Numele de utilizator introdus nu există!");
                }
            }
            else
                MessageBox.Show("Introduceți numele de utilizator!");
        }
        int c = 0;
        private void timer1_Tick(object sender, EventArgs e)
        {
            c++;
            if(c == 80)
            {
                muzica.controls.stop();
                muzica.controls.play();
                c = 0;
            }
        }

        private void button14_Click(object sender, EventArgs e)
        {
            muzica.controls.stop();
            button14.Visible = false;
            button15.Visible = true;
            timer1.Stop();
            muzica.controls.stop();
        }

        private void button15_Click(object sender, EventArgs e)
        {
            muzica.controls.play();
            button14.Visible = true;
            button15.Visible = false;
            timer1.Start();
            muzica.controls.play();
            c = 0;
        }
    }
}
