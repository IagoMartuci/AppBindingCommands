using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AppBindingCommands.ViewModels
{
    // Sempre que chamamos uma interface ":", temos que implementá-la: CTRL + . ---> Implementar interface
    public class UsuarioViewModel : INotifyPropertyChanged // Interface de notificação de mudança de propriedade
    {
        // Implementação da interface
        public event PropertyChangedEventHandler PropertyChanged;

        void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        // Método construtor da classe UsuarioViewModel
        public UsuarioViewModel()
        {
            // Define o que será executado
            ShowMessageCommand = new Command(ShowMessage);
            CountCommand = new Command(async () => await CountCharacters());
            CleanCommand = new Command(async () => await CleanConfirmation());
            OptionCommand = new Command(async () => await ShowOptions());
        }

        // CLASSES NO MAUI
        // Criação de atributo: private e nome com a primeira letra minúscula, o valor do atributo pode ser alterado
        private string name = string.Empty; //CTRL + R, E                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                 

        // Criação da propriedade: public e com a primeira letra maiúscula, o valor da propriedade não pode ser alterado
        public string Name
        {
            get => name;
            set
            {
                if (name == null) // Faz o if se nenhum valor for atribuído para o atributo name
                    return; // return vazio: normalmente o return sem expressão é utilizado para encerrar um membro da função antecipadamente

                name = value; // A palavra value faz referência ao valor que o código do cliente está tentando atribuir à propriedade ou ao indexador
                OnPropertyChanged(nameof(Name));
                OnPropertyChanged(nameof(DisplayName));
                OnPropertyChanged(nameof(LenghtName));
            }
        }

        public string DisplayName => $"Nome digitado: {Name}"; // Propriedade DisplayName, recebe os dados da propriedade Name
        public string LenghtName => $"Quantidade de caracteres digitados: {Name.Length}"; // Propriedade LenghtName, recebe os dados da propriedade Name e conta o número de caracteres

        /*Já nas linhas 46, 47 e 48 é basicamente o que vai acontecer quando o usuário digitar algo na View,
        vai entrar no Set da Propriedade name, que não estando mais nula, vai disparar um evento OnPropertyChange
        para estas duas propriedades que estão nos parenteses das linhas 46, 47 e 48, dessa forma o conteúdo vai voltar
        atualizado na tela, ou seja, a Label será atualizada*/

        // Atributo
        string displayMessage = string.Empty;

        // Propriedade
        public string DisplayMessage
        {
            get => displayMessage;
            set
            {
                if (displayMessage is null)
                    return;

                displayMessage = value;
                OnPropertyChanged(nameof(DisplayMessage));
            }
        }

        // Propriedade
        public ICommand ShowMessageCommand { get; }

        // Método
        public void ShowMessage()
        {
            /*DateTime data = DateTime.MinValue;
            string dataString = Preferences.Get("dtAtual", data.ToString());
            DisplayMessage = $"Boa noite {Name}, hoje é {dataString}";*/

            // OU

            string data = Preferences.Get("dtAtual", "");
            DisplayMessage = $"Boa noite {Name}, hoje é {data}";

            // Assim funcionaria se eu não tivesse utilizado o globalization e convertido o DateTime para strings no App.xaml.cs
            /*string data = Preferences.Get("dtAtual", DateTime.MinValue);
            DisplayMessage = $"Boa noite {Name}, hoje é {data}";*/

            // Traz a data e hora atual, e não o que está na Preferences "dtAtual"
            /*string data = DateTime.Now.ToString();
            DisplayMessage = $"Boa noite {Name}, hoje é {data}";*/
        }

        public ICommand CountCommand { get; }

        // Sempre que criamos um método async no MAUI ele será seguido da palavra Task
        public async Task CountCharacters()
        {
            string nameLegenth = string.Format("Seu nome tem {0} caracteres", name.Length);
            // Aguarda a ação do usuário
            await Application.Current.MainPage.DisplayAlert("Informação", nameLegenth, "Ok");
        }

        public ICommand CleanCommand { get; }

        public async Task CleanConfirmation()
        {
            if (await Application.Current.MainPage
                    .DisplayAlert("Confirmação", "Confirma limpeza dos dados?", "Sim", "Não"))
            {
                Name = string.Empty;
                DisplayMessage = string.Empty;
                OnPropertyChanged(Name);
                OnPropertyChanged(nameof(DisplayMessage));

                await Application.Current.MainPage
                    .DisplayAlert("Informação", "Limpeza realizada com sucesso", "Ok");
            }
        }

        public ICommand OptionCommand { get; }

        public async Task ShowOptions()
        {
            string result = await Application.Current.MainPage
                .DisplayActionSheet("Selecione uma opção: ", "",
                "Cancelar", "Limpar", "Contar Caracteres", "Exibir Saudação");

            if (result is not null) // Validação para verificar se alguma opção foi escolhida
            {
                if (result.Equals("Limpar"))
                    await CleanConfirmation();

                if (result.Equals("Contar Caracteres"))
                    await CountCharacters();

                if (result.Equals("Exibir Saudação"))
                    ShowMessage();
            }
        }
    }
}