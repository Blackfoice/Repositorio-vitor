﻿using Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChamadosTecnicosTec55.Adicionar
{
    public partial class frmTecnicoAdicionar : Form
    {
        string _conexao = ChamadosTecnicosTec55.Properties.Settings.Default.Conexao;

        public frmTecnicoAdicionar()
        {
            InitializeComponent();
        }

        private void btnLimpar_Click(object sender, EventArgs e)
        {
            txbEmail.Clear();
            txbEspecialidade.Clear();
            txbNome.Clear();
            txbObs.Clear();
            txbSenha.Clear();
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            // Chama o objeto Cliente
            Tecnico tecnico = new Tecnico();
            TecnicoDao tecnicoDao = new TecnicoDao(_conexao);

            if (string.IsNullOrWhiteSpace(txbNome.Text) || string.IsNullOrWhiteSpace(txbObs.Text) || string.IsNullOrWhiteSpace(txbSenha.Text) || string.IsNullOrWhiteSpace(txbEspecialidade.Text) || string.IsNullOrWhiteSpace(txbEmail.Text))

            {
                MessageBox.Show("CADE OS DADOSSS ??");
            }
            else
            {
                // TODA VEZ QUE MEXER COM BD USAR TRY
                try
                {
                    // Preenche o Objeto Cliente
                    tecnico.Nome = txbNome.Text;
                    tecnico.Email = txbEmail.Text;
                    tecnico.Obs = txbObs.Text;
                    tecnico.Especialidade = txbEspecialidade.Text;
                    tecnico.Senha = txbSenha.Text;

                    // CHAMA O DAO para incluir o cliente
                    tecnicoDao.IncluiTecnicos(tecnico);

                    MessageBox.Show("Cadastrado com sucesso !");
                    this.Close();

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao Cadastrar" + ex, "Atenção",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);
                }
            }
        }
    }
}

