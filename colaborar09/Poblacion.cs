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
    internal class Poblacion {
		//Almacena los individuos de la población
		public Individuo[] Individuos;
		public int MejorIndividuo;

		//Inicializa la población con un número de individuos
		public Poblacion(Random Azar, int NumIndividuos, int NumEntradas, int NumPiezas) {
			Individuos = new Individuo[NumIndividuos];
			for (int cont = 0; cont < NumIndividuos; cont++)
                Individuos[cont] = new Individuo(Azar, NumEntradas, NumPiezas);
			MejorIndividuo = -1;
		}

		public void Proceso(Random Azar, bool[][] Entradas, bool[] SalidaEsperada) {
			Individuo Ganador, Perdedor;

			while (true) {

				//Escoge dos individuos al azar
				int indivA = Azar.Next(Individuos.Length);
				int indivB;
				do {
					indivB = Azar.Next(Individuos.Length);
				} while (indivB == indivA);

				//Evalúa cada individuo con respecto a las entradas y salidas esperadas
				int valorA = 0, valorB = 0;
				for (int registro = 0; registro < Entradas.Length; registro++) {
					if (Individuos[indivA].EvaluaIndividuo(Entradas[registro]) == SalidaEsperada[registro]) valorA++;
					if (Individuos[indivB].EvaluaIndividuo(Entradas[registro]) == SalidaEsperada[registro]) valorB++;
				}

				//Si encontró el individuo que cumple con todo
				if (valorA == Entradas.Length) { MejorIndividuo = indivA; return; }
				if (valorB == Entradas.Length) { MejorIndividuo = indivB; return; }

				//El mejor individuo genera una copia que sobreescribe al peor y la copia se muta
				if (valorA > valorB) {
					Ganador = Individuos[indivA];
					Perdedor = Individuos[indivB];
				}
				else {
					Ganador = Individuos[indivB];
					Perdedor = Individuos[indivA];
				}

				//Copia el individuo
				for (int copia = 0; copia < Ganador.Piezas.Length; copia++) {
					Perdedor.Piezas[copia].TipoA = Ganador.Piezas[copia].TipoA;
					Perdedor.Piezas[copia].EntradaA = Ganador.Piezas[copia].EntradaA;
					Perdedor.Piezas[copia].PiezaA = Ganador.Piezas[copia].PiezaA;
					Perdedor.Piezas[copia].OperadorA = Ganador.Piezas[copia].OperadorA;
					Perdedor.Piezas[copia].TipoB = Ganador.Piezas[copia].TipoB;
					Perdedor.Piezas[copia].EntradaB = Ganador.Piezas[copia].EntradaB;
					Perdedor.Piezas[copia].PiezaB = Ganador.Piezas[copia].PiezaB;
					Perdedor.Piezas[copia].OperadorB = Ganador.Piezas[copia].OperadorB;
				}

				//Muta la copia
				Perdedor.Muta(Azar, Entradas[0].Length);
				Perdedor.Ajuste = -1;
			}
		}

		public int ResultadoMejorIndividuo(bool[][] Entradas, int Registro) {
			if (Individuos[MejorIndividuo].EvaluaIndividuo(Entradas[Registro]))
				return 1;
			return 0;
		}

		public void ImprimeIndividuo(int Individuo) {
			Individuos[Individuo].Imprime();
        }
	}
}
