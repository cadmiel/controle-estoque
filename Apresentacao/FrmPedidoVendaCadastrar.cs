using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ObjetoTransferencia;

namespace Apresentacao
{
    public partial class FrmPedidoVendaCadastrar : Form
    {

        Filial filialEmitente;
        Cliente clienteDestinatario; 

        public FrmPedidoVendaCadastrar()
        {
            InitializeComponent();
        }

        private void txtFechar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnEmitente_Click(object sender, EventArgs e)
        {
            FrmFilialPesquisar frmFilialPesquisar = new FrmFilialPesquisar();
            DialogResult resultado = frmFilialPesquisar.ShowDialog();

            if (resultado == DialogResult.OK)
            {
                txtEmitente.Text = frmFilialPesquisar.filialSelecionada.Pessoa.Nome;
                filialEmitente = frmFilialPesquisar.filialSelecionada;
            }
        }

        private void txtEmitente_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Back || e.KeyCode == Keys.Delete)
            { 
                txtEmitente.Clear();
                filialEmitente = null;

            }
        }

        private void btnDestinatario_Click(object sender, EventArgs e)
        {
            FrmClientePesquisar frmClientePesquisar = new FrmClientePesquisar();

            DialogResult resultado = frmClientePesquisar.ShowDialog();

            if (resultado == DialogResult.OK)
            {
                txtDestinatario.Text = frmClientePesquisar.clienteSelecionada.Pessoa.Nome;
                clienteDestinatario = frmClientePesquisar.clienteSelecionada;
            }
        }

        private void txtEmitente_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtDestinatario_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Back || e.KeyCode == Keys.Delete)
            {
                txtDestinatario.Clear();
                clienteDestinatario = null;

            }
        }

        
    }
}
