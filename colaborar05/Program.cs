/* Autor: Rafael Alberto Moreno Parra
Fecha: 15 de marzo de 2022
Algoritmo evolutivo Mejorado vs Red Neuronal

*/
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Management;

namespace AGMvsRN {
    internal class Program {
        static void Main(string[] args) {
            Random Azar = new Random(); //Generador único

            int numPruebas = 50; //Número de veces que se generará un dataset con ecuación aleatoria para probar ambos algoritmos
            int numDatosDataSet = 100; //Cuántos registros tendrá cada dataset
            long TiempoParaOperar = 20000; //Cuántos milisegundos dará a cada tipo de algoritmo genético para operar
			int numPiezas = 5; //Número de piezas que compondrá la ecuación que genera el dataset

			//Configuración del algoritmo genético
			int IndividuosPorPoblacion = 100;
			int RangoCoeficientes = 20; //Rango para crear los coeficientes de los individuos.

			//Configuración del perceptrón multicapa
			int capa0 = 5; //Total neuronas en la capa 0
			int capa1 = 5; //Total neuronas en la capa 1

			Console.WriteLine("Fecha: " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
			Console.WriteLine("Total Pruebas: " + numPruebas.ToString());
			Console.WriteLine("Número de registros por conjunto de datos: " + numDatosDataSet.ToString());
			Console.WriteLine("Tiempo en milisegundos para cada algoritmo: " + TiempoParaOperar.ToString());
			Console.WriteLine("Número de piezas que compondrá la ecuación que genera el dataset: " + numPiezas.ToString());
			Console.WriteLine("Velocidad PC: " + GetCpuSpeed().ToString());

			Console.WriteLine("\r\n\r\nALGORITMO GENETICO: MEJORA LOS INDIVIDUOS Y LUEGO COMPITEN");
			Console.WriteLine("Número de individuos en la población: " + IndividuosPorPoblacion.ToString());
			Console.WriteLine("Rango de Coeficientes: " + RangoCoeficientes.ToString());

			Console.WriteLine("\r\n\r\nPERCEPTRON MULTICAPA");
			Console.WriteLine("Número de neuronas Capa 0: " + capa0.ToString());
			Console.WriteLine("Número de neuronas Capa 1: " + capa1.ToString() + "\r\n");

			//Prepara el generador de datos
			GeneradorDatos genera = new GeneradorDatos();

            for (int pruebas = 1; pruebas <= numPruebas; pruebas++) {

                //Genera el dataset en forma aleatoria
                genera.GeneraEcuacion(Azar, numPiezas, numDatosDataSet);

                //Llama al algoritmo genético que mejora primero los individuos
                GeneticoB(Azar, genera.Entradas, genera.Salidas, 0, genera.Entradas.Count, TiempoParaOperar, IndividuosPorPoblacion, RangoCoeficientes);

                //Llama a la red neuronal
                Perceptron(Azar, genera.Entradas, genera.Salidas, 0, genera.Entradas.Count, TiempoParaOperar, capa0, capa1);
            }

            Console.WriteLine("\r\nFINAL\r\n");
            //Console.ReadKey();
        }

        public static void GeneticoB(Random Azar, List<double> Entradas, List<double> Salidas, int posInicial, int posFinal, long TiempoParaOperar, int IndividuosPorPoblacion, int RangoCoeficientes) {

            PoblacionB poblacionB = new PoblacionB(Azar, IndividuosPorPoblacion, RangoCoeficientes);
            poblacionB.Proceso(Azar, Entradas, Salidas, posInicial, posFinal, TiempoParaOperar);

            Console.Write("AGM;" + poblacionB.MejorAproximacion().ToString());
        }

        //La red neuronal
        public static void Perceptron(Random Azar, List<double> Entradas, List<double> Salidas, int posInicial, int posFinal, long TiempoParaOperar, int capa0, int capa1) {
			//Medidor de tiempos
			Stopwatch cronometro = new Stopwatch();
			cronometro.Reset();
			cronometro.Start();

			int numEntradas = 1; //Número de entradas
			int capa2 = 1; //Total neuronas en la capa 2
			Perceptron perceptron = new Perceptron(numEntradas, capa0, capa1, capa2);

			//Estas serán las entradas externas al perceptrón
			List<double> entradas = new List<double>();
			entradas.Add(0);

			//Estas serán las salidas esperadas externas al perceptrón
			List<double> salidaEsperada = new List<double>();
			salidaEsperada.Add(0);

			//Ciclo que entrena la red neuronal
			while (cronometro.ElapsedMilliseconds < TiempoParaOperar) {

				//Por cada ciclo, se entrena el perceptrón con todos los valores
				for (int conjunto = 0; conjunto < Entradas.Count; conjunto++) {
					//Entradas y salidas esperadas
					entradas[0] = Entradas[conjunto];
					salidaEsperada[0] = Salidas[conjunto];

					//Primero calcula la salida del perceptrón con esas entradas
					perceptron.calculaSalida(entradas);

					//Luego entrena el perceptrón para ajustar los pesos y umbrales
					perceptron.Entrena(entradas, salidaEsperada);
				}
			}

			//Console.WriteLine("Entrada normalizada | Salida esperada normalizada | Salida perceptrón normalizada");
			double Aproxima = 0;
			for (int conjunto = 0; conjunto < Entradas.Count; conjunto++) {
				//Entradas y salidas esperadas
				entradas[0] = Entradas[conjunto];
				salidaEsperada[0] = Salidas[conjunto];

				//Calcula la salida del perceptrón con esas entradas
				perceptron.calculaSalida(entradas);
				Aproxima += (perceptron.capas[2].salidas[0] - salidaEsperada[0]) * (perceptron.capas[2].salidas[0] - salidaEsperada[0]);
			}

			Console.WriteLine(";RN;" + Aproxima.ToString());
		}

		static public uint GetCpuSpeed() {
			var managementObject = new ManagementObject("Win32_Processor.DeviceID='CPU0'");
			var speed = (uint)(managementObject["CurrentClockSpeed"]);
			managementObject.Dispose();
			return speed;
		}
	}
}
