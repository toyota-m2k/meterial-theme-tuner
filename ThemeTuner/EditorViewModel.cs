using Reactive.Bindings;
using System.Linq;
using System.Reactive.Linq;

namespace ThemeTuner;

class EditorViewModel {
    //public ReactiveProperty<StorageFile?> InputFile { get; } = new();
    public ReactiveCommand OpenFileCommand { get; } = new();

    //public ReactiveProperty<AndroidColors?> Colors { get; } = new();
    public ReactiveProperty<AndroidColorThemeSet> ThemeSet { get; } = new();

    public ReactiveProperty<ColorContrast> Contrast { get; } = new(ColorContrast.Normal);
    public ReactiveProperty<DayNightMode> DayNight { get; } = new(DayNightMode.Light);
    public ReadOnlyReactiveProperty<AndroidColorTheme?> Theme { get; }
    public ReadOnlyReactiveProperty<bool> IsThemeAvailable { get; }
    //public ReactiveProperty<bool> IsThemeAvailable = new(false);

    public EditorViewModel() {
        Theme = ThemeSet.CombineLatest(DayNight, Contrast, (themeSet, daynight, contrast) => themeSet?.ThemeOf(daynight, contrast)).ToReadOnlyReactiveProperty();
        IsThemeAvailable = Theme.Select(it => it != null).ToReadOnlyReactiveProperty();
    }
}
