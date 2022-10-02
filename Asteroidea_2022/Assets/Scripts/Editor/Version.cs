using System;
using UnityEngine;
using UnityEditor;

public class Version : IEquatable<Version>
{
    private int[] versionNumbers = new int[VersionNumbersCount];

    private const int VersionNumbersCount = 3;

    public bool IsValid { get; private set; }

    public Version() { }

    public Version(Version copy)
    {
        versionNumbers = copy.versionNumbers;
        IsValid = copy.IsValid;
    }

    public static Version GetCurrentVersion()
    {
        Version currentVersion = new Version();

        currentVersion.IsValid = currentVersion.TryParseFromString(PlayerSettings.bundleVersion);

        return currentVersion;
    }

    public override string ToString()
    {
        string versionString = string.Empty;

        for (int i = 0; i < versionNumbers.Length; i++)
        {
            versionString += versionNumbers[i].ToString();

            if (i != versionNumbers.Length - 1)
                versionString += ".";
        }

        return versionString;
    }

    public bool TryParseFromString(string version)
    {
        bool parsed = false;

        string[] versionParts = version.Split('.');

        if (versionParts.Length == VersionNumbersCount)
        {
            for (int i = 0; i < VersionNumbersCount; i++)
            {
                parsed = int.TryParse(versionParts[i], out versionNumbers[i]);

                if (!parsed)
                {
                    Debug.LogError("The version string doesn't contain numbers.");
                    break;
                }
            } 
        }
        else
        {
            Debug.LogError("The version string isn't in the correct 'x.y' format.");
        }

        return parsed;
    }

    public bool Equals(Version other)
    {
        bool areEqual = true;

        for (int i = 0; i < VersionNumbersCount; i++)
        {
            if (this.versionNumbers[i] != other.versionNumbers[i])
            {
                areEqual = false;
                break;
            }
        }

        return areEqual;
    }

    public void IncreaseMajor()
    {
        versionNumbers[0] = versionNumbers[0] + 1;
        versionNumbers[1] = 0;
        versionNumbers[2] = 0;
    }

    public void IncreaseMinor()
    {
        versionNumbers[1] = versionNumbers[1] + 1;
        versionNumbers[2] = 0;
    }

    public void IncreasePatch()
    {
        versionNumbers[2] = versionNumbers[2] + 1;
    }
}
