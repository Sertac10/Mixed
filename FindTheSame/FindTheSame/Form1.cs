using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FindTheSame
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        PictureBox[,] kartlar = new PictureBox[4, 4];
        
        int[] siralisayilar = new int[16];
        
        PictureBox ilkTiklanan = null;
        
        int sayac = 0;

        private void Form1_Load(object sender, EventArgs e)
        {
            KartYukle();
            OyunaHazirla();
        }

        private void OyunaHazirla()
        {
            Random rnd = new Random();
            int[] sayiHavuzu = new int[] { 1, 2, 3, 4, 5, 6, 7, 8 };
            int indis;
            Array.Clear(siralisayilar, 0, siralisayilar.Length);
            
            for (int i = 0; i < siralisayilar.Length; i++)
            {
                indis = rnd.Next(sayiHavuzu.Length);
                if (siralisayilar.Contains(sayiHavuzu[indis]))
                {
                    siralisayilar[i] = sayiHavuzu[indis];
                    sayiHavuzu[indis] = sayiHavuzu[sayiHavuzu.Length - 1];
                    Array.Resize(ref sayiHavuzu, sayiHavuzu.Length - 1);
                }
                else
                {
                    siralisayilar[i] = sayiHavuzu[indis];
                }
            }
            
            indis = 0;
            for (int i = 0; i < kartlar.GetLength(0); i++)
            {
                for (int j = 0; j < kartlar.GetLength(1); j++)
                {
                    kartlar[i, j].Image = Image.FromFile($"fruit/{siralisayilar[indis]}.png");
                    kartlar[i, j].Tag = siralisayilar[indis++];
                }
            }
           
            this.Refresh();
            System.Threading.Thread.Sleep(1000);
            for (int i = 0; i < kartlar.GetLength(0); i++)
            {
                for (int j = 0; j < kartlar.GetLength(1); j++)
                {
                    kartlar[i, j].Image = Image.FromFile("fruit/que.png");
                }
            }

        }

        private void KartYukle()
        {
            PictureBox yeni;

            for (int i = 0; i < kartlar.GetLength(0); i++)
            {
                for (int j = 0; j < kartlar.GetLength(1); j++)
                {
                    yeni = new PictureBox();
                    yeni.Name = "pbx" + i + j;
                    yeni.Size = new Size(100, 100);
                    yeni.Location = new Point(100 * j + 5, 100 * i + 5);
                    yeni.Image = Image.FromFile("fruit/que.png");
                    yeni.SizeMode = PictureBoxSizeMode.StretchImage;
                    yeni.Click += Yeni_Click;

                    kartlar[i, j] = yeni;
                    panel1.Controls.Add(yeni);
                }
            }
        }
       
        private void Yeni_Click(object sender, EventArgs e)
        {
            PictureBox tiklanan = sender as PictureBox;
            tiklanan.Image = Image.FromFile($"fruit/{tiklanan.Tag.ToString()}.png");
            if (ilkTiklanan == null)
            {
                ilkTiklanan = tiklanan;
                return;
            }
            else if (ilkTiklanan.Tag.ToString() != tiklanan.Tag.ToString())
            {
                this.Refresh();
                System.Threading.Thread.Sleep(1000);
                ilkTiklanan.Image = tiklanan.Image = Image.FromFile("fruit/que.png");
            }
            else
            {
                sayac++;
                if (sayac == 8) 
                {
                    MessageBox.Show("TEBRİKLER");
                    OyunaHazirla();
                }
            }
            ilkTiklanan = null;
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OyunaHazirla();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
