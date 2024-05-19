using WebApp.Models;

namespace WebApp.ViewModels
{
    public class KontaktCreateViewModel
    {
        public Kontakt Kontakt { get; set; }
        public List<Kategoria> WszystkieKategorie { get; set; }
        public List<Podkategoria> WszystkiePodkategorie { get; set; }


    }
}
