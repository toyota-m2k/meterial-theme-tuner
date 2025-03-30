using System;
using System.IO;
using System.IO.Compression;
using Windows.Storage;

namespace ThemeTuner;

public enum ColorContrast {
    Normal,
    Medium,
    High,
}

public class AndroidConstractColorTheme {
    private AndroidColors Colors;
    public AndroidConstractColorTheme(AndroidColors colors) {
        Colors = colors;
    }

    private AndroidColorTheme? mNormal = null;
    private AndroidColorTheme? mMedium = null;
    private AndroidColorTheme? mHigh = null;

    public AndroidColorTheme NormalTheme {
        get {
            if (mNormal == null) {
                mNormal = AndroidColorTheme.Create(Colors, ColorContrast.Normal);
            }
            return mNormal!;
        }
    }
    public AndroidColorTheme MediumTheme {
        get {
            if (mMedium == null) {
                mMedium = AndroidColorTheme.Create(Colors, ColorContrast.Medium);
            }
            return mMedium!;
        }
    }
    public AndroidColorTheme HighTheme {
        get {
            if (mHigh == null) {
                mHigh = AndroidColorTheme.Create(Colors, ColorContrast.High);
            }
            return mHigh!;
        }
    }

    public AndroidColorTheme ThemeOf(ColorContrast contrast) {
        return contrast switch {
            ColorContrast.Normal => NormalTheme,
            ColorContrast.Medium => MediumTheme,
            ColorContrast.High => HighTheme,
            _ => throw new ArgumentException("Invalid contrast", nameof(contrast)),
        };
    }
}

public enum DayNightMode {
    Light,
    Dark,
}

public class AndroidColorThemeSet {
    public AndroidColorThemeSet(AndroidConstractColorTheme light, AndroidConstractColorTheme dark) {
        Light = light;
        Dark = dark;
    }

    public AndroidConstractColorTheme Light { get; }
    public AndroidConstractColorTheme Dark { get; }

    public AndroidColorTheme ThemeOf(DayNightMode mode, ColorContrast contrast) {
        return mode switch {
            DayNightMode.Light => Light.ThemeOf(contrast),
            DayNightMode.Dark => Dark.ThemeOf(contrast),
            _ => throw new ArgumentException("Invalid mode", nameof(mode)),
        };
    }

    public class ThemeBuilder {
        AndroidColors Light = new();
        AndroidColors Dark = new();

        public ThemeBuilder AppendLight(StorageFile path) {
            try {
                Light.Append(path);
            }
            catch (Exception e) {
                System.Diagnostics.Debug.WriteLine(e.Message);
            }
            return this;
        }
        public ThemeBuilder AppendLight(Stream stream) {
            try {
                Light.Append(stream);
            }
            catch (Exception e) {
                System.Diagnostics.Debug.WriteLine(e.Message);
            }
            return this;
        }
        public ThemeBuilder AppendDark(StorageFile path) {
            try {
                Dark.Append(path);
            }
            catch (Exception e) {
                System.Diagnostics.Debug.WriteLine(e.Message);
            }
            return this;
        }
        public ThemeBuilder AppendDark(Stream stream) {
            try {
                Dark.Append(stream);
            }
            catch (Exception e) {
                System.Diagnostics.Debug.WriteLine(e.Message);
            }
            return this;
        }
        public ThemeBuilder AppendAuto(StorageFile path) {
            if (path.FileType == ".xml") {
                if (path.Path.Contains("night")) {
                    return AppendDark(path);
                }
                else {
                    return AppendLight(path);
                }
            }
            else if (path.FileType == ".zip") {
                try {
                    using var stream = path.OpenStreamForReadAsync().Result;
                    var archive = new ZipArchive(stream);
                    foreach (var entry in archive.Entries) {
                        var innerStream = entry.Open();
                        if (entry.FullName.Contains("night")) {
                            AppendDark(innerStream);
                        }
                        else {
                            AppendLight(innerStream);
                        }
                    }
                }
                catch (Exception e) {
                    System.Diagnostics.Debug.WriteLine(e.Message);
                }
            }
            return this;
        }
        public ThemeBuilder AppendDirectory(StorageFolder folder) {
            foreach (var file in folder.GetFilesAsync().AsTask().Result) {
                AppendAuto(file);
            }
            return this;
        }
        public AndroidColorThemeSet Build() {
            return new AndroidColorThemeSet(new AndroidConstractColorTheme(Light), new AndroidConstractColorTheme(Dark));
        }
    }

    public static ThemeBuilder Builder => new();
}