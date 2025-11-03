using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Game_On.Data.Context;
using Game_On.Models;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace Game_On.ViewModels
{
    public partial class GagnantVM : ObservableObject
    {
        private readonly ModelContext context;

        public GagnantVM()
        {
            this.context = new ModelContext();
           
        }

    }
}
