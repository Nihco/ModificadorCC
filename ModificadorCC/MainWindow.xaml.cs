using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using HtmlAgilityPack;
using Microsoft.Extensions.Configuration;
using ModificadorCC.Model;
using ModificadorCC.Service;
using Newtonsoft.Json;

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

    private static async Task<Root> CallApi(string idTp)
    {
        try
        {
            if (string.IsNullOrEmpty(idTp)) return new Root();
            var stackTrace = new StackTrace(new StackFrame(true));
            var directoryName = Path.GetDirectoryName(stackTrace.GetFrame(0)?.GetFileName());

            var builder = new ConfigurationBuilder()
                .SetBasePath(directoryName!)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            var configuration = builder.Build();
            var appSettings = new AppSettings();
            configuration.GetSection("Targetprocess").Bind(appSettings);

            var url = appSettings.url+"Assignables";
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(url!),
                Content = new FormUrlEncodedContent(new Dictionary<string, string>()
                {
                    { "access_token", appSettings.token },
                    { "format", "json" },
                    {"include","[Name,Description,CustomFields,Assignments[GeneralUser,Role]]"},
                    { "where", $"(Id eq {idTp})" }
                })
            };
            using var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            var body = await response.Content.ReadAsStringAsync();

            var tpModel = JsonConvert.DeserializeObject<Root>(body);
            foreach (var tp in tpModel.Items)
            {
                tp.Description = ConvertHtmlToPlainText(tp.Description);
            }
            Excel.Edit(tpModel);
            return tpModel;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
        
    }
    private static string ConvertHtmlToPlainText(string htmlDescription)
    {
        var htmlDoc = new HtmlDocument();
        htmlDoc.LoadHtml(htmlDescription);
        var text = htmlDoc.DocumentNode.InnerText;
        text = HtmlEntity.DeEntitize(text);
        text = Regex.Replace(text, @"^\s+|\s+$", "", RegexOptions.Multiline);
        text = Regex.Replace(text, @"[\r\n]{3,}", "\r\n\r\n");
        text = Regex.Replace(text, @" {2,}", " ");
        text = text.Replace("\n", "");
        text = text.Replace("\u200C", "");
        return text;
    }
}