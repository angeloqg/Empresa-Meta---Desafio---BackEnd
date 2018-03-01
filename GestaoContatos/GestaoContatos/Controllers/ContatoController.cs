using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataLogic.BaseContext;
using DataLogic.BusinessLogic;
using DataLogic.Domain;
using GestaoContatos.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GestaoContatos.Controllers
{
    [Produces("application/json")]
    [Route("components/schemas/Contato")]
    [EnableCors("MyPolicy")]
    public class ContatoController : Controller
    {
        private readonly BaseContext ctx;
        private DataProcess negocio;

        public ContatoController(BaseContext controller)
        {
            ctx = controller;
            negocio = new DataProcess(ctx);
        }

        [HttpGet()]
        public IActionResult GetAll(int page = 0, int size = 10)
        {
            var resultado = negocio.getContato().OrderBy(i => i.idContato).Skip(page).Take(size).ToList();
            var msg = new MensagemModel();

            if (resultado.Count() == 0)
            {
                msg.Mensagem = "Nenhum dado retornado!";
                return NotFound(msg);
            }
            return new ObjectResult(resultado);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(string id)
        {
            var item = negocio.getContatoId(id);
            var msg = new MensagemModel();

            if (item.idContato == 0)
            {
                msg.Mensagem ="Nenhum dado retornado!" ;
                return NotFound(msg);
            }

            return new ObjectResult(item);
        }

        [HttpPost]
        [Route("~/components/schemas/ContatoCreate")]
        public IActionResult Create( [FromBody] Contato novo)
        {
            string mensagem = String.Empty;
            var msg = new MensagemModel();

            if (novo == null)
            {
                msg.Mensagem = "Nenhuma informação foi passado no objeto!";
                msg.Status = false;
                return BadRequest(msg);
            }

            if(!negocio.addContato(novo, out mensagem))
            {
                msg.Mensagem = mensagem;
                msg.Status = false;
                return BadRequest(msg);
            }

            var contatosAtualizados = negocio.getContato().ToList();

            msg.Contatos = contatosAtualizados;
            msg.Mensagem = mensagem;
            msg.Status = true;

            return Ok(msg);
        }

        [HttpPut("{id}")]
        [Route("~/components/schemas/ContatoUpdate")]
        public IActionResult Update(string id, [FromBody] Contato alterar)
        {
            string mensagem = String.Empty;
            var msg = new MensagemModel();

            if (alterar == null)
            {
                msg.Mensagem = "Nenhuma informação foi passado no objeto!";
                msg.Status = false;

                return BadRequest(msg);
            }

            if (String.IsNullOrEmpty(id))
            {
                msg.Mensagem = "O id do contato não foi informado!";
                msg.Status = false;

                return BadRequest(msg);
            }
            else
            {
                int valor = 0;

                if(Int32.TryParse(id, out valor))
                {
                    if(valor != alterar.idContato)
                    {
                        msg.Mensagem = "O id informado não confere com o id do contato!";
                        msg.Status = false;

                        return BadRequest(msg);
                    }

                    if (valor == 0)
                    {
                        msg.Mensagem = "O id do contato não foi informado!";
                        msg.Status = false;

                        return BadRequest(msg);
                    }
                }
            }
            
            if (!negocio.updateContato(alterar, out mensagem))
            {
                msg.Mensagem = mensagem;
                msg.Status = false;

                return BadRequest(msg);
            }

            var contatosAtualizados = negocio.getContato().ToList();

            msg.Contatos = contatosAtualizados;
            msg.Mensagem = mensagem;
            msg.Status = true;

            return Ok(msg);
        }


        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            string mensagem = String.Empty;
            var msg = new MensagemModel();

            if (String.IsNullOrEmpty(id))
            {
                msg.Mensagem = "O id do contato não foi informado!";
                msg.Status = false;

                return BadRequest(msg);
            }
            else
            {
                int valor = 0;

                if (!Int32.TryParse(id, out valor))
                {
                    msg.Mensagem = "O id do contato não foi informado!";
                    msg.Status = false;

                    return BadRequest(msg);
                }

                if(negocio.getContato().Where(i => i.idContato == valor).Count() == 0)
                {
                    msg.Mensagem = "O id do contato não foi informado!";
                    msg.Status = false;

                    return NotFound(msg);                    
                }

                if (!negocio.deleteContato(id, out mensagem))
                {
                    msg.Mensagem = mensagem;
                    msg.Status = false;

                    return NotFound(msg);
                }
            }

            var contatosAtualizados = negocio.getContato().ToList();

            msg.Contatos = contatosAtualizados;
            msg.Mensagem = mensagem;
            msg.Status = true;

            return Ok(msg);
        }

    }
}