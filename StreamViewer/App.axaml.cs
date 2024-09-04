using System;

using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Data.Core.Plugins;
using Avalonia.Markup.Xaml;
using CommunityToolkit.Mvvm.DependencyInjection;
using HanumanInstitute.MvvmDialogs;
using HanumanInstitute.MvvmDialogs.Avalonia;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using StreamViewer.ViewModels;

namespace StreamViewer;

public partial class App : Application
{
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);

        ConfigureServices();

        var serviceProvider = ConfigureServices();

        Ioc.Default.ConfigureServices(serviceProvider);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            // Line below is needed to remove Avalonia data validation.
            // Without this line you will get duplicate validations from both Avalonia and CT
            BindingPlugins.DataValidators.RemoveAt(0);

            var dialogService = Ioc.Default.GetService<IDialogService>();
            var mainWindow = Ioc.Default.GetService<MainWindowViewModel>();
            dialogService!.Show(null, mainWindow!);
        }

        base.OnFrameworkInitializationCompleted();
    }

    private IServiceProvider ConfigureServices()
    {
        var services = new ServiceCollection();

        var loggerFactory = LoggerFactory.Create(
            builder => builder.AddFilter(logLevel => true));

        services.AddSingleton<IDialogService>(
            new DialogService(
                new DialogManager(
                    viewLocator: new ViewLocator(),
                    logger: loggerFactory.CreateLogger<DialogManager>()),
                    viewModelFactory: x => Ioc.Default.GetService(x)));

        services.AddTransient<MainWindowViewModel>();
        services.AddTransient<SelectStreamWindowViewModel>();

        return services.BuildServiceProvider();
    }
}

// References:
// https://github.com/mysteryx93/HanumanInstitute.MvvmDialogs/tree/master/samples/Avalonia/Demo.ModalDialog
// [Ioc (Inversion of control)](https://learn.microsoft.com/en-us/dotnet/communitytoolkit/mvvm/ioc)
// https://github.com/CommunityToolkit/MVVM-Samples
// https://github.com/CommunityToolkit/MVVM-Samples/blob/master/samples/MvvmSampleXF/MvvmSampleXF/App.xaml.cs
// https://github.com/CommunityToolkit/dotnet/blob/main/src/CommunityToolkit.Mvvm/DependencyInjection/Ioc.cs
