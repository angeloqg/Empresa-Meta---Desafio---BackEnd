
using System;
using System.Collections.Generic;
using System.Linq;
using DataLogic.Domain;
using Base = DataLogic.BaseContext;


namespace DataLogic.BusinessLogic
{
    public class DataProcess
    {
        private Base.BaseContext ctx;

        public DataProcess(Base.BaseContext contexto)
        {
            ctx = contexto;

           // initialize();
        }

        #region Incialização de Dados
        private void initialize()
        {
            var verificar = ctx.Contato;

            if (verificar.Count() == 0)
            {

                ctx.Contato.AddRange(new Contato { nome = "Angelo", canal = "TV", valor = "Alto", obs = "Nenhuma" },
                                     new Contato { nome = "Marcos", canal = "Internet", valor = "Alto", obs = "Muito Bom" },
                                     new Contato { nome = "Ana", canal = "Radio", valor = "Médio", obs = "Razoável" },
                                     new Contato { nome = "Renato", canal = "Cartas", valor = "Ruim", obs = "Baixo" },
                                     new Contato { nome = "Selena", canal = "Propagandas", valor = "Alto", obs = "Importante" });
                ctx.SaveChanges();
            }
        }

        #endregion

        #region CRUD Dados

        /// <summary>
        /// Retorna todos os registros de contatos
        /// </summary>
        /// <returns></returns>
        public List<Contato> getContato()
        {
            List<Contato> lista = new List<Contato>();

            try
            {
                var dados = ctx.Contato.ToList();

                foreach (var item in dados)
                {
                    var obj = new Contato();
                    obj = item;
                    lista.Add(obj);
                }
            }
            catch
            {
                // -- Sem ação
            }

            return lista;
        }

        /// <summary>
        /// Retorna os contatos pelo ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Contato getContatoId(string id)
        {
            var contato = new Contato();

            int valor = 0;

            if (!String.IsNullOrEmpty(id))
            {
                id = id.Trim();

                if (Int32.TryParse(id, out valor))
                {
                    try
                    {
                        contato = getContato().Where(i => i.idContato == valor).First();
                    }
                    catch
                    {
                        // -- Sem ação
                    }

                }

            }

            return contato;
        }

        /// <summary>
        /// Adicionando um novo contato
        /// </summary>
        /// <param name="nome"></param>
        /// <param name="canal"></param>
        /// <param name="valor"></param>
        /// <param name="obs"></param>
        /// <param name="mensagem"></param>
        /// <returns></returns>
        public bool addContato(Contato dados, out string mensagem)
        {
            mensagem = String.Empty;
            bool result = false;

            if (!String.IsNullOrEmpty(dados.nome) && !String.IsNullOrEmpty(dados.canal) && !String.IsNullOrEmpty(dados.valor))
            {
                try
                {
                    var contato = new Contato();
                    contato.nome = dados.nome.Trim();
                    contato.canal = dados.canal.Trim();
                    contato.valor = dados.valor.Trim();
                    contato.obs = !String.IsNullOrEmpty(dados.obs) ? dados.obs.Trim() : String.Empty;

                    ctx.Contato.Add(contato);
                    ctx.SaveChanges();

                    mensagem = "Contato gravado com sucesso!";
                    result = true;
                }
                catch
                {
                    mensagem = "Falha ao gravar contato!";
                }
            }
            else
                mensagem = "Existem dados obrigatórios não informados!";

            return result;
        }

        /// <summary>
        /// Atualiza contato
        /// </summary>
        /// <param name="id"></param>
        /// <param name="nome"></param>
        /// <param name="canal"></param>
        /// <param name="valor"></param>
        /// <param name="obs"></param>
        /// <param name="mensagem"></param>
        /// <returns></returns>
        public bool updateContato(Contato dados, out string mensagem)
        {
            bool result = false;
            mensagem = String.Empty;

            string id = dados.idContato.ToString();

            if (!String.IsNullOrEmpty(id))
            {
                try
                {
                    var contato = getContatoId(id);

                    if (contato != null)
                    {

                        if (!String.IsNullOrEmpty(dados.nome) && !String.IsNullOrEmpty(dados.canal) && !String.IsNullOrEmpty(dados.valor))
                        {
                            contato.nome = dados.nome; ;
                            contato.canal = dados.canal;
                            contato.valor = dados.valor;
                            contato.obs = !String.IsNullOrEmpty(dados.obs) ? dados.obs.Trim() : String.Empty;

                            ctx.Contato.Update(contato);
                            ctx.SaveChanges();
                            mensagem = "Contato alterado com sucesso!";

                            result = true;
                        }
                        else
                        {
                            mensagem = "Existem dados obrigatórios não informados!";
                        }
                    }
                    else
                    {
                        mensagem = "Contato não encontrado!";
                    }
                }
                catch
                {
                    mensagem = "Falha ao alterar contato!";
                }
            }
            else
            {
                mensagem = "Contato não informado!";
            }

            return result;
        }

        public bool deleteContato(string id, out string mensagem)
        {
            bool result = false;
            string msg = String.Empty;
            int valor = 0;

            if (!String.IsNullOrEmpty(id))
            {
                id = id.Trim();

                try
                {
                    var dados = getContatoId(id);

                    if (dados != null)
                    {
                        ctx.Contato.Remove(dados);
                        ctx.SaveChanges();

                        msg = "Contato excluido com sucesso!";
                        result = true;
                    }
                    else
                    {
                        msg = "Nenhum contato encontado!";
                    }
                }
                catch
                {
                    msg = "Falha ao excluir contato!";
                }
            }
            else
            {
                msg = "Contato não informado!!";
            }

            mensagem = msg;
            return result;
        }
        #endregion





    }
}
