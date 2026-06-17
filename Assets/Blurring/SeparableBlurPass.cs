using System;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;
[Serializable]
class SeparableBlurPass : CustomPass
{
    public Material blurMaterial;
    RTHandle tempTexture;
    public static class BlurSettings
    {
        public static int strength = 1;
        public static int stepSize = 1;
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
    float Gaussian(int x)
    {
        float sigmaSqu = BlurSettings.strength / 7.5f * BlurSettings.strength / 7.5f;
        return (1 / Mathf.Sqrt(2 * MathF.PI * sigmaSqu)) * Mathf.Exp(-(x * x) / (2 * sigmaSqu));
    }
    protected override void Execute(CustomPassContext ctx)
    {
        if (blurMaterial == null)
            return;

        // SOURCE = camera color
        var source = ctx.cameraColorBuffer;

        blurMaterial.SetInt("_KernelSize", BlurSettings.strength);
        blurMaterial.SetFloat("_Spread", BlurSettings.strength / 7.5f);
        blurMaterial.SetInt("_BlurStepSize", BlurSettings.stepSize);

        const int MAX_KERNEL = 100;

        float[] weights = new float[MAX_KERNEL];

        int kernelSize = BlurSettings.strength;
        int halfKernel = kernelSize / 2;

        // fill real weights
        for (int i = 0; i < kernelSize; i++)
            weights[i] = Gaussian(i - halfKernel);

        // pad remaining
        for (int i = kernelSize; i < MAX_KERNEL; i++)
            weights[i] = 0f;

        blurMaterial.SetFloatArray("_Weights", weights);

        blurMaterial.SetVector("_BlurDirection", new Vector2(1, 0));
        ctx.cmd.SetGlobalTexture("_InputTexture", source);

        CoreUtils.SetRenderTarget(ctx.cmd, tempTexture);
        CoreUtils.DrawFullScreen(ctx.cmd, blurMaterial);

        blurMaterial.SetVector("_BlurDirection", new Vector2(0, 1));
        ctx.cmd.SetGlobalTexture("_InputTexture", tempTexture);

        CoreUtils.SetRenderTarget(ctx.cmd, ctx.cameraColorBuffer);
        CoreUtils.DrawFullScreen(ctx.cmd, blurMaterial);
    }

    protected override void Cleanup()
    {
        tempTexture?.Release();
    }
}