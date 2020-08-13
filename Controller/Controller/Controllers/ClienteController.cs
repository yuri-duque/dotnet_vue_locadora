using Domain.DTO;
using Microsoft.AspNetCore.Mvc;
using Service.Models;
using System;
using System.Linq;

namespace Controller.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        private readonly ClienteService _clienteService;

        public ClienteController(ClienteService clienteService)
        {
            _clienteService = clienteService;
        }

        [HttpGet]
        public ActionResult GetAll()
        {
            try
            {
                var response = _clienteService.BuscarTodos();

                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public ActionResult GetById(int id)
        {
            try
            {
                var response = _clienteService.GetById(id);

                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public ActionResult Save(ClienteDTO clienteDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState.Values.Select(x => x.Errors.Select(x => x.ErrorMessage)));

                var response = _clienteService.Salvar(clienteDTO);

                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public ActionResult Update(ClienteDTO clienteDTO, int id)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState.Values.Select(x => x.Errors.Select(x => x.ErrorMessage)));

                var response = _clienteService.Atualizar(clienteDTO, id);

                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            try
            {
                _clienteService.Deletar(id);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("options-select")]
        public ActionResult GetOptionsSelect()
        {
            try
            {
                var response = _clienteService.GetOptionsSelect();

                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("relatorio")]
        public ActionResult Relatorio(bool isAtrasados, int? indexRecordistas)
        {
            try
            {
                var response = _clienteService.Relatorio(isAtrasados, indexRecordistas);

                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}