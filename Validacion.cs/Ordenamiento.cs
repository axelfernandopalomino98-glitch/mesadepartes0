using System;

namespace MesaDePartes
{
    public static class Ordenamiento
    {
        public static void OrdenarPorNombres(int[] orden)
        {
            int i, j, auxiliar;
            for (i = 1; i <= Expedientes.cantidad; i++)
            {
                orden[i] = i;
            }
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