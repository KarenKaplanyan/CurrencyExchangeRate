using System.Globalization;
using System.Net.Http.Headers;
using System.Text;
using System.Xml.Serialization;
using CurrencyExchangeRate.MainApp.Infrastructure.Dto;

namespace CurrencyExchangeRate.MainApp.Infrastructure.Services;

public class XmlService: IXmlService
{
    public async Task<ValCurs> GetDataFromCbr(CancellationToken cancellationToken)
    {
        Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        var httpClient = new HttpClient();
        httpClient.Timeout = new TimeSpan(0, 0, 30);
        httpClient.DefaultRequestHeaders.Clear();
        httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("text/xml"));
        using var response = await httpClient.GetAsync(new Uri("http://www.cbr.ru/scripts/XML_daily.asp"), cancellationToken);
        response.EnsureSuccessStatusCode();
        var stream = await response.Content.ReadAsStreamAsync(cancellationToken);
        CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("ru-RU");
        var serializer = new XmlSerializer(typeof(ValCurs));
        return (ValCurs)serializer.Deserialize(stream)!;
    }
}