using Reactive.Bindings;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TrayNotify;
using Windows.Media.Ocr;
using System.Drawing;

namespace ThemeTuner;

public class AndroidColorTheme {
    // primary
    private ReactiveProperty<NamedColor> primary { get; } = new(NamedColor.Empty);
    private ReactiveProperty<NamedColor> primaryContainer { get; } = new(NamedColor.Empty);
    private ReactiveProperty<NamedColor> inversePrimary { get; } = new(NamedColor.Empty);
    private ReactiveProperty<NamedColor> primaryFixed { get; } = new(NamedColor.Empty);
    private ReactiveProperty<NamedColor> primaryFixedDim { get; } = new(NamedColor.Empty);
    private ReactiveProperty<NamedColor> onPrimary { get; } = new(NamedColor.Empty);
    private ReactiveProperty<NamedColor> onPrimaryContainer { get; } = new(NamedColor.Empty);
    private ReactiveProperty<NamedColor> onPrimaryFixed { get; } = new(NamedColor.Empty);
    private ReactiveProperty<NamedColor> onPrimaryFixedVariant { get; } = new(NamedColor.Empty);

    //Secondary --

    private ReactiveProperty<NamedColor> secondary { get; } = new(NamedColor.Empty);
    private ReactiveProperty<NamedColor> secondaryContainer { get; } = new(NamedColor.Empty);
    private ReactiveProperty<NamedColor> secondaryFixed { get; } = new(NamedColor.Empty);
    private ReactiveProperty<NamedColor> secondaryFixedDim { get; } = new(NamedColor.Empty);
    private ReactiveProperty<NamedColor> onSecondary { get; } = new(NamedColor.Empty);
    private ReactiveProperty<NamedColor> onSecondaryContainer { get; } = new(NamedColor.Empty);
    private ReactiveProperty<NamedColor> onSecondaryFixed { get; } = new(NamedColor.Empty);
    private ReactiveProperty<NamedColor> onSecondaryFixedVariant { get; } = new(NamedColor.Empty);


    //Tertiary --
    private ReactiveProperty<NamedColor> tertiary { get; } = new(NamedColor.Empty);
    private ReactiveProperty<NamedColor> tertiaryContainer { get; } = new(NamedColor.Empty);
    private ReactiveProperty<NamedColor> tertiaryFixed { get; } = new(NamedColor.Empty);
    private ReactiveProperty<NamedColor> tertiaryFixedDim { get; } = new(NamedColor.Empty);
    private ReactiveProperty<NamedColor> onTertiary { get; } = new(NamedColor.Empty);
    private ReactiveProperty<NamedColor> onTertiaryContainer { get; } = new(NamedColor.Empty);
    private ReactiveProperty<NamedColor> onTertiaryFixed { get; } = new(NamedColor.Empty);
    private ReactiveProperty<NamedColor> onTertiaryFixedVariant { get; } = new(NamedColor.Empty);

    //Surface --
    private ReactiveProperty<NamedColor> surface { get; } = new(NamedColor.Empty);
    private ReactiveProperty<NamedColor> surfaceVariant { get; } = new(NamedColor.Empty);
    private ReactiveProperty<NamedColor> inverseSurface { get; } = new(NamedColor.Empty);
    private ReactiveProperty<NamedColor> inverseOnSurface { get; } = new(NamedColor.Empty);
    private ReactiveProperty<NamedColor> surfaceDim { get; } = new(NamedColor.Empty);
    private ReactiveProperty<NamedColor> surfaceBright { get; } = new(NamedColor.Empty);
    private ReactiveProperty<NamedColor> surfaceContainerLowest { get; } = new(NamedColor.Empty);
    private ReactiveProperty<NamedColor> surfaceContainerLow { get; } = new(NamedColor.Empty);
    private ReactiveProperty<NamedColor> surfaceContainer { get; } = new(NamedColor.Empty);
    private ReactiveProperty<NamedColor> surfaceContainerHigh { get; } = new(NamedColor.Empty);
    private ReactiveProperty<NamedColor> surfaceContainerHighest { get; } = new(NamedColor.Empty);
    private ReactiveProperty<NamedColor> onSurface { get; } = new(NamedColor.Empty);
    private ReactiveProperty<NamedColor> onSurfaceVariant { get; } = new(NamedColor.Empty);

