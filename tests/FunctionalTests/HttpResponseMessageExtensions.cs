using AngleSharp.Html.Dom;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Forma1Teams.FunctionalTests
{
    public static class HttpResponseMessageExtensions
    {
        public static async Task<IHtmlDocument> GetDocumentAsync(this HttpResponseMessage response)
        {
            return await HtmlHelpers.GetDocumentAsync(response);
        }
        public static async Task<string> GetRequestVerificationToken(this HttpResponseMessage response)
        {
            var content = await response.Content.ReadAsStringAsync();
            string regexpression = @"name=""__RequestVerificationToken"" type=""hidden"" value=""([-A-Za-z0-9+=/\\_]+?)""";
            var regex = new Regex(regexpression);
            var match = regex.Match(content);
            return match.Groups.Values.LastOrDefault().Value;
        }
    }
}
