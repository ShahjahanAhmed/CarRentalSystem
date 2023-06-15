using CarRentalSystem.Business.Model;
using CarRentalSystem.SampleConsoleApp.Presenter;
using Spectre.Console;

namespace CarRentalSystem.SampleConsoleApp.View
{
    internal class ReturnRegistrationView : IReturnRegistrationView
    {
        private readonly IReturnRegistrationPresenter presenter;

        public ReturnRegistrationView(IReturnRegistrationPresenter presenter)
        {
            this.presenter = presenter;
            this.presenter.Bind(this);
        }

        public void Render()
        {
            AnsiConsole.WriteLine();
            AnsiConsole.MarkupLine("[underline bold]Registration of returned car [/] ");

            var bookingNumber = AnsiConsole.Ask<long>("Booking number: ");
            var returnTime = AnsiConsole.Ask<DateTime>("Return time (yyyy'-'MM'-'dd'T'HH':'mm'): ");
            var currentMeterReading = AnsiConsole.Ask<int>("Current meter reading: ");

            presenter.RegisterReturnOfCar(bookingNumber, returnTime, currentMeterReading);
            AnsiConsole.MarkupLine($"[green]Car return registration successful! [/] ");
        }

        private static void RenderCarDeliveryRegistration(RentalSummary rentalSummary)
        {
            AnsiConsole.WriteLine();
            AnsiConsole.MarkupLine("[bold blue] ------- Rental Summary -------- [/] ");

            var table = new Table();

            table.AddColumn("Property");
            table.AddColumn("Value");

            table.AddRow("Booking Number", rentalSummary.BookingNumber.ToString());
            table.AddRow("Number of days", rentalSummary.NumberOfDays.ToString());
            table.AddRow("Number of KMs", rentalSummary.NumberOfKm.ToString());
            table.AddRow("Total price", rentalSummary.TotalPrice.ToString("F2") + "kr");

            AnsiConsole.Write(table);
        }

        public void PromptPriceFactorsFromUser(CarReturn registeredCarReturn)
        {
            AnsiConsole.WriteLine();
            AnsiConsole.MarkupLine($"[underline bold]Price calculation for booking {registeredCarReturn.BookingNumber} [/] ");
            var baseDayRental = AnsiConsole.Ask<int>("Enter Base Day Rental (SEK): ");
            var baseKmPrice = AnsiConsole.Ask<int>("Enter Base Km Price (SEK): ");
            presenter.OnPriceFactorsCollected(registeredCarReturn, baseDayRental, baseKmPrice);
        }

        public void ShowSummary(RentalSummary rentalSummary)
        {
            AnsiConsole.WriteLine();
            RenderCarDeliveryRegistration(rentalSummary);
        }
    }
}
