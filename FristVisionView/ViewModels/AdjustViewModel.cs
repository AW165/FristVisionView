using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FirstVisionView.Card;

namespace FirstVisionView.ViewModels
{
  public partial class AdjustViewModel :ObservableObject
    {
        [ObservableProperty]
        private ObservableCollection<ToolCard> _allcards = new();
        [ObservableProperty]
        private ObservableCollection<ToolCard> _selectedcards = new();
        [RelayCommand]
        private void AddCard(string? cardName = null)
        {
            ToolCard card = new();
            Allcards.Add(card);
        }

    }
}
