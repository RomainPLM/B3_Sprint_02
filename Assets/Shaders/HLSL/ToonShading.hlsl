#ifndef ToonShading
#define ToonShading

void Toon_float(float3 Normal, float3 LightDir, float3 LightCol, float3 AdditionalLight, float Attenuation,
               float3 ViewDir, float3 BaseColor, float2 LightAttenuation,
               out float3 OutColor)
{
    float NdotL = saturate(dot(Normal, LightDir));
    float diffuse = smoothstep(LightAttenuation.x, LightAttenuation.y, NdotL);

    OutColor = BaseColor * diffuse * (LightCol /** Attenuation + AdditionalLight)*/) + BaseColor * .1;
}
#endif