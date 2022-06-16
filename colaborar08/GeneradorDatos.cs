/* Autor: Rafael Alberto Moreno Parra
 * Fecha: 17 de mayo de 2022
 * Objetivo: Generar una expresión matemática aleatoria de dos variables independientes  Z = F(X,Y)
 * 
La expresión matemática se forma así:

PiezaA = funcion( [x|y] [*|+|-|/] [x|y|numero] )
PiezaB = función( [x|y] [*|+|-|/] [x|y|numero|Pieza] ) [*|+|-|/] PiezaA
PiezaC = función( [x|y] [*|+|-|/] [x|y|numero|Pieza] ) [*|+|-|/] PiezaB
PiezaD = función( [x|y] [*|+|-|/] [x|y|numero|Pieza] ) [*|+|-|/] PiezaC
PiezaE = función( [x|y] [*|+|-|/] [x|y|numero|Pieza] ) [*|+|-|/] PiezaD

*/

using System;
using System.Collections.Generic;


namespace Colaborar08 {
	internal class GeneradorDatos {
		//Los valores de entrada
		public double[] Entrada1 = new double[20];
		public double[] Entrada2 = new double[20];

		//Un arreglo de arreglos será el que guarda los valores de salida
		public double[][] Salidas = new double[20][];

		//Ecuación generada al azar
		List<Pieza> Piezas = new List<Pieza>();

		//Usada para detectar si la ecuación generada al azar NO genera errores matemáticos
		bool ErrorDetectado;

		public GeneradorDatos() {
			//Crea el arreglo bidimensional
			for (int cont = 0; cont < Salidas.Length; cont++)
				Salidas[cont] = new double[20];
		}

		//Genera la ecuación 
		public void GeneraEcuacion(Random Azar, int numPiezas) {

			//Si el mínimo valor y el máximo valor son muy parecidos, entonces la ecuación está generando un
			//plano horizontal de datos, sin cambios, eso no es aceptable.
			double MinValorTotal, MaxValorTotal;

			//Verifica que en cada diferente valor de X hallan cambios en Y
			double MinimoValor, MaximoValor;

			//Evita que la ecuación genere simples líneas diagonales
			int Zsube, Zbaja;
			double Zanterior;

			do {
				MinValorTotal = double.MaxValue;
				MaxValorTotal = double.MinValue;

				//Genera las piezas al azar que componen la ecuación, verifica que las dos variables
				//participen en la ecuación
				Piezas.Clear();
				int cuentaX = 0;
				int cuentaY = 0;
				for (int cont = 0; cont < numPiezas; cont++) {
					Piezas.Add(new Pieza(Azar, cont));
					if (Piezas[Piezas.Count - 1].VariableA) cuentaX++; else cuentaY++;
				}
				if (cuentaX == 0 || cuentaY == 0) continue; //Si no hay participación de alguna variable

				for (int X = 0; X < Salidas.Length; X++) Entrada1[X] = (double)2 / Salidas.Length * X - 1;
				for (int Y = 0; Y < Salidas[0].Length; Y++) Entrada2[Y] = (double)2 / Salidas.Length * Y - 1;

				//Genera los registros
				ErrorDetectado = false;
				for (int X = 0; X < Salidas.Length && !ErrorDetectado; X++) {
					MinimoValor = double.MaxValue;
					MaximoValor = double.MinValue;
					Zsube = 0;
					Zbaja = 0;
					Zanterior = 0;
					for (int Y = 0; Y < Salidas[X].Length; Y++) {
						double Z = Evaluar((double)2 / Salidas.Length * X - 1, (double)2 / Salidas.Length * Y - 1);
						if (ErrorDetectado) break;

						//Si el mínimo valor y el máximo valor son muy parecidos, entonces la ecuación está generando una
						//línea horizontal de datos, sin cambios, eso no es aceptable.
						if (Z < MinValorTotal) MinValorTotal = Z;
						if (Z > MaxValorTotal) MaxValorTotal = Z;

						if (Z < MinimoValor) MinimoValor = Z;
						if (Z > MaximoValor) MaximoValor = Z;

						//Evita que la ecuación genere simples líneas diagonales
						if (Y > 0 && Z > Zanterior) Zsube++;
						if (Y > 0 && Z < Zanterior) Zbaja++;
						Zanterior = Z;

						//Guarda en el arreglo de arreglos
						Salidas[X][Y] = Z;
					}
					if (MaximoValor - MinimoValor <= 0.1 || Zsube == 0 || Zbaja == 0 || MaximoValor - MinimoValor > 20 || MaxValorTotal - MinValorTotal > 20) ErrorDetectado = true;
				}

				//Revisa ahora en YX para ver si se respeta lo de variación en YX
				for (int Y = 0; Y < Salidas[0].Length && !ErrorDetectado; Y++) {
					MinimoValor = double.MaxValue;
					MaximoValor = double.MinValue;
					for (int X = 0; X < Salidas.Length; X++) {
						double Z = Salidas[X][Y];
						if (Z < MinimoValor) MinimoValor = Z;
						if (Z > MaximoValor) MaximoValor = Z;
					}
					if (MaximoValor - MinimoValor <= 0.1 || MaximoValor - MinimoValor > 20) ErrorDetectado = true;
				}

				//Si la ecuación genera errores o genera una línea horizontal de datos o es una simple diagonal entonces se genera otra 
			} while (ErrorDetectado);

			//Normaliza la salida
			for (int X = 0; X < Salidas.Length; X++)
				for (int Y = 0; Y < Salidas[X].Length; Y++)
					Salidas[X][Y] = (Salidas[X][Y] - MinValorTotal) / (MaxValorTotal - MinValorTotal);
		}

