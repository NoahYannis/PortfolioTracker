using Newtonsoft.Json;
using PortfolioTrackerShared.Models;

namespace PortfolioTrackerServer.Services.GetStockInfoService;

public class GetStockInfoServiceConsole
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _config;

    public GetStockInfoServiceConsole(HttpClient httpClient, IConfiguration config)
    {
        httpClient = _httpClient;
        _config = config;
    }

    public string GetTickerSymbolFromUser()
    {
        string ticker;
        Console.WriteLine("Enter a stock ticker to find out its recent price. There is a 24h delay. " + "\n");
        Console.Write("Please enter a ticker symbol: ");

        do
        {
            ticker = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(ticker))
            {
                Console.Write("Ticker musn't be empty. Please enter a valid stock ticker: ");
            }
        }
        while (string.IsNullOrWhiteSpace(ticker));

        return ticker.ToUpper();
    }

    public List<decimal> GetRequestedParametersFromUser(ApiQueryStock stock)
    {
        Console.WriteLine("Enter all requested parameters. Press Enter to continue and display the data." + "\n");

        List<decimal> parameters = new List<decimal>();

        Console.WriteLine("(1) Open Price");
        Console.WriteLine("(2) Close Price");
        Console.WriteLine("(3) Lowest Price");
        Console.WriteLine("(4) Highest Price");
        Console.WriteLine("(5) Pre Market Price");
        Console.WriteLine("(6) After Hours Price" + "\n");

        bool wantsToContinue = false;

        do
        {
            ConsoleKeyInfo userInput = Console.ReadKey(true);

            switch (userInput.Key)
            {
                case ConsoleKey.D1:
                    parameters.Add(stock.Open);
                    break;
                case ConsoleKey.D2:
                    parameters.Add(stock.Close);
                    break;
                case ConsoleKey.D3:
                    parameters.Add(stock.Low);
                    break;
                case ConsoleKey.D4:
                    parameters.Add(stock.High);
                    break;
                case ConsoleKey.D5:
                    parameters.Add(stock.PreMarket);
                    break;
                case ConsoleKey.D6:
                    parameters.Add(stock.AfterHours);
                    break;
                case ConsoleKey.Enter:
                    wantsToContinue = true;
                    break;
                default:
                    break;
            }
        }
        while (!wantsToContinue);

        return parameters;
    }

    public async Task GetStockData(ApiQueryStock stock)
    {
        string apiKey = _config["AppSettings:ApiKey"];
        string date = DateTime.Now.AddHours(-24).ToString("yyyy-MM-dd");  // The free API version only delivers end of day data. A 24h delay is required.
        string ticker = GetTickerSymbolFromUser();

        string url = $"https://api.polygon.io/v1/open-close/{ticker}/{date}?adjusted=true&apiKey={apiKey}";

        HttpClient client = new HttpClient();
        HttpResponseMessage response = await client.GetAsync(url);

        if (response.IsSuccessStatusCode)
        {
            string json = await response.Content.ReadAsStringAsync();
            stock = JsonConvert.DeserializeObject<ApiQueryStock>(json);
            DisplayRequestedParameters(GetRequestedParametersFromUser(stock));
        }
        else
        {
            Console.WriteLine();
            Console.Write(response.ReasonPhrase);
            Console.ReadLine();
        }
    }

    public void DisplayRequestedParameters(List<decimal> requestedParams)
    {
        for (int i = 0; i < requestedParams?.Count; i++)
        {
            Console.WriteLine(requestedParams[i].ToString());
        }

        Console.ReadLine();
    }

}
