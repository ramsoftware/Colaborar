/* Título: Algoritmo genético para interpretar los números de un reloj digital
 * Autor: Rafael Alberto Moreno Parra
 * Fecha: 12 de junio de 2022
 * 
 * Dado los números de un reloj digital, por ejemplo:
 * 
 * |-------|       |       |---------|
 * |       |       |                 |
 * |       |       |                 |
 * |       |       |       |---------|          
 * |       |       |       |
 * |       |       |       |
 * |-------|       |       |---------|
 * 
 * Puede interpretarlos como 0, 1 y 2 respectivamente.
 * Este problema se abordó en mi libro Redes Neuronales en https://github.com/ramsoftware/LibroRedNeuronal2020 (página 84)
 * y fue resuelto usando un percetrón multicapa. 
 * En esta ocasión se usará un algoritmo genético para resolver el mismo problema.
 * 
 * Así se representa el dibujo de cada número de un reloj digital
 *              
            Entradas = new int[][] {
                new int[] { 1, 1, 1, 0, 1, 1, 1 }, // Número 0 
                new int[] { 0, 0, 1, 0, 0, 1, 0 }, // Número 1 
                new int[] { 1, 0, 1, 1, 1, 0, 1 }, // Número 2 
                new int[] { 1, 0, 1, 1, 0, 1, 1 }, // Número 3 
                new int[] { 0, 1, 1, 1, 0, 1, 0 }, // Número 4 
                new int[] { 1, 1, 0, 1, 0, 1, 1 }, // Número 5 
                new int[] { 1, 1, 0, 1, 1, 1, 1 }, // Número 6 
                new int[] { 1, 0, 1, 0, 0, 1, 0 }, // Número 7 
                new int[] { 1, 1, 1, 1, 1, 1, 1 }, // Número 8 
                new int[] { 1, 1, 1, 1, 0, 1, 1 }  // Número 9 
            };

           Valor del número en notación binaria
            SalidasEsperadas = new int[][] {
                new int[]  { 0, 0, 0, 0 },
                new int[]  { 0, 0, 0, 1 },
                new int[]  { 0, 0, 1, 0 },
                new int[]  { 0, 0, 1, 1 },
                new int[]  { 0, 1, 0, 0 },
                new int[]  { 0, 1, 0, 1 },
                new int[]  { 0, 1, 1, 0 },
                new int[]  { 0, 1, 1, 1 },
                new int[]  { 1, 0, 0, 0 },
                new int[]  { 1, 0, 0, 1 }
            };
  
  
  * Se usa colaboración entre distintos individuos. 
  
              I. Para una población 1:
              
              Significa que si se ingresa 1, 1, 1, 0, 1, 1, 1 (primer registro de entradas)
              se obtiene 0 (primer valor del primer registro de salidas Esperadas)
             
              Si se ingresa 0, 0, 1, 0, 0, 1, 0 (segundo registro de entradas)
              se obtiene 0 (primer valor del segundo registro de salidas Esperadas)
              
              Si se ingresa 1, 0, 1, 1, 1, 0, 1 (tercer registro de entradas)
              se obtiene 0 (primer valor del tercer registro de salidas Esperadas)

              y así sucesivamente...
              
              
              II. Para una población 2:
              
              Significa que si se ingresa 1, 1, 1, 0, 1, 1, 1 (primer registro de entradas)
              se obtiene 0 (segundo valor del primer registro de salidas Esperadas)
              
              Si se ingresa 0, 0, 1, 0, 0, 1, 0 (segundo registro de entradas)
              se obtiene 0 (segundo valor del segundo registro de salidas Esperadas)
              
              Si se ingresa 1, 0, 1, 1, 1, 0, 1 (tercer registro de entradas)
              se obtiene 0 (segundo valor del tercer registro de salidas Esperadas)

              y así sucesivamente...

              
              III. Para una población 3:
              
              Significa que si se ingresa 1, 1, 1, 0, 1, 1, 1 (primer registro de entradas)
              se obtiene 0 (tercer valor del primer registro de salidas Esperadas)
              
              Si se ingresa 0, 0, 1, 0, 0, 1, 0 (segundo registro de entradas)
              se obtiene 0 (tercer valor del segundo registro de salidas Esperadas)
              
              Si se ingresa 1, 0, 1, 1, 1, 0, 1 (tercer registro de entradas)
              se obtiene 1 (tercer valor del tercer registro de salidas Esperadas)

              y así sucesivamente...


              IV. Para una población 4:
              
              Significa que si se ingresa 1, 1, 1, 0, 1, 1, 1 (primer registro de entradas)
              se obtiene 0 (cuarto valor del primer registro de salidas Esperadas)
              
              Si se ingresa 0, 0, 1, 0, 0, 1, 0 (segundo registro de entradas)
              se obtiene 1 (cuarto valor del segundo registro de salidas Esperadas)
              
              Si se ingresa 1, 0, 1, 1, 1, 0, 1 (tercer registro de entradas)
              se obtiene 0 (cuarto valor del tercer registro de salidas Esperadas)
   
              y así sucesivamente...

    Son 4 poblaciones, cada población generará un individuo que cumpla con las salidas esperadas. Luego al juntar
    los 4 individuos, se interpreta el reloj digital.
              
*/
using System;

