using System;
using System.Collections.Generic;
using System.Linq;
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

        public List<Usuario> Clientes
        {
            get { return _usuarios; }
        }
        public List<Articulo> Articulo
        {
            get { return _articulos; }
        }
        public Sistema()
        {
            PrecargarUsuariosClientes();
            PrecargarUsuarios();
            PrecargarArticulos();
            PrecargarVentas();
            PrecargarSubastas();
            PrecargarOfertasASubastas();
        }
        //Usuarios
        private void AltaUsuario(string nombre, string apellido, string email, string contraseña) //Precarga de datos usuario
        {
            Usuario usuario = new Usuario(nombre, apellido, email, contraseña);
            usuario.Validar();
            if (!_usuarios.Contains(usuario) && _usuarios != null)
            {
                _usuarios.Add(usuario);
            }
        }

        private void AltaUsuarioCliente(string nombre, string apellido, string email, string contraseña, int saldoDisponible) //Precarga de datos Cliente
        {
            Cliente usuarioCliente = new Cliente(nombre, apellido, email, contraseña, saldoDisponible);
            usuarioCliente.Validar();
            if (!_usuarios.Contains(usuarioCliente))
            {
                _usuarios.Add(usuarioCliente);
            }
        }

        private Cliente BuscarCliente(int idCliente) //Busca clientes especificos
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

        private Usuario BuscarUsuario(int id) //Busca Usuarios especificos
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
        public void AltaArticulos(string nombre, string categoria, int precioVenta) //Precarga de articulos y guarda datos ingresados desde program.
        {
            Articulo articulo = new Articulo(nombre, categoria, precioVenta);
            articulo.Validar();
            _articulos.Add(articulo);

        }

        private List<List<Articulo>> ListarArticulosPorTipo() //Genera de grupos articulos para crear/precargar publicaciones
        {
            List<Articulo> articulosOficina = new List<Articulo>();
            List<Articulo> ropayCalzados = new List<Articulo>();
            List<Articulo> accesoriosyJoyeria = new List<Articulo>();
            List<Articulo> mueblesyIluminacion = new List<Articulo>();
            List<Articulo> electronica = new List<Articulo>();
            List<Articulo> decoracion = new List<Articulo>();
            List<Articulo> cocina = new List<Articulo>();
            List<Articulo> papeleria = new List<Articulo>();
            List<Articulo> electrodomesticos = new List<Articulo>();

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
                else if (categoria.Contains("ACCESORIOS") || categoria.Contains("JOYERIA"))
                {
                    accesoriosyJoyeria.Add(articulo);
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
            }

            return new List<List<Articulo>>()
                {
                    articulosOficina,
                    ropayCalzados,
                    accesoriosyJoyeria,
                    mueblesyIluminacion,
                    electronica,
                    decoracion,
                    cocina,
                    papeleria,
                    electrodomesticos
                };
        }

        public List<Articulo> BuscarArticulos(string tipo) //Busca artioculos por un string y los muestra, llamado desde program
        {
            List<Articulo> articulo = new List<Articulo>();

            for (int i = 0; i < _articulos.Count; i++)
            {
                if (_articulos[i].Categoria.ToUpper().Trim() == tipo.ToUpper().Trim())
                {
                    articulo.Add(_articulos[i]);
                }
            }
            return articulo;
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
        private void AgregarOfertaASubasta(int idCliente, int monto, DateTime fecha, int idPublicacion) // Envia parametros a subasta llamando al metodo. Luego desde subasta agrega oferta.
        {
            Subasta subasta = BuscarSubastas(idPublicacion);
            Cliente cliente = BuscarCliente(idCliente);

            if (cliente != null && subasta != null)
            {
                subasta.AgregarOferta(cliente, monto, fecha);
            }
        }

        private Subasta BuscarSubastas(int idPublicacion) // Busca Subastas especificas
        {
            Subasta subasta = null;
            int i = 0;
            while (i < _publicaciones.Count && subasta == null)
            {
                if (_publicaciones[i].ID != idPublicacion && _publicaciones[i] is Subasta)
                {
                    subasta = (Subasta)_publicaciones[i];
                }
                i++;
            }
            return subasta;
        }

        public List<Publicacion> BuscarEntreDosFechas(DateTime fechaInicial, DateTime fechaFinal) //Recorre list publicaciones entre dos fechas y devuelve una nueva filtrada. Llamada desde program
        {
            List<Publicacion> publicacionesPorNombre = new List<Publicacion>();

            foreach (Publicacion publicacion in _publicaciones)
            {
                if (publicacion.FechaDePublicacion > fechaInicial && publicacion.FechaFinalizacion < fechaFinal)
                {
                    publicacionesPorNombre.Add(publicacion);
                }
            }
            return publicacionesPorNombre;
        }

        // PRECARGAS
        private void PrecargarUsuariosClientes()
        {
            AltaUsuarioCliente("Jose", "Pedro", "JP@gmail.com", "1234", 2000);
            AltaUsuarioCliente("Juan", "Pérez", "juan.perez@email.com", "Contraseña123", 1500);
            AltaUsuarioCliente("María", "Gómez", "maria.gomez@email.com", "ClaveSegura456", 2500);
            AltaUsuarioCliente("Carlos", "Ramírez", "carlos.ramirez@email.com", "MiContraseña789", 3000);
            AltaUsuarioCliente("Ana", "Martínez", "ana.martinez@email.com", "SuperClave987", 1000);
            AltaUsuarioCliente("Luis", "Hernández", "luis.hernandez@email.com", "Pass1234", 5000);
            AltaUsuarioCliente("Sofía", "López", "sofia.lopez@email.com", "Clave4567", 2000);
            AltaUsuarioCliente("Diego", "Sánchez", "diego.sanchez@email.com", "Segura1234", 3500);
            AltaUsuarioCliente("Lucía", "Fernández", "lucia.fernandez@email.com", "Seguridad567", 4000);
            AltaUsuarioCliente("Pedro", "Rodríguez", "pedro.rodriguez@email.com", "ClaveSegura123", 4500);
        }
        private void PrecargarUsuarios()
        {
            AltaUsuario("Lautaro", "Alvarez", "LA@gmail.com", "1234");
            AltaUsuario("Pepe", "Alvarez", "PA@gmail.com", "1234");
        }
        private void PrecargarSubastas()
        {
            List<List<Articulo>> listasDeArticulos = ListarArticulosPorTipo();
            AltaSubastas("Equipo de Oficina", "ABIERTA", new DateTime(2024, 6, 18), listasDeArticulos[0], null, null, new DateTime(2024, 7, 18));
            AltaSubastas("Articulos de Cocina", "ABIERTA", new DateTime(2024, 9, 5), listasDeArticulos[6], null, null, new DateTime(2024, 10, 5));
            AltaSubastas("Papeleria y mas", "ABIERTA", new DateTime(2024, 11, 22), listasDeArticulos[7], null, null, new DateTime(2024, 12, 22));
            AltaSubastas("Dispositivos Electronicos Personales", "ABIERTA", new DateTime(2023, 7, 30), listasDeArticulos[4], null, null, new DateTime(2023, 8, 30));
            AltaSubastas("Equipo de Oficina", "ABIERTA", new DateTime(2024, 5, 14), listasDeArticulos[0], null, null, new DateTime(2024, 6, 14));
            AltaSubastas("Ropa y Calzado", "ABIERTA", new DateTime(2024, 12, 3), listasDeArticulos[1], null, null, new DateTime(2025, 1, 3));
            AltaSubastas("Dispositivos Electronicos Personales", "ABIERTA", new DateTime(2024, 2, 20), listasDeArticulos[4], null, null, new DateTime(2024, 3, 20));
            AltaSubastas("Muebles y Iluminacion", "ABIERTA", new DateTime(2023, 10, 11), listasDeArticulos[3], null, null, new DateTime(2023, 11, 11));
            AltaSubastas("Joyeria y Accesorios Personales", "ABIERTA", new DateTime(2024, 8, 7), listasDeArticulos[2], null, null, new DateTime(2024, 9, 7));
            AltaSubastas("Dispositivos Electronicos Personales", "ABIERTA", new DateTime(2024, 3, 29), listasDeArticulos[4], null, null, new DateTime(2024, 4, 29));
        }
        private void PrecargarOfertasASubastas()
        {
            AgregarOfertaASubasta(3, 1500, new DateTime(2024, 04, 15), 7);
            AgregarOfertaASubasta(1, 2000, new DateTime(2024, 05, 20), 2);
        }
        private void PrecargarVentas()
        {
            List<List<Articulo>> listasDeArticulos = ListarArticulosPorTipo();
            AltaVentas("Ropa y Calzado", "ABIERTA", new DateTime(2024, 10, 1), listasDeArticulos[1], BuscarCliente(1), BuscarUsuario(1), new DateTime(2024, 11, 1), true);
            AltaVentas("Papeleria y mas", "ABIERTA", new DateTime(2023, 5, 15), listasDeArticulos[7], BuscarCliente(0), BuscarUsuario(0), new DateTime(2023, 6, 15), false);
            AltaVentas("Joyeria y Accesorios Personales", "ABIERTA", new DateTime(2024, 1, 10), listasDeArticulos[2], BuscarCliente(8), BuscarUsuario(1), new DateTime(2024, 2, 10), true);
            AltaVentas("Electrodomesticos del Hogar", "ABIERTA", new DateTime(2024, 8, 21), listasDeArticulos[8], BuscarCliente(9), BuscarUsuario(1), new DateTime(2024, 9, 21), false);
            AltaVentas("Muebles y Iluminacion", "ABIERTA", new DateTime(2022, 12, 30), listasDeArticulos[3], BuscarCliente(7), BuscarUsuario(0), new DateTime(2023, 1, 30), true);
            AltaVentas("Dispositivos Electronicos Personales", "ABIERTA", new DateTime(2024, 3, 1), listasDeArticulos[4], BuscarCliente(6), BuscarUsuario(1), new DateTime(2024, 4, 1), false);
            AltaVentas("Articulos de Decoracion ", "ABIERTA", new DateTime(2024, 7, 10), listasDeArticulos[5], BuscarCliente(5), BuscarUsuario(0), new DateTime(2024, 8, 10), true);
            AltaVentas("Articulos de Cocina", "ABIERTA", new DateTime(2024, 2, 5), listasDeArticulos[6], BuscarCliente(4), BuscarUsuario(0), new DateTime(2024, 3, 5), false);
            AltaVentas("Muebles y Iluminacion", "ABIERTA", new DateTime(2023, 9, 14), listasDeArticulos[3], BuscarCliente(3), BuscarUsuario(1), new DateTime(2023, 10, 14), true);
            AltaVentas("Equipo de Oficina", "ABIERTA", new DateTime(2024, 4, 25), listasDeArticulos[0], BuscarCliente(2), BuscarUsuario(0), new DateTime(2024, 5, 25), false);
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
        }
    }
}
