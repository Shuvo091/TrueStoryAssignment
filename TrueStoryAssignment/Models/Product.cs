using System.Text.Json;
namespace TrueStoryAssignment.Models
{
	public class Product
	{
		public string Name { get; set; } = string.Empty;
		public JsonElement? Data { get; set; }
	}

	public class ProductResponse
	{
		public string Id { get; set; } = string.Empty;
		public string Name { get; set; } = string.Empty;
		public JsonElement? Data { get; set; }
	}
}
