using Chapter.Models;
using Microsoft.EntityFrameworkCore;

namespace Chapter.Contexts
{
    /// <summary>
    /// Classe responsável pelo contexto do projeto
    /// Faz a comunicação entre a API e o banco de dados
    /// </summary>
    public class ChapterContext : DbContext
    {
        /// <summary>
        /// Método construtor vazio
        /// </summary>
        public ChapterContext()
        {
        }

        /// <summary>
        /// Método construtor com parâmetro a classe DbContextOptions e herdando de base
        /// https://learn.microsoft.com/pt-br/aspnet/core/tutorials/first-web-api?view=aspnetcore-7.0&tabs=visual-studio
        /// </summary>
        /// <param name="options"></param>
        public ChapterContext(DbContextOptions<ChapterContext> options) : base(options)
        {
        }

        /// <summary>
        /// Define as opções de construção do banco de dados
        /// </summary>
        /// <param name="optionsBuilder">Objeto com as configurações definidas</param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                // cada provedor tem sua sintaxe para especificação
                // string de conexão
                    // Data Source : Nome da instância do banco de dados - \ é um caracter de escape, vai ignorar qualquer caractere especial, deve ser colocado antes do caractere especial
                    // Initial catalog : Nome do banco de dados criado
                    // Integrated Security = true : Autenticação pelo Windows
                    // user Id=sa; pwd=senai@132; : Autenticação com username e senha
                optionsBuilder.UseSqlServer("Data Source = DESKTOP-TNO97N6; initial catalog = Chapter; Integrated Security = true");
            }
        }

        /// <summary>
        /// DbSet representa as entidades que serão utilizadas nas operações de leitura, criação, atualização e deleção (CRUD)
        /// Nesse sistema, a classe livro faz referência a tabela Livros do banco de dados
        /// </summary>
        public DbSet<Livro> Livros { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
    }
}
