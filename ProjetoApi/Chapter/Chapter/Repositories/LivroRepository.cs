using Chapter.Contexts;
using Chapter.Interfaces;
using Chapter.Models;

namespace Chapter.Repositories
{
    /// <summary>
    /// repositório para classe Livro, deverá se comunicar com o contexto
    /// vai pegar a solicitação do controller e vai acessar as informações no banco via context 
    /// </summary>
    public class LivroRepository : ILivroRepository
    {
        //variável privada criada para armazenar os dados do context
        private readonly ChapterContext _chapterContext;

        //injeção de dependência: o repository depende do context
        public LivroRepository(ChapterContext context)
        {
            _chapterContext = context;//armazenamento das informações do context dentro da variável privada
        }

        /// <summary>
        /// método para atualizar um livro
        /// </summary>
        /// <param name="id">id do livro a ser atualizado</param>
        /// <param name="livro">objeto usuário com os novos dados</param>
        public void Atualizar(int id, Livro livro)
        {
            Livro livroBuscado = _chapterContext.Livros.Find(id);

            if (livroBuscado != null)
            {
                livroBuscado.Titulo = livro.Titulo;
                livroBuscado.QuantidadePaginas = livro.QuantidadePaginas;
                livroBuscado.Disponivel= livro.Disponivel;
            }

            _chapterContext.Livros.Update(livroBuscado);
            _chapterContext.SaveChanges();
        }

        /// <summary>
        /// método para buscar um livro pelo id
        /// </summary>
        /// <param name="id">id do livro a ser buscado</param>
        /// <returns>livro buscado</returns>
        public Livro BuscarPorId(int id)
        {
            return _chapterContext.Livros.Find(id);
        }

        /// <summary>
        /// método para buscar um livro pelo título
        /// </summary>
        /// <param name="titulo">título do livro a ser buscado</param>
        /// <returns>livro buscado</returns>
        public Livro BuscarPorTitulo(string titulo)
        {
            return _chapterContext.Livros.FirstOrDefault(t => t.Titulo == titulo.Trim());
        }

        /// <summary>
        /// método para cadastrar um livro
        /// </summary>
        /// <param name="livro">objeto a ser cadastrado</param>
        public void Cadastrar(Livro livro)
        {
            _chapterContext.Livros.Add(livro);
            _chapterContext.SaveChanges();
        }

        /// <summary>
        /// método para excluir um livro
        /// </summary>
        /// <param name="id">id do livro a ser excluido</param>
        public void Deletar(int id)
        {
            Livro livro = _chapterContext.Livros.Find(id);
            _chapterContext.Livros.Remove(livro);
            _chapterContext.SaveChanges();
        }

        /// <summary>
        /// método para listagem de livros
        /// </summary>
        /// <returns>lista com os livros cadastrados</returns>
        public List<Livro> Ler()
        {
            //o contexto.acesso a tabela Livros.e lista os itens dentro dela
            return _chapterContext.Livros.ToList();     
        }
    }
}