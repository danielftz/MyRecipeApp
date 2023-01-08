using MyRecipeApp.Model;
using SQLite;

namespace RecipeApp.Tools
{
    public class DatabaseService
    {
        private static SQLiteAsyncConnection _dbConnection;

        private static readonly string _dbPath = Path.Combine(FileSystem.AppDataDirectory, "RecipeAppDatabase.db");

        public static async Task OpenConnectionAsync()
        {
            if (_dbConnection is null)
            {
                _dbConnection = new SQLiteAsyncConnection(_dbPath);
                await _dbConnection.CreateTableAsync<RecipeDatabaseRow>();
                await _dbConnection.CreateTableAsync<IngredientDatabaseRow>();
            }
        }

        public static async Task CloseConnectionAsync()
        {
            if (_dbConnection is not null)
            {
                await _dbConnection.CloseAsync();
            }
        }

        public DatabaseService()
        {
            if (_dbConnection is null)
            {
                _dbConnection = new SQLiteAsyncConnection(_dbPath);
                Task.Run(async () =>
                {
                    await _dbConnection.CreateTableAsync<RecipeDatabaseRow>();
                    await _dbConnection.CreateTableAsync<IngredientDatabaseRow>();
                });

            }
        }

        //CRUD for Ingredients
        public async Task<int> AddIngredientAsync(Ingredient ingredient)
        {
            return await _dbConnection.InsertAsync(new IngredientDatabaseRow(ingredient));
        }


        public async Task<List<Ingredient>> GetAllIngredientsAsync()
        {
            List<IngredientDatabaseRow> data = await _dbConnection.Table<IngredientDatabaseRow>().ToListAsync();
            List<Ingredient> ing = new();
            foreach (IngredientDatabaseRow d in data)
            {
                ing.Add(new Ingredient(d));
            }
            return ing;
        }

        public async Task<Ingredient> GetIngredientByNameAsync(string name)
        {
            IngredientDatabaseRow data = await _dbConnection.FindAsync<IngredientDatabaseRow>(name);
            if (data is not null)
            {
                return new Ingredient(data);
            }
            return null;
        }

        public async Task<int> UpdateOrInsertIngredientAsync(Ingredient ingredient)
        {
            IngredientDatabaseRow data = await _dbConnection.FindAsync<IngredientDatabaseRow>(ingredient.Name);
            if (data is null)
            {
                return await AddIngredientAsync(ingredient);
            }

            return await _dbConnection.UpdateAsync(new IngredientDatabaseRow(ingredient));
        }

        public async Task<int> RemoveIngredientAsync(string name)
        {
            return await _dbConnection.DeleteAsync<IngredientDatabaseRow>(name);
        }



        //CRUD for recipes


        public async Task<int> AddRecipeAsync(Recipe recipe)
        {
            return await _dbConnection.InsertAsync(new RecipeDatabaseRow(recipe));
        }


        public async Task<List<Recipe>> GetAllRecipesAsync()
        {
            List<RecipeDatabaseRow> data = await _dbConnection.Table<RecipeDatabaseRow>().ToListAsync();
            List<Recipe> recipes = new();
            foreach (RecipeDatabaseRow d in data)
            {
                recipes.Add(new Recipe(d));
            }
            return recipes;
        }

        public async Task<Recipe?> GetRecipeByNameAsync(string name)
        {
            RecipeDatabaseRow data = await _dbConnection.FindAsync<RecipeDatabaseRow>(name);
            if (data is not null)
            {
                return new Recipe(data);
            }
            return null;
        }

        public async Task<int> UpdateOrInsertRecipeAsync(Recipe recipe)
        {
            RecipeDatabaseRow data = await _dbConnection.FindAsync<RecipeDatabaseRow>(recipe.Name);
            if (data is null)
            {
                return await AddRecipeAsync(recipe);
            }

            return await _dbConnection.UpdateAsync(new RecipeDatabaseRow(recipe));
        }

        public async Task<int> RemoveRecipeAsync(string name)
        {
            return await _dbConnection.DeleteAsync<RecipeDatabaseRow>(name);
        }
        
    }
}
