using System;
using System.Runtime.Remoting.Messaging;

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
        // Verdadero si el DNI tiene exactamente 8 digitos y no es 00000000
        public static bool DniValido(string texto)
        {
            bool resultado;
            resultado = EsDigitosExactos(texto, 8);
            if (texto == "00000000")
            {
                resultado = false;
            }
            return resultado;
        }

        // Verdadero si el telefono tiene exactamente 9 digitos y empieza con 9.
        // Texto[0] es el primer caracter. Solo se revisa si ya tiene 9 digitos, asi nunca esta vacio.

        public static bool TelefonoValido(string texto)
        {
            bool resultado;
            resultado = EsDigitosExactos(texto, 9);
            if (resultado && texto[0] != '9')
            {
                resultado = false;
            }
            return resultado;
        }

        // Verdadero si el correo tiene un nombre, exactamente un arroba y al menos un punto despues de ella.
        public static bool CorreoValido(string texto)
        {
            int cantidadArrobas;
            bool tienePunto;
            int posicion;

            cantidadArrobas = 0;
            tienePunto = false;
            posicion = 0;
            foreach (char caracter in texto)
            {
                if (caracter == '@')
                {
                    cantidadArrobas++;
                    if (posicion == 0)
                    {
                        return false; 
                    }
                }
                if (caracter == '.' && cantidadArrobas == 1)
                {
                    tienePunto = true;
                }
                posicion++;
            }
            return cantidadArrobas == 1 && tienePunto;
        }
        // Devuelve cuantas arrobas tiene el texto.
        public static int ContarArrobas(string texto)
        {
            int cantidad;

            cantidad = 0;
            foreach (char caracter in texto)
            {
                if (caracter == '@')
                {
                    cantidad++;
                }
            }
            return cantidad;
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