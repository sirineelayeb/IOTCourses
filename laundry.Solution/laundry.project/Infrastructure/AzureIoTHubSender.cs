using laundry.project.Entities;
using Microsoft.Azure.Devices.Client;
using System;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace laundry.project.Infrastructure
{
    class AzureIoTHubSender : ISender
    {
        //private static readonly string connectionString = "HostName=LaundryIoTHub.azure-devices.net;DeviceId=laundryMachine1;SharedAccessKey=p987uIBaTS50xSprAQfp3M/EwzSKTN2+yhaWCFN50ZQ=";
        private static readonly string connectionString = "HostName=;DeviceId=;SharedAccessKey=";

        // Create the DeviceClient from the connection string
        private static readonly DeviceClient deviceClient = DeviceClient.CreateFromConnectionString(connectionString, TransportType.Mqtt);

        public async Task SendMessageAsync(laundry.project.Entities.Message message)
        {
            var payload = new
            {
                machineId = message.IdMachine,
                state = message.State.ToString(),
                timestamp = message.Date.ToString("o")
            };

            string jsonPayload = JsonSerializer.Serialize(payload);
            var cloudMessage = new Microsoft.Azure.Devices.Client.Message(Encoding.UTF8.GetBytes(jsonPayload));
            cloudMessage.Properties.Add("machineState", message.State.ToString());

            await deviceClient.SendEventAsync(cloudMessage);
            Console.WriteLine($"[AzureIoTHub] Message sent: {jsonPayload}");
        }

        public void SendMessage(laundry.project.Entities.Message message)
        {
            Task.Run(() => SendMessageAsync(message)).GetAwaiter().GetResult();
        }
    }
}
