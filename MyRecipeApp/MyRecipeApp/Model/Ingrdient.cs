using Newtonsoft.Json;
using SQLite;
using System.Collections.ObjectModel;

namespace MyRecipeApp.Model
{
    public class Ingredient
    {

        public string Name { get; set; } = string.Empty;

        public UnitType Unit { get; set; }

        public double CaloriesPerUnit { get; set; }

        public double ProteinPerUnit { get; set; }

        public double FatPerUnit { get; set; }

        public double CarbsPerUnit { get; set; }

        public List<string> PartOfRecipe { get; set; } = new ();

        public Ingredient()
        {

        }

        public Ingredient(IngredientDatabaseRow data)
        {
            Name = data.Name;
            Unit = data.Unit;
            CaloriesPerUnit= data.CaloriesPerUnit;
            ProteinPerUnit= data.ProteinPerUnit;
            FatPerUnit= data.FatPerUnit;
            CarbsPerUnit= data.CarbsPerUnit;
            PartOfRecipe = JsonConvert.DeserializeObject<List<string>>(data.PartOfRecipeJson) ?? throw new InvalidCastException("json deserialization error");
        }

        public static readonly ReadOnlyCollection<string> UnitNames = new ReadOnlyCollection<string>(new List<string>
        {
            "Single","1g", "100g", "1mL", "1000mL"
        });

        public static readonly ReadOnlyDictionary<UnitType, int> UnitValueLookUp = new(new Dictionary<UnitType, int> {
            { UnitType.Single, 1 },
            { UnitType.OneGram, 1 },
            { UnitType.OneHundredGrams, 100 },
            { UnitType.OneMillileter, 1 },
            { UnitType.OneThousandMilliLeters, 1000 }
        });
    }

    public enum UnitType
    {
        Single = 0,

        OneGram = 1,
        
        OneHundredGrams = 2,

        OneMillileter = 3,

        OneThousandMilliLeters = 4,
    }

    public class IngredientDatabaseRow
    {
        [PrimaryKey, Column("_id")]
        public string Name { get; set; }

        public UnitType Unit { get; set; }

        public double CaloriesPerUnit { get; set; }

        public double ProteinPerUnit { get; set; }

        public double FatPerUnit { get; set; }

        public double CarbsPerUnit { get; set; }

        public string PartOfRecipeJson { get; set; }

        public IngredientDatabaseRow()
        {

        }

        public IngredientDatabaseRow(Ingredient ingredient)
        {
            Name = ingredient.Name;
            Unit = ingredient.Unit;
            CaloriesPerUnit = ingredient.CaloriesPerUnit;
            ProteinPerUnit = ingredient.ProteinPerUnit;
            FatPerUnit = ingredient.FatPerUnit;
            CarbsPerUnit = ingredient.CarbsPerUnit;
            PartOfRecipeJson = JsonConvert.SerializeObject(ingredient.PartOfRecipe);
        }

    }

}

