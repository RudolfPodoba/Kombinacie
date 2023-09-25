using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Kombinacie
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        
        private int vypis_kombinacie(int i_k , int[] i_kombinacia, int i_pocet_kombinacii)
        {
            string s_kombinacia = "{";
            int i_vysledok = i_pocet_kombinacii;

            // vypísanie kombinácie
            for (int i = 0; i < i_k; i++)
            {
                s_kombinacia += i_kombinacia[i].ToString();
                if (i < (i_k - 1))
                {
                    s_kombinacia += ", ";
                }
            }

            s_kombinacia += "}";

            richTextBox1.AppendText(s_kombinacia + Environment.NewLine);
            i_vysledok++;

            return i_vysledok;
        }
        
        private int faktorial(int i_cislo)
        {
            int i_vysledok = i_cislo;

            for (int i = i_vysledok - 1; i > 0; i--)
            {
                i_vysledok *= i;
            }

            return i_vysledok;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // na námet, resp. inšpirácia z https://stackoverflow.com/questions/8316479/combination-without-repetition-of-n-elements-without-use-for-to-do
            // výsledok je možné overiť na https://www.omnicalculator.com/statistics/combinations-without-repetition
            // definovanie premenných a načítanie
            int i_n = 0;
            int i_k = 0;
            int i_i = 1;
            int i_j = 1;
            int i_pocet_kombinacii = 0;
            int i_pocet_kombinacii_vypocet = 0;
            
            // načítanie zadaných hodnôt
            Int32.TryParse(textBox1.Text, out i_n);
            Int32.TryParse(textBox2.Text, out i_k);

            // nastavenie dĺžky pola pre kombinácie a jeho definovanie
            int[] komb = new int[i_k];

            // vyčistenie priestoru pre zapisovanie
            richTextBox1.Clear();

            // naplnenie pola prvou kombináciou
            for (int i = 0; i < i_k; i++)
            {
                komb[i] = i + 1;
            }
            
            // vypísanie kombinácie
            i_pocet_kombinacii = vypis_kombinacie(i_k, komb, i_pocet_kombinacii);

            if (i_k > 0 && i_k < i_n)
            {
                do
                {
                    if (komb[i_k - 1] < i_n) // ak sa posledné číslo kombinácie ešte dá zvýšiť
                    {
                        // zvýšenie posledného čísla kombinácie
                        komb[i_k - 1]++;

                        // vypísanie kombinácie
                        i_pocet_kombinacii = vypis_kombinacie(i_k, komb, i_pocet_kombinacii);
                    }
                    else // aj je posledné číslo kombinácie maximálne možné
                    {
                        i_i = 1;
                        // nájde najbližšie číslo kombinácie, ktoré sa dá ešte môže zvýšiť
                        while (komb[(i_k - 1) - i_i] == i_n - i_i)
                        {
                            i_i++;
                        }

                        // zvýši nájdené číslo kombinácie
                        komb[(i_k - 1) - i_i]++;

                        i_j = 1;
                        // vytvorí ďalši kombináciu; každé ďalšie číslo kombinácie bude o jedno vyššie, ako predchádzajúce
                        while (((i_k - 1) - i_i) + i_j <= (i_k - 1))
                        {
                            komb[((i_k - 1) - i_i) + i_j] = komb[(i_k - 1) - i_i] + i_j;
                            i_j++;
                        }
                        // vypísanie kombinácie
                        i_pocet_kombinacii = vypis_kombinacie(i_k, komb, i_pocet_kombinacii);
                    }
                } while (komb[0] <= (i_n - i_k));

                // kontrola počtu nájdených kombinácií Ck(n) = n! / (k! * (n - k)!) - https://www.hackmath.net/sk/kalkulacka/kombinacie-a-permutacie
                i_pocet_kombinacii_vypocet = faktorial(i_n) / (faktorial(i_k) * faktorial(i_n - i_k));

                if (i_pocet_kombinacii_vypocet == i_pocet_kombinacii)
                {
                    // vypíše počet nájdených kombinácií                
                    label2.Text = "Počet kombinácií bez opakovania: " + i_pocet_kombinacii.ToString();
                }
                else
                {
                    // vypíše chybu
                    label2.Text = "CHYBA PRI VÝPOČTE!!!";
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            label1.Text = "Aplikácia vypíše všetky možnosti výberu 'k' prvok " + Environment.NewLine;
            label1.Text += "z 'n' prvkovej množiny kladných celých čísel " + Environment.NewLine + "bez opakovania.";

            label2.Text = "Počet kombinácií bez opakovania: ";
        }
    }
}