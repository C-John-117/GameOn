using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Game_On.Data.Context;
using Game_On.Models;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace Game_On.ViewModels
{
    public partial class UtilisateurVM : ObservableObject
    {
        private readonly ModelContext context;
        public UtilisateurVM()
        {
            this.context = new ModelContext();
            utilisateurs = new ObservableCollection<Utilisateur>(context.Utilisateur.ToList());

        }
        public ObservableCollection<Utilisateur> utilisateurs { get; set; }

        [ObservableProperty]
        string _nomUtilisateur = "";

        [ObservableProperty]
        string _prenomUtilisateur = "";

        [ObservableProperty]
        Departement _departement;


        [ObservableProperty]
        Entreprise _entreprise;

        [ObservableProperty]
        DateTime _dateDeConnexion;

        [ObservableProperty]
        string _email = "";

        [ObservableProperty]
        String _pseudo = "";

        [ObservableProperty]
        string _motDePasse = ""; 

        //[ObservableProperty]
        //int _score;

        [ObservableProperty]
        DateTime _dateDeDeconnexion;
    }
}
