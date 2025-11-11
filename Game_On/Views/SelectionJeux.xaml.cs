using Game_On.Models;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Game_On.Views
{
    /// <summary>
    /// Logique d'interaction pour SelectionJeux.xaml
    /// </summary>
    public partial class SelectionJeux : UserControl
    {


        public SelectionJeux()
        {
            InitializeComponent();
            ChargerJeux();
        }

        private void ChargerJeux()
        {
            List<Jeu> jeux = new List<Jeu>
            {
                new Jeu { NomJeu = "Poker Pro" },
                new Jeu { NomJeu = "Sudoku Master" },
                new Jeu { NomJeu = "Quiz Mania" }
            };

            foreach (Jeu jeu in jeux)
                PanelJeux.Children.Add(CreerCarteJeu(jeu));
        }

        private Border CreerCarteJeu(Jeu jeu)
        {
            Border carte = new Border
            {
                Margin = new Thickness(10),
                Background = new SolidColorBrush(Color.FromRgb(40, 40, 40)),
                CornerRadius = new CornerRadius(15),
                Padding = new Thickness(15),
                Effect = new System.Windows.Media.Effects.DropShadowEffect
                {
                    Color = Colors.Black,
                    BlurRadius = 10,
                    ShadowDepth = 2
                }
            };

            var contenu = new StackPanel
            {
                Orientation = Orientation.Horizontal,
                Margin = new Thickness(10)
            };

            // Image
            Image image = new Image
            {
                Width = 80,
                Height = 80,
                Margin = new Thickness(0, 0, 20, 0)
            };

            try
            {
                //En attente de la suite pour les images
                //image.Source = new BitmapImage(new System.Uri(jeu.ImagePath, System.UriKind.RelativeOrAbsolute));
            }
            catch
            {
                // Image par défaut
            }

            // Infos du jeu
            StackPanel infoPanel = new StackPanel();

            TextBlock titre = new TextBlock
            {
                Text = jeu.NomJeu,
                FontSize = 20,
                Foreground = Brushes.White,
                Margin = new Thickness(0, 0, 0, 5)
            };

            infoPanel.Children.Add(titre);

            // Boutons
            StackPanel boutonsPanel = new StackPanel();
            boutonsPanel.Orientation = Orientation.Horizontal;
            boutonsPanel.Margin = new Thickness(30, 0, 0, 0);

            Button btnClassement = CreerBouton("Classement", Brushes.SteelBlue);
            Button btnEntrainement = CreerBouton("Entraînement", Brushes.MediumSeaGreen);
            Button btnJouer = CreerBouton("Jouer", Brushes.OrangeRed);

            btnJouer.Click += (s, e) => AfficherNiveaux(boutonsPanel, btnJouer);

            boutonsPanel.Children.Add(btnClassement);
            boutonsPanel.Children.Add(btnEntrainement);
            boutonsPanel.Children.Add(btnJouer);

            contenu.Children.Add(image);
            contenu.Children.Add(infoPanel);
            contenu.Children.Add(boutonsPanel);

            carte.Child = contenu;
            return carte;
        }

        private Button CreerBouton(string texte, Brush couleur)
        {
            Button bouton = new Button();
            bouton.Content = texte;
            bouton.Background = couleur;
            bouton.Foreground = Brushes.White;
            bouton.Margin = new Thickness(5, 0, 0, 0);
            bouton.Padding = new Thickness(10, 5, 10, 5);
            bouton.BorderThickness = new Thickness(0);
            bouton.Width = 95;
            return bouton;
        }

        private void AfficherNiveaux(StackPanel parent, Button boutonJouer)
        {
            parent.Children.Remove(boutonJouer);

            StackPanel niveauxPanel = new StackPanel();
            niveauxPanel.Orientation = Orientation.Horizontal;

            Button facile = CreerBouton("Facile", Brushes.LightGreen);
            facile.Click += (s, e) => AfficherSudoku("Facile");

            Button moyen = CreerBouton("Moyen", Brushes.Goldenrod);
            moyen.Click += (s, e) => AfficherSudoku("Moyen");

            Button difficile = CreerBouton("Difficile", Brushes.IndianRed);
            difficile.Click += (s, e) => AfficherSudoku("Difficile");

            niveauxPanel.Children.Add(facile);
            niveauxPanel.Children.Add(moyen);
            niveauxPanel.Children.Add(difficile);

            parent.Children.Add(niveauxPanel);
        }

        private void AfficherSudoku(string Difficulty)
        {
            FenetrePrincipale? fenetre = Window.GetWindow(this) as FenetrePrincipale;

            this.Visibility = Visibility.Collapsed;

            fenetre.Jeu.ChooseDifficulty(Difficulty);
            fenetre.Jeu.Visibility = Visibility.Visible;

        }
    }
}

