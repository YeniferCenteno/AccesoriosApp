namespace AccesoriosApp.DTOs
{
    public class TipoDeAccesorioDTO
    {
        public string Nombre { get; set; } = null!;

    }

    public class TipoDeAccesorioEditar
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
    }

    public class ContentResponseTipoAccesorio
    {
        public List<TipoDeAccesorio> Content { get; set; }
        public Pageable Pageable { get; set; } 
        public int TotalElements { get; set; }
        public int TotalPages { get; set; }
        public bool Last { get; set; }
        public bool First { get; set; }
    }

}