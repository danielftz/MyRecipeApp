using CommunityToolkit.Maui.Markup;
using Microsoft.Maui.Layouts;

namespace MyRecipeApp.Components
{
    public class AccordionMenu : AbsoluteLayout
    {
        #region IsExpanded BindableProperty
        public static readonly BindableProperty IsExpandedProperty = BindableProperty.Create(
            propertyName: nameof(IsExpanded),
            returnType: typeof(bool),
            declaringType: typeof(AccordionMenu),
            propertyChanged: async (b, o, n) =>
            {
                AccordionMenu obj = (AccordionMenu)b;

                if ((bool)n is false)
                {
                    await obj._menu.ScaleYTo(0, 150, Easing.SinOut);
                    obj._menu.IsVisible = false;
                }
                else
                {
                    obj._menu.IsVisible = true; 
                    await obj._menu.ScaleYTo(1, 300, Easing.BounceOut);
                }
            }
        );
        public bool IsExpanded
        {
            get => (bool)GetValue(IsExpandedProperty);
            set => SetValue(IsExpandedProperty, value);
        }
        #endregion

        private ImageButton _button;
        private Grid _menu;

        public AccordionMenu()
        {
            // TODO: temporarily create a recipe here. Will remove later
            

            Add(_button = new ImageButton
            {
                CornerRadius = 30,
                BackgroundColor = Color.FromRgba("#A757D8"),
                Source = ImageSource.FromFile("icon_plus"),
                Command = new Command(() =>
                {
                    IsExpanded = !IsExpanded;
                })
            }.LayoutFlags(AbsoluteLayoutFlags.PositionProportional)
            .LayoutBounds(1, 1, 60, 60)
            );

            Add(_menu = new Grid
            {
                IsVisible = false,
                ScaleY = 0,
                AnchorY = 1,
                Margin = new Thickness(0, 0, 30, 30),
                BackgroundColor = Colors.White,
                RowDefinitions =
                {
                    new RowDefinition{ Height = GridLength.Star},
                    new RowDefinition{ Height = GridLength.Star},
                },
                Children =
                {
                    new Button
                    {
                        BorderColor = Colors.Transparent,
                        BackgroundColor = Colors.Transparent,
                        TextColor = Colors.Black,
                        Text = "Recipe",
                        FontSize = 16,
                        FontAttributes = FontAttributes.Bold,
                        Command = new Command(async() =>
                        {
                            await Navigation.PushAsync(new CreateARecipePage());
                        })
                    }.Row(0)
                    .Fill(),

                    new Button
                    {
                        BorderColor = Colors.Transparent,
                        BackgroundColor = Colors.Transparent,
                        TextColor = Colors.Black,
                        Text = "Ingredient",
                        FontSize = 16,
                        FontAttributes = FontAttributes.Bold,
                        Command = new Command(async() =>
                        {
                            await Navigation.PushAsync(new CreateAnIngredientPage());
                        })
                    }.Row(1)
                    .Fill()
                }
            }.LayoutFlags(AbsoluteLayoutFlags.All)
            .LayoutBounds(0, 0, 1, 1)
            );
        }
    }
}
