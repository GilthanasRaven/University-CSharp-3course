using System;
using System.Windows;
using System.Windows.Media;
using System.Numerics;
using System.Text;

namespace Lab01
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const int MAXLENGTH = 190;
        private const int MAXROUND = 4;

        //Конструктор
        public MainWindow()
        {
            InitializeComponent();
            //TextBoxEquation.MaxLength = MAXLENGTH;
            //ParseEquation();
        }

        //Парсинг
        private void ParseEquation()
        {
            //Парсинг и решение
            double[] coeffs = App.SolveParser(TextBoxEquation.Text);
            if (coeffs == null)
            {
                TextBoxEquation.Foreground = Brushes.Red;
                LabelAnswers.Content = "";
                LabelCoeffs.Content = "";
                return;
            }
            Complex[] results = App.SolverCardano(coeffs);
            if (results == null)
            {
                TextBoxEquation.Foreground = Brushes.Red;
                LabelAnswers.Content = "";
                LabelCoeffs.Content = "";
                return;
            }
            TextBoxEquation.Foreground = Brushes.Green;

            //Коэффициенты
            StringBuilder coeffShow = new StringBuilder(100);
            coeffShow.AppendFormat("Коэффициенты:\n");
            for (int i = 3; i >= 0; i--)
            {
                coeffShow.AppendFormat("c");
                coeffShow.AppendFormat((4 - i).ToString());
                coeffShow.AppendFormat(" = ");
                coeffShow.AppendFormat(coeffs[i].ToString());
                coeffShow.AppendFormat("\n");
            }
            LabelCoeffs.Content = coeffShow.ToString();

            //Решение
            StringBuilder resultShow = new StringBuilder(MAXLENGTH);
            resultShow.AppendFormat("Кардано:\n");
            for (int i = 0; i < 3; i++)
            {
                resultShow.AppendFormat("x");
                resultShow.AppendFormat((i + 1).ToString());
                resultShow.AppendFormat(" = ");
                double real = Math.Round(results[i].Real, MAXROUND);
                if (real != 0)
                {
                    resultShow.AppendFormat(real.ToString());
                }
                double imagine = Math.Round(results[i].Imaginary, MAXROUND);
                if (imagine > 0 && real != 0)
                {
                    resultShow.AppendFormat("+");
                }
                if (imagine != 0)
                {
                    resultShow.AppendFormat(imagine.ToString());
                    resultShow.AppendFormat("*i");
                }
                if(real == 0 && imagine == 0)
                {
                    resultShow.AppendFormat("0");
                }

                resultShow.AppendFormat("\n");
            }
            resultShow.AppendFormat("\n");

            //Альтернативное решение
            results = App.SolverViet(coeffs);
            resultShow.AppendFormat("Виет-Кардано:\n");
            for (int i = 0; i < 3; i++)
            {
                resultShow.AppendFormat("x");
                resultShow.AppendFormat((i + 1).ToString());
                resultShow.AppendFormat(" = ");
                double real = Math.Round(results[i].Real, MAXROUND);
                if (real != 0)
                {
                    resultShow.AppendFormat(real.ToString());
                }
                double imagine = Math.Round(results[i].Imaginary, MAXROUND);
                if (imagine > 0 && real != 0)
                {
                    resultShow.AppendFormat("+");
                }
                if (imagine != 0)
                {
                    resultShow.AppendFormat(imagine.ToString());
                    resultShow.AppendFormat("*i");
                }
                if (real == 0 && imagine == 0)
                {
                    resultShow.AppendFormat("0");
                }

                resultShow.AppendFormat("\n");
            }

            LabelAnswers.Content = resultShow.ToString();
        }

        //Изменение поля
        private void TextBoxEquation_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            ParseEquation();
        }
    }
}
