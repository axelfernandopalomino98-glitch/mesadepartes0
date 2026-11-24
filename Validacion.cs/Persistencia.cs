using System;
using System.IO;

namespace MesaDePartes
{
    public static class Persistencia
    {
        public const string ARCHIVO = "expedientes.txt";
        const int CAMPOS_POR_EXPEDIENTE = 15;

        static string Limpiar(string texto)
        {
            return texto.Replace("\r", " ").Replace("\n", " ");
        }

        public static void GuardarExpedientes()
        {
            using (StreamWriter escritor = new StreamWriter(ARCHIVO, false))
            {
                for (int i = 1; i <= Expedientes.cantidad; i++)
                {
                    escritor.WriteLine(Limpiar(Expedientes.codigo[i]));
                    escritor.WriteLine(Limpiar(Expedientes.nombres[i]));
                    escritor.WriteLine(Limpiar(Expedientes.dni[i]));
                    escritor.WriteLine(Limpiar(Expedientes.direccion[i]));
                    escritor.WriteLine(Limpiar(Expedientes.departamento[i]));
                    escritor.WriteLine(Limpiar(Expedientes.provincia[i]));
                    escritor.WriteLine(Limpiar(Expedientes.distrito[i]));
                    escritor.WriteLine(Limpiar(Expedientes.urbanizacion[i]));
                    escritor.WriteLine(Limpiar(Expedientes.correo[i]));
                    escritor.WriteLine(Limpiar(Expedientes.telefono[i]));
                    escritor.WriteLine(Limpiar(Expedientes.informacion[i]));
                    escritor.WriteLine(Limpiar(Expedientes.dependencia[i]));
                    escritor.WriteLine(Limpiar(Expedientes.formaEntrega[i]));
                    escritor.WriteLine(Limpiar(Expedientes.fechaHora[i]));
                    escritor.WriteLine(Limpiar(Expedientes.estado[i]));
                }
            }
        }

        public static void CargarExpedientes()
        {
            if (!File.Exists(ARCHIVO))
                return;

            string[] lineas = File.ReadAllLines(ARCHIVO);
            if (lineas.Length > 0 && Array.Exists(lineas, linea => linea.Contains("|")))
            {
                foreach (string linea in lineas)
                {
                    string[] partes = linea.Split('|');
                    if (partes.Length == CAMPOS_POR_EXPEDIENTE && Expedientes.cantidad < Expedientes.MAXIMO)
                        CargarExpediente(partes);
                }
                return;
            }

            for (int inicio = 0; inicio + CAMPOS_POR_EXPEDIENTE <= lineas.Length && Expedientes.cantidad < Expedientes.MAXIMO; inicio += CAMPOS_POR_EXPEDIENTE)
            {
                string[] partes = new string[CAMPOS_POR_EXPEDIENTE];
                Array.Copy(lineas, inicio, partes, 0, CAMPOS_POR_EXPEDIENTE);
                CargarExpediente(partes);
            }
        }

        static void CargarExpediente(string[] partes)
        {
            Expedientes.cantidad++;
            int i = Expedientes.cantidad;
            Expedientes.codigo[i] = partes[0];
            Expedientes.nombres[i] = partes[1];
            Expedientes.dni[i] = partes[2];
            Expedientes.direccion[i] = partes[3];
            Expedientes.departamento[i] = partes[4];
            Expedientes.provincia[i] = partes[5];
            Expedientes.distrito[i] = partes[6];
            Expedientes.urbanizacion[i] = partes[7];
            Expedientes.correo[i] = partes[8];
            Expedientes.telefono[i] = partes[9];
            Expedientes.informacion[i] = partes[10];
            Expedientes.dependencia[i] = partes[11];
            Expedientes.formaEntrega[i] = partes[12];
            Expedientes.fechaHora[i] = partes[13];
            Expedientes.estado[i] = partes[14];
        }
    }
}
