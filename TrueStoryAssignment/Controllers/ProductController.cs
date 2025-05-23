using Microsoft.AspNetCore.Mvc;
using TrueStoryAssignment.Models;

namespace TrueStoryAssignment.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class ProductController : ControllerBase
	{
		private readonly HttpClient _httpClient;
		private const string baseUrl = "https://api.restful-api.dev/objects";

		public ProductController(IHttpClientFactory httpClientFactory)
		{
			_httpClient = httpClientFactory.CreateClient();
		}

		// GET: api/Product?nameFilter=iphone&page=1&pageSize=10
		[HttpGet]
		public async Task<IActionResult> Get(string? nameFilter, int page = 1, int pageSize = 10)
		{
			try
			{
				var response = await _httpClient.GetFromJsonAsync<List<ProductResponse>>(baseUrl);
				if (response == null) return NotFound();

				var filtered = response
					.Where(p => string.IsNullOrEmpty(nameFilter) || p.Name.Contains(nameFilter, StringComparison.OrdinalIgnoreCase))
					.Skip((page - 1) * pageSize)
					.Take(pageSize)
					.ToList();

				return Ok(filtered);
			}
			catch (Exception ex)
			{
				return StatusCode(500, $"Internal server error: {ex.Message}");
			}
		}

		// POST: api/Product
		[HttpPost]
		public async Task<IActionResult> Post([FromBody] Product product)
		{
			if (string.IsNullOrWhiteSpace(product.Name))
				return BadRequest("Product name is required.");

			try
			{
				var requestBody = new
				{
					name = product.Name,
					data = product.Data
				};

				var result = await _httpClient.PostAsJsonAsync(baseUrl, requestBody);
				result.EnsureSuccessStatusCode();
				return Created(string.Empty, await result.Content.ReadFromJsonAsync<ProductResponse>());
			}
			catch (Exception ex)
			{
				return StatusCode(500, $"Internal server error: {ex.Message}");
			}
		}

		// DELETE: api/Product/{id}
		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete(string id)
		{
			try
			{
				var result = await _httpClient.DeleteAsync($"{baseUrl}/{id}");
				if (!result.IsSuccessStatusCode)
					return NotFound();

				return NoContent();
			}
			catch (Exception ex)
			{
				return StatusCode(500, $"Internal server error: {ex.Message}");
			}
		}


		//Bonus: http-patch
		[HttpPatch("{id}")]
		public async Task<IActionResult> Patch(string id, [FromBody] Product product)
		{
			try
			{
				var requestBody = new { name = product.Name, data = product.Data };
				var patchRequest = new HttpRequestMessage(HttpMethod.Patch, $"{baseUrl}/{id}")
				{
					Content = JsonContent.Create(requestBody)
				};
				var result = await _httpClient.SendAsync(patchRequest);
				if (!result.IsSuccessStatusCode)
					return NotFound();

				return Ok(await result.Content.ReadFromJsonAsync<ProductResponse>());
			}
			catch (Exception ex)
			{
				return StatusCode(500, $"Internal server error: {ex.Message}");
			}
		}
	}
}
