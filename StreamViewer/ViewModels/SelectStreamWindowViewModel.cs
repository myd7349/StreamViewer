using CommunityToolkit.Mvvm.ComponentModel;
using HanumanInstitute.MvvmDialogs;
using SharpLSL;

namespace StreamViewer.ViewModels;

internal partial class SelectStreamWindowViewModel
    : ViewModelBase, IModalDialogViewModel
{
    public SelectStreamWindowViewModel()
    {
        _continuousResolver = new ContinuousResolver();
    }

    [ObservableProperty]
    private bool? dialogResult;

    private ContinuousResolver _continuousResolver;
}
