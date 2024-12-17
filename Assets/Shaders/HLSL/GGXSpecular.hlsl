void GGXSpecular_float(float3 Normal, float3 LightVector, float Roughness, out float Result)
{
    float NoH = dot(normalize(Normal + LightVector), Normal);
    float a2 = Pow4(Roughness, 4);
    
    float d = (NoH * a2 - NoH) * NoH + 1;
    Result = a2 / (PI * d * d);
}