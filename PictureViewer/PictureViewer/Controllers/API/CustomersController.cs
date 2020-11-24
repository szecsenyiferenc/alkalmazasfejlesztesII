using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Core.Models;
using Core.Models.Input;
using Core.Models.Output;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PictureViewer.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PictureViewer.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly DatabaseService _databaseService;
        private readonly IMapper _autoMapper;

        public CustomersController(DatabaseService databaseService, IMapper autoMapper)
        {
            _databaseService = databaseService;
            _autoMapper = autoMapper;
        }

        [HttpPost]
        [Route("checklogin")]
        public ActionResult<CustomerOutputDto> CheckLogin([FromBody] LoginData loginData)
        {
            var customer = _databaseService.CheckLogin(loginData);
            if (customer == null)
            {
                return Forbid();
            }

            var customerDto = _autoMapper.Map<CustomerOutputDto>(customer);

            return Ok(customerDto);
        }

        [HttpGet]
        [HttpHead]
        public ActionResult<IEnumerable<CustomerOutputDto>> Get()
        {
            var customers = _databaseService.GetCustomers();

            var customerDtos = _autoMapper.Map<IEnumerable<CustomerOutputDto>>(customers);

            return Ok(customerDtos);
        }

        [HttpGet("{id}", Name = "GetCustomer")]
        [HttpHead("{id}")]
        public ActionResult<CustomerOutputDto> Get([FromRoute] int id)
        {
            var customer = _databaseService.GetCustomer(id);
            if (customer == null)
            {
                return NotFound();
            }

            var customerDto = _autoMapper.Map<CustomerOutputDto>(customer);

            return Ok(customerDto);
        }

        [HttpPost]
        public async Task<ActionResult<CustomerOutputDto>> Post([FromBody] CustomerInputDto customerInput)
        {
            var customer = _autoMapper.Map<Customer>(customerInput);

            var newCustomer = await _databaseService.AddCustomer(customer);

            var customerDto = _autoMapper.Map<CustomerOutputDto>(newCustomer);

            return CreatedAtRoute("GetCustomer", new { id = customerDto.Id }, customerDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromRoute] int id, [FromBody] object pictureInputObject)
        {
            PictureInputDto pictureInput = JsonConvert.DeserializeObject<PictureInputDto>(pictureInputObject.ToString());

            var picture = _autoMapper.Map<Picture>(pictureInput);
            picture.Id = id;

            try
            {
                await _databaseService.UpdatePicture(picture);
            }
            catch
            {
                return BadRequest();
            }

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await _databaseService.DeleteCustomer(id);
        }

        [HttpOptions]
        public IActionResult Options(int id)
        {
            Response.Headers.Add("Allow", "GET, POST, OPTIONS, HEAD");
            return Ok();
        }
    }
}
