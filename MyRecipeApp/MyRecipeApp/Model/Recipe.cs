using MyRecipeApp.Tools;
using Newtonsoft.Json;
using RecipeApp.Tools;
using SQLite;

namespace MyRecipeApp.Model
{
    public class Recipe

    {
        public string Name { get; set; } = string.Empty;

        public string Instruction { get; set; } = string.Empty;

        public TimeSpan TimeToMake { get; set; }

        public bool IsFavorite { get; set; }

        /// <summary>
        /// Use the name of the ingredient as keys, and store the amounts
        /// </summary>
        public Dictionary<string, double> IngredientAmount { get; set; } = new();

        /// <summary>
        /// Initially stored and retrieved from the database
        /// </summary>
        public double TotalCalories { get; private set; }

        public double TotalProtein { get; private set; }

        public double TotalFat { get; private set; }

        public double TotalCarbs { get; private set; }

        public Recipe()
        {

        }

        public Recipe(IList<Ingredient> ingredients, IList<double> ingredientAmount)
        {
            if (ingredients.Count != ingredientAmount.Count)
            {
                throw new ArgumentException("the ingredients and their amount do not match");
            }

            for (int i = 0; i< ingredients.Count; i++)
            {
                IngredientAmount[ingredients[i].Name] = ingredientAmount[i];
            }
        }

        public Recipe(RecipeDatabaseRow data)
        {
            Name = data.Name; 
            Instruction = data.Instruction;
            TimeToMake = data.TimeToMake;
            IsFavorite = data.IsFavorite;
            IngredientAmount = JsonConvert.DeserializeObject<Dictionary<string, double>>(data.IngredientAmountJson) ?? throw new InvalidCastException("json deserialzation error");
            TotalCalories = data.TotalCalories;
            TotalProtein = data.TotalProtein;
            TotalFat = data.TotalFat;
            TotalCarbs = data.TotalCarbs;
        }

        public async Task UpdateNutritionValue(DatabaseService dbService)
        {
            double totalCalories = 0;
            double totalProtein = 0;
            double totalFat = 0;
            double totalCarbs = 0;


            foreach (string name in IngredientAmount.Keys)
            {
                Ingredient? ing = await dbService.GetIngredientByNameAsync(name);
                if (ing is not null)
                {
                    int divisor = Ingredient.UnitValueLookUp[ing.Unit];
                    double amount = IngredientAmount[name];
                    totalCalories += ing.CaloriesPerUnit * amount / divisor;
                    totalProtein += ing.ProteinPerUnit * amount / divisor;
                    totalCarbs += ing.CarbsPerUnit * amount / divisor;
                    totalFat = ing.FatPerUnit * amount / divisor;
                }
                else
                {
                    IngredientAmount.Remove(name);
                }
            }


            TotalCalories = totalCalories;
            TotalProtein = totalProtein;
            TotalFat = totalFat;
            TotalCarbs = totalCarbs;
        }
    }

    public class RecipeDatabaseRow
    {
        [PrimaryKey, Column("_id")]
        public string Name { get; set; }

        public string Instruction { get; set; }

        public TimeSpan TimeToMake { get; set; }

        public bool IsFavorite { get; set; }        

        public double TotalCalories { get; set; }

        public double TotalProtein { get; set; }

        public double TotalFat { get; set; }

        public double TotalCarbs { get; set; }

        public string IngredientAmountJson { get; set; }

        public RecipeDatabaseRow()
        {

        }
        public RecipeDatabaseRow(Recipe recipe)
        {
            Name = recipe.Name;
            Instruction = recipe.Instruction;
            TimeToMake= recipe.TimeToMake;
            IsFavorite = recipe.IsFavorite;
            TotalCalories = recipe.TotalCalories;
            TotalProtein = recipe.TotalProtein;
            TotalFat = recipe.TotalFat;
            TotalCarbs= recipe.TotalCarbs;
            IngredientAmountJson = JsonConvert.SerializeObject(recipe.IngredientAmount);
        }
    }
}
