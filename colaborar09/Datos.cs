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
    internal class Datos {
        public int[][] Entradas;
        public int[][] SalidasEsperadas;

        public bool[][] EntradasBinarias;
        public bool[] SalidasEsperadasBinarias;

        public Datos() {
            /* https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/arrays/jagged-arrays */            
            /* Dibujo de los números en un reloj digital */
            Entradas = new int[][] {
                new int[] { 1, 1, 1, 0, 1, 1, 1 }, /* Número 0 */
                new int[] { 0, 0, 1, 0, 0, 1, 0 }, /* Número 1 */
                new int[] { 1, 0, 1, 1, 1, 0, 1 }, /* Número 2 */
                new int[] { 1, 0, 1, 1, 0, 1, 1 }, /* Número 3 */
                new int[] { 0, 1, 1, 1, 0, 1, 0 }, /* Número 4 */
                new int[] { 1, 1, 0, 1, 0, 1, 1 }, /* Número 5 */
                new int[] { 1, 1, 0, 1, 1, 1, 1 }, /* Número 6 */
                new int[] { 1, 0, 1, 0, 0, 1, 0 }, /* Número 7 */
                new int[] { 1, 1, 1, 1, 1, 1, 1 }, /* Número 8 */
                new int[] { 1, 1, 1, 1, 0, 1, 1 }  /* Número 9 */
            };

            /* Valor del número en notación binaria. Cada columna es un ambiente donde trabaja una poblaciòn en particular */
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
        }

        /* Imprime los valores de los datos de entrada y salida */
        public void ImprimeRegistro(int Registro) {
            for (int Entra = 0; Entra < Entradas[Registro].Length; Entra++)
                Console.Write(Entradas[Registro][Entra] + ",");
            Console.Write("\t\t\t");
            for (int Sale = 0; Sale < SalidasEsperadas[Registro].Length; Sale++)
                Console.Write(SalidasEsperadas[Registro][Sale] + ",");
        }

        /* Con cual población va a trabajar */
        public void SeleccionaPoblacion(int Poblacion) {

            /* Convierte los valores enteros donde la población va a trabajar en valores binarios */
            EntradasBinarias = new bool[Entradas.Length][];
            SalidasEsperadasBinarias = new bool[Entradas.Length];

            for (int Registro = 0; Registro < Entradas.Length; Registro++) {
                EntradasBinarias[Registro] = new bool[Entradas[Registro].Length];
                SalidasEsperadasBinarias[Registro] = SalidasEsperadas[Registro][Poblacion] == 1 ? true : false;
                for (int Entrada = 0; Entrada < Entradas[Registro].Length; Entrada++) {
                    EntradasBinarias[Registro][Entrada] = Entradas[Registro][Entrada] == 1 ? true : false;
                }
            }
        }
    }
}
