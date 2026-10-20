using System;
using System.Collections.Generic;

namespace MesaDePartes
{
    public static class Expedientes
    {
        public const int COD = 0, NOMBRES = 1, DNI = 2, DIRECCION = 3, DEPARTAMENTO = 4,
                         PROVINCIA = 5, DISTRITO = 6, URBANIZACION = 7, CORREO = 8,
                         TELEFONO = 9, INFORMACION = 10, DEPENDENCIA = 11,
                         FORMA_ENTREGA = 12, FECHA_HORA = 13, ESTADO = 14;
        public const int NUM_CAMPOS = 15;
        public const int CAPACIDAD = 1000;

        public static readonly string[] Etiquetas =
        {
            "Código", "Solicitante", "DNI", "Dirección", "Departamento", "Provincia",
            "Distrito", "Urbanización", "Correo", "Teléfono", "Información solicitada",
            "Dependencia", "Forma de entrega", "Fecha y hora", "Estado"
        };

        public static List<string>[] Col = CrearColumnas();

        public static int Cantidad
        {
            get { return Col[COD].Count; }
        }

        private static List<string>[] CrearColumnas()
        {
            List<string>[] c = new List<string>[NUM_CAMPOS];
            for (int i = 0; i < NUM_CAMPOS; i++) c[i] = new List<string>();
            return c;
        }

        public static void Reiniciar()
        {
            Col = CrearColumnas();
        }

        public static void AgregarFila(string[] f)
        {
            for (int i = 0; i < NUM_CAMPOS; i++) Col[i].Add(f[i]);
        }

        public static string Sanear(string t)
        {
            return t.Replace("|", "/").Trim();
        }

        public static string GenerarCodigo(DateTime ahora)
        {
            int anio = ahora.Year;
            int correlativo = 1;
            if (Cantidad > 0)
            {
                string ultimo = Col[COD][Cantidad - 1];
                int anioAnterior = int.Parse(ultimo.Substring(4, 4));
                if (anioAnterior == anio)
                {
                    correlativo = int.Parse(ultimo.Substring(9, 5)) + 1;
                }
            }
            return "EXP-" + anio + "-" + correlativo.ToString().PadLeft(5, '0');
        }

        public static string FormatearFechaHora(DateTime ahora)
        {
            return ahora.ToString("yyyy-MM-dd HH:mm:ss");
        }

        public static string Registrar(string[] d, DateTime ahora)
        {
            if (Cantidad >= CAPACIDAD) return null;
            string[] fila = new string[NUM_CAMPOS];
            for (int i = 0; i < NUM_CAMPOS; i++) fila[i] = Sanear(d[i] ?? "");
            fila[COD] = GenerarCodigo(ahora);
            fila[FECHA_HORA] = FormatearFechaHora(ahora);
            fila[ESTADO] = "Pendiente";
            AgregarFila(fila);
            return fila[COD];
        }
    }
}