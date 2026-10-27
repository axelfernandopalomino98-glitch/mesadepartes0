using System;

namespace MesaDePartes
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            int cantidad, op;
            string[] codigo = new string[1001];
            string[] nombres = new string[1001];
            string[] dni = new string[1001];
            string[] direccion = new string[1001];
            string[] departamento = new string[1001];
            string[] provincia = new string[1001];
            string[] distrito = new string[1001];
            string[] urbanizacion = new string[1001];
            string[] correo = new string[1001];
            string[] telefono = new string[1001];
            string[] informacion = new string[1001];
            string[] dependencia = new string[1001];
            string[] formaEntrega = new string[1001];
            string[] fechaHora = new string[1001];
            string[] estado = new string[1001];

            cantidad = 0;
            do
            {
                Console.WriteLine("");
                Console.WriteLine("MESA DE PARTES DIGITAL - MUNICIPALIDAD DE COMAS");
                Console.WriteLine("1. Presentar una solicitud");
                Console.WriteLine("2. Consultar mi expediente por código");
                Console.WriteLine("3. Ver todos mis expedientes por DNI");
                Console.WriteLine("4. Salir");
                Console.WriteLine("Seleccione una opción:");
                if (!int.TryParse(Console.ReadLine(), out op)) op = 0;
                switch (op)
                {
                    case 1:
                        RegistrarSolicitud(codigo, nombres, dni, direccion, departamento, provincia, distrito, urbanizacion, correo, telefono, informacion, dependencia, formaEntrega, fechaHora, estado, ref cantidad);
                        break;
                    case 2:
                        ConsultarExpediente(codigo, nombres, dni, direccion, departamento, provincia, distrito, urbanizacion, correo, telefono, informacion, dependencia, formaEntrega, fechaHora, estado, cantidad);
                        break;
                    case 3:
                        VerExpedientesPorDni(codigo, nombres, dni, direccion, departamento, provincia, distrito, urbanizacion, correo, telefono, informacion, dependencia, formaEntrega, fechaHora, estado, cantidad);
                        break;
                    case 4:
                        Console.WriteLine("Gracias por utilizar la Mesa de Partes Digital.");
                        break;
                    default:
                        Console.WriteLine("Opción no válida. Seleccione del 1 al 4.");
                        break;
                }
            } while (op != 4);
        }

        static void RegistrarSolicitud(string[] codigo, string[] nombres, string[] dni, string[] direccion, string[] departamento, string[] provincia, string[] distrito, string[] urbanizacion, string[] correo, string[] telefono, string[] informacion, string[] dependencia, string[] formaEntrega, string[] fechaHora, string[] estado, ref int cantidad)
        {
            string nuevoCodigo, nuevoNombre, nuevoDni, nuevaDireccion;
            string nuevoDepartamento, nuevaProvincia, nuevoDistrito, nuevaUrbanizacion;
            string nuevoCorreo, nuevoTelefono, nuevaInformacion, nuevaDependencia;
            string nuevaFormaEntrega = "", nuevaFechaHora;
            string entradaEntrega, otraForma;
            int fechaNumero, horaNumero, anioActual, opEntrega;
            bool valido;

            if (cantidad >= 1000)
            {
                Console.WriteLine("No se pueden registrar más expedientes en esta ejecución.");
            }
            else
            {
                DateTime ahora = DateTime.Now;
                fechaNumero = ahora.Year * 10000 + ahora.Month * 100 + ahora.Day;
                horaNumero = ahora.Hour * 10000 + ahora.Minute * 100 + ahora.Second;
                anioActual = fechaNumero / 10000;
                nuevoCodigo = GenerarCodigo(codigo, cantidad, anioActual);
                Console.WriteLine("");
                Console.WriteLine("Número de registro: " + nuevoCodigo);
                Console.WriteLine("");
                Console.WriteLine("I. Funcionario responsable de entregar la información");
                Console.WriteLine("Erika Julissa Otero Herrera");
                Console.WriteLine("");
                Console.WriteLine("Coloca tu dirección real y tus datos correctos.");
                Console.WriteLine("II. Datos del solicitante");
                nuevoNombre = LeerTextoNoVacio("Apellidos y nombres: ");
                do
                {
                    Console.WriteLine("Documento de identidad (8 dígitos):");
                    nuevoDni = Console.ReadLine();
                    valido = EsDigitosExactos(nuevoDni, 8);
                    if (!valido)
                    {
                        Console.WriteLine("El DNI debe tener exactamente 8 dígitos.");
                    }
                } while (!valido);
                nuevaDireccion = LeerTextoNoVacio("Av./Calle/Jr./Psj.: ");
                nuevoDepartamento = LeerTextoNoVacio("Departamento: ");
                nuevaProvincia = LeerTextoNoVacio("Provincia: ");
                nuevoDistrito = LeerTextoNoVacio("Distrito: ");
                nuevaUrbanizacion = LeerTextoNoVacio("Urbanización: ");
                do
                {
                    Console.WriteLine("Correo electrónico:");
                    nuevoCorreo = Console.ReadLine();
                    valido = CorreoValido(nuevoCorreo);
                    if (!valido)
                    {
                        Console.WriteLine("El correo debe contener arroba y punto.");
                    }
                } while (!valido);
                do
                {
                    Console.WriteLine("Teléfono (9 dígitos):");
                    nuevoTelefono = Console.ReadLine();
                    valido = EsDigitosExactos(nuevoTelefono, 9);
                    if (!valido)
                    {
                        Console.WriteLine("El teléfono debe tener exactamente 9 dígitos.");
                    }
                } while (!valido);
                Console.WriteLine("");
                Console.WriteLine("III. Información solicitada");
                Console.WriteLine("Aquí tienes que explicar claramente qué documento o información quieres obtener.");
                nuevaInformacion = LeerTextoNoVacio("Información solicitada: ");
                Console.WriteLine("");
                Console.WriteLine("IV. Dependencia de la cual se requiere la información");
                Console.WriteLine("Escribe el nombre del área municipal que posee o administra el documento que solicitas.");
                nuevaDependencia = LeerTextoNoVacio("Dependencia: ");
                Console.WriteLine("");
                Console.WriteLine("V. Forma de entrega de la información");
                Console.WriteLine("1. Copia simple (reproducción física del documento)");
                Console.WriteLine("2. Disquete (opción que aparece en el formulario impreso)");
                Console.WriteLine("3. CD (si está disponible)");
                Console.WriteLine("4. Correo electrónico");
                Console.WriteLine("5. Otro");
                do
                {
                    Console.WriteLine("Seleccione una opción del 1 al 5:");
                    entradaEntrega = Console.ReadLine();
                    if (entradaEntrega != "1" && entradaEntrega != "2" && entradaEntrega != "3" && entradaEntrega != "4" && entradaEntrega != "5")
                    {
                        Console.WriteLine("Opción no válida.");
                    }
                } while (!(entradaEntrega == "1" || entradaEntrega == "2" || entradaEntrega == "3" || entradaEntrega == "4" || entradaEntrega == "5"));
                opEntrega = Convert.ToInt32(entradaEntrega);
                switch (opEntrega)
                {
                    case 1:
                        nuevaFormaEntrega = "Copia simple";
                        break;
                    case 2:
                        nuevaFormaEntrega = "Disquete";
                        break;
                    case 3:
                        nuevaFormaEntrega = "CD";
                        break;
                    case 4:
                        nuevaFormaEntrega = "Correo electrónico";
                        break;
                    case 5:
                        otraForma = LeerTextoNoVacio("Especifique la forma de entrega: ");
                        nuevaFormaEntrega = "Otro: " + otraForma;
                        break;
                }
                nuevaFechaHora = FormatearFechaHora(fechaNumero, horaNumero);
                cantidad = cantidad + 1;
                codigo[cantidad] = nuevoCodigo;
                nombres[cantidad] = nuevoNombre;
                dni[cantidad] = nuevoDni;
                direccion[cantidad] = nuevaDireccion;
                departamento[cantidad] = nuevoDepartamento;
                provincia[cantidad] = nuevaProvincia;
                distrito[cantidad] = nuevoDistrito;
                urbanizacion[cantidad] = nuevaUrbanizacion;
                correo[cantidad] = nuevoCorreo;
                telefono[cantidad] = nuevoTelefono;
                informacion[cantidad] = nuevaInformacion;
                dependencia[cantidad] = nuevaDependencia;
                formaEntrega[cantidad] = nuevaFormaEntrega;
                fechaHora[cantidad] = nuevaFechaHora;
                estado[cantidad] = "Pendiente";
                Console.WriteLine("");
                Console.WriteLine("===== CARGO DE RECEPCIÓN =====");
                Console.WriteLine("Número de registro: " + codigo[cantidad]);
                Console.WriteLine("Fecha y hora: " + fechaHora[cantidad]);
                Console.WriteLine("Solicitante: " + nombres[cantidad]);
                Console.WriteLine("DNI: " + dni[cantidad]);
                Console.WriteLine("Información solicitada: " + informacion[cantidad]);
                Console.WriteLine("Estado: " + estado[cantidad]);
                Console.WriteLine("Se enviaría una confirmación a " + correo[cantidad] + ".");
            }
        }

        static string GenerarCodigo(string[] codigo, int cantidad, int anioActual)
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

        static void ConsultarExpediente(string[] codigo, string[] nombres, string[] dni, string[] direccion, string[] departamento, string[] provincia, string[] distrito, string[] urbanizacion, string[] correo, string[] telefono, string[] informacion, string[] dependencia, string[] formaEntrega, string[] fechaHora, string[] estado, int cantidad)
        {
            string codigoBuscado;
            int posicion;
            bool valido;

            do
            {
                Console.WriteLine("Ingrese el código (EXP-AAAA-NNNNN):");
                codigoBuscado = Console.ReadLine();
                valido = CodigoValido(codigoBuscado);
                if (!valido)
                {
                    Console.WriteLine("Formato incorrecto. Ejemplo: EXP-2026-00001.");
                }
            } while (!valido);
            posicion = BusquedaBinaria(codigo, cantidad, codigoBuscado);
            if (posicion == 0)
            {
                Console.WriteLine("Expediente no encontrado.");
            }
            else
            {
                MostrarExpediente(codigo, nombres, dni, direccion, departamento, provincia, distrito, urbanizacion, correo, telefono, informacion, dependencia, formaEntrega, fechaHora, estado, posicion);
            }
        }

        static int BusquedaBinaria(string[] codigo, int cantidad, string codigoBuscado)
        {
            int izquierda, derecha, medio, posicion;

            izquierda = 1;
            derecha = cantidad;
            posicion = 0;
            while (izquierda <= derecha && posicion == 0)
            {
                medio = (izquierda + derecha) / 2;
                if (codigo[medio] == codigoBuscado)
                {
                    posicion = medio;
                }
                else
                {
                    if (string.CompareOrdinal(codigo[medio], codigoBuscado) < 0)
                    {
                        izquierda = medio + 1;
                    }
                    else
                    {
                        derecha = medio - 1;
                    }
                }
            }
            return posicion;
        }

        static void VerExpedientesPorDni(string[] codigo, string[] nombres, string[] dni, string[] direccion, string[] departamento, string[] provincia, string[] distrito, string[] urbanizacion, string[] correo, string[] telefono, string[] informacion, string[] dependencia, string[] formaEntrega, string[] fechaHora, string[] estado, int cantidad)
        {
            string dniBuscado;
            int i, encontrados;
            bool valido;

            do
            {
                Console.WriteLine("Ingrese su DNI (8 dígitos):");
                dniBuscado = Console.ReadLine();
                valido = EsDigitosExactos(dniBuscado, 8);
                if (!valido)
                {
                    Console.WriteLine("El DNI debe tener exactamente 8 dígitos.");
                }
            } while (!valido);
            encontrados = 0;
            for (i = 1; i <= cantidad; i++)
            {
                if (dni[i] == dniBuscado)
                {
                    encontrados = encontrados + 1;
                    Console.WriteLine("");
                    Console.WriteLine("Expediente " + encontrados);
                    MostrarExpediente(codigo, nombres, dni, direccion, departamento, provincia, distrito, urbanizacion, correo, telefono, informacion, dependencia, formaEntrega, fechaHora, estado, i);
                }
            }
            if (encontrados == 0)
            {
                Console.WriteLine("No se encontraron expedientes asociados a ese DNI.");
            }
        }

        static void MostrarExpediente(string[] codigo, string[] nombres, string[] dni, string[] direccion, string[] departamento, string[] provincia, string[] distrito, string[] urbanizacion, string[] correo, string[] telefono, string[] informacion, string[] dependencia, string[] formaEntrega, string[] fechaHora, string[] estado, int posicion)
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

        static bool EsDigitosExactos(string texto, int largoEsperado)
        {
            int i;
            char caracter;
            bool resultado;

            if (texto.Length == largoEsperado)
            {
                resultado = true;
                for (i = 1; i <= largoEsperado; i++)
                {
                    caracter = texto[i - 1];
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

        static bool CorreoValido(string texto)
        {
            int i;
            char caracter;
            bool tieneArroba, tienePunto;

            tieneArroba = false;
            tienePunto = false;
            for (i = 1; i <= texto.Length; i++)
            {
                caracter = texto[i - 1];
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

        static bool CodigoValido(string texto)
        {
            int i;
            char caracter;
            bool resultado;

            resultado = false;
            if (texto.Length == 14)
            {
                if (texto.Substring(0, 3) == "EXP" && texto[3] == '-' && texto[8] == '-')
                {
                    resultado = true;
                    for (i = 5; i <= 8; i++)
                    {
                        caracter = texto[i - 1];
                        if (caracter < '0' || caracter > '9')
                        {
                            resultado = false;
                        }
                    }
                    for (i = 10; i <= 14; i++)
                    {
                        caracter = texto[i - 1];
                        if (caracter < '0' || caracter > '9')
                        {
                            resultado = false;
                        }
                    }
                }
            }
            return resultado;
        }

        static string LeerTextoNoVacio(string mensaje)
        {
            string valor;

            do
            {
                Console.WriteLine(mensaje);
                valor = Console.ReadLine();
                if (valor.Length == 0)
                {
                    Console.WriteLine("Este dato no puede quedar vacío.");
                }
            } while (valor.Length <= 0);
            return valor;
        }

        static string FormatearFechaHora(int fechaNumero, int horaNumero)
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
            resultado = fechaTexto.Substring(0, 4) + "-" + fechaTexto.Substring(4, 2) + "-" + fechaTexto.Substring(6, 2) + " " + horaTexto.Substring(0, 2) + ":" + horaTexto.Substring(2, 2);
            return resultado;
        }
    }
}