using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EF_Core.Domains;
using EF_Core.Interfaces;
using EF_Core.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EF_Core.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PedidosController : ControllerBase
    {
        private readonly IPedidoRepository _pedidoRepository;

        public PedidosController()
        {
            _pedidoRepository = new PedidoRepository();
        }

        /// <summary>
        /// Mostra todos os pedidos cadastrados
        /// </summary>
        /// <returns>Lista com todos os pedidos</returns>
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var pedidos = _pedidoRepository.Listar();

                if (pedidos.Count == 0)
                    return NoContent();

                return Ok(pedidos);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Mostra um único pedido
        /// </summary>
        /// <param name="id">Id do pedido</param>
        /// <returns>Um pedido especificado pelo seu id</returns>
        [HttpGet("{id}")]
        public IActionResult GetById(Guid id)
        {
            try
            {
                var pedido = _pedidoRepository.BuscarPorId(id);

                if (pedido == null)
                    return NotFound();

                return Ok(pedido);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Cadastra um novo pedido
        /// </summary>
        /// <param name="pedidosItens">Objeto completo de pedidos itens</param>
        /// <returns>Pedidos cadastrado</returns>
        [HttpPost]
        public IActionResult Post(List<PedidoItem> pedidosItens)
        {
            try
            {
                //Adiciona um pedido
                Pedido pedido = _pedidoRepository.Adicionar(pedidosItens);
                return Ok(pedido);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
