using System;

namespace MesaDePartes
{
    // CLASE COLORES
    // Escribe los mensajes en la consola con colores para que se vea mejor.
    // Cada función cambia el color, escribe el mensaje y vuelve al color normal.
    //   Titulo       = celeste (títulos y secciones)
    //   Texto        = blanco (texto normal y preguntas)
    //   Advertencia  = amarillo (avisos)
    //   Error        = rojo (datos incorrectos)
    //   Exito        = verde (todo salió bien)
    public static class Colores
    {
        // Función SIN retorno con dos parámetros: el mensaje y el color. Las demás la reutilizan.
        static void Escribir(string mensaje, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(mensaje);
            Console.ResetColor();
        }

        public static void Titulo(string mensaje)
        {
            Escribir(mensaje, ConsoleColor.Cyan);
        }

        public static void Texto(string mensaje)
        {
            Escribir(mensaje, ConsoleColor.White);
        }

        public static void Advertencia(string mensaje)
        {
            Escribir(mensaje, ConsoleColor.Yellow);
        }

        public static void Error(string mensaje)
        {
            Escribir(mensaje, ConsoleColor.Red);
        }

        public static void Exito(string mensaje)
        {
            Escribir(mensaje, ConsoleColor.Green);
        }
    }
}