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

        [RelayCommand]
        public async Task Inscription()
        {
            Utilisateur utilisateur = new Utilisateur();
            Departement departement = new Departement();
            Entreprise entreprise = new Entreprise();

            entreprise.NomEntreprise = "Hourglass Unlimited";
            departement.NomDepartement = "Gestionnaires de projet";

            utilisateur.NomUtilisateur = NomUtilisateur;
            utilisateur.PrenomUtilisateur = PrenomUtilisateur;
            utilisateur.Entreprise = entreprise;
            utilisateur.Departement = departement;
            utilisateur.LoginTime = DateTime.Now;
            utilisateur.Email = Email;
            utilisateur.MotDePasse = MotDePasse;
            utilisateur.Pseudo = Pseudo;



            try
            {
                await context.Utilisateur.AddAsync(utilisateur);
                await context.SaveChangesAsync();
                utilisateurs.Add(utilisateur);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Erreur EF : {ex.Message}");
                if (ex.InnerException != null)
                    Debug.WriteLine($"Détail SQL : {ex.InnerException.Message}");
            }

            NomUtilisateur = String.Empty;
            PrenomUtilisateur = String.Empty;
        }
    }
}
