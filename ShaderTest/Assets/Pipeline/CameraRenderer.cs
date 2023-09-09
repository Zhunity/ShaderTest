using UnityEngine;
using UnityEngine.Rendering;

public class CameraRenderer
{

	ScriptableRenderContext context;

	Camera camera;

	public void Render(ScriptableRenderContext context, Camera camera)
	{
		this.context = context;
		this.camera = camera;

		Setup();
		DrawVisibleGeometry();
		Submit();
	}

	/// <summary>
	/// At the moment, the unity_MatrixVP matrix is always the same.
	/// We have to apply the camera's properties to the context, 
	/// via the SetupCameraProperties method. 
	/// That sets up the matrix as well as some other properties. 
	/// Do this before invoking DrawVisibleGeometry, in a separate Setup method.
	/// </summary>
	void Setup()
	{
		context.SetupCameraProperties(camera);
	}

	/// <summary>
	/// That's because the commands that we issue to the context are buffered. 
	/// We have to submit the queued work for execution, by invoking Submit on the context. 
	/// Let's do this in a separate Submit method, invoked after DrawVisibleGeometry.
	/// </summary>
	void DrawVisibleGeometry()
	{
		context.DrawSkybox(camera);
	}

	void Submit()
	{
		context.Submit();
	}
}