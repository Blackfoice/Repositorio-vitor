using ChamadosTecnicosTec55.Adicionar;
using ChamadosTecnicosTec55.Alterar;
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
    public partial class frmGestorTecnico : Form
    {
        string _conexao = ChamadosTecnicosTec55.Properties.Settings.Default.Conexao;
        public frmGestorTecnico()
        {
            InitializeComponent();
        }
        private void Listartecnico()
        {
            // Chama o Cliente DAO 
            TecnicoDao tecnicoDao = new TecnicoDao(_conexao);
            // Captura o valor digitado na barra texto TXB
            string busca = txbBuscar.Text.ToString();
            // Chama o Metodo BuscaCliente do objeto 
            DataSet ds = new DataSet();
            ds = tecnicoDao.BuscaTecnico(busca);
            // Defini o DataSource do DataGridView
            dgvTecnicos.DataSource = ds;
            // DEFINI O MEMBRO DO DATASET 
            dgvTecnicos.DataMember = "Tecnicos";
        }


        private void btnIncluir_Click(object sender, EventArgs e)
        {
            var frmaddTecnico = new frmTecnicoAdicionar();
            frmaddTecnico.Show();
        }

        private void btnB_Click(object sender, EventArgs e)
        {
            if (txbBuscar.Text != "")
            {
                Listartecnico();
            }
            else
            {
                MessageBox.Show("Digite algo para buscar");
            }
        }

        private void btnAlterar_Click(object sender, EventArgs e)
        {
            // Verifique se algum linha está selecionada no DGV
            if (dgvTecnicos.SelectedRows.Count > 0)
            {
                // obtém o código do cliente da linha selecionada
                int codigo = Convert.ToInt32(dgvTecnicos.CurrentRow.Cells[0].Value);

                var frmAlteraTecnico = new frmAlterarTecnico(codigo);
                frmAlteraTecnico.ShowDialog();

                // Apos a tela fechar listar os clientes cadastrados 
                Listartecnico();
            }
            else
            {
                // Exibe uma mensagem de Aviso se nenhuma linha estiver selecionada
                MessageBox.Show("Selecione um registro para alteração");
            }
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            if (dgvTecnicos.SelectedRows.Count > 0)
            {
                int codigo = Convert.ToInt32(dgvTecnicos.CurrentRow.Cells[0].Value);

                var resultado = MessageBox.Show("Deseja Excluir?", "Pesquisa", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

                if (resultado == DialogResult.Yes)
                {
                    ClienteDao clienteDao = new ClienteDao(_conexao);
                    clienteDao.ExcluiCliente(codigo);
                    Listartecnico();
                }
                else
                {
                    MessageBox.Show("Selecione um Registro");
                }
            }
        }

        private void frmGestorTecnico_Load(object sender, EventArgs e)
        {
            Listartecnico();
        }
    }
}
