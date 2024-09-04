using System.Threading.Tasks;

using CommunityToolkit.Mvvm.DependencyInjection;
using CommunityToolkit.Mvvm.Input;
using HanumanInstitute.MvvmDialogs;

namespace StreamViewer.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    public MainWindowViewModel(IDialogService dialogService)
    {
        _dialogService = dialogService;
    }

    [RelayCommand]
    private async Task Connect()
    {
        var selectStreamWindow = Ioc.Default.GetService<SelectStreamWindowViewModel>();
        var result = await _dialogService.ShowDialogAsync(this, selectStreamWindow!);
    }

    private readonly IDialogService _dialogService;
}