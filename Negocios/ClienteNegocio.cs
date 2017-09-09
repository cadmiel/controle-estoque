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
    public class ClienteNegocio
    {

        AcessoDadosSqlServer acessoDados = new AcessoDadosSqlServer();

        public ClienteColecao ConsultarPorCodigoNome(string nomeParametro, object valorParametro)
        {
            try
            {

                acessoDados.LimparParametros();
                acessoDados.AdicionarParametros(nomeParametro, valorParametro);

                DataTable dataTable = acessoDados.ExecutarConsulta(CommandType.StoredProcedure, "uspClienteConsultarPorCodigoNome");

                ClienteColecao clienteColecao = new ClienteColecao();

                foreach (DataRow dataRow in dataTable.Rows)
                {
                    Cliente cliente = new Cliente();

                    Pessoa pessoa = new Pessoa();
                    pessoa.CpfCnpj = Convert.ToString(dataRow["CpfCnpj"]);
                    pessoa.IdPessoa = Convert.ToInt32(dataRow["IdPessoa"]);
                    pessoa.Nome = Convert.ToString(dataRow["Nome"]);

                    PessoaTipo pessoaTipo = new PessoaTipo();
                    pessoaTipo.Descricao = Convert.ToString(dataRow["Descricao"]);
                    pessoaTipo.IdPessoaTipo = Convert.ToInt32(dataRow["IdPessoaTipo"]);

                    pessoa.PessoaTipo = pessoaTipo;

                    cliente.Pessoa = pessoa;

                    clienteColecao.Add(cliente);

                }

                return clienteColecao;
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao consultar por nome e codigo, detalhes: " + e.Message);
            }
        }

    }
}
