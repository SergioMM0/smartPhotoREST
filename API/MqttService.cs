using System.Text;
using API.Controllers;
using Infrastructure;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;

namespace API;

public class MqttService
{
    public MqttClient MqttClient { get; set; }

    public string receivedPhoto;

    public MqttService()
    {
        MqttClient = new MqttClient("mqtt.flespi.io", 1883, false, null, null, MqttSslProtocols.TLSv1_2);

        MqttClient.MqttMsgPublishReceived += MqttClientPhotoReceived;

        MqttClient.Connect("api-receiver", "lFSqid6YHUFzB5S50F9cLl8h4A3ia5LjyrzEuts1eXpFR7yHAyHbHFiz5cH35hdr", "");

        MqttClient.Subscribe(new string[] { "espcam/take" }, new byte[] { MqttMsgBase.QOS_LEVEL_AT_LEAST_ONCE });
    }

/*

public void MqttClientPhotoReceived(object sender, MqttMsgPublishEventArgs e)
{
    //prints out the data received
    byte[] data = e.Message;
    string dataString = System.Text.Encoding.Default.GetString(data);
    Console.WriteLine(e.Message.Length);
    Console.WriteLine(dataString);

    receivedPhoto = dataString;

}
}

*/


    static async void MqttClientPhotoReceived(object sender, MqttMsgPublishEventArgs e)
    {
        byte[] data = e.Message;
        string dataString = System.Text.Encoding.Default.GetString(data);

        dataString = "\"" + dataString + "\"";

        // Make HTTP POST request to SavePhoto endpoint
        using (var client = new HttpClient())
        {
            var content = new StringContent(dataString, Encoding.UTF8, "application/json");
            var response = await client.PostAsync("http://localhost:5059/SmartPhoto/SavePhoto", content);

            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("Photo saved successfully");
            }
            else
            {
                Console.WriteLine("Failed to save photo");
                Console.WriteLine(response.ToString());
            }
        }
    }
}