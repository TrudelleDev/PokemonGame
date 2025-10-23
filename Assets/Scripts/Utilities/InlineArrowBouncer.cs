using UnityEngine;
using TMPro;
using System.Collections;

namespace PokemonGame.Utilities
{
    [RequireComponent(typeof(TMP_Text))]
    public class InlineArrowBouncer : MonoBehaviour
    {
        [SerializeField] private float amplitude = 4f;   // How far up it moves (pixels)
        [SerializeField] private float frequency = 2f;   // How many hops per second
        [SerializeField] private bool pixelSnap = true;  // Keep pixel-perfect

        private TMP_Text textMesh;

        private void Awake()
        {
            textMesh = GetComponent<TMP_Text>();
        }

        private void LateUpdate()
        {
            textMesh.ForceMeshUpdate();
            var textInfo = textMesh.textInfo;

            // Create a wave that stays between 0 and +amplitude (never below 0)
            float normalized = (Mathf.Sin(Time.unscaledTime * Mathf.PI * frequency) + 1f) * 0.5f;
            float offset = normalized * amplitude;

            if (pixelSnap)
                offset = Mathf.Round(offset);

            for (int i = 0; i < textInfo.characterCount; i++)
            {
                var charInfo = textInfo.characterInfo[i];
                if (!charInfo.isVisible || charInfo.elementType != TMP_TextElementType.Sprite)
                    continue;

                int matIndex = charInfo.materialReferenceIndex;
                int vertIndex = charInfo.vertexIndex;
                var verts = textInfo.meshInfo[matIndex].vertices;

                for (int j = 0; j < 4; j++)
                    verts[vertIndex + j].y += offset;
            }

            textMesh.UpdateVertexData(TMP_VertexDataUpdateFlags.Vertices);
        }
    }
}
