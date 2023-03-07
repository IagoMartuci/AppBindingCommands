using AppBindingCommands.ViewModels;

namespace AppBindingCommands.Views;

public partial class UsuarioView : ContentPage
{
	private UsuarioViewModel viewModel;

	public UsuarioView()
	{
		InitializeComponent();
        // Contexto de carregamento da UsuarioView será UsuarioViewModel
        // Vinculando a View (Content Page) a classe criada na pasta ViewModels, como estão em locais diferentes, será necessário o using da pasta ViewModels
		viewModel = new UsuarioViewModel();
		BindingContext = viewModel;
	}
}