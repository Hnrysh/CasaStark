namespace CasaStark.Models
{
    public class usuario
    {

        public int Id { get; set; }

        public string Nombre { get; set; }

        public string Correo { get; set; }
        public string Contrasena { get; set; }

        public string Rol { get; set; }
    }

    public class ListadeUsuarios 
    {
        public usuario nuevoUsuario { get; set; }
        public List<usuario> ListaUsuarios { get; set; } = new List<usuario>();
    }
}
