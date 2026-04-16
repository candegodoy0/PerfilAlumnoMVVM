namespace PerfilAlumnoMVVM.Views;

public partial class DetallePage : ContentPage
{
    public DetallePage()
    {
        InitializeComponent();
        BindingContext = new PerfilAlumnoMVVM.ViewModels.ProfileViewModel();
    }
}