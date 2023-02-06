namespace Chapter.Models
{
    /// <summary>
    /// classe Livro
    /// </summary>
    public class Livro
    {
        //propriedades da classe Livro
        public int Id { get; set; }
        public string? Titulo { get; set; }
        public int QuantidadePaginas { get; set; }
        public bool Disponivel { get; set; }
    }
}
