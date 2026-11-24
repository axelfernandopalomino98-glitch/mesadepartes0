using System;

namespace MesaDePartes
{
    public static class Busqueda
    {
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