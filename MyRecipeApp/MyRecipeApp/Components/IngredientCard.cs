using CommunityToolkit.Maui.Markup;
using Microsoft.Maui.Controls.Shapes;
using Microsoft.Maui.Layouts;
using MyRecipeApp.Model;
using MyRecipeApp.Tools;
using System.Windows.Input;

namespace MyRecipeApp.Components
{
    public class IngredientCard : Border
	{

        #region IsSelected BindableProperty
        public static readonly BindableProperty IsSelectedProperty = BindableProperty.Create(
            propertyName: nameof(IsSelected),
            returnType: typeof(bool),
            declaringType: typeof(IngredientCard)
        );
        public bool IsSelected
        {
            get => (bool)GetValue(IsSelectedProperty);
            set => SetValue(IsSelectedProperty, value);
        }
        #endregion

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


        #region SelectCommand BindableProperty
        public static readonly BindableProperty SelectCommandProperty = BindableProperty.Create(
            propertyName: nameof(SelectCommand),
            returnType: typeof(ICommand),
            declaringType: typeof(IngredientCard)
        );
        public ICommand SelectCommand
        {
            get => (ICommand)GetValue(SelectCommandProperty);
            set => SetValue(SelectCommandProperty, value);
        }
        #endregion


        #region SelectCommandParameter BindableProperty
        public static readonly BindableProperty SelectCommandParameterProperty = BindableProperty.Create(
            propertyName: nameof(SelectCommandParameter),
            returnType: typeof(object),
            declaringType: typeof(IngredientCard)
        );
        public object SelectCommandParameter
        {
            get => (object)GetValue(SelectCommandParameterProperty);
            set => SetValue(SelectCommandParameterProperty, value);
        }
        #endregion



        public IngredientCard()
		{
            StrokeShape = new RoundRectangle
            {
                CornerRadius = 20,
            };
            Padding = 20;
            BackgroundColor = MyColors.Secondary;

            Content = new Grid
            {
                RowDefinitions = new RowDefinitionCollection
                {
                    new RowDefinition(new GridLength(1, GridUnitType.Auto)),
                    new RowDefinition(new GridLength(1, GridUnitType.Star)),
                    new RowDefinition(new GridLength(1, GridUnitType.Star)),
                    new RowDefinition(new GridLength(1, GridUnitType.Star)),
                    new RowDefinition(new GridLength(1, GridUnitType.Star)),
                    new RowDefinition(new GridLength(1, GridUnitType.Auto)),
                },

                ColumnDefinitions =
                {
                    new ColumnDefinition(new GridLength(1, GridUnitType.Star)),
                    new ColumnDefinition(new GridLength(3, GridUnitType.Star)),
                    new ColumnDefinition(new GridLength(4, GridUnitType.Star)),
                },
                RowSpacing = 5,
                ColumnSpacing = 5,
                Children =
                {
                    new CheckBox
                    {
                        Color = MyColors.Primary,
                        InputTransparent = true,
                        BindingContext = this
                    }.Row(0, 6).Column(0).Top().Start()
                    .Bind(CheckBox.IsCheckedProperty, static(IngredientCard card) => card.IsSelected),

                    new Label
                    {
                        TextColor = MyColors.TextPrimary,
                        FontAttributes = FontAttributes.Bold,
                        FontSize = 17,
                        FormattedText = new FormattedString
                        {
                            Spans =
                            {
                                new Span
                                {
                                    TextColor = MyColors.TextPrimary,
                                    FontAttributes = FontAttributes.Bold,
                                    FontSize = 17,
                                }.Bind(Span.TextProperty, static(Ingredient ing)=>ing.Name),

                                new Span
                                {
                                    TextColor = MyColors.TextSecondary,
                                    FontSize = 16,
                                }.Bind(Span.TextProperty, static(Ingredient ing)=>ing.Unit, convert: (UnitType unit) =>
                                {
                                    return $" ({Ingredient.UnitNames[(int)unit]})";
                                }),
                            }
                        }
                    }.Row(0).Column(1, 2).Start().CenterVertical(),

                    new Label
                    {
                        TextColor = MyColors.TextPrimary,
                        FontSize = 15,
                    }.Row(1).Column(1,2).CenterVertical().Start()
                    .Bind(Label.TextProperty, static(Ingredient ing)=>ing.CaloriesPerUnit,
                    convert: (double v) =>
                    {
                        return $"Calories: {v}";
                    }),

                    new Label
                    {
                        TextColor = MyColors.TextPrimary,
                        FontSize = 15,
                    }.Row(2).Column(1,2).CenterVertical().Start()
                    .Bind(Label.TextProperty, static(Ingredient ing)=>ing.ProteinPerUnit,
                    convert: (double v) =>
                    {
                        return $"Protein: {v}";
                    }),

                    new Label
                    {
                        TextColor = MyColors.TextPrimary,
                        FontSize = 15,
                    }.Row(3).Column(1, 2).CenterVertical().Start()
                    .Bind(Label.TextProperty, static(Ingredient ing) => ing.CarbsPerUnit,
                    convert: (double v) =>
                    {
                        return $"Total Carbs: {v}";
                    }),

                    new Label
                    {
                        TextColor = MyColors.TextPrimary,
                        FontSize = 15,
                    }.Row(4).Column(1, 2).CenterVertical().Start()
                    .Bind(Label.TextProperty, static(Ingredient ing) => ing.FatPerUnit,
                    convert: (double v) =>
                    {
                        return $"Total Fat: {v}";
                    }),

                    new Button
                    {
                        Margin = new Thickness(10,10,10,0),
                        BindingContext = this,
                        BackgroundColor = Colors.Transparent,
                        BorderWidth = 2,
                        BorderColor = MyColors.Primary,
                        TextColor = MyColors.Primary,
                        FontSize = 15,
                    }.Row(5).Column(0, 2).Fill()
                    .Bind(Button.TextProperty, static(IngredientCard card)=>card.IsSelected, convert: (bool isSelected) =>
                    {
                        if (isSelected is true)
                        {
                            return "Unselect";
                        }
                        return "Select";
                    })
                    .Bind(Button.CommandProperty, static(IngredientCard card)=>card.SelectCommand)
                    .Bind(Button.CommandParameterProperty, static(IngredientCard card)=>card.SelectCommandParameter),

                    new Button
                    {
                        Margin = new Thickness(10,10,10,0),
                        BindingContext = this,
                        BackgroundColor = Colors.Orange,
                        TextColor = Colors.White,
                        FontSize = 15,
                        Text = "Remove"
                    }.Row(5).Column(2).Fill()
                    .Bind(Button.CommandProperty, static(IngredientCard card)=>card.RemoveCommand)
                    .Bind(Button.CommandParameterProperty, static(IngredientCard card)=>card.RemoveCommandParameter),
                }
            };
        }
	}
}
