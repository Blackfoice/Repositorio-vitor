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

                cliente = clienteDao.obtemCliente(codigo);

                if (cliente == null) 
                {
                    MessageBox.Show("Cliente não encontrado");
                    this.Close();

                }
                txbCodigo.Text = cliente.CodigoCliente.ToString();
                txbNome.Text = cliente.Nome;
                txbProfissao.Text = cliente.Profissao;
                txbSetor.Text = cliente.Setor;
                txbObs.Text = cliente.Obs;



            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            Cliente cliente = new Cliente();
            ClienteDao clientedao = new ClienteDao(_conexao);

            try
            {
                cliente.Nome = txbNome.Text;
                cliente.Profissao = txbProfissao.Text;
                cliente.Setor = txbSetor.Text;
                cliente.Obs = txbObs.Text;

                int codigo = Convert.ToInt32(txbCodigo.Text);

                cliente.CodigoCliente = codigo;

                cliente.AlterarCliente(cliente);

                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);    
            }
        }
    }
}
