using laundry.project.Business;
using laundry.project.Entities;
using laundry.project.Infrastructure;
using laundry.project.Infrastructure.Sender;
using laundry.project.Presentation;
using System.Collections.Generic;

SetConsoleSizeToMax();
"Reading config file...".WriteLineRight(ConsoleColor.Blue);

"Reading config file...".WriteLineLeft(ConsoleColor.White);
ConfigurationManager configService = new();
DisplayManager displaySercice = new DisplayManager();
"Config File Path : ".WriteLeft(ConsoleColor.White);
string fileConfigFilePath=Console.ReadLine();
"Config File Name : ".WriteLeft(ConsoleColor.White);
string fileConfigFileName = Console.ReadLine();
List<Machine> machines = configService.SetConfig(fileConfigFilePath +"\\"+ fileConfigFileName);
SensorManager sensor = new SensorManager();
ISender sender = new ConsoleSender();
StateMachineManager.LancerStateMachine(machines, sensor, sender);
displaySercice.DicplayConfiguration(machines,Structure.MapInputMachine);
displaySercice.DicplayMenuPrincipale(machines, Structure.MapInputMachine);
static void SetConsoleSizeToMax()
{
    try
    {
        // Obtenir la taille maximale possible de la console
        int maxWidth = Console.LargestWindowWidth;
        int maxHeight = Console.LargestWindowHeight;

        // Définir la taille de la fenêtre de la console
        Console.SetWindowSize(maxWidth, maxHeight);

        // Définir la taille du tampon de la console
        Console.SetBufferSize(maxWidth, maxHeight);
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Erreur lors de la définition de la taille de la console : {ex.Message}");
    }
}


