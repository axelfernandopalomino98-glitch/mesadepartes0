namespace MesaDePartes
{
    public static class Validaciones
    {
        public static bool TextoNoVacio(string t)
        {
            return !string.IsNullOrWhiteSpace(t);
        }

        public static bool EsDigitosExactos(string t, int n)
        {
            if (t == null || t.Length != n) return false;
            for (int i = 0; i < t.Length; i++)
            {
                if (t[i] < '0' || t[i] > '9') return false;
            }
            return true;
        }

        public static bool CorreoValido(string t)
        {
            if (string.IsNullOrWhiteSpace(t) || t.Contains(" ")) return false;
            int arroba = t.IndexOf('@');
            if (arroba < 1 || arroba != t.LastIndexOf('@')) return false;
            int punto = t.LastIndexOf('.');
            return punto > arroba + 1 && punto < t.Length - 1;
        }

        public static bool CodigoValido(string t)
        {
            if (t == null || t.Length != 14) return false;
            if (!t.StartsWith("EXP-") || t[8] != '-') return false;
            return EsDigitosExactos(t.Substring(4, 4), 4) && EsDigitosExactos(t.Substring(9, 5), 5);
        }
    }
}