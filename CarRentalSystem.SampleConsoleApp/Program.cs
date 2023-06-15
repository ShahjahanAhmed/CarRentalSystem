// See https://aka.ms/new-console-template for more information

using CarRentalSystem.SampleConsoleApp.Infrastructure;

var container = new AppIocContainer();
container.CreateMainView().Render();