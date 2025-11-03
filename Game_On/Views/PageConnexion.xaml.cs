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
    /// Logique d'interaction pour PageConnexion.xaml
    /// </summary>
    public partial class PageConnexion : UserControl
    {
        public PageConnexion()
        {
            InitializeComponent();
        }

        private void btn_connexion_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void btn_inscription_Click(object sender, RoutedEventArgs e)
        {
            FenetrePrincipale? fenetre = Window.GetWindow(this) as FenetrePrincipale;
            this.Visibility = Visibility.Collapsed;

            if (fenetre != null)
            {
                fenetre.Inscription.Visibility = Visibility.Visible;
            }
        }
    }
}
