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
	internal class PoblacionB {
		//Generador de nùmeros aleatorios
		Random azar;

		//Almacena los individuos de la población
		List<IndividuoB> Individuos = new List<IndividuoB>();

		//Inicializa la población con un número de individuos
		public PoblacionB(Random azar, int numIndividuos) {
			this.azar = azar;
			Individuos.Clear();
			for (int cont = 1; cont <= numIndividuos; cont++)
				Individuos.Add(new IndividuoB(azar));
		}

		public void Proceso(double[] Entrada1, double[] Entrada2, double[][] Salidas, int minY, int maxY, long TiempoParaOperar) {
			IndividuoB ganador, perdedor;

			//Medidor de tiempos
			Stopwatch cronometro = new Stopwatch();
			cronometro.Reset();
			cronometro.Start();

			//Llama al que mejora cada individuo
			int cont = 0;
			while (cronometro.ElapsedMilliseconds < TiempoParaOperar / 2) {
				Individuos[cont++].MejoraIndividuo(azar, Entrada1, Entrada2, Salidas, minY, maxY);
				if (cont >= Individuos.Count) cont = 0;
			}


			//Veces que se repetirá el proceso evolutivo
			while (cronometro.ElapsedMilliseconds < TiempoParaOperar) {

				//Escoge dos individuos al azar
				int indivA = azar.Next(Individuos.Count);
				int indivB;
				do {
					indivB = azar.Next(Individuos.Count);
				} while (indivB == indivA);

				//Evalúa cada individuo con respecto a las entradas y salidas esperadas
				Individuos[indivA].AjusteIndividuo(Entrada1, Entrada2, Salidas, minY, maxY);
				Individuos[indivB].AjusteIndividuo(Entrada1, Entrada2, Salidas, minY, maxY);

				//El mejor individuo genera una copia que sobreescribe al peor y la copia se muta
				if (Individuos[indivA].Ajuste < Individuos[indivB].Ajuste) {
					ganador = Individuos[indivA];
					perdedor = Individuos[indivB];
				}
				else {
					ganador = Individuos[indivB];
					perdedor = Individuos[indivA];
				}

				//Copia el individuo
				for (int copia = 0; copia < ganador.Coef.Length; copia++) perdedor.Coef[copia] = ganador.Coef[copia];

				//Muta la copia
				perdedor.Coef[azar.Next(0, perdedor.Coef.Length)] += azar.NextDouble() * 2 - 1;
				perdedor.Ajuste = -1;
			}
		}

		public double MejorAproximacion() {
			double MejorAjuste = double.MaxValue;
			for (int cont = 0; cont < Individuos.Count; cont++)
				if (Individuos[cont].Ajuste != -1 && Individuos[cont].Ajuste < MejorAjuste)
					MejorAjuste = Individuos[cont].Ajuste;
			return MejorAjuste;
		}
	}
}
