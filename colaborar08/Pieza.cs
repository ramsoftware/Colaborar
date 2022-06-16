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