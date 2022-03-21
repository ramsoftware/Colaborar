using System;

namespace Comparacion {
    public class Pieza {
        public double ValorPieza; /* Almacena el valor que genera la pieza al evaluarse */
        public int Funcion; /* Código de la función 0:seno, 1:coseno, 2:tangente, 3: valor absoluto, 4: arcoseno, 5: arcocoseno, 6: arcotangente, 7: logaritmo natural, 8: valor tope, 9: exponencial, 10: raíz cuadrada, 11: raíz cúbica */
        public int TipoA; /* La primera parte es un número o una variable o trae el valor de otra pieza */
        public double NumeroA; /* Es un número literal */
        public int PiezaA; /* Trae el valor de otra pieza */
        public int Operador; /* + suma - resta * multiplicación / división ^ potencia */
        public int TipoB; /* La segunda parte es un número o una variable o trae el valor de otra pieza */
        public double NumeroB; /* Es un número literal */
        public int PiezaB; /* Trae el valor de otra pieza */

        public Pieza(Random Azar, int maxPieza) {
            if (Azar.NextDouble() < 0.5)
                Funcion = -1; //Sin función
            else
                Funcion = Azar.Next(12); //Alguna de las 11 funciones

            //Constantes
            NumeroA = Azar.NextDouble();
            NumeroB = Azar.NextDouble();

            //Operador
            Operador = Azar.Next(5);

            //Si hay más de una pieza
            if (maxPieza > 0) {
                PiezaA = Azar.Next(maxPieza);
                PiezaB = Azar.Next(maxPieza);
                TipoA = Azar.Next(3); //variable, constante, pieza
                TipoB = Azar.Next(3); //variable, constante, pieza
            }
            else {
                TipoA = Azar.Next(2); //variable, constante
                TipoB = Azar.Next(2); //variable, constante
            }
        }

    }
}