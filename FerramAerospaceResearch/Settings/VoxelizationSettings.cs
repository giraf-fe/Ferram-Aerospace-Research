using System.Collections.Generic;
using FerramAerospaceResearch.Reflection;
using UnityEngine;

// ReSharper disable CollectionNeverUpdated.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable ClassNeverInstantiated.Global

namespace FerramAerospaceResearch.Settings
{
    [ConfigNode("ColorMap")]
    public class ColorMap
    {
        [ConfigValueIgnore]
        public static readonly ColorMap Default = new ColorMap
        {
            Name = "default",
            Colors = {new Color(0.18f, 0f, 0.106f)}
        };

        [ConfigValue("name")] public string Name { get; set; }

        [ConfigValue("color")] public List<Color> Colors { get; } = new List<Color>();

        public Color this[int index]
        {
            get { return Colors[index]; }
            set { Colors[index] = value; }
        }

        public Color Get(int index)
        {
            return Colors[index % Colors.Count];
        }
    }

    [ConfigNode("Voxelization", shouldSave: false, parent: typeof(FARConfig))]
    public static class VoxelizationSettings
    {
        [ConfigValue("default")] public static string Default { get; set; }

        [ConfigValue("ColorMap")] public static List<ColorMap> ColorMaps { get; } = new List<ColorMap>();

        public static ColorMap FirstOrDefault()
        {
            return FirstOrDefault(Default);
        }
        public static ColorMap FirstOrDefault(string name)
        {
            if (string.IsNullOrEmpty(name))
                return ColorMap.Default;
            foreach (ColorMap map in ColorMaps)
            {
                if (map.Name == name)
                    return map;
            }

            return ColorMap.Default;
        }
    }
}
