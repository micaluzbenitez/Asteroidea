using UnityEngine;
using UnityEditor;

[InitializeOnLoad]
public static class CustomBuilderWithVersionNumber
{
    private static BuildPlayerOptions buildPlayerOptions;
    private static SetVersionWindow window;

    static CustomBuilderWithVersionNumber()
    {
        BuildPlayerWindow.RegisterBuildPlayerHandler(OnPressBuild);
    }

    private static void OnPressBuild(BuildPlayerOptions options)
    {
        buildPlayerOptions = options;
        OpenSetVersionWindow();
    }

    private static void OpenSetVersionWindow()
    {
        Version currentVersion = Version.GetCurrentVersion();
        bool isValidVersionNumber = true;

        if (!currentVersion.IsValid)
        {
            isValidVersionNumber = EditorUtility.DisplayDialog(BuildTexts.VersionFormatErrorDialog.Title,
                                                            string.Format(BuildTexts.VersionFormatErrorDialog.Message, PlayerSettings.bundleVersion),
                                                            BuildTexts.VersionFormatErrorDialog.Ok,
                                                            BuildTexts.VersionFormatErrorDialog.Cancel);

            if (isValidVersionNumber)
                PlayerSettings.bundleVersion = "0.0.1";
        }

        if (isValidVersionNumber)
        {
            window = EditorWindow.GetWindow<SetVersionWindow>(true, "Set Version");

            window.OnAccept += ExecuteBuild;

            Rect screenRect = new Rect(0f, 0f, Screen.width, Screen.height);
            Rect position = window.position;

            position.center = screenRect.center;
            window.position = position;

            window.ShowModalUtility();
        }
    }

    private static void ExecuteBuild(Version version)
    {
        window.Close();
        PlayerSettings.bundleVersion = version.ToString();
        BuildPipeline.BuildPlayer(buildPlayerOptions);
    }
}
