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

namespace Colaborar08 {
	internal class IndividuoB {
		public double[] Coef;

		//Guarda en "caché" el ajuste para no tener que calcularlo continuamente
		public double Ajuste;

		//Inicializa el individuo con las Piezas, Modificadores y Operadores al azar
		public IndividuoB(Random azar) {
			Coef = new double[18];
			for (int cont = 0; cont < Coef.Length; cont++) Coef[cont] = azar.NextDouble();
			Ajuste = -1;
		}

		//Mejora el individuo antes de entrar a la arena de la competición, cambiando los coeficientes 
		public void MejoraIndividuo(Random azar, double[] Entrada1, double[] Entrada2, double[][] Salidas, int minY, int maxY) {
			int Cambiar = azar.Next(0, Coef.Length);
			double Antes = Coef[Cambiar];
			Coef[Cambiar] += azar.NextDouble() * 2 - 1;

			//El cálculo del ajustes es la sumatoria de (valor esperado - valor calculado por el individuo)^2
			//Prueba el individuo con los datos
			double NuevoAjuste = 0;
			for (int X = 0; X < Salidas.Length; X++) {
				for (int Y = minY; Y <= maxY; Y++) {
					double diferencia = Salida(Entrada1[X], Entrada2[Y]) - Salidas[X][Y];
					NuevoAjuste += diferencia * diferencia;
					if (NuevoAjuste > Ajuste && Ajuste != -1) {
						Coef[Cambiar] = Antes;
						return;
					}
				}
			}

			Ajuste = NuevoAjuste;
		}

		//Calcula el ajuste del individuo con los valores de salida esperados
		public void AjusteIndividuo(double[] Entrada1, double[] Entrada2, double[][] Salidas, int minY, int maxY) {
			//Si ya había sido calculado entonces evita calcularlo de nuevo
			if (Ajuste != -1) return;

			//El calculo del ajustes es la sumatoria de (valor esperado - valor calculado por el individuo)^2
			Ajuste = 0;
			for (int X = 0; X < Salidas.Length; X++)
				for (int Y = minY; Y <= maxY; Y++) {
					double Diferencia = Salida(Entrada1[X], Entrada2[Y]) - Salidas[X][Y];
					Ajuste += Diferencia * Diferencia;
				}
		}

		//Retorna el valor de salida obtenido por el individuo con el valor de entrada dado
		public double Salida(double x, double y) {
			double valorP1 = Coef[0] * (1 - x) * (1 - x) * (1 - x) * (1 - x) + Coef[1] * (1 - x) * (1 - x) * (1 - x) + Coef[2] * (1 - x) * (1 - x) + Coef[3] * (1 - x) + Coef[4] + Coef[5] * x + Coef[6] * x * x + Coef[7] * x * x * x + Coef[8] * x * x * x * x;
			double valorP2 = Coef[9] * (1 - y) * (1 - y) * (1 - y) * (1 - y) + Coef[10] * (1 - y) * (1 - y) * (1 - y) + Coef[11] * (1 - y) * (1 - y) + Coef[12] * (1 - x) + Coef[13] + Coef[14] * y + Coef[15] * y * y + Coef[16] * y * y * y + Coef[17] * y * y * y * y;
			return (Math.Sin(valorP1 + valorP2) + 1) / 2;
		}
	}
}
