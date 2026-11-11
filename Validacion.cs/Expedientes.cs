using System;

namespace MesaDePartes
{
    // CLASE EXPEDIENTES
    // Guarda los datos en ARREGLOS PARALELOS, igual que en el PSeInt.
    // El expediente número k está en la posición k de TODOS los arreglos.
    // Los arreglos empiezan en la posición 1 (como en PSeInt), por eso tienen tamaño 1001.
    // Estos arreglos y "cantidad" son variables de la clase (alcance global):
    // todas las demás clases pueden usarlas con el prefijo "Expedientes.".
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

        // Cuántos expedientes hay registrados (PSeInt: cantidad <- 0)
        public static int cantidad = 0;

        // FUNCIÓN CON RETORNO. Recibe el año actual (por valor) y devuelve el código nuevo.
        // Formato EXP-AAAA-NNNNN. Si cambia el año, el correlativo vuelve a 1.
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
                // Substring(4,4) toma el año del último código (PSeInt: Subcadena(...,5,8))
                anioAnterior = Convert.ToInt32(codigo[cantidad].Substring(4, 4));
                if (anioAnterior == anioActual)
                {
                    // Substring(9,5) toma el número (PSeInt: Subcadena(...,10,14))
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

        // FUNCIÓN CON RETORNO. Convierte 20261027 y 153045 en "2026-10-27 15:30".
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

        // FUNCIÓN SIN RETORNO (void). Muestra en pantalla el expediente de la posición indicada.
        public static void MostrarExpediente(int posicion)
        {
            Console.WriteLine("Código: " + codigo[posicion]);
            Console.WriteLine("Apellidos y nombres: " + nombres[posicion]);
            Console.WriteLine("DNI: " + dni[posicion]);
            Console.WriteLine("Dirección: " + direccion[posicion]);
            Console.WriteLine("Departamento: " + departamento[posicion]);
            Console.WriteLine("Provincia: " + provincia[posicion]);
            Console.WriteLine("Distrito: " + distrito[posicion]);
            Console.WriteLine("Urbanización: " + urbanizacion[posicion]);
            Console.WriteLine("Correo: " + correo[posicion]);
            Console.WriteLine("Teléfono: " + telefono[posicion]);
            Console.WriteLine("Información solicitada: " + informacion[posicion]);
            Console.WriteLine("Dependencia: " + dependencia[posicion]);
            Console.WriteLine("Forma de entrega: " + formaEntrega[posicion]);
            Console.WriteLine("Fecha y hora: " + fechaHora[posicion]);
            Console.WriteLine("Estado: " + estado[posicion]);
        }
    }
}