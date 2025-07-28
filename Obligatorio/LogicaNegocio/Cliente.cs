using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio
{
    public class Cliente : Usuario
    {
        private int _saldoDisponible;

        public Cliente(string nombre, string apellido, string email, string password, int saldoDisponible) : base(nombre, apellido, email, password)
        {

            _saldoDisponible = saldoDisponible;
        }

        public override string ToString()
        {
            return "\nCliente ID : " + base.ID + "\n Nombre : " + base.Nombre + "\n Apellido : " + base.Apellido + "\n Email : " + base.Email + "\n Saldo : " + _saldoDisponible;
        }
        public void Validar()
        {
            base.Validar();
            if (_saldoDisponible < 0)
            {
                throw new Exception("El saldo debe ser mayor a 0");
            }
        }
    }
}