    // Ohers ... Error and Scrim --
    private ReactiveProperty<NamedColor> error { get; } = new(NamedColor.Empty);
    private ReactiveProperty<NamedColor> onError { get; } = new(NamedColor.Empty);
    private ReactiveProperty<NamedColor> errorContainer { get; } = new(NamedColor.Empty);
    private ReactiveProperty<NamedColor> onErrorContainer { get; } = new(NamedColor.Empty);
    private ReactiveProperty<NamedColor> background { get; } = new(NamedColor.Empty);
    private ReactiveProperty<NamedColor> onBackground { get; } = new(NamedColor.Empty);
    private ReactiveProperty<NamedColor> outline { get; } = new(NamedColor.Empty);
    private ReactiveProperty<NamedColor> outlineVariant { get; } = new(NamedColor.Empty);
    private ReactiveProperty<NamedColor> scrim { get; } = new(NamedColor.Empty);


    public List<ReactiveProperty<NamedColor>> ColorProperties => new() {
        primary,
        onPrimary,
        primaryContainer,
        onPrimaryContainer,
        secondary,
        onSecondary,
        secondaryContainer,
        onSecondaryContainer,
        tertiary,
        onTertiary,
        tertiaryContainer,
        onTertiaryContainer,
        error,
        onError,
        errorContainer,
        onErrorContainer,
        background,
        onBackground,
        surface,
        onSurface,
        surfaceVariant,
        onSurfaceVariant,
        outline,
        outlineVariant,
        scrim,
        inverseSurface,
        inverseOnSurface,
        inversePrimary,
        primaryFixed,
        onPrimaryFixed,
        primaryFixedDim,
        onPrimaryFixedVariant,
        secondaryFixed,
        onSecondaryFixed,
        secondaryFixedDim,
        onSecondaryFixedVariant,
        tertiaryFixed,
        onTertiaryFixed,
        tertiaryFixedDim,
        onTertiaryFixedVariant,
        surfaceDim,
        surfaceBright,
        surfaceContainerLowest,
        surfaceContainerLow,
        surfaceContainer,
        surfaceContainerHigh,
        surfaceContainerHighest,
    };

    public Color[] SettingColors {
        get => ColorProperties.Select(c => c.Value.Color).ToArray();
        set {
            var dst = ColorProperties;
            for (int i=0, ci=dst.Count; i<ci; i++) {
                if(dst[i].Value.Color != value[i]) {
                    dst[i].Value = new(dst[i].Value.Name, value[i]);
                }
            }
        }
    }

    public class ColorPair {
        public bool IsSingle { get; }
        public ReactiveProperty<NamedColor> Background { get; }
        public ReactiveProperty<NamedColor> Foreground { get; }
        public ColorPair(ReactiveProperty<NamedColor> background, ReactiveProperty<NamedColor> foreground, bool isSingle=false) {
            IsSingle = isSingle;
            Background = background;
            Foreground = foreground;
        }

        public bool IsEmpty => Background.Value.IsEmpty && Foreground.Value.IsEmpty;
        public ReadOnlyReactiveProperty<bool> IsEmptyObservable => Background.CombineLatest(Foreground, (b, f) => b.IsEmpty && f.IsEmpty).ToReadOnlyReactiveProperty();
    }

    public abstract class Common {
        public ColorPair Base { get; set; }
        public ColorPair Container { get; set; }
        public ColorPair Fixed { get; set; }
        public ColorPair FixedDim { get; set; }
        public Common(ColorPair @base, ColorPair container, ColorPair @fixed, ColorPair fixedDim) {
            Base = @base;
            Container = container;
            Fixed = @fixed;
            FixedDim = fixedDim;
        }
    }

