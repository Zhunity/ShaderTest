using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class CustomRenderPipeline : RenderPipeline
{
	CameraRenderer renderer = new CameraRenderer();

	/// <summary>
	/// This method was the entry point defined for custom SRPs, 
	/// but because the camera array parameter requires allocating memory every frame an alternative 
	/// has been introduced that has a list parameter instead. 
	/// We can use that one in Unity 2022, 
	/// but still have to keep the other version as well because it is declared abstract, 
	/// even though it won't be used. 
	/// Note that later profiler screenshots include the old allocations for the camera array.
	/// </summary>
	/// <param name="context"></param>
	/// <param name="cameras"></param>
	protected override void Render(ScriptableRenderContext context, Camera[] cameras)
	{

	}

	protected override void Render(
	ScriptableRenderContext context, List<Camera> cameras)
	{
		for (int i = 0; i < cameras.Count; i++)
		{
			renderer.Render(context, cameras[i]);
		}
	}

}