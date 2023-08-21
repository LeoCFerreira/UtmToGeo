//using AngleSharp;
//using AngleSharp.Dom;
//using AngleSharp.Html.Dom;
//using AngleSharp.Io;
//using AngleSharp.Js;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace ConsoleUI
//{
//    public static class CoordinatesConverter
//    {
//        public static async Task ConvertAsync()
//        {
            
//            var config = Configuration.Default
//                .WithDefaultCookies()
//                .WithJs()
//                .WithDefaultLoader(new LoaderOptions
//                {
//                    IsNavigationDisabled = false,
//                    IsResourceLoadingEnabled = true,
//                }) ;

//            var context = BrowsingContext.New(config);

//            var doc = await context
//                .OpenAsync("https://sigam.ambiente.sp.gov.br/sigam3/Controles/latlongutm.htm?latTxt=ctl00_con")
//                .WaitUntilAvailable();

//            //var scripts = doc.Scripts[0];

//            var form = doc.GetElementsByName("frmConverter")
//                .FirstOrDefault()
//                as IHtmlFormElement;
            
//            var utmButton = doc.QuerySelector("body > table > tbody > tr:nth-child(2) > td > input[type=radio]:nth-child(1)")
//                as IHtmlInputElement;

//            utmButton.IsChecked = true;

//            utmButton.DoClick();

//            await form.SubmitAsync();

//            var xElement = doc.GetElementsByName("txtX")
//                .FirstOrDefault() as IHtmlInputElement;

//            var x = "225199,560";
            
//            xElement.Value = x;

//            var yElement = doc.GetElementsByName("txtY")
//                .FirstOrDefault() as IHtmlInputElement;
            
//            var y = "9390621,965";

//            yElement.Value = y;

//            var timeZoneElement = doc.GetElementsByName("txtZone")
//                .FirstOrDefault();

//            timeZoneElement.SetAttribute("value", "23");

//            var hemisphereButtonElement = doc.QuerySelector("#tbUTMparaGeo > tbody > tr:nth-child(5) > td:nth-child(1) > input[type=radio]:nth-child(2)")
//                as IHtmlInputElement;

//            hemisphereButtonElement.IsChecked = true;

//            var btnConvert = doc.GetElementsByName("btnToUTM")
//                .FirstOrDefault() as IHtmlElement;

//            btnConvert.Clicked += BtnConvert_Clicked;

//            btnConvert.DoClick();

//            await Task.Delay(5000);

//            var longitudeTextboxElement = doc.GetElementsByName("txtLongitude")
//                .FirstOrDefault() as IHtmlInputElement;
            
//            var longitude = longitudeTextboxElement.TextContent;
            
//            longitude = longitudeTextboxElement.Value;

//            var latitudeTextboxElement = doc.QuerySelector("#tbUTMparaGeo > tbody > tr:nth-child(3) > td:nth-child(7) > input[type=text]")
//                as IHtmlInputElement;
            
//            var latitude = latitudeTextboxElement.Value;

//            var longitudeTxt = context.Active.All
//                .Where(e => e.LocalName == "input")
//                .Cast<IHtmlInputElement>()
//                .Where(input => input.Name == "txtLongitute");
//        }

//        private static void BtnConvert_Clicked(object sender, AngleSharp.Dom.Events.Event ev)
//        {
//            Console.WriteLine("button clicked");
//        }
//    }
//}
