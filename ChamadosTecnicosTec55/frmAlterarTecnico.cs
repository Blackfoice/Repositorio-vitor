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
    public partial class frmAlterarTecnico : Form
    {
        string _conexao = ChamadosTecnicosTec55.Properties.Settings.Default.Conexao;
        public frmAlterarTecnico(int codigo)
        {
            InitializeComponent();

            // Verifica se o código é maior que zero 
            if (codigo > 0)
            {
                // Cria uma instância do objeto Cliente
                Tecnico tecnico = new Tecnico();
                TecnicoDao tecnicoDao = new TecnicoDao(_conexao);

                tecnico = tecnicoDao.ObtemTecnico(codigo);

                //Se o cliente não foi encontrado
                if (tecnico == null)
                {
                    MessageBox.Show("Tecnico não encontrado");
                    this.Close();
                }

                txbCod.Text = tecnico.CodigoTecnico.ToString();
                txbNome.Text = tecnico.Nome;
                txbEspecialidade.Text = tecnico.Especialidade;
                txbSenha.Text = tecnico.Senha;
                txbObs.Text = tecnico.Obs;
                txbEmail.Text = tecnico.Email;
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            Tecnico tecnico = new Tecnico();
            TecnicoDao tecnicoDao = new TecnicoDao(_conexao);

            try
            {
                tecnico.Nome = txbNome.Text;
                tecnico.Especialidade = txbEspecialidade.Text;
                tecnico.Senha = txbSenha.Text;
                tecnico.Obs = txbObs.Text;
                tecnico.Senha = txbSenha.Text;

                int codigo = Convert.ToInt32(txbCod.Text);

                tecnico.CodigoTecnico = codigo;

                tecnicoDao.AlteraTecnico(tecnico);

                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}

