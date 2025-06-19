namespace App.Integrations.Steam;

public static class SteamModule
{
    public const string ConfigurationSectionName = "Steam";

    public static T AddSteamApiClient<T>(this T builder, string configurationSectionName = ConfigurationSectionName)
        where T : IHostApplicationBuilder
    {
        builder.Services.Configure<SteamOptions>(builder.Configuration.GetSection(configurationSectionName));
        
        builder.Services.AddHttpClient<ISteamApiClient, SteamApiClient>(client =>
        {
            client.BaseAddress = new Uri("https://api.steampowered.com");
        });

        return builder;
    }
}