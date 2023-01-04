using MyRecipeApp.Model;
using RecipeApp.Tools;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace MyRecipeApp
{

    public class MyIngredientsPageViewModel : BaseViewModel
    {
        public ICommand RemoveCommand { get; set; }
        public ICommand RefreshItemsCommand { get; set; }
        public ICommand SaveToRecipeCommand { get; set; }
        public ICommand SelectCommand { get; set; }

        private ObservableCollection<Ingredient> _storedIngredients = new();
        public ObservableCollection<Ingredient> StoredIngredients
        {
            get => _storedIngredients;
            set => SetValue(ref _storedIngredients, value);
        }

        private ObservableCollection<Ingredient> _selectedIngredients = new();
        public ObservableCollection<Ingredient> SelectedIngredients
        {
            get => _selectedIngredients;
            set => SetValue(ref _selectedIngredients, value);
        }


        public MyIngredientsPageViewModel(string destinationRecipe, ObservableCollection<Ingredient> destinationIngredients)
        {
            DatabaseService service = new();

            Task.Run(async () =>
            {
                List<Ingredient> list = await service.GetAllIngredientsAsync();
                StoredIngredients = new ObservableCollection<Ingredient>(list);

                List<Ingredient> selected = new();
                foreach (Ingredient ing in destinationIngredients)
                {
                    Ingredient? s = (StoredIngredients.Where(i=>i.Name == ing.Name).FirstOrDefault());
                    if (s is not null)
                    {
                        selected.Add(s);
                    }
                }

                SelectedIngredients = new ObservableCollection<Ingredient>(selected);
            });


            RemoveCommand = new Command<Ingredient>(async (ingredient) =>
            {
                bool confirmRemoval = await App.Current.MainPage.DisplayAlert("Confirm", "Do you want to remove this ingredient from the database?", "Yes", "No");
                if (confirmRemoval is true)
                {
                    SelectedIngredients.Remove(ingredient);
                    StoredIngredients.Remove(ingredient);
                    await service.RemoveIngredientAsync(ingredient.Name);
                }
            });


            RefreshItemsCommand = new Command<RefreshView>(async(refreshView) =>
            {
                List<Ingredient> list = await service.GetAllIngredientsAsync();
                for (int i = StoredIngredients.Count- 1; i >= 0; i--)
                {
                    if (list.Contains(StoredIngredients[i]) is not true)
                    {
                        StoredIngredients.RemoveAt(i);
                    }

                }

                foreach(Ingredient ing in list)
                {
                    if (StoredIngredients.Contains(ing) is not true)
                    {
                        StoredIngredients.Add(ing);
                    }
                }

                refreshView.IsRefreshing = false;
            });


            SelectCommand = new Command<Ingredient>((Ingredient ing) =>
            {
                if (SelectedIngredients.Contains(ing) is true)
                {
                    SelectedIngredients.Remove(ing);
                }
                else
                {
                    SelectedIngredients.Add(ing);
                }

            });

            SaveToRecipeCommand = new Command(() =>
            {
                //make sure everything not in selected ingredients is removed
                for (int i = destinationIngredients.Count - 1; i >= 0; i--)
                {
                    if (SelectedIngredients.Contains(destinationIngredients[i]) is not true)
                    {
                        destinationIngredients.RemoveAt(i);

                        ////remove reference of ingredient to 
                        //Ingredient? ingredient = (Ingredient?) SelectedIngredients.Where(ing => ((Ingredient)ing).Name == destinationIngredients[i].Name).FirstOrDefault();
                        //ingredient?.PartOfRecipe.Remove(destinationRecipe);
                    }
                }
                //make sure everything in selected ingredients is saved
                foreach (Ingredient ing in SelectedIngredients)
                {
                    if (destinationIngredients.Contains(ing) is not true)
                    {
                        destinationIngredients.Add(ing);
                    }
                    if (ing.PartOfRecipe.Contains(destinationRecipe) is not true)
                    {
                        ing.PartOfRecipe.Add(destinationRecipe);
                    }
                }
            });

        }

    }
}
