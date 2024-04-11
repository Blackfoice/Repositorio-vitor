using Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChamadosTecnicosTec55
{
    public partial class frm_AlterarCliente : Form
    {
        string _conexao = ChamadosTecnicosTec55.Properties.Settings.Default.Conexao;

        public frm_AlterarCliente(int codigo)
        {
            InitializeComponent();

            if(codigo > 0)
            {
                Cliente cliente = new Cliente();
                ClienteDao clienteDao = new ClienteDao(_conexao);

                cliente = clienteDao.ObtemCliente(codigo);

                if (cliente == null) 
                {
                    MessageBox.Show("Clienre não encontrado");
                    this.Close();

                }
                txbCodigo.Text = cliente.CodigoCliente.ToString();
                txbCodigo.Text = cliente.Nome;
                txbCodigo.Text = cliente.Profissao;
                txbCodigo.Text = cliente.Setor;
                txbCodigo.Text = cliente.Obs;



            }
        }

    }
}
