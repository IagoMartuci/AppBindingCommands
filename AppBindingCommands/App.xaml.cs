using System.Globalization;

namespace AppBindingCommands;

// Classe Application possui 3 métodos virtuais (OnStart, OnSleep e OnResume) que podem ser sobrescritos para editar o ciclo de vida do app
public partial class App : Application
{
    CultureInfo ptBr = new CultureInfo("pt-BR");
    public App()
	{
        InitializeComponent();

        DateTime data = DateTime.Now;
        // Preferences: ideal para armazenarmos variáveis e dados (id, tokens e etc)
        // Set: qual valor vamos colocar na key
        Preferences.Set("dtAtual", data.ToString("G", ptBr));
		Preferences.Set("AcaoInicial", string.Format("* App executado: {0}. \n", data.ToString("G", ptBr)));

        // Propriedade MainPage: define qual a página inicial do app
		MainPage = new AppShell();
	}

    // Sobrescrevendo métodos da classe Application com o override (permite que alteremos algo que faz parte do framework)
    protected override void OnStart()
    {
        base.OnStart();
		Preferences.Set("AcaoStart", string.Format("* App iniciado: {0}. \n", DateTime.Now.ToString("G", ptBr)));
    }
    protected override void OnSleep()
    {
        base.OnSleep();
        Preferences.Set("AcaoSleep", string.Format("* App em segundo plano: {0}. \n", DateTime.Now.ToString("G", ptBr)));
    }
    protected override void OnResume()
    {
        base.OnResume();
        Preferences.Set("AcaoResume", string.Format("* App reativado: {0}. \n", DateTime.Now.ToString("G", ptBr)));
    }
}
