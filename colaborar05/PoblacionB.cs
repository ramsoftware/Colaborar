using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace AGMvsRN {
    internal class PoblacionB {
		//Almacena los individuos de la población
		List<IndividuoB> Individuos = new List<IndividuoB>();

		//Inicializa la población con un número de individuos
		public PoblacionB(Random azar, int numIndividuos, int RangoCoeficientes) {
			Individuos.Clear();
			for (int cont = 1; cont <= numIndividuos; cont++) 
				Individuos.Add(new IndividuoB(azar, RangoCoeficientes));
		}

		public void Proceso(Random azar, List<double> Entradas, List<double> Salidas, int posInicial, int posFinal, long TiempoParaOperar) {
			int iteraA = 0, iteraB = 0;

			//Medidor de tiempos
			Stopwatch cronometro = new Stopwatch();
			cronometro.Reset();
			cronometro.Start();

			//Llama al que mejora cada individuo
			int cont = 0;
			while (cronometro.ElapsedMilliseconds < TiempoParaOperar / 2) {
				Individuos[cont].MejoraIndividuo(azar, Entradas, Salidas, posInicial, posFinal);
				cont++;
				if (cont >= Individuos.Count) cont = 0;
				iteraA++;
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
				Individuos[indivA].AjusteIndividuo(Entradas, Salidas, posInicial, posFinal);
				Individuos[indivB].AjusteIndividuo(Entradas, Salidas, posInicial, posFinal);

				//El mejor individuo genera una copia que sobreescribe al peor y la copia se muta
				if (Individuos[indivA].Ajuste < Individuos[indivB].Ajuste)
					CopiaMuta(azar, indivA, indivB);
				else
					CopiaMuta(azar, indivB, indivA);

				iteraB++;
			}

			//Console.Write("Genético mejorando individuo. Tiempo: " + cronometro.ElapsedMilliseconds.ToString() + " Itera A: " + iteraA.ToString() + " Itera B: " + iteraB.ToString());
		}

		public void CopiaMuta(Random azar, int Origen, int Destino) {
			IndividuoB ganador, perdedor;
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
