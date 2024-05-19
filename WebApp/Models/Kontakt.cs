namespace WebApp.Models
{
    public class Kontakt
    {
        public int Id { get; set; }
        public string Imie { get; set; }
        public string Nazwisko {  get; set; }
        public string Email { get; set; }
        public string Haslo { get; set; }
        public string Kategoria { get; set; }
        public long Telefon { get; set; }
        public DateTime DataUrodzenia { get; set; }

        public Kontakt()
        {

        }
    }
}
