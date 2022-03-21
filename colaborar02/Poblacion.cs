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
using System.Diagnostics;

namespace OptimizaIndividuo {
    internal class Poblacion {
		//Busca el individuo con mejor ajuste
		public double MejorAjuste;
		public int ElMejor;
		public long TiempoTomado;

		//Almacena los individuos de la población
		List<Individuo> Individuos = new List<Individuo>();

		//Inicializa la población con un número de individuos
		public Poblacion(Random azar, int numIndividuos, int RangoCoeficientes) {
			for (int cont = 1; cont <= numIndividuos; cont++) Individuos.Add(new Individuo(azar, RangoCoeficientes));
		}

		public void Proceso(Random azar, List<double> Entradas, List<double> Salidas, int posInicial, int posFinal, int CiclosMejoraIndividuo, int CiclosGenetico) {
			//Busca el individuo con mejor ajuste
			MejorAjuste = double.MaxValue;
			ElMejor = -1;

			//Medidor de tiempos
			Stopwatch cronometro = new Stopwatch();
			cronometro.Reset();
			cronometro.Start();

			//Llama al que mejora cada individuo
			for (int cont = 0; cont < Individuos.Count; cont++)
				Individuos[cont].MejoraIndividuo(azar, Entradas, Salidas, posInicial, posFinal, CiclosMejoraIndividuo);

			//Veces que se repetirá el proceso evolutivo
			for (int ciclo = 1; ciclo <= CiclosGenetico; ciclo++) {

				//Escoge dos individuos al azar
				int indivA = azar.Next(Individuos.Count);
				int indivB;
				do {
					indivB = azar.Next(Individuos.Count);
				} while (indivB == indivA);

				//Evalúa cada individuo con respecto a las entradas y salidas esperadas
				Individuos[indivA].AjusteIndividuo(Entradas, Salidas, posInicial, posFinal);
				Individuos[indivB].AjusteIndividuo(Entradas, Salidas, posInicial, posFinal);

				//El mejor individuo genera una copia que sobreescribe al peor y la copia se muta
				if (Individuos[indivA].Ajuste < Individuos[indivB].Ajuste)
					CopiaMuta(azar, indivA, indivB);
				else
					CopiaMuta(azar, indivB, indivA);
			}

			//Tiempo tomado por el proceso
			TiempoTomado = cronometro.ElapsedMilliseconds;
		}

		public void CopiaMuta(Random azar, int Origen, int Destino) {
			Individuo ganador, perdedor;
			ganador = Individuos[Origen];
			perdedor = Individuos[Destino];

			//Copia el individuo
			perdedor.a = ganador.a;
			perdedor.b = ganador.b;
			perdedor.c = ganador.c;
			perdedor.d = ganador.d;
			perdedor.e = ganador.e;
			perdedor.f = ganador.f;
			perdedor.g = ganador.g;
			perdedor.h = ganador.h;
			perdedor.i = ganador.i;

			//Muta la copia
			perdedor.Muta(azar);

			//Verifica si el ganador, tiene el mejor ajuste
			if (ganador.Ajuste < MejorAjuste) {
				MejorAjuste = ganador.Ajuste;
				ElMejor = Origen;
			}
		}

		//Imprime los valores de salida del mejor individuo de la población
		public void Imprime(List<double> Entradas, List<double> Salidas, int posInicial, int posFinal) {
			//Muestra los valores esperados junto con el valor calculado
			for (int cont = posInicial; cont <= posFinal && cont < Entradas.Count; cont++) {
				double Ycalcula = Individuos[ElMejor].Salida(Entradas[cont]);
				Console.WriteLine(Entradas[cont].ToString() + ";" + Salidas[cont].ToString() + ";" + Ycalcula.ToString());
			}
		}
	}
}

