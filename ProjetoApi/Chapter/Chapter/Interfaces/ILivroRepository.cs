using Chapter.Models;

namespace Chapter.Interfaces
{
    public interface ILivroRepository
    {
        //métodos que deverão ser implementados pela classe que herdar desta interface
        List<Livro> Ler();

        Livro BuscarPorId(int id);

        void Cadastrar(Livro livro);

        void Atualizar(int id, Livro livro);

        void Deletar(int id);

        Livro BuscarPorTitulo(string titulo);
    }
}