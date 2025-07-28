using LogicaNegocio.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio
{
    public class Articulo : IValidate, IEquatable<Articulo>
    {
        private int _id;
        private static int s_ultimoID;
        private string _nombre;
        private string _categoria;
        private int _precioVenta;

        public string Categoria
        {
            get { return _categoria; }
        }
        public int Precio
        {
            get { return _precioVenta; }
        }
        public Articulo(string nombre, string categoria, int precioVenta)
        {
            _id = Articulo.s_ultimoID;
            Articulo.s_ultimoID++;
            _nombre = nombre;
            _categoria = categoria;
            _precioVenta = precioVenta;
        }

        public void Validar()
        {
            if (string.IsNullOrEmpty(_nombre))
            {
                throw new Exception("El nombre no puede quedar vacio");
            }
            if (string.IsNullOrEmpty(_categoria))
            {
                throw new Exception("La categoria no puede quedar vacia");
            }
            if (_precioVenta < 0)
            {
                throw new Exception("El precio no puede ser menor a 0");
            }
        }

        public bool Equals(Articulo? other)
        {
            return _nombre == other._nombre;
        }
    }
}
