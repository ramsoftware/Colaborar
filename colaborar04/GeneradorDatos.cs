using System;
using System.Collections.Generic;

namespace Comparacion {
    public class GeneradorDatos {
        //Almacena los valores de la variable independiente (entrada)
        public List<double> Entradas = new List<double>();

        //Almacena los valores de la variable dependiente (salida) 
        public List<double> Salidas = new List<double>();

        //Ecuación generada al azar
        List<Pieza> Piezas = new List<Pieza>();

        //Usada para detectar si la ecuación generada al azar NO genera errores matemáticos
        bool HayError;

        //Genera la ecuación 
        public void GeneraEcuacion(Random Azar, int numPiezas, double numDatosDataSet) {

            //Si el mínimo valor y el máximo valor son muy parecidos, entonces la ecuación está generando una
            //línea horizontal de datos, sin cambios, eso no es aceptable.
            double MinimoValor, MaximoValor;

            //Evita que la ecuación genere simples líneas diagonales
            int Ysube, Ybaja;
            double Yanterior;

            do {
                HayError = false;
                MinimoValor = double.MaxValue;
                MaximoValor = double.MinValue;
                Ysube = 0;
                Ybaja = 0;
                Yanterior = 0;

                //Genera las piezas al azar que componen la ecuación
                Piezas.Clear();
                for (int cont = 0; cont < numPiezas; cont++) Piezas.Add(new Pieza(Azar, cont));

                //Evalúa con los datos de entrada de x de 0 a 1
                double avance = 1 / numDatosDataSet;
                for (double x = 0; x <= 1; x += avance) {
                    double y = Evaluar(x);
                    if (HayError) break;

                    //Si el mínimo valor y el máximo valor son muy parecidos, entonces la ecuación está generando una
                    //línea horizontal de datos, sin cambios, eso no es aceptable.
                    if (y < MinimoValor) MinimoValor = y;
                    if (y > MaximoValor) MaximoValor = y;

                    //Evita que la ecuación genere simples líneas diagonales
                    if (x > 0 && y > Yanterior) Ysube++;
                    if (x > 0 && y < Yanterior) Ybaja++;
                    Yanterior = y;
                }

                if (MaximoValor - MinimoValor <= 0.05 || Ysube == 0 || Ybaja == 0) HayError = true;
                if (HayError) continue;

                //Si no hay errores, entonces normaliza los valores y los guarda.
                Entradas.Clear();
                Salidas.Clear();
                for (double x = 0; x <= 1; x += avance) {
                    double y = (Evaluar(x) - MinimoValor) / (MaximoValor - MinimoValor);
                    Entradas.Add(x);
                    Salidas.Add(y);
                }


                //Si la ecuación genera errores o genera una línea horizontal de datos o es una simple diagonal entonces se genera otra 
            } while (HayError);
        }

        //Se va de pieza en pieza evaluando toda la ecuación
        public double Evaluar(double x) {
            double resultado = 0;

            for (int pos = 0; pos < Piezas.Count; pos++) {
                Pieza tmpPieza = Piezas[pos];
                double numA, numB;

                switch (tmpPieza.TipoA) {
                    case 0: numA = tmpPieza.NumeroA; break;
                    case 1: numA = x; break;
                    default: numA = Piezas[tmpPieza.PiezaA].ValorPieza; break;
                }

                switch (tmpPieza.TipoB) {
                    case 0: numB = tmpPieza.NumeroB; break;
                    case 1: numB = x; break;
                    default: numB = Piezas[tmpPieza.PiezaB].ValorPieza; break;
                }

                switch (tmpPieza.Operador) {
                    case 0: resultado = numA * numB; break;
                    case 1: resultado = numA / numB; break;
                    case 2: resultado = numA + numB; break;
                    case 3: resultado = numA - numB; break;
                    default: resultado = Math.Pow(numA, numB); break;
                }

                if (double.IsNaN(resultado) || double.IsInfinity(resultado)) {
                    HayError = true;
                    return 0;
                }

                switch (tmpPieza.Funcion) {
                    case 0: resultado = Math.Sin(resultado); break;
                    case 1: resultado = Math.Cos(resultado); break;
                    case 2: resultado = Math.Tan(resultado); break;
                    case 3: resultado = Math.Abs(resultado); break;
                    case 4: resultado = Math.Asin(resultado); break;
                    case 5: resultado = Math.Acos(resultado); break;
                    case 6: resultado = Math.Atan(resultado); break;
                    case 7: resultado = Math.Log(resultado); break;
                    case 8: resultado = Math.Ceiling(resultado); break;
                    case 9: resultado = Math.Exp(resultado); break;
                    case 10: resultado = Math.Sqrt(resultado); break;
                    case 11: resultado = Math.Pow(resultado, 0.3333333333333333333333); break;
                }

                if (double.IsNaN(resultado) || double.IsInfinity(resultado)) {
                    HayError = true;
                    return 0;
                }

                tmpPieza.ValorPieza = resultado;
            }
            return resultado;
        }
    }
}
