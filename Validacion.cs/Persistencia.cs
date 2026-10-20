using System;
using System.IO;
using System.Text;

namespace MesaDePartes
{
    public static class Persistencia
    {
        public static string Ruta = "expedientes.txt";

        public static void Cargar(out int cargados, out int omitidas)
        {
            cargados = 0;
            omitidas = 0;
            if (!File.Exists(Ruta)) return;
            try
            {
                string[] lineas = File.ReadAllLines(Ruta, Encoding.UTF8);
                for (int i = 0; i < lineas.Length; i++)
                {
                    if (lineas[i].Trim().Length == 0) continue;
                    string[] f = lineas[i].Split('|');
                    bool ok = f.Length == Expedientes.NUM_CAMPOS
                              && Validaciones.CodigoValido(f[Expedientes.COD])
                              && Expedientes.Cantidad < Expedientes.CAPACIDAD;
                    if (ok && Expedientes.Cantidad > 0)
                    {
                        string ultimo = Expedientes.Col[Expedientes.COD][Expedientes.Cantidad - 1];
                        ok = string.CompareOrdinal(f[Expedientes.COD], ultimo) > 0;
                    }
                    if (ok) { Expedientes.AgregarFila(f); cargados++; }
                    else omitidas++;
                }
            }
            catch (IOException e)
            {
                Console.WriteLine("No se pudo leer el archivo de datos: " + e.Message);
            }
            catch (UnauthorizedAccessException e)
            {
                Console.WriteLine("Sin permiso para leer el archivo de datos: " + e.Message);
            }
        }

        public static bool GuardarUltimo()
        {
            try
            {
                int i = Expedientes.Cantidad - 1;
                string[] campos = new string[Expedientes.NUM_CAMPOS];
                for (int k = 0; k < Expedientes.NUM_CAMPOS; k++) campos[k] = Expedientes.Col[k][i];
                File.AppendAllText(Ruta, string.Join("|", campos) + Environment.NewLine, Encoding.UTF8);
                return true;
            }
            catch (IOException e)
            {
                Console.WriteLine("No se pudo guardar: " + e.Message);
                return false;
            }
            catch (UnauthorizedAccessException e)
            {
                Console.WriteLine("Sin permiso para guardar: " + e.Message);
                return false;
            }
        }
    }
}