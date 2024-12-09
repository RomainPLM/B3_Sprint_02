#ifndef ToonShading
#define ToonShading

void Toon_float(float3 Normal, float3 LightDir, float3 ViewDir, float4 BaseColor, float Smoothness, out float3 OutColor)
{
    float3 H = normalize(ViewDir + (-LightDir));
    float NdotL = dot(-LightDir, Normal);
    float Ndoth = dot(Normal, H);
    float e = exp2(Smoothness * 12);
    //float diffuse = step(-.99, NdotL) + step(-.75, NdotL) + step(-5, NdotL) + step(-.25, NdotL) + step(0, NdotL) + step(.25, NdotL) + step(.5, NdotL) + step(.75, NdotL) + step(.99, NdotL);
    float diffuse = smoothstep(-.05, .05, NdotL);
    float specular = smoothstep(.4, .6, pow(Ndoth, e)) * diffuse;
    
    OutColor = BaseColor * diffuse /*+ specular*/ + BaseColor*.1;
}

#endif