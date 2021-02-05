using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PoleTrojkata
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        #region Variables

        /// <summary>
        /// Zmienne używane w obliczaniu pola trójkąta
        /// </summary>
        double a = 0;
        double b = 0;
        double c = 0;
        double height1 = 0;
        double height2 = 0;
        double height3 = 0;
        double r = 0;
        double R = 0;
        double sinA = 0;
        double sinB = 0;
        double sinC = 0;
        double Alpha = 0;
        double Beta = 0;
        double Gamma = 0;
        double Alpha1 = 0;
        double Beta1 = 0;
        double Gamma1 = 0;
        int indeks = 0;

        #endregion

        #region Angle Checker
        /// <summary>
        /// Sprawdza czy kąty są podane w radianach czy w stopniach
        /// </summary>
        string miaraKata = "";
        private void stopnie_CheckedChanged(object sender, EventArgs e)
        {
            miaraKata = "stopnie";
        }

        private void radiany_CheckedChanged(object sender, EventArgs e)
        {
            miaraKata = "radiany";
        }
        #endregion

        #region Buttons

        /// <summary>
        /// Oblicza pole trójkąta sprawdzając poprawność danych
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ObliczBtn_Click(object sender, EventArgs e)
        {
            PoleTrojkata.Clear();

            // liczba wzorów: 13
            // tab1 - W tej tablicy przechowywana jest ilość danych podanych przez użytkownika dla każdego wzoru
            int[] tab1 = new int[13];

            // tab2 - W tej tablicy przechowywana jest ilość danych potrzebna dla każdego wzoru
            int[] tab2 = { 2, 2, 2, 3, 3, 3, 4, 4, 4, 4, 4, 4, 3 };

            // tab3 - W tej tablicy przechowywana jest ilość danych brakujących dla każdego wzoru
            int[] tab3 = { 2, 2, 2, 3, 3, 3, 4, 4, 4, 4, 4, 4, 3 };
            
            // tab4 - W tej tablicy znajduje się informacja które wzory można wykorzystać do obliczenia pola trójkąta
            int[] tab4 = new int[13];

            for (int i = 0; i < 13; i++)
            {
                tab4[i] = -1;
            }
            for (int i = 0; i < 13; i++)
            {
                tab1[i] = 0;
            }

            // Sprawdzanie ile danych zostało podanych w każdym ze wzorów
            if (this.bokA.Text != "")
            {
                try
                {
                    a = Convert.ToDouble(this.bokA.Text);
                    if (a <= 0)
                        MessageBox.Show("Podałeś niewłaściwą długość boku. Podaj 'bok a' o długości dodatniej.");
                    else
                    {
                        tab1[0]++;
                        tab1[3]++;
                        tab1[4]++;
                        tab1[6]++;
                        tab1[9]++;
                        tab1[11]++;
                        tab1[12]++;
                    }
                }
                catch (Exception exc)
                {
                    MessageBox.Show($"Błąd: {exc.Message} Podaj długość boku A w postaci liczby rzeczywistej dodatniej");
                }
            }            
            if (this.bokB.Text != "")
            {
                try
                {
                    b = Convert.ToDouble(this.bokB.Text);
                    if (b <= 0)
                        MessageBox.Show("Podałeś niewłaściwą długość boku. Podaj 'bok b' o długości dodatniej.");
                    else
                    {
                        tab1[1]++;
                        tab1[3]++;
                        tab1[5]++;
                        tab1[7]++;
                        tab1[9]++;
                        tab1[11]++;
                        tab1[12]++;
                    }
                }
                catch (Exception exc)
                {
                    MessageBox.Show($"Błąd: {exc.Message} Podaj długość boku B w postaci liczby rzeczywistej dodatniej");
                }
            }
            if (this.bokC.Text != "")
            {
                try
                {
                    c = Convert.ToDouble(this.bokC.Text);
                    if (c <= 0)
                        MessageBox.Show("Podałeś niewłaściwą długość boku. Podaj 'bok c' o długości dodatniej.");
                    else
                    {
                        tab1[2]++;
                        tab1[4]++;
                        tab1[5]++;
                        tab1[8]++;
                        tab1[11]++;
                        tab1[12]++;
                    }
                }
                catch (Exception exc)
                {
                    MessageBox.Show($"Błąd: {exc.Message} Podaj długość boku C w postaci liczby rzeczywistej dodatniej");
                }
            }
            if (this.alpha.Text != "")
            {
                try
                {
                    Alpha = Convert.ToDouble(this.alpha.Text);
                    if (miaraKata == "")
                        MessageBox.Show("Nie zaznaczyłeś w czym są podane kąty");
                    else if ((miaraKata == "stopnie" && Alpha < 180 && Alpha > 0) || (miaraKata == "radiany" && Alpha < 180*Math.PI && Alpha > 0))
                    {
                        if (miaraKata == "radiany")
                            sinA = Math.Sin(Alpha);
                        else if (miaraKata == "stopnie")
                        {
                            Alpha1 = Alpha;
                            Alpha /= 180;
                            Alpha *= Math.PI;
                            sinA = Math.Sin(Alpha);
                        }
                        tab1[5]++;
                        tab1[6]++;
                        tab1[7]++;
                        tab1[8]++;
                        tab1[10]++;
                    }
                    else
                        MessageBox.Show("Podałeś niewłaściwy kąt alpha.");
                }
                catch (Exception exc)
                {
                    MessageBox.Show($"Błąd: {exc.Message} Podaj miarę kąta alpha w postaci liczby rzeczywistej dodatniej");
                }
            }
            if (this.beta.Text != "")
            {
                try
                {
                    Beta = Convert.ToDouble(this.beta.Text);
                    if (miaraKata == "")
                        MessageBox.Show("Nie zaznaczyłeś w czym są podane kąty");
                    else if((miaraKata == "stopnie" && Beta < 180 && Beta > 0) || (miaraKata == "radiany" && Beta < 180 * Math.PI && Beta > 0))
                    {
                        if (miaraKata == "radiany")
                            sinB = Math.Sin(Beta);
                        else if (miaraKata == "stopnie")
                        {
                            Beta1 = Beta;
                            Beta /= 180;
                            Beta *= Math.PI;
                            sinB = Math.Sin(Beta);
                        }
                        tab1[4]++;
                        tab1[6]++;
                        tab1[7]++;
                        tab1[8]++;
                        tab1[10]++;
                    }
                    else
                        MessageBox.Show("Podałeś niewłaściwy kąt beta.");
                }
                catch (Exception exc)
                {
                    MessageBox.Show($"Błąd: {exc.Message} Podaj miarę kąta beta w postaci liczby rzeczywistej dodatniej");
                }
            }
            if (this.gamma.Text != "")
            {
                try
                {
                    Gamma = Convert.ToDouble(this.gamma.Text);
                    if (miaraKata == "")
                        MessageBox.Show("Nie zaznaczyłeś w czym są podane kąty");
                    else if((miaraKata == "stopnie" && Gamma < 180 && Gamma > 0) || (miaraKata == "radiany" && Gamma < 180 * Math.PI && Gamma > 0))
                    {
                        if (miaraKata == "radiany")
                            sinC = Math.Sin(Gamma);
                        else if (miaraKata == "stopnie")
                        {
                            Gamma1 = Gamma;
                            Gamma /= 180;
                            Gamma *= Math.PI;
                            sinC = Math.Sin(Gamma);
                        }
                        tab1[3]++;
                        tab1[6]++;
                        tab1[7]++;
                        tab1[8]++;
                        tab1[10]++;
                    }
                    else
                        MessageBox.Show("Podałeś niewłaściwy kąt gamma.");
                }
                catch (Exception exc)
                {
                    MessageBox.Show($"Błąd: {exc.Message} Podaj miarę kąta gamma w postaci liczby rzeczywistej dodatniej");
                }
            }
            if (this.wysokoscA.Text != "")
            {
                try
                {
                    height1 = Convert.ToDouble(this.wysokoscA.Text);
                    if (height1 <= 0)
                        MessageBox.Show("Podałeś niewłaściwą wyoskość. Podaj 'wysokość a' o długości dodatniej.");
                    else
                        tab1[0]++;
                }
                catch (Exception exc)
                {
                    MessageBox.Show($"Błąd: {exc.Message} Podaj wysokośćA w postaci liczby rzeczywistej dodatniej");
                }
            }
            if (this.wysokoscB.Text != "")
            {
                try
                {
                    height2 = Convert.ToDouble(this.wysokoscB.Text);
                    if (height2 <= 0)
                        MessageBox.Show("Podałeś niewłaściwą wyoskość. Podaj 'wysokość b' o długości dodatniej.");
                    else
                        tab1[1]++;
                }
                catch (Exception exc)
                {
                    MessageBox.Show($"Błąd: {exc.Message} Podaj wysokośćB w postaci liczby rzeczywistej dodatniej");
                }
            }
            if (this.wysokoscC.Text != "")
            {
                try
                {
                    height3 = Convert.ToDouble(this.wysokoscC.Text);
                    if (height3 <= 0)
                        MessageBox.Show("Podałeś niewłaściwą wyoskość. Podaj 'wysokość c' o długości dodatniej.");
                    else
                        tab1[2]++;
                }
                catch (Exception exc)
                {
                    MessageBox.Show($"Błąd: {exc.Message} Podaj wysokośćC w postaci liczby rzeczywistej dodatniej");
                }
            }
            if (this.promien_r.Text != "")
            {
                try
                {
                    r = Convert.ToDouble(this.promien_r.Text);
                    if (r <= 0)
                        MessageBox.Show("Podałeś niewłaściwą długość promienia. Podaj 'promień r' o długości dodatniej.");
                    else
                        tab1[11]++;
                }
                catch (Exception exc)
                {
                    MessageBox.Show($"Błąd: {exc.Message} Podaj długość promienia r w postaci liczby rzeczywistej dodatniej");
                }
            }
            if (this.promienR.Text != "")
            {
                try
                {
                    R = Convert.ToDouble(this.promienR.Text);
                    if (R <= 0)
                        MessageBox.Show("Podałeś niewłaściwą długość promienia. Podaj 'promień R' o długości dodatniej.");
                    else
                    {
                        tab1[10]++;
                        tab1[11]++;
                    }
                }
                catch (Exception exc)
                {
                    MessageBox.Show($"Błąd: {exc.Message} Podaj długość promienia R w postaci liczby rzeczywistej dodatniej");
                }
            }

            

            // Sprawdzanie w którym wzorze nie brakuje żadnych danych i dodanie go do tab4
            for (int i = 0; i < 13; i++)
            {
                tab3[i] = tab2[i] - tab1[i];
                if (tab3[i] == 0)
                {
                    tab4[i] = i;
                }    
                    
                else if (tab3[i] < 0)
                    MessageBox.Show("Error 000");
            }

            // Sprawdza czy suma kątów w trójkącie jest równa 180 stopni (pi rad)
            if (((Alpha1 + Beta1 + Gamma1 != 180 && miaraKata == "stopnie") || (Math.Round(Alpha + Beta + Gamma, 4) != Math.Round(Math.PI, 4) && miaraKata == "radiany"))
                && (tab3[10] == 0 || tab3[6] == 0 || tab3[7] == 0 || tab3[8] == 0))
            {
                MessageBox.Show("Suma kątów nie równa się 180 stopni (ok. 2pi rad). Podaj prawidłowe miary kątów.");
                return;
            }

            // Sprawdza czy a + b > c
            if ((a + b <= c || b + c <= a || a + c <= b) && (tab3[9] == 0 || tab3[11] == 0 || tab3[12] == 0))
            {
                MessageBox.Show("Pamiętaj że w trójkącie dwa krótsze boki muszą być dłuższe od najdłuższego. Wprowadź poprawne dane.");
                return;
            }

            // Jeśli wprowadzone dane są spójne to oblicza pole trójkąt; w przeciwnym razie zwraca błąd
            if (CzySpojne(tab4) && !CzyPusta(tab4))
            {
                this.PoleTrojkata.Text = Oblicz(indeks).ToString();
            }
            else if (!CzyPusta(tab4))
                MessageBox.Show("Niespójne dane. Wprowadź poprawne dane.");
            
            // Wyświetla komunikat jeśli użytkownik nie podał wystarczającej ilości danych
            if (CzyPusta(tab4))
                MessageBox.Show("Nie podałeś wystarczającej ilości danych.");

        }

        /// <summary>
        /// Czyści wszystkie wpisane dane
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CzyscBtn_Click(object sender, EventArgs e)
        {
            this.bokA.Clear();
            this.bokB.Clear();
            this.bokC.Clear();
            this.alpha.Clear();
            this.beta.Clear();
            this.gamma.Clear();
            this.wysokoscA.Clear();
            this.wysokoscB.Clear();
            this.wysokoscC.Clear();
            this.promienR.Clear();
            this.promien_r.Clear();
            this.PoleTrojkata.Clear();
        }

        /// <summary>
        /// Zamyka program
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ZamknijBtn_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        #endregion

        #region ToolStrip

        /// <summary>
        /// Pokazuje informacje o autorze
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void autorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            form2.Show();
        }

        private void wzoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }
        /// <summary>
        /// Pokazuje wzory na pole trójkąta
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void wzoryToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Form3 form3 = new Form3();
            form3.Show();
        }

        #endregion

        #region Private helpers

        /// <summary>
        /// Sprawdza czy wprowadzone wartości są spójne
        /// </summary>
        /// <param name="txt"></param>
        private bool CzySpojne(int[] tablica)
        {
            if (CzyPusta(tablica))
                return true;      
            
            for (int i = 0; i < 13; i++)
            {
                if (tablica[i] != -1)
                    indeks = i;
            }
            for (int i = 0; i < 13; i++)
            {
                if(tablica[i] != -1)
                {
                    if (Oblicz(tablica[indeks]) != Oblicz(tablica[i]))
                        return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Sprawdza czy tablica jest pusta (tzn. czy którakolwiek z jej wartości różni się od -1)
        /// </summary>
        /// <param name="tablica"></param>
        /// <returns></returns>
        private bool CzyPusta(int[] tablica)
        {
            for (int i = 0; i < 13; i++)
            {
                if (tablica[i] != -1)
                    return false;
            }
            return true;
        }

        /// <summary>
        /// Oblicza pole trójkąta korzystając ze wzorów maturalnych
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        private double Oblicz(int x)
        {
            double Area = 0;
            // p - połowa obwodu trójkąta
            double p = (a + b + c) / 2;

            switch (x)
            {
                case 0:
                    Area = 0.5 * a * height1;
                    break;
                case 1:
                    Area = 0.5 * b * height2;
                    break;
                case 2:
                    Area = 0.5 * c * height3;
                    break;
                case 3:
                    Area = 0.5 * a * b * sinC;
                    break;
                case 4:
                    Area = 0.5 * a * c * sinB;
                    break;
                case 5:
                    Area = 0.5 * b * c * sinA;
                    break;
                case 6:
                    Area = 0.5 * a * a * sinB * sinC / sinA;
                    break;
                case 7:
                    Area = 0.5 * b * b * sinA * sinC / sinB;
                    break;
                case 8:
                    Area = 0.5 * c * c * sinA * sinB / sinC;
                    break;
                case 9:
                    Area = 0.25 * a * b * c / R;
                    break;
                case 10:
                    Area = 2 * R * R * sinA * sinB * sinC;
                    break;
                case 11:
                    /*if (a + b <= c)
                    {
                        MessageBox.Show("Błąd: Pamiętaj o tym, że a + b > c");
                        return 0;
                    }*/
                    Area = r * p;
                    break;
                case 12:
                    /*if (a + b <= c)
                    {
                        MessageBox.Show("Błąd: Pamiętaj o tym, że a + b > c");
                        return 0;
                    }*/
                    Area = Math.Sqrt(p * (p - a) * (p - b) * (p - c));
                    break;
                default:
                    MessageBox.Show("Nie działa funkcja Oblicz");
                    break;
            }

            return Area;
        }

        #endregion

        
    }
}
