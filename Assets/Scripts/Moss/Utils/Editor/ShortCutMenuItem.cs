using UnityEditor;
using UnityEngine;

namespace Moss
{
    public static class ShortCutMenuItem
    {
        [MenuItem("Moss/Open PersistentDataPath")]
        private static void OpenPersistentDataPath()
        {
            EditorUtility.RevealInFinder(Application.persistentDataPath);
        }

        [MenuItem("Moss/Open DataPath")]
        private static void OpenDataPath()
        {
            EditorUtility.RevealInFinder(Application.dataPath);
        }

        [MenuItem("Moss/Open StreamingAssetsPath")]
        private static void OpenStreamingAssetsPath()
        {
            EditorUtility.RevealInFinder(Application.streamingAssetsPath);
        }

        [MenuItem("Moss/Open TemporaryCachePath")]
        private static void OpenTemporaryCachePath()
        {
            EditorUtility.RevealInFinder(Application.temporaryCachePath);
        }
    }
}