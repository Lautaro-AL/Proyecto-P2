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
        public List<Oferta> Ofertas
        {
            get
            {
                return _ofertas;
            }
        }

        public void AgregarOferta(Cliente cliente, int monto, DateTime fecha) //Agrega Oferta a subastas
        {
            Oferta oferta = new Oferta(cliente, monto, fecha);

            oferta.Validar();
            if (!_ofertas.Contains(oferta))
            {
                _ofertas.Add(oferta);
            }
        }

        public override double CalcularPrecio(int id) //Retorna el precio maximo a una subasa
        {
            int ofertaMax = 0;
            for (int i = 0; i < _ofertas.Count; i++)
            {
                if (id == base.ID && _ofertas[i].Monto > ofertaMax)
                {
                    ofertaMax = _ofertas[i].Monto;
                }
            }
            return ofertaMax;
        }


        public override void CerrarPublicacion(int id, int idUsuario) // Cierra una subasta por metodo polimorfico.
        {
            Oferta mejorOferta = null;
            Subasta subasta = Sistema.Instancia.BuscarSubastas(id);
            Usuario usuario = Sistema.Instancia.BuscarUsuario(idUsuario);
            foreach (Oferta oferta in subasta.Ofertas)
            {

                if (oferta.OfertaCliente.Saldo >= oferta.Monto)
                {
                    if (mejorOferta == null || oferta.Monto > mejorOferta.Monto)
                    {
                        mejorOferta = oferta;
                    }
                    oferta.OfertaCliente.Saldo -= oferta.Monto;
                }
                else
                {
                    base.Estado = "CANCELADA";
                    throw new Exception("No exisen clientes con saldo suficiente para efectuar la oferta");
                }
            }
            if (subasta != null && mejorOferta != null)
            {
                base.Estado = "CERRADA";
                base.FechaFinalizacion = DateTime.Now;
                base.ClienteCompra = mejorOferta.OfertaCliente;
                base.FinalizoCompra = usuario;
            }
            else
            {
                base.Estado = "CANCELADA";
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