		//Se va de pieza en pieza evaluando toda la ecuación
		public double Evaluar(double X, double Y) {
			double resultado = 0;

			for (int pos = 0; pos < Piezas.Count; pos++) {
				Pieza tmpPieza = Piezas[pos];
				double numA, numB;

				//¿Con qué variable trabaja?
				numA = tmpPieza.VariableA ? tmpPieza.coeficienteA * X : tmpPieza.coeficienteA * Y;

				//La parte siguiente es número, alguna pieza anterior o variable
				switch (tmpPieza.TipoB) {
					case 0: numB = tmpPieza.NumeroB; break;
					case 1: numB = tmpPieza.VariableB ? tmpPieza.coeficienteB * X : tmpPieza.coeficienteB * Y; ; break;
					default: numB = Piezas[tmpPieza.PiezaB].ValorPieza; break;
				}

				//Operación entre las dos partes
				switch (tmpPieza.OperadorA) {
					case 0: resultado = numA * numB; break;
					case 1: resultado = numA / numB; break;
					case 2: resultado = numA + numB; break;
					case 3: resultado = numA - numB; break;
				}

				if (double.IsNaN(resultado) || double.IsInfinity(resultado)) {
					ErrorDetectado = true;
					return 0;
				}

				//Si hay función
				switch (tmpPieza.Funcion) {
					case 0: resultado = Math.Sin(resultado); break;
					case 1: resultado = Math.Cos(resultado); break;
					case 2: resultado = Math.Tan(resultado); break;
					case 3: resultado = Math.Abs(resultado); break;
					case 4: resultado = Math.Asin(resultado); break;
					case 5: resultado = Math.Acos(resultado); break;
					case 6: resultado = Math.Atan(resultado); break;
					case 7: resultado = Math.Log(resultado); break;
					case 8: resultado = Math.Exp(resultado); break;
					case 9: resultado = Math.Sqrt(resultado); break;
				}

				if (double.IsNaN(resultado) || double.IsInfinity(resultado)) {
					ErrorDetectado = true;
					return 0;
				}

				//La tercera parte es con la pieza anterior
				if (pos > 0) {
					//Operación con la pieza anterior
					switch (tmpPieza.OperadorB) {
						case 0: resultado *= Piezas[pos - 1].ValorPieza; break;
						case 1: resultado /= Piezas[pos - 1].ValorPieza; break;
						case 2: resultado += Piezas[pos - 1].ValorPieza; break;
						case 3: resultado -= Piezas[pos - 1].ValorPieza; break;
					}

					if (double.IsNaN(resultado) || double.IsInfinity(resultado)) {
						ErrorDetectado = true;
						return 0;
					}
				}

				//Guarda el resultado dentro de la pieza para ser usada en la siguiente pieza
				tmpPieza.ValorPieza = resultado;
			}
			return resultado;
		}
	}
}
