public static class BuildTexts
{
    public const string AppBuildInfoTitle = "App build info";
    public const string CurrentVersion = "Current version: ";

    public const string Title = "Select the type of build";

    public const string VersionBuildTitle = "Versioned Build";
    public const string VersionBuildInfo = "This will replace the Application Version on Player Settings.";
    public const string LocalBuildTitle = "Debug Build";
    public const string LocalBuildInfo = "Keeps the current version. Only for local tests";

    public const string MajorVersion = "Major version [ X.Y.Z => (X + 1).0.0 ]";
    public const string MinorVersion = "Minor version [ X.Y.Z => X.(Y + 1).0 ]";
    public const string PatchVersion = "Patch version [ X.Y.Z => X.Y.(Z + 1) ]";
    public const string LocalVersion = "Local test version (keeps the same version)";

    public static class ConfirmationDialog
    {
        public const string Title = "Version confirmation"; 

        public const string Message = "The version {0} will be created \n" +
                                      "(current version: {1}) \n\n" +
                                      "Are you sure?";

        public const string Ok = "Accept";

        public const string Cancel = "Cancel";  
    }

    public static class VersionFormatErrorDialog
    {
        public const string Title = "Invalid version format";

        public const string Message = "The current version set in Project Settings doesn't have the X.Y.Z format. \n\n" +
                                      "Do you wish to change the current format ({0}) for an X.Y.Z (0.0.1) one?";

        public const string Ok = "Change";

        public const string Cancel = "Do not change";
    }
}
