using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;

namespace PrawdaFalsz
{
    public partial class MainPage : ContentPage
    {
        bool corect=true;
        public MainPage()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost/majnkraftroulette/");
            HttpResponseMessage response = client.GetAsync("pytanie").Result;
            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = response.Content.ReadAsStringAsync().Result;
                // Deserializacja JSON
                Question q = JsonConvert.DeserializeObject<Question>(jsonResponse);
                pytanieLabel.Text = q.Pytanie;
                if (q.Odpowiedz =="tak")


                    corect = true;
                else corect = false;
            }
           
            
            

        }
        private void LoadNewQuestion()
        {
            int index = random.Next(questions.Count);
            currentQuestion = questions[index];
            corect = currentQuestion.IsCorrect; 
                                                
            pytanieLabel.Text = currentQuestion.Text; 
        }
        private void OnTrueButtonClicked(object sender, EventArgs e)
        {
            if (corect)
            {
                DisplayAlert("Info", "Prawidłowa odpowiedż!", "OK");
                    LoadNewQuestion(); 


            }
            else { DisplayAlert("Info", "Nieprawidłowa odpowiedź!", "OK");
            }
        }

        private void OnFalseButtonClicked(object sender, EventArgs e)
        {

            if (!corect)
            {
                DisplayAlert("Info", "Prawidłowa odpowiedż!", "OK");



            }
            else
            {
                DisplayAlert("Info", "Nieprawidłowa odpowiedź!", "OK");
            }
        }
    }

   
}


