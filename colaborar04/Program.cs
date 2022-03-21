/* Autor: Rafael Alberto Moreno Parra
Fecha: 13 de marzo de 2022
Algoritmo evolutivo
Paso 1: Dada una serie de valores (X, Y) donde X es la variable independiente y Y la variable dependiente
		se usará un algoritmo evolutivo para determinar cuál expresión matemática de tipo Y=F(X) logra
		ajustarse a esos datos.
Paso 2: Los individuos son  del tipo
		Y = (Math.Sin(a*(1-x)^4 + b*(1-x)^3 + c*(1-x)^2 + d*(1-x) + e + f*x + g*x^2 + h*x^3 + i*x^4) + 1)/2
Paso 3: La población se compone de N individuos generados al azar
Paso 4: El proceso evolutivo es:
			a. Generar una población de individuos al azar.
		    b. Cada individuo se modifica a si mismo (muta) para ajustarse lo mejor posible al conjunto de datos dado.
			c. Seleccionar dos individuos al azar.
			d. Evaluar cada individuo escogido que tanto ajusta con la salida esperada.
			e. El individuo con mejor ajuste sobreescribe al de menor ajuste. Vuelve al punto c.
Paso 5: El proceso evolutivo se repite N veces
Paso 6: Se escanea toda la población por el mejor individuo (mejor ajuste)
Paso 7: El mejor individuo muestra sus valores de salida dado los valores de entrada
*/
using System;
using System.Collections.Generic;


namespace Comparacion {
    internal class Program {
        static void Main(string[] args) {
			/* La comparación va a ser así:
			 * 1. Ciclo
			 * 2. Generar un dataset al azar
			 * 3. Probarlo con algoritmo genético clásico
			 * 4. Probarlo con algoritmo genético mejora individuo
			 * 5. Comparar tiempos y exactitud. Hacer que el tiempo sea el mismo.
			 * 6. Generar promedios de aproximación.
			 * */

			Random Azar = new Random(); //Generador único
			
			int numPruebas = 50; //Número de veces que se generará un dataset con ecuación aleatoria para probar ambos algoritmos
			int numDatosDataSet = 200; //Cuántos registros tendrá cada dataset
			long TiempoParaOperar = 10000; //Cuántos milisegundos dará a cada tipo de algoritmo genético para operar

			//Prepara el generador de datos
			GeneradorDatos genera = new GeneradorDatos();
			int numPiezas = 5;
			for (int pruebas = 1; pruebas <= numPruebas; pruebas++) {

				//Genera el dataset en forma aleatoria
				genera.GeneraEcuacion(Azar, numPiezas, numDatosDataSet);

				//Llama al algoritmo genético clásico
				GeneticoA(Azar, genera.Entradas, genera.Salidas, 0, genera.Entradas.Count, TiempoParaOperar);

				//Llama al algoritmo genético que mejora primero los individuos
				GeneticoB(Azar, genera.Entradas, genera.Salidas, 0, genera.Entradas.Count, TiempoParaOperar);
			}

			Console.WriteLine("\r\nFINAL\r\n");
			//Console.ReadKey();
        }

		public static void GeneticoA(Random Azar, List<double> Entradas, List<double> Salidas, int posInicial, int posFinal, long TiempoParaOperar) {
			//Configuración del algoritmo genético
			int IndividuosPorPoblacion = 200;
			int RangoCoeficientes = 20; //Rango para crear los coeficientes de los individuos.

			PoblacionA poblacionA = new PoblacionA(Azar, IndividuosPorPoblacion, RangoCoeficientes);
			poblacionA.Proceso(Azar, Entradas, Salidas, posInicial, posFinal, TiempoParaOperar);

			Console.Write("A;" + poblacionA.MejorAproximacion().ToString() + ";");
		}

		public static void GeneticoB(Random Azar, List<double> Entradas, List<double> Salidas, int posInicial, int posFinal, long TiempoParaOperar) {
			//Configuración del algoritmo genético
			int IndividuosPorPoblacion = 200;
			int RangoCoeficientes = 20; //Rango para crear los coeficientes de los individuos.

			PoblacionB poblacionB = new PoblacionB(Azar, IndividuosPorPoblacion, RangoCoeficientes);
			poblacionB.Proceso(Azar, Entradas, Salidas, posInicial, posFinal, TiempoParaOperar);

			Console.WriteLine("B;" + poblacionB.MejorAproximacion().ToString());
		}
	}
}
