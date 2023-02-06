namespace Chapter.Models
{
    /// <summary>
    /// Classe Usuário
    /// </summary>
    public class Usuario
    {
        //propriedades da classe Usuário
        public int Id { get; set; }
        public string? Email { get; set; }
        public string? Senha { get; set; }
        public string? Tipo { get; set;}        
    }
}