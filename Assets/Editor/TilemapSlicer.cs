using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapSlicer : EditorWindow
{
    private Texture2D sourceTexture;
    private string savePath = "Assets/SlicedTiles"; // Default save path

    [MenuItem("Tools/Tilemap Slicer")]
    public static void ShowWindow()
    {
        GetWindow<TilemapSlicer>("Tilemap Slicer");
    }

    private void OnGUI()
    {
        GUILayout.Label("Tilemap Slicer", EditorStyles.boldLabel);

        // Source texture selection
        sourceTexture = (Texture2D)EditorGUILayout.ObjectField("Source Texture", sourceTexture, typeof(Texture2D), false);

        // Save path selection
        savePath = EditorGUILayout.TextField("Save Path", savePath);

        // Select folder button
        if (GUILayout.Button("Select Folder"))
        {
            string folder = EditorUtility.OpenFolderPanel("Select Folder", "Assets", "");
            if (!string.IsNullOrEmpty(folder))
            {
                if (folder.StartsWith(Application.dataPath))
                {
                    savePath = "Assets" + folder.Substring(Application.dataPath.Length);
                }
                else
                {
                    EditorUtility.DisplayDialog("Invalid Path", "Please select a path inside the 'Assets' folder.", "OK");
                }
            }
        }

        // Save sprites as tiles button
        if (GUILayout.Button("Save Sprites as Tiles") && sourceTexture != null)
        {
            SaveSpritesAsTiles();
        }
    }

    private void SaveSpritesAsTiles()
    {
        string path = AssetDatabase.GetAssetPath(sourceTexture);
        TextureImporter textureImporter = AssetImporter.GetAtPath(path) as TextureImporter;

        if (textureImporter == null) return;

        // Ensure the texture is set to Multiple sprite mode
        textureImporter.spriteImportMode = SpriteImportMode.Multiple;
        AssetDatabase.ImportAsset(path, ImportAssetOptions.ForceUpdate);

        // Create folder if it doesn't exist
        if (!AssetDatabase.IsValidFolder(savePath))
        {
            AssetDatabase.CreateFolder("Assets", savePath.Substring(7)); // Remove "Assets/" from path
        }

        // Load all sprites associated with the texture
        Object[] sprites = AssetDatabase.LoadAllAssetsAtPath(path);

        foreach (Object obj in sprites)
        {
            if (obj is Sprite sprite)
            {
                // Create tile for each sprite, including transparent ones
                Tile tile = ScriptableObject.CreateInstance<Tile>();
                tile.sprite = sprite; // Assign sprite to tile

                // Check if sprite has transparency
                if (IsTransparent(sprite))
                {
                    tile.sprite = null; // Optionally assign a default transparent sprite if needed
                }

                // Save tile as an asset
                string tileName = sprite.name;
                string assetPath = $"{savePath}/{tileName}.asset";
                AssetDatabase.CreateAsset(tile, assetPath);
            }
        }

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();

        Debug.Log("Sprites saved as tiles successfully!");
    }

    // Function to check if a sprite is fully transparent
    private bool IsTransparent(Sprite sprite)
    {
        if (sprite == null) return false;

        Texture2D texture = sprite.texture;
        Rect rect = sprite.textureRect;

        for (int y = (int)rect.yMin; y < rect.yMax; y++)
        {
            for (int x = (int)rect.xMin; x < rect.xMax; x++)
            {
                if (texture.GetPixel(x, y).a > 0)
                {
                    return false; // If there's any non-transparent pixel, it's not fully transparent
                }
            }
        }

        return true; // All pixels are transparent
    }
}
