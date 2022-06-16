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
using System.Collections.Generic;

namespace Colaborar08 {
	class Neurona {
		public List<double> pesos; //Los pesos para cada entrada
		public List<double> nuevospesos; //Nuevos pesos dados por el algoritmo de "backpropagation"
		public double umbral; //El peso del umbral
		public double nuevoumbral; //Nuevo umbral dado por el algoritmo de "backpropagation"

		//Inicializa los pesos y umbral con un valor al azar
		public Neurona(Random azar, int totalEntradas) {
			pesos = new List<double>();
			nuevospesos = new List<double>();
			for (int cont = 0; cont < totalEntradas; cont++) {
				pesos.Add(azar.NextDouble());
				nuevospesos.Add(0);
			}
			umbral = azar.NextDouble();
			nuevoumbral = 0;
		}

		//Calcula la salida de la neurona dependiendo de las entradas
		public double calculaSalida(List<double> entradas) {
			double valor = 0;
			for (int cont = 0; cont < pesos.Count; cont++) {
				valor += entradas[cont] * pesos[cont];
			}
			valor += umbral;
			return 1 / (1 + Math.Exp(-valor));
		}

		//Reemplaza viejos pesos por nuevos
		public void actualiza() {
			for (int cont = 0; cont < pesos.Count; cont++) {
				pesos[cont] = nuevospesos[cont];
			}
			umbral = nuevoumbral;
		}
	}
}
