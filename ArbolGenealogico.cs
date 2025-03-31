using System;

class Nodo  // creo mi clase nodo
{
    public string Nombre; // creo el nombre del hijo y sus 2 padres
    public Nodo Padre1, Padre2;

    public Nodo(string nombre)
    {
        Nombre = nombre;     // nombre va a ser igual al nombre que le quiera dar  
        Padre1 = null;         // los 2 padres iniciaran como nulo
        Padre2 = null;
    }

    public bool TieneEspacioParaPadres() // comprobar si puedo ingresar algun padre
    {
        if(Padre1 == null || Padre2 == null)  
                                                // si algun padre es null puedo meter otro padre
            return true;
        else{
            return false;
        }
    }

    public void AgregarPadre(Nodo padre)  // agrego un padre
    {
        if (Padre1 == null)             // si alguno de los 2 padres es null lo agrego
            Padre1 = padre;
        else if (Padre2 == null)
            Padre2 = padre;
        else
            throw new InvalidOperationException("Este nodo ya tiene dos padres."); // mando una exception si no se pueden agregar padres
    }
}

class ArbolGenealogico // mi clase arbol
{
    private Nodo raiz; // creo mi nodo raiz

    public void Insertar(string hijoNombre, string nuevoPadreNombre) // metodo para insertar padres a los hijos
    {
        if (raiz == null) // si el arbol esta vacio creo un primer hijo
        {
            raiz = new Nodo(hijoNombre);
            Console.WriteLine($"Se creó el nodo raíz: {hijoNombre}");
            return;
        }
        
        Nodo hijo = Buscar(raiz, hijoNombre); // uso la función buscar para encontrar el nodo al que le quiero insertar un padre
        if (hijo == null) // si no encontre el nodo 
        {
            Console.WriteLine($"No se encontró el nodo '{hijoNombre}'."); // imprimo que no se encontró
            return;
        }

        if (!hijo.TieneEspacioParaPadres()) // si los 2 padres ya estan ocupados
        {
            Console.WriteLine($"El nodo '{hijoNombre}' ya tiene dos padres."); //  escribo que no puedo meter mas padres
            return;
        }

        Nodo nuevoPadre = new Nodo(nuevoPadreNombre); // creo el nodo padre
        hijo.AgregarPadre(nuevoPadre); // inserto el padre
        Console.WriteLine($"Se agregó a '{nuevoPadreNombre}' como padre de '{hijoNombre}'."); // escribo en la pantalla la operación hecha
    }

    private Nodo Buscar(Nodo nodo, string nombre) // metodo para buscar a hijo de manera recursiva
    {
        if (nodo == null) return null; // si el nodo esta vacio entonces mando null
        if (nodo.Nombre == nombre) return nodo; // si encuentro el nombre devuelvo el nodo donde esta el nombre

        Nodo encontrado = Buscar(nodo.Padre1, nombre); // si el nodo en el que estoy no es el nombre que busco, voy a investigar al padre1
        if (encontrado != null) return encontrado; // al terminar de recorrer todos los padre1, y encontré el nombre, retorno

        return Buscar(nodo.Padre2, nombre); // si en todos los padres 1 no esta, entonces recorro todos los padre2
    }

    public void Preorden() // creo el preorden
    {
        if (raiz == null) // si no existe el arbol aviso que no existe
        {
            Console.WriteLine("El árbol está vacío.");
            return;
        }
        PreordenRec(raiz, 0); // si existe paso al siguiente metodo que recorre todo el arbol
        Console.WriteLine();
    }

    private void PreordenRec(Nodo nodo, int nivel) // metodo para recorrer todo el arbol
    {
        if (nodo == null) // si el nodo en el que estoy es null, retorno
        return; 
        Console.WriteLine(new string('-', nivel * 2) + nodo.Nombre); // dependiendo en el nivel que este en el arbol voy imprimiendo guiones para poder distinguir padres e hijos
        PreordenRec(nodo.Padre1, nivel + 1); //paso al siguiente nodo padre1, asi sucesivamente recursivamente hasta que el primer if se cumpla
        PreordenRec(nodo.Padre2, nivel + 1); // al recorres todos los padre1 ahora recorro los padre2 recursivamente;
    }

    public void MostrarPadres(string nombre)
    {
        Nodo nodo = Buscar(raiz, nombre); //busco el nombre
        if (nodo == null) // si no existe el nombre aviso que no existe
        {
            Console.WriteLine($"No se encontró el nodo '{nombre}'.");
            return;
        }

        Console.WriteLine($"Padres de {nombre}:"); // imprimo los padres del nodo, dependiendo cuantos padres tenga
        if (nodo.Padre1 != null)
            Console.WriteLine($"- {nodo.Padre1.Nombre}");
        if (nodo.Padre2 != null)
            Console.WriteLine($"- {nodo.Padre2.Nombre}");
        if (nodo.Padre1 == null && nodo.Padre2 == null)
            Console.WriteLine("No tiene padres registrados."); // si no tiene padres, escribo q no tiene
    }
}

class Program // mi clase donde se va a ejecutar todo el programa
{
    static void Main() // mi metodo principal
    {
        ArbolGenealogico arbol = new ArbolGenealogico(); // creo la clase arbol
        int opcion;
        
        do // con un do while hago el menu
        {
            Console.Clear(); // borro cada vez que termine de hacer una opción
            Console.WriteLine("\nMenú:");
            Console.WriteLine("1. Insertar un nodo");               // creo mi menu con 4 diferentes opciones
            Console.WriteLine("2. Imprimir árbol en preorden");
            Console.WriteLine("3. Buscar padres de un miembro");
            Console.WriteLine("4. Salir");
            Console.Write("Seleccione una opción: ");

            if (!int.TryParse(Console.ReadLine(), out opcion)) // verifico si ingrese un entero en mi variable opcion, que me ayudara a elegir que opcion del menu quiero
            {
                Console.WriteLine("Opción inválida. Inténtelo de nuevo.");
                continue;
            }

            switch (opcion) // creo mi switch para ir caso por caso, es decir opcion por opcion
            {
                case 1:// opcion para insertar un nuevo padre
                    Console.Write("Ingrese el nombre del hijo: ");   // ingreso el nombre del hijo
                    string hijo = Console.ReadLine();
                
                    
                        Console.Write("Ingrese el nombre del padre/madre: "); // ingreso el nombre del padre
                        string padre = Console.ReadLine();
                        arbol.Insertar(hijo, padre); // los inserto, y si por ejemplo no existen, en la función insertar se mandara un mensaje
                    
                    break;
                case 2: // imprimir en preorden
                    arbol.Preorden(); // llamo la función preorden
                    break;
                case 3: // mostrar padres de un hijo
                    Console.Write("Ingrese el nombre de la persona a buscar: ");
                    string nombre = Console.ReadLine();
                    arbol.MostrarPadres(nombre); // uso la función mostrar padres
                    break;
                case 4: // para salir del programa
                    Console.WriteLine("Saliendo del programa..."); 
                    break;
                default:
                    Console.WriteLine("Opción inválida. Intente de nuevo."); // mi condición default por si algo sale mal
                    break;
            }

            if (opcion != 4)  // verifico si quiero seguir con el programa
            {
                Console.Write("¿Desea continuar? (s/n): ");
                string continuar = Console.ReadLine().ToLower();
                if (continuar != "s")
                    break;
            }
        } while (opcion != 4);
    }
}
