using CommunityToolkit.Maui.Markup;
using MyRecipeApp.Model;
using MyRecipeApp.Tools;

namespace MyRecipeApp
{
    public class CreateAnIngredientPage : ContentPage
    {
        private readonly CreateAnIngredientPageViewModel _vm;
        public CreateAnIngredientPage(Ingredient? ingredient = null)
        {
            Title = "Create An Ingredient";
            BindingContext = _vm = new CreateAnIngredientPageViewModel(Navigation, ingredient);


            Content = new Grid
            {
                RowDefinitions =
                {
                    new RowDefinition() { Height = new GridLength(50, GridUnitType.Absolute) },
                    new RowDefinition() { Height = new GridLength(50, GridUnitType.Absolute) },
                    new RowDefinition() { Height = new GridLength(50, GridUnitType.Absolute) },
                    new RowDefinition() { Height = new GridLength(50, GridUnitType.Absolute) },
                    new RowDefinition() { Height = new GridLength(50, GridUnitType.Absolute) },
                    new RowDefinition() { Height = new GridLength(50, GridUnitType.Absolute) },
                    new RowDefinition() { Height = new GridLength(1, GridUnitType.Star) },
                },

                ColumnDefinitions =
                {
                    new ColumnDefinition() { Width = new GridLength(80, GridUnitType.Absolute) },
                    new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) }
                },

                RowSpacing = 15,
                ColumnSpacing = 10,
                Children =
                {
                    new Label
                    {
                        Text = "Name"
                    }.Row(0).Column(0)
                    .Fill().TextCenter(),

                    new Label
                    {
                        Text = "Unit Type"
                    }.Row(1).Column(0),

                    new Label
                    {
                        Text = "Calories"
                    }.Row(2).Column(0),

                    new Label
                    {
                        Text = "Protein"
                    }.Row(3).Column(0),

                    new Label
                    {
                        Text = "Fat"

                    }.Row(4).Column(0),

                    new Label
                    {
                        Text = "Carbs"
                    }.Row(5).Column(0),

                    new Entry
                    {
                        BackgroundColor = MyColors.Background,
                        FontSize = 16,
                        TextColor = MyColors.TextPrimary,
                    }.Row(0).Column(1)
                    .Bind(Entry.TextProperty, nameof(_vm.Name), BindingMode.TwoWay),


                    new Picker
                    {
                        Title = "Unit Type",
                        ItemsSource = Ingredient.UnitNames,
                    }.Row(1).Column(1)
                    .Bind(Picker.SelectedIndexProperty, nameof(_vm.Unit), BindingMode.TwoWay,
                    convert: (UnitType unit) =>
                    {
                        switch (unit)
                        {
                            case(UnitType.Single):
                                return 0;
                            case (UnitType.OneHundredGrams):
                                return 2;
                            case (UnitType.OneMillileter):
                                return 3;
                            case (UnitType.OneLiter):
                                return 4;
                            default:
                            case(UnitType.OneGram):
                                return 1;

                        }
                    },
                    convertBack: (int idx) =>
                    {
                        return (UnitType) idx;
                    }),


                    new Entry
                    {
                        BackgroundColor = MyColors.Background,
                        FontSize = 16,
                        TextColor = MyColors.TextPrimary,
                        Keyboard = Keyboard.Numeric,
                        CursorPosition = 0,
                        SelectionLength = int.MaxValue,
                    }.Row(2).Column(1)
                    .Bind(Entry.TextProperty, nameof(_vm.Calories), BindingMode.TwoWay),

                    new Entry
                    {
                        BackgroundColor = MyColors.Background,
                        FontSize = 16,
                        TextColor = MyColors.TextPrimary,
                        Keyboard = Keyboard.Numeric,
                        CursorPosition = 0,
                        SelectionLength = int.MaxValue,
                    }.Row(3).Column(1)
                    .Bind(Entry.TextProperty, nameof(_vm.Protein), BindingMode.TwoWay),

                    new Entry
                    {
                        BackgroundColor = MyColors.Background,
                        FontSize = 16,
                        TextColor = MyColors.TextPrimary,
                        Keyboard = Keyboard.Numeric,
                        CursorPosition = 0,
                        SelectionLength = int.MaxValue,
                    }.Row(4).Column(1)
                    .Bind(Entry.TextProperty, nameof(_vm.Fat), BindingMode.TwoWay),

                    new Entry
                    {
                        BackgroundColor = MyColors.Background,
                        FontSize = 16,
                        TextColor = MyColors.TextPrimary,
                        Keyboard = Keyboard.Numeric,
                        CursorPosition = 0,
                        SelectionLength = int.MaxValue,
                    }.Row(5).Column(1)
                    .Bind(Entry.TextProperty, nameof(_vm.Carbs), BindingMode.TwoWay),

                    new Button
                    {
                        Text = "Save",
                        HeightRequest = 50,
                        Margin = new Thickness(30, 0),
                        BackgroundColor = MyColors.Primary,
                    }.CenterVertical().FillHorizontal()
                    .Row(6).Column(0,2)
                    .Bind(Button.CommandProperty, nameof(_vm.SaveCommand), source: _vm)
                }
            };



        }
    }
}
