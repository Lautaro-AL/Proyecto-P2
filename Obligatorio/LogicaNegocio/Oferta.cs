using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio
{
    public class Oferta : IEquatable<Oferta>
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

        public bool Equals(Oferta? other)
        {
            return _cliente == other._cliente;
        }
    }
}