    interface ICommon {
        ColorPair Base { get; set; }
        ColorPair Container { get; set; }
        ColorPair Fixed { get; set; }
        ColorPair FixedDim { get; set; }
    }
    public class PrimaryTheme : ICommon {
        public ColorPair Base { get; set; }
        public ColorPair Container { get; set; }
        public ColorPair Fixed { get; set; }
        public ColorPair FixedDim { get; set; }
        public ColorPair Inverse { get; set; }
        public PrimaryTheme(AndroidColorTheme p) { 
            Base = new ColorPair(p.primary, p.onPrimary);
            Container = new ColorPair(p.primaryContainer, p.onPrimaryContainer);
            Fixed = new ColorPair(p.primaryFixed, p.onPrimaryFixedVariant);
            FixedDim = new ColorPair(p.primaryFixedDim, p.onPrimaryFixed);
            Inverse = new ColorPair(p.inversePrimary, p.onPrimaryContainer);
        }
        public bool IsEmpty => Base.IsEmpty || Container.IsEmpty || Fixed.IsEmpty || FixedDim.IsEmpty || Inverse.IsEmpty;
        public ReadOnlyReactiveProperty<bool> IsEmptyObservable => Base.IsEmptyObservable.CombineLatest(Container.IsEmptyObservable, Fixed.IsEmptyObservable, FixedDim.IsEmptyObservable, Inverse.IsEmptyObservable, (b, c, f, fd, i) => b && c && f && fd && i).ToReadOnlyReactiveProperty();
    }

    public class SecondaryTheme : ICommon {
        public ColorPair Base { get; set; }
        public ColorPair Container { get; set; }
        public ColorPair Fixed { get; set; }
        public ColorPair FixedDim { get; set; }
        public SecondaryTheme(AndroidColorTheme p) {
            Base = new ColorPair(p.secondary, p.onSecondary);
            Container = new ColorPair(p.secondaryContainer, p.onSecondaryContainer);
            Fixed = new ColorPair(p.secondaryFixed, p.onSecondaryFixedVariant);
            FixedDim = new ColorPair(p.secondaryFixedDim, p.onSecondaryFixed);
        }

        public bool IsEmpty => Base.IsEmpty || Container.IsEmpty || Fixed.IsEmpty || FixedDim.IsEmpty;
        public ReadOnlyReactiveProperty<bool> IsEmptyObservable => Base.IsEmptyObservable.CombineLatest(Container.IsEmptyObservable, Fixed.IsEmptyObservable, FixedDim.IsEmptyObservable, (b, c, f, fd) => b && c && f && fd).ToReadOnlyReactiveProperty();
    }

    public class TertiaryTheme : ICommon {
        public ColorPair Base { get; set; }
        public ColorPair Container { get; set; }
        public ColorPair Fixed { get; set; }
        public ColorPair FixedDim { get; set; }
        public TertiaryTheme(AndroidColorTheme p) {
            Base = new ColorPair(p.tertiary, p.onTertiary);
            Container = new ColorPair(p.tertiaryContainer, p.onTertiaryContainer);
            Fixed = new ColorPair(p.tertiaryFixed, p.onTertiaryFixedVariant);
            FixedDim = new ColorPair(p.tertiaryFixedDim, p.onTertiaryFixed);
        }

        public bool IsEmpty => Base.Background.Value.IsEmpty && Base.Foreground.Value.IsEmpty && Container.Background.Value.IsEmpty && Container.Foreground.Value.IsEmpty && Fixed.Background.Value.IsEmpty && Fixed.Foreground.Value.IsEmpty && FixedDim.Background.Value.IsEmpty && FixedDim.Foreground.Value.IsEmpty;
        public ReadOnlyReactiveProperty<bool> IsEmptyObservable => Base.IsEmptyObservable.CombineLatest(Container.IsEmptyObservable, Fixed.IsEmptyObservable, FixedDim.IsEmptyObservable, (b, c, f, fd) => b && c && f && fd).ToReadOnlyReactiveProperty();
    }

