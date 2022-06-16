using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Colaborar07 {
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

		public void Proceso(double[] Entrada1, double[] Entrada2, double[][] Salidas, long TiempoParaOperar) {
			IndividuoB ganador, perdedor;

			//Medidor de tiempos
			Stopwatch cronometro = new Stopwatch();
			cronometro.Reset();
			cronometro.Start();

			//Llama al que mejora cada individuo
			int cont = 0;
			while (cronometro.ElapsedMilliseconds < TiempoParaOperar / 2) {
				Individuos[cont++].MejoraIndividuo(azar, Entrada1, Entrada2, Salidas);
				if (cont >= Individuos.Count) cont = 0;
			}


			//Tiempo que se repetirá el proceso evolutivo
			while (cronometro.ElapsedMilliseconds < TiempoParaOperar) {

				//Escoge dos individuos al azar
				int indivA = azar.Next(Individuos.Count);
				int indivB;
				do {
					indivB = azar.Next(Individuos.Count);
				} while (indivB == indivA);

				//Evalúa cada individuo con respecto a las entradas y salidas esperadas
				Individuos[indivA].AjusteIndividuo(Entrada1, Entrada2, Salidas);
				Individuos[indivB].AjusteIndividuo(Entrada1, Entrada2, Salidas);

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
