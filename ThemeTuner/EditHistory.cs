using Reactive.Bindings;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThemeTuner;
public class EditHistory {
    public class Entry {
        public Color[] Colors { get; }
        public ColorContrast Contrast { get; }
        public DayNightMode DayNight { get; }

        public Entry(DayNightMode dayNight, ColorContrast contrast, Color[] colors) {
            Contrast = contrast;
            DayNight = dayNight;
            Colors = colors;
        }
    }
    public ReadOnlyReactiveProperty<bool> CanUndo { get; }
    public ReadOnlyReactiveProperty<bool> CanRedo { get; }

    private List<Color[]> _history = new();
    private ReactiveProperty<int> _lastIndex = new(-1);

    public EditHistory() {
        CanUndo = _lastIndex.Select(it => it > 0).ToReadOnlyReactiveProperty();
        CanRedo = _lastIndex.Select(it => it < _history.Count - 1).ToReadOnlyReactiveProperty();
    }

    public void Reset(DayNightMode dayNight, ColorContrast contrast, Color[] colors) {
        _history.Clear();
        _lastIndex.Value = -1;
        Push(dayNight, contrast, colors);
    }

    public void Push(DayNightMode dayNight, ColorContrast contrast, Color[] colors) {
        var lastIndex = _lastIndex.Value;
        if (lastIndex < _history.Count - 1) {
            _history.RemoveRange(lastIndex + 1, _history.Count - lastIndex - 1);
        }
        _history.Add(colors);
        _lastIndex.Value = _history.Count - 1;
    }
    public Entry? Back() {
        if (_lastIndex.Value > 0) {
            _lastIndex.Value--;
            return new Entry(DayNightMode.Light, ColorContrast.Normal, _history[_lastIndex.Value]);
        }
        return null;
    }
    public Entry? Forward() {
        if (_lastIndex.Value < _history.Count - 1) {
            _lastIndex.Value++;
            return new Entry(DayNightMode.Light, ColorContrast.Normal, _history[_lastIndex.Value]);
        }
        return null;
    }
}
