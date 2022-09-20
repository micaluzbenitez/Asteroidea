#if UNITY_EDITOR

using UnityEngine;
using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;

namespace Utils
{

    public class VersionChanger : IPreprocessBuildWithReport
    {

        #region VARIABLES
        #region SERIALIZED VARIABLES

        #endregion

        #region STATIC VARIABLES

        #endregion

        #region PROTECTED VARIABLES

        #endregion

        #region PRIVATE VARIABLES

        #endregion
        #endregion

        #region METHODS
        #region PUBLIC METHODS
        public int callbackOrder { get { return 0; } }
        public void OnPreprocessBuild(BuildReport report)
        {
            ChangeVersion();
        }

        #endregion

        #region STATIC METHODS

        #endregion

        #region PROTECTED METHODS

        #endregion

        #region PRIVATE METHODS
        private void ChangeVersion()
        {
            string currentBundleVersion = PlayerSettings.bundleVersion;

            string[] versionArray = currentBundleVersion.Split('.');

            int nextVersion = 0;

            if(int.TryParse(versionArray[versionArray.Length - 1], out nextVersion))
            {
                versionArray[versionArray.Length - 1] = (nextVersion+1).ToString();
                string newBundleVersion = "";
                for (int i = 0; i < versionArray.Length; i++)
                {
                    newBundleVersion += versionArray[i];
                    if(i<versionArray.Length-1)
                        newBundleVersion += ".";
                }

                PlayerSettings.bundleVersion = newBundleVersion;

            }

        }
        #endregion
        #endregion
    }
}

#endif