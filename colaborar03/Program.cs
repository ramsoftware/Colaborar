using System;

namespace GeneraDataset {
    class MainClass {
        public static void Main(string[] args) {
            Random Azar = new Random();
            GeneradorDatos genera = new GeneradorDatos();
            genera.GeneraEcuacion(Azar, 5, 100);
        }
    }
}
