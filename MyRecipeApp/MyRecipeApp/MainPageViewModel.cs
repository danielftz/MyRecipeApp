using MyRecipeApp.Model;
using System.Collections.ObjectModel;

namespace MyRecipeApp
{
    public class MainPageViewModel : BaseViewModel
    {
        private Recipe _randomRecipe;
        public Recipe RandomRecipe
        {
            get => _randomRecipe;
            set => SetValue(ref _randomRecipe, value);
        }

        private ObservableCollection<Recipe> _tempRecipes;
        public ObservableCollection<Recipe> TempRecipes
        {
            get => _tempRecipes;
            set => SetValue(ref _tempRecipes, value);
        }


        public MainPageViewModel()
        {
            RandomRecipe = new Recipe();
            //Retrieve all recipe from the databse

            //retrieve a random recipe from the datbase
        }
    }
}
