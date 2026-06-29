using System;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;
[Serializable]
class Brightness : CustomPass
{
    public Material brightnessMaterial;
    RTHandle tempTexture;
    public static class BrightnessSettings
    {
        public static float brightness = 1f;
    }
    protected override void Setup(ScriptableRenderContext renderContext, CommandBuffer cmd)
    {
        tempTexture = RTHandles.Alloc(
            Vector2.one,
            TextureXR.slices,
            dimension: TextureXR.dimension,
            colorFormat: UnityEngine.Experimental.Rendering.GraphicsFormat.R16G16B16A16_SFloat,
            useDynamicScale: true,
            name: "BlurTemp"
        );
    }
    protected override void Execute(CustomPassContext ctx)
    {
        brightnessMaterial.SetFloat("_Brightness", BrightnessSettings.brightness);

        // Copy camera -> temp
        HDUtils.BlitCameraTexture(ctx.cmd, ctx.cameraColorBuffer, tempTexture);

        // Sample temp and render back into camera
        brightnessMaterial.SetTexture("_MainTex", tempTexture);

        CoreUtils.SetRenderTarget(ctx.cmd, ctx.cameraColorBuffer);
        CoreUtils.DrawFullScreen(ctx.cmd, brightnessMaterial);
    }

    protected override void Cleanup()
    {
        tempTexture?.Release();
    }
}