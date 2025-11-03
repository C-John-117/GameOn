using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Game_On.Data.Context;
using Game_On.Models;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace Game_On.ViewModels
{
    public partial class EntrepriseVM : ObservableObject
    {
        private readonly ModelContext context;
        public EntrepriseVM()
        {
            this.context = new ModelContext();
            entreprises = new ObservableCollection<Entreprise>(context.Entreprise.ToList());
        }

        public ObservableCollection<Entreprise> entreprises { get; set; }

        [ObservableProperty]
        string nomEntreprise = "";

        [RelayCommand]
        public async Task AjouterEntreprise()
        {
            Entreprise entreprise = new Entreprise();
            entreprise.NomEntreprise = NomEntreprise;

            try
            {
                await context.Entreprise.AddAsync(entreprise);
                await context.SaveChangesAsync();
                entreprises.Add(entreprise);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Erreur EF : {ex.Message}");
                if (ex.InnerException != null)
                    Debug.WriteLine($"Détail SQL : {ex.InnerException.Message}");
            }

            NomEntreprise = string.Empty;

        }

        [RelayCommand]
        public async Task RecupererEntreprises()
        {
            Entreprise entreprise = await context.Entreprise.FindAsync();
        }
    }
}
