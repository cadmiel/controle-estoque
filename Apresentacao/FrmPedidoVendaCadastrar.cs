using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Apresentacao
{
    public partial class FrmPedidoVendaCadastrar : Form
    {
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
            frmFilialPesquisar.ShowDialog();
        }

        
    }
}
