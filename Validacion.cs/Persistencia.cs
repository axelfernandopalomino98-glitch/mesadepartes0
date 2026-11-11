using System;
using System.IO;

namespace MesaDePartes
{
    // CLASE PERSISTENCIA
    // Guarda y lee los expedientes en un ARCHIVO DE TEXTO (expedientes.txt).
    // Una línea por expediente, con los 15 datos separados por el símbolo |
    // Se eligió texto y no binario porque se puede abrir con el Bloc de notas para revisarlo.
    public static class Persistencia
    {
        public const string ARCHIVO = "expedientes.txt";

        // Si un dato trae el símbolo |, se cambia por / para no romper la línea.
        static string Limpiar(string texto)
        {
            return texto.Replace("|", "/");
        }

        // Escribe TODOS los expedientes en el archivo (reemplaza el contenido anterior).
        public static void GuardarExpedientes()
        {
            int i;
            StreamWriter escritor;

            escritor = new StreamWriter(ARCHIVO, false);
            for (i = 1; i <= Expedientes.cantidad; i++)
            {
                escritor.WriteLine(
                    Limpiar(Expedientes.codigo[i]) + "|" +
                    Limpiar(Expedientes.nombres[i]) + "|" +
                    Limpiar(Expedientes.dni[i]) + "|" +
                    Limpiar(Expedientes.direccion[i]) + "|" +
                    Limpiar(Expedientes.departamento[i]) + "|" +
                    Limpiar(Expedientes.provincia[i]) + "|" +
                    Limpiar(Expedientes.distrito[i]) + "|" +
                    Limpiar(Expedientes.urbanizacion[i]) + "|" +
                    Limpiar(Expedientes.correo[i]) + "|" +
                    Limpiar(Expedientes.telefono[i]) + "|" +
                    Limpiar(Expedientes.informacion[i]) + "|" +
                    Limpiar(Expedientes.dependencia[i]) + "|" +
                    Limpiar(Expedientes.formaEntrega[i]) + "|" +
                    Limpiar(Expedientes.fechaHora[i]) + "|" +
                    Limpiar(Expedientes.estado[i]));
            }
            escritor.Close();
        }

        // Lee el archivo al iniciar el programa y llena los arreglos paralelos.
        // File.ReadAllLines devuelve todas las líneas del archivo en un arreglo de textos,
        // y FOREACH recorre ese arreglo línea por línea.
        // Si el archivo no existe (primera vez), no hace nada.
        public static void CargarExpedientes()
        {
            string[] lineas;
            string[] partes;

            if (File.Exists(ARCHIVO))
            {
                lineas = File.ReadAllLines(ARCHIVO);
                foreach (string linea in lineas)
                {
                    // Split separa la línea en pedazos cada vez que encuentra el símbolo |
                    partes = linea.Split('|');
                    if (partes.Length == 15 && Expedientes.cantidad < Expedientes.MAXIMO)
                    {
                        Expedientes.cantidad = Expedientes.cantidad + 1;
                        Expedientes.codigo[Expedientes.cantidad] = partes[0];
                        Expedientes.nombres[Expedientes.cantidad] = partes[1];
                        Expedientes.dni[Expedientes.cantidad] = partes[2];
                        Expedientes.direccion[Expedientes.cantidad] = partes[3];
                        Expedientes.departamento[Expedientes.cantidad] = partes[4];
                        Expedientes.provincia[Expedientes.cantidad] = partes[5];
                        Expedientes.distrito[Expedientes.cantidad] = partes[6];
                        Expedientes.urbanizacion[Expedientes.cantidad] = partes[7];
                        Expedientes.correo[Expedientes.cantidad] = partes[8];
                        Expedientes.telefono[Expedientes.cantidad] = partes[9];
                        Expedientes.informacion[Expedientes.cantidad] = partes[10];
                        Expedientes.dependencia[Expedientes.cantidad] = partes[11];
                        Expedientes.formaEntrega[Expedientes.cantidad] = partes[12];
                        Expedientes.fechaHora[Expedientes.cantidad] = partes[13];
                        Expedientes.estado[Expedientes.cantidad] = partes[14];
                    }
                }
            }
        }
    }
}