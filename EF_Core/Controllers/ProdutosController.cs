using EF_Core.Domains;
using EF_Core.Interfaces;
using EF_Core.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

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
                var produtos =  _produtoRepository.Listar();

                //Verifico se existe produto cadastrado
                //Caso não exista eu retorno NoContent
                if (produtos.Count == 0)
                    return NoContent();

                //Caso exista retorno Ok e os produtos cadastrados
                return Ok(produtos);

            }
            catch (Exception ex)
            {
                //Caso ocorra algun erro retorna BadRequest e a mensagem de erro
                return BadRequest(ex.Message);
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
        public IActionResult Post(Produto produto)
        {
            try
            {
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
                var produtoTemp = _produtoRepository.BuscarPorId(id);

                if (produtoTemp == null)
                    return NotFound();

                produto.Id = id;
                _produtoRepository.Editar(produto);

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
