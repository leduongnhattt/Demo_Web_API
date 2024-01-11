using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using Walks.UI.Models;
using Walks.UI.Models.DTO;

namespace Walks.UI.Controllers
{
    public class RegionsController : Controller
    {
        private readonly IHttpClientFactory httpClientFactory;

        public RegionsController(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<RegionDto> respone = new List<RegionDto>();

            try
            {
                var client = httpClientFactory.CreateClient();

                var httpResponeMessage = await client.GetAsync("https://localhost:7177/api/regions");

                httpResponeMessage.EnsureSuccessStatusCode();

                 respone.AddRange(await httpResponeMessage.Content.ReadFromJsonAsync<IEnumerable<RegionDto>>());

               
            }
            catch (Exception)
            {
                //Log the exception
                
            }

            // Get All Regions from Web API
            return View(respone);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Add(AddRegionViewModel model)
        {
            var client = httpClientFactory.CreateClient();

            var httpRequestMessage = new HttpRequestMessage()
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri( "https://localhost:7177/api/regions"),
                Content = new StringContent(JsonSerializer.Serialize(model), Encoding.UTF8, "application/json"),
            };
            var httpResponeMessage = await client.SendAsync(httpRequestMessage);

            httpResponeMessage.EnsureSuccessStatusCode();

			var respone = await httpResponeMessage.Content.ReadFromJsonAsync<IEnumerable<RegionDto>>();

            if(respone is not null)
            {
                return RedirectToAction("Index", "Regions");
            }
            return View();
		}

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var client = httpClientFactory.CreateClient();

            var respone = await client.GetFromJsonAsync<RegionDto>($"https://localhost:7177/api/regions{id.ToString()}");


			if (respone is not null)
			{
                return View(respone);
			}
			return View(null); 
        }

        [HttpPut]
        public async Task<IActionResult> Edit(RegionDto request)
        {
            var client = httpClientFactory.CreateClient();

            var httpRequestMessage = new HttpRequestMessage()
            {
                Method = HttpMethod.Put,
                RequestUri = new Uri($"https://localhost:7177/api/regions/{request.Id}"),
                Content = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json"),
            };

            var httpResponeMessage = await client.SendAsync(httpRequestMessage);
            httpResponeMessage.EnsureSuccessStatusCode();

            var respone = await httpResponeMessage.Content.ReadFromJsonAsync<RegionDto>();

            if(respone is not null)
            {
                return RedirectToAction("Edit", "Regions");
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Delete(RegionDto request)
        {
            try
            {
                var client = httpClientFactory.CreateClient();

                var httpResponeMesage = await client.DeleteAsync($"https://localhost:7177/api/regions/{request.Id}");
                httpResponeMesage.EnsureSuccessStatusCode();

                return RedirectToAction("Index", "Regions");
            }
            catch (Exception)
            {
                //Console
            }

            return View("Edit");
        }
    }
}
