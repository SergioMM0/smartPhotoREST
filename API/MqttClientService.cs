using MQTTnet;
using MQTTnet.Client;

namespace API;

public class MqttClientService
{
    
    /*
    private static object Options;
    private static object mqttClient;

    static MqttClient()
    {
        Options = new MqttClientOptionsBuilder()
            .WithTcpServer("mqtt.flespi.io", 1883)
            .WithCredentials("t60iunhGAss20vZ245rrjaasZjUAlSRCOFI7SPBh7T9VdWO7S3pZ1nhQw0eFGU7j", "")
            .Build();

        var factory = new MqttFactory();
        mqttClient = factory.CreateMqttClient();
        await mqttClient.ConnetAsync(Options, CancellationToken.None);
    }

    public static async void StablishConnection()
    {
        await mqttClient.ConnetAsync(Options, CancellationToken.None);
    }
    */
    
    
    
    
    
    
    

    public async Task Handle_Received_Application_Message()
    {
            
         // This sample subscribes to a topic and processes the received message.
         

        var mqttFactory = new MqttFactory();

        using (var mqttClient = mqttFactory.CreateMqttClient())
        {
            var mqttClientOptions = new MqttClientOptionsBuilder().WithTcpServer("mqtt.flespi.io", 1883)
                .WithCredentials("lFSqid6YHUFzB5S50F9cLl8h4A3ia5LjyrzEuts1eXpFR7yHAyHbHFiz5cH35hdr", "")
                .Build();

            // Setup message handling before connecting so that queued messages
            // are also handled properly. When there is no event handler attached all
            // received messages get lost.
            mqttClient.ApplicationMessageReceivedAsync += e =>
            {
                Console.WriteLine("Received application message.");
                Console.WriteLine(e.ResponseReasonString);

                return Task.CompletedTask;
            };

            await mqttClient.ConnectAsync(mqttClientOptions, CancellationToken.None);

            var mqttSubscribeOptions = mqttFactory.CreateSubscribeOptionsBuilder()
                .WithTopicFilter(
                    f =>
                    {
                        f.WithTopic("espcam/take");
                    })
                .Build();

            await mqttClient.SubscribeAsync(mqttSubscribeOptions, CancellationToken.None);

            Console.WriteLine("MQTT client subscribed to topic.");
        }
    }

    
}