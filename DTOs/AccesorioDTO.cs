namespace AccesoriosApp.DTOs
{
    public class AccesorioSalida
    {

        public int id { get; set; }
        public string nombre { get; set; } = null!;

        public int tipoDeAccesorio { get; set; } 

        public string descripcion { get; set; }


        public string urlFoto { get; set; } = null!;
    }

    public class TipoDeAccesorio
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
    }

    public class Accesorio
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public TipoDeAccesorio TipoDeAccesorio { get; set; }
        public string Descripcion { get; set; }
        public string UrlFoto { get; set; }
    }

    public class Sort
    {
        public bool Empty { get; set; }
        public bool Sorted { get; set; }
        public bool Unsorted { get; set; }
    }

    public class Pageable
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public Sort Sort { get; set; }
        public int Offset { get; set; }
        public bool Paged { get; set; }
        public bool Unpaged { get; set; }
    }

    public class ContentResponse
    {
        public List<Accesorio> Content { get; set; }
        public Pageable Pageable { get; set; }
        public bool Last { get; set; }
        public int TotalElements { get; set; }
        public int TotalPages { get; set; }
        public int Size { get; set; }
        public int Number { get; set; }
        public Sort Sort { get; set; }
        public int NumberOfElements { get; set; }
        public bool First { get; set; }
        public bool Empty { get; set; }
    }

    public class AccesorioPayload
    {
        public string Descripcion { get; set; } = null!;
        public int TipoDeAccesorioId { get; set; } = 0;
        public string Nombre { get; set; } = null!;
        public string UrlFoto { get; set; }
    }

    public class AccesorioEditar
    {
        public int id { get; set; }
        public string descripcion { get; set; } = null!;
        public int tipoDeAccesorioId { get; set; } = 0;
        public string nombre { get; set; } = null!;
        public string urlFoto { get; set; }
    }
}
