using MyRecipeApp.Model;
using RecipeApp.Tools;
using System.Windows.Input;

namespace MyRecipeApp
{
    public class CreateAnIngredientPageViewModel : BaseViewModel
    {
        private string _name = "";
        public string Name
        {
            get => _name;
            set => SetValue(ref _name, value);
        }

        private UnitType _unit;
        public UnitType Unit
        {
            get => _unit;
            set => SetValue(ref _unit, value);
        }

        private string _calories = "";
        public string Calories
        {
            get => _calories;
            set => SetValue(ref _calories, value);
        }

        private string _protein = "";
        public string Protein
        {
            get => _protein;
            set => SetValue(ref _protein, value);
        }

        private string _carbs = "";
        public string Carbs
        {
            get => _carbs;
            set => SetValue(ref _carbs, value);
        }

        private string _fat = "";
        public string Fat
        {
            get => _fat;
            set => SetValue(ref _fat, value);
        }

        public ICommand SaveCommand { get; private set; }
        public CreateAnIngredientPageViewModel(INavigation navigation, Ingredient? ingredient = null)
        {
            DatabaseService dbService = new();
            if (ingredient is not null)
            {
                Name = ingredient.Name;
                Unit = ingredient.Unit;
                Calories = ingredient.CaloriesPerUnit.ToString();
                Protein = ingredient.ProteinPerUnit.ToString();
                Carbs = ingredient.ProteinPerUnit.ToString();
                Fat = ingredient.FatPerUnit.ToString();
            }



            SaveCommand = new Command(async () =>
            {
                if (string.IsNullOrEmpty(Name))
                {
                    await App.Current!.MainPage!.DisplayAlert("Error", "You must enter a name", "Ok");
                    return;
                }

                bool result = await App.Current!.MainPage!.DisplayAlert("Confirm", "Do you want to save this ingredient?", "Ok", "Cancel");

                if (result is true &&
                    double.TryParse(Calories, out double calories) &&
                    double.TryParse(Protein, out double protein) &&
                    double.TryParse(Carbs, out double carbs) &&
                    double.TryParse(Fat, out double fat)
                )
                {
                    Ingredient ingredient = new Ingredient
                    {
                        Name = Name,
                        Unit = Unit,
                        CaloriesPerUnit = calories,
                        ProteinPerUnit = protein,
                        CarbsPerUnit = carbs,
                        FatPerUnit = fat
                    };

                    await dbService.AddIngredientAsync(ingredient);

                    await navigation.PopAsync();
                }
            });
        }

    }
}
