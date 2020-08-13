using Domain.DTO;
using Microsoft.AspNetCore.Mvc;
using Service.Models;
using System;
using System.Linq;

namespace Controller.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilmeController : ControllerBase
    {
        private readonly FilmeService _filmeService;

        public FilmeController(FilmeService filmeService)
        {
            _filmeService = filmeService;
        }

        [HttpGet]
        public ActionResult GetAll()
        {
            try
            {
                var response = _filmeService.BuscarTodos();

                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public ActionResult Save(FilmeDTO filmeDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState.Values.Select(x => x.Errors.Select(x => x.ErrorMessage)));

                var response = _filmeService.Salvar(filmeDTO);

                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public ActionResult Update(FilmeDTO filmeDTO, int id)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState.Values.Select(x => x.Errors.Select(x => x.ErrorMessage)));

                var response = _filmeService.Atualizar(filmeDTO, id);

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
                _filmeService.Deletar(id);

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
                var response = _filmeService.GetOptionsSelect();

                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("relatorio")]
        public ActionResult Relatorio(bool isNuncaAlugados, bool? maisAlugados = null, DateTime? PeriodoMaisAlugados = null, int? quantidadeItens = null)
        {
            try
            {
                var response = _filmeService.Relatorio(isNuncaAlugados, maisAlugados, PeriodoMaisAlugados, quantidadeItens);

                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}