namespace Colaborar09 {
    internal class Individuo {
        //Ajuste del individuo
        public int Ajuste; //El máximo puntaje sería el total de registros

        //Ecuación generada al azar
        public Pieza[] Piezas;

        //Genera la ecuación 
        public Individuo(Random Azar, int TotalEntradas, int NumPiezas) {
            Piezas = new Pieza[NumPiezas];
            for (int cont = 0; cont < NumPiezas; cont++) Piezas[cont] = new Pieza(Azar, TotalEntradas, cont);
            Ajuste = -1;
        }

        public bool EvaluaIndividuo(bool[] Entradas) {
            bool Resultado = false; 

            for (int Posicion = 0; Posicion < Piezas.Length; Posicion++) {
                Pieza tmpPieza = Piezas[Posicion];
                bool numA, numB;

                //¿Con qué entrada trabaja?
                if (tmpPieza.TipoA)
                    numA = Piezas[tmpPieza.PiezaA].Valor;
                else
                    numA = Entradas[tmpPieza.EntradaA];

                //¿Con qué entrada trabaja?
                if (tmpPieza.TipoB)
                    numB = Piezas[tmpPieza.PiezaB].Valor;
                else
                    numB = Entradas[tmpPieza.EntradaB];

                //Operación entre las dos partes
                switch (tmpPieza.OperadorA) {
                    case 0: Resultado = numA & numB; break;
                    case 1: Resultado = numA | numB; break;
                    case 2: Resultado = numA ^ numB; break;
                }

                //La tercera parte es con la pieza anterior
                if (Posicion >= 2) {
                    //Operación con la pieza anterior
                    switch (tmpPieza.OperadorB) {
                        case 0: Resultado &= Piezas[Posicion - 1].Valor; break;
                        case 1: Resultado |= Piezas[Posicion - 1].Valor; break;
                        case 2: Resultado ^= Piezas[Posicion - 1].Valor; break;
                    }
                }

                //Guarda el resultado dentro de la pieza para ser usada en la siguiente pieza
                tmpPieza.Valor = Resultado;
            }
            return Resultado;
        }

        public void Muta(Random Azar, int TotalEntradas) {
            int Posicion = Azar.Next(3, Piezas.Length);
            Piezas[Posicion].Muta(Azar, TotalEntradas, Piezas.Length);
        }

        //Imprime el individuo: las piezas que lo conforman.
        public void Imprime() {
            for (int Posicion = 0; Posicion < Piezas.Length; Posicion++)
                Piezas[Posicion].Imprime(Posicion);
        }
    }
}
