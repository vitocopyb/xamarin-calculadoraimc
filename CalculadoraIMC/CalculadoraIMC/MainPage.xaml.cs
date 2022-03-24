using Acr.UserDialogs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CalculadoraIMC
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            //UserDialogs.Instance.ShowLoading("Calculando");

            string sAltura = Altura.Text;
            string sPeso = Peso.Text;

            if (!string.IsNullOrEmpty(sAltura) && !string.IsNullOrEmpty(sPeso))
            {
                double altura = double.Parse(sAltura);
                double peso = double.Parse(sPeso);
                double imc = peso / Math.Pow(altura, 2);

                // coloca un delay para esperar unos segundos
                bool cancelada = false;
                using (IProgressDialog dialog = UserDialogs.Instance.Progress("Calculando", () => cancelada = true, "Cancelar"))
                {
                    for (int i = 1; i <= 10; i++)
                    {
                        await Task.Delay(300);

                        if (!cancelada)
                        {
                            dialog.PercentComplete = i * 10;
                        }
                        else
                        {
                            break;
                        }
                    }
                }

                //await Task.Delay(3000);
                IMC.Text = Math.Round(imc, 2).ToString();

                // obtiene resultado del IMC
                string resultado = ResultadoIMC(imc);

                //UserDialogs.Instance.HideLoading();
                await DisplayAlert("Resultado", resultado, "Aceptar");
            }
            else
            {
                //UserDialogs.Instance.HideLoading();
                await DisplayAlert("Datos erróneos", "Por favor, ingrese toda la información", "Aceptar");
            }
        }

        private string ResultadoIMC(double imc)
        {
            string resultado;

            if (imc < 18.5)
            {
                resultado = "Tienes peso bajo";
            }
            else if (imc >= 18.5 && imc <= 24.9)
            {
                resultado = "Tu peso es normal";
            }
            else if (imc >= 25 && imc <= 29.9)
            {
                resultado = "Tienes sobrepeso";
            }
            else
            {
                resultado = "Tienes obesidad, ¡Cuídate!";
            }

            return resultado;
        }
    }
}
