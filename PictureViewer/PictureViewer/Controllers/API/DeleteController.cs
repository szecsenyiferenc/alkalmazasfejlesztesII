using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PictureViewer.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PictureViewer.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeleteController : ControllerBase
    {
        private readonly DatabaseService _databaseService;

        public DeleteController(DatabaseService databaseService)
        {
            _databaseService = databaseService;
        }

        // GET: api/<DeleteController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<DeleteController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<DeleteController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<DeleteController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<DeleteController>/5
        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await _databaseService.DeletePicture(id);

        }
    }
}
