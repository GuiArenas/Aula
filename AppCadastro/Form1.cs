using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AppCadastro
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            //Necessário desativar a auto validação para que o sistema não cancele uma ação essencial
            //Como o fechamento da tela
            AutoValidate = AutoValidate.Disable;
        }

        private void btnClientes_Click(object sender, EventArgs e)
        {
            //Vamos chamar a tela de cadastro de clientes
            //Para uma tela ser exibida, ela precisa ser carregada na memória RAM

            //Primeiro, vou criar a variável que será o ponteiro
            //Deve-se criar a variável com o mesmo tipo de dado da tela, ou seja, a própria tela.
            //frmCadCliente é o Tipo de dados, e tela é a variável que sera o ponteiro
            //Precisamos instanciar a tela em memória, usando a palavra "new"
            //É preciso utilizar (); para criar a tela

            frmCadCliente tela = new frmCadCliente();

            //Acessar a propriedade da tela pela variável ponteiro, para exibir a tela ao usuário

            //tela.Show();
            tela.ShowDialog();

            //Show exibe a tela de maneira distinta
            //ShowDialog exibe a tela travando o restante do sistema
        }

        private void txtName_Validating(object sender, CancelEventArgs e)
        {
            //Aqui iremos criar a validação e notificação do campo exclusivamente para o campo txtName
            //Validar se o campo está preenchido

            if (string.IsNullOrEmpty(txtName.Text))
            {
                //Marcar o cancelamento da execução. EX: Parar a execução do botão salvar
                e.Cancel = true;
                //Definir mensagem a ser exibida para o usuário 
                errProvider.SetError(
                    txtName, "Preencha o campo corretamente!");
            }
            else
            {
                //Preciso cancelar o cancelamento 
                e.Cancel = false;
                //Limpar a mensagem de erro
                errProvider.SetError(
                    txtName, "");
            }
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            //Iremos validar se possui algum cancelamento, se não houver cancelamento, confirmar a
            //gravação dos dados para o usuário. Se houver, aprensetar a mensagem ao usuário.

            //Validamos se algum filho(componente em tela) possui uma solicitação de cancelamento
            if (ValidateChildren(
                ValidationConstraints.Enabled))
            {
                MessageBox.Show(
                    "Registro salvo com sucesso!", "Informação", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                Close();
            }
            else
            {
                MessageBox.Show("Preencha todos os campos corretamente!", "Atenção",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            //Na ação do botão Cancelar, iremos deixar a decisão por parte do usuário.
            //Iremos apresentar uma mensagem de SIM ou NÃO, para confirmação do cancelamento da alteração
            //MessageBoxButtons.YesOrNo para definir a opção de SIM ou NÃO
            //Precisamor definir o botão padrão que receberá o foco e será acionado ao pressionar o ENTER
            //MessageBoxDefaultButton.button2
            //O foco do button será na opção NÃO, para evitar que o cliente confirme sem ler a mensagem.
            //Validar o retorno da mensagem, utilizando: DialogoResult.Yes para validar se usuário clicou em SIM
            if (MessageBox.Show(
                "Deseja realmente descartar as alterações?", "Confirmação", MessageBoxButtons.YesNo,
                MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)
                == DialogResult.Yes)
            {
                //Se o usuário clicar em SIM, a tela será fechada
                Close();
            }

        }

        string SoNumero(string pTexto)
        {
            string retorno = "";

            for (int i = 0; i < pTexto.Length; i++)
            {
                //IsDigit pega traços "-", então nesse caso usar o IsNumber
                if (char.IsNumber(pTexto[i]))
                {
                    retorno += pTexto[i];
                }
            }

            return retorno;
        }

        private void mskCPF_Validating(object sender, CancelEventArgs e)
        {
            //Iremos remover a máscara do CPF
            string CPF = SoNumero(mskCPF.Text);

            //Agora podemos validar

            if (!string.IsNullOrEmpty(CPF))
            {
                e.Cancel = true;
                errProvider.SetError(mskCPF, "Preencha o CPF.");
            }
            //Validar se tem a quantidade correta de números
            else if(CPF.Length != 11)
            {
                e.Cancel= true;
                errProvider.SetError(mskCPF, "Preencha o CPF corretamente!");
            }
            else
            {
                //Esse encerramento é obrigatório em todos os eventos VALIDATING
                e.Cancel = false;
                errProvider.SetError(
                    mskCPF, "");
            }
        }
    }
}
