using System.ComponentModel;
using System.Runtime.CompilerServices;
using PerfilAlumnoMVVM.Models;

namespace PerfilAlumnoMVVM.ViewModels
{
    public class ProfileViewModel : INotifyPropertyChanged
    {
        private UserProfile _user = new UserProfile();

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

        public ProfileViewModel()
        {
            Nombre = "Cande";
            Edad = 22;
            Descripcion = "Estudiante de programación";
            ImagenUrl = "perfil.jpeg";
            GuardarCommand = new Command(Guardar);
        }

        private async void Guardar()
        {
            // Nombre vacío
            if (string.IsNullOrWhiteSpace(Nombre))
            {
                await Shell.Current.DisplayAlert("Error", "El nombre no puede estar vacío", "OK");
                return;
            }

            // Nombre con números
            if (Nombre.Any(char.IsDigit))
            {
                await Shell.Current.DisplayAlert("Error", "El nombre no puede contener números", "OK");
                return;
            }

            // Edad inválida
            if (Edad <= 0)
            {
                await Shell.Current.DisplayAlert("Error", "La edad debe ser mayor a 0", "OK");
                return;
            }

            // Descripción vacía
            if (string.IsNullOrWhiteSpace(Descripcion))
            {
                await Shell.Current.DisplayAlert("Error", "La descripción no puede estar vacía", "OK");
                return;
            }

            await Shell.Current.DisplayAlert("Guardado", "Datos actualizados correctamente", "OK");
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propiedad = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propiedad));
        }
    }
}