using System;

namespace MesaDePartes
{
    public static class Expedientes
    {
        public const int MAXIMO = 1000;

        public static string[] codigo = new string[MAXIMO + 1];
        public static string[] nombres = new string[MAXIMO + 1];
        public static string[] dni = new string[MAXIMO + 1];
        public static string[] direccion = new string[MAXIMO + 1];
        public static string[] departamento = new string[MAXIMO + 1];
        public static string[] provincia = new string[MAXIMO + 1];
        public static string[] distrito = new string[MAXIMO + 1];
        public static string[] urbanizacion = new string[MAXIMO + 1];
        public static string[] correo = new string[MAXIMO + 1];
        public static string[] telefono = new string[MAXIMO + 1];
        public static string[] informacion = new string[MAXIMO + 1];
        public static string[] dependencia = new string[MAXIMO + 1];
        public static string[] formaEntrega = new string[MAXIMO + 1];
        public static string[] fechaHora = new string[MAXIMO + 1];
        public static string[] estado = new string[MAXIMO + 1];
        public static int cantidad = 0;
        public static string GenerarCodigo(int anioActual)
        {
            int correlativo, anioAnterior;
            string anioTexto, correlativoTexto, codigoNuevo;

            if (cantidad == 0)
            {
                correlativo = 1;
            }
            else
            {
                anioAnterior = Convert.ToInt32(codigo[cantidad].Substring(4, 4));
                if (anioAnterior == anioActual)
                {
                    correlativo = Convert.ToInt32(codigo[cantidad].Substring(9, 5)) + 1;
                }
                else
                {
                    correlativo = 1;
                }
            }
            anioTexto = anioActual.ToString();
            correlativoTexto = correlativo.ToString();
            while (correlativoTexto.Length < 5)
            {
                correlativoTexto = "0" + correlativoTexto;
            }
            codigoNuevo = "EXP-" + anioTexto + "-" + correlativoTexto;
            return codigoNuevo;
        }
        public static string FormatearFechaHora(int fechaNumero, int horaNumero)
        {
            string fechaTexto, horaTexto, resultado;

            fechaTexto = fechaNumero.ToString();
            horaTexto = horaNumero.ToString();
            while (fechaTexto.Length < 8)
            {
                fechaTexto = "0" + fechaTexto;
            }
            while (horaTexto.Length < 6)
            {
                horaTexto = "0" + horaTexto;
            }
            resultado = fechaTexto.Substring(0, 4) + "-" + fechaTexto.Substring(4, 2) + "-" + fechaTexto.Substring(6, 2)
                      + " " + horaTexto.Substring(0, 2) + ":" + horaTexto.Substring(2, 2);
            return resultado;
        }
        public static void MostrarExpediente(int posicion)
        {
            Colores.Texto("Código: " + codigo[posicion]);
            Colores.Texto("Apellidos y nombres: " + nombres[posicion]);
            Colores.Texto("DNI: " + dni[posicion]);
            Colores.Texto("Dirección: " + direccion[posicion]);
            Colores.Texto("Departamento: " + departamento[posicion]);
            Colores.Texto("Provincia: " + provincia[posicion]);
            Colores.Texto("Distrito: " + distrito[posicion]);
            Colores.Texto("Urbanización: " + urbanizacion[posicion]);
            Colores.Texto("Correo: " + correo[posicion]);
            Colores.Texto("Teléfono: " + telefono[posicion]);
            Colores.Texto("Información solicitada: " + informacion[posicion]);
            Colores.Texto("Dependencia: " + dependencia[posicion]);
            Colores.Texto("Forma de entrega: " + formaEntrega[posicion]);
            Colores.Texto("Fecha y hora: " + fechaHora[posicion]);
            Colores.Texto("Estado: " + estado[posicion]);
        }
        public static string BuscarNombrePorDni(string dniBuscado)
        {
            int i;
            string resultado = null;

            for (i = 1; i <= cantidad && resultado == null; i++)
            {
                if (dni[i] == dniBuscado)
                {
                    resultado = nombres[i];
                }
            }
            return resultado;
        }
        public static bool MismoNombre(string nombreA, string nombreB)
        {
            return string.Equals(nombreA.Trim(), nombreB.Trim(), StringComparison.OrdinalIgnoreCase);
        }
    }
}