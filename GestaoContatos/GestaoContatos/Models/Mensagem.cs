using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataLogic.Domain;

namespace GestaoContatos.Models
{
    public class MensagemModel
    {
        public string Mensagem { get; set; }
        public bool Status { get; set; }
        public List<Contato> Contatos { get; set; }
    }
}
