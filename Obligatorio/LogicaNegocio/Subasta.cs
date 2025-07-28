using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio
{
    public class Subasta : Publicacion
    {
        private List<Oferta> _ofertas = new List<Oferta>();
        public Subasta(string nombre, string estado, DateTime fechaDePublicacion, List<Articulo> articulo, Cliente clienteCompra, Usuario finalizoCompra, DateTime fechaFinalizacion) : base(nombre, estado, fechaDePublicacion, articulo, clienteCompra, finalizoCompra, fechaFinalizacion)
        {
        }

        public void AgregarOferta(Cliente cliente, int monto, DateTime fecha) //Agrega Oferta a subastas
        {
            Oferta oferta = new Oferta(cliente, monto, fecha);
            if (!_ofertas.Contains(oferta))
            {
                _ofertas.Add(oferta);
            }

        }

        public void Validar()
        {
            base.Validar();
            if (_ofertas == null)
            {
                throw new Exception("No existen ofertas");
            }
        }
    }
}
