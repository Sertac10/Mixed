using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SansOyunu
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        Random rnd;

        int[] sansliSayilar;
        Label[] labels;

        int sayac = 1, frekans = 10, sira = 0;

        private void btnOyna_Click(object sender, EventArgs e)
        {
            Sifirla();
            sayiUret();
            timer1.Start();
        }

        private void Sifirla()
        {
            sira = 0;
            lbxSayilar.Items.Clear();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (sayac++ % frekans == 0)
            {
                if (sira < labels.Length)
                {
                    lbxSayilar.Items.Add(sansliSayilar[sira].ToString().PadLeft(2, '0'));
                    labels[sira].Text = sansliSayilar[sira].ToString().PadLeft(2, '0');
                }
                sira++;
            }

            //yalandan dönen sayılar..
            for (int i = sira; i < labels.Length; i++)
            {
                labels[i].Text = rnd.Next(1, 50).ToString().PadLeft(2, '0');
            }
            System.Threading.Thread.Sleep(10);
            if (sira > labels.Length)
            {
                for (int i = 0; i < labels.Length; i++)
                {
                    labels[i].Visible = !labels[i].Visible;
                }
            }

            if (sira == labels.Length + 3)
            {
                timer1.Stop();
                sansliSirala();
                for (int i = 0; i < labels.Length; i++)
                {
                    labels[i].Visible = true;
                    labels[i].Text = sansliSayilar[i].ToString().PadLeft(2, '0');
                }
            }
        }

        private void sansliSirala()
        {
            int temp;

            for (int i = 0; i < sansliSayilar.Length - 1; i++)
            {
                for (int j = i + 1; j < sansliSayilar.Length; j++)
                {
                    if (sansliSayilar[i] > sansliSayilar[j])
                    {
                        temp = sansliSayilar[i];
                        sansliSayilar[i] = sansliSayilar[j];
                        sansliSayilar[j] = temp;
                    }
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            labels = new Label[] { lbl1, lbl2, lbl3, lbl4, lbl5, lbl6 };
        }

        private void sayiUret()
        {
            sansliSayilar = new int[6];
            int sansli;
            rnd = new Random();
            for (int i = 0; i < sansliSayilar.Length; i++)
            {
                do
                {
                    sansli = rnd.Next(1, 50);
                } while (sansliSayilar.Contains(sansli));
                sansliSayilar[i] = sansli;
            }
        }
    
    }
}
