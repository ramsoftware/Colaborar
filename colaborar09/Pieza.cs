/*Estructura

A: variable operador1 variable
B: variable operador1 variable
C: (variable|pieza   operador1    variable|pieza) operador2 PiezaB
D: (variable|pieza   operador1    variable|pieza) operador2 PiezaC

variable es cualquier entrada
operador | or & and ^ xor https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/operators/boolean-logical-operators
*/

using System;
namespace Colaborar09 {
    internal class Pieza {
		public bool Valor; //Guarda el valor obtenido
		public bool TipoA; //¿Entrada (false) o pieza (true)?
		public int EntradaA; //Para saber cuál entrada es
		public int PiezaA; //Para saber cuál pieza es
		public int OperadorA; // | or 	& and 	^ xor 
		public bool TipoB; //¿Entrada (false) o pieza (true)?
		public int EntradaB; //Para saber cuál entrada es
		public int PiezaB; //Para saber cuál pieza es
		public int OperadorB; // | or 	& and 	^ xor 

		public Pieza(Random Azar, int TotalEntradas, int MaxPieza) {
			TipoA = false;
			EntradaA = Azar.Next(TotalEntradas);
			PiezaA = 0;

			OperadorA = Azar.Next(3); // | or 	& and 	^ xor

			TipoB = false;
			do {
				EntradaB = Azar.Next(TotalEntradas);
			} while (EntradaA == EntradaB);
			PiezaB = 0;

			OperadorB = Azar.Next(3); // | or 	& and 	^ xor

			if (MaxPieza >= 2) {
				if (Azar.NextDouble() < 0.5) TipoA = true;
				PiezaA = Azar.Next(MaxPieza);
				if (Azar.NextDouble() < 0.5) TipoB = true;
				do {
					PiezaB = Azar.Next(MaxPieza);
				} while (PiezaA == PiezaB);
			}
		}

		public void Muta(Random Azar, int TotalEntradas, int MaxPieza) {
            switch (Azar.Next(0, 8)) {
				case 0: OperadorA++; if (OperadorA > 2) OperadorA = 0; break;
				case 1: OperadorB++; if (OperadorB > 2) OperadorB = 0; break;
				case 2: TipoA = !TipoA; break;
				case 3: TipoB = !TipoB; break;
				case 4: EntradaA = Azar.Next(TotalEntradas); break;
				case 5: EntradaB = Azar.Next(TotalEntradas); break;
				case 6: PiezaA = Azar.Next(MaxPieza); break;
				case 7: PiezaB = Azar.Next(MaxPieza); break;
			}
		}

		public void Imprime(int Pieza) {
			Console.Write(Pieza.ToString() + ": (");
			if (TipoA)
				Console.Write("P" + PiezaA.ToString());
			else
				Console.Write("E" + EntradaA.ToString());

            switch (OperadorA) {
				case 0: Console.Write(" & "); break;
				case 1: Console.Write(" | "); break;
				case 2: Console.Write(" ^ "); break;
			}

			if (TipoB)
				Console.Write("P" + PiezaB.ToString());
			else
				Console.Write("E" + EntradaB.ToString());

			Console.Write(")");

			if (Pieza >= 2){
				switch (OperadorB) {
					case 0: Console.Write(" & "); break;
					case 1: Console.Write(" | "); break;
					case 2: Console.Write(" ^ "); break;
				}
				Console.Write("P" + (Pieza - 1).ToString());
			}
			Console.WriteLine(" ");
		}
	}
}
