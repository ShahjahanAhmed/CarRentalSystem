// See https://aka.ms/new-console-template for more information

using CarRentalSystem.SampleConsoleApp.Infrastructure;
using NLog;

var logger = LogManager.GetCurrentClassLogger();
logger.Info("Starting application");

var container = new AppIocContainer();
container.CreateMainView().Render();

logger.Info("Closing application");
