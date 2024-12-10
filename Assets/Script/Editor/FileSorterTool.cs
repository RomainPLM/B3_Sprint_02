using UnityEngine;
using UnityEditor;
using System.IO;
using System.Collections.Generic;

public class FileSorterTool : EditorWindow
{
    private string sourceFolderPath = "Assets/Unsorted";
    private string backupFilePath = "Assets/file_positions_backup.json";

    private Dictionary<string, string> fileTypeToFolderMap = new Dictionary<string, string>()
    {
        { ".shader", "Assets/Shaders" },
        { ".png", "Assets/Textures" },
        { ".jpg", "Assets/Textures" },
        { ".jpeg", "Assets/Textures" },
        { ".tga", "Assets/Textures" },
        { ".cs", "Assets/Scripts" },
        { ".mat", "Assets/Materials" },
        { ".fbx", "Assets/Meshes" },
        { ".obj", "Assets/Meshes" },
        { ".prefab", "Assets/Prefabs" },
        { ".anim", "Assets/Animations" },
        { ".controller", "Assets/Animators" },
        { ".hsls", "Assets/HLSL" }
        
    };

    private Dictionary<string, string> fileMoveLog = new Dictionary<string, string>();

    [System.Serializable]
    private class FileLocationData
    {
        public List<string> originalPaths = new List<string>();
        public List<string> newPaths = new List<string>();
    }

    [MenuItem("Tools/File Sorter")]
    public static void ShowWindow()
    {
        GetWindow<FileSorterTool>("File Sorter");
    }

    private void OnGUI()
    {
        GUILayout.Label("Source Folder", EditorStyles.boldLabel);
        sourceFolderPath = EditorGUILayout.TextField("Source Folder", sourceFolderPath);

        if (GUILayout.Button("Sort Files"))
        {
            SortFilesAndLogMoves();
        }

        AssetDatabase.Refresh();
    }

    private void SortFilesAndLogMoves()
    {
        fileMoveLog.Clear();

        if (!Directory.Exists(sourceFolderPath))
        {
            Debug.LogError($"Source folder does not exist: {sourceFolderPath}");
            return;
        }

        string[] files = Directory.GetFiles(sourceFolderPath, "*", SearchOption.AllDirectories);

        foreach (var file in files)
        {
            if (file.EndsWith(".meta"))
                continue;

            string fileExtension = Path.GetExtension(file).ToLower();
            string fileName = Path.GetFileNameWithoutExtension(file);

            if (!fileTypeToFolderMap.ContainsKey(fileExtension))
            {
                string notRecognisedFolder = $"Assets/NotRecognised/Files{fileExtension}";
                fileTypeToFolderMap[fileExtension] = notRecognisedFolder;

                if (!Directory.Exists(notRecognisedFolder))
                {
                    Directory.CreateDirectory(notRecognisedFolder);
                }
            }

            string targetFolder = fileTypeToFolderMap[fileExtension];
            MoveAssetWithMetaAndLog(file, targetFolder);

            // Additional logic for textures containing "Normal" in the name
            if (fileExtension == ".png" || fileExtension == ".jpg" || fileExtension == ".jpeg" || fileExtension == ".tga")
            {
                if (fileName.Contains("Normal"))
                {
                    SetTextureTypeToNormal(targetFolder, Path.GetFileName(file));
                }
            }
        }

        SaveFileMoveLog();
        AssetDatabase.Refresh();
        Debug.Log("File sorting completed!");
    }

    private void MoveAssetWithMetaAndLog(string filePath, string targetFolder)
    {
        if (!Directory.Exists(targetFolder))
        {
            Directory.CreateDirectory(targetFolder);
        }

        string assetPath = filePath.Replace(Application.dataPath, "Assets");
        string fileName = Path.GetFileName(assetPath);
        string targetPath = Path.Combine(targetFolder, fileName);

        string error = AssetDatabase.MoveAsset(assetPath, targetPath);
        if (string.IsNullOrEmpty(error))
        {
            fileMoveLog[assetPath] = targetPath;
            Debug.Log($"Moved {fileName} (and its meta) to {targetFolder}");
        }
        else
        {
            Debug.LogError($"Failed to move {fileName} to {targetFolder}: {error}");
        }
    }

    private void SetTextureTypeToNormal(string folderPath, string textureName)
    {
        string texturePath = Path.Combine(folderPath, textureName).Replace("\\", "/");
        TextureImporter textureImporter = AssetImporter.GetAtPath(texturePath) as TextureImporter;

        if (textureImporter != null)
        {
            textureImporter.textureType = TextureImporterType.NormalMap;
            textureImporter.SaveAndReimport();
            Debug.Log($"Texture type set to Normal Map for: {textureName}");
        }
        else
        {
            Debug.LogError($"Failed to set texture type for: {textureName}");
        }
    }

    private void SaveFileMoveLog()
    {
        FileLocationData locationData = new FileLocationData();

        if (File.Exists(backupFilePath))
        {
            string existingDataJson = File.ReadAllText(backupFilePath);
            FileLocationData existingData = JsonUtility.FromJson<FileLocationData>(existingDataJson);

            if (existingData != null)
            {
                locationData.originalPaths.AddRange(existingData.originalPaths);
                locationData.newPaths.AddRange(existingData.newPaths);
            }
        }

        foreach (var entry in fileMoveLog)
        {
            locationData.originalPaths.Add(entry.Key);
            locationData.newPaths.Add(entry.Value);
        }

        File.WriteAllText(backupFilePath, JsonUtility.ToJson(locationData, true));
        Debug.Log("File move log updated and saved successfully!");
    }
}
