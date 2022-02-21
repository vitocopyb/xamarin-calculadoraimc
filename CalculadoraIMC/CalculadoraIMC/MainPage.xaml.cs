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
            string sAltura = Altura.Text;
            string sPeso = Peso.Text;

            if (!string.IsNullOrEmpty(sAltura) && !string.IsNullOrEmpty(sPeso))
            {
                double altura = double.Parse(sAltura);
                double peso = double.Parse(sPeso);
                double imc = peso / Math.Pow(altura, 2);

                IMC.Text = Math.Round(imc, 2).ToString();

                // obtiene resultado del IMC
                string resultado = ResultadoIMC(imc);

                await DisplayAlert("Resultado", resultado, "Aceptar");
            }
            else
            {
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
