using CommunityToolkit.Maui.Markup;
using Microsoft.Maui.Controls.Shapes;
using MyRecipeApp.Tools;

namespace MyRecipeApp.Components
{
    public class RecipeCard : Border
    {
        public static readonly BindableProperty RecipeNameProperty = BindableProperty.Create(
            propertyName: nameof(RecipeName),
            returnType: typeof(string),
            declaringType: typeof(RecipeCard)
        );
        public string RecipeName
        {
            get
            {
                return (string)GetValue(RecipeNameProperty);
            }
            set
            {
                SetValue(RecipeNameProperty, value);
            }
        }


        #region Calories BindableProperty
        public static readonly BindableProperty CaloriesProperty = BindableProperty.Create(
            propertyName: nameof(Calories),
            returnType: typeof(double),
            declaringType: typeof(RecipeCard)
        );
        public double Calories
        {
            get => (double)GetValue(CaloriesProperty);
            set => SetValue(CaloriesProperty, value);
        }
        #endregion


        #region Protein BindableProperty
        public static readonly BindableProperty ProteinProperty = BindableProperty.Create(
            propertyName: nameof(Protein),
            returnType: typeof(double),
            declaringType: typeof(RecipeCard)
        );
        public double Protein
        {
            get => (double)GetValue(ProteinProperty);
            set => SetValue(ProteinProperty, value);
        }
        #endregion


        #region Carbohydrate BindableProperty
        public static readonly BindableProperty CarbohydrateProperty = BindableProperty.Create(
            propertyName: nameof(Carbohydrate),
            returnType: typeof(double),
            declaringType: typeof(RecipeCard)
        );
        public double Carbohydrate
        {
            get => (double)GetValue(CarbohydrateProperty);
            set => SetValue(CarbohydrateProperty, value);
        }
        #endregion


        #region Fat BindableProperty
        public static readonly BindableProperty FatProperty = BindableProperty.Create(
            propertyName: nameof(Fat),
            returnType: typeof(double),
            declaringType: typeof(RecipeCard)
        );
        public double Fat
        {
            get => (double)GetValue(FatProperty);
            set => SetValue(FatProperty, value);
        }
        #endregion


        #region TimeToMake BindableProperty
        public static readonly BindableProperty TimeToMakeProperty = BindableProperty.Create(
            propertyName: nameof(TimeToMake),
            returnType: typeof(TimeSpan),
            declaringType: typeof(RecipeCard)
        );
        public TimeSpan TimeToMake
        {
            get => (TimeSpan)GetValue(TimeToMakeProperty);
            set => SetValue(TimeToMakeProperty, value);
        }
        #endregion


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
            //HeightRequest = 150;
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
                    .Bind(Label.TextProperty, nameof(RecipeName), BindingMode.OneWay, source: this),

                    new Label
                    {
                        TextColor = MyColors.TextPrimary,
                        FontSize = 15,
                    }.Row(1).Column(0).CenterVertical().Start()
                    .Bind(Label.TextProperty, nameof(Calories), BindingMode.OneWay, source: this,
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
                    .Bind(Label.TextProperty, nameof(Protein), BindingMode.OneWay, source: this,
                    convert: (double v) =>
                    {
                        return $"Total Protein: {v}";
                    }),

                    new Label
                    {
                        TextColor = MyColors.TextPrimary,
                        FontSize = 15,
                    }.Row(3).Column(0).CenterVertical().Start()
                    .Bind(Label.TextProperty, nameof(Carbohydrate), BindingMode.OneWay, source: this,
                    convert: (double v) =>
                    {
                        return $"Total Carbs: {v}";
                    }),

                    new Label
                    {
                        TextColor = MyColors.TextPrimary,
                        FontSize = 15,
                    }.Row(4).Column(0).CenterVertical().Start()
                    .Bind(Label.TextProperty, nameof(Fat), BindingMode.OneWay, source: this,
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
                    .Bind(Label.TextProperty, nameof(TimeToMake), BindingMode.OneWay, source: this,
                    convert: (TimeSpan v) =>
                    {
                        return $"Time To Make: {v}";
                    }),

                    new ImageButton
                    {
                        Command = new Command (()=> IsFavorite = !IsFavorite),
                    }.Row(0,6).Column(1).Size(25)
                    .End().Top()
                    .Bind(ImageButton.SourceProperty, nameof(IsFavorite), source: this,
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
