using RecipeApp.Tools;

namespace MyRecipeApp;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();
        MainPage = new NavigationPage(new MainPage())
        {
            BarBackgroundColor = Color.FromArgb("#A757D8"),
        };
    }

    protected override Window CreateWindow(IActivationState? activationState)
    {

        Window window = base.CreateWindow(activationState);


        window.Created += async (s, e) =>
        {
            await DatabaseService.OpenConnectionAsync();
        };
        //raised when the window is being destroyed and deallocated.
        window.Destroying += async (s, e) =>
        {
            await DatabaseService.CloseConnectionAsync();
        };


        return window;
    }
}
