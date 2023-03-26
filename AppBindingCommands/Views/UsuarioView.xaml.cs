using AppBindingCommands.ViewModels;

namespace AppBindingCommands.Views;

public partial class UsuarioView : ContentPage
{
	private UsuarioViewModel viewModel;

	public UsuarioView()
	{
		InitializeComponent();
        // Contexto de carregamento da UsuarioView ser� UsuarioViewModel
        // Vinculando a View (Content Page) a classe criada na pasta ViewModels, como est�o em locais diferentes, ser� necess�rio o using da pasta ViewModels
		viewModel = new UsuarioViewModel();
		BindingContext = viewModel;
	}
}