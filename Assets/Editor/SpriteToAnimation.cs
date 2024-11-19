using UnityEngine;
using UnityEditor;
using UnityEditorInternal;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public class SpriteToAnimation : EditorWindow
{
    private int animationSamples = 8; // 애니메이션 샘플 수 (초당 프레임 수)
    private string savePath = "Assets/Animations"; // 애니메이션 저장 경로 기본값
    private List<Texture2D> texturesToAnimate = new List<Texture2D>(); // 애니메이션을 만들 Texture2D 목록
    private ReorderableList reorderableTextureList;

    [MenuItem("Tools/Sprite to Animation")]
    private static void ShowWindow()
    {
        GetWindow<SpriteToAnimation>("Sprite to Animation");
    }

    private void OnEnable()
    {
        reorderableTextureList = new ReorderableList(texturesToAnimate, typeof(Texture2D), true, true, true, true);

        reorderableTextureList.drawHeaderCallback = (Rect rect) =>
        {
            EditorGUI.LabelField(rect, "Textures to Animate (Drag and Drop Here)");
        };

        reorderableTextureList.drawElementCallback = (Rect rect, int index, bool isActive, bool isFocused) =>
        {
            texturesToAnimate[index] = (Texture2D)EditorGUI.ObjectField(
                new Rect(rect.x, rect.y, rect.width, EditorGUIUtility.singleLineHeight),
                texturesToAnimate[index],
                typeof(Texture2D),
                false
            );
        };

        reorderableTextureList.onAddCallback = (ReorderableList list) =>
        {
            texturesToAnimate.Add(null); // 빈 항목 추가 가능
        };

        reorderableTextureList.onRemoveCallback = (ReorderableList list) =>
        {
            texturesToAnimate.RemoveAt(list.index);
        };
    }

    private void OnGUI()
    {
        GUILayout.Label("Animation Settings", EditorStyles.boldLabel);

        // 애니메이션 샘플 설정
        animationSamples = EditorGUILayout.IntField("Animation Samples", animationSamples);

        // 애니메이션 저장 경로 선택
        GUILayout.BeginHorizontal();
        EditorGUILayout.TextField("Save Path", savePath);
        if (GUILayout.Button("Select Folder", GUILayout.Width(100)))
        {
            string selectedPath = EditorUtility.OpenFolderPanel("Select Folder for Animation", "Assets", "");
            if (!string.IsNullOrEmpty(selectedPath))
            {
                if (selectedPath.StartsWith(Application.dataPath))
                {
                    savePath = "Assets" + selectedPath.Substring(Application.dataPath.Length);
                }
                else
                {
                    Debug.LogWarning("Please select a folder within the Assets directory.");
                }
            }
        }
        GUILayout.EndHorizontal();

        // ReorderableList로 Texture2D 리스트 관리
        reorderableTextureList.DoLayoutList();

        // 선택된 텍스처로 애니메이션 생성 버튼
        if (GUILayout.Button("Create Animation from Selected Textures") && texturesToAnimate.Count > 0)
        {
            CreateAnimations();
        }
    }

    private void CreateAnimations()
    {
        foreach (Texture2D texture in texturesToAnimate)
        {
            if (texture == null) continue;

            string spritePath = AssetDatabase.GetAssetPath(texture);
            Sprite[] sprites = AssetDatabase.LoadAllAssetsAtPath(spritePath).OfType<Sprite>().ToArray();
            if (sprites.Length > 0)
            {
                string animationName = texture.name;
                CreateAnimationClip(sprites, animationName);
            }
        }
    }

    private void CreateAnimationClip(Sprite[] sprites, string animationName)
    {
        AnimationClip clip = new AnimationClip();
        clip.frameRate = animationSamples;

        var curveBinding = new EditorCurveBinding
        {
            type = typeof(SpriteRenderer),
            path = "",
            propertyName = "m_Sprite"
        };

        ObjectReferenceKeyframe[] keyFrames = new ObjectReferenceKeyframe[sprites.Length];
        for (int i = 0; i < sprites.Length; i++)
        {
            keyFrames[i] = new ObjectReferenceKeyframe
            {
                time = i / (float)animationSamples,
                value = sprites[i]
            };
        }

        AnimationUtility.SetObjectReferenceCurve(clip, curveBinding, keyFrames);

        string fullPath = Path.Combine(savePath, animationName + ".anim");
        AssetDatabase.CreateAsset(clip, fullPath);
        AssetDatabase.SaveAssets();

        Debug.Log($"Animation '{animationName}' created at: {fullPath}");
    }
}
