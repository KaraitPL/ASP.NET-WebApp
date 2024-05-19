namespace WebApp.Models
{
    public class Kontakt
    {
        public enum Kategoria
        {
            służbowy,
            prywatny,
            inny
        }

        public int Id { get; set; }
        public string Imie { get; set; }
        public string Nazwisko {  get; set; }
        public string Email { get; set; }
        public string Haslo { get; set; }
        public Kategoria Kat { get; set; }
        public long Telefon { get; set; }
        public DateTime DataUrodzenia { get; set; }

        public Kontakt()
        {

        }
    }
}
