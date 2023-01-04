using MyRecipeApp.Model;
using RecipeApp.Tools;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Windows.Input;

namespace MyRecipeApp
{
    public class CreateARecipePageViewModel : BaseViewModel
    {
        private string _name = string.Empty;
        public string Name
        {
            get => _name;
            set => SetValue(ref _name, value);
        }

        private ObservableCollection<Ingredient> _ingredients = new();
        public ObservableCollection<Ingredient> Ingredients
        {
            get => _ingredients;
            set => SetValue(ref _ingredients, value);
        }

        private ObservableCollection<double> _ingredientAmount = new();
        public ObservableCollection<double> IngredientAmount
        {
            get => _ingredientAmount;
            set => SetValue(ref _ingredientAmount, value);
        }


        private string _instruction = string.Empty;
        public string Instruction
        {
            get => _instruction;
            set => SetValue(ref _instruction, value);
        }

        public ICommand SaveRecipeCommand { get; set; }

        public ICommand FindMoreIngredientsCommand { get; set; }


        public CreateARecipePageViewModel(INavigation navigation, Recipe? recipe = null)
        {
            Ingredients.CollectionChanged += (s, e) =>
            {
                switch (e.Action)
                {
                    case (NotifyCollectionChangedAction.Add):
                        IngredientAmount.Insert(e.NewStartingIndex, 0);
                        return;
                    case NotifyCollectionChangedAction.Remove:
                        IngredientAmount.RemoveAt(e.OldStartingIndex);
                        return;
                    case NotifyCollectionChangedAction.Replace:
                        IngredientAmount[e.OldStartingIndex] = 0;
                        return;
                    case NotifyCollectionChangedAction.Move:
                        IngredientAmount.Move(e.OldStartingIndex, e.NewStartingIndex);
                        return;
                    case NotifyCollectionChangedAction.Reset:
                        IngredientAmount.Clear();
                        return;
                }
            };


            DatabaseService dbService= new();

            if (recipe is not null)
            {
                Name = recipe.Name;
                Instruction = recipe.Instruction;
                Task.Run(async () =>
                {
                    foreach (string name in recipe.IngredientAmount.Keys)
                    {
                        Ingredient? ing = await dbService.GetIngredientByNameAsync(name);
                        if (ing is not null)
                        {
                            Ingredients.Add(ing);
                            IngredientAmount.Add(recipe.IngredientAmount[name]);
                        }
                    }
                });
            }

            FindMoreIngredientsCommand = new Command(async () =>
            {
                if (string.IsNullOrEmpty(Name))
                {
                    await App.Current!.MainPage!.DisplayAlert("Error", "You must enter a name", "Ok");
                    return;
                }

                Recipe? r = await dbService.GetRecipeByNameAsync(Name);

                if (r is not null)
                {
                    await App.Current!.MainPage!.DisplayAlert("Error", "This recipe name already exists. Please try another.", "Ok");
                    return;
                }

                await navigation.PushAsync(new MyIngredientsPage(Name, Ingredients));
            });

            SaveRecipeCommand = new Command(async () =>
            {
                if (string.IsNullOrEmpty(Name))
                {
                    await App.Current!.MainPage!.DisplayAlert("Error", "You must enter a name", "Ok");
                    return;
                }
                //check if name already exists in database

                Recipe? r = await dbService.GetRecipeByNameAsync(Name);

                if (r is not null)
                {
                    await App.Current!.MainPage!.DisplayAlert("Error", "This recipe name already exists. Please try another.", "Ok");
                    return;
                }

                bool result = await App.Current!.MainPage!.DisplayAlert("Confirm", "Do you want to save this Recipe?", "Ok", "Cancel");

                if (result is true)
                {
                    Recipe recipe = new Recipe(Ingredients, IngredientAmount)
                    {
                        Name = Name,
                  
                        Instruction = Instruction,
                        TimeToMake = TimeSpan.FromMinutes(30),
                        IsFavorite = true,
                    };

                    await dbService.AddRecipeAsync(recipe);

                    await navigation.PopAsync();
                }
            });
        }
    }
}
