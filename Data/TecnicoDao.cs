﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data; // ADO.net
using System.Data.SqlClient; // ADO para SQL SERVER

namespace Data
{
    public class TecnicoDao
    {
        private string _conexao;

        // Metodo Construtor => Inicializa Objeto buscando Conexao
        public TecnicoDao(string conexao)
        {
            // RECEBA Conexão 
            _conexao = conexao;
        }

        public void IncluiTecnico(Tecnico tecnico)
        {
            using(SqlConnection conexaoBd = new SqlConnection(_conexao))
            {
                string sql = "insert into Tecnicos (nome,especialidade,email,senha,obs) values (@nome,@especialdiade,@email,@senha,@obs)";

                using (SqlCommand comando = new SqlCommand(sql, conexaoBd))
                {
                    comando.Parameters.AddWithValue("@nome", tecnico.Nome);
                    comando.Parameters.AddWithValue("@especialdiade", tecnico.Especialidade);
                    comando.Parameters.AddWithValue("@email", tecnico.Email);
                    comando.Parameters.AddWithValue("@senha", tecnico.Senha);
                    comando.Parameters.AddWithValue("@obs", tecnico.Obs);

                    try
                    {
                        conexaoBd.Open();
                        comando.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Erro ao Incluir Tecnicos:" + ex.Message);
                    }
                }

            }
        }
        public DataSet BuscaTecnico(string pesquisa = "")
        {
            // Constante com o Código SQL que faz busca a partir de texto
            const string query = "Select * From Tecnicos Where Nome like @pesquisa";

            // Validar Erro
            try
            {
                using (var conexaoBd = new SqlConnection(_conexao))
                using (var comando = new SqlCommand(query, conexaoBd))
                using (var adaptador = new SqlDataAdapter(comando))
                {
                    string parametroPesquisa = $"%{pesquisa}%";
                    comando.Parameters.AddWithValue("@pesquisa", parametroPesquisa);
                    conexaoBd.Open();
                    var dsTecnicos = new DataSet();
                    adaptador.Fill(dsTecnicos, "Tecnicos");
                    return dsTecnicos;
                }

            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao buscar Tecnicos: {ex.Message}");
            }
        }
        // Xuxar aqui
        public Tecnico ObtemTecnico(int codigoTecnico)
        {
            // Define o sql para obter o cliente
            const string query = @"select * from Tecnicos where
                                   CodigoTecnico = @CodigoTecnico";
            Tecnico tecnico = null;

            try
            {
                using (var conexaoBd = new SqlConnection(_conexao))
                using (var comando = new SqlCommand(query, conexaoBd))
                {
                    comando.Parameters.AddWithValue("@CodigoTecnico", codigoTecnico);
                    conexaoBd.Open();
                    using (var reader = comando.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            tecnico = new Tecnico
                            {
                                CodigoTecnico = Convert.ToInt32(reader["CodigoTecnico"]),
                                Nome = reader["Nome"].ToString(),
                                Especialidade = reader["Especialidade"].ToString(),
                                Obs = reader["Obs"].ToString(),
                                Email = reader["Email"].ToString(),
                                Senha= reader["Senha"].ToString()
                            };
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao obter o tecnico {ex.Message}", ex);
            }
            return tecnico;
        }
        public void AlteraTecnico(Tecnico tecnico)
        {
            const string query = @"update Tecnicos set Nome=@Nome,
                                    Senha = @Senha,
                                    Email = @Email,
                                    Especialidade = @ Especialidade,
                                    Obs = @Observacao
                                    where CodigoTecnico = @CodigoTecnico";
            try
            {
                using (var conexaoBd = new SqlConnection(_conexao))
                using (var comando = new SqlCommand(query, conexaoBd))
                {
                    comando.Parameters.AddWithValue("@Nome", tecnico.Nome);
                    comando.Parameters.AddWithValue("@Senha", tecnico.Senha);
                    comando.Parameters.AddWithValue("@Email", tecnico.Email);
                    comando.Parameters.AddWithValue("@Especialidade", tecnico.Especialidade);
                    comando.Parameters.AddWithValue("@Observacao", tecnico.Obs);
                    comando.Parameters.AddWithValue("@CodTecnico", tecnico.CodigoTecnico);

                    conexaoBd.Open();
                    comando.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro{ex}");
            }
        }

        public void ExcluiTecnico(int codigoTecnico)
        {
            const string query = @"delete from tecnicos where codigotecnico = @codigoTecnico";

            try
            {
                using (var conexaoBd = new SqlConnection(_conexao))
                using (var comando = new SqlCommand(query, conexaoBd))
                {
                    comando.Parameters.AddWithValue("@CodigoTecbicos", codigoTecnico);
                    conexaoBd.Open();
                    comando.ExecuteNonQuery();

                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao obter o cliente {ex.Message}", ex);
            }
        }
    }
}
