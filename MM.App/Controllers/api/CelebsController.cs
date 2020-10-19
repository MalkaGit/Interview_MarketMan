using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MM.BL;
using MM.Common;



namespace MM.App.Controllers.api
{
    [Route("api/[controller]")]
    [ApiController]
    public class CelebsController : ControllerBase
    {
        private readonly ILogger<CelebsController> _logger;

        public CelebsController(ILogger<CelebsController> logger)
        {
            _logger = logger;
        }


        [HttpGet]
        public IActionResult  Get()
        {
            try
            {
                var celebsService   = new CelebsService();
                var celebs          = celebsService.GetAll();

                return StatusCode(StatusCodes.Status200OK, celebs);
            }
            catch (Exception e)
            {
                _logger.LogError("{0}", e.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }



        // DELETE /api/Celebs/4
        [Route("{id:int}")]
        [HttpDelete]
        public IActionResult Delete(int id)
        {

            try
            {
                var celebsService   = new CelebsService();
                var found           = celebsService.Delete(id);
                var statusCode      = (found) ? StatusCodes.Status200OK : StatusCodes.Status404NotFound;

                return StatusCode(statusCode); 
            }
            catch (Exception e)
            {
                _logger.LogError("{0}", e.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }


        // POST /api/Celebs
        [HttpPost]
        public IActionResult Post()
        {
            try
            {
                CelebsService.ResetData();
                return StatusCode(StatusCodes.Status200OK);
            }
            catch (Exception e)
            {
                _logger.LogError("{0}", e.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }


    }
}