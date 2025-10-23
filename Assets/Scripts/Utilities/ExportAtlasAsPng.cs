using UnityEditor;
using UnityEngine;

public static class ExportSpriteAtlas
{
    [MenuItem("Tools/Export Selected Atlas as PNG")]
    public static void ExportAtlasAsPng()
    {
        Texture2D atlas = Selection.activeObject as Texture2D;
        if (atlas == null)
        {
            Debug.LogWarning("Select the atlas Texture2D (.asset) in the Project window first.");
            return;
        }

        string path = EditorUtility.SaveFilePanel(
            "Export Atlas as PNG",
            "",
            atlas.name + "_Export.png",
            "png"
        );

        if (string.IsNullOrEmpty(path))
            return;

        // Copy the texture to a readable Texture2D
        RenderTexture rt = RenderTexture.GetTemporary(atlas.width, atlas.height, 0);
        Graphics.Blit(atlas, rt);
        RenderTexture.active = rt;

        Texture2D readable = new Texture2D(atlas.width, atlas.height, TextureFormat.RGBA32, false);
        readable.ReadPixels(new Rect(0, 0, atlas.width, atlas.height), 0, 0);
        readable.Apply();

        RenderTexture.active = null;
        RenderTexture.ReleaseTemporary(rt);

        // Save PNG
        System.IO.File.WriteAllBytes(path, readable.EncodeToPNG());
        AssetDatabase.Refresh();

        Debug.Log($"Exported {atlas.name} as PNG to {path}");
    }
}
