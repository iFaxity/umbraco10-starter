namespace Guttew.Umbraco.Icon;

public class IconViewModel
{
    public string? Class { get; set; }
    public string? Id { get; set; }
    public string? Title { get; set; }

    public IconViewModel(string? className = null, string? id = null, string? title = null)
    {
        Class = string.IsNullOrWhiteSpace(className) ? null : className.Trim();
        Id = string.IsNullOrWhiteSpace(id) ? null : id.Trim();
        Title = string.IsNullOrWhiteSpace(title) ? null : title.Trim();
    }
}
