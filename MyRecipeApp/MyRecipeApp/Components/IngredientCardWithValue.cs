using CommunityToolkit.Maui.Markup;
using Microsoft.Maui.Controls.Shapes;
using MyRecipeApp.Model;
using MyRecipeApp.Tools;

namespace MyRecipeApp.Components
{
    public class IngredientCardWithValue : Border
    {
        public IngredientCardWithValue()
        {
            StrokeShape = new RoundRectangle
            {
                CornerRadius = 20,
            };
            Padding = 20;
            BackgroundColor = MyColors.Secondary;
            Shadow = new Shadow
            {
                Brush = Colors.Black,
                Radius = 15,
                Opacity = 0.20f,
                Offset = new Point(5, 5),
            };
            Content = new Grid
            {
                RowDefinitions =
                {
                    new RowDefinition { Height = GridLength.Star },
                    new RowDefinition { Height = GridLength.Star },
                },

                ColumnDefinitions =
                {
                    new ColumnDefinition { Width = GridLength.Star },
                    new ColumnDefinition { Width = GridLength.Auto },
                },

                Children =
                {
                    new Label
                    {
                    }.Row(0).Column(0,2)
                    .Bind(Label.TextProperty, static(Ingredient ing) => ing.Name),

                    new Entry
                    {
                    }.Row(0).Column(0, 1),

                    new Label
                    {
                    }
                    .Bind(Label.TextProperty, static(Ingredient ing)=> ing.Unit, 
                    convert: (UnitType u) =>
                    {
                        switch (u) 
                        {
                            case (UnitType.Single):
                                return "Item(s)";
                            case (UnitType.OneGram):
                                return "g";
                            case (UnitType.OneHundredGrams):
                                return "g";
                            case (UnitType.OneMillileter):
                                return "mL";
                            case (UnitType.OneThousandMilliLeters):
                                return "mL";
                            default:
                                throw new InvalidCastException();
                        }
                    })
                }
            };
        }

    }
}
