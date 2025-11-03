using Game_On.ViewModels;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Game_On.Views
{
    /// <summary>
    /// Logique d'interaction pour PageInscription.xaml
    /// </summary>
    public partial class PageInscription : UserControl
    {
        public PageInscription()
        {
            InitializeComponent();

        }

        private void btn_retour_Click(object sender, RoutedEventArgs e)
        {
            FenetrePrincipale? fenetre = Window.GetWindow(this) as FenetrePrincipale;
            this.Visibility = Visibility.Collapsed;

            if (fenetre != null)
            {
                fenetre.Connexion.Visibility = Visibility.Visible;
            }
        }

        private void Pwd_PasswordChanged(object s, RoutedEventArgs e)
        {
            if (DataContext is UtilisateurVM vm) vm.MotDePasse = ((PasswordBox)s).Password;
        }


        private void btn_inscription_Click(object sender, RoutedEventArgs e)
        {
            //PopupChoixDep popupChoixDep = new PopupChoixDep();
            //popupChoixDep.ShowDialog();
        }
    }
}
