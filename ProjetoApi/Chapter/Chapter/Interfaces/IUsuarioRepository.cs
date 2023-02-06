using Chapter.Models;

namespace Chapter.Interfaces
{
    /// <summary>
    /// Interface IUsuarioRepository
    /// </summary>
    public interface IUsuarioRepository
    {
        //métodos que deverão ser implementados pela classe que herdar desta interface
        List<Usuario> Listar();
        void Cadastrar(Usuario usuario);
        void Atualizar (int id, Usuario usuario);
        void Deletar(int id);
        Usuario BuscarPorId(int id);
        Usuario Login (string email, string senha);
    }
}
