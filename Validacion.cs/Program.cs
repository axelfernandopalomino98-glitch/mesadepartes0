using System;

namespace MesaDePartes
{
    // CLASE PROGRAM
    // Es el programa principal: muestra el menú y coordina a las demás clases.
    // Las funciones tienen los mismos nombres que en el PSeInt.
    public static class Program
    {
        public static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8; // solo para que se vean las tildes

            Persistencia.CargarExpedientes(); // lee expedientes.txt (si existe)
            ControlarMenu();
        }

        // Repite el menú hasta que el usuario elige "Salir" (PSeInt: Repetir ... Hasta Que op=5)
        static void ControlarMenu()
        {
            int op;

            do
            {
                Console.WriteLine("");
                Console.WriteLine("MESA DE PARTES DIGITAL - MUNICIPALIDAD DE COMAS");
                Console.WriteLine("1. Presentar una solicitud");
                Console.WriteLine("2. Consultar mi expediente por código");
                Console.WriteLine("3. Ver todos mis expedientes por DNI");
                Console.WriteLine("4. Listar expedientes ordenados por apellidos y nombres");
                Console.WriteLine("5. Salir");
                Console.WriteLine("Seleccione una opción:");
                // Si escriben letras en vez de un número, op queda en 0 y cae en "Opción no válida"
                if (!int.TryParse(Console.ReadLine(), out op))
                {
                    op = 0;
                }
                switch (op)
                {
                    case 1:
                        RegistrarSolicitud();
                        break;
                    case 2:
                        ConsultarExpediente();
                        break;
                    case 3:
                        VerExpedientesPorDni();
                        break;
                    case 4:
                        ListarOrdenados();
                        break;
                    case 5:
                        Console.WriteLine("Gracias por utilizar la Mesa de Partes Digital.");
                        break;
                    default:
                        Console.WriteLine("Opción no válida. Seleccione del 1 al 5.");
                        break;
                }
            } while (op != 5);
        }

        // Pide el texto repetidamente hasta que no esté vacío. Devuelve el texto escrito.
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

        // Pide los datos, los valida, los guarda en los arreglos paralelos y en el archivo.
        static void RegistrarSolicitud()
        {
            string nuevoCodigo, nuevoNombre, nuevoDni, nuevaDireccion;
            string nuevoDepartamento, nuevaProvincia, nuevoDistrito, nuevaUrbanizacion;
            string nuevoCorreo, nuevoTelefono, nuevaInformacion, nuevaDependencia;
            string nuevaFormaEntrega = "", nuevaFechaHora;
            string entradaEntrega, otraForma;
            int fechaNumero, horaNumero, anioActual, opEntrega;
            bool valido;

            if (Expedientes.cantidad >= Expedientes.MAXIMO)
            {
                Console.WriteLine("No se pueden registrar más expedientes.");
            }
            else
            {
                // Fecha y hora como números (PSeInt: FechaActual() y HoraActual())
                DateTime ahora = DateTime.Now;
                fechaNumero = ahora.Year * 10000 + ahora.Month * 100 + ahora.Day;
                horaNumero = ahora.Hour * 10000 + ahora.Minute * 100 + ahora.Second;
                anioActual = fechaNumero / 10000;
                nuevoCodigo = Expedientes.GenerarCodigo(anioActual);
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
                    valido = Validaciones.EsDigitosExactos(nuevoDni, 8);
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
                    valido = Validaciones.CorreoValido(nuevoCorreo);
                    if (!valido)
                    {
                        Console.WriteLine("El correo debe contener arroba y punto.");
                    }
                } while (!valido);
                do
                {
                    Console.WriteLine("Teléfono (9 dígitos):");
                    nuevoTelefono = Console.ReadLine();
                    valido = Validaciones.EsDigitosExactos(nuevoTelefono, 9);
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
                nuevaFechaHora = Expedientes.FormatearFechaHora(fechaNumero, horaNumero);

                // Guardar en los arreglos paralelos: todos en la misma posición "cantidad"
                Expedientes.cantidad = Expedientes.cantidad + 1;
                Expedientes.codigo[Expedientes.cantidad] = nuevoCodigo;
                Expedientes.nombres[Expedientes.cantidad] = nuevoNombre;
                Expedientes.dni[Expedientes.cantidad] = nuevoDni;
                Expedientes.direccion[Expedientes.cantidad] = nuevaDireccion;
                Expedientes.departamento[Expedientes.cantidad] = nuevoDepartamento;
                Expedientes.provincia[Expedientes.cantidad] = nuevaProvincia;
                Expedientes.distrito[Expedientes.cantidad] = nuevoDistrito;
                Expedientes.urbanizacion[Expedientes.cantidad] = nuevaUrbanizacion;
                Expedientes.correo[Expedientes.cantidad] = nuevoCorreo;
                Expedientes.telefono[Expedientes.cantidad] = nuevoTelefono;
                Expedientes.informacion[Expedientes.cantidad] = nuevaInformacion;
                Expedientes.dependencia[Expedientes.cantidad] = nuevaDependencia;
                Expedientes.formaEntrega[Expedientes.cantidad] = nuevaFormaEntrega;
                Expedientes.fechaHora[Expedientes.cantidad] = nuevaFechaHora;
                Expedientes.estado[Expedientes.cantidad] = "Pendiente";

                // Guardar en el archivo para no perder el trámite al cerrar el programa
                Persistencia.GuardarExpedientes();

                Console.WriteLine("");
                Console.WriteLine("===== CARGO DE RECEPCIÓN =====");
                Console.WriteLine("Número de registro: " + Expedientes.codigo[Expedientes.cantidad]);
                Console.WriteLine("Fecha y hora: " + Expedientes.fechaHora[Expedientes.cantidad]);
                Console.WriteLine("Solicitante: " + Expedientes.nombres[Expedientes.cantidad]);
                Console.WriteLine("DNI: " + Expedientes.dni[Expedientes.cantidad]);
                Console.WriteLine("Información solicitada: " + Expedientes.informacion[Expedientes.cantidad]);
                Console.WriteLine("Estado: " + Expedientes.estado[Expedientes.cantidad]);
                Console.WriteLine("Se enviaría una confirmación a " + Expedientes.correo[Expedientes.cantidad] + ".");
            }
        }

