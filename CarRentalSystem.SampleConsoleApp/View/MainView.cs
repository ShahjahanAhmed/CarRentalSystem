using CarRentalSystem.SampleConsoleApp.Constants;
using CarRentalSystem.SampleConsoleApp.Infrastructure;
using Spectre.Console;

namespace CarRentalSystem.SampleConsoleApp.View
{
    public class MainView
    {
        private readonly IAppIocContainer iocContainer;

        public MainView(IAppIocContainer iocContainer)
        {
            this.iocContainer = iocContainer;
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
                        iocContainer.CreateDeliveryRegistrationView().Render();
                        break;
                    case RegistrationOptions.CarReturn:
                        iocContainer.CreateReturnRegistrationView().Render();
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
