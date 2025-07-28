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
        private double _saldoDisponible;

        public Cliente(string nombre, string apellido, string email, string password, double saldoDisponible) : base(nombre, apellido, email, password)
        {

            _saldoDisponible = saldoDisponible;
        }
        public double Saldo
        {
            get { return _saldoDisponible; }
            set { _saldoDisponible = value; }
        }

        public void AgregarSaldoCliente(int saldo)
        {
            _saldoDisponible += saldo;
        }

        public void Validar()
        {
            base.Validar();
            if (_saldoDisponible < 0 )
            {
                throw new Exception("El saldo debe ser mayor a 0");
            }
        }
        public override string SaberRol()
        {
            return "Cliente";
        }
    }
}
