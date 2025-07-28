using LogicaNegocio;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace InterfazUsuario
{
    internal class Program
    {
        private static Sistema miSistema = new Sistema();
        static void Main(string[] args)
        {
            int opcion = -1;
            while (opcion != 0)
            {
                Console.WriteLine("-> Enter para continuar");
                Console.ReadKey();
                Console.Clear();
                MostrarMenu();
                Console.WriteLine("Ingrese opcion ");
                int.TryParse(Console.ReadLine(), out opcion);
                Console.Clear();
                OpcionMenu(opcion);

            }
        }

        static void MostrarClientes() //Recorre Clientes y los muestra
        {
            try
            {
                List<Usuario> ListaDeClientes = miSistema.Clientes;
                for (int i = 0; i < ListaDeClientes.Count; i++)
                {
                    if (ListaDeClientes[i] is Cliente && ListaDeClientes != null)
                    {
                        Console.WriteLine(ListaDeClientes[i]);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

        }

        static void BuscarArticulosPorNombreDeCategoria()//Verifica si los datos ingresados son correctos, luego recorre un metodo en sistema que devuelve la lista
        {
            try
            {
                Console.WriteLine("Ingrese Nombre de la categoria");
                string nombre = Console.ReadLine();
                bool encontrado = false;
                if (string.IsNullOrEmpty(nombre))
                {
                    Console.WriteLine("Escriba correctamente la categoria");
                }
                else
                {
                    List<Articulo> listaDeArticulos = miSistema.BuscarArticulos(nombre);
                    foreach (Articulo lista in listaDeArticulos)
                    {
                        Console.WriteLine(lista);
                        encontrado = true;
                    }
                    if (!encontrado)
                    {
                        Console.WriteLine("Articulo no encontrado");

                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        static void AltaArticulo()//Verifica si los datos ingresados son correctos, luego los guarda en una lista por metodo en sistema
        {
            try
            {
                Console.WriteLine("Ingrese Nombre del Articulo");
                string nombre = Console.ReadLine();
                Console.WriteLine("Ingrese categoria");
                string categoria = Console.ReadLine();
                Console.WriteLine("Ingrese precio de venta");
                int.TryParse(Console.ReadLine(), out int precioVenta);
                if (string.IsNullOrEmpty(nombre) && string.IsNullOrEmpty(categoria) && precioVenta < 0)
                {
                    Console.WriteLine("Error -> Complete todos los campos correctamente.");
                }
                else
                {
                    miSistema.AltaArticulos(nombre, categoria, precioVenta);
                    Console.WriteLine("Articulo ingresado en el sistema correctamente");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

        }

        static void BuscarPublicacionesPorFechas()//Verifica si los datos ingresados son correctos, luego recorre un metodo en sistema que devuelve la lista
        {
            try
            {
                Console.WriteLine("Ingrese fecha inicial; Formato yyyy/mm/dd");
                DateTime.TryParse(Console.ReadLine(), out DateTime fechaInicial);
                Console.WriteLine("Ingrese fecha final; Formato yyyy/mm/dd");
                DateTime.TryParse(Console.ReadLine(), out DateTime fechaFinal);
                bool encontrado = false;
                if (fechaInicial < DateTime.MinValue || fechaFinal < DateTime.MinValue || fechaFinal < fechaInicial)
                {
                    Console.WriteLine("Error -> Complete todos los campos correctamente.");
                }
                else
                {
                    List<Publicacion> listaDePublicaciones = miSistema.BuscarEntreDosFechas(fechaInicial, fechaFinal);
                    foreach (Publicacion publicacion in listaDePublicaciones)
                    {
                        Console.WriteLine(publicacion);
                        encontrado = true;
                    }
                    if (!encontrado)
                    {
                        Console.WriteLine("No existen publicaciones en esas fechas");

                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        static void MostrarMenu()
        {
            Console.WriteLine("1 - Listado de Clientes");
            Console.WriteLine("2 - Buscar articulos por categoria");
            Console.WriteLine("3 - Dar de Alta un articulo");
            Console.WriteLine("4 - Buscar publicaciones entre dos fechas");
            Console.WriteLine("0 - Salir");
        }
        static void OpcionMenu(int opcion)
        {
            switch (opcion)
            {
                case 1:
                    MostrarClientes();
                    ;
                    break;
                case 2:
                    BuscarArticulosPorNombreDeCategoria()
                    ;
                    break;
                case 3:
                    AltaArticulo()
                    ;
                    break;
                case 4:
                    BuscarPublicacionesPorFechas()
                    ;
                    break;
                default:
                    Console.Clear();
                    break;
            }
        }
    }
}
