/* Autor: Rafael Alberto Moreno Parra
 * Fecha: 03 de abril de 2022
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

namespace Colaborar06 {
    internal class Program {
        static void Main(string[] args) {
            Random azar = new Random();

            int numPiezas = 10; //Cuantas piezas tendrá esa expresión
            GeneradorDatos datos = new GeneradorDatos();
            datos.GeneraEcuacion(azar, numPiezas);
        }
    }
}
