using System;
using System.Collections.Generic;
using System.IO;

namespace ConsoleApplication4
{
    class Program
    {
        //caso se 
        static string BingMapsKey = "AhvF5bvOKxLPD_KU-Ii09N3LoQDomL4rKGOafr-iaaWCUZTcPEY4wWuwd-dke1J5";

        static void Main(string[] args)
        {

            Rotas Objeto = new Rotas();
            int OPC = -1;
            string arquivo;
            string arquivoNovo;
            string locationsRequest;
            GeoCode locationsResponse;
            string origem, destino;
            int OPC1 = -1;
            while (OPC != 0)
            {
                Console.Clear();
                Console.WriteLine("Digite 1 para inserir o arquivo de rota:\n" +
                                  "       2 para consulta web:\n" +
                                  "       0 para sair:");
                OPC = Convert.ToInt32(Console.ReadLine());
                switch (OPC)
                {
                    case 1:
                        Console.WriteLine(@"Digite o caminho do arquivo txt:(ex: c:\windows\rota.txt)");
                        arquivo = Console.ReadLine();
                        StreamReader rd = new StreamReader(arquivo);
                        while (!rd.EndOfStream)
                        {
                            string linha = rd.ReadLine();
                            Console.WriteLine(linha);
                        }
                        Console.ReadKey();
                        rd.Close();
                        break;
                    case 2:
                        try
                        {

                            Console.WriteLine("Digite a origem:(ex: (rua/av),(cidade))");
                            origem = Console.ReadLine();
                            Console.WriteLine("Digite o destino:(ex: (rua/av),(cidade))");
                            destino = Console.ReadLine();
                            Console.Clear();
                            locationsRequest = Objeto.CreateRequest(origem, destino);
                            locationsResponse = Objeto.MakeRequest(locationsRequest);
                            Objeto.ProcessResponse(locationsResponse);
                            Console.WriteLine("Deseja salvar o arquivo?(S/N)");
                            string arq = Console.ReadLine();
                            if (arq == "s" || arq == "S")
                            {
                                Console.WriteLine("Digite o caminho para salvar o arquivo:");
                                arquivoNovo = Console.ReadLine();
                                Objeto.CreateTxt(locationsResponse, arquivoNovo);
                                //Objeto.criaMatriz(locationsResponse);
                            }
                            
                            Console.WriteLine("Pressione qualquer tecla");
                            Console.Clear();
                            while (OPC1 != 0)
                            {
                                Console.WriteLine(" Digite 1 inserir novo trajeto:\n" +
                                  "       2 para alterar um trajeto existente:\n" +
                                  "       3 para excluir um trajeto existente:\n" +
                                  "       0 para sair:");
                                OPC1 = Convert.ToInt32(Console.ReadLine());
                                Console.Clear();
                                switch (OPC1)
                                {
                                    case 1:
                                        //Objeto.CreateMatriz(locationsResponse,matriz);    
                                       

                                        
                                       // Objeto.ProcessResponse(locationsResponse);
                                        //Objeto.AddSteps(locationsResponse);

                                        break;
                                    case 2:
                                        Objeto.ProcessResponse(locationsResponse);
                                        //Objeto.UpdateSteps(locationsResponse);

                                        break;
                                    case 3:
                                        Objeto.ProcessResponse(locationsResponse);
                                        //Objeto.DeleteSteps(locationsResponse);
                                        break;
                                    case 0:
                                        break;
                                }
                            }



                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message);
                            Console.Read();

                        }
                        break;
                    case 0:
                        break;
                }
            }
        }

    }
}