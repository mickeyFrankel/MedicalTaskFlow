using System.Windows;
using MedicalTaskFlow.Desktop.ViewModels;

namespace MedicalTaskFlow.Desktop;

/// <summary>
/// Main applaicatinon window.
/// Code-behind is minimal - logic lives in ViewModel.
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow(MainViewModel viewModel)
    {        
        InitializeComponent();
        DataContext = viewModel;
    }
}