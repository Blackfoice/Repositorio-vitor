using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data; // ADO.net
using System.Data.SqlClient; // ADO para SQL SERVER

namespace Data
{
    public class ClienteDao
    {
        private string _conexao;

        // Metodo Construtor => Inicializa Objeto buscando Conexao
        public ClienteDao(string conexao)
        {
            // RECEBA Conexão 
            _conexao = conexao;
        }

        // Inserir Cliente Vulgo XUXAR
        public void IncluiCliente(Cliente cliente)
        {
            using(SqlConnection conexaoBd = new SqlConnection(_conexao))
            {
                string sql = "insert into Clientes (nome,profissao,setor,obs) values (@nome,@profissao,@setor,@obs)";

                using (SqlCommand comando = new SqlCommand(sql, conexaoBd))
                {
                    comando.Parameters.AddWithValue("@nome", cliente.Nome);
                    comando.Parameters.AddWithValue("@profissao", cliente.Profissao);
                    comando.Parameters.AddWithValue("@setor", cliente.Setor);
                    comando.Parameters.AddWithValue("@obs", cliente.Obs);

                    try
                    {
                        conexaoBd.Open();
                        comando.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Erro ao Incluir Cliente:" + ex.Message);
                    }
                }

            }
        }

        public DataSet BuscaCliente(string pesquisa = "")
        {
            const string query = "Select * From Clientes Where Nome Like @pesquisa";

            try
            {
                using (var conexaoBd = new SqlConnection(_conexao))
                using (var comando = new SqlCommand(query, conexaoBd))
                using (var adaptador = new SqlDataAdapter(comando))
                {
                    string parametroPesquisa = $"%{pesquisa}%";
                    comando.Parameters.AddWithValue("@pesquisa", parametroPesquisa);
                    conexaoBd.Open();
                    var dsClientes = new DataSet();
                    adaptador.Fill(dsClientes, "Clientes");
                    return dsClientes;
                }
            }
            catch (Exception ex)
            {

                throw new Exception($"Erro ao Buscar Clientes: {ex.Message}");
            }     
        }
        public Cliente obtemCliente(int codigoCliente)
        {
            const string query = @"select * from Clientes where CodigoCliente = @CodigoCliente";

            Cliente cliente = null;

            try
            {
                using (var conexaoBd = new SqlConnection(_conexao))
                using (var comando = new SqlCommand(query,conexaoBd))
                {
                  comando.Parameters.AddWithValue("@CodigoCliente", codigoCliente);
                  conexaoBd.Open();
                     using(var reader = comando.ExecuteReader())
                     {
                        if (reader.Read())
                        {
                          cliente = new Cliente
                          {
                              CodigoCliente = Convert.ToInt32(reader["CodigoCliente"]),
                              Nome = reader["Nome"].ToString(),
                              Profissao = reader["Profissao"].ToString(),
                              Obs = reader["Obs"].ToString(),
                              Setor = reader["Setor"].ToString(),
                          };  
                        }
                     }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao obter o cliente{ex.Message}", ex);
            }
            return cliente;
        }
        public void AlterarCliente(Cliente cliente)
        {
            const string query = @"update Clientes set nome=@Nome,  
                                                       Setor=@Setor,
                                                       Profissao=@Profissao,
                                                       Obs=@Obs
                                                       where CodigoCliente = @CodigoCliente";
            try
            {
                using (var conexaoBd = new SqlConnection(_conexao))
                using (var comando = new SqlCommand(query, conexaoBd))
                {
                    comando.Parameters.AddWithValue("@Nome", cliente.Nome);
                    comando.Parameters.AddWithValue("@Setor", cliente.Setor);
                    comando.Parameters.AddWithValue("@Profissao", cliente.Profissao);
                    comando.Parameters.AddWithValue("@Obs", cliente.Obs);
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro{ex.Message}");
            }
        }
    }
}
