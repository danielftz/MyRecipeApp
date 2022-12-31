using CommunityToolkit.Maui.Markup;
using Microsoft.Maui.Controls.Shapes;
using MyRecipeApp.Model;
using MyRecipeApp.Tools;

namespace MyRecipeApp.Components
{
    public class IngredientCardWithValue : Border
    {

        #region Index BindableProperty
        public static readonly BindableProperty IndexProperty = BindableProperty.Create(
            propertyName: nameof(Index),
            returnType: typeof(int),
            declaringType: typeof(IngredientCardWithValue),
            defaultValue: 0
        );
        public int Index
        {
            get => (int)GetValue(IndexProperty);
            set => SetValue(IndexProperty, value);
        }
        #endregion

        #region Amount BindableProperty
        public static readonly BindableProperty AmountProperty = BindableProperty.Create(
            propertyName: nameof(Amount),
            returnType: typeof(double),
            declaringType: typeof(IngredientCardWithValue)
        );
        public double Amount
        {
            get => (double)GetValue(AmountProperty);
            set => SetValue(AmountProperty, value);
        }
        #endregion

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
                    new RowDefinition { Height = GridLength.Auto },
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
                        Keyboard = Keyboard.Numeric,
                        BindingContext = this
                    }.Row(1).Column(0)
                    .Bind(Entry.TextProperty, nameof(Amount), BindingMode.TwoWay,
                    convert: (double d) =>
                    {
                        return d.ToString();
                    },
                    convertBack: (string s) =>
                    {
                        if (double.TryParse(s, out var d))
                        {
                            return d;
                        }
                        return 0;
                    }),

                    new Label
                    {
                    }.Row(1).Column(2)
                    .Bind(Label.TextProperty, static(Ingredient ing)=> ing.Unit, 
                    convert: (UnitType u) =>
                    {
                        switch (u) 
                        {
                            case (UnitType.Single):
                                return "item(s)";
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
