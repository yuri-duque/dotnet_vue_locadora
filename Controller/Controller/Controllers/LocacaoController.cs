using Domain.DTO;
using Microsoft.AspNetCore.Mvc;
using Service.Models;
using System;
using System.Linq;

namespace Controller.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocacaoController : ControllerBase
    {
        private readonly LocacaoService _locacaoService;

        public LocacaoController(LocacaoService locacaoService)
        {
            _locacaoService = locacaoService;
        }

        [HttpGet]
        public ActionResult GetAll()
        {
            try
            {
                var response = _locacaoService.BuscarTodos();

                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("Alugar")]
        public ActionResult Alugar(LocacaoAlugarDTO locacao)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState.Values.Select(x => x.Errors.Select(x => x.ErrorMessage)));

                var response = _locacaoService.Alugar(locacao);

                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("Devolver")]
        public ActionResult Devolver(LocacaoAlugarDTO locacao)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState.Values.Select(x => x.Errors.Select(x => x.ErrorMessage)));

                var response = _locacaoService.Devolver(locacao);

                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public ActionResult Atualizar([FromBody]LocacaoAtualizarDTO locacao, int idCliente, int idFilme)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState.Values.Select(x => x.Errors.Select(x => x.ErrorMessage)));

                var response = _locacaoService.Atualizar(locacao, idCliente, idFilme);

                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        public ActionResult Delete(int idCliente, int idFilme)
        {
            try
            {
                _locacaoService.Deletar(idCliente, idFilme);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}