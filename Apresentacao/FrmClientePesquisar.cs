using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Negocios;
using ObjetoTransferencia;
using System.Reflection;

namespace Apresentacao
{
    public partial class FrmClientePesquisar : Form
    {
        public Cliente clienteSelecionada { get; set; }

        public FrmClientePesquisar()
        {
            InitializeComponent();
            dgwPrincipal.AutoGenerateColumns = false;
        }

        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            ClienteNegocio clienteNegocio = new ClienteNegocio();
            ClienteColecao clienteColecao = new ClienteColecao();

            int codigoDigitado;

            if (int.TryParse(txtPesquisar.Text, out codigoDigitado) == true)
            {
                clienteColecao = clienteNegocio.ConsultarPorCodigoNome("@IdPessoaCliente", codigoDigitado);
            }
            else
            {
                clienteColecao = clienteNegocio.ConsultarPorCodigoNome("@Nome", txtPesquisar.Text);
            }

            dgwPrincipal.DataSource = null;
            dgwPrincipal.DataSource = clienteColecao;
            dgwPrincipal.Update();
            dgwPrincipal.Refresh();
        }

        private object CarregarPropriedade(object propriedade, string nomePropriedade)
        {
            try
            {
                object retorno = "";

                if (nomePropriedade.Contains("."))
                {
                    PropertyInfo[] propertyInfoArray;
                    string propriedadeAntesDoPonto;
                    propriedadeAntesDoPonto = nomePropriedade.Substring(0, nomePropriedade.IndexOf("."));

                    if (propriedade != null)
                    {
                        propertyInfoArray = propriedade.GetType().GetProperties();
                        foreach (PropertyInfo propertyInfo in propertyInfoArray)
                        {
                            if (propertyInfo.Name == propriedadeAntesDoPonto)
                            {
                                retorno = CarregarPropriedade(
                                    propertyInfo.GetValue(propriedade, null),
                                    nomePropriedade.Substring(nomePropriedade.IndexOf(".") + 1)
                                    );
                            }
                        }
                    }

                }
                else
                {
                    Type tpyPropertyType;
                    PropertyInfo pfoPropertyInfo;
                    if (propriedade != null)
                    {
                        tpyPropertyType = propriedade.GetType();
                        pfoPropertyInfo = tpyPropertyType.GetProperty(nomePropriedade);
                        retorno = pfoPropertyInfo.GetValue(propriedade, null);
                    }

                }

                return retorno;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                return null;
            }
        }

        private void dgwPrincipal_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            try
            {
                if ((dgwPrincipal.Rows[e.RowIndex].DataBoundItem != null) &&
                    (dgwPrincipal.Columns[e.ColumnIndex].DataPropertyName.Contains(".")))
                {
                    e.Value = CarregarPropriedade(
                        dgwPrincipal.Rows[e.RowIndex].DataBoundItem,
                        dgwPrincipal.Columns[e.ColumnIndex].DataPropertyName
                        );
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnSelecionar_Click(object sender, EventArgs e)
        {
            if (dgwPrincipal.Rows.Count <= 0)
            {
                MessageBox.Show("Nenhuma linha selecionada");
                return;
            }

            clienteSelecionada = dgwPrincipal.SelectedRows[0].DataBoundItem as Cliente;
            DialogResult = DialogResult.OK;
        }
    }
}
