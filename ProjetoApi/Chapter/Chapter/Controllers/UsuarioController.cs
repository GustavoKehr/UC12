using Chapter.Interfaces;
using Chapter.Models;
using Chapter.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Chapter.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {

        private readonly IUsuarioRepository _iUsuarioRepository;

        public UsuarioController(IUsuarioRepository iUsuarioRepository)
        {
            _iUsuarioRepository = iUsuarioRepository;
        }

        /// <summary>
        /// método que controla o acesso a listagem de usuários
        /// </summary>
        /// <returns>lista dos usuários cadastrados</returns>
        /// <exception cref="Exception">mensagem de erro</exception>
        [HttpGet]
        public IActionResult Listar()
        {
            try
            {
                return Ok(_iUsuarioRepository.Listar());
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }        
        }

        /// <summary>
        /// método que controla o acesso a buscar um usuário por id
        /// </summary>
        /// <param name="id">id do usuário a ser buscado</param>
        /// <returns>status code Ok e usuário buscado</returns>
        /// <exception cref="Exception">mensagem de erro</exception>
        [HttpGet("{id}")]
        public IActionResult BuscarPorId(int id)
        {
            try
            {
                Usuario usuarioBuscado = _iUsuarioRepository.BuscarPorId(id);

                if (usuarioBuscado == null)
                {
                    return NotFound();
                }
                return Ok(usuarioBuscado);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// método que controla o acesso para cadastro de um usuário
        /// </summary>
        /// <param name="usuario">objeto a ser cadastrado</param>
        /// <returns>status code created</returns>
        /// <exception cref="Exception">mensage de erro</exception>
        [HttpPost]
        public IActionResult Cadastrar(Usuario usuario)
        {
            try
            {
                _iUsuarioRepository.Cadastrar(usuario);
                return StatusCode(201);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// método que controla o acesso a atualização de um usuário
        /// </summary>
        /// <param name="id">id do usuário a ser atualizado</param>
        /// <param name="usuario">objeto usuário com os novos dados</param>
        /// <returns>mensagem de sucesso</returns>
        /// <exception cref="Exception">mensagem de erro</exception>
        [HttpPut("{id}")]
        public IActionResult Atualizar (int id, Usuario usuario)
        {
            try
            {
                _iUsuarioRepository.Atualizar(id, usuario);

                return Ok("Usuário atualizado");
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// método que controla o acesso para excluir um usuário
        /// </summary>
        /// <param name="id">id do usuário a ser excluído</param>
        /// <returns>status code ok e mensagem de sucesso</returns>
        /// <exception cref="Exception">mensagem de erro</exception>
        [HttpDelete("{id}")]
        public IActionResult Deletar(int id)
        {
            try
            {
                _iUsuarioRepository.Deletar(id);
                return Ok("Usuário excluido");
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
