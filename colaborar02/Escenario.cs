/* Autor: Rafael Alberto Moreno Parra
Fecha: 27 de febrero de 2022
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


namespace OptimizaIndividuo {
    internal class Escenario {
		List<Poblacion> Poblaciones;

		//Crea una población por cada división del conjunto de datos.
		//Ejemplo: Si el conjunto de datos se divide en cinco partes, cada parte tendrá su propia población
		public Escenario(int DivisionConjuntoDatos, Random Azar, int TotalIndividuos, int RangoCoeficientes) {
			Poblaciones = new List<Poblacion>();
			for (int cont = 1; cont <= DivisionConjuntoDatos; cont++) {
				Poblaciones.Add(new Poblacion(Azar, TotalIndividuos, RangoCoeficientes));
			}
		}

		public void Procesar(Random Azar, List<double> Entradas, List<double> Salidas, int CiclosMejoraIndividuo, int CiclosGenetico) {
			double AjusteTotalDatos = 0;
			int RangoDatos = Entradas.Count / Poblaciones.Count;
			int posInicial = 0;
			int posFinal = posInicial + RangoDatos;

			//Como el conjunto de datos se divide en varias partes, ahora lo que se hace es evaluar
			//parte por parte, cada una con su propia población
			for (int cont = 0; cont < Poblaciones.Count; cont++) {

				//Busca el mejor individuo de determinada población en esa porción de datos
				Poblaciones[cont].Proceso(Azar, Entradas, Salidas, posInicial, posFinal, CiclosMejoraIndividuo, CiclosGenetico);

				//Suma los mejores ajustes de cada población
				AjusteTotalDatos += Poblaciones[cont].MejorAjuste;

				//Imprime los valores de los mejores individuos
				//Poblaciones[cont].Imprime(Entradas, Salidas, posInicial, posFinal);

				//Calcula que porción de datos se va a usar en cada población
				posInicial = posFinal + 1;
				posFinal += RangoDatos;
				if (cont == Poblaciones.Count - 1) posFinal = int.MaxValue;
			}

			//Imprime el ajuste de datos 
			Console.WriteLine(AjusteTotalDatos.ToString());
		}
	}
}