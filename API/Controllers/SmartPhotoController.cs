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
    public SmartPhotoController(PhotoRepository repository)
    {
        _photoRepository = repository;
    }
    [HttpGet]
    [Route("CreateDB")]
    public String CreateDb()
    {
        _photoRepository.CreateDb();
        return "DB has been created :)";
    }

    [HttpGet]
    [Route("getAllPhotos")]
    public ActionResult<Photo[]> GetAllPhotos()
    {
        List<Photo> photos = _photoRepository.GetAllPhotos();
        photos.Reverse();
        return Ok(photos);
    }

    [HttpPost("savePhoto")]
    public IActionResult SavePhoto([FromBody] string rawPhoto)
    {
        var photo = new Photo
        {
            RawPhoto = rawPhoto,
            Time = DateTime.UtcNow
        };

        var savedPhoto = _photoRepository.SavePhoto(photo);
        return Ok(savedPhoto);
    }
 
}