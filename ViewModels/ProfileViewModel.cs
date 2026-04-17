using System.ComponentModel;
using System.Runtime.CompilerServices;
using PerfilAlumnoMVVM.Models;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;

namespace PerfilAlumnoMVVM.ViewModels
{

//Recibe parametros cuando navegamos a otra pagina
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

//propiedades 
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

//comandos
        public Command GuardarCommand { get; }

        public Command IrDetalleCommand { get; }

        public ProfileViewModel()
        //datos inciales (simulacion de perfil cargado)
        {
            Nombre = "Cande Godoy";
            Edad = 22;
            Descripcion = "Tengo 22 años y estoy estudiando Programación. Empecé desde cero, pero con el tiempo me fui enganchando mucho y hoy realmente me gusta. Soy constante, curiosa y bastante detallista, siempre trato de entender bien las cosas.\r\n\r\nTrabajo mientras estudio, lo que me hizo más responsable y organizada. En lo personal soy tranquila, sociable y muy familiera. Tengo una perrita que es súper importante para mí.\r\n\r\nMe gusta aprender cosas nuevas y seguir creciendo todo el tiempo.";
            ImagenUrl = "perfil.jpeg";

            GuardarCommand = new Command(Guardar);
            IrDetalleCommand = new Command(IrADetalle);
        }

        //Guarda datos con valiaciones (simulacion)
        private async void Guardar()
        {
            if (!await ValidarDatos()) return;

            if (DeviceInfo.Platform == DevicePlatform.Android)
            {
                var toast = Toast.Make("Datos actualizados correctamente", ToastDuration.Short);
                await toast.Show();
            }
            else
            {
                await Shell.Current.DisplayAlert("Éxito", "Datos actualizados correctamente", "OK");
            }
        }

        // Navega a la otra pantalla
        private async void IrADetalle()
        {
            if (!await ValidarDatos()) return;
            try
            {
                //Feedback visual antes de navegar
                if (DeviceInfo.Platform == DevicePlatform.Android)
                {
                    var toast = Toast.Make("Cargando perfil...", ToastDuration.Short);
                    await toast.Show();
                }
                else
                {
                    await Shell.Current.DisplayAlert("Info", "Cargando perfil...", "OK");
                }

                await Shell.Current.GoToAsync(
                    $"detalle?nombre={Nombre}&edad={Edad}&descripcion={Descripcion}&imagen={ImagenUrl}");
            }
            catch (Exception ex)
            {
                Mensaje = $"Error al navegar: {ex.Message}";
            }
        }

//validaciones centralizadas
        private async Task<bool> ValidarDatos()
        {
            // Nombre vacío
            if (string.IsNullOrWhiteSpace(Nombre))
            {
                await MostrarError("El nombre no puede estar vacío");
                return false;
            }

            // Nombre con números
            if (Nombre.Any(char.IsDigit))
            {
                await MostrarError("El nombre no puede contener números");
                return false;
            }

            // Edad inválida
            if (Edad <= 0)
            {
                await MostrarError("La edad debe ser mayor a 0");
                return false;
            }

            // Descripción vacía
            if (string.IsNullOrWhiteSpace(Descripcion))
            {
                await MostrarError("La descripción no puede estar vacía");
                return false;
            }

            return true;
        }

//metodo reutilizable para errores
        private async Task MostrarError(string mensaje)
        {
            await Shell.Current.DisplayAlert("Error", mensaje, "OK");
        }

//notificacion de cambios (binding)
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propiedad = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propiedad));
        }
    }
}
