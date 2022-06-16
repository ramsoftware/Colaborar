using System;
using System.Collections.Generic;
using System.Diagnostics;


namespace Colaborar07 {
    internal class Program {
        static void Main(string[] args) {
            Random Azar = new Random(100); //Generador único

            int numPruebas = 20; //Número de veces que se generará un dataset con ecuación aleatoria para probar ambos algoritmos
            long TiempoParaOperar = 20000; //Cuántos milisegundos dará a cada tipo de algoritmo genético para operar
            int numPiezas = 5; //Número de piezas que compondrá la ecuación que genera el dataset

            //Configuración del algoritmo genético
            int IndividuosPorPoblacion = 1000;

            //Configuración del perceptrón multicapa
            int Capa0 = 5; //Total neuronas en la capa 0
            int Capa1 = 5; //Total neuronas en la capa 1

            Console.WriteLine("Fecha: " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            Console.WriteLine("Total Pruebas: " + numPruebas.ToString());
            Console.WriteLine("Tiempo en milisegundos para cada algoritmo: " + TiempoParaOperar.ToString());
            Console.WriteLine("Número de piezas que compondrá la ecuación que genera el dataset: " + numPiezas.ToString());

            Console.WriteLine("\r\n\r\nALGORITMO GENETICO: MEJORA LOS INDIVIDUOS Y LUEGO COMPITEN");
			Console.WriteLine("Número de individuos en la población: " + IndividuosPorPoblacion.ToString());

            Console.WriteLine("\r\n\r\nPERCEPTRON MULTICAPA");
            Console.WriteLine("Número de neuronas Capa 0: " + Capa0.ToString());
            Console.WriteLine("Número de neuronas Capa 1: " + Capa1.ToString() + "\r\n");

            //Prepara el generador de datos
            GeneradorDatos ConjuntoDatos = new GeneradorDatos();

            for (int Pruebas = 1; Pruebas <= numPruebas; Pruebas++) {

                //Genera el dataset en forma aleatoria
                ConjuntoDatos.GeneraEcuacion(Azar, numPiezas);

                //Llama al algoritmo genético que mejora primero los individuos
                Genetico(Azar, ConjuntoDatos.Entrada1, ConjuntoDatos.Entrada2, ConjuntoDatos.Salidas, TiempoParaOperar, IndividuosPorPoblacion);

                //Llama a la red neuronal
                Perceptron(Azar, ConjuntoDatos.Entrada1, ConjuntoDatos.Entrada2, ConjuntoDatos.Salidas, TiempoParaOperar, Capa0, Capa1);
            }

            Console.WriteLine("\r\nFINAL\r\n");
            //Console.ReadKey();
        }

        public static void Genetico(Random Azar, double[] Entrada1, double[] Entrada2, double[][] Salidas, long TiempoParaOperar, int IndividuosPorPoblacion) {
            PoblacionB poblacionB = new PoblacionB(Azar, IndividuosPorPoblacion);
            poblacionB.Proceso(Entrada1, Entrada2, Salidas, TiempoParaOperar);
            Console.Write("AGM;" + poblacionB.MejorAproximacion().ToString());
        }

        //La red neuronal
        public static void Perceptron(Random azar, double[] Entrada1, double[] Entrada2, double[][] Salidas, long TiempoParaOperar, int capa0, int capa1) {
            //Medidor de tiempos
            Stopwatch cronometro = new Stopwatch();
            cronometro.Reset();
            cronometro.Start();

            int numEntradas = 2; //Número de entradas
            int capa2 = 1; //Total neuronas en la capa 2
            Perceptron perceptron = new Perceptron(azar, numEntradas, capa0, capa1, capa2);

            //Estas serán las entradas externas al perceptrón
            List<double> entradas = new List<double>();
            entradas.Add(0);
            entradas.Add(0);

            //Estas serán las salidas esperadas externas al perceptrón
            List<double> salidaEsperada = new List<double>();
            salidaEsperada.Add(0);

            //Ciclo que entrena la red neuronal
            while (cronometro.ElapsedMilliseconds < TiempoParaOperar) {

                //Por cada ciclo, se entrena el perceptrón con todos los valores
                for (int X = 0; X < Salidas.Length; X++)
                    for (int Y = 0; Y < Salidas[X].Length; Y++) {
                        //Entradas y salidas esperadas
                        entradas[0] = Entrada1[X];
                        entradas[1] = Entrada2[Y];
                        salidaEsperada[0] = Salidas[X][Y];

                        //Primero calcula la salida del perceptrón con esas entradas
                        perceptron.calculaSalida(entradas);

                        //Luego entrena el perceptrón para ajustar los pesos y umbrales
                        perceptron.Entrena(entradas, salidaEsperada);
                    }
            }

            double Aproxima = 0;
            for (int X = 0; X < Salidas.Length; X++)
                for (int Y = 0; Y < Salidas[X].Length; Y++) {
                    //Entradas y salidas esperadas
                    entradas[0] = Entrada1[X];
                    entradas[1] = Entrada2[Y];
                    salidaEsperada[0] = Salidas[X][Y];

                    //Calcula la salida del perceptrón con esas entradas
                    perceptron.calculaSalida(entradas);
                    Aproxima += (perceptron.capas[2].salidas[0] - salidaEsperada[0]) * (perceptron.capas[2].salidas[0] - salidaEsperada[0]);
                }

            Console.WriteLine(";RN;" + Aproxima.ToString());
        }
    }
}
