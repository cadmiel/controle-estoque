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
    public class PedidoItemNegocio
    {
        AcessoDadosSqlServer acessoDados = new AcessoDadosSqlServer();

        public string Inserir(PedidoItem pedidoItem)
        { 
            try 
	        {
                acessoDados.LimparParametros();
                acessoDados.AdicionarParametros("@IdPedido", pedidoItem.Pedido.IdPedido);
                acessoDados.AdicionarParametros("@IdProduto", pedidoItem.Produto.IdProduto);
                acessoDados.AdicionarParametros("@Quantidade", pedidoItem.Quantidade);
                acessoDados.AdicionarParametros("@ValorUnitario", pedidoItem.ValorUnitario);
                acessoDados.AdicionarParametros("@PercentualDesconto", pedidoItem.PercentualDesconto);
                acessoDados.AdicionarParametros("@ValorDesconto", pedidoItem.ValorDesconto);
                acessoDados.AdicionarParametros("@ValorTotal", pedidoItem.ValorTotal);
                string idProduto = acessoDados.ExecutarManipulacao(CommandType.StoredProcedure, "uspPedidoItemInserir").ToString();
                return idProduto;
	        }
	        catch (Exception e)
	        {
		
		        return e.Message;
	        }
        }

        public PedidoItemColecao Consultar(int IdPedido) 
        {
            try
            {
                acessoDados.LimparParametros();
                acessoDados.AdicionarParametros("@IdPedido",IdPedido);
                DataTable dataTable = acessoDados.ExecutarConsulta(CommandType.StoredProcedure, "uspPedidoItemConsultar");

                PedidoItemColecao pedidoItemColecao = new PedidoItemColecao();

                foreach (DataRow dataRow in dataTable.Rows)
                {
                    PedidoItem pedidoItem = new PedidoItem();

                    Pedido pedido = new Pedido();
                    pedido.IdPedido = Convert.ToInt32(dataRow["IdPedido"]);
                    pedidoItem.Pedido = pedido;

                    Produto produto = new Produto();
                    produto.IdProduto = Convert.ToInt32(dataRow["IdProduto"]);
                    produto.Descricao = Convert.ToString(dataRow["Descricao"]);
                    pedidoItem.Produto = produto;

                    pedidoItem.Quantidade = Convert.ToInt32(dataRow["Quantidade"]);
                    pedidoItem.ValorDesconto = Convert.ToDecimal(dataRow["ValorDesconto"]);
                    pedidoItem.PercentualDesconto = Convert.ToDecimal(dataRow["PercentualDesconto"]);
                    pedidoItem.ValorUnitario = Convert.ToDecimal(dataRow["ValorUnitario"]);
                    pedidoItem.ValorTotal = Convert.ToDecimal(dataRow["ValorTotal"]);
                    pedidoItemColecao.Add(pedidoItem);
                }

                return pedidoItemColecao;
            }
            catch (Exception e)
            {

                throw new Exception("Erro ao consultar pedido item, detalhes: "+e.Message);
            }
        }

    }
}
