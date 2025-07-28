using LogicaNegocio.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio
{
    public abstract class Publicacion : IValidate, IEquatable<Publicacion>, IComparable<Publicacion>
    {
        private int _id;
        private static int s_ultimoID;
        private string _nombre;
        private string _estado;
        private DateTime _fechaDePublicacion;
        private List<Articulo> _articulo = new List<Articulo>();
        private Cliente _clienteCompra;
        private Usuario _finalizoCompra;
        private DateTime _fechaFinalizacion;

        public int ID
        {
            get { return _id; }
        }
        public DateTime FechaDePublicacion
        {
            get { return _fechaDePublicacion; }
        }
        public DateTime FechaFinalizacion
        {
            get { return _fechaFinalizacion; }
            set { _fechaFinalizacion = value; }
        }
        public Usuario FinalizoCompra
        {
            get { return _finalizoCompra; }
            set { _finalizoCompra = value; }
        }
        public Cliente ClienteCompra
        {
            get { return _clienteCompra; }
            set { _clienteCompra = value; }
        }

        public string Nombre
        {
            get { return _nombre; }
        }
        public string Estado
        {
            get { return _estado; }
            set { _estado = value; }
        }

        public List<Articulo> Articulos
        {
            get { return _articulo; }
        }

        public Publicacion(string nombre, string estado, DateTime fechaDePublicacion, List<Articulo> articulo, Cliente clienteCompra, Usuario finalizoCompra, DateTime fechaFinalizacion)
        {
            _nombre = nombre;
            _estado = estado;
            _fechaDePublicacion = fechaDePublicacion;
            _articulo = articulo;
            _clienteCompra = clienteCompra;
            _finalizoCompra = finalizoCompra;
            _fechaFinalizacion = fechaFinalizacion;
            _id = Publicacion.s_ultimoID;
            Publicacion.s_ultimoID++;
        }

        public void Validar()
        {
            if (string.IsNullOrEmpty(_nombre))
            {
                throw new Exception("El nombre no puede estar vacio");
            }
            if (string.IsNullOrEmpty(_estado))
            {
                throw new Exception("El estado no puede estar vacio");
            }

            if (_fechaDePublicacion == DateTime.MinValue)
            {
                throw new Exception("Ingrese una fecha de inicio valida");
            }
            if (_fechaFinalizacion == DateTime.MinValue || _fechaFinalizacion < _fechaDePublicacion)
            {
                throw new Exception("Ingrese una fecha de finalizacion valida");
            }
        }


        public abstract double CalcularPrecio(int id); //retorna precios de venta y subasta

        public abstract void CerrarPublicacion(int id, int idCliente); //cierra publicaciones venta y subasta

        public int CompareTo(Publicacion? other)
        {
            return other._fechaDePublicacion.CompareTo(_fechaDePublicacion);
        }
        public bool Equals(Publicacion? other)
        {
            return _nombre == other._nombre;
        }
    }

}
