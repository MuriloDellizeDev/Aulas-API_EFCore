using EF_Core.Domains;
using EF_Core.Interfaces;
using EF_Core.Repositories;
using EF_Core.Utils;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;

namespace EF_Core.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutosController : ControllerBase
    {

        private readonly IProdutoRepository _produtoRepository;

        public ProdutosController()
        {
            _produtoRepository = new ProdutoRepository();
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                //Lista os produtos 
                var produtos = _produtoRepository.Listar();

                //Verifico se existe produto cadastrado
                //Caso não exista eu retorno NoContent
                if (produtos.Count == 0)
                    return NoContent();

                //Caso exista retorno Ok e os produtos cadastrados
                return Ok(new { 

                    totalCount = produtos.Count,
                    data = produtos
                
                });

            }
            catch (Exception)
            {


                //Caso ocorra algun erro retorna BadRequest e a mensagem de erro
                //TODO : Cadstra mensagem de erro no dominio logErro
                return BadRequest(new
                {
                    statusCode = 400,
                    error = "Ocorreu um erro no endpoint Get/produtos, envie um e-mail para email@email informando"

                });
            }
        }


        // GET api/<Produtos>/5
        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            try
            {
                //Busco o produto no repositorio
                Produto produto =   _produtoRepository.BuscarPorId(id);


                //Verifica se o produto existe
                //Caso produto não exista retorna NotFound
                if (produto == null)
                    return NotFound();

                //Caso produto exista retorna
                //Ok e os dados do produto
                return Ok(produto);

            }
            catch (Exception ex)
            {
                //Caso ocorra um erro retorna BadRequest com a mensagem
                //de erro
                return BadRequest(ex.Message);
            }
        }

        // POST api/<Produtos>
        [HttpPost]

        //FromForm - Recebe os dados do produto via form-Data
        public IActionResult Post([FromForm]Produto produto)
        {
            try
            {

                //Verifico se foi enviado um arquivo com foto

                //Verifico se foi enviado um arquivo com a imagem
                if (produto.Imagem != null)
                {
                    var urlImagem = Upload.Local(produto.Imagem);

                    produto.UrlImages = urlImagem;
                }
                //Adiciona um produto
                _produtoRepository.Adicionar(produto);

                //retorna ok com os dados do produto
                return Ok(produto);

            }
            catch (Exception ex)
            {

                //Caso ocorra um erro retorna BadRequest com a mensagem
                //de erro

                return BadRequest(ex.Message);
            }
        }

        // PUT api/<Produtos>/5
        [HttpPut("{id}")]
        public IActionResult Put(Guid id, Produto produto)
        {

            try
            {
               
                //Edita o produto
                _produtoRepository.Editar(produto);

                //Retorna oK com os dados do produto
                return Ok(produto);
            }
            catch (Exception ex)
            {
                //Caso ocorra um erro retorna BadRequest com a mensagem
                //de erro
                return BadRequest(ex.Message);
            }
        
        }

        // DELETE api/<Produtos>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            try
            {
                _produtoRepository.Remover(id);


                return Ok(id);
            }
            catch (Exception ex)
            {
                //Caso ocorra um erro retorna BadRequest com a mensagem
                //de erro
                return BadRequest(ex.Message);
            }
        }


    }
}
