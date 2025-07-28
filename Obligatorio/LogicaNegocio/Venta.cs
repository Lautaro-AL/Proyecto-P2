using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio
{
    internal class Venta : Publicacion
    {
        private bool _ofertaRelampago;

        public Venta(string nombre, string estado, DateTime fechaDePublicacion, List<Articulo> articulo, Cliente clienteCompra, Usuario finalizoCompra, DateTime fechaFinalizacion, bool ofertaRelampago) : base(nombre, estado, fechaDePublicacion, articulo, clienteCompra, finalizoCompra, fechaFinalizacion)
        {
            _ofertaRelampago = ofertaRelampago;
        }


    }
}
