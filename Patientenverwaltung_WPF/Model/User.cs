using System.ComponentModel;

namespace Patientenverwaltung_WPF
{
    public class User : Datamodel, IDataErrorInfo
    {
        public long UserId { get; set; }
        public string Title { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public string Passwordhash { get; set; }

        public string this[string columnName]
        {
            get
            {
                switch (columnName)
                {
                    case "UserId":
                        if (UserId < 0) return "UserId darf nicht kleiner 0 sein";
                        break;
                    case "Title":
                        if (string.IsNullOrEmpty(Title)) return "Feld darf nicht leer sein.";
                        break;
                    case "Name":
                        if (string.IsNullOrEmpty(Name)) return "Feld darf nicht leer sein.";
                        break;
                    case "Username":
                        if (string.IsNullOrEmpty(Username)) return "Feld darf nicht leer sein.";
                        break;
                }

                return string.Empty;
            }
        }

        public string Error { get; set; }

        public void SetValues(User user, bool overwriteValues = false)
        {
            if (overwriteValues)
            {
                if (UserId != user.UserId) UserId = user.UserId;
                if (Title != user.Title) Title = user.Title;
                if (Name != user.Name) Name = user.Name;
                if (Username != user.Username) Username = user.Username;
                if (Passwordhash != user.Passwordhash) Passwordhash = user.Passwordhash;
            }
            else
            {
                if (user.UserId != UserId || user.UserId > 0) UserId = user.UserId;
                if (string.IsNullOrEmpty(user.Title) || user.Title != Title) Title = user.Title;
                if (string.IsNullOrEmpty(user.Name) || user.Name != Name) Name = user.Name;
                if (string.IsNullOrEmpty(user.Username) || user.Username != Username) Username = user.Username;
                if (string.IsNullOrEmpty(user.Passwordhash) || user.Passwordhash != Passwordhash)
                    Passwordhash = user.Passwordhash;
            }
        }
    }
}