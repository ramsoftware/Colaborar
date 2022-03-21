/* Autor: Rafael Alberto Moreno Parra
Fecha: 22 de febrero de 2022
Algoritmo evolutivo
Paso 1: Dada una serie de valores (X, Y) donde X es la variable independiente y Y la variable dependiente
		se usará un algoritmo evolutivo para determinar cuál expresión matemática de tipo Y=F(X) logra
		ajustarse a esos datos.
Paso 2: Los individuos son  del tipo
		Y = (Math.Sin(a*(1-x)^4 + b*(1-x)^3 + c*(1-x)^2 + d*(1-x) + e + f*x + g*x^2 + h*x^3 + i*x^4) + 1)/2
Paso 3: La población se compone de N individuos generados al azar
Paso 4: El proceso evolutivo es:
			a. Seleccionar dos individuos al azar
			b. Evaluar cada individuo escogido que tanto ajusta con la salida esperada
			c. El individuo con mejor ajuste sobreescribe al de menor ajuste
			d. El nuevo individuo (copia del mejor) se modifica al azar
Paso 5: El proceso evolutivo se repite N veces
Paso 6: Se escanea toda la población por el mejor individuo (mejor ajuste)
Paso 7: El mejor individuo muestra sus valores de salida dado los valores de entrada
*/
using System;
using System.Collections.Generic;


namespace DividirDatos {
	class Program {
		static void Main() {
			Console.WriteLine("Genético con polinomios. 22 de febrero de 2022.");

			//Almacena los valores de la variable independiente (entrada)
			List<double> entrada = new List<double>();

			//Almacena los valores de la variable dependiente (salida) 
			List<double> salida = new List<double>();

			//Genera un "dataset" con una ecuación generada al azar
			Random Azar = new Random();
			double numDatosDataSet = 2000; //Cuantos datos tendrá el dataset

			Console.WriteLine("Genera un conjunto de datos (dataset) de una ecuación generada al azar y luego averiguar la tendencia usando algoritmos genéticos");
			Console.WriteLine("Total registros en el dataset: " + numDatosDataSet.ToString());
			GeneraDatos(Azar, entrada, salida, numDatosDataSet);

			//Configuración del algoritmo genético
			int IndividuosPorPoblacion = 200; //Total de individuos en la población
			int TotalCiclosEvalua = 10000; //Cuantas veces ejecutará el proceso completo de selección y evaluación de individuos
			int RangoCoeficientes = 20; //Rango para crear los coeficientes de los individuos.
			Console.WriteLine("Total individuos por población: " + IndividuosPorPoblacion.ToString());
			Console.WriteLine("Total ciclos a evaluar: " + TotalCiclosEvalua.ToString());

			//Usa uno, dos, tres, ... N individuos para cubrir el conjunto de datos
			Console.WriteLine("Número de individuos que cubren los datos");
			List<Escenario> Escenarios = new List<Escenario>();
			int MaxIndividuos = 10;
			for (int numIndividuos = 0; numIndividuos < MaxIndividuos; numIndividuos++) {
				Console.Write((numIndividuos + 1).ToString() + ";");
				Escenarios.Add(new Escenario(numIndividuos + 1, Azar, IndividuosPorPoblacion, RangoCoeficientes));
				Escenarios[numIndividuos].Procesar(Azar, entrada, salida, TotalCiclosEvalua);
			}
			Console.WriteLine("FINAL");
			Console.ReadKey();
		}

		//Genera una ecuación al azar para que de origen al conjunto de datos
		//Los datos de entrada y salida salen normalizados
		public static void GeneraDatos(Random Azar, List<double> Entradas, List<double> Salidas, double numDatosDataSet) {
			double Potencia = 30;
			double a = Azar.NextDouble() * Potencia;
			double b = Azar.NextDouble() * Potencia;
			double c = Azar.NextDouble() * Potencia;
			double d = Azar.NextDouble() * Potencia;

			double avance = 1 / numDatosDataSet;
			for (double x = 0; x <= 1; x+=avance) {
				double y = (Math.Sin(a * x * x * x + b * x * x + c * x + d) + 1) / 2;
				Entradas.Add(x);
				Salidas.Add(y);
			}
		}
	}
}
