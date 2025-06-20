namespace App.Integrations.Steam;

public static class SteamModule
{
    public static T AddSteamApiClient<T>(this T builder) where T : IHostApplicationBuilder
    {
        builder.Services.Configure<SteamOptions>(o =>
        {
            o.ApiKey = builder.Configuration["steam-api-key"];
            o.SteamId = builder.Configuration["steam-id"];
        });
        
        builder.Services.AddHttpClient<ISteamApiClient, SteamApiClient>(client =>
        {
            client.BaseAddress = new Uri("https://api.steampowered.com");
        });

        return builder;
    }
}