using Entities;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using MQTTnet;
using MQTTnet.Client;

namespace API.Controllers;

[ApiController]
[Route("[Controller]")]
public class SmartPhotoController:ControllerBase
{
    private PhotoRepository _photoRepository;
    //public MqttClientService MQTTclient;
    


    public SmartPhotoController(PhotoRepository repository)
    {
        _photoRepository = repository;
        //MQTTclient = new MqttClientService();
        //MQTTclient.Handle_Received_Application_Message();
    }

    /*
    [HttpGet]
    [Route("getConnection")]
    public async void GetConnection()
    {
        var options = new MqttClientOptionsBuilder()
            .WithTcpServer("mqtt.flespi.io", 1883)
            .WithCredentials("t60iunhGAss20vZ245rrjaasZjUAlSRCOFI7SPBh7T9VdWO7S3pZ1nhQw0eFGU7j", "")
            .Build();

        var factory = new MqttFactory();
        var mqttClient = factory.CreateMqttClient();

        await mqttClient.ConnectAsync(options, CancellationToken.None);

        var message = new MqttApplicationMessageBuilder()
            .WithTopic("Test/Mqtt")
            .WithPayload("Hello World")
            .Build();

        await mqttClient.PublishAsync(message, CancellationToken.None); 
    }
*/
    [HttpGet]
    [Route("CreateDB")]
    public String CreateDb()
    {
        _photoRepository.CreateDb();
        return "DB has been created :)";
    }

    [HttpGet]
    [Route("getAllPhotos")]
    public object GetAllPhotos()
    {
        return _photoRepository.GetAllPhotos();
    }

    [HttpPost("savePhoto")]
    public IActionResult SavePhoto([FromBody] string rawPhoto)
    {
        var photo = new Photo
        {
            RawPhoto = rawPhoto
        };

        var savedPhoto = _photoRepository.SavePhoto(photo);
        return Ok(savedPhoto);
    }
    
    
    
    

}