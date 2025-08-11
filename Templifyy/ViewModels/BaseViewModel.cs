namespace Templifyy.ViewModels;

public abstract class BaseViewModel
{
    public string? Title { get; set; }
    public string? Message { get; set; }
    public bool HasError { get; set; }
    public List<string> Errors { get; set; } = new();
}

public class ErrorViewModel : BaseViewModel
{
    public string? RequestId { get; set; }
    public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
}
