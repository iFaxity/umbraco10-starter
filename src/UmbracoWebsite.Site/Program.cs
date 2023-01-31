using UmbracoWebsite.Site;

var builder = Host.CreateDefaultBuilder()
    .ConfigureUmbracoDefaults()
    .ConfigureWebHostDefaults(webBuilder =>
    {
        webBuilder.UseStaticWebAssets();
        webBuilder.UseStartup<Startup>();
    })
    .ConfigureAppConfiguration((ctx, builder) =>
    {
        var env = ctx.HostingEnvironment;

        // Load both appsettings and environment specific appsettings files
        builder
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true);

#if DEBUG
        builder.AddJsonFile("appsettings.Local.json", optional: true, reloadOnChange: true);
#endif
    });

var host = builder.Build();
host.Run();
