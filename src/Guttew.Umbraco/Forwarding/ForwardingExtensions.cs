using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using IPAddress = System.Net.IPAddress;

namespace Guttew.Umbraco.Forwarding;

public static class ForwardingExtensions
{
    /// <summary>
    /// Configures <see cref="ForwardedHeadersOptions"/> from appsettings "Forwarding" key.
    /// The format of the appsettings options cna be seen in <seealso cref="ForwardingOptions"/>.
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    public static void AddForwardingOptions(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<ForwardingOptions>(configuration.GetSection("Forwarding"));

        services.AddOptions<ForwardedHeadersOptions>()
            .Configure<IOptions<ForwardingOptions>>((options, forwardOptions) =>
            {
                var forwardingOptions = forwardOptions.Value;

                options.ForwardedHeaders = ForwardedHeaders.All;
                options.ForwardLimit = forwardingOptions.ForwardLimit;

                // Set allowed hosts if defined
                if (forwardingOptions.AllowedHosts.Any())
                    options.AllowedHosts = forwardingOptions.AllowedHosts.ToList();

                if (forwardingOptions.ClearProxies)
                {
                    options.KnownProxies.Clear();
                    options.KnownNetworks.Clear();
                }

                foreach (var network in forwardingOptions.AllowedProxies)
                {
                    if (network.Contains('/'))
                        options.KnownNetworks.Add(ParseNetwork(network));
                    else
                        options.KnownProxies.Add(ParseProxy(network));
                }
            });
    }

    /// <summary>
    /// Parses a network address with a CIDR notation like "192.168.1.0/24"
    /// </summary>
    /// <exception cref="InvalidOperationException"></exception>
    private static IPNetwork ParseNetwork(string network)
    {
        var split = network.Split('/', 3);

        if (!IPAddress.TryParse(split[0], out var address))
            throw new InvalidOperationException($"Forwarding: Invalid proxy address '{split[0]}'!");

        if (!int.TryParse(split[1], out var cidr))
            throw new InvalidOperationException($"Forwarding: Invalid proxy CIDR '{split[1]}'!");

        return new IPNetwork(address, cidr);
    }

    /// <summary>
    /// Parses a network address like "192.168.1.0"
    /// </summary>
    /// <exception cref="InvalidOperationException"></exception>
    private static IPAddress ParseProxy(string network)
    {
        if (!IPAddress.TryParse(network, out var address))
            throw new InvalidOperationException($"Forwarding: Invalid proxy address '{network}'!");

        return address;
    }
}