    public class SurfaceTheme {
        public ColorPair Base { get; set; }
        public ColorPair Variant { get; set; }
        public ColorPair Inverse { get; set; }
        public ColorPair Dim { get; set; }
        public ColorPair Bright { get; set; }
        public ColorPair ContainerLowest { get; set; }
        public ColorPair ContainerLow { get; set; }
        public ColorPair Container { get; set; }
        public ColorPair ContainerHigh { get; set; }
        public ColorPair ContainerHighest { get; set; }

        public SurfaceTheme(AndroidColorTheme p) {
            Base = new ColorPair(p.surface, p.onSurface);
            Variant = new ColorPair(p.surfaceVariant, p.onSurfaceVariant);
            Inverse = new ColorPair(p.inverseSurface, p.inverseOnSurface);
            Dim = new ColorPair(p.surfaceDim, p.onSurface);
            Bright = new ColorPair(p.surfaceBright, p.onSurface);
            ContainerLowest = new ColorPair(p.surfaceContainerLowest, p.onSurface);
            ContainerLow = new ColorPair(p.surfaceContainerLow, p.onSurface);
            Container = new ColorPair(p.surfaceContainer, p.onSurface);
            ContainerHigh = new ColorPair(p.surfaceContainerHigh, p.onSurface);
            ContainerHighest = new ColorPair(p.surfaceContainerHighest, p.onSurface);
        }

        public bool IsEmpty => Base.IsEmpty || Variant.IsEmpty || Inverse.IsEmpty || Dim.IsEmpty || Bright.IsEmpty || ContainerLowest.IsEmpty || ContainerLow.IsEmpty || Container.IsEmpty || ContainerHigh.IsEmpty || ContainerHighest.IsEmpty;
        public ReadOnlyReactiveProperty<bool> IsEmptyObservable => Base.IsEmptyObservable.CombineLatest(Variant.IsEmptyObservable, Inverse.IsEmptyObservable, Dim.IsEmptyObservable, Bright.IsEmptyObservable, ContainerLowest.IsEmptyObservable, ContainerLow.IsEmptyObservable, Container.IsEmptyObservable, ContainerHigh.IsEmptyObservable, ContainerHighest.IsEmptyObservable, (b, v, i, d, br, cl, clow, c, ch, chigh) => b && v && i && d && br && cl && clow && c && ch && chigh).ToReadOnlyReactiveProperty();
    }

    public class  OtherTheme {
        public ColorPair Error { get; set; }
        public ColorPair ErrorContainer { get; set; }
        public ColorPair Background { get; set; }
        public ColorPair Outline { get; set; }
        public ColorPair OutlineVariant { get; set; }
        public ColorPair Scrim { get; set; }

        public OtherTheme(AndroidColorTheme p) {
            Error = new (p.error, p.onError);
            ErrorContainer = new (p.errorContainer, p.onErrorContainer);
            Background = new (p.background, p.onBackground);
            Outline = new(p.outline, p.inverseOnSurface, isSingle:true);
            OutlineVariant = new(p.outlineVariant, p.onSurface, isSingle: true);
            Scrim = new(p.scrim, p.inverseOnSurface, isSingle: true);
        }
    }

    public PrimaryTheme Primary;
    public SecondaryTheme Secondary;
    public TertiaryTheme Tertiary;
    public SurfaceTheme Surface;
    public OtherTheme Other;

