using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AcessoBancoDados;
using ObjetoTransferencia;
using System.Data;

namespace Negocios
{
    public class PedidoNegocios
    {
        AcessoDadosSqlServer acessoDados = new AcessoDadosSqlServer();
        
        public string Inserir(Pedido pedido)
        {
            try
            {
                acessoDados.LimparParametros();
                acessoDados.AdicionarParametros("@IdOperacao", pedido.Operacao.IdOperacao);
                acessoDados.AdicionarParametros("@IdSituacao", pedido.Situacao.IdSituacao);
                acessoDados.AdicionarParametros("@IdPessoaEmitente", pedido.Emitente.IdPessoa);
                acessoDados.AdicionarParametros("@IdPessoaDestinatario", pedido.Destinatario.IdPessoa);
                string IdPedido = acessoDados.ExecutarManipulacao(CommandType.StoredProcedure, "uspPedidoInserir").ToString();
                return IdPedido;
            }
            catch (Exception e)
            {
                
                return e.Message;
            }
            
        }

        public PedidoColecao ConsultarPorData(DateTime DataInicial, DateTime DataFinal)
        {
            try
            {
                acessoDados.LimparParametros();
                acessoDados.AdicionarParametros("@DataInicial", DataInicial);
                acessoDados.AdicionarParametros("@DataFinal", DataFinal);
                
                DataTable dataTable = acessoDados.ExecutarConsulta(CommandType.StoredProcedure, "uspPedidoConsultarPorData");
                
                PedidoColecao pedidoColecao = new PedidoColecao();
                
                foreach (DataRow dataRow in dataTable.Rows)
                {
                    Pedido pedido = new Pedido();
                    pedido.IdPedido = Convert.ToInt32(dataRow["IdPedido"]);
                    pedido.DataHora = Convert.ToDateTime(dataRow["DataHora"]);

                    Operacao operacao = new Operacao();
                    operacao.IdOperacao = Convert.ToInt32(dataRow["IdOperacao"]);
                    operacao.Descricao = Convert.ToString(dataRow["DescOperacao"]);
                    pedido.Operacao = operacao;

                    Situacao situacao = new Situacao();
                    situacao.IdSituacao = Convert.ToInt32(dataRow["IdSituacao"]);
                    situacao.Descricao = Convert.ToString(dataRow["DescSituacao"]);
                    pedido.Situacao = situacao;

                    Pessoa emitente = new Pessoa();
                    emitente.IdPessoa = Convert.ToInt32(dataRow["IdPessoaEmitente"]);
                    emitente.Nome = Convert.ToString(dataRow["NomeEmitente"]);
                    pedido.Emitente = emitente;

                    Pessoa destinatario = new Pessoa();
                    destinatario.IdPessoa = Convert.ToInt32(dataRow["IdPessoaDestinatario"]);
                    destinatario.Nome = Convert.ToString(dataRow["NomeDestinatario"]);
                    pedido.Destinatario = destinatario;

                    pedidoColecao.Add(pedido);

                }

                return pedidoColecao;
            }
            catch (Exception e)
            {

                throw new Exception("Erro ao consultar por data, detalhes: "+ e.Message);
            }
        }

    }
}
