using UnityEngine;
using UnityEngine.Rendering;

public class CameraRenderer
{
	/// <summary>
	/// We don't see anything yet because we also have to indicate which kind of shader passes are allowed. 
	/// As we only support unlit shaders in this tutorial 
	/// we have to fetch the shader tag ID for the SRPDefaultUnlit pass, 
	/// which we can do once and cache it in a static field.
	/// </summary>
	static ShaderTagId unlitShaderTagId = new ShaderTagId("SRPDefaultUnlit");
	const string bufferName = "Render Camera";

		/// <summary>
		/// It's as if we've written buffer.name = bufferName; 
		/// as a separate statement after invoking the constructor. 
		/// But when creating a new object, you can append a code block to the constructor's invocation. 
		/// Then you can set the object's fields and properties in the block 
		/// without having to reference the object instance explicitly. 
		/// It makes explicit that the instances should only be used after those fields and properties have been set.
		/// Besides that, it makes initialization possible where only a single statement
		/// is allowed—for example a field initialization, 
		/// which we're using here—without requiring constructors with many parameter variants.
		/// 
		/// Note that we omitted the empty parameter list of the constructor invocation, 
		/// which is allowed when object initializer syntax is used.
		/// </summary>
		CommandBuffer buffer = new CommandBuffer
	{
		name = bufferName
	};

	ScriptableRenderContext context;

	Camera camera;
	CullingResults cullingResults;

	public void Render(ScriptableRenderContext context, Camera camera)
	{
		this.context = context;
		this.camera = camera;
		if (!Cull())
		{
			return;
		}
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
		// CommandBuffer.ClearRenderTarget requires at least three arguments.
		// The first two indicate whether the depth and color data should be cleared,
		// which is true for both. The third argument is the color used to clearing,
		// for which we'll use Color.clear.
		buffer.ClearRenderTarget(true, true, Color.red);    // 选中摄像机会变成后面的颜色
		buffer.BeginSample(bufferName);
		
		ExecuteBuffer();
		
	}

	/// <summary>
	/// That's because the commands that we issue to the context are buffered. 
	/// We have to submit the queued work for execution, by invoking Submit on the context. 
	/// Let's do this in a separate Submit method, invoked after DrawVisibleGeometry.
	/// </summary>
	void DrawVisibleGeometry()
	{
		var sortingSettings = new SortingSettings(camera)
		{
			criteria = SortingCriteria.CommonOpaque
		};
		var drawingSettings = new DrawingSettings(unlitShaderTagId, sortingSettings);
		var filteringSettings = new FilteringSettings(RenderQueueRange.all);

		context.DrawRenderers(
			cullingResults, ref drawingSettings, ref filteringSettings
		);

		context.DrawSkybox(camera);
	}

	void Submit()
	{
		buffer.EndSample(bufferName);
		ExecuteBuffer();
		context.Submit();
	}

	void ExecuteBuffer()
	{
		context.ExecuteCommandBuffer(buffer);
		buffer.Clear();
	}

	bool Cull()
	{
		if (camera.TryGetCullingParameters(out ScriptableCullingParameters p))
		{
			cullingResults = context.Cull(ref p);
			return true;
		}
		return false;
	}
}