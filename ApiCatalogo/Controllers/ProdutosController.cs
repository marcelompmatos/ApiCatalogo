using ApiCatalogo.Context;
using ApiCatalogo.Models;
using ApiCatalogo.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Diagnostics;

namespace ApiCatalogo.Controllers
{
    [Route("[controller]")]
    [ApiController]

    public class ProdutosController : ControllerBase
    {
        private readonly AppDbContext _context;

        #region Atribui o contexto a uma variavel.
        public ProdutosController(AppDbContext context)
        {
            _context = context;
        }

        #endregion

        #region Metodo que Retorne toda base de dados Produtos 

        [HttpGet]
        public ActionResult<IEnumerable<Produto>> Get() // Para usar o IEnumerable e retornar um NotFound() e necessario usar ActionResult
        {
            var produtos = _context.Produtos.ToList();
            if (produtos is null)
            {
                return NotFound("Produtos não encontrados...");
            }
            return produtos;
        }

        #endregion

        #region Busca Produto por id 

        [HttpGet("{id:int}", Name = "ObterProduto")]
        public ActionResult<Produto> Get([FromQuery] int id)
        {
            var produto = _context.Produtos.FirstOrDefault(p => p.ProdutoId == id); 
            if(produto is null)
            {
                return NotFound("Produtos não encontrados...");
            }
            return produto;

        }

        #endregion

        #region Criar um produto na base de dados

        [HttpPost]
        public ActionResult Post(Produto produto)
        {
            if (produto is null)
                return BadRequest();

            _context.Produtos.Add(produto);  // Ate o momento estou trabalhando em memoria
            _context.SaveChanges();          // aqui inclui no banco perciste os dados na tabela   

            return new CreatedAtRouteResult("ObterProduto",
                new { id = produto.ProdutoId }, produto);
        }

        #endregion

        #region Edita um produto mas neste codigo abaixo é encessario todos os itens estar modificado

        [HttpPut("{id:int}")]
        public ActionResult Put (int id, Produto produto) //Metodo para alterar o Produto
        {
            if(id != produto.ProdutoId)
            {
                return BadRequest(); ///status is 400
            }

            // Aqui verificar se o contexto entidade produto esta modificada conferindo na base pois trabalhamos desconectado
            _context.Entry(produto).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();

            return Ok(produto); // retorna status 200
 
        }

        #endregion

        #region Deletar um produto
        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            var produto = _context.Produtos.FirstOrDefault(p => p.ProdutoId == id);

            if (produto is null)
            {
                return NotFound("Produto não localizado...");  // retorna status 404

            }
            _context.Produtos.Remove(produto);
            _context.SaveChanges();

            return Ok(produto);
        }

        #endregion

        [HttpGet("saudacao/{nome}")]
        public ActionResult<string> GetSaudacao([FromServices] IMeuServico meuservico, string nome)
        {
            return meuservico.Saudacao(nome);
        }

    }
}
