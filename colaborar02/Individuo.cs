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
    internal class Individuo {
		public double a, b, c, d, e, f, g, h, i;

		//Guarda en "caché" el ajuste para no tener que calcularlo continuamente
		public double Ajuste;

		//Mejora el individuo antes de entrar a la arena de la competición, cambiando los coeficientes 
		public void MejoraIndividuo(Random azar, List<double> Entradas, List<double> Salidas, int posInicial, int posFinal, int CiclosMejoraIndividuo)
		{
			//Anteriores coeficientes
			double AntA, AntB, AntC, AntD, AntE, AntF, AntG, AntH, AntI;
			Ajuste = double.MaxValue;

			for (int mejora = 1; mejora <= CiclosMejoraIndividuo; mejora++) {

				//Guarda los coeficientes
				AntA = a;
				AntB = b;
				AntC = c;
				AntD = d;
				AntE = e;
				AntF = f;
				AntG = g;
				AntH = h;
				AntI = i;
				switch (azar.Next(0, 9)) {
					case 0: a += azar.NextDouble() * 2 - 1; break;
					case 1: b += azar.NextDouble() * 2 - 1; break;
					case 2: c += azar.NextDouble() * 2 - 1; break;
					case 3: d += azar.NextDouble() * 2 - 1; break;
					case 4: e += azar.NextDouble() * 2 - 1; break;
					case 5: f += azar.NextDouble() * 2 - 1; break;
					case 6: g += azar.NextDouble() * 2 - 1; break;
					case 7: h += azar.NextDouble() * 2 - 1; break;
					case 8: i += azar.NextDouble() * 2 - 1; break;
				}

				//El calculo del ajustes es la sumatoria de (valor esperado - valor calculado por el individuo)^2
				//Prueba el individuo con los datos
				double NuevoAjuste = 0;
				for (int cont = posInicial; cont < posFinal && cont < Entradas.Count; cont++) {
					double diferencia = Salida(Entradas[cont]) - Salidas[cont];
					NuevoAjuste += diferencia * diferencia;
					if (NuevoAjuste > Ajuste) break;
				}

				//Si el nuevo coeficiente genera un desempeño peor, entonces recupera los antiguos coeficientes
				if (NuevoAjuste > Ajuste) {
					a = AntA;
					b = AntB;
					c = AntC;
					d = AntD;
					e = AntE;
					f = AntF;
					g = AntG;
					h = AntH;
					i = AntI;
				}
				else
					Ajuste = NuevoAjuste;
			}
		}

		//Calcula el ajuste del individuo con los valores de salida esperados
		public void AjusteIndividuo(List<double> Entradas, List<double> Salidas, int posInicial, int posFinal) {
			//Si ya había sido calculado entonces evita calcularlo de nuevo
			if (Ajuste != -1) return;

			//El calculo del ajustes es la sumatoria de (valor esperado - valor calculado por el individuo)^2
			Ajuste = 0;
			for (int cont = posInicial; cont < posFinal && cont < Entradas.Count; cont++) {
				double diferencia = Salida(Entradas[cont]) - Salidas[cont];
				Ajuste += diferencia * diferencia;
			}
		}

		//Inicializa el individuo con las Piezas, Modificadores y Operadores al azar
		public Individuo(Random azar, int Rango) {
			a = azar.Next(-Rango, Rango);
			b = azar.Next(-Rango, Rango);
			c = azar.Next(-Rango, Rango);
			d = azar.Next(-Rango, Rango);
			e = azar.Next(-Rango, Rango);
			f = azar.Next(-Rango, Rango);
			g = azar.Next(-Rango, Rango);
			h = azar.Next(-Rango, Rango);
			i = azar.Next(-Rango, Rango);
			Ajuste = -1;
		}

		//Retorna el valor de salida obtenido por el individuo con el valor de entrada dado
		public double Salida(double x) {
			return (Math.Sin(a * (1 - x) * (1 - x) * (1 - x) * (1 - x) + b * (1 - x) * (1 - x) * (1 - x) + c * (1 - x) * (1 - x) + d * (1 - x) + e + f * x + g * x * x + h * x * x * x + i * x * x * x * x) + 1) / 2;
		}

		//Muta alguna parte del individuo
		public void Muta(Random azar) {
			switch (azar.Next(0, 9)) {
				case 0: a += azar.NextDouble() * 2 - 1; break;
				case 1: b += azar.NextDouble() * 2 - 1; break;
				case 2: c += azar.NextDouble() * 2 - 1; break;
				case 3: d += azar.NextDouble() * 2 - 1; break;
				case 4: e += azar.NextDouble() * 2 - 1; break;
				case 5: f += azar.NextDouble() * 2 - 1; break;
				case 6: g += azar.NextDouble() * 2 - 1; break;
				case 7: h += azar.NextDouble() * 2 - 1; break;
				case 8: i += azar.NextDouble() * 2 - 1; break;
			}
			Ajuste = -1;
		}
	}
}
