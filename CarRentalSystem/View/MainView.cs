using CarRentalSystem.Constants;
using CarRentalSystem.Core;
using Spectre.Console;

namespace CarRentalSystem.View
{
    public class MainView
    {
        private readonly Container container;

        public MainView(Container container)
        {
            this.container = container;
        }

        public void Render()
        {
            AnsiConsole.MarkupLine("[bold blue underline]Welcome to Car Rental System[/]");
            AnsiConsole.WriteLine();
            bool continueRegistration;
            do
            {
                AnsiConsole.WriteLine();

                var registrationOption = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title("Select registration option (up/down key):")
                        .AddChoices(RegistrationOptions.CarDelivery, RegistrationOptions.CarReturn));

                switch (registrationOption)
                {
                    case RegistrationOptions.CarDelivery:
                        container.CreateDeliveryRegistrationView().Render();
                        break;
                    case RegistrationOptions.CarReturn:
                        container.CreateReturnRegistrationView().Render();
                        break;
                }

                AnsiConsole.WriteLine();
                continueRegistration = AnsiConsole.Confirm("Do you want to do another registration?");
            } while (continueRegistration);

            AnsiConsole.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }

}
