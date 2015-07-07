using System;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Web.Script.Serialization;
using System.Text.RegularExpressions;
using System.IO;
using System.Collections;
using System.Collections.Generic;

namespace ConsoleApplication4
{
    class Rotas
    {
        IList<Location> lista;
        Class1 obj;

        //Criação da URL que irá gerar o arquivo json da coordenada passada
        public string CreateRequest(string origin, string destination)
        {

            string url = string.Format("http://maps.googleapis.com/maps/api/directions/json?origin={0}&destination={1}&avoid=highways&mode=driving&language=pt-br&sensor=false&alternatives=true", origin, destination);

            return url;
        }
        //Apartir da requisição json, popula um objeto do tipo GeoCode
        public GeoCode MakeRequest(string requestUrl)
        {

            try
            {

                HttpWebRequest request = WebRequest.Create(requestUrl) as HttpWebRequest;

                using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
                {
                    if (response.StatusCode != HttpStatusCode.OK)
                        throw new Exception(String.Format("Server error (HTTP {0}: {1}).", response.StatusCode, response.StatusDescription));
                    DataContractJsonSerializer jsonSerializer = new DataContractJsonSerializer(typeof(GeoCode));
                    object objResponse = jsonSerializer.ReadObject(response.GetResponseStream());
                    GeoCode jsonResponse
                    = objResponse as GeoCode;
                    return jsonResponse;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }

        }
        //função responsável por exibição do passo a passo 
        public void ProcessResponse(GeoCode locationsResponse)
        {
            //string utilizada para remover as tags html
            const string HTML_TAG_PATTERN = "<.*?>";

            //int locNum = locationsResponse.rotas[0].trajeto.Length;
            //for (int i = 0; i < locNum; i++)
            //{
            foreach (var item in locationsResponse.rotas)
            {

                foreach (var rota in item.trajeto)
                {
                    Console.WriteLine(" Origem: " + rota.ruaOrigem);
                    Console.WriteLine("Destino: " + rota.ruaDestino);
                    Console.WriteLine();
                    for (int j = 0; j < rota.caminhos.Length; j++)
                    {
                    //    Console.WriteLine("lat origem:" + rota.caminhos[j].origem.lat);
                    //    Console.WriteLine("long origem:" + rota.caminhos[j].origem.lng);

                        Console.WriteLine("Passo " + j + ": " + Regex.Replace(rota.caminhos[j].instrucao, HTML_TAG_PATTERN, string.Empty));
                        Console.WriteLine("Tempo estimado: " + rota.caminhos[j].duracao.valor);
                        Console.WriteLine("Distância à percorrer nesse caminho: " + rota.caminhos[j].distancia.texto);
                        //Console.WriteLine("lat Destino:" + rota.caminhos[j].destino.lat);
                        //Console.WriteLine("long Destino:" + rota.caminhos[j].destino.lng);

                    }
                    Console.WriteLine();
                    Console.WriteLine();

                }
                Console.ReadKey();
            }
        }
        public void editaTrajeto(GeoCode locationsResponse, int valor) {


            const string HTML_TAG_PATTERN = "<.*?>";

            foreach (var rota in locationsResponse.rotas[valor].trajeto)
            {

                Console.WriteLine(" Origem: " + rota.ruaOrigem);
                Console.WriteLine("Destino: " + rota.ruaDestino);
                Console.WriteLine();
                for (int j = 0; j < rota.caminhos.Length; j++)
                {

                    Console.WriteLine("Passo " + j + ": " + Regex.Replace(rota.caminhos[j].instrucao, HTML_TAG_PATTERN, string.Empty));
                    Console.WriteLine("Tempo estimado: " + rota.caminhos[j].duracao.valor);
                    Console.WriteLine("Distância à percorrer nesse caminho: " + rota.caminhos[j].distancia.texto);

                }
            }

        }
        public void criaMatriz(GeoCode locationsResponse)
        {
            const string HTML_TAG_PATTERN = "<.*?>";
            int countOrigem = 0;
            int countDestino = 0;
            foreach (var item in locationsResponse.rotas)
            {
                //var x = locationsResponse.rotas.Length;
                foreach (var rota in item.trajeto)
                {
                    //var y = item.trajeto.Length;

                    foreach (var itens in rota.caminhos)
                    {

                        if (!lista.Contains(itens.origem))
                        {
                            lista.Add(itens.origem);
                            obj.origem = itens.origem;
                            obj.tempo.texto = itens.duracao.texto;
                            obj.tempo.valor = itens.duracao.valor;
                            obj.destino.Add(itens.destino);
                            //Console.WriteLine("passo:{0}     origem{1}",item.trajeto)
                        }
                        else
                        {
                            //já existe a origem
                        }
                        //passos.Add(items.origem);
                    }


                    //for (int j = 0; j < rota.caminhos.Length; j++)
                    //{
                    //    Console.WriteLine("lat origem:" + rota.caminhos[j].origem.lat);
                    //    Console.WriteLine("long origem:" + rota.caminhos[j].origem.lng);
                }

            }
        }

        //Cria um arquivo txt com o passo a passo gerado pelo json.
        public void CreateTxt(GeoCode locationsResponse, string arquivo)
        {
            StreamWriter rd = File.CreateText(arquivo);
            const string HTML_TAG_PATTERN = "<.*?>";
            
            foreach (var item in locationsResponse.rotas)
            {
                var x = locationsResponse.rotas.Length;

                //Legs rota = (Legs)locationsResponse.rotas[0].trajeto[i];
                foreach (var rota in item.trajeto)
                {
                    var y = item.trajeto.Length;
                    var distanciaTotal = 0;
                    var tempoTotal = 0;
                    Console.WriteLine(" Origem: " + rota.ruaOrigem);
                    Console.WriteLine("Destino: " + rota.ruaDestino);
                    Console.WriteLine();
                    //foreach (var items in rota.caminhos)
                    //{
                    //    passos.Add(items.instrucao);
                    //} 
                    
                    for (int j = 0; j < rota.caminhos.Length; j++)
                    {
                        tempoTotal =tempoTotal+ rota.caminhos[j].distancia.valor; ;
                        distanciaTotal = distanciaTotal + rota.caminhos[j].distancia.valor;
                        rd.WriteLine(j + ": " + Regex.Replace(rota.caminhos[j].instrucao, HTML_TAG_PATTERN, string.Empty) + ";" + rota.caminhos[j].distancia.texto);
                    }
                    rd.WriteLine("tempo Estimado:{0} distancia Estimada:{1} ", tempoTotal/60, distanciaTotal/1000);
                    rd.WriteLine();
                    rd.WriteLine();
                }
            } rd.Close();
        }

    }

}


