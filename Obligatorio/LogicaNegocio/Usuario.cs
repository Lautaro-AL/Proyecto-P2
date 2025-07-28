using LogicaNegocio.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio
{
    public class Usuario : IValidate, IEquatable<Usuario>
    {
        private int _id;
        private static int s_ultimoID;
        private string _nombre;
        private string _apellido;
        private string _email;
        private string _password;

        public Usuario(string nombre, string apellido, string email, string password)
        {
            _nombre = nombre;
            _apellido = apellido;
            _email = email;
            _password = password;
            _id = Usuario.s_ultimoID;
            Usuario.s_ultimoID++;
        }

        public int ID
        {
            get { return _id; }
        }

        public string Nombre
        {
            get { return _nombre; }
        }
        public string Email
        {
            get { return _email; }
        }
        public string Apellido
        {
            get { return _apellido; }
        }

        public void Validar()
        {
            if (string.IsNullOrEmpty(_nombre))
            {
                throw new Exception(" Nombre no puede ser vacio");
            }
            if (string.IsNullOrEmpty(_apellido))
            {
                throw new Exception(" Apellido no puede ser vacio");
            }
            if (string.IsNullOrEmpty(_email))
            {
                throw new Exception(" Email no puede ser vacio");
            }
            if (string.IsNullOrEmpty(_password))
            {
                throw new Exception("La contraseña no puede estar vacia");
            }

        }

        public bool Equals(Usuario? other)
        {
            return _email == other._email;
        }


    }
}
