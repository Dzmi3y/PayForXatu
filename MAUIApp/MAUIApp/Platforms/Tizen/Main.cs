using Microsoft.Maui;
using Microsoft.Maui.Hosting;
using System;

namespace MAUIApp
{
    public class Program : MauiApplication
    {
        protected override MauiApp CreatePrismApp() => MauiProgram.CreatePrismApp();

        static void Main(string[] args)
        {
            var app = new Program();
            app.Run(args);
        }
    }
}