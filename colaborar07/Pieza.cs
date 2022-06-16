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

namespace Colaborar07 {
	public class Pieza {
		public double ValorPieza; /* Almacena el valor que genera la pieza al evaluarse */
		public int Funcion; /* Código de la función 0:seno, 1:coseno, 2:tangente, 3: valor absoluto, 4: arcoseno, 5: arcocoseno, 6: arcotangente, 7: logaritmo natural, 8: exponencial, 9: raíz cuadrada */
		public double coeficienteA; /* El que multiplica la variable de la primera parte */
		public bool VariableA; /* La primera parte es una variable */
		public int OperadorA; /* + suma - resta * multiplicación / división */
		public int TipoB; /* La segunda parte es un número o una variable o trae el valor de otra pieza */
		public double NumeroB; /* Es un número literal */
		public int PiezaB; /* Trae el valor de otra pieza */
		public double coeficienteB; /* El que multiplica la variable de la segunda parte */
		public bool VariableB; /* La segunda parte puede ser una variable */
		public int OperadorB; /* Conecta con la pieza anterior */

		public Pieza(Random Azar, int NumeroPieza) {
			ValorPieza = 0; //Inicializa en cero
			
			//Si habrá una función que se use para esta pieza
			if (Azar.NextDouble() < 0.5)
				Funcion = -1; //Sin función
			else
				Funcion = Azar.Next(10); //Alguna de las 9 funciones

			//Variable
			coeficienteA = Azar.NextDouble();
			VariableA = Azar.NextDouble() < 0.5 ? true: false;
			
			//Operador
			OperadorA = Azar.Next(4);

			//Si hay más de una pieza
			if (NumeroPieza > 0) {
				TipoB = Azar.Next(3); //variable, constante, pieza
				PiezaB = Azar.Next(NumeroPieza);
			}
			else {
				TipoB = Azar.Next(2); //variable, constante
			}

			//Constante
			NumeroB = Azar.NextDouble();

			//VariableB
			coeficienteB = Azar.NextDouble();
			VariableB = Azar.NextDouble() < 0.5 ? true : false;

			//Operador
			OperadorB = Azar.Next(4);
		}
	}
}