using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System.Xml;
using Windows.Storage;
using Windows.Storage.Pickers;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace ThemeTuner {
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class EditorPage : Page {
        EditorViewModel ViewModel = new EditorViewModel();
        public EditorPage() {
            this.InitializeComponent();
            ViewModel.OpenFileCommand.Subscribe(async () => {
                try {
                    var window = ((App)Application.Current).Window;
                    var hwnd = WinRT.Interop.WindowNative.GetWindowHandle(window);
                    var picker = new Windows.Storage.Pickers.FileOpenPicker();
                    picker.FileTypeFilter.Add(".xml");
                    picker.FileTypeFilter.Add(".zip");
                    WinRT.Interop.InitializeWithWindow.Initialize(picker, hwnd);
                    var file = await picker.PickSingleFileAsync();
                    if (file != null) {
                        //ViewModel.InputFile.Value = file;
                        var themeSet = AndroidColorThemeSet.Builder.AppendAuto(file).Build();
                        ViewModel.ThemeSet.Value = themeSet;
                    }
                }
                catch (Exception e) {
                    Console.WriteLine(e);
                }
            });
        }
    }
}
