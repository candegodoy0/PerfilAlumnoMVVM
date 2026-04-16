using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace PerfilAlumnoMVVM.ViewModels
{

//Recibe los datos enviados dese la pantalla anterior
    [QueryProperty(nameof(Nombre), "nombre")]
    [QueryProperty(nameof(Edad), "edad")]
    [QueryProperty(nameof(Descripcion), "descripcion")]
    [QueryProperty(nameof(ImagenUrl), "imagen")]

    public class DetalleViewModel : INotifyPropertyChanged
    {
        private string nombre;
        public string Nombre
        {
            get => nombre;
            set
            {
                nombre = value;
                OnPropertyChanged();
            }
        }

        private int edad;
        public int Edad
        {
            get => edad;
            set
            {
                edad = value;
                OnPropertyChanged();
            }
        }

        private string descripcion;
        public string Descripcion
        {
            get => descripcion;
            set
            {
                descripcion = value;
                OnPropertyChanged();
            }
        }

        private string imagenUrl;
        public string ImagenUrl
        {
            get => imagenUrl;
            set
            {
                imagenUrl = value;
                OnPropertyChanged();
            }
        }

//notifica cambios para actualizar la UI automáticamente
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string prop = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
