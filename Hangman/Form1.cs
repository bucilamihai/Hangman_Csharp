using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hangman
{
    public partial class Form1 : Form
    {
        Button[,] litere = new Button[3, 14];
        PictureBox jucator = new PictureBox();
        Label cuvant = new Label();
        string cuvant_gasit;
        int i, j, count;

        private void restart()
        {
            this.Controls.Clear();
            this.InitializeComponent();
            Form1Load();
        }

        private void litere_load()
        {
            for (int i = 1; i <= 2; i++)
            {
                for (int j = 1; j <= 13; j++)
                {
                    litere[i, j] = new Button();
                    litere[i, j].Width = 50;
                    litere[i, j].Height = 50;
                    litere[i, j].Location = new Point(10 + 50 * (j - 1), 250 + 50 * (i - 1));
                    litere[i, j].Text = Convert.ToString(Convert.ToChar('A' + j - 1 + (i - 1) * 13));
                    litere[i, j].Font = new Font("Microsoft Sans Serif", 16);
                    this.Controls.Add(litere[i, j]);
                }
            }
        }

        private void jucator_load()
        {
            jucator.Width = 200;
            jucator.Height = 250;
            jucator.Location = new Point(400, 25);
            jucator.Image = Image.FromFile("1.png");
            this.Controls.Add(jucator);
        }

        private void cuvant_load()
        {
            cuvant.Text = "";
            cuvant.Width = 200;
            cuvant.Height = 50;
            cuvant.Location = new Point(75, 100);
            cuvant.Font = new Font("Microsoft Sans Serif", 20);
            cuvant.TextAlign = ContentAlignment.MiddleCenter;
            this.Controls.Add(cuvant);
        }

        private void cauta_cuvant()
        {
            string[] cuvinte = System.IO.File.ReadAllLines("cuvinte.txt");
            Random rnd = new Random();
            int x = rnd.Next(0, 49);
            cuvant_gasit = cuvinte[x];
            for (int i = 1; i <= cuvant_gasit.Length; i++)
                cuvant.Text += "_ ";
        }
        private void litere_click(object sender, EventArgs e)
        {
            Button litera_btn = (Button)sender;
            if (cuvant_gasit.Contains(litera_btn.Text)) // verificam daca litera se gaseste in cuvant
            {
                string cuvant_copy = cuvant.Text;
                cuvant.Text = "";
                foreach (char lit in cuvant_gasit) // actualizam textul din label, in functie de litere
                {       
                    if (Convert.ToString(lit) == litera_btn.Text) // litera gasita acum
                        cuvant.Text += litera_btn.Text + " ";
                    else
                    {
                        if (cuvant_copy.Contains(lit)) // litere gasite anterior
                            cuvant.Text += lit + " ";
                        else                           // litere care nu au fost gasite
                            cuvant.Text += "_ "; 
                    }  
                }
                if(!cuvant.Text.Contains('_'))
                {
                    MessageBox.Show("Felicitari, ai castigat!");
                    restart();
                }   
            }
            else
            {
                count++;
                jucator.Image = Image.FromFile(count + ".png");
                if (count == 11)
                {
                    MessageBox.Show("Ai pierdut!" + '\n' + "Cuvantul era " + cuvant_gasit);
                    restart();
                }
            }
            litera_btn.Enabled = false;
            cuvant.Focus();
        }


        public Form1()
        {
            InitializeComponent();
            Form1Load();
        }

        private void Form1Load()
        {
            cuvant_load();
            litere_load();
            jucator_load();
            cauta_cuvant();
            cuvant.Focus();
            count = 1;
            for (i = 1; i <= 2; i++)
                for (j = 1; j <= 13; j++)
                    litere[i, j].Click += new EventHandler(litere_click);
        }
    }
}
