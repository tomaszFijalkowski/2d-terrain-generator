using System.Collections.Generic;
using UnityEngine;

public class ColorMap
{
    private readonly Dictionary<LandType, Color> colorMap = new Dictionary<LandType, Color>
    {
        {LandType.DeepWater, new Color(0.1168586f, 0.482513f, 0.72f)},

        {LandType.ShallowWater, new Color(0.109f, 0.529f, 0.760f)},

        {LandType.AridBeach, new Color(0.854f, 0.745f, 0.580f)},
        {LandType.TemperateBeach, new Color(0.878f, 0.749f, 0.556f)},
        {LandType.HumidBeach, new Color(0.901f, 0.752f, 0.533f)},

        {LandType.AridCoast, new Color(0.988f, 0.937f, 0.752f)},
        {LandType.TemperateCoast, new Color(1f, 0.945f, 0.737f)},
        {LandType.HumidCoast, new Color(0.462f, 0.792f, 0.494f)},

        {LandType.AridLowTerrain, new Color(0.988f, 0.937f, 0.752f)},
        {LandType.TemperateLowTerrain, new Color(0.490f, 0.764f, 0.513f)},
        {LandType.HumidLowTerrain, new Color(0.278f, 0.678f, 0.450f)},

        {LandType.AridMediumTerrain, new Color(0.517f, 0.737f, 0.537f)},
        {LandType.TemperateMediumTerrain, new Color(0.317f, 0.639f, 0.458f)},
        {LandType.HumidMediumTerrain, new Color(0.247f, 0.592f, 0.396f)},

        {LandType.AridHighTerrain, new Color(0.352f, 0.603f, 0.458f)},
        {LandType.TemperateHighTerrain, new Color(0.278f, 0.556f, 0.4f)},
        {LandType.HumidHighTerrain, new Color(0.239f, 0.458f, 0.364f)},

        {LandType.AridVeryHighTerrain, new Color(0.313f, 0.525f, 0.403f)},
        {LandType.TemperateVeryHighTerrain, new Color(0.266f, 0.431f, 0.360f)},
        {LandType.HumidVeryHighTerrain, new Color(0.239f, 0.458f, 0.364f)},

        {LandType.AridLowHills, new Color(0.619f, 0.619f, 0.588f)},
        {LandType.TemperateLowHills, new Color(0.611f, 0.611f, 0.596f)},
        {LandType.HumidLowHills, new Color(0.603f, 0.603f, 0.603f)},

        {LandType.AridMediumHills, new Color(0.474f, 0.474f, 0.435f)},
        {LandType.TemperateMediumHills, new Color(0.462f, 0.462f, 0.447f)},
        {LandType.HumidMediumHills, new Color(0.454f, 0.454f, 0.454f)},

        {LandType.AridHighHills, new Color(0.333f, 0.333f, 0.309f)},
        {LandType.TemperateHighHills, new Color(0.329f, 0.329f, 0.313f)},
        {LandType.HumidHighHills, new Color(0.321f, 0.321f, 0.321f)},

        {LandType.Snow, new Color(0.847f, 0.898f, 0.886f)},
        {LandType.RedLava, new Color(1f, 0.44f, 0.24f)},
        {LandType.OrangeLava, new Color(1f, 0.6f, 0.24f)},
        {LandType.YellowLava, new Color(1f, 0.79f, 0.24f)}
    };

    public Color this[float elevation, float humidity, float seaLevel] =>
        colorMap[GetLandType(elevation, humidity, seaLevel)];

    private LandType GetLandType(float elevation, float humidity, float seaLevel)
    {
        if (elevation < 0.05f + seaLevel) return LandType.DeepWater;

        if (elevation < 0.15f + seaLevel) return LandType.ShallowWater;

        if (elevation < 0.175f + seaLevel)
        {
            if (humidity < 0.33f) return LandType.AridBeach;
            if (humidity < 0.66f) return LandType.TemperateBeach;
            return LandType.HumidBeach;
        }

        if (elevation < 0.25f)
        {
            if (humidity < 0.33f) return LandType.AridCoast;
            if (humidity < 0.66f) return LandType.TemperateCoast;
            return LandType.HumidCoast;
        }

        if (elevation < 0.35f)
        {
            if (humidity < 0.33f) return LandType.AridLowTerrain;
            if (humidity < 0.66f) return LandType.TemperateLowTerrain;
            return LandType.HumidLowTerrain;
        }

        if (elevation < 0.45f)
        {
            if (humidity < 0.33f) return LandType.AridMediumTerrain;
            if (humidity < 0.66f) return LandType.TemperateMediumTerrain;
            return LandType.HumidMediumTerrain;
        }

        if (elevation < 0.55f)
        {
            if (humidity < 0.33f) return LandType.AridHighTerrain;
            if (humidity < 0.66f) return LandType.TemperateHighTerrain;
            return LandType.HumidHighTerrain;
        }

        if (elevation < 0.65f)
        {
            if (humidity < 0.33f) return LandType.AridVeryHighTerrain;
            if (humidity < 0.66f) return LandType.TemperateVeryHighTerrain;
            return LandType.HumidVeryHighTerrain;
        }

        if (elevation < 0.7f)
        {
            if (humidity < 0.33f) return LandType.AridLowHills;
            if (humidity < 0.66f) return LandType.TemperateLowHills;
            return LandType.HumidLowHills;
        }

        if (elevation < 0.75f)
        {
            if (humidity < 0.33f) return LandType.AridMediumHills;
            if (humidity < 0.66f) return LandType.TemperateMediumHills;
            return LandType.HumidMediumHills;
        }

        if (elevation < 0.8f)
        {
            if (humidity < 0.33f) return LandType.AridHighHills;
            if (humidity < 0.66f) return LandType.TemperateHighHills;
            return LandType.HumidHighHills;
        }

        if (humidity < 0.33f)
        {
            if (elevation < 0.85f) return LandType.AridHighHills;
            if (elevation < 0.90f) return LandType.RedLava;
            if (elevation < 0.95f) return LandType.OrangeLava;
            return LandType.YellowLava;
        }

        if (humidity < 0.66f)
        {
            if (elevation < 0.90f) return LandType.TemperateHighHills;
            return LandType.TemperateMediumHills;
        }

        return LandType.Snow;
    }
}