        // Pide un código válido, lo busca con búsqueda binaria y muestra el expediente.
        static void ConsultarExpediente()
        {
            string codigoBuscado;
            int posicion;
            bool valido;

            do
            {
                Console.WriteLine("Ingrese el código (EXP-AAAA-NNNNN):");
                codigoBuscado = Console.ReadLine();
                valido = Validaciones.CodigoValido(codigoBuscado);
                if (!valido)
                {
                    Console.WriteLine("Formato incorrecto. Ejemplo: EXP-2026-00001.");
                }
            } while (!valido);
            posicion = Busqueda.BusquedaBinaria(codigoBuscado);
            if (posicion == 0)
            {
                Console.WriteLine("Expediente no encontrado.");
            }
            else
            {
                Expedientes.MostrarExpediente(posicion);
            }
        }

        // Búsqueda lineal: recorre todos los expedientes y muestra los que tienen ese DNI.
        // Un mismo DNI puede tener varios expedientes.
        static void VerExpedientesPorDni()
        {
            string dniBuscado;
            int i, encontrados;
            bool valido;

            do
            {
                Console.WriteLine("Ingrese su DNI (8 dígitos):");
                dniBuscado = Console.ReadLine();
                valido = Validaciones.EsDigitosExactos(dniBuscado, 8);
                if (!valido)
                {
                    Console.WriteLine("El DNI debe tener exactamente 8 dígitos.");
                }
            } while (!valido);
            encontrados = 0;
            for (i = 1; i <= Expedientes.cantidad; i++)
            {
                if (Expedientes.dni[i] == dniBuscado)
                {
                    encontrados = encontrados + 1;
                    Console.WriteLine("");
                    Console.WriteLine("Expediente " + encontrados);
                    Expedientes.MostrarExpediente(i);
                }
            }
            if (encontrados == 0)
            {
                Console.WriteLine("No se encontraron expedientes asociados a ese DNI.");
            }
        }

        // Ordena con el método de la burbuja y muestra la lista por apellidos y nombres.
        static void ListarOrdenados()
        {
            int[] orden = new int[Expedientes.MAXIMO + 1];
            int i;

            if (Expedientes.cantidad == 0)
            {
                Console.WriteLine("No hay expedientes registrados.");
            }
            else
            {
                Ordenamiento.OrdenarPorNombres(orden);
                Console.WriteLine("");
                Console.WriteLine("EXPEDIENTES ORDENADOS POR APELLIDOS Y NOMBRES");
                for (i = 1; i <= Expedientes.cantidad; i++)
                {
                    Console.WriteLine(Expedientes.nombres[orden[i]] + " | " + Expedientes.codigo[orden[i]]
                        + " | " + Expedientes.fechaHora[orden[i]] + " | " + Expedientes.estado[orden[i]]);
                }
            }
        }
    }
}