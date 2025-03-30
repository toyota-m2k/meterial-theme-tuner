using Reactive.Bindings;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;

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

    public class ColorPair {
        public ReactiveProperty<NamedColor> Background { get; }
        public ReactiveProperty<NamedColor> Foreground { get; }
        public ColorPair(ReactiveProperty<NamedColor> background, ReactiveProperty<NamedColor> foreground) {
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

    public PrimaryTheme Primary;
    public SecondaryTheme Secondary;
    public TertiaryTheme Tertiary;
    public SurfaceTheme Surface;
    private AndroidColorTheme(List<NamedColor> colors) {
        primary.Value = colors.FirstOrDefault(it => it.Name == "primary") ?? NamedColor.Empty;
        primaryContainer.Value = colors.FirstOrDefault(it => it.Name == "primaryContainer") ?? NamedColor.Empty;
        inversePrimary.Value = colors.FirstOrDefault(it => it.Name == "inversePrimary") ?? NamedColor.Empty;
        primaryFixed.Value = colors.FirstOrDefault(it => it.Name == "primaryFixed") ?? NamedColor.Empty;
        primaryFixedDim.Value = colors.FirstOrDefault(it => it.Name == "primaryFixedDim") ?? NamedColor.Empty;
        onPrimary.Value = colors.FirstOrDefault(it => it.Name == "onPrimary") ?? NamedColor.Empty;
        onPrimaryContainer.Value = colors.FirstOrDefault(it => it.Name == "onPrimaryContainer") ?? NamedColor.Empty;
        onPrimaryFixed.Value = colors.FirstOrDefault(it => it.Name == "onPrimaryFixed") ?? NamedColor.Empty;
        onPrimaryFixedVariant.Value = colors.FirstOrDefault(it => it.Name == "onPrimaryFixedVariant") ?? NamedColor.Empty;
        secondary.Value = colors.FirstOrDefault(it => it.Name == "secondary") ?? NamedColor.Empty;
        secondaryContainer.Value = colors.FirstOrDefault(it => it.Name == "secondaryContainer") ?? NamedColor.Empty;
        secondaryFixed.Value = colors.FirstOrDefault(it => it.Name == "secondaryFixed") ?? NamedColor.Empty;
        secondaryFixedDim.Value = colors.FirstOrDefault(it => it.Name == "secondaryFixedDim") ?? NamedColor.Empty;
        onSecondary.Value = colors.FirstOrDefault(it => it.Name == "onSecondary") ?? NamedColor.Empty;
        onSecondaryContainer.Value = colors.FirstOrDefault(it => it.Name == "onSecondaryContainer") ?? NamedColor.Empty;
        onSecondaryFixed.Value = colors.FirstOrDefault(it => it.Name == "onSecondaryFixed") ?? NamedColor.Empty;
        onSecondaryFixedVariant.Value = colors.FirstOrDefault(it => it.Name == "onSecondaryFixedVariant") ?? NamedColor.Empty;
        tertiary.Value = colors.FirstOrDefault(it => it.Name == "tertiary") ?? NamedColor.Empty;
        tertiaryContainer.Value = colors.FirstOrDefault(it => it.Name == "tertiaryContainer") ?? NamedColor.Empty;
        tertiaryFixed.Value = colors.FirstOrDefault(it => it.Name == "tertiaryFixed") ?? NamedColor.Empty;
        tertiaryFixedDim.Value = colors.FirstOrDefault(it => it.Name == "tertiaryFixedDim") ?? NamedColor.Empty;
        onTertiary.Value = colors.FirstOrDefault(it => it.Name == "onTertiary") ?? NamedColor.Empty;
        onTertiaryContainer.Value = colors.FirstOrDefault(it => it.Name == "onTertiaryContainer") ?? NamedColor.Empty;
        onTertiaryFixed.Value = colors.FirstOrDefault(it => it.Name == "onTertiaryFixed") ?? NamedColor.Empty;
        onTertiaryFixedVariant.Value = colors.FirstOrDefault(it => it.Name == "onTertiaryFixedVariant") ?? NamedColor.Empty;
        surface.Value = colors.FirstOrDefault(it => it.Name == "surface") ?? NamedColor.Empty;
        surfaceVariant.Value = colors.FirstOrDefault(it => it.Name == "surfaceVariant") ?? NamedColor.Empty;
        inverseSurface.Value = colors.FirstOrDefault(it => it.Name == "inverseSurface") ?? NamedColor.Empty;
        inverseOnSurface.Value = colors.FirstOrDefault(it => it.Name == "inverseOnSurface") ?? NamedColor.Empty;
        surfaceDim.Value = colors.FirstOrDefault(it => it.Name == "surfaceDim") ?? NamedColor.Empty;
        surfaceBright.Value = colors.FirstOrDefault(it => it.Name == "surfaceBright") ?? NamedColor.Empty;
        surfaceContainerLowest.Value = colors.FirstOrDefault(it => it.Name == "surfaceContainerLowest") ?? NamedColor.Empty;
        surfaceContainerLow.Value = colors.FirstOrDefault(it => it.Name == "surfaceContainerLow") ?? NamedColor.Empty;
        surfaceContainer.Value = colors.FirstOrDefault(it => it.Name == "surfaceContainer") ?? NamedColor.Empty;
        surfaceContainerHigh.Value = colors.FirstOrDefault(it => it.Name == "surfaceContainerHigh") ?? NamedColor.Empty;
        surfaceContainerHighest.Value = colors.FirstOrDefault(it => it.Name == "surfaceContainerHighest") ?? NamedColor.Empty;
        onSurface.Value = colors.FirstOrDefault(it => it.Name == "onSurface") ?? NamedColor.Empty;
        onSurfaceVariant.Value = colors.FirstOrDefault(it => it.Name == "onSurfaceVariant") ?? NamedColor.Empty;
        Primary = new(this);
        Secondary = new(this);
        Tertiary = new(this);
        Surface = new(this);
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