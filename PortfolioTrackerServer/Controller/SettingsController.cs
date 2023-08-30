using Microsoft.AspNetCore.Mvc;

namespace PortfolioTrackerServer.Controller
{
	[Route("api/[controller]")]
	[ApiController]
	public class SettingsController : ControllerBase
	{
		// GET: api/<SettingsController>
		[HttpGet]
		public IEnumerable<string> Get()
		{
			return new string[] { "value1", "value2" };
		}

		// GET api/<SettingsController>/5
		[HttpGet("{id}")]
		public string Get(int id)
		{
			return "value";
		}

		// POST api/<SettingsController>
		[HttpPost]
		public void Post([FromBody] string value)
		{
		}

		// PUT api/<SettingsController>/5
		[HttpPut("{id}")]
		public void Put(int id, [FromBody] string value)
		{
		}

		// DELETE api/<SettingsController>/5
		[HttpDelete("{id}")]
		public void Delete(int id)
		{
		}
	}
}
