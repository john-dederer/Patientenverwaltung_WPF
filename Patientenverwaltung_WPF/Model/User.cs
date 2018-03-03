using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Patientenverwaltung_WPF
{
    public class User : PropertyChangedNotification
    {        
        public long UserId
        {
            get { return GetValue(() => UserId); }
            set { SetValue(() => UserId, value); }
        }

        [StringLength(50, ErrorMessage = "Titel darf nicht länger als 50 Zeichen sein")]
        public string Title
        {
            get { return GetValue(() => Title); }
            set { SetValue(() => Title, value); }
        }

        [Required(ErrorMessage = "Feld darf nicht leer sein")]
        public string Name
        {
            get { return GetValue(() => Name); }
            set { SetValue(() => Name, value); }
        }

        [Required(ErrorMessage = "Feld darf nicht leer sein")]
        [UniqueUsername(ErrorMessage = "Benutzername existiert bereits")]
        public string Username
        {
            get { return GetValue(() => Username); }
            set { SetValue(() => Username, value); }
        }

        [Required(ErrorMessage = "Passwort darf nicht leer sein")]
        public string Passwordhash
        {
            get { return GetValue(() => Passwordhash); }
            set { SetValue(() => Passwordhash, value); }
        }
    }
}