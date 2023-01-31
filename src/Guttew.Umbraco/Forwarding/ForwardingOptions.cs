namespace Guttew.Umbraco.Forwarding;

[Serializable]
public class ForwardingOptions
{
    public string[] AllowedProxies { get; set; } = Array.Empty<string>();

    public string[] AllowedHosts { get; set; } = Array.Empty<string>();

    public int? ForwardLimit { get; set; } = 1;

    public bool ClearProxies { get; set; } = false;
}
