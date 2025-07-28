using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio
{
    public class Venta : Publicacion
    {
        private bool _ofertaRelampago;

        public Venta(string nombre, string estado, DateTime fechaDePublicacion, List<Articulo> articulo, Cliente clienteCompra, Usuario finalizoCompra, DateTime fechaFinalizacion, bool ofertaRelampago) : base(nombre, estado, fechaDePublicacion, articulo, clienteCompra, finalizoCompra, fechaFinalizacion)
        {
            _ofertaRelampago = ofertaRelampago;
        }

        public override void CerrarPublicacion(int idpublicacion, int idCliente)
        {
            Publicacion publicacion = Sistema.Instancia.BuscarVentas(idpublicacion);
            Cliente cliente = Sistema.Instancia.BuscarCliente(idCliente);
            double precio = CalcularPrecio(idpublicacion);

            if (publicacion != null && precio <= cliente.Saldo && publicacion.Estado == "ABIERTA")
            {
                cliente.Saldo -= precio;
                publicacion.Estado = "CERRADA";
                base.ClienteCompra = cliente;
                base.FinalizoCompra = cliente;
                base.FechaFinalizacion = DateTime.Now;
            }
            else
            {
                throw new Exception("Saldo insuficiente para realizar la transaccion");
            }
        }

        public override double CalcularPrecio(int id)
        {
            double valorTotal = 0;
            foreach (Articulo articulo in base.Articulos)
            {
                if (id == base.ID)
                {
                    valorTotal += articulo.Precio;
                }
            }
            if (_ofertaRelampago)
            {
                valorTotal = valorTotal - (valorTotal * 0.20);
            }
            return valorTotal;
        }
    }
}
