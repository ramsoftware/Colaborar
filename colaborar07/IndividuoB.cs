using System;

namespace Colaborar07 {
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
		public void MejoraIndividuo(Random azar, double[] Entrada1, double[] Entrada2, double[][] Salidas) {
			int Cambiar = azar.Next(0, Coef.Length);
			double Antes = Coef[Cambiar];
			Coef[Cambiar] += azar.NextDouble() * 2 - 1;

			//El cálculo del ajustes es la sumatoria de (valor calculado por el individuo - valor esperado)^2
			double NuevoAjuste = 0;
			for (int X = 0; X < Salidas.Length; X++) {
				for (int Y = 0; Y < Salidas[X].Length; Y++) {
					double diferencia = Salida(Entrada1[X], Entrada2[Y]) - Salidas[X][Y];
					NuevoAjuste += diferencia * diferencia;
					
					//Si el cambio hace que pierda ajuste, entonces devuelve el cambio
					if (NuevoAjuste > Ajuste && Ajuste != -1) {
						Coef[Cambiar] = Antes;
						return;
					}
				}
			}

			Ajuste = NuevoAjuste;
		}

		//Calcula el ajuste del individuo con los valores de salida esperados
		public void AjusteIndividuo(double[] Entrada1, double[] Entrada2, double[][] Salidas) {
			//Si ya había sido calculado entonces evita calcularlo de nuevo
			if (Ajuste != -1) return;

			//El calculo del ajustes es la sumatoria de (valor esperado - valor calculado por el individuo)^2
			Ajuste = 0;
			for (int X = 0; X < Salidas.Length; X++)
				for (int Y = 0; Y < Salidas[X].Length; Y++) {
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
