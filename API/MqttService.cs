using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;

namespace API;

public class MqttService
{
    public MqttClient MqttClient { get; set; }

    public MqttService()
    {
        MqttClient = new MqttClient("mqtt.flespi.io",1883,false, null, null,MqttSslProtocols.TLSv1_2);

        MqttClient.MqttMsgPublishReceived += MqttClientPhotoReceived;

        MqttClient.Connect("api-receiver", "lFSqid6YHUFzB5S50F9cLl8h4A3ia5LjyrzEuts1eXpFR7yHAyHbHFiz5cH35hdr", "");

        MqttClient.Subscribe(new string[] { "espcam/take" }, new byte[] { MqttMsgBase.QOS_LEVEL_AT_LEAST_ONCE });
    }

    static void MqttClientPhotoReceived(object sender, MqttMsgPublishEventArgs e)
    {
        byte[] data = e.Message;
        string dataString = System.Text.Encoding.Default.GetString(data);
        Console.WriteLine(e.Message.Length);
        Console.WriteLine(dataString);
    }
}