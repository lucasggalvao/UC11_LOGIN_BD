using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
namespace UC11_LOGIN_BD
{
    public partial class Form1 : Form
    {
        string servidor;       
        MySqlConnection conexao;
        MySqlCommand comando;

        public Form1()
        {
            InitializeComponent();
            servidor = "Server=localhost;Database=login_bd;Uid=root;Pwd=";
            conexao = new MySqlConnection(servidor);
            comando = conexao.CreateCommand();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void buttonCADASTRAR_Click(object sender, EventArgs e)
        {
            bool novo_usuario = true;

            try
            {
                conexao.Open();
                comando.CommandText = "SELECT usuario FROM tbl_usuarios WHERE usuario = '" + textBoxUSUARIO.Text + "' ;";
                
                MySqlDataReader valida_usuario = comando.ExecuteReader();

                if (valida_usuario.Read())
                {
                    novo_usuario = false;
                    MessageBox.Show("Usuario ja existente!");
                }
            }
            catch (Exception erro)
            {
                MessageBox.Show("Erro ao cadastrar novo usuario. Fale com o administrador do sistema.");
            }
            finally
            {
                conexao.Close();
            }

            if (novo_usuario)
            {
                try
                {
                    conexao.Open();
                    comando.CommandText = "INSERT INTO tbl_usuarios(usuario, senha) VALUES ('" + textBoxUSUARIO.Text + "','" + textBoxSENHA.Text + "');";
                    comando.ExecuteNonQuery();
                    MessageBox.Show("Usuario cadastrado com sucesso!");

                }
                catch (Exception erro)
                {
                    //MessageBox.Show(erro.Message);
                    MessageBox.Show("Erro ao cadastrar novo usuario. Fale com o administrador do sistema.");
                }
                finally
                {
                    conexao.Close();
                }
            }

         
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void buttonENTRAR_Click(object sender, EventArgs e)
        {
            try
            {
                conexao.Open();
                comando.CommandText = "SELECT usuario, senha FROM tbl_usuarios WHERE usuario = '" + textBoxUSUARIO.Text + "' AND senha = '" + textBoxSENHA.Text + "';";

                MySqlDataReader resultado = comando.ExecuteReader();

                if (resultado.Read())
                {
                    MessageBox.Show("Usuario e senha corretos!");
                }
                else
                {
                    MessageBox.Show("Usuario e/ou senha incorretos!");
                }
            }
            catch (Exception erro)
            {

            }
            finally
            {
                conexao.Close();
            }
        }
    }
}
