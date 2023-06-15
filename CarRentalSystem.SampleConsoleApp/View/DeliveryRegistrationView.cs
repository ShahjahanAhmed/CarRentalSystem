using CarRentalSystem.Business.Model;
using CarRentalSystem.SampleConsoleApp.Presenter;
using Spectre.Console;

namespace CarRentalSystem.SampleConsoleApp.View
{
    internal class DeliveryRegistrationView : IDeliveryRegistrationView
    {
        private readonly IDeliveryRegistrationPresenter presenter;

        public DeliveryRegistrationView(IDeliveryRegistrationPresenter presenter)
        {
            this.presenter = presenter;
            this.presenter.Bind(this);
        }

        public void Render()
        {
            AnsiConsole.WriteLine();
            AnsiConsole.MarkupLine("[underline bold]Registration of car delivery[/] ");

            var registration = AnsiConsole.Ask<string>("Registration number: ");
            var socialSecurity = AnsiConsole.Ask<string>("Social Security number: ");

            var selectedCarCategory = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("Select car category:")
                    .AddChoices(presenter.SupportedCarCategories));
            AnsiConsole.WriteLine($"Selected category: {selectedCarCategory}");


            var pickupTime = AnsiConsole.Ask<DateTime>("Pickup time (yyyy'-'MM'-'dd'T'HH':'mm'): ");
            var currentMeterReading = AnsiConsole.Ask<int>("Current meter reading: ");

            presenter.RegisterDelivery(registration, socialSecurity, selectedCarCategory, pickupTime, currentMeterReading);

        
        }

        public void RenderCarDeliveryRegistration(CarDelivery registeredDelivery)
        {
            AnsiConsole.MarkupLine($"[green]Car delivery registration successful! [/] ");

            var table = new Table();

            table.AddColumn("Property");
            table.AddColumn("Value");

            table.AddRow("Booking Number", registeredDelivery.BookingNumber.ToString());
            table.AddRow("Registration Number", registeredDelivery.CarRegistration);
            table.AddRow("Social Security", registeredDelivery.SocialSecurityNumber);
            table.AddRow("Car Category", registeredDelivery.CarCategory);
            table.AddRow("Pickup time", registeredDelivery.PickupTime.ToString("yyyy'-'MM'-'dd'T'HH':'mm"));
            table.AddRow("Current meter reading", registeredDelivery.MeterReadingAtDelivery.ToString());

            AnsiConsole.Write(table);
        }

        public void ShowError(string message)
        {
            AnsiConsole.WriteLine();
            AnsiConsole.MarkupLine($"[bold red] {message} [/] ");
            AnsiConsole.WriteLine();
        }
    }
}
