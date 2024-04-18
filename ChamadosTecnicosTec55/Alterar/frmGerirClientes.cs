using ChamadosTecnicosTec55.Adicionar;
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

namespace ChamadosTecnicosTec55.Alterar
{
    public partial class frmGerirClientes : Form
    {
        string _conexao = ChamadosTecnicosTec55.Properties.Settings.Default.Conexao;

        public frmGerirClientes()
        {
            InitializeComponent();
        }
        private void ListarClientes()
        {
            ClienteDao clientedao = new ClienteDao(_conexao);
            string busca = txbBuscar.Text.ToString();
            
            DataSet ds = new DataSet();
            ds = clientedao.BuscaCliente(busca);

            dgvCliente.DataSource = ds;
            dgvCliente.DataMember = "Clientes";

        }

        private void frmGerirClientes_Load(object sender, EventArgs e)
        {
            ListarClientes();
        }

        private void btnIncluir_Click(object sender, EventArgs e)
        {
            var asss = new frmAdicionarCliente();
            asss.Show();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            if(txbBuscar.Text != "")
            {
                ListarClientes();
            }
            else
            {
                MessageBox.Show("Digite algo para buscar");
            }
        }

        private void btnAlterar_Click(object sender, EventArgs e)
        {
            if(dgvCliente.SelectedRows.Count > 0)
            {
                int codigo = Convert.ToInt32(dgvCliente.CurrentRow.Cells[0].Value);

                var frmAlterarCliente = new frm_AlterarCliente(codigo);
                frmAlterarCliente.ShowDialog();

                ListarClientes();
            }
            else
            {
                MessageBox.Show("Selecione um resgistro para alterãção");
            }
        }
    }
}
