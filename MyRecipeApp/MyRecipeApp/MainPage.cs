using CommunityToolkit.Maui.Markup;
using Microsoft.Maui.Layouts;
using MyRecipeApp.Components;
using MyRecipeApp.Model;
using MyRecipeApp.Tools;

namespace MyRecipeApp
{
    public class MainPage : ContentPage
    {
        //title: Recipe
        private MainPageViewModel _vm;
        public MainPage()
        {
            BindingContext = _vm = new MainPageViewModel();

            Label title = new Label
            {
                Text = "Recipe",
                FontAttributes = FontAttributes.Bold,
                FontSize = 18,
                TextColor = Colors.White,
            }.Center().TextCenter();
            NavigationPage.SetTitleView(this, title);



            Content = new AbsoluteLayout
            {
                Children =
                {
                    new Grid
                    {
                        Padding = new Thickness(30,15),
                        RowSpacing = 15,
                        ColumnSpacing = 30,
                        RowDefinitions =
                        {
                            new RowDefinition(){ Height = new GridLength(1, GridUnitType.Star)},
                            new RowDefinition(){ Height = new GridLength(4, GridUnitType.Star)},
                        },
                        ColumnDefinitions =
                        {
                            new ColumnDefinition(),
                            new ColumnDefinition(),
                        },
                        Children =
                        {
                            new Label
                            {
                                Text = "Recipe of the day",
                                TextColor = MyColors.TextPrimary,
                                FontSize = 15,
                            }.Row(0).Column(0)
                            .Start().CenterVertical(),


                            new Button
                            {
                                BorderColor = MyColors.Primary,
                                BorderWidth = 2,
                                Text = "SURPRISE ME!",
                                TextColor = MyColors.Primary,
                                BackgroundColor = MyColors.Secondary.WithAlpha(0.2f),
                                CornerRadius = 10,
                                FontSize = 15,
                            }.Row(0).Column(1),

                            new RecipeCard()
                            {
                                //BindingContext = _vm.RandomRecipe,
                            }
                            .Row(1).Column(0,2)
                        }
                    }.LayoutFlags(AbsoluteLayoutFlags.All)
                    .LayoutBounds(0,0,1,0.35),

                    new BoxView
                    {
                        Color = Colors.Black,
                        Opacity = 0.3,
                    }.LayoutFlags(AbsoluteLayoutFlags.PositionProportional |AbsoluteLayoutFlags.WidthProportional  )
                    .LayoutBounds(0, 0.35, 1, 2),

                    new Grid
                    {
                        Padding = new Thickness(30,15),
                        RowSpacing = 15,
                        RowDefinitions =
                        {
                            new RowDefinition(){Height = GridLength.Auto},
                            new RowDefinition(){Height = GridLength.Star},
                        },
                        Children =
                        {
                            new Label
                            {
                                TextColor = MyColors.TextPrimary,
                                FontSize = 15,
                                Text = "Recipe",
                            }.Row(0).CenterVertical().Start(),

                            new CollectionView()
                            {
                                
                                //ItemSizingStrategy = ItemSizingStrategy.MeasureAllItems,
                                ItemsLayout = new LinearItemsLayout(ItemsLayoutOrientation.Vertical)
                                {
                                    ItemSpacing = 10,

                                },
                                ItemTemplate = new DataTemplate(() =>
                                {
                                    return new RecipeCard
                                    {
                                        //BindingContext automatically assigned to each item

                                    }
                                    .Bind(RecipeCard.RecipeNameProperty, nameof(Recipe.Name))
                                    .Bind(RecipeCard.CaloriesProperty, nameof(Recipe.TotalCalories))
                                    .Bind(RecipeCard.ProteinProperty, nameof(Recipe.TotalProtein))
                                    .Bind(RecipeCard.CarbohydrateProperty, nameof(Recipe.TotalCarbs))
                                    .Bind(RecipeCard.FatProperty, nameof(Recipe.TotalFat))
                                    .Bind(RecipeCard.TimeToMakeProperty, nameof(Recipe.TimeToMake))
                                    .Bind(RecipeCard.IsFavoriteProperty, nameof(Recipe.IsFavorite), BindingMode.TwoWay);
                                })
                            }.Bind(CollectionView.ItemsSourceProperty, nameof(_vm.TempRecipes), source: _vm)
                            .Row(1)
                        }
                    } .LayoutFlags(AbsoluteLayoutFlags.All)
                    .LayoutBounds(0,1,1,0.65),

                    new AccordionMenu()
                    {
                        Margin = new Thickness(0,0,30,30)
                    }.LayoutFlags(AbsoluteLayoutFlags.All)
                    .LayoutBounds(1,1,0.4,0.3)
                }
            };


        }
    }
}
