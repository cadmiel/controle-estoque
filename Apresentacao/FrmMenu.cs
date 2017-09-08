﻿using System;
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
    public partial class FrmMenu : Form
    {
        public FrmMenu()
        {
            InitializeComponent();
        }

        private void sairToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void menuPedido_Click(object sender, EventArgs e)
        {
            FrmPedidoVendaCadastrar frmPedidoVendaCadastrar = new FrmPedidoVendaCadastrar();
            frmPedidoVendaCadastrar.MdiParent = this;
            frmPedidoVendaCadastrar.Show();
        }
    }
}
