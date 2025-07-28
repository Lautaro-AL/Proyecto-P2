using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace LogicaNegocio
{
    public class Sistema
    {
        private List<Usuario> _usuarios = new List<Usuario>();
        private List<Articulo> _articulos = new List<Articulo>();
        private List<Publicacion> _publicaciones = new List<Publicacion>();
        private static Sistema _instancia;

        public List<Publicacion> Publicacion
        {
            get { return _publicaciones; }
        }
        public static Sistema Instancia
        {
            get
            {
                if (_instancia == null)
                {
                    _instancia = new Sistema();
                }
                return _instancia;
            }
        }
        public Sistema()
        {
            PrecargarUsuariosClientes();
            PrecargarAdmins();
            PrecargarArticulos();
            PrecargarSubastas();
            PrecargarOfertasASubastas();
            PrecargarVentas();
        }
        //Usuarios
        private void AltaUsuario(string nombre, string apellido, string email, string contraseña) //Precarga de datos Admin
        {
            Administrador admin = new Administrador(nombre, apellido, email, contraseña);
            admin.Validar();
            if (!_usuarios.Contains(admin) && _usuarios != null)
            {
                _usuarios.Add(admin);
            }
        }

        public void AltaUsuarioCliente(string nombre, string apellido, string email, string contraseña, int saldoDisponible) //Precarga de datos Cliente
        {
            Cliente usuarioCliente = new Cliente(nombre, apellido, email, contraseña, saldoDisponible);
            foreach (Usuario u in _usuarios)
            {
                if (email.Equals(u.Email))
                {
                    throw new Exception("Email ya registrado.");
                }
            }
            usuarioCliente.Validar();
            if (!_usuarios.Contains(usuarioCliente))
            {
                _usuarios.Add(usuarioCliente);
            }
        }


        public Cliente BuscarCliente(int idCliente) //Busca clientes especificos
        {
            Cliente clienteBuscado = null;
            int i = 0;
            while (i < _usuarios.Count && clienteBuscado == null)
            {
                if (_usuarios[i].ID == idCliente)
                {
                    if (_usuarios[i] is Cliente cliente)
                    {
                        clienteBuscado = cliente;
                    }
                }
                i++;
            }
            return clienteBuscado;

        }

        public Usuario BuscarUsuario(int id) //Busca Usuarios especificos
        {
            Usuario usuario = null;
            int i = 0;
            while (i < _usuarios.Count && usuario == null)
            {
                if (_usuarios[i].ID != id)
                {
                    usuario = _usuarios[i];
                }
                i++;
            }
            return usuario;
        }

        //Articulos
        private void AltaArticulos(string nombre, string categoria, int precioVenta) //Precarga de articulos y guarda datos ingresados desde program.
        {
            Articulo articulo = new Articulo(nombre, categoria, precioVenta);
            articulo.Validar();
            //realizar contains y verificar 
            if (!_articulos.Contains(articulo))
            {
                _articulos.Add(articulo);
            }


        }

        private List<List<Articulo>> ListarArticulosPorTipo() //Genera de grupos articulos para crear/precargar publicaciones
        {

            List<Articulo> articulosOficina = new List<Articulo>();
            List<Articulo> ropayCalzados = new List<Articulo>();
            List<Articulo> accesorios = new List<Articulo>();
            List<Articulo> mueblesyIluminacion = new List<Articulo>();
            List<Articulo> electronica = new List<Articulo>();
            List<Articulo> decoracion = new List<Articulo>();
            List<Articulo> cocina = new List<Articulo>();
            List<Articulo> papeleria = new List<Articulo>();
            List<Articulo> electrodomesticos = new List<Articulo>();
            List<Articulo> Joyeria = new List<Articulo>();

            List<Articulo> deportes = new List<Articulo>();
            List<Articulo> herramientas = new List<Articulo>();
            List<Articulo> jardineria = new List<Articulo>();
            List<Articulo> libros = new List<Articulo>();
            List<Articulo> belleza = new List<Articulo>();
            List<Articulo> fitness = new List<Articulo>();
            List<Articulo> arte = new List<Articulo>();
            List<Articulo> energia = new List<Articulo>();
            List<Articulo> hogar = new List<Articulo>();
            List<Articulo> viajes = new List<Articulo>();



            foreach (Articulo articulo in _articulos)
            {
                string categoria = articulo.Categoria.ToUpper().Trim();

                if (categoria.Contains("OFICINA"))
                {
                    articulosOficina.Add(articulo);
                }
                else if (categoria.Contains("ROPA") || categoria.Contains("CALZADO"))
                {
                    ropayCalzados.Add(articulo);
                }
                else if (categoria.Contains("ACCESORIOS"))
                {
                    accesorios.Add(articulo);
                }
                else if (categoria.Contains("MUEBLES") || categoria.Contains("ILUMINACION"))
                {
                    mueblesyIluminacion.Add(articulo);
                }
                else if (categoria.Contains("ELECTRONICA"))
                {
                    electronica.Add(articulo);
                }
                else if (categoria.Contains("DECORACION"))
                {
                    decoracion.Add(articulo);
                }
                else if (categoria.Contains("COCINA"))
                {
                    cocina.Add(articulo);
                }
                else if (categoria.Contains("PAPELERIA"))
                {
                    papeleria.Add(articulo);
                }
                else if (categoria.Contains("ELECTRODOMESTICOS"))
                {
                    electrodomesticos.Add(articulo);
                }
                else if (categoria.Contains("JOYERIA"))
                {
                    Joyeria.Add(articulo);
                }
                else if (categoria.Contains("DEPORTES"))
                {
                    deportes.Add(articulo);
                }
                else if (categoria.Contains("HERRAMIENTAS"))
                {
                    herramientas.Add(articulo);
                }
                else if (categoria.Contains("LIBROS"))
                {
                    libros.Add(articulo);
                }
                else if (categoria.Contains("BELLEZA"))
                {
                    belleza.Add(articulo);
                }
                else if (categoria.Contains("FITNESS"))
                {
                    fitness.Add(articulo);
                }
                else if (categoria.Contains("ARTE"))
                {
                    arte.Add(articulo);
                }
                else if (categoria.Contains("ENERGÍA"))
                {
                    energia.Add(articulo);
                }
                else if (categoria.Contains("HOGAR"))
                {
                    hogar.Add(articulo);
                }
                else if (categoria.Contains("VIAJES"))
                {
                    viajes.Add(articulo);
                }

                else if (categoria.Contains("JARDINERIA"))
                {
                    jardineria.Add(articulo);
                }


            }

            return new List<List<Articulo>>()
                {
                    articulosOficina,
                    ropayCalzados,
                    accesorios,
                    mueblesyIluminacion,
                    electronica,
                    decoracion,
                    cocina,
                    papeleria,
                    electrodomesticos,
                    Joyeria,
                    deportes,
                    herramientas,
                    jardineria,
                    libros,
                    belleza,
                    fitness,
                    arte,
                    energia,
                    hogar,
                    viajes
    };
        }

        //VENTAS - SUBASTAS
        private void AltaVentas(string nombre, string estado, DateTime fechaPublicacion, List<Articulo> articulo, Cliente usuarioCompra, Usuario usuarioAdmin, DateTime fechaFinalizacion, bool oferta) //Precarga Ventas
        {
            Venta ventas = new Venta(nombre, estado, fechaPublicacion, articulo, usuarioCompra, usuarioAdmin, fechaFinalizacion, oferta);
            ventas.Validar();
            if (!_publicaciones.Contains(ventas))
            {
                _publicaciones.Add(ventas);
            }
        }

        private void AltaSubastas(string nombre, string estado, DateTime fechaPublicacion, List<Articulo> articulo, Cliente usuarioCompra, Usuario usuarioAdmin, DateTime fechaFinalizacion) //Precarga Subastas
        {
            Subasta subasta = new Subasta(nombre, estado, fechaPublicacion, articulo, usuarioCompra, usuarioAdmin, fechaFinalizacion);
            subasta.Validar();
            if (!_publicaciones.Contains(subasta))
            {
                _publicaciones.Add(subasta);
            }
        }
        public void AgregarOfertaASubasta(int idCliente, int monto, DateTime fecha, int idPublicacion) // Envia parametros a subasta llamando al metodo. Luego desde subasta agrega oferta.
        {
            Subasta subasta = BuscarSubastas(idPublicacion);
            Cliente cliente = BuscarCliente(idCliente);


            if (cliente != null && subasta != null)
            {
                subasta.AgregarOferta(cliente, monto, fecha);
            }
        }

        public Subasta BuscarSubastas(int idPublicacion) // Busca Subastas especificas
        {
            Subasta subasta = null;
            int i = 0;
            while (i < _publicaciones.Count && subasta == null)
            {
                if (_publicaciones[i].ID == idPublicacion && _publicaciones[i] is Subasta)
                {
                    subasta = (Subasta)_publicaciones[i];
                }
                i++;
            }
            return subasta;
        }

        public Venta BuscarVentas(int idPublicacion) // Busca Ventas especificas
        {
            Venta venta = null;
            int i = 0;
            while (i < _publicaciones.Count && venta == null)
            {
                if (_publicaciones[i].ID == idPublicacion && _publicaciones[i] is Venta)
                {
                    venta = (Venta)_publicaciones[i];
                }
                i++;
            }
            return venta;
        }

        public void AgregarSaldo(int id, int saldo) //Busca el cliente por id y agrega el saldo
        {
            Cliente cliente = BuscarCliente(id);
            cliente.Validar();
            if (cliente != null)
            {
                cliente.AgregarSaldoCliente(saldo);
            }
        }

        public Usuario BuscarUsuarioParaLogin(string correo, string password) //Busca usuario por corre y password para login
        {
            Usuario ClienteBuscado = null;
            int i = 0;
            while (i < _usuarios.Count && ClienteBuscado == null)
            {
                if (_usuarios[i].Email.Trim().ToUpper() == correo.Trim().ToUpper() && _usuarios[i].Password == password)
                {
                    ClienteBuscado = _usuarios[i];
                }
                i++;
            }
            return ClienteBuscado;
        }
        public List<Publicacion> SubastasOrdenadasPorFecha() //Ordena Subastas por Fecha
        {
            _publicaciones.Sort();
            return _publicaciones;
        }

        public void CerrarSubasta(int id, int idUsuario) //Cierra una subasta metodo polimorfico
        {
            Subasta subasta = BuscarSubastas(id);
            subasta.CerrarPublicacion(id, idUsuario);
        }
        public void Comprar(int id, int idCliente)
        {
            Venta venta = BuscarVentas(id);
            venta.CerrarPublicacion(id, idCliente);
        }

        // PRECARGAS
        private void PrecargarUsuariosClientes()
        {
            AltaUsuarioCliente("Jose", "Pedro", "JP@gmail.com", "12345678", 2000);
            AltaUsuarioCliente("Juan", "Pérez", "juan.perez@email.com", "Contraseña123", 1500);
            AltaUsuarioCliente("María", "Gómez", "maria.gomez@email.com", "ClaveSegura456", 2500);
            AltaUsuarioCliente("Carlos", "Ramírez", "carlos.ramirez@email.com", "MiContraseña789", 3000);
            AltaUsuarioCliente("Ana", "Martínez", "ana.martinez@email.com", "SuperClave987", 1000);
            AltaUsuarioCliente("Luis", "Hernández", "luis.hernandez@email.com", "Pass1234", 5000);
            AltaUsuarioCliente("Sofía", "López", "sofia.lopez@email.com", "Clave4567", 2000);
            AltaUsuarioCliente("Diego", "Sánchez", "dS@gmail.com", "Segura1234", 3500);
            AltaUsuarioCliente("Lucía", "Fernández", "lucia.fernandez@email.com", "Seguridad567", 4000);
            AltaUsuarioCliente("Pedro", "Rodríguez", "pedro.rodriguez@email.com", "ClaveSegura123", 4500);
        }
        private void PrecargarAdmins()
        {
            AltaUsuario("Lautaro", "Alvarez", "LA@gmail.com", "12345678");
            AltaUsuario("Pepe", "Alvarez", "PA@gmail.com", "12345678");
        }

        private void PrecargarSubastas()
        {
            List<List<Articulo>> listasDeArticulos = ListarArticulosPorTipo();
            AltaSubastas("Equipo Deportivo", "CERRADA", new DateTime(2024, 6, 10), listasDeArticulos[10], null, null, new DateTime(2024, 7, 10));
            AltaSubastas("Herramientas para el Hogar", "ABIERTA", new DateTime(2024, 6, 12), listasDeArticulos[11], null, null, new DateTime(2024, 7, 12));
            AltaSubastas("Artículos de Jardinería", "ABIERTA", new DateTime(2024, 6, 14), listasDeArticulos[12], null, null, new DateTime(2024, 7, 14));
            AltaSubastas("Colección de Libros", "ABIERTA", new DateTime(2024, 6, 16), listasDeArticulos[13], null, null, new DateTime(2024, 7, 16));
            AltaSubastas("Belleza y Cuidado Personal", "ABIERTA", new DateTime(2024, 6, 18), listasDeArticulos[14], null, null, new DateTime(2024, 7, 18));
            AltaSubastas("Fitness y Bienestar", "ABIERTA", new DateTime(2024, 6, 20), listasDeArticulos[15], null, null, new DateTime(2024, 7, 20));
            AltaSubastas("Kit de Arte", "ABIERTA", new DateTime(2024, 6, 22), listasDeArticulos[16], null, null, new DateTime(2024, 7, 22));
            AltaSubastas("Energía Sostenible", "ABIERTA", new DateTime(2024, 6, 24), listasDeArticulos[17], null, null, new DateTime(2024, 7, 24));
            AltaSubastas("Decoración para el Hogar", "ABIERTA", new DateTime(2024, 6, 26), listasDeArticulos[18], null, null, new DateTime(2024, 7, 26));
            AltaSubastas("Accesorios de Viaje", "ABIERTA", new DateTime(2024, 6, 28), listasDeArticulos[19], null, null, new DateTime(2024, 7, 28));

        }
        private void PrecargarOfertasASubastas()
        {
            AgregarOfertaASubasta(3, 1500, new DateTime(2024, 11, 15), 1);
            AgregarOfertaASubasta(1, 2000, new DateTime(2024, 11, 20), 0);
        }
        private void PrecargarVentas()
        {
            List<List<Articulo>> listasDeArticulos = ListarArticulosPorTipo();
            AltaVentas("Equipo de Oficina", "ABIERTA", new DateTime(2024, 4, 25), listasDeArticulos[0], BuscarCliente(2), BuscarUsuario(0), new DateTime(2024, 5, 25), false);
            AltaVentas("Ropa y Calzado", "ABIERTA", new DateTime(2024, 10, 1), listasDeArticulos[1], BuscarCliente(1), BuscarUsuario(1), new DateTime(2024, 11, 1), true);
            AltaVentas("Accesorios Personales", "ABIERTA", new DateTime(2024, 1, 10), listasDeArticulos[2], BuscarCliente(8), BuscarUsuario(1), new DateTime(2024, 2, 10), true);
            AltaVentas("Muebles y Iluminacion", "ABIERTA", new DateTime(2022, 12, 30), listasDeArticulos[3], BuscarCliente(7), BuscarUsuario(0), new DateTime(2023, 1, 30), true);
            AltaVentas("Dispositivos Electronicos", "ABIERTA", new DateTime(2024, 3, 1), listasDeArticulos[4], BuscarCliente(6), BuscarUsuario(1), new DateTime(2024, 4, 1), false);
            AltaVentas("Articulos de Decoracion ", "ABIERTA", new DateTime(2024, 7, 10), listasDeArticulos[5], BuscarCliente(5), BuscarUsuario(0), new DateTime(2024, 8, 10), true);
            AltaVentas("Articulos de Cocina", "ABIERTA", new DateTime(2024, 2, 5), listasDeArticulos[6], BuscarCliente(4), BuscarUsuario(0), new DateTime(2024, 3, 5), false);
            AltaVentas("Papeleria y mas", "ABIERTA", new DateTime(2023, 5, 15), listasDeArticulos[7], BuscarCliente(0), BuscarUsuario(0), new DateTime(2023, 6, 15), false);
            AltaVentas("Electrodomesticos del Hogar", "ABIERTA", new DateTime(2024, 8, 21), listasDeArticulos[8], BuscarCliente(9), BuscarUsuario(1), new DateTime(2024, 9, 21), true);
            AltaVentas("Joyeria", "ABIERTA", new DateTime(2023, 9, 14), listasDeArticulos[9], BuscarCliente(3), BuscarUsuario(1), new DateTime(2023, 10, 14), true);
        }

        private void PrecargarArticulos()
        {
            AltaArticulos("Camiseta basica", "Ropa", 150);
            AltaArticulos("Pantalon vaquero", "Ropa", 600);
            AltaArticulos("Zapatos deportivos", "Calzado", 1200);
            AltaArticulos("Zapatos casuales", "Calzado", 1200);
            AltaArticulos("Championes de futbol", "Calzado", 1200);
            AltaArticulos("Sombrero de paja", "Accesorios", 200);
            AltaArticulos("Bolso de cuero", "Accesorios", 900);
            AltaArticulos("Reloj de pulsera", "Joyeria", 2500);
            AltaArticulos("Anillo de plata", "Joyeria", 800);
            AltaArticulos("Collar de perlas", "Joyeria", 3000);
            AltaArticulos("Mesa de comedor", "Muebles", 4500);
            AltaArticulos("Silla de oficina", "Muebles", 1500);
            AltaArticulos("Sofa de tres plazas", "Muebles", 8000);
            AltaArticulos("Lampara de pie", "Iluminacion", 1200);
            AltaArticulos("Lampara de escritorio", "Iluminacion", 600);
            AltaArticulos("Televisor 55 pulgadas", "Electronica", 15000);
            AltaArticulos("Computadora portatil", "Electronica", 20000);
            AltaArticulos("Telefono movil", "Electronica", 10000);
            AltaArticulos("Tablet 10 pulgadas", "Electronica", 7000);
            AltaArticulos("Auriculares inalambricos", "Electronica", 1200);
            AltaArticulos("Camara fotografica", "Electronica", 9500);
            AltaArticulos("Refrigerador de dos puertas", "Electrodomesticos", 12000);
            AltaArticulos("Lavadora automatica", "Electrodomesticos", 8500);
            AltaArticulos("Microondas digital", "Electrodomesticos", 2500);
            AltaArticulos("Licuadora de 12 velocidades", "Electrodomesticos", 1500);
            AltaArticulos("Cafetera de capsulas", "Electrodomesticos", 3000);
            AltaArticulos("Plancha de vapor", "Electrodomesticos", 1200);
            AltaArticulos("Freidora de aire", "Electrodomesticos", 3500);
            AltaArticulos("Espejo decorativo", "Decoracion", 2000);
            AltaArticulos("Cuadro abstracto", "Decoracion", 1800);
            AltaArticulos("Cortinas para sala", "Decoracion", 1200);
            AltaArticulos("Alfombra persa", "Decoracion", 4500);
            AltaArticulos("Cojines decorativos", "Decoracion", 800);
            AltaArticulos("Vela aromatica", "Decoracion", 400);
            AltaArticulos("Jarron de ceramica", "Decoracion", 600);
            AltaArticulos("Set de cuchillos", "Cocina", 2000);
            AltaArticulos("Sarten antiadherente", "Cocina", 1000);
            AltaArticulos("Bateria de cocina", "Cocina", 4500);
            AltaArticulos("Tostadora electrica", "Cocina", 800);
            AltaArticulos("Batidora manual", "Cocina", 1500);
            AltaArticulos("Mochila escolar", "Papeleria", 1200);
            AltaArticulos("Cuadernos de espiral", "Papeleria", 100);
            AltaArticulos("Boligrafo de gel", "Papeleria", 50);
            AltaArticulos("Calculadora cientifica", "Papeleria", 600);
            AltaArticulos("Resaltadores fluorescentes", "Papeleria", 150);
            AltaArticulos("Silla gamer", "Oficina", 6000);
            AltaArticulos("Escritorio de madera", "Oficina", 3000);
            AltaArticulos("Monitor 24 pulgadas", "Oficina", 5000);
            AltaArticulos("Teclado mecanico", "Oficina", 1500);
            AltaArticulos("Raton inalambrico", "Oficina", 800);
            AltaArticulos("Bicicleta de montaña", "Deportes", 8000);
            AltaArticulos("Balón de fútbol", "Deportes", 800);
            AltaArticulos("Martillo de carpintero", "Herramientas", 600);
            AltaArticulos("Caja de herramientas", "Herramientas", 2500);
            AltaArticulos("Parrilla para asados", "Jardinería", 4000);
            AltaArticulos("Tijeras de podar", "Jardinería", 1200);
            AltaArticulos("Libro de cocina", "Libros", 500);
            AltaArticulos("Novela de ciencia ficción", "Libros", 800);
            AltaArticulos("Crema hidratante", "Belleza", 900);
            AltaArticulos("Perfume de lujo", "Belleza", 3500);
            AltaArticulos("Pelota de yoga", "Fitness", 1200);
            AltaArticulos("Kit de pintura acrílica", "Arte", 1500);
            AltaArticulos("Lámpara solar para jardín", "Energía", 1800);
            AltaArticulos("Juego de edredón", "Hogar", 3500);
            AltaArticulos("Maleta de viaje", "Viajes", 4500);
            AltaArticulos("Pesas rusas", "Fitness", 2500);
            AltaArticulos("Lienzo en blanco", "Arte", 800);
            AltaArticulos("Cargador portátil solar", "Energía", 2200);
            AltaArticulos("Cortina de baño", "Hogar", 900);
            AltaArticulos("Neceser de viaje", "Viajes", 1200);
        }
    }
}
