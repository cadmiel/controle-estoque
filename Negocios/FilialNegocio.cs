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
    public class FilialNegocio
    {
        AcessoDadosSqlServer acessoDados = new AcessoDadosSqlServer();

        public FilialColecao ConsultarPorCodigo(int IdPessoaFilial)
        {
            try
            {
                acessoDados.LimparParametros();
                acessoDados.AdicionarParametros("@IdPessoaFilial", IdPessoaFilial);

                DataTable dataTable = acessoDados.ExecutarConsulta(CommandType.StoredProcedure, "uspFilialConsultarPorCodigo");

                FilialColecao filialColecao = new FilialColecao();

                foreach (DataRow dataRow in dataTable.Rows)
                {
                    Filial filial = new Filial();

                    Pessoa pessoa = new Pessoa();
                    pessoa.CpfCnpj  = Convert.ToString(dataRow["CpfCnpj"]);
                    pessoa.IdPessoa = Convert.ToInt32(dataRow["IdPessoa"]);
                    pessoa.Nome     = Convert.ToString(dataRow["Nome"]);

                    PessoaTipo pessoaTipo = new PessoaTipo();
                    pessoaTipo.Descricao = Convert.ToString(dataRow["Descricao"]);
                    pessoaTipo.IdPessoaTipo = Convert.ToInt32(dataRow["IdPessoaTipo"]);

                    pessoa.PessoaTipo = pessoaTipo;

                    filial.Pessoa = pessoa;

                    filialColecao.Add(pedido);

                }

                return filialColecao;
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao consultar por codigo, detalhes: " + e.Message);
            }
        }


        public FilialColecao ConsultarPorNome(string Nome)
        {
            try
            {
                acessoDados.LimparParametros();
                acessoDados.AdicionarParametros("@Nome", Nome);

                DataTable dataTable = acessoDados.ExecutarConsulta(CommandType.StoredProcedure, "uspFilialConsultarPorNome");

                FilialColecao filialColecao = new FilialColecao();

                foreach (DataRow dataRow in dataTable.Rows)
                {
                    Filial filial = new Filial();

                    Pessoa pessoa = new Pessoa();
                    pessoa.CpfCnpj = Convert.ToString(dataRow["CpfCnpj"]);
                    pessoa.IdPessoa = Convert.ToInt32(dataRow["IdPessoa"]);
                    pessoa.Nome = Convert.ToString(dataRow["Nome"]);

                    PessoaTipo pessoaTipo = new PessoaTipo();
                    pessoaTipo.Descricao = Convert.ToString(dataRow["Descricao"]);
                    pessoaTipo.IdPessoaTipo = Convert.ToInt32(dataRow["IdPessoaTipo"]);

                    pessoa.PessoaTipo = pessoaTipo;

                    filial.Pessoa = pessoa;

                    filialColecao.Add(pedido);

                }

                return filialColecao;
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao consultar por codigo, detalhes: " + e.Message);
            }
        }

    }
}
