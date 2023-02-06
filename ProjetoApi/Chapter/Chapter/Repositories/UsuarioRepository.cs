using Chapter.Contexts;
using Chapter.Interfaces;
using Chapter.Models;

namespace Chapter.Repositories
{
    /// <summary>
    /// /// repositório para classe Livro, deverá se comunicar com o contexto
    /// vai pegar a solicitação do controller e vai acessar as informações no banco via context
    /// herda da interface
    /// </summary>
    public class UsuarioRepository : IUsuarioRepository
    {
        //variável privada criada para armazenar os dados do context
        private readonly ChapterContext _chapterContext;

        ////injeção de dependência: o repository depende do context
        public UsuarioRepository(ChapterContext context)
        {
            _chapterContext = context;//armazenamento das informações do context dentro da variável privada
        }

        /// <summary>
        /// método para atualizar um usuário
        /// </summary>
        /// <param name="id">id do usuário a ser atualizado</param>
        /// <param name="usuario">objeto usuário com os novos dados</param>
        public void Atualizar(int id, Usuario usuario)
        {
            Usuario usuarioBuscado = _chapterContext.Usuarios.Find(id);

            if (usuarioBuscado != null)
            {
                usuarioBuscado.Email = usuario.Email;
                usuarioBuscado.Senha= usuario.Senha;
                usuarioBuscado.Tipo = usuario.Tipo;

                _chapterContext.Usuarios.Update(usuarioBuscado);
                _chapterContext.SaveChanges();
            }
        }

        /// <summary>
        /// método para buscar um usuário por id
        /// </summary>
        /// <param name="id">id do usário a ser </param>
        /// <returns></returns>
        public Usuario BuscarPorId(int id)
        {
            return _chapterContext.Usuarios.Find(id);
        }

        /// <summary>
        /// método para cadastrar um usuário
        /// </summary>
        /// <param name="usuario">objeto a ser cadastrado</param>
        public void Cadastrar(Usuario usuario)
        {
            _chapterContext.Usuarios.Add(usuario);
            _chapterContext.SaveChanges();
        }

        /// <summary>
        /// método para excluir um usuário
        /// </summary>
        /// <param name="id">id do usuário a ser excluido</param>
        public void Deletar(int id)
        {
            Usuario usuarioBuscado = _chapterContext.Usuarios.Find(id);
            _chapterContext.Usuarios.Remove(usuarioBuscado);
            _chapterContext.SaveChanges();
        }

        /// <summary>
        /// método para listar usuários cadastrados
        /// </summary>
        /// <returns>lista de usuários</returns>
        public List<Usuario> Listar()
        {
            return _chapterContext.Usuarios.ToList();
        }

        /// <summary>
        /// método para realizar login
        /// </summary>
        /// <param name="email">email a ser verificado</param>
        /// <param name="senha">senha a ser verificada</param>
        /// <returns></returns>
        public Usuario Login(string email, string senha)
        {
            return _chapterContext.Usuarios.FirstOrDefault(u => u.Email == email && u.Senha == senha);
        }
    }
}
