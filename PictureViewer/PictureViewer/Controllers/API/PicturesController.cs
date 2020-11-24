using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Core.Models;
using Core.Models.Input;
using Core.Models.Output;
using Microsoft.AspNetCore.Mvc;
using PictureViewer.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PictureViewer.Controllers.API
{
    [ApiController]
    public class PicturesController : ControllerBase
    {
        private readonly DatabaseService _databaseService;
        private readonly IMapper _autoMapper;

        public PicturesController(DatabaseService databaseService, IMapper autoMapper)
        {
            _databaseService = databaseService;
            _autoMapper = autoMapper;
        }

        [HttpGet]
        [HttpHead]
        [Route("api/pictures")]
        public ActionResult<IEnumerable<PictureOutputDto>> GetAll()
        {
            var customers = _databaseService.GetPictures();

            var pictureDtos = _autoMapper.Map<IEnumerable<PictureOutputDto>>(customers);

            return Ok(pictureDtos);
        }

        [Route("api/pictures")]
        [HttpDelete("{id}")]
        public async Task Delete([FromRoute] int id)
        {
            await _databaseService.DeletePicture(id);
        }

        [HttpGet]
        [HttpHead]
        [Route("api/customers/{customerId}/pictures")]
        public ActionResult<IEnumerable<PictureOutputDto>> Get()
        {
            var customers = _databaseService.GetPictures();

            var pictureDtos = _autoMapper.Map<IEnumerable<PictureOutputDto>>(customers);

            return Ok(pictureDtos);
        }

        [HttpGet("{id}", Name = "GetPicture")]
        [HttpHead("{id}")]
        [Route("api/customers/{customerId}/pictures")]
        public ActionResult<PictureOutputDto> Get([FromRoute] int id)
        {
            var customer = _databaseService.GetPicture(id);
            if (customer == null)
            {
                return NotFound();
            }

            var pictureDto = _autoMapper.Map<PictureOutputDto>(customer);

            return Ok(pictureDto);
        }

        [HttpPost]
        [Route("api/customers/{customerId}/pictures")]
        public async Task<ActionResult<PictureOutputDto>> Post(int customerId, [FromBody] PictureInputDto pictureInput)
        {
            var customer = _databaseService.GetCustomer(customerId);
            if(customer == null)
            {
                return NotFound();
            }

            var picture = _autoMapper.Map<Picture>(pictureInput);

            var newPicture = await _databaseService.AddPicture(customer.Id, picture);

            var pictureDto = _autoMapper.Map<PictureOutputDto>(newPicture);

            return CreatedAtRoute("GetPicture", new { id = pictureDto.Id }, pictureDto);
        }


    }
}
