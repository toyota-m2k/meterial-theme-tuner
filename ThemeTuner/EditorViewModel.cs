using Reactive.Bindings;
using System;
using System.Linq;
using System.Reactive.Linq;
using Windows.Storage;

namespace ThemeTuner;

class EditorViewModel {
    public EditHistory History { get; } = new();

    //public ReactiveProperty<StorageFile?> InputFile { get; } = new();
    public ReactiveCommand OpenFileCommand { get; } = new();

    //public ReactiveProperty<AndroidColors?> Colors { get; } = new();
    public ReactiveProperty<AndroidColorThemeSet> ThemeSet { get; } = new();

    public ReactiveProperty<ColorContrast> Contrast { get; } = new(ColorContrast.Normal);
    public ReactiveProperty<DayNightMode> DayNight { get; } = new(DayNightMode.Light);
    public ReadOnlyReactiveProperty<AndroidColorTheme?> Theme { get; }
    public ReadOnlyReactiveProperty<bool> IsThemeAvailable { get; }
    //public ReactiveProperty<bool> IsThemeAvailable = new(false);

    public void OnColorChanged() {
        History.Push(DayNight.Value, Contrast.Value, Theme.Value!.SettingColors);
    }

    public void LoadTheme(StorageFile file) {
        try {         
            var themeSet = AndroidColorThemeSet.Builder.AppendAuto(file).Build();
            ThemeSet.Value = themeSet;
            History.Reset(DayNight.Value, Contrast.Value, Theme.Value!.SettingColors);
        } catch(Exception e) {
            Console.WriteLine(e);
        }
    }

    public ReactiveCommand UndoCommand { get; } = new();
    public ReactiveCommand RedoCommand { get; } = new();

    public EditorViewModel() {
        Theme = ThemeSet.CombineLatest(DayNight, Contrast, (themeSet, daynight, contrast) => themeSet?.ThemeOf(daynight, contrast)).ToReadOnlyReactiveProperty();
        IsThemeAvailable = Theme.Select(it => it != null).ToReadOnlyReactiveProperty();
        UndoCommand.Subscribe(() => {
            var entry = History.Back();
            if (entry != null) {
                Contrast.Value = entry.Contrast;
                DayNight.Value = entry.DayNight;
                Theme.Value!.SettingColors = entry.Colors;
            }
        });
        RedoCommand.Subscribe(() => {
            var entry = History.Forward();
            if (entry != null) {
                Contrast.Value = entry.Contrast;
                DayNight.Value = entry.DayNight;
                Theme.Value!.SettingColors = entry.Colors;
            }
        });
    }
}
