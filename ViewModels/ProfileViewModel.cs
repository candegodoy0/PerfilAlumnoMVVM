using System.ComponentModel;
using System.Runtime.CompilerServices;
using PerfilAlumnoMVVM.Models;

namespace PerfilAlumnoMVVM.ViewModels
{
    [QueryProperty(nameof(Nombre), "nombre")]
    [QueryProperty(nameof(Edad), "edad")]
    [QueryProperty(nameof(Descripcion), "descripcion")]
    [QueryProperty(nameof(ImagenUrl), "imagenUrl")]

    public class ProfileViewModel : INotifyPropertyChanged
    {
        private UserProfile _user = new UserProfile();


        // Mensaje para manejar errores simples 
        private string _mensaje;
        public string Mensaje
        {
            get => _mensaje;
            set
            {
                _mensaje = value;
                OnPropertyChanged();
            }
        }

        public string Nombre
        {
            get => _user.Nombre;
            set
            {
                if (_user.Nombre != value)
                {
                    _user.Nombre = value;
                    OnPropertyChanged();
                }
            }
        }

        public int Edad
        {
            get => _user.Edad;
            set
            {
                if (_user.Edad != value)
                {
                    _user.Edad = value;
                    OnPropertyChanged();
                }
            }
        }

        public string Descripcion
        {
            get => _user.Descripcion;
            set
            {
                if (_user.Descripcion != value)
                {
                    _user.Descripcion = value;
                    OnPropertyChanged();
                }
            }
        }

        public string ImagenUrl
        {
            get => _user.ImagenUrl;
            set
            {
                if (_user.ImagenUrl != value)
                {
                    _user.ImagenUrl = value;
                    OnPropertyChanged();
                }
            }
        }

        public Command GuardarCommand { get; }

        public Command IrDetalleCommand { get; }

        public ProfileViewModel()
        {
            Nombre = "Cande Godoy";
            Edad = 22;
            Descripcion = "Tengo 22 años y estoy estudiando Programación. Empecé desde cero, pero con el tiempo me fui enganchando mucho y hoy realmente me gusta. Soy constante, curiosa y bastante detallista, siempre trato de entender bien las cosas.\r\n\r\nTrabajo mientras estudio, lo que me hizo más responsable y organizada. En lo personal soy tranquila, sociable y muy familiera. Tengo una perrita que es súper importante para mí.\r\n\r\nMe gusta aprender cosas nuevas y seguir creciendo todo el tiempo.";
            ImagenUrl = "perfil.jpeg";

            GuardarCommand = new Command(Guardar);
            IrDetalleCommand = new Command(IrADetalle);
        }

        //Guarda datos con valiaciones
        private async void Guardar()
        {
            if (!ValidarDatos()) return;

            await Shell.Current.DisplayAlert("Guardado", "Datos actualizados correctamente", "OK");
        }

        // Navega a la otra pantalla
        private async void IrADetalle()
        {
            if (!ValidarDatos()) return;

            try
            {
                await Shell.Current.DisplayAlert("Info", "Cargando perfil...", "OK");

                await Shell.Current.GoToAsync(
                    $"detalle?nombre={Nombre}&edad={Edad}&descripcion={Descripcion}&imagen={ImagenUrl}");
            }
            catch (Exception ex)
            {
                Mensaje = $"Error al navegar: {ex.Message}";
            }
        }

        private bool ValidarDatos()
        {
            // Nombre vacío
            if (string.IsNullOrWhiteSpace(Nombre))
            {
                Shell.Current.DisplayAlert("Error", "El nombre no puede estar vacío", "OK");
                return false;
            }

            // Nombre con números
            if (Nombre.Any(char.IsDigit))
            {
                Shell.Current.DisplayAlert("Error", "El nombre no puede contener números", "OK");
                return false;
            }

            // Edad inválida
            if (Edad <= 0)
            {
                Shell.Current.DisplayAlert("Error", "La edad debe ser mayor a 0", "OK");
                return false;
            }

            // Descripción vacía
            if (string.IsNullOrWhiteSpace(Descripcion))
            {
                Shell.Current.DisplayAlert("Error", "La descripción no puede estar vacía", "OK");
                return false;
            }

            return true;
        }


        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propiedad = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propiedad));
        }
    }
}