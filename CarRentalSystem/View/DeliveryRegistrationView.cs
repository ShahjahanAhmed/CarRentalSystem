using CarRentalSystem.Constants;
using CarRentalSystem.Model;
using CarRentalSystem.Presenter;
using Spectre.Console;

namespace CarRentalSystem.View
{
    public class DeliveryRegistrationView
    {
        private readonly DeliveryRegistrationPresenter presenter;

        public DeliveryRegistrationView(DeliveryRegistrationPresenter presenter)
        {
            this.presenter = presenter;
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
                    .AddChoices(new[]
                    {
                        "Small car", "Combi", "Truck"
                    }));
            AnsiConsole.WriteLine($"Selected category: {selectedCarCategory}");


            var pickupTime = AnsiConsole.Ask<DateTime>("Pickup time (yyyy'-'MM'-'dd'T'HH':'mm'): ");
            var currentMeterReading = AnsiConsole.Ask<int>("Current meter reading: ");

            var registeredDelivery = presenter.RegisterDelivery(registration, socialSecurity, selectedCarCategory, pickupTime, currentMeterReading);

            RenderCarDeliveryRegistration(registeredDelivery);
            AnsiConsole.MarkupLine($"[green]Car delivery registration successful! [/] ");
        }

        private static void RenderCarDeliveryRegistration(CarDelivery registeredDelivery)
        {
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
    }
}
