#pragma once

float calculateLineSDF(const float positionOnAxis, const float length) 
{
    return abs(positionOnAxis) - length;
}