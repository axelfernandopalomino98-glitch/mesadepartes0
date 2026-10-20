using System;

namespace MesaDePartes
{
    public static class Ordenamiento
    {
        public static int[] OrdenarIndices(int campo, bool ascendente)
        {
            int n = Expedientes.Cantidad;
            int[] idx = new int[n];
            for (int i = 0; i < n; i++) idx[i] = i;

            for (int i = 0; i < n - 1; i++)
            {
                int sel = i;
                for (int j = i + 1; j < n; j++)
                {
                    if (Precede(idx[j], idx[sel], campo, ascendente)) sel = j;
                }
                if (sel != i)
                {
                    int tmp = idx[i];
                    idx[i] = idx[sel];
                    idx[sel] = tmp;
                }
            }
            return idx;
        }

        private static bool Precede(int a, int b, int campo, bool ascendente)
        {
            int c = string.Compare(Expedientes.Col[campo][a], Expedientes.Col[campo][b], StringComparison.OrdinalIgnoreCase);
            if (c == 0)
            {
                c = string.CompareOrdinal(Expedientes.Col[Expedientes.COD][a], Expedientes.Col[Expedientes.COD][b]);
            }
            return ascendente ? c < 0 : c > 0;
        }
    }
}
