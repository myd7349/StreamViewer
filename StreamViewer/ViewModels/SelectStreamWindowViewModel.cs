using System;
using System.Collections.Generic;
using System.Linq;

using Avalonia.Threading;
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
        _dispatcherTimer = new DispatcherTimer(
            TimeSpan.FromSeconds(1),
            DispatcherPriority.Normal,
            TimerCallback);
    }

    [ObservableProperty]
    private IList<StreamInfo> regularStreams = Array.Empty<StreamInfo>();

    [ObservableProperty]
    private IList<StreamInfo> irregularStreams = Array.Empty<StreamInfo>();

    [ObservableProperty]
    private bool? dialogResult;

    private void TimerCallback(object? sender, EventArgs e)
    {
        var streams = _continuousResolver.Results();

        RegularStreams = streams
            .Where(stream => stream.NominalSrate != LSL.IrregularRate)
            .ToList();
        IrregularStreams = streams
            .Where(stream => stream.NominalSrate == LSL.IrregularRate)
            .ToList();
    }

    private ContinuousResolver _continuousResolver;
    private DispatcherTimer _dispatcherTimer;
}
