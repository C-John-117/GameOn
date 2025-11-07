using System.ComponentModel;
using Game_On.Models;

namespace Game_On.ViewModels
{
    public class SudokuVM : INotifyPropertyChanged
    {
        private readonly CoupScanner _scanner;

        public event PropertyChangedEventHandler PropertyChanged;

        public SudokuVM(string initialGrid)
        {
            _scanner = new CoupScanner(initialGrid);
        }

        /// <summary>
        /// Tente de jouer un chiffre sur (row,col). Retourne true si le coup est valide.
        /// </summary>
        public bool TryPlay(int row, int col, int digit)
        {
            if (digit <= 0 || digit > 9) return false;
            return _scanner.isCoupValide(row, col, digit);
        }

        // (Optionnel) selon vos besoins, exposez aussi l’état complet,
        // des erreurs, etc. et appelez OnPropertyChanged si nécessaire.
        protected void OnPropertyChanged(string name)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
