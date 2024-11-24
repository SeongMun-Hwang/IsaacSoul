using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using UnityEditorInternal;

public class SpriteAutoSlicer : EditorWindow
{
    [MenuItem("Tools/Sprite Auto Slicer")]
    private static void ShowWindow()
    {
        GetWindow<SpriteAutoSlicer>("Sprite Auto Slicer");
    }

    private void OnGUI()
    {
        GUILayout.Label("Sprite Auto Slicer", EditorStyles.boldLabel);

        if (GUILayout.Button("Slice Selected Sprites"))
        {
            SliceSelectedSprites();
        }
    }

    private void SliceSelectedSprites()
    {
        // 선택한 오브젝트 가져오기
        Object[] selectedObjects = Selection.objects;

        foreach (Object obj in selectedObjects)
        {
            string assetPath = AssetDatabase.GetAssetPath(obj);

            // TextureImporter 가져오기
            TextureImporter importer = AssetImporter.GetAtPath(assetPath) as TextureImporter;
            if (importer == null || importer.textureType != TextureImporterType.Sprite)
            {
                Debug.LogWarning($"'{obj.name}' is not a valid Sprite texture.");
                continue;
            }

            importer.spriteImportMode = SpriteImportMode.Multiple; // 다중 스프라이트 모드 설정

            // 자동 슬라이스 실행
            SpriteMetaData[] spriteData = AutomaticSlice(importer);
            if (spriteData != null)
            {
                // Importer에 메타데이터 설정
                ApplySpriteMetaData(importer, spriteData);
                AssetDatabase.ImportAsset(assetPath, ImportAssetOptions.ForceUpdate);
                Debug.Log($"'{obj.name}' sliced successfully.");
            }
            else
            {
                Debug.LogWarning($"No valid sprites found in '{obj.name}'.");
            }
        }
    }

    private SpriteMetaData[] AutomaticSlice(TextureImporter importer)
    {
        // Texture 데이터 가져오기
        Texture2D texture = AssetDatabase.LoadAssetAtPath<Texture2D>(importer.assetPath);
        if (texture == null) return null;

        List<SpriteMetaData> spriteMetaDataList = new List<SpriteMetaData>();

        // 텍스처의 투명 영역을 기준으로 Rect 생성
        Rect[] rects = InternalSpriteUtility.GenerateAutomaticSpriteRectangles(texture, 0, 0);
        foreach (Rect rect in rects)
        {
            SpriteMetaData metaData = new SpriteMetaData
            {
                rect = rect,
                name = $"{texture.name}_{spriteMetaDataList.Count}",
                pivot = new Vector2(0.5f, 0f) // Bottom 피벗
            };
            spriteMetaDataList.Add(metaData);
        }

        return spriteMetaDataList.ToArray();
    }

    private void ApplySpriteMetaData(TextureImporter importer, SpriteMetaData[] spriteData)
    {
        TextureImporterSettings settings = new TextureImporterSettings();
        importer.ReadTextureSettings(settings);

        // 필요한 설정 적용
        settings.spriteMode = (int)SpriteImportMode.Multiple;
        settings.spritePixelsPerUnit = 100f; // 필요에 따라 수정 가능

        importer.SetTextureSettings(settings);
        importer.spritesheet = spriteData; // spritesheet 대신 메타데이터 설정
    }
}
