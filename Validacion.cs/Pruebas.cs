using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MesaDePartes
{
    public static class Pruebas
    {
        private static int total = 0, aprobadas = 0;
        private static StringBuilder reporte = new StringBuilder();

        private static void Verificar(string id, string descripcion, bool ok)
        {
            total++;
            if (ok) aprobadas++;
            string linea = id + " | " + (ok ? "APROBADO" : "FALLÓ   ") + " | " + descripcion;
            Console.WriteLine(linea);
            reporte.AppendLine(linea);
        }

        private static string[] Datos(string nombre, string dni)
        {
            string[] d = new string[Expedientes.NUM_CAMPOS];
            d[Expedientes.NOMBRES] = nombre;
            d[Expedientes.DNI] = dni;
            d[Expedientes.DIRECCION] = "Av. Túpac Amaru 1234";
            d[Expedientes.DEPARTAMENTO] = "Lima";
            d[Expedientes.PROVINCIA] = "Lima";
            d[Expedientes.DISTRITO] = "Comas";
            d[Expedientes.URBANIZACION] = "Collique";
            d[Expedientes.CORREO] = "usuario@correo.com";
            d[Expedientes.TELEFONO] = "987654321";
            d[Expedientes.INFORMACION] = "Copia del plano de zonificación";
            d[Expedientes.DEPENDENCIA] = "Gerencia de Desarrollo Urbano";
            d[Expedientes.FORMA_ENTREGA] = "Copia simple";
            return d;
        }

        public static void Ejecutar()
        {
            Persistencia.Ruta = Path.Combine(Path.GetTempPath(), "mesa_partes_pruebas.txt");
            if (File.Exists(Persistencia.Ruta)) File.Delete(Persistencia.Ruta);
            Expedientes.Reiniciar();

            Console.WriteLine("ID | RESULTADO | DESCRIPCIÓN");

            Verificar("V01", "DNI de 8 dígitos es válido", Validaciones.EsDigitosExactos("12345678", 8));
            Verificar("V02", "DNI de 7 dígitos es rechazado", !Validaciones.EsDigitosExactos("1234567", 8));
            Verificar("V03", "DNI con letras es rechazado", !Validaciones.EsDigitosExactos("1234567A", 8));
            Verificar("V04", "Teléfono de 9 dígitos es válido", Validaciones.EsDigitosExactos("987654321", 9));
            Verificar("V05", "Teléfono de 8 dígitos es rechazado", !Validaciones.EsDigitosExactos("98765432", 9));
            Verificar("V06", "Correo válido aceptado", Validaciones.CorreoValido("ana@correo.com"));
            Verificar("V07", "Correo sin '@' rechazado", !Validaciones.CorreoValido("anacorreo.com"));
            Verificar("V08", "Correo sin '.' rechazado", !Validaciones.CorreoValido("ana@correo"));
            Verificar("V09", "Correo 'a@.com' rechazado", !Validaciones.CorreoValido("a@.com"));
            Verificar("V10", "Campo vacío/espacios rechazado", !Validaciones.TextoNoVacio("   "));
            Verificar("V11", "Código EXP-2026-00001 válido", Validaciones.CodigoValido("EXP-2026-00001"));
            Verificar("V12", "Código EXP-26-1 rechazado", !Validaciones.CodigoValido("EXP-26-1"));

            string c1 = Expedientes.Registrar(Datos("Quispe Rojas, Ana", "11111111"), new DateTime(2026, 10, 16, 9, 0, 0));
            Verificar("G01", "Primer código es EXP-2026-00001", c1 == "EXP-2026-00001");
            string c2 = Expedientes.Registrar(Datos("Vargas Lima, Luis", "22222222"), new DateTime(2026, 10, 16, 9, 5, 0));
            Verificar("G02", "Segundo código es EXP-2026-00002 (correlativo)", c2 == "EXP-2026-00002");
            string c3 = Expedientes.Registrar(Datos("Quispe Rojas, Ana", "11111111"), new DateTime(2026, 10, 16, 9, 10, 0));
            Verificar("G03", "Tercer código es EXP-2026-00003", c3 == "EXP-2026-00003");
            string c4 = Expedientes.Registrar(Datos("Zegarra Díaz, Rosa", "33333333"), new DateTime(2027, 1, 2, 8, 0, 0));
            Verificar("G04", "Cambio de año reinicia a EXP-2027-00001", c4 == "EXP-2027-00001");
            Verificar("G05", "Estado inicial es Pendiente", Expedientes.Col[Expedientes.ESTADO][0] == "Pendiente");

            string[] conBarra = Datos("Pérez|Gómez, Juan", "44444444");
            Expedientes.Registrar(conBarra, new DateTime(2027, 1, 2, 8, 5, 0));
            Verificar("G06", "El símbolo '|' se sanea (no rompe el archivo)",
                      Expedientes.Col[Expedientes.NOMBRES][4] == "Pérez/Gómez, Juan");

            Verificar("B01", "Binaria encuentra EXP-2026-00002 en posición 1", Busqueda.BinariaPorCodigo("EXP-2026-00002") == 1);
            Verificar("B02", "Binaria encuentra el primero", Busqueda.BinariaPorCodigo("EXP-2026-00001") == 0);
            Verificar("B03", "Binaria encuentra el último", Busqueda.BinariaPorCodigo("EXP-2027-00002") == 4);
            Verificar("B04", "Binaria devuelve -1 si no existe", Busqueda.BinariaPorCodigo("EXP-2026-09999") == -1);
            Verificar("B05", "Lineal por DNI devuelve 2 expedientes de 11111111", Busqueda.LinealPorDni("11111111").Count == 2);
            Verificar("B06", "Lineal por DNI sin expedientes devuelve 0", Busqueda.LinealPorDni("99999999").Count == 0);

            int[] porCodDesc = Ordenamiento.OrdenarIndices(Expedientes.COD, false);
            Verificar("O01", "Orden por código descendente (primero el último)", porCodDesc[0] == 4 && porCodDesc[4] == 0);
            int[] porNombreAsc = Ordenamiento.OrdenarIndices(Expedientes.NOMBRES, true);
            Verificar("O02", "Orden por solicitante ascendente (Pérez primero)", Expedientes.Col[Expedientes.NOMBRES][porNombreAsc[0]].StartsWith("Pérez"));
            int[] porFechaAsc = Ordenamiento.OrdenarIndices(Expedientes.FECHA_HORA, true);
            Verificar("O03", "Orden por fecha ascendente (primero el más antiguo)", porFechaAsc[0] == 0 && porFechaAsc[4] == 4);
            Verificar("O04", "Ordenar no altera el orden de los arreglos paralelos", Expedientes.Col[Expedientes.COD][0] == "EXP-2026-00001");

            string ruta = Persistencia.Ruta;
            if (File.Exists(ruta)) File.Delete(ruta);
            int cantidadAntes = Expedientes.Cantidad;
            for (int i = 0; i < cantidadAntes; i++)
            {
                string[] campos = new string[Expedientes.NUM_CAMPOS];
                for (int k = 0; k < Expedientes.NUM_CAMPOS; k++) campos[k] = Expedientes.Col[k][i];
                File.AppendAllText(ruta, string.Join("|", campos) + Environment.NewLine, Encoding.UTF8);
            }
            Expedientes.Reiniciar();
            int cargados, omitidas;
            Persistencia.Cargar(out cargados, out omitidas);
            Verificar("P01", "Se recuperan los 5 expedientes tras 'reiniciar'", cargados == 5 && omitidas == 0);
            string siguiente = Expedientes.GenerarCodigo(new DateTime(2027, 1, 3));
            Verificar("P02", "La numeración continúa tras reiniciar (EXP-2027-00003)", siguiente == "EXP-2027-00003");

            File.AppendAllText(ruta, "linea corrupta sin separadores" + Environment.NewLine);
            File.AppendAllText(ruta, string.Join("|", new string[] {
                "EXP-2026-00001","a","12345678","d","d","p","d","u","a@b.com","987654321","i","d","f","2026-10-16 09:00:00","Pendiente" }) + Environment.NewLine);
            Expedientes.Reiniciar();
            Persistencia.Cargar(out cargados, out omitidas);
            Verificar("P03", "Línea corrupta y código duplicado/desordenado se ignoran", cargados == 5 && omitidas == 2);

            File.Delete(ruta);
            Expedientes.Reiniciar();
            Persistencia.Cargar(out cargados, out omitidas);
            Verificar("P04", "Sin archivo: inicia vacío sin errores", cargados == 0 && Expedientes.Cantidad == 0);

            Expedientes.Registrar(Datos("Prueba Guardar", "55555555"), new DateTime(2026, 10, 17, 10, 0, 0));
            Verificar("P05", "GuardarUltimo escribe el archivo", Persistencia.GuardarUltimo() && File.Exists(ruta));

            Expedientes.Reiniciar();
            string ultimo = null;
            for (int i = 0; i < Expedientes.CAPACIDAD; i++)
                ultimo = Expedientes.Registrar(Datos("Persona " + i, "10000000"), new DateTime(2026, 10, 17, 10, 0, 0));
            Verificar("L01", "Se registran 1000 expedientes (último EXP-2026-01000)", ultimo == "EXP-2026-01000" && Expedientes.Cantidad == 1000);
            Verificar("L02", "El expediente 1001 es rechazado (null)", Expedientes.Registrar(Datos("Extra", "10000000"), DateTime.Now) == null);
            Verificar("L03", "Binaria encuentra el expediente 1000", Busqueda.BinariaPorCodigo("EXP-2026-01000") == 999);

            if (File.Exists(ruta)) File.Delete(ruta);

            string resumen = "RESUMEN: " + aprobadas + " de " + total + " pruebas aprobadas.";
            Console.WriteLine(resumen);
            reporte.AppendLine(resumen);
            try { File.WriteAllText("resultados_pruebas.txt", reporte.ToString(), Encoding.UTF8); }
            catch (IOException) { Console.WriteLine("No se pudo escribir resultados_pruebas.txt"); }
        }
    }
}