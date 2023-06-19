using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Extensions.Configuration;

namespace ModificadorCC;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    
    public MainWindow()
    {
        InitializeComponent();
    }


    private async void ButtonBase_OnClick(object sender, RoutedEventArgs e)
    {
        string contenidoTextBox = NumeroTpTxt.Text;
        await CallApi(contenidoTextBox);
    }

    private static async Task<Stream> CallApi(string idTp)
    {
        try
        {
            var stackTrace = new StackTrace(new StackFrame(true));
            var directoryName = Path.GetDirectoryName(stackTrace.GetFrame(0)?.GetFileName());

            var builder = new ConfigurationBuilder()
                .SetBasePath(directoryName!)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            var configuration = builder.Build();
            var appSettings = new AppSettings();
            configuration.GetSection("Targetprocess").Bind(appSettings);

            var url = appSettings.url;
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(url!),
                Content = new FormUrlEncodedContent(new Dictionary<string, string>()
                {
                    { "access_token", appSettings.token },
                    { "format", "json" },
                    { "where", $"(Id eq {idTp})" }
                })
            };
            using var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            var body = await response.Content.ReadAsStringAsync();
            Console.WriteLine(body);

            return null;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}