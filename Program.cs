using System;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;

namespace IntegracaoChatGPT
{
    class Program
    {
        static void Main(string[] args)
        {
            // Substituir pela sua chave de API OpenAI
            var apiKey = "COLOQUE_SUA_CHAVE_API";

            // Criar um novo HttpClient
            using (var client = new HttpClient())
            {
                // Configurar a URL da API OpenAI
                client.BaseAddress = new Uri("https://api.openai.com/v1/completions");

                // Configurar a autorização com a sua chave de API
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}");

                // Criar um objeto JSON com os parâmetros da solicitação
                var request = new
                {
                    model = "text-davinci-003",
                    prompt = "Como ganhar dinheiro com programação? Liste em tópicos",
                    max_tokens = 2200,
                    n = 1,
                    stop = (string)null,
                    temperature = 0.5
                };

                // Serializar o objeto JSON em uma string
                var requestJson = JsonConvert.SerializeObject(request);

                // Criar uma nova solicitação HTTP POST
                var response = client.PostAsync("", new StringContent(requestJson, Encoding.UTF8, "application/json")).Result;

                // Verificar se a solicitação foi bem-sucedida
                if (response.IsSuccessStatusCode)
                {
                    // Obter a resposta da API como uma string
                    var responseJson = response.Content.ReadAsStringAsync().Result;

                    // Deserializar a resposta JSON em um objeto dinâmico
                    dynamic responseData = JsonConvert.DeserializeObject(responseJson);

                    // Imprimir a resposta gerada pelo GPT-3
                    Console.WriteLine(responseData.choices[0].text);
                }
                else
                {
                    Console.WriteLine("A solicitação à API OpenAI falhou com o status: " + response.StatusCode);
                }
            }
            Console.ReadLine();
        }
    }
}
