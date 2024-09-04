using HanumanInstitute.MvvmDialogs.Avalonia;

namespace StreamViewer;

public class ViewLocator : ViewLocatorBase
{
    protected override string GetViewName(object viewModel) =>
        viewModel.GetType().FullName!
        .Replace(".ViewModels.", ".Views.")
        .Replace("ViewModel", "");
}