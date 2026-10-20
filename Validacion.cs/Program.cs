using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MesaDePartes
{
    public static class Program
    {
        private const string FUNCIONARIO = "(funcionario responsable por definir)";

        public static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.OutputEncoding = Encoding.UTF8;
            if (args.Length > 0 && args[0] == "--pruebas")
                if (args.Length > 0 && args[0] == "--pruebas")
            {
                Pruebas.Ejecutar();
                return;
            }

            int cargados, omitidas;
            Persistencia.Cargar(out cargados, out omitidas);
            Console.WriteLine("Expedientes cargados desde archivo: " + cargados
                              + (omitidas > 0 ? " (líneas ignoradas: " + omitidas + ")" : ""));

            try
            {
                string op;
                do
                {
                    Console.WriteLine();
                    Console.WriteLine("MESA DE PARTES DIGITAL - MUNICIPALIDAD DE COMAS");
                    Console.WriteLine("1. Presentar una solicitud");
                    Console.WriteLine("2. Consultar expediente por código");
                    Console.WriteLine("3. Ver todos mis expedientes por DNI");
                    Console.WriteLine("4. Listar expedientes ordenados");
                    Console.WriteLine("5. Salir");
                    Console.Write("Seleccione una opción: ");
                    op = Leer();
                    switch (op)
                    {
                        case "1": RegistrarSolicitud(); break;
                        case "2": ConsultarPorCodigo(); break;
                        case "3": ConsultarPorDni(); break;
                        case "4": ListarOrdenado(); break;
                        case "5": Console.WriteLine("Gracias por utilizar la Mesa de Partes Digital."); break;
                        default: Console.WriteLine("Opción no válida. Seleccione del 1 al 5."); break;
                    }
                } while (op != "5");
            }
            catch (EndOfStreamException)
            {
                Console.WriteLine("Entrada finalizada.");
            }
        }

        private static string Leer()
        {
            string s = Console.ReadLine();
            if (s == null) throw new EndOfStreamException();
            return s.Trim();
        }

        private static string LeerTextoNoVacio(string mensaje)
        {
            string v;
            do
            {
                Console.Write(mensaje);
                v = Leer();
                if (!Validaciones.TextoNoVacio(v)) Console.WriteLine("Este campo es obligatorio.");
            } while (!Validaciones.TextoNoVacio(v));
            return v;
        }

        private static string LeerDigitos(string mensaje, int n)
        {
            string v;
            bool ok;
            do
            {
                Console.Write(mensaje);
                v = Leer();
                ok = Validaciones.EsDigitosExactos(v, n);
                if (!ok) Console.WriteLine("Debe tener exactamente " + n + " dígitos.");
            } while (!ok);
            return v;
        }

        private static string LeerCorreo()
        {
            string v;
            bool ok;
            do
            {
                Console.Write("Correo electrónico: ");
                v = Leer();
                ok = Validaciones.CorreoValido(v);
                if (!ok) Console.WriteLine("Correo no válido (ej.: nombre@dominio.com).");
            } while (!ok);
            return v;
        }

        private static string LeerFormaEntrega()
        {
            string[] formas = { "Copia simple", "Disquete", "CD", "Correo electrónico", "Otro" };
            Console.WriteLine("Forma de entrega:");
            for (int i = 0; i < formas.Length; i++) Console.WriteLine((i + 1) + ". " + formas[i]);
            string op;
            do
            {
                Console.Write("Seleccione una opción del 1 al 5: ");
                op = Leer();
                if (op != "1" && op != "2" && op != "3" && op != "4" && op != "5")
                    Console.WriteLine("Opción no válida.");
            } while (op != "1" && op != "2" && op != "3" && op != "4" && op != "5");
            int k = int.Parse(op);
            if (k == 5) return "Otro: " + LeerTextoNoVacio("Especifique la forma de entrega: ");
            return formas[k - 1];
        }

        private static void RegistrarSolicitud()
        {
            if (Expedientes.Cantidad >= Expedientes.CAPACIDAD)
            {
                Console.WriteLine("Capacidad máxima alcanzada (" + Expedientes.CAPACIDAD + " expedientes).");
                return;
            }
            string[] d = new string[Expedientes.NUM_CAMPOS];
            Console.WriteLine("Funcionario responsable: " + FUNCIONARIO);
            Console.WriteLine("II. Datos del solicitante");
            d[Expedientes.NOMBRES] = LeerTextoNoVacio("Apellidos y nombres: ");
            string dni = LeerDigitos("DNI (8 dígitos): ", 8);
            List<int> previos = Busqueda.LinealPorDni(dni);
            if (previos.Count > 0 && !string.Equals(Expedientes.Col[Expedientes.NOMBRES][previos[0]],
                d[Expedientes.NOMBRES], StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine("Ese DNI ya está registrado a nombre de: "
                    + Expedientes.Col[Expedientes.NOMBRES][previos[0]]);
                Console.WriteLine("Verifique el DNI o el nombre. Registro cancelado.");
                return;
            }
            d[Expedientes.DNI] = dni;
            d[Expedientes.DIRECCION] = LeerTextoNoVacio("Av./Calle/Jr./Psj.: ");
            d[Expedientes.DEPARTAMENTO] = LeerTextoNoVacio("Departamento: ");
            d[Expedientes.PROVINCIA] = LeerTextoNoVacio("Provincia: ");
            d[Expedientes.DISTRITO] = LeerTextoNoVacio("Distrito: ");
            d[Expedientes.URBANIZACION] = LeerTextoNoVacio("Urbanización: ");
            d[Expedientes.CORREO] = LeerCorreo();
            d[Expedientes.TELEFONO] = LeerDigitos("Teléfono (9 dígitos): ", 9);
            Console.WriteLine("III. Información solicitada");
            d[Expedientes.INFORMACION] = LeerTextoNoVacio("Información solicitada: ");
            Console.WriteLine("IV. Dependencia");
            d[Expedientes.DEPENDENCIA] = LeerTextoNoVacio("Dependencia: ");
            Console.WriteLine("V. Entrega");
            d[Expedientes.FORMA_ENTREGA] = LeerFormaEntrega();

            string codigo = Expedientes.Registrar(d, DateTime.Now);
            int pos = Expedientes.Cantidad - 1;
            bool guardado = Persistencia.GuardarUltimo();

            Console.WriteLine();
            Console.WriteLine("===== CARGO DE RECEPCIÓN =====");
            Console.WriteLine("Número de registro: " + codigo);
            Console.WriteLine("Fecha y hora: " + Expedientes.Col[Expedientes.FECHA_HORA][pos]);
            Console.WriteLine("Solicitante: " + Expedientes.Col[Expedientes.NOMBRES][pos]);
            Console.WriteLine("Estado: " + Expedientes.Col[Expedientes.ESTADO][pos]);
            Console.WriteLine("(Simulación) Se enviaría una confirmación a " + Expedientes.Col[Expedientes.CORREO][pos]);
            if (!guardado) Console.WriteLine("ADVERTENCIA: el expediente quedó solo en memoria.");
        }

        private static void ConsultarPorCodigo()
        {
            string codigo;
            do
            {
                Console.Write("Ingrese el código (EXP-AAAA-NNNNN): ");
                codigo = Leer().ToUpper();
                if (!Validaciones.CodigoValido(codigo)) Console.WriteLine("Formato incorrecto. Ejemplo: EXP-2026-00001.");
            } while (!Validaciones.CodigoValido(codigo));

            int pos = Busqueda.BinariaPorCodigo(codigo);
            if (pos < 0) Console.WriteLine("Expediente no encontrado.");
            else MostrarExpediente(pos);
        }

        private static void ConsultarPorDni()
        {
            string dni = LeerDigitos("Ingrese su DNI (8 dígitos): ", 8);
            List<int> pos = Busqueda.LinealPorDni(dni);
            if (pos.Count == 0) { Console.WriteLine("No hay expedientes asociados a ese DNI."); return; }
            Console.WriteLine("Expedientes encontrados: " + pos.Count);
            for (int i = 0; i < pos.Count; i++) MostrarExpediente(pos[i]);
        }

        private static void ListarOrdenado()
        {
            if (Expedientes.Cantidad == 0) { Console.WriteLine("No hay expedientes registrados."); return; }
            Console.WriteLine("Ordenar por: 1. Código  2. Fecha y hora  3. Solicitante");
            Console.Write("Criterio: ");
            string c = Leer();
            int campo = c == "2" ? Expedientes.FECHA_HORA : (c == "3" ? Expedientes.NOMBRES : Expedientes.COD);
            Console.Write("Sentido (A = ascendente, D = descendente): ");
            bool asc = Leer().ToUpper() != "D";

            int[] orden = Ordenamiento.OrdenarIndices(campo, asc);
            for (int i = 0; i < orden.Length; i++)
            {
                int p = orden[i];
                Console.WriteLine(Expedientes.Col[Expedientes.COD][p] + " | "
                    + Expedientes.Col[Expedientes.FECHA_HORA][p] + " | "
                    + Expedientes.Col[Expedientes.NOMBRES][p] + " | "
                    + Expedientes.Col[Expedientes.ESTADO][p]);
            }
        }

        private static void MostrarExpediente(int pos)
        {
            Console.WriteLine("----------------------------------------");
            for (int k = 0; k < Expedientes.NUM_CAMPOS; k++)
                Console.WriteLine(Expedientes.Etiquetas[k] + ": " + Expedientes.Col[k][pos]);
        }
    }
}