using CommunityToolkit.Maui.Markup;
using MyRecipeApp.Components;
using MyRecipeApp.Model;
using MyRecipeApp.Tools;
using System.Collections.ObjectModel;

namespace MyRecipeApp
{
    public class MyIngredientsPage : ContentPage
    {
        private readonly MyIngredientsPageViewModel _vm;


        private readonly RefreshView _refreshView;
        public RefreshView RefreshView { get => _refreshView; }
        public MyIngredientsPage(string recipeName, ObservableCollection<Ingredient> destinationList)
        {
            Title = "My Ingredients";

            BindingContext = _vm = new MyIngredientsPageViewModel(recipeName, destinationList);

            Content = new Grid
            {
                RowDefinitions =
                {
                    new RowDefinition() { Height = new GridLength(50, GridUnitType.Absolute) },
                    new RowDefinition() { Height = new GridLength(1, GridUnitType.Star) },
                    new RowDefinition() { Height = new GridLength(50, GridUnitType.Absolute) },
                    new RowDefinition() { Height = new GridLength(50, GridUnitType.Absolute) },
                },

                Children =
                {
                    new Entry
                    {
                        Placeholder = "Type in the name of the ingredient",
                        BackgroundColor = Colors.LightGrey,
                    }.Row(0),

                    new RefreshView
                    {
                        RefreshColor = MyColors.Primary,
                        Content =  new CollectionView
                        {
                            SelectionMode = SelectionMode.None,
                            ItemSizingStrategy = ItemSizingStrategy.MeasureAllItems,
                            ItemsLayout = new LinearItemsLayout (ItemsLayoutOrientation.Vertical)
                            {
                                ItemSpacing = 10,
                            },
                            BackgroundColor = Colors.White,
                            ItemTemplate = new DataTemplate(() =>
                            {
                                return new IngredientCard()
                                {
                                }
                                .Bind(IngredientCard.RemoveCommandProperty, static(MyIngredientsPageViewModel vm) => vm.RemoveCommand, source: _vm)
                                .Bind(IngredientCard.RemoveCommandParameterProperty, ".")
                                .Bind(IngredientCard.SelectCommandProperty, static(MyIngredientsPageViewModel vm) => vm.SelectCommand, source: _vm)
                                .Bind(IngredientCard.SelectCommandParameterProperty, ".")
                                .Bind(IngredientCard.IsSelectedProperty,
                                new Binding("."),
                                new Binding(nameof(_vm.SelectedIngredients), source: _vm),
                                convert: ((Ingredient ing, ObservableCollection<Ingredient> list) v) =>
                                {
                                    return v.list.Contains(v.ing);
                                });
                            })
                        }
                        .Bind(CollectionView.ItemsSourceProperty, nameof(_vm.StoredIngredients), source: _vm)
                        //.Bind(CollectionView.SelectedItemsProperty, nameof(_vm.SelectedIngredients), source: _vm),
                    }.Row(1)
                    .Assign(out _refreshView)
                    .Bind(RefreshView.CommandProperty, nameof(_vm.RefreshItemsCommand), source:_vm )
                    .Bind(RefreshView.CommandParameterProperty, nameof(RefreshView), source: this ),


                     new Button
                    {
                        Text = "Save Selection",
                        BackgroundColor = Color.FromArgb("#A757D8"),
                    }.Row(2)
                    .Bind(Button.CommandProperty, nameof(_vm.SaveToRecipeCommand), source: _vm),

                    new Button
                    {
                        Text = "Create an ingredient",
                        BackgroundColor = Color.FromArgb("#A757D8"),
                        Command = new Command(async() =>
                        {
                            await Navigation.PushAsync(new CreateAnIngredientPage());
                        })
                    }.Row(3),
                }
            };
        }

        //protected override void OnDisappearing()
        //{
        //    _vm.SaveToRecipeCommand.Execute(null);
        //}
    }
}
