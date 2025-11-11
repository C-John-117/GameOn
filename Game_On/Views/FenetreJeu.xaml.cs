using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Game_On.ViewModels;

namespace Game_On.Views
{
    public partial class FenetreJeu : UserControl
    {
        private string memory;
        private bool noteMode = false;
        private string puzzle;
        private TextBox[,] textBoxes = new TextBox[9, 9];

        public FenetreJeu()
        {
            InitializeComponent();

            // Grille de départ (0 = vide)
            puzzle = "007300900030014070000020000078160059005800047002700160750093000280000000001400000";
            memory = puzzle;

            // Instancier le VM avec la grille initiale si non présent
            if (DataContext is not SudokuVM)
                DataContext = new SudokuVM(puzzle);

            CreateSudokuGrid(puzzle);
        }

        //A implementer lorsque la BD sera connecter
        public void ChooseDifficulty(string difficulte)
        {
            //TODO
        }

        private void CreateSudokuGrid(string puzzle)
        {
            SudokuGrid.Children.Clear();
            SudokuGrid.RowDefinitions.Clear();
            SudokuGrid.ColumnDefinitions.Clear();

            for (int i = 0; i < 9; i++)
            {
                SudokuGrid.RowDefinitions.Add(new RowDefinition());
                SudokuGrid.ColumnDefinitions.Add(new ColumnDefinition());
            }

            for (int row = 0; row < 9; row++)
            {
                for (int col = 0; col < 9; col++)
                {
                    int index = row * 9 + col;
                    char c = puzzle[index];

                    var tb = new TextBox
                    {
                        Text = c == '0' ? "" : c.ToString(),
                        FontSize = 24,
                        FontWeight = FontWeights.Bold,
                        HorizontalContentAlignment = HorizontalAlignment.Center,
                        VerticalContentAlignment = VerticalAlignment.Center,
                        BorderBrush = Brushes.Gray,
                        Background = c == '0' ? Brushes.White : new SolidColorBrush(Color.FromRgb(235, 235, 235)),
                        IsReadOnly = c != '0',
                        MaxLength = 1,
                        Width = 50,
                        Height = 50,
                        Margin = new Thickness(2)
                    };

                    // Empêcher autre chose que 1–9 à la saisie clavier
                    tb.PreviewTextInput += (s, e) =>
                    {
                        e.Handled = !char.IsDigit(e.Text[0]) || e.Text == "0";
                    };

                    // Gérer les changements (y compris coller/supprimer)
                    tb.TextChanged += TextBox_TextChanged;

                    // Bordures épaisses pour les blocs 3x3
                    Thickness thick = new Thickness(0.5);
                    if (col % 3 == 0) thick.Left = 3;
                    if (row % 3 == 0) thick.Top = 3;
                    if (col == 8) thick.Right = 3;
                    if (row == 8) thick.Bottom = 3;
                    tb.BorderThickness = thick;

                    Grid.SetRow(tb, row);
                    Grid.SetColumn(tb, col);
                    SudokuGrid.Children.Add(tb);
                    textBoxes[row, col] = tb;
                }
            }
        }

        /// <summary>
        /// Handler NOMMÉ pour éviter CS0079 et pouvoir (dé)abonner proprement.
        /// </summary>
        private void TextBox_TextChanged(object? sender, TextChangedEventArgs e)
        {
            if (sender is not TextBox tb || tb.IsReadOnly) return;

            // Empêche la boucle lors des modifications programmatiques de tb.Text
            tb.TextChanged -= TextBox_TextChanged;

            // Normalise la saisie : ne garder que le 1er char, ou vide
            if (tb.Text.Length > 1)
                tb.Text = tb.Text[0].ToString();

            int row = Grid.GetRow(tb);
            int col = Grid.GetColumn(tb);
            char newChar = tb.Text.Length > 0 ? tb.Text[0] : '0';

            bool ok = UpdatePuzzle(row, col, newChar);

            if (!ok)
            {
                // Annuler la saisie et feedback visuel doux
                tb.Text = "";
                tb.Background = new SolidColorBrush(Color.FromRgb(255, 220, 220)); // rouge pâle
            }
            else
            {
                // Remettre un fond normal selon le mode
                tb.Background = noteMode
                    ? new SolidColorBrush(Color.FromRgb(230, 245, 255))
                    : Brushes.White;
            }

            tb.TextChanged += TextBox_TextChanged;
        }

        /// <summary>
        /// Met à jour la string puzzle ET interroge le ViewModel/CoupScanner.
        /// Retourne true si accepté, false si invalide.
        /// </summary>
        private bool UpdatePuzzle(int row, int col, char value)
        {
            int digit = (value >= '1' && value <= '9') ? (value - '0') : 0;

            // Valider via le VM uniquement si un chiffre 1..9 est saisi
            if (digit != 0 && DataContext is SudokuVM vm)
            {
                bool valid = vm.TryPlay(row, col, digit);
                if (!valid)
                {
                    // Coup refusé -> ne pas modifier puzzle
                    return false;
                }
            }

            // MAJ du string puzzle (on accepte aussi vider la case => '0')
            int index = row * 9 + col;
            char[] chars = puzzle.ToCharArray();
            chars[index] = (digit == 0) ? '0' : value;
            puzzle = new string(chars);

            Console.WriteLine($"Puzzle mis à jour : {puzzle}");
            return true;
        }

        // ✏️ Mode Note
        private void BtnNote_Click(object sender, RoutedEventArgs e)
        {
            noteMode = !noteMode;
            BtnNote.Content = noteMode ? "📝 Mode Normal" : "✏️ Mode Note";
            BtnNote.Background = noteMode
                ? new SolidColorBrush(Color.FromRgb(180, 220, 255))
                : new SolidColorBrush(Color.FromRgb(255, 238, 170));

            foreach (var child in SudokuGrid.Children)
            {
                if (child is TextBox tb && !tb.IsReadOnly)
                {
                    tb.Background = noteMode
                        ? new SolidColorBrush(Color.FromRgb(230, 245, 255))
                        : Brushes.White;
                }
            }
        }

        private void btn_Reset_Click(object sender, RoutedEventArgs e)
        {
            CreateSudokuGrid(memory);
        }
    }
}
