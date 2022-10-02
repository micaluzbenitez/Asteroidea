using System;
using UnityEngine;
using UnityEditor;

internal class SetVersionWindow : EditorWindow
{
    public event Action<Version> OnAccept;

    private void OnGUI()
    {
        Version currentVersion = Version.GetCurrentVersion();
        Version newVersion = Version.GetCurrentVersion();

        EditorGUILayout.LabelField(BuildTexts.AppBuildInfoTitle, EditorStyles.boldLabel);

        EditorGUILayout.Space(16);

        EditorGUILayout.LabelField(BuildTexts.CurrentVersion + PlayerSettings.bundleVersion);

        EditorGUILayout.Space(32);

        EditorGUILayout.LabelField(BuildTexts.Title, EditorStyles.boldLabel);

        EditorGUILayout.Space(16);

        EditorGUILayout.BeginFoldoutHeaderGroup(true, BuildTexts.VersionBuildTitle);

        EditorGUILayout.HelpBox(BuildTexts.VersionBuildInfo, MessageType.Info);

        EditorGUILayout.Space(16);

        if (GUILayout.Button(BuildTexts.MajorVersion, GUILayout.MinHeight(32)))
            newVersion.IncreaseMajor();

        if (GUILayout.Button(BuildTexts.MinorVersion, GUILayout.MinHeight(32)))
            newVersion.IncreaseMinor();

        if (GUILayout.Button(BuildTexts.PatchVersion, GUILayout.MinHeight(32)))
            newVersion.IncreasePatch();

        EditorGUILayout.Space(16);

        EditorGUILayout.EndFoldoutHeaderGroup();

        EditorGUILayout.Space(32);

        EditorGUILayout.BeginFoldoutHeaderGroup(true, BuildTexts.LocalBuildTitle);

        EditorGUILayout.HelpBox(BuildTexts.LocalBuildInfo, MessageType.Info);

        EditorGUILayout.Space(16);

        if (GUILayout.Button(BuildTexts.LocalVersion, GUILayout.MinHeight(32)))
            OnAccept?.Invoke(newVersion);

        if (!newVersion.Equals(currentVersion))
        {
            bool confirmedNewVersion = EditorUtility.DisplayDialog(BuildTexts.ConfirmationDialog.Title,
                                                                   string.Format(BuildTexts.ConfirmationDialog.Message,
                                                                   newVersion.ToString(),
                                                                   currentVersion.ToString()),
                                                                   BuildTexts.ConfirmationDialog.Ok,
                                                                   BuildTexts.ConfirmationDialog.Cancel);

            if (confirmedNewVersion)
                OnAccept?.Invoke(newVersion);
        }
    }

    void OnInspectorUpdate()
    {
        Repaint();
    }
}
