using System;

namespace MesaDePartes
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            Persistencia.CargarExpedientes();
            ControlarMenu();
        }
        static void ControlarMenu()
        {
            int op;

            do
            {
                Console.WriteLine("");
                Colores.Titulo("MESA DE PARTES DIGITAL - MUNICIPALIDAD DE COMAS");
                Colores.Texto("1. Presentar una solicitud");
                Colores.Texto("2. Consultar mi expediente por código");
                Colores.Texto("3. Ver todos mis expedientes por DNI");
                Colores.Texto("4. Listar expedientes ordenados por apellidos y nombres");
                Colores.Texto("5. Salir");
                Colores.Texto("Seleccione una opción:");
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
                        Colores.Exito("Gracias por utilizar la Mesa de Partes Digital.");
                        break;
                    default:
                        Colores.Error("Opción no válida. Seleccione del 1 al 5.");
                        break;
                }
            } while (op != 5);
        }
        static string LeerTextoNoVacio(string mensaje)
        {
            string valor;

            do
            {
                Colores.Texto(mensaje);
                valor = Console.ReadLine();
                if (valor.Length == 0)
                {
                    Colores.Error("Este dato no puede quedar vacío.");
                }
            } while (valor.Length <= 0);
            return valor;
        }
        static string LeerDependencia()
        {
            string[] dependencias = {
                "Oficina de Atención al Ciudadano, Trámite Documentario y Archivo",
                "Gerencia de Administración Tributaria",
                "Gerencia de Gestión Territorial y Desarrollo Económico",
                "Subgerencia de Obras Privadas y Catastro",
                "Subgerencia de Gestión del Riesgo de Desastres"
            };
            int i, opcion;

            for (i = 0; i < dependencias.Length; i++)
            {
                Colores.Texto((i + 1) + ". " + dependencias[i]);
            }
            do
            {
                Colores.Texto("Seleccione una opción del 1 al 5:");
                if (!int.TryParse(Console.ReadLine(), out opcion))
                {
                    opcion = 0;
                }
                if (opcion < 1 || opcion > 5)
                {
                    Colores.Error("Opción no válida.");
                }
            } while (opcion < 1 || opcion > 5);
            return dependencias[opcion - 1];
        }
        static void RegistrarSolicitud()
        {
            string nuevoCodigo, nuevoNombre, nuevoDni, nuevaDireccion;
            string nuevoDepartamento, nuevaProvincia, nuevoDistrito, nuevaUrbanizacion;
            string nuevoCorreo, nuevoTelefono, nuevaInformacion, nuevaDependencia;
            string nuevaFormaEntrega = "", nuevaFechaHora;
            string entradaEntrega, otraForma;
            int fechaNumero, horaNumero, anioActual, opEntrega;
            bool valido;
            string nombreRegistrado;
            bool nombreCoincide;

            if (Expedientes.cantidad >= Expedientes.MAXIMO)
            {
                Colores.Error("No se pueden registrar más expedientes.");
            }
            else
            {
                DateTime ahora = DateTime.Now;
                fechaNumero = ahora.Year * 10000 + ahora.Month * 100 + ahora.Day;
                horaNumero = ahora.Hour * 10000 + ahora.Minute * 100 + ahora.Second;
                anioActual = fechaNumero / 10000;
                nuevoCodigo = Expedientes.GenerarCodigo(anioActual);
                Console.WriteLine("");
                Colores.Titulo("Número de registro: " + nuevoCodigo);
                Console.WriteLine("");
                Colores.Titulo("I. Funcionario responsable de entregar la información");
                Colores.Texto("Erika Julissa Otero Herrera");
                Console.WriteLine("");
                Colores.Advertencia("Coloca tu dirección real y tus datos correctos.");
                Colores.Titulo("II. Datos del solicitante");
                do
                {
                    nuevoNombre = LeerTextoNoVacio("Apellidos y nombres: ");
                    do
                    {
                        Colores.Texto("Documento de identidad (8 dígitos):");
                        nuevoDni = Console.ReadLine();
                        valido = Validaciones.DniValido(nuevoDni);
                        if (!valido)
                        {
                            Colores.Error("El DNI debe tener exactamente 8 dígitos y no puede ser 00000000.");
                        }
                    } while (!valido);

                    nombreRegistrado = Expedientes.BuscarNombrePorDni(nuevoDni);
                    nombreCoincide = nombreRegistrado == null || Expedientes.MismoNombre(nombreRegistrado, nuevoNombre);
                    if (!nombreCoincide)
                    {
                        Colores.Error("Este DNI ya fue registrado con otro nombre. Verifique sus datos.");
                    }
                } while (!nombreCoincide);

                nuevaDireccion = LeerTextoNoVacio("Av./Calle/Jr./Psj.: ");
                nuevoDepartamento = LeerTextoNoVacio("Departamento: ");
                nuevaProvincia = LeerTextoNoVacio("Provincia: ");
                nuevoDistrito = LeerTextoNoVacio("Distrito: ");
                nuevaUrbanizacion = LeerTextoNoVacio("Urbanización: ");
                do
                {
                    Colores.Texto("Correo electrónico:");
                    nuevoCorreo = Console.ReadLine();
                    valido = Validaciones.CorreoValido(nuevoCorreo);
                    if (!valido)
                    {
                        Colores.Error("El correo debe contener arroba y punto.");
                    }
                } while (!valido);
                do
                {
                    Colores.Texto("Teléfono (9 dígitos):");
                    nuevoTelefono = Console.ReadLine();
                    valido = Validaciones.TelefonoValido(nuevoTelefono);
                    if (!valido)
                    {
                        Colores.Error("El teléfono debe tener exactamente 9 dígitos y empezar con 9.");
                    }
                } while (!valido);
                Console.WriteLine("");
                Colores.Titulo("III. Información solicitada");
                Colores.Texto("Aquí tienes que explicar claramente qué documento o información quieres obtener.");
                nuevaInformacion = LeerTextoNoVacio("Información solicitada: ");
                Console.WriteLine("");
                Colores.Titulo("IV. Dependencia de la cual se requiere la información");
                Colores.Texto("Selecciona el área municipal que posee o administra el documento que solicitas.");
                nuevaDependencia = LeerDependencia();
                Console.WriteLine("");
                Colores.Titulo("V. Forma de entrega de la información");
                Colores.Texto("1. Copia simple (reproducción física del documento)");
                Colores.Texto("2. Disquete (opción que aparece en el formulario impreso)");
                Colores.Texto("3. CD (si está disponible)");
                Colores.Texto("4. Correo electrónico");
                Colores.Texto("5. Otro");
                do
                {
                    Colores.Texto("Seleccione una opción del 1 al 5:");
                    entradaEntrega = Console.ReadLine();
                    if (entradaEntrega != "1" && entradaEntrega != "2" && entradaEntrega != "3" && entradaEntrega != "4" && entradaEntrega != "5")
                    {
                        Colores.Error("Opción no válida.");
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
                Persistencia.GuardarExpedientes();
                Colores.Exito("Datos registrados correctamente.");

                Console.WriteLine("");
                Colores.Exito("===== CARGO DE RECEPCIÓN =====");
                Colores.Texto("Número de registro: " + Expedientes.codigo[Expedientes.cantidad]);
                Colores.Texto("Fecha y hora: " + Expedientes.fechaHora[Expedientes.cantidad]);
                Colores.Texto("Solicitante: " + Expedientes.nombres[Expedientes.cantidad]);
                Colores.Texto("DNI: " + Expedientes.dni[Expedientes.cantidad]);
                Colores.Texto("Información solicitada: " + Expedientes.informacion[Expedientes.cantidad]);
                Colores.Texto("Estado: " + Expedientes.estado[Expedientes.cantidad]);
                Colores.Texto("Se enviaría una confirmación a " + Expedientes.correo[Expedientes.cantidad] + ".");
            }
        }
        static void ConsultarExpediente()
        {
            string codigoBuscado;
            int posicion;
            bool valido;

            do
            {
                Colores.Texto("Ingrese el código (EXP-AAAA-NNNNN):");
                codigoBuscado = Console.ReadLine();
                valido = Validaciones.CodigoValido(codigoBuscado);
                if (!valido)
                {
                    Colores.Error("Formato incorrecto. Ejemplo: EXP-2026-00001.");
                }
            } while (!valido);
            posicion = Busqueda.BusquedaBinaria(codigoBuscado);
            if (posicion == 0)
            {
                Colores.Advertencia("Expediente no encontrado.");
            }
            else
            {
                Expedientes.MostrarExpediente(posicion);
            }
        }
        static void VerExpedientesPorDni()
        {
            string dniBuscado;
            int i, encontrados;
            bool valido;

            do
            {
                Colores.Texto("Ingrese su DNI (8 dígitos):");
                dniBuscado = Console.ReadLine();
                valido = Validaciones.DniValido(dniBuscado);
                if (!valido)
                {
                    Colores.Error("El DNI debe tener exactamente 8 dígitos y no puede ser 00000000.");
                }
            } while (!valido);
            encontrados = 0;
            for (i = 1; i <= Expedientes.cantidad; i++)
            {
                if (Expedientes.dni[i] == dniBuscado)
                {
                    encontrados = encontrados + 1;
                    Console.WriteLine("");
                    Colores.Titulo("Expediente " + encontrados);
                    Expedientes.MostrarExpediente(i);
                }
            }
            if (encontrados == 0)
            {
                Colores.Advertencia("No se encontraron expedientes asociados a ese DNI.");
            }
        }
        static void ListarOrdenados()
        {
            int[] orden = new int[Expedientes.MAXIMO + 1];
            int i;

            if (Expedientes.cantidad == 0)
            {
                Colores.Advertencia("No hay expedientes registrados.");
            }
            else
            {
                Ordenamiento.OrdenarPorNombres(orden);
                Console.WriteLine("");
                Colores.Titulo("EXPEDIENTES ORDENADOS POR APELLIDOS Y NOMBRES");
                for (i = 1; i <= Expedientes.cantidad; i++)
                {
                    Colores.Texto(Expedientes.nombres[orden[i]] + " | " + Expedientes.codigo[orden[i]]
                        + " | " + Expedientes.fechaHora[orden[i]] + " | " + Expedientes.estado[orden[i]]);
                }
            }
        }
    }
}