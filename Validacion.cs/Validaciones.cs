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

            string usuario = t.Substring(0, arroba);
            string dominio = t.Substring(arroba + 1);

            // Nombre (antes de @): letras, dígitos y . _ - +
            for (int i = 0; i < usuario.Length; i++)
            {
                char c = usuario[i];
                bool valido = (c >= 'a' && c <= 'z') || (c >= 'A' && c <= 'Z') || (c >= '0' && c <= '9')
                              || c == '.' || c == '_' || c == '-' || c == '+';
                if (!valido) return false;
            }
            if (usuario[0] == '.' || usuario[usuario.Length - 1] == '.') return false;

            // Dominio (después de @): debe tener al menos un punto y cada parte solo letras, dígitos o guion
            int punto = dominio.LastIndexOf('.');
            if (punto < 1 || punto == dominio.Length - 1) return false;
            string[] etiquetas = dominio.Split('.');
            for (int e = 0; e < etiquetas.Length; e++)
            {
                string et = etiquetas[e];
                if (et.Length == 0) return false;
                for (int i = 0; i < et.Length; i++)
                {
                    char c = et[i];
                    bool valido = (c >= 'a' && c <= 'z') || (c >= 'A' && c <= 'Z') || (c >= '0' && c <= '9') || c == '-';
                    if (!valido) return false;
                }
            }
            return true;
        }

        public static bool CodigoValido(string t)
        {
            if (t == null || t.Length != 14) return false;
            if (!t.StartsWith("EXP-") || t[8] != '-') return false;
            return EsDigitosExactos(t.Substring(4, 4), 4) && EsDigitosExactos(t.Substring(9, 5), 5);
        }
    }
}