    private AndroidColorTheme(List<NamedColor> colors) {
        foreach (var nc in colors) {
            switch (nc.Name) {
                case "primary": primary.Value = nc; break;
                case "primaryContainer": primaryContainer.Value = nc; break;
                case "inversePrimary": inversePrimary.Value = nc; break;
                case "primaryFixed": primaryFixed.Value = nc; break;
                case "primaryFixedDim": primaryFixedDim.Value = nc; break;
                case "onPrimary": onPrimary.Value = nc; break;
                case "onPrimaryContainer": onPrimaryContainer.Value = nc; break;
                case "onPrimaryFixed": onPrimaryFixed.Value = nc; break;
                case "onPrimaryFixedVariant": onPrimaryFixedVariant.Value = nc; break;
                case "secondary": secondary.Value = nc; break;
                case "secondaryContainer": secondaryContainer.Value = nc; break;
                case "secondaryFixed": secondaryFixed.Value = nc; break;
                case "secondaryFixedDim": secondaryFixedDim.Value = nc; break;
                case "onSecondary": onSecondary.Value = nc; break;
                case "onSecondaryContainer": onSecondaryContainer.Value = nc; break;
                case "onSecondaryFixed": onSecondaryFixed.Value = nc; break;
                case "onSecondaryFixedVariant": onSecondaryFixedVariant.Value = nc; break;
                case "tertiary": tertiary.Value = nc; break;
                case "tertiaryContainer": tertiaryContainer.Value = nc; break;
                case "tertiaryFixed": tertiaryFixed.Value = nc; break;
                case "tertiaryFixedDim": tertiaryFixedDim.Value = nc; break;
                case "onTertiary": onTertiary.Value = nc; break;
                case "onTertiaryContainer": onTertiaryContainer.Value = nc; break;
                case "onTertiaryFixed": onTertiaryFixed.Value = nc; break;
                case "onTertiaryFixedVariant": onTertiaryFixedVariant.Value = nc; break;
                case "surface": surface.Value = nc; break;
                case "surfaceVariant": surfaceVariant.Value = nc; break;
                case "inverseSurface": inverseSurface.Value = nc; break;
                case "inverseOnSurface": inverseOnSurface.Value = nc; break;
                case "surfaceDim": surfaceDim.Value = nc; break;
                case "surfaceBright": surfaceBright.Value = nc; break;
                case "surfaceContainerLowest": surfaceContainerLowest.Value = nc; break;
                case "surfaceContainerLow": surfaceContainerLow.Value = nc; break;
                case "surfaceContainer": surfaceContainer.Value = nc; break;
                case "surfaceContainerHigh": surfaceContainerHigh.Value = nc; break;
                case "surfaceContainerHighest": surfaceContainerHighest.Value = nc; break;
                case "onSurface": onSurface.Value = nc; break;
                case "onSurfaceVariant": onSurfaceVariant.Value = nc; break;
                case "error": error.Value = nc; break;
                case "onError": onError.Value = nc; break;
                case "errorContainer": errorContainer.Value = nc; break;
                case "onErrorContainer": onErrorContainer.Value = nc; break;
                case "background": background.Value = nc; break;
                case "onBackground": onBackground.Value = nc; break;
                case "outline": outline.Value = nc; break;
                case "outlineVariant": outlineVariant.Value = nc; break;
                case "scrim": scrim.Value = nc; break;
            }
        }

        Primary = new(this);
        Secondary = new(this);
        Tertiary = new(this);
        Surface = new(this);
        Other = new(this);
    }

    public static AndroidColorTheme NormalTheme(AndroidColors colors) {
        return new AndroidColorTheme(colors.Normal);
    }
    public static AndroidColorTheme MediumTheme(AndroidColors colors) {
        return new AndroidColorTheme(colors.Medium);
    }
    public static AndroidColorTheme HighTheme(AndroidColors colors) {
        return new AndroidColorTheme(colors.High);
    }
    public static AndroidColorTheme? Create(AndroidColors? colors, ColorContrast contrast) {
        if(colors == null) {
            return null;
        }
        return contrast switch {
            ColorContrast.Normal => NormalTheme(colors),
            ColorContrast.Medium => MediumTheme(colors),
            ColorContrast.High => HighTheme(colors),
            _ => throw new InvalidEnumArgumentException(),
        };
    }
}