namespace LogicaNegocio
{
    public class Administrador : Usuario
    {
        public Administrador(string nombre, string apellido, string email, string password) : base(nombre, apellido, email, password)
        {
        }
        public override string SaberRol()
        {
            return "Administrador";
        }
    }

}
