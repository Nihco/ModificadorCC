using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ModificadorCC;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    static HttpClient _client = new HttpClient();

    public MainWindow()
    {
        InitializeComponent();
    }


    private async void ButtonBase_OnClick(object sender, RoutedEventArgs e)
    {
        string contenidoTextBox = NumeroTpTxt.Text;
        await CallApi();
    }

    private static async Task<Stream> CallApi()
    {
        try
        {
            _client.BaseAddress = new Uri("https://restapi.tpondemand.com/api/v1/");
            const string token = "MTAyMjMxOkdoT2xFMHBNWnIxR0JmWTUyNmpUMFdrVWMrM0NEelJIR3VIbEh4KzNYTlk9=";
           
            var response = await _client.GetAsync(
                $"https://restapi.tpondemand.com/api/v1/UserStories/?access_token={token}");
            response.EnsureSuccessStatusCode();
            var responseBody = await response.Content.ReadAsStreamAsync();
            return responseBody;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}