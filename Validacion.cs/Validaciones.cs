using System;

namespace MesaDePartes
{
    // CLASE VALIDACIONES
    // Revisa los datos CARÁCTER POR CARÁCTER (cadenas de caracteres) antes de guardarlos.
    // Cada función recibe un texto y devuelve verdadero o falso.
    // Se usa FOREACH para recorrer cada carácter del texto sin necesitar un contador:
    // "foreach (char caracter in texto)" significa "para cada carácter del texto".
    public static class Validaciones
    {
        // Verdadero si el texto tiene exactamente "largoEsperado" caracteres y todos son dígitos.
        // Se usa con 8 para el DNI y con 9 para el teléfono.
        public static bool EsDigitosExactos(string texto, int largoEsperado)
        {
            bool resultado;

            if (texto.Length == largoEsperado)
            {
                resultado = true;
                foreach (char caracter in texto)
                {
                    if (caracter < '0' || caracter > '9')
                    {
                        resultado = false;
                    }
                }
            }
            else
            {
                resultado = false;
            }
            return resultado;
        }

        // Verdadero si el correo tiene al menos una arroba y al menos un punto.
        public static bool CorreoValido(string texto)
        {
            bool tieneArroba, tienePunto;

            tieneArroba = false;
            tienePunto = false;
            foreach (char caracter in texto)
            {
                if (caracter == '@')
                {
                    tieneArroba = true;
                }
                if (caracter == '.')
                {
                    tienePunto = true;
                }
            }
            return tieneArroba && tienePunto;
        }

        // Verdadero si el texto tiene el formato EXP-AAAA-NNNNN (14 caracteres).
        // Substring(4, 4) toma el año y Substring(9, 5) toma el número.
        public static bool CodigoValido(string texto)
        {
            bool resultado;
            string anio, numero;

            resultado = false;
            if (texto.Length == 14)
            {
                if (texto.Substring(0, 3) == "EXP" && texto[3] == '-' && texto[8] == '-')
                {
                    resultado = true;
                    anio = texto.Substring(4, 4);
                    numero = texto.Substring(9, 5);
                    foreach (char caracter in anio)
                    {
                        if (caracter < '0' || caracter > '9')
                        {
                            resultado = false;
                        }
                    }
                    foreach (char caracter in numero)
                    {
                        if (caracter < '0' || caracter > '9')
                        {
                            resultado = false;
                        }
                    }
                }
            }
            return resultado;
        }
    }
}