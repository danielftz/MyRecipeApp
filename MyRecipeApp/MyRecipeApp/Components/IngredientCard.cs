using CommunityToolkit.Maui.Markup;
using Microsoft.Maui.Controls.Shapes;
using MyRecipeApp.Model;
using MyRecipeApp.Tools;
using System.Windows.Input;

namespace MyRecipeApp.Components
{
    public class IngredientCard : SwipeView
	{
        #region RemoveCommand BindableProperty
        public static readonly BindableProperty RemoveCommandProperty = BindableProperty.Create(
            propertyName: nameof(RemoveCommand),
            returnType: typeof(ICommand),
            declaringType: typeof(IngredientCard)
        );
        public ICommand RemoveCommand
        {
            get => (ICommand)GetValue(RemoveCommandProperty);
            set => SetValue(RemoveCommandProperty, value);
        }
        #endregion


        #region RemoveCommandParameter BindableProperty
        public static readonly BindableProperty RemoveCommandParameterProperty = BindableProperty.Create(
            propertyName: nameof(RemoveCommandParameter),
            returnType: typeof(object),
            declaringType: typeof(IngredientCard)
        );
        public object RemoveCommandParameter
        {
            get => (object)GetValue(RemoveCommandParameterProperty);
            set => SetValue(RemoveCommandParameterProperty, value);
        }
        #endregion

        
        public IngredientCard()
		{
			BackgroundColor = Colors.Transparent;
			List<SwipeItem> swipeItems = new List<SwipeItem>()
			{
				new SwipeItem()
				{
					Text = "Remove",
					BackgroundColor = Colors.Orange,
					IsVisible = true,
                    BindingContext = this
				}.Bind(SwipeItem.CommandProperty, static (IngredientCard card)=>card.RemoveCommand) //nameof(RemoveCommand), source: this)
				.Bind(SwipeItem.CommandParameterProperty, static (IngredientCard card)=>card.RemoveCommandParameter )
			};

			RightItems = new SwipeItems(swipeItems)
			{
				Mode = SwipeMode.Reveal,
			};

            Content = new Border
            {
                StrokeShape = new RoundRectangle
                {
                    CornerRadius = 20,
                },
                Padding = 20,
                BackgroundColor = MyColors.Secondary,
                Shadow = new Shadow
                {
                    Brush = Colors.Black,
                    Radius = 15,
                    Opacity = 0.20f,
                    Offset = new Point(5, 5),
                },
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
                        .Bind(Label.TextProperty, static(Ingredient ing)=>ing.Name),

                        new Label
                        {
                            TextColor = Colors.Grey,
                            FontSize = 15,
                        }.Row(1).Column(0).CenterVertical().Start()
                        .Bind(Label.TextProperty, static(Ingredient ing)=>ing.Unit, convert: (UnitType unit) =>
                        {
                            return Ingredient.UnitValueLookUp[unit];
                        }),

                        new Label
                        {
                            TextColor = MyColors.TextPrimary,
                            FontSize = 15,
                        }.Row(2).Column(0).CenterVertical().Start()
                        .Bind(Label.TextProperty, static(Ingredient ing)=>ing.CaloriesPerUnit,
                        convert: (double v) =>
                        {
                            return $"Calories: {v}";
                        }),

                        new Label
                        {
                            TextColor = MyColors.TextPrimary,
                            FontSize = 15,
                            BindingContext = this,
                        }.Row(3).Column(0).CenterVertical().Start()
                        .Bind(Label.TextProperty, static(Ingredient ing)=>ing.ProteinPerUnit,
                        convert: (double v) =>
                        {
                            return $"Protein: {v}";
                        }),

                        new Label
                        {
                            TextColor = MyColors.TextPrimary,
                            FontSize = 15,
                        }.Row(4).Column(0).CenterVertical().Start()
                        .Bind(Label.TextProperty, static(Ingredient ing) => ing.CarbsPerUnit,
                        convert: (double v) =>
                        {
                            return $"Total Carbs: {v}";
                        }),

                        new Label
                        {
                            TextColor = MyColors.TextPrimary,
                            FontSize = 15,
                        }.Row(5).Column(0).CenterVertical().Start()
                        .Bind(Label.TextProperty, static(Ingredient ing) => ing.FatPerUnit,
                        convert: (double v) =>
                        {
                            return $"Total Fat: {v}";
                        }),
                    }
                }
            };
        }
	}
}
