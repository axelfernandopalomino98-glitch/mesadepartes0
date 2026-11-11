using System;

namespace MesaDePartes
{
    // CLASE ORDENAMIENTO
    // Ordena los expedientes por apellidos y nombres con el MÉTODO DE LA BURBUJA.
    // No mueve los 15 arreglos: ordena un arreglo auxiliar "orden" que guarda POSICIONES.
    // Así el arreglo "codigo" sigue en orden y la búsqueda binaria sigue funcionando.
    public static class Ordenamiento
    {
        // El arreglo "orden" se recibe como parámetro: lo que se cambia aquí se ve afuera.
        public static void OrdenarPorNombres(int[] orden)
        {
            int i, j, auxiliar;

            // Al inicio orden[1]=1, orden[2]=2, ... (sin ordenar)
            for (i = 1; i <= Expedientes.cantidad; i++)
            {
                orden[i] = i;
            }
            // Burbuja: compara cada pareja vecina y las intercambia si están al revés
            for (i = 1; i <= Expedientes.cantidad - 1; i++)
            {
                for (j = 1; j <= Expedientes.cantidad - i; j++)
                {
                    if (string.Compare(Expedientes.nombres[orden[j]], Expedientes.nombres[orden[j + 1]], StringComparison.OrdinalIgnoreCase) > 0)
                    {
                        auxiliar = orden[j];
                        orden[j] = orden[j + 1];
                        orden[j + 1] = auxiliar;
                    }
                }
            }
        }
    }
}