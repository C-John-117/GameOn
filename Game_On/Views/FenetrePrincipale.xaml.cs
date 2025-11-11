using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Game_On.Models;
using Game_On.ViewModels;

namespace Game_On.Views
{
    /// <summary>
    /// Logique d'interaction pour FenetrePrincipale.xaml
    /// </summary>
    public partial class FenetrePrincipale : Window
    {
        ConnexionVM connexionVm = new ConnexionVM();
        public Utilisateur tokenUser = new Utilisateur();

        public FenetrePrincipale()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
           
        }

        private async void Button_Deconnexion(object sender, RoutedEventArgs e)
        {
            connexionVm.UtilisateurAuthentifie = tokenUser;
            await connexionVm.DeconnexionCommand.ExecuteAsync(null);
            Connexion.Visibility = Visibility.Visible;
            Inscription.Visibility = Visibility.Collapsed;
            Jeu.Visibility = Visibility.Collapsed;
            


        }
    }
}
