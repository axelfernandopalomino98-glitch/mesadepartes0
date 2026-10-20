using System;
using System.Collections.Generic;

namespace MesaDePartes
{
    public static class Busqueda
    {
        public static int BinariaPorCodigo(string codigoBuscado)
        {
            int izq = 0, der = Expedientes.Cantidad - 1;
            while (izq <= der)
            {
                int medio = (izq + der) / 2;
                int c = string.CompareOrdinal(Expedientes.Col[Expedientes.COD][medio], codigoBuscado);
                if (c == 0) return medio;
                if (c < 0) izq = medio + 1;
                else der = medio - 1;
            }
            return -1;
        }

        public static List<int> LinealPorDni(string dniBuscado)
        {
            List<int> pos = new List<int>();
            for (int i = 0; i < Expedientes.Cantidad; i++)
            {
                if (Expedientes.Col[Expedientes.DNI][i] == dniBuscado) pos.Add(i);
            }
            return pos;
        }
    }
}