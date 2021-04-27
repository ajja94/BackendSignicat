using Idfy;
using Idfy.IdentificationV2;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace SignicatOppgave
{
    class Program
    {
        static async Task Main(string[] args)
        {
            //laget egen classe med cliens 
            var clientInfo = new Clients();

            var client = new IdentificationV2Service(clientInfo.GetClientId(), clientInfo.GetClientSecret(),
                new List<OAuthScope>()
                {
                    OAuthScope.Identify
                }) ;

            var session = await client.CreateSessionAsync(new IdSessionCreateOptions()
            //valg av hvilken tyåe inn logging du ønsker, bankId/sms osv
            {
                AllowedProviders = new List<IdProviderType>()
            {
                 IdProviderType.NoBankidMobile,
                 IdProviderType.NoBankidNetcentric,
                 IdProviderType.SmsOtp
                
            },
            //redirect Url'er med success/error og abort 
            // success er satt til en hjemmelaget html front end med kun success status på V-1 
            RedirectSettings = new RedirectSettings()
            {
                ErrorUrl = "https://www.signicat.com#error/",
                AbortUrl = "https://www.signicat.com#abort/",
                SuccessUrl = "https://ajja94.github.io/SignicatFrontend/ClientApp/src/app/home/home.component.html"
            },
            //valg av språk og 
            ExternalReference = Guid.NewGuid().ToString("n"),
            Flow = IdSessionFlow.Redirect,
            Language = Language.No,
            Include = new List<Include>()
            {
                Include.Nin,
                Include.Name,
            },
            //styiling
            Ui = new UiSettings()
            {
                ColorTheme = ColorTheme.Default,
                ThemeMode = ThemeMode.Dark
            }
            });

            OpenUrl(session.Url);
            Console.ReadLine();

            var sessionCompleted = await client.GetSessionAsync(session.Id);
            Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(sessionCompleted, Newtonsoft.Json.Formatting.Indented));
            Console.ReadLine();
        }

        private static void OpenUrl(string url)
        {
            try
            {
                Process.Start(url);
            }
            catch
            {
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                {
                    url = url.Replace("&", "^&");
                    Process.Start(new ProcessStartInfo("cmd", $"/c start {url}") { CreateNoWindow = true }); 

                }
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                {
                    Process.Start("xdg-open", url);
                }
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
                {
                    Process.Start("open", url);
                }
                else
                {
                    throw;
                }
            }
        }
    }
}
