﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Edux_Api_EFcore.Domains;
using Edux_Api_EFcore.Interfaces;
using Edux_Api_EFcore.Repositories;
using Edux_Api_EFcore.Utils;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Edux_Api_EFcore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DicaController : ControllerBase
    {
        private readonly IDicaRepository _dicaRepository;

        public DicaController()
        {
            _dicaRepository = new DicaRepository();
        }

        // GET: api/<DicaController>
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var dicas = _dicaRepository.Buscar();
                var qtdDicas = dicas.Count;

                if (qtdDicas == 0)
                    return NoContent(); 

                return Ok(new
                { 
                    totalCount = qtdDicas, 
                    data = dicas
                });
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message + ". Envie um email para a nossa equipe de suporte: edux.suport@gmail.com");
            }
        }

        // GET api/<DicaController>/buscar/id/5
        [HttpGet("buscar/id/{id}")]
        public IActionResult Get(Guid id)
        {
            try
            {
                var dica = _dicaRepository.Buscar(id);

                if (dica == null)
                    return NotFound();

                return Ok(dica);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message + ". Envie um email para a nossa equipe de suporte: edux.suport@gmail.com.");
            }
        }

        // GET: api/<DicaController>/buscar/termo/desenvolvimento
        [HttpGet("buscar/termo/{termo}")]
        public IActionResult Get(string palavraChave)
        {
            try
            {
                var dicas = _dicaRepository.Buscar(palavraChave);
                var qtdDicas = dicas.Count;

                if (qtdDicas == 0)
                    return NoContent();

                return Ok(new
                {
                    totalCount = qtdDicas,
                    data = dicas
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message + ". Envie um email para a nossa equipe de suporte: edux.suport@gmail.com");
            }
        }

        // POST api/<DicaController>
        [HttpPost]
        public IActionResult Post([FromForm] Dica dica)
        {
            try
            {
                if (dica.Imagem != null)
                {
                    var urlImagem = Upload.Local(dica.Imagem);
                    dica.UrlImagem = urlImagem;
                }

                _dicaRepository.Criar(dica);

                return Ok(dica);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message + ". Envie um email para a nossa equipe de suporte: edux.suport@gmail.com");
            }
        }

        // PUT api/<DicaController>
        [HttpPut]
        public IActionResult Put([FromBody] Dica dica)
        {
            try
            {
                _dicaRepository.Editar(dica);

                return Ok(dica);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message + ". Envie um email para a nossa equipe de suporte: edux.suport@gmail.com");
            }
        }

        // DELETE api/<DicaController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            try
            {
                var dica = _dicaRepository.Buscar(id);

                if (dica == null)
                    return NotFound();

                _dicaRepository.Excluir(id);
                return Ok(dica);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message + ". Envie um email para a nossa equipe de suporte: edux.suport@gmail.com");
            }
        }
    }
}