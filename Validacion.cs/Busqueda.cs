using System;

namespace MesaDePartes
{
    // CLASE BUSQUEDA
    // Búsqueda binaria por código. Funciona porque los códigos se generan en orden creciente
    // (cada código nuevo es mayor que el anterior), así que el arreglo "codigo" está ordenado.
    public static class Busqueda
    {
        // Devuelve la posición del código buscado, o 0 si no existe (igual que en PSeInt).
        public static int BusquedaBinaria(string codigoBuscado)
        {
            int izquierda, derecha, medio, posicion;

            izquierda = 1;
            derecha = Expedientes.cantidad;
            posicion = 0;
            while (izquierda <= derecha && posicion == 0)
            {
                medio = (izquierda + derecha) / 2;
                if (Expedientes.codigo[medio] == codigoBuscado)
                {
                    posicion = medio;
                }
                else
                {
                    // CompareOrdinal da un número negativo si el primer texto es menor que el segundo
                    if (string.CompareOrdinal(Expedientes.codigo[medio], codigoBuscado) < 0)
                    {
                        izquierda = medio + 1;
                    }
                    else
                    {
                        derecha = medio - 1;
                    }
                }
            }
            return posicion;
        }
    }
}