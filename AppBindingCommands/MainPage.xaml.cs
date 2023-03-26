namespace AppBindingCommands;

public partial class MainPage : ContentPage
{
	int count = 0;

	public MainPage()
	{
		InitializeComponent();
	}

	private void OnCounterClicked(object sender, EventArgs e)
	{
		count++;

		if (count == 1)
			CounterBtn.Text = $"Clicked {count} time";
		else
			CounterBtn.Text = $"Clicked {count} times";

		SemanticScreenReader.Announce(CounterBtn.Text);
	}

    // Irá recuperar o valor das Preferences
    private void btnAtualizarInformacoes_Clicked(object sender, EventArgs e)
    {
		string informacoes = string.Empty;

		if (Preferences.ContainsKey("AcaoInicial")) // Se tiver dado armazenado na key AcaoInicial, ele irá fazer o if
            informacoes += Preferences.Get("AcaoInicial", string.Empty); // string.Empty ---> Valor default (até que não tenha dado na key)

        if (Preferences.ContainsKey("AcaoStart"))
			informacoes += Preferences.Get("AcaoStart", string.Empty);

        if (Preferences.ContainsKey("AcaoSleep"))
            informacoes += Preferences.Get("AcaoSleep", string.Empty);

        if (Preferences.ContainsKey("AcaoResume"))
            informacoes += Preferences.Get("AcaoResume", string.Empty);

        // Quando existir dados na Preference, irá fazer o Get dos dados
        // Os dados irão "substituir" a string.Empty
        // E serão exibidos em tela na label lblInformacoes, no espaço do Text = "" (empty)
        lblInformacoes.Text = informacoes;
    }
}

