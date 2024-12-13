#ifndef CUSTOMLIGHT_INCLUDE
#define CUSTOMLIGHT_INCLUDE

void MainLight_float(out float3 Direction, out float3 Color/*,out float Attenuation*/)
{
    Light light = GetMainLight();
    Direction = light.direction;
    Color = light.color;
    //Attenuation = light.distanceAttenuation;
}

void AdditionalLight_float(float3 WorldPos, float3 WorldNormal, float2 LightAttenuation, out float3 Color)
{
    Color = 0.0f;
#ifndef SHADERGRAPH_PREVIEW
    int lightCount = GetAdditionalLightsCount();
    for (int i = 0; i < lightCount; i++)
    {
        Light light = GetAdditionalLight(i, WorldPos);
        float3 color = dot(light.direction, WorldNormal);
        color = smoothstep(LightAttenuation.x, LightAttenuation.y, color);
        color *= light.color;
        color *= light.distanceAttenuation;
        
        Color += color;

    }
#endif
}
#endif