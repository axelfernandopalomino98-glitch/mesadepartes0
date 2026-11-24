using System;
using System.Runtime.Remoting.Messaging;

namespace MesaDePartes
{
    public static class Validaciones
    {
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