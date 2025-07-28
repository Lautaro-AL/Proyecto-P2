using LogicaNegocio.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio
{
    public class Oferta : IEquatable<Oferta>, IValidate, IComparable<Oferta>
    {
        private int _id;
        private static int s_ultimoID;
        private Cliente _cliente;
        private int _monto;
        private DateTime _fechaRealizacion;

        public Oferta(Cliente usuario, int monto, DateTime fechaRealizacion)
        {
            _cliente = usuario;
            _monto = monto;
            _fechaRealizacion = fechaRealizacion;
            _id = Oferta.s_ultimoID;
            Oferta.s_ultimoID++;
        }
        
        public Cliente OfertaCliente
        {
            get { return _cliente; }
        }
        public int Monto
        {
            get { return _monto; }
        }

        public bool Equals(Oferta? other)
        {
            return _monto == other._monto;
        }

        public void Validar()
        {
            if(_monto <= 0) 
            { 
                throw new Exception("El monto ingresado debe ser positivo");
            }
            if (_fechaRealizacion == DateTime.MinValue) //La fecha se define automaticamente
            {
                throw new Exception("Error en fecha");
            }

        }

        public int CompareTo(Oferta? other)
        {
            return other._monto.CompareTo(_monto);
        }
    }
}
