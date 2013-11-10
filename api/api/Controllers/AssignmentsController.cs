using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Mvc;
using HtmlAgilityPack;

namespace api.Controllers
{
    public class AssignmentsController : Controller
    {
        
        public async Task<JsonResult> GetAssignmentHtml(string url)
        {
            if(string.IsNullOrEmpty(url))
                return Json(new { error = "Error. Empty Url." });

            var response = await GetWebPageAsync(url);
            var node = "//div[@id='sw-content-layout-wrapper']";
            var assignmentNode = GetDocumentNode(response, node);

            var model = new AssignmentViewModel();
            model.Url = url;
            model.InnerHtml = assignmentNode.InnerHtml;

            return Json(model, JsonRequestBehavior.AllowGet);
        }

        public string GetWebsiteHtml(string url)
        {
            if (string.IsNullOrEmpty(url))
                return string.Empty;
            using (var stream = GetWebsiteHtmlStream(url))
            {
                if (stream != null)
                {
                    using (var reader = new StreamReader(stream))
                    {
                        string result = reader.ReadToEnd();
                        stream.Dispose();
                        reader.Dispose();
                        return result;   
                    }
                }
            }
            return string.Empty;
        }

        public Stream GetWebsiteHtmlStream(string url)
        {
            if (!string.IsNullOrEmpty(url))
            {
                WebRequest request = WebRequest.Create(url);
                WebResponse response = request.GetResponse();
                Stream stream = response.GetResponseStream();
                if (stream != null)
                {
                    return stream;
                }
            }
            return null;
        }

        public async Task<HttpResponseMessage> GetWebPageAsync(string url)
        {
            var client = new HttpClient();
            Uri uri = new Uri(url);
            var result = await client.GetAsync(url);
            return result;
        }

        public async Task<HtmlAgilityPack.HtmlDocument> GetDocumentAsync(HttpResponseMessage responseMessage)
        {
            string result = await responseMessage.Content.ReadAsStringAsync();
            if (!responseMessage.IsSuccessStatusCode)
                throw new FileNotFoundException("Unable to retrieve document");

            var document = new HtmlAgilityPack.HtmlDocument();
            document.LoadHtml(result);
            return document;
        }

        public HtmlNode GetDocumentNode(HttpResponseMessage responseMessage, string node)
        {
            var document = GetDocumentAsync(responseMessage);
            return document.Result.DocumentNode.SelectSingleNode(node);
        }
    }

    public class AssignmentViewModel
    {
        public string Url { get; set; }

        public string InnerHtml { get; set; }
    }
}