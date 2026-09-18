using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        // 1. LISTA: Inventario actual del pato
        List<string> inventario = new List<string> { "Pluma", "Pan", "Sombrero" };

        // 2. DICCIONARIO: Información extendida de los objetos (Descripción y Uso)
        Dictionary<string, string> diccionarioObjetos = new Dictionary<string, string>
        {
            { "Pluma", "Una pluma afilada del pato. USO: Sirve para forzar cerraduras simples o hacer cosquillas." },
            { "Pan", "Un trozo de pan crujiente. USO: Sirve para recuperar energías o distraer al cazador." },
            { "Sombrero", "Un sombrero elegante y pequeño. USO: Oculta al pato en las sombras para evitar ser visto." }
        };

        // 3. PILA (LIFO): Estructura del escenario (El último en entrar es el primero en salir)
        Stack<string> escenario = new Stack<string>();
        escenario.Push("Sala Principal (Salida)"); // Fondo de la pila
        escenario.Push("Cocina del Cazador");
        escenario.Push("Jaula del Sótano");        // Cima de la pila (Punto de inicio)

        // 4. COLA (FIFO): Carga de 5 movimientos iniciales para la zona actual
        Queue<string> objetivos = new Queue<string>();
        CargarMovimientosPorZona(escenario.Peek(), objetivos);

        bool jugando = true;

        Console.Clear();
        Console.WriteLine("==================================================");
        Console.WriteLine("             DUCK'SCAPE: EL ESCAPE                ");
        Console.WriteLine("==================================================");
        Console.WriteLine("Controles: [D / W] Avanzar hacia la salida");
        Console.WriteLine("           [A / S] Retroceder (Te alejas del objetivo)");
        Console.WriteLine("           [I] Abrir Inventario | [Q] Salir");
        Console.WriteLine("==================================================\n");

        // Bucle Principal
        while (jugando && escenario.Count > 0)
        {
            int movimientosRestantes = objetivos.Count;

            Console.WriteLine($"[Ubicación Actual]: {escenario.Peek()}");
            Console.WriteLine($"[Pasos restantes para salir de la habitación]: {movimientosRestantes}");

            if (movimientosRestantes > 0)
            {
                Console.WriteLine($"[Siguiente Acción]: {objetivos.Peek()}");
            }

            Console.Write("\nAcción (W, A, S, D, I, Q): ");
            ConsoleKeyInfo tecla = Console.ReadKey(true);
            Console.WriteLine();

            // AVANZAR HACIA ADELANTE (D o W)
            if (tecla.Key == ConsoleKey.D || tecla.Key == ConsoleKey.W)
            {
                if (objetivos.Count > 0)
                {
                    // DEQUEUE: Reduce los pasos pendientes en la cola
                    string pasoCumplido = objetivos.Dequeue();
                    Console.WriteLine($"\n[>] ¡El pato avanza ({tecla.Key})! | Realizado: {pasoCumplido}");

                    // Si se completaron los pasos de la zona
                    if (objetivos.Count == 0)
                    {
                        // POP: Remueve la habitación de la Pila
                        string zonaCompletada = escenario.Pop();
                        Console.WriteLine($"\n==================================================");
                        Console.WriteLine($" ¡LOGRO! Has completado todos los pasos y saliste de: {zonaCompletada}");
                        Console.WriteLine($"==================================================");

                        if (escenario.Count > 0)
                        {
                            CargarMovimientosPorZona(escenario.Peek(), objetivos);
                            Console.WriteLine($"[>] Entrando a: {escenario.Peek()} (Se asignaron 5 nuevos movimientos)\n");
                        }
                    }
                }
            }
            // RETROCEDER (A o S)
            else if (tecla.Key == ConsoleKey.A || tecla.Key == ConsoleKey.S)
            {
                // ENQUEUE: Añade un nuevo paso a la cola, incrementando la distancia al objetivo
                objetivos.Enqueue("Paso de retroceso: Te alejaste del objetivo");
                Console.WriteLine($"\n[<] El pato retrocede ({tecla.Key}). ¡Te has alejado de la salida! (+1 paso extra cargado a la cola)");
            }
            // CONTROL DE INVENTARIO (I)
            else if (tecla.Key == ConsoleKey.I)
            {
                MostrarInventarioYDICCIONARIO(inventario, diccionarioObjetos);
            }
            // SALIR DEL JUEGO (Q)
            else if (tecla.Key == ConsoleKey.Q)
            {
                jugando = false;
                Console.WriteLine("\nEl pato decidió rendirse por hoy...");
            }
            else
            {
                Console.WriteLine("\n[!] Tecla no válida. Usa D para avanzar, A para retroceder o 'I' para el inventario.");
            }

            Console.WriteLine("--------------------------------------------------\n");
        }

        if (escenario.Count == 0)
        {
            Console.WriteLine("\n==================================================");
            Console.WriteLine(" ¡VICTORIA TOTAL! El pato ha salido de todas las habitaciones.");
            Console.WriteLine("==================================================");
        }

        Console.WriteLine("\nPresiona cualquier tecla para finalizar...");
        Console.ReadKey();
    }

    // Asigna 5 movimientos base a la Cola según la zona
    static void CargarMovimientosPorZona(string zona, Queue<string> objetivos)
    {
        objetivos.Clear();

        if (zona.Contains("Jaula"))
        {
            objetivos.Enqueue("1. Avanzar con sigilo hacia los barrotes");
            objetivos.Enqueue("2. Inspeccionar la cerradura de la jaula");
            objetivos.Enqueue("3. Forzar el cerrojo utilizando la pluma");
            objetivos.Enqueue("4. Empujar la puerta silenciosamente");
            objetivos.Enqueue("5. Salir de la jaula y subir las escaleras");
        }
        else if (zona.Contains("Cocina"))
        {
            objetivos.Enqueue("1. Esconderse detrás de la barra de la cocina");
            objetivos.Enqueue("2. Esquivar la trampa para osos en el suelo");
            objetivos.Enqueue("3. Tomar la llave olvidada en la mesa de cortar");
            objetivos.Enqueue("4. Distraer al cazador lanzando un plato");
            objetivos.Enqueue("5. Escapar por el pasillo hacia la sala principal");
        }
        else if (zona.Contains("Sala"))
        {
            objetivos.Enqueue("1. Caminar en puntillas junto al cazador ebrio");
            objetivos.Enqueue("2. Esquivar el sillón reclinable");
            objetivos.Enqueue("3. Alcanzar la puerta principal de la cabaña");
            objetivos.Enqueue("4. Desbloquear el pasador de madera");
            objetivos.Enqueue("5. Abrir la puerta y huir hacia la libertad");
        }
    }

    // Consulta la LISTA de inventario y busca la descripción en el DICCIONARIO
    static void MostrarInventarioYDICCIONARIO(List<string> inventario, Dictionary<string, string> diccionario)
    {
        Console.WriteLine("\n================ INVENTARIO (LISTA) ================");
        for (int i = 0; i < inventario.Count; i++)
        {
            Console.WriteLine($" [{i + 1}] {inventario[i]}");
        }
        Console.WriteLine("====================================================");
        Console.Write("Selecciona un objeto (1-3) para ver detalles o [Otra tecla] para cerrar: ");

        ConsoleKeyInfo opcion = Console.ReadKey(true);
        Console.WriteLine();

        int indice = -1;
        if (opcion.Key == ConsoleKey.D1 || opcion.Key == ConsoleKey.NumPad1) indice = 0;
        if (opcion.Key == ConsoleKey.D2 || opcion.Key == ConsoleKey.NumPad2) indice = 1;
        if (opcion.Key == ConsoleKey.D3 || opcion.Key == ConsoleKey.NumPad3) indice = 2;

        if (indice >= 0 && indice < inventario.Count)
        {
            string nombreObjeto = inventario[indice];

            if (diccionario.ContainsKey(nombreObjeto))
            {
                Console.WriteLine($"\n--- DETALLES EN DICCIONARIO: {nombreObjeto.ToUpper()} ---");
                Console.WriteLine($"-> {diccionario[nombreObjeto]}");
            }
        }
        else
        {
            Console.WriteLine("\n[Cerrando inventario...]");
        }
    }
}