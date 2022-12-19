using CommunityToolkit.Maui.Markup;
using Microsoft.Maui.Controls.Shapes;
using MyRecipeApp.Model;
using MyRecipeApp.Tools;

namespace MyRecipeApp.Components
{
    public class RecipeCard : Border
    {
        #region IsFavorite BindableProperty
        public static readonly BindableProperty IsFavoriteProperty = BindableProperty.Create(
            propertyName: nameof(IsFavorite),
            returnType: typeof(bool),
            declaringType: typeof(RecipeCard)
        );
        public bool IsFavorite
        {
            get => (bool)GetValue(IsFavoriteProperty);
            set => SetValue(IsFavoriteProperty, value);
        }
        #endregion

        public RecipeCard()
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
                Offset = new Point(5,5),
            };

            this.Bind(IsFavoriteProperty, static(Recipe r)=>r.IsFavorite, static()=>, BindingMode.TwoWay)

            Content = new Grid
            {
                RowDefinitions = new RowDefinitionCollection
                {
                    new RowDefinition(new GridLength(1.5, GridUnitType.Star)),
                    new RowDefinition(new GridLength(1, GridUnitType.Star)),
                    new RowDefinition(new GridLength(1, GridUnitType.Star)),
                    new RowDefinition(new GridLength(1, GridUnitType.Star)),
                    new RowDefinition(new GridLength(1, GridUnitType.Star)),
                    new RowDefinition(new GridLength(1, GridUnitType.Star)),
                },

                ColumnDefinitions =
                {
                    new ColumnDefinition(new GridLength(5, GridUnitType.Star)),
                    new ColumnDefinition(GridLength.Star),
                },
                RowSpacing = 5,
                Children =
                {
                    new Label
                    {
                        //Text = "Chicken Fried Rice",
                        TextColor = MyColors.TextPrimary,
                        FontAttributes = FontAttributes.Bold,
                        FontSize = 17,
                    }.Row(0).Column(0).CenterVertical().Start()
                    .Bind(Label.TextProperty, static (Recipe r)=>r.Name),

                    new Label
                    {
                        TextColor = MyColors.TextPrimary,
                        FontSize = 15,
                    }.Row(1).Column(0).CenterVertical().Start()
                    .Bind(Label.TextProperty, static (Recipe r)=>r.TotalCalories,
                    convert: (double v) =>
                    {
                        return $"Total Calories: {v}";
                    }),

                    new Label
                    {
                        TextColor = MyColors.TextPrimary,
                        FontSize = 15,
                        BindingContext = this,
                    }.Row(2).Column(0).CenterVertical().Start()
                    .Bind(Label.TextProperty, static (Recipe r)=>r.TotalProtein,
                    convert: (double v) =>
                    {
                        return $"Total Protein: {v}";
                    }),

                    new Label
                    {
                        TextColor = MyColors.TextPrimary,
                        FontSize = 15,
                    }.Row(3).Column(0).CenterVertical().Start()
                    .Bind(Label.TextProperty, static(Recipe r) => r.TotalCarbs,
                    convert: (double v) =>
                    {
                        return $"Total Carbs: {v}";
                    }),

                    new Label
                    {
                        TextColor = MyColors.TextPrimary,
                        FontSize = 15,
                    }.Row(4).Column(0).CenterVertical().Start()
                    .Bind(Label.TextProperty, static(Recipe r) => r.TotalFat,
                    convert: (double v) =>
                    {
                        return $"Total Fat: {v}";
                    }),

                    new Label
                    {
                        Text = "Time To Make: 35 mins",
                        TextColor = MyColors.TextPrimary,
                        FontSize = 15,
                    }.Row(5).Column(0).CenterVertical().Start()
                    .Bind(Label.TextProperty, static (Recipe r)=>r.TimeToMake,
                    convert: (TimeSpan v) =>
                    {
                        return $"Time To Make: {v}";
                    }),

                    new ImageButton
                    {
                        BindingContext = this,
                        Command = new Command (()=> IsFavorite = !IsFavorite),
                    }.Row(0,6).Column(1).Size(25)
                    .End().Top()
                    .Bind(ImageButton.SourceProperty, static (Recipe r)=>r.IsFavorite,
                    convert: (bool isFavorite) =>
                    {
                        if (isFavorite)
                        {
                            return ImageSource.FromFile("icon_star_filled");
                        }
                        return ImageSource.FromFile("icon_star_outline");
                    })
                }
            };
          
        }
    }
}
