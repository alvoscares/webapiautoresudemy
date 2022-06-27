namespace WebAPIAutores.DTOs
{
    public class PaginacionDTO
    {
        public int Pagina { get; set; } = 1;
        private int recordsPorPagina = 10;
        private readonly int cantidadMaximaPorPagina = 50;

        public int RecordsPorPagina
        {
            get { return recordsPorPagina; }
            // Si el cliente me pide una cantidad mayor a la cantidad maxima por pagina, le voy a retornar la cantidad mayor por pagina
            set { recordsPorPagina = (value > cantidadMaximaPorPagina) ? cantidadMaximaPorPagina : value; }
        }
    }
}
