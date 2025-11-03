using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Game_On.Data.Context;
using Game_On.Models;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace Game_On.ViewModels
{
    public partial class DepartementVM : ObservableObject
    {
        private readonly ModelContext context;
        public DepartementVM()
        {
            this.context = new ModelContext();
            departements = new ObservableCollection<Departement>(context.Departement.ToList());
        }
        public ObservableCollection<Departement> departements { get; set; }

        [ObservableProperty]
        string nomDepartement = "";

        [RelayCommand]
        public async Task AjouterDepartement()
        {
            Departement departement = new Departement();
            departement.NomDepartement = NomDepartement;

            try
            {
                await context.Departement.AddAsync(departement);
                await context.SaveChangesAsync();
                departements.Add(departement);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Erreur EF : {ex.Message}");
                if (ex.InnerException != null)
                    Debug.WriteLine($"Détail SQL : {ex.InnerException.Message}");
            }

            NomDepartement = string.Empty;
        }
    }
}
