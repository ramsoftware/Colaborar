/* Autor: Rafael Alberto Moreno Parra
 * Fecha: 17 de mayo de 2022
 * 
 * Dada una serie de datos del tipo X,Y,Z donde X,Y son las entradas y Z es la salida.
 * El programa genera una ecuación al azar Z=F(X,Y) y da valores a X entre 0 y 1, Y entre 0 y 1. Así se
 * obtiene el dataset.
 *
 * Se busca la curva que mejor se ajuste a esa serie de datos.
 * La mejor curva permitiría hacer operaciones de interpolación.
 * Se prueban dos técnicas para buscar esa curva:
 * 1. Algoritmos evolutivos.
 * 2. Red neuronal, tipo perceptrón multicapa (algoritmos ya definidos)
 * 
 * La investigación se concentra en los algoritmos evolutivos, NO en la red neuronal.
 * Se comparan ambas técnicas, sobre cuál logra encontrar la curva con mejor ajuste con el mismo tiempo de procesamiento
 *
 * En este proyecto en particular "Colaborar08", en algoritmos evolutivos, se prueba a generar dos curvas,
 * la primera curva cubre la primera mitad de los datos y la segunda curva la segunda mitad de los datos. Luego la
 * expresión sería:
 *      Z = f(X,Y) si Y está entre 0 y 0.5
 *      Z = g(X,Y) si Y está entre 0.5 y 1
*/
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Colaborar08 {
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
			Console.ReadKey();
		}

		public static void Genetico(Random Azar, double[] Entrada1, double[] Entrada2, double[][] Salidas, long TiempoParaOperar, int IndividuosPorPoblacion) {
			PoblacionB poblacion1 = new PoblacionB(Azar, IndividuosPorPoblacion);
			PoblacionB poblacion2 = new PoblacionB(Azar, IndividuosPorPoblacion);

			poblacion1.Proceso(Entrada1, Entrada2, Salidas, 0, Entrada2.Length / 2, TiempoParaOperar / 2);
			poblacion2.Proceso(Entrada1, Entrada2, Salidas, Entrada2.Length / 2, Entrada2.Length-1, TiempoParaOperar / 2);

			double Aproxima = (poblacion1.MejorAproximacion() + poblacion2.MejorAproximacion()) / 2;
			Console.Write("AGM;" + Aproxima.ToString());
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

