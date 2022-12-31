using CommunityToolkit.Maui.Markup;
using MyRecipeApp.Components;
using MyRecipeApp.Model;
using MyRecipeApp.Tools;
using System.Collections.ObjectModel;

namespace MyRecipeApp
{
    public class CreateARecipePage : ContentPage
    {
        private CreateARecipePageViewModel _vm;
        public CreateARecipePage(Recipe? recipe = null)
        {
            Title = "Create A Recipe";
            BindingContext = _vm = new CreateARecipePageViewModel(Navigation, recipe);

            Content = new Grid
            {
                Padding = new Thickness(30,15),
                ColumnSpacing = 15,
                RowDefinitions =
                {
                    new RowDefinition() {Height = new GridLength(1, GridUnitType.Star)},
                    new RowDefinition() {Height = new GridLength(2, GridUnitType.Star)},
                    new RowDefinition() {Height = new GridLength(2, GridUnitType.Star)},
                },

                Children =
                {
                    new Grid
                    {
                        RowDefinitions =
                        {
                            new RowDefinition(),
                            new RowDefinition(){ Height = GridLength.Auto},
                        },
                        ColumnDefinitions =
                        {
                            new ColumnDefinition(){ Width = new GridLength(1, GridUnitType.Star)},
                            new ColumnDefinition(){ Width = new GridLength(3, GridUnitType.Star)},
                            new ColumnDefinition(){ Width = new GridLength(1, GridUnitType.Star)},
                        },

                        Children =
                        {
                            new Label
                            {
                                Text = "Name",
                                FontSize = 18,
                                TextColor = MyColors.TextPrimary,
                            }.Row(0).Column(0).Start().CenterVertical(),

                   
                            new Entry
                            {
                                BackgroundColor = MyColors.Background,
                                Placeholder = "Enter the name of the recipe",
                                FontSize = 18,
                                TextColor = Colors.Black,
                            }.Row(0).Column(1,2).CenterVertical().FillHorizontal().TextCenter()
                            .Bind(Entry.TextProperty, 
                            static(CreateARecipePageViewModel vm)=> vm.Name, 
                            static(CreateARecipePageViewModel vm, string name)=> vm.Name = name, BindingMode.TwoWay),


                            new Label
                            {
                                Text = "Ingredients",
                                FontSize = 18,
                                TextColor = MyColors.TextPrimary,
                            }.Row(1).Column(0,2).Start().CenterVertical(),

                            new Button
                            {
                                Text = "Add",
                                BackgroundColor = MyColors.Primary,
                                
                            }.Row(1).Column(2)
                            .Bind(Button.CommandProperty, static(CreateARecipePageViewModel vm)=> vm.FindMoreIngredientsCommand),

                        }
                    }.Row(0),

                    new CollectionView
                    {
                        ItemTemplate = new DataTemplate(() =>
                        {

                            IngredientCardWithValue card = new IngredientCardWithValue();


                            card.BindingContextChanged += (s, e) =>
                            {
                                card.Bind(IngredientCardWithValue.IndexProperty,
                                new Binding("."),
                                new Binding(nameof(_vm.Ingredients), source: _vm),
                                convert: ((Ingredient ing, ObservableCollection<Ingredient> list) v) =>
                                {
                                    int idx = v.list.IndexOf(v.ing);
                                    return idx;
                                });

                                card.Bind(IngredientCardWithValue.AmountProperty,
                                new Binding(nameof(IngredientCardWithValue.Index), source: card),
                                new Binding(nameof(_vm.IngredientAmount), source: _vm),
                                convert: ((int idx, ObservableCollection<double> list) v) =>
                                {
                                    return v.list[v.idx];
                                });

                                card.PropertyChanged += (s, e) =>
                                {
                                    if (e.PropertyName is nameof(IngredientCardWithValue.Amount))
                                    {
                                        _vm.IngredientAmount[card.Index] = card.Amount;
                                    }
                                };

                            };

                            
                            return card;
                            
                        })
                        
                    }.Row(1)
                    .Bind(CollectionView.ItemsSourceProperty, static(CreateARecipePageViewModel vm)=> vm.Ingredients),

                    new Grid
                    {
                        RowDefinitions =
                        {
                            new RowDefinition() {Height = new GridLength(40, GridUnitType.Absolute)},
                            new RowDefinition() {Height = new GridLength(1, GridUnitType.Star)},
                            new RowDefinition() {Height = new GridLength(40, GridUnitType.Absolute)},
                        },

                        Children =
                        {
                            new Label
                            {
                                Text = "Cooking Instruction"
                            }.Row(0),

                            new Editor
                            {
                                MinimumWidthRequest = 600,
                            }.Row(1)
                            .Bind(Editor.TextProperty, 
                            static (CreateARecipePageViewModel vm)=> vm.Instruction, 
                            static (CreateARecipePageViewModel vm, string instruction) => vm.Instruction = instruction, BindingMode.TwoWay),

                            new Button
                            {
                                Text = "Save",
                                FontSize = 17,
                                BackgroundColor = Color.FromArgb("#A757D8"),
                                TextColor = Colors.White,

                            }.Row(2)
                            .Bind(Button.CommandProperty, static(CreateARecipePageViewModel vm) => vm.SaveRecipeCommand),
                        }

                    }.Row(2),



                }
            };

        }
    }
}
