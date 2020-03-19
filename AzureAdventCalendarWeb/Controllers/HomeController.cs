using System;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AzureAdventCalendarWeb.Models;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace AzureAdventCalendarWeb.Controllers
{
    public class HomeController : Controller
    {
        private static string _OcpApimSubscriptionKey;
        private static string _KnowledgeBase;

        public HomeController(IOptions<CustomSettings> CustomSettings)
        {
            // Set the custom values
            _OcpApimSubscriptionKey = CustomSettings.Value.OcpApimSubscriptionKey;
            _KnowledgeBase = CustomSettings.Value.KnowledgeBase;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> FAQ(string searchString)
        {
            RootObject objQnAResult = new RootObject();
            try
            {
                if (searchString != null)
                {
                    objQnAResult = await QueryQnABot(searchString);
                }
                return View(objQnAResult);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Error: " + ex);
                return View(objQnAResult);
            }
        }

        private static async Task<RootObject> QueryQnABot(string Query)
        {
            RootObject QnAQueryResult = new RootObject();

            using (System.Net.Http.HttpClient client = new System.Net.Http.HttpClient())
            {
                string RequestURI = String.Format("{0}{1}{2}{3}{4}",
                    @"https://azureadventcalendarbot.azurewebsites.net/",
                    @"qnamaker/",
                    @"knowledgebases/",
                    _KnowledgeBase,
                    @"/generateAnswer");
                client.DefaultRequestHeaders.Add("Authorization", _OcpApimSubscriptionKey);
                var httpContent = new StringContent($"{{\"question\": \"{Query}\"}}", Encoding.UTF8, "application/json");

                System.Net.Http.HttpResponseMessage msg = await client.PostAsync(RequestURI, httpContent);

                if (msg.IsSuccessStatusCode)
                {
                    var JsonDataResponse = await msg.Content.ReadAsStringAsync();
                    QnAQueryResult = JsonConvert.DeserializeObject<RootObject>(JsonDataResponse);
                }
            }
            return QnAQueryResult;
        }

        public IActionResult About()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
