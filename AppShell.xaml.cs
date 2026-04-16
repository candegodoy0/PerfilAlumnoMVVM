namespace PerfilAlumnoMVVM;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();
        Routing.RegisterRoute("detalle", typeof(Views.DetallePage));
    }
}