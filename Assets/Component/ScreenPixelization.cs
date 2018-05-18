using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(Camera))]
[AddComponentMenu("Image Effects/Pixelization")]
public class ScreenPixelization : MonoBehaviour
{
	[Range(1, 25)]
	public int resolution = 8;


	void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		int w = source.width / resolution;
		int h = source.height / resolution;

		var lowRez = RenderTexture.GetTemporary(w, h);
		lowRez.filterMode = FilterMode.Point;
		Graphics.Blit(source, lowRez);
		Graphics.Blit(lowRez, destination);
		RenderTexture.ReleaseTemporary(lowRez);
	}
}