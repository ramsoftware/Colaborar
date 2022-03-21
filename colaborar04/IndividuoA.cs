using System;
using System.Collections.Generic;


namespace Comparacion {
    internal class IndividuoA {
        public double a, b, c, d, e, f, g, h, i;

        //Guarda en "caché" el ajuste para no tener que calcularlo continuamente
        public double Ajuste;

        //Inicializa el individuo con las Piezas, Modificadores y Operadores al azar
        public IndividuoA(Random azar, int Rango) {
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
