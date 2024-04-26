using GeoApi.Domain.Exceptions;
using GeoApi.Domain.Interfaces.Services;
using GeoApi.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace GeoApi.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CountriesController : ControllerBase
    {
        private readonly ICountriesService _countriesService;

        public CountriesController(ICountriesService countriesService)
        {
            _countriesService = countriesService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                return Ok(await _countriesService.GetAll());
            }
            catch (ErrorException ex)
            {
                return BadRequest(ErrorCodeModel.Create(ex.Code, ex.Message));
            }
        }
    }
}