using ApiCatalogo.Context;
using ApiCatalogo.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiCatalogo.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CategoriasController : ControllerBase
    {
        private readonly AppDbContext _context;

        #region Atribui o contexto a uma variavel.
        public CategoriasController(AppDbContext context)
        {
            _context = context;
        }
        #endregion

        #region Retorna rodas os Produtos que estão vinculados com a categora devido ao (Include)

        [HttpGet("produtos")]
        public ActionResult<IEnumerable<Categoria>> GetCategoriasProdutos()
        {
            //return _context.Categorias.Include(p => p.Produtos).ToList();

            return _context.Categorias.Include(p => p.Produtos).Where(c => c.CategoriaId <= 5).ToList();

        }

        #endregion

        #region Metodo que Retorne toda base de dados Categorias 
        [HttpGet]
        public ActionResult<IEnumerable<Categoria>> Get()
        {
            try
            {
                //todo verbo httpget AsNoTracking para melhoar o desenpenho
                return _context.Categorias.AsNoTracking().ToList();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, 
                    "Ocorreu um problema ao tratar a sua solicitação");
            }

        }
        #endregion

        #region Obter Categoria por id

        [HttpGet("{id:int}", Name = "ObterCategoria")]
        public ActionResult<Categoria> Get(int id)
        {
            try
            {
                var categorias = _context.Categorias.FirstOrDefault(p => p.CategoriaId == id);

                if (categorias == null)
                {
                    return NotFound($"Categoria com id = { id } nao encontrado");
                }
                return Ok(categorias);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Ocorreu um problema ao tratar a sua solicitação");
            }
        }

        #endregion

        #region Criar um Categoria na base de dados
        [HttpPost]  
        public ActionResult Post(Categoria categoria)
        {
            if (categoria is null)
                return BadRequest("Dados Inválidos");

            _context.Categorias.Add(categoria);
            _context.SaveChanges();

            return new CreatedAtRouteResult("ObterProduto", new { id = categoria.CategoriaId }, categoria);

        }
        #endregion

        #region Edita um categoria mas neste codigo abaixo é necessario todos os itens estar modificado

        [HttpPut("{id:int}")]
        public ActionResult Put(int id, Categoria categoria)
        {
            if (id != categoria.CategoriaId)
            {
                return BadRequest();
            }
            _context.Entry(categoria).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();

            return Ok(categoria);
        }

        #endregion

        #region Deletar um Categoria
        [HttpDelete("{id:int}")]
        public ActionResult<Categoria> Delete(int id)
        {
            var categoria = _context.Categorias.FirstOrDefault(p => p.CategoriaId == id);

            if (categoria is null)
            {
                return NotFound($"Categoria com id= {id} nao encontrada...");  // retorna status 404
            }
            _context.Categorias.Remove(categoria);
            _context.SaveChanges();

            return Ok(categoria);
        }

        #endregion
    }
}
