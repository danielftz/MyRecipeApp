using Microsoft.Maui.Controls.Shapes;
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
                    new RowDefinition {Height = GridLength.Auto},
                    new RowDefinition {Height = GridLength.Auto},
                },

            };
        }

    }
}
