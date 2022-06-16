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
                new int[]  { 0, 0, 0, 0 }, //0 
                new int[]  { 0, 0, 0, 1 }, //1
                new int[]  { 0, 0, 1, 0 }, //2
                new int[]  { 0, 0, 1, 1 }, //3
                new int[]  { 0, 1, 0, 0 }, //4
                new int[]  { 0, 1, 0, 1 }, //5
                new int[]  { 0, 1, 1, 0 }, //6
                new int[]  { 0, 1, 1, 1 }, //7
                new int[]  { 1, 0, 0, 0 }, //8
                new int[]  { 1, 0, 0, 1 }  //9
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
    internal class Program {
        static void Main(string[] args) {
            
            /* Generador único de números aleatorios */
            Random Azar = new Random();
            
            /* Los datos de entrada (dibujo) y salida (representación binaria) del problema de reloj digital */
            Datos ConjuntoDatos = new Datos();

            /* Las 4 poblaciones */
            Poblacion[] Sociedad = new Poblacion[4];

            /* Va de población en población, dando origen al mejor individuo */
            for (int Contador = 0; Contador <= 3; Contador++) {
                ConjuntoDatos.SeleccionaPoblacion(Contador);
                Sociedad[Contador] = new Poblacion(Azar, 100, ConjuntoDatos.EntradasBinarias[0].Length, 20);
                Sociedad[Contador].Proceso(Azar, ConjuntoDatos.EntradasBinarias, ConjuntoDatos.SalidasEsperadasBinarias);
                //Sociedad[Contador].ImprimeIndividuo(MejorIndividuo[Contador]);
            }

            /* Ahora muestra el resultado de la colaboración */
            Console.WriteLine("Dibujo del número\t\tSalida Esperada\t\tSalida Algoritmo Genético");
            for (int Registro = 0; Registro < ConjuntoDatos.Entradas.Length; Registro++) {
                /* Imprime las entradas y salida esperada */
                ConjuntoDatos.ImprimeRegistro(Registro);

                /* Imprime el resultado cada uno de los 4 individuos en horizontal y al colaborarse entre sí,
                   dan con la salida esperada. */
                Console.Write("\t\t");
                for (int Contador = 0; Contador <= 3; Contador++) {
                    ConjuntoDatos.SeleccionaPoblacion(Contador);
                    int Resultado = Sociedad[Contador].ResultadoMejorIndividuo(ConjuntoDatos.EntradasBinarias, Registro);
                    Console.Write(Resultado + ",");
                }
                Console.WriteLine(" ");
            }

            Console.ReadKey();
        }
    }
}
