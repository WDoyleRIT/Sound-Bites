using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.UI;

/******************************************************************************
  Copyright (c) 2008-2012 Ryan Juckett
  http://www.ryanjuckett.com/

  This software is provided 'as-is', without any express or implied
  warranty. In no event will the authors be held liable for any damages
  arising from the use of this software.

  Permission is granted to anyone to use this software for any purpose,
  including commercial applications, and to alter it and redistribute it
  freely, subject to the following restrictions:

  1. The origin of this software must not be misrepresented; you must not
	 claim that you wrote the original software. If you use this software
	 in a product, an acknowledgment in the product documentation would be
	 appreciated but is not required.

  2. Altered source versions must be plainly marked as such, and must not be
	 misrepresented as being the original software.

  3. This notice may not be removed or altered from any source
	 distribution.
******************************************************************************/

public struct tDampedSpringMotionParams
{
    public float M_posPosCoef, M_posVelCoef;
    public float M_velPosCoef, M_velVelCoef;
}

public class Spring
{
    private tDampedSpringMotionParams m_params;

    private float angularFrequency, dampingRatio;
    
    public float RestPosition {  get; set; }
    public float Velocity { get => velocity; set => velocity = value; }
    public float Position { get => position; set => position = value; }

    private float velocity;
    private float position;
    private bool scaledDeltaTime;

    public Spring(float angularFrequency, float dampingRatio, float restPosition, bool scaledDeltaTime)
    {
        this.scaledDeltaTime = scaledDeltaTime;

        this.angularFrequency = angularFrequency;
        this.dampingRatio = dampingRatio;
        this.RestPosition = restPosition;
        this.position = restPosition;
    }

    public void Update()
    {
        CalcDampedSpringMotionParams(ref m_params, scaledDeltaTime ? Time.deltaTime : Time.unscaledDeltaTime, angularFrequency, dampingRatio);
        UpdateDampedSpringMotion(ref position, ref velocity, RestPosition, m_params);
    }

    public void CalcDampedSpringMotionParams(
        ref tDampedSpringMotionParams pOutParams,
        float deltaTime, float angularFrequency, float dampingRatio)
    {

        const float epsilon = 0.000001f;

        if (dampingRatio < 0.0f) dampingRatio = 0.0f;
        if (angularFrequency < 0.0f) angularFrequency = 0.0f;

        if (angularFrequency < epsilon)
        {
            pOutParams.M_posPosCoef = 1.0f; pOutParams.M_posVelCoef = 0.0f;
            pOutParams.M_velPosCoef = 0.0f; pOutParams.M_velVelCoef = 1.0f;

            return;
        }

        if (dampingRatio > 1.0f + epsilon)
        {
            float za = -angularFrequency * dampingRatio;
            float zb = angularFrequency * Mathf.Sqrt(dampingRatio * dampingRatio - 1.0f);
            float z1 = za - zb;
            float z2 = za + zb;

            float e1 = Mathf.Exp(z1 * deltaTime);
            float e2 = Mathf.Exp(z2 * deltaTime);

            float invTwozb = 1.0f / (2.0f * zb);

            float e1_Over_TwoZb = e1 * invTwozb;
            float e2_Over_TwoZb = e2 * invTwozb;

            float z1e1_Over_TwoZb = z1 * e1_Over_TwoZb;
            float z2e2_Over_TwoZb = z2 * e2_Over_TwoZb;

            pOutParams.M_posPosCoef = e1_Over_TwoZb * z2 - z2e2_Over_TwoZb + e2;
            pOutParams.M_posVelCoef = -e1_Over_TwoZb + e2_Over_TwoZb;

            pOutParams.M_velPosCoef = (z1e1_Over_TwoZb - z2e2_Over_TwoZb + e2) * z2;
            pOutParams.M_velVelCoef = -z1e1_Over_TwoZb + z2e2_Over_TwoZb;
        }

        else if (dampingRatio < 1.0f - epsilon)
        {
            float omegaZeta = angularFrequency * dampingRatio;
            float alpha = angularFrequency * Mathf.Sqrt(1.0f - dampingRatio * dampingRatio);

            float expTerm = Mathf.Exp(-omegaZeta * deltaTime);
            float cosTerm = Mathf.Cos(alpha * deltaTime);
            float sinTerm = Mathf.Sin(alpha * deltaTime);

            float invAlpha = 1.0f / alpha;

            float expSin = expTerm * sinTerm;
            float expCos = expTerm * cosTerm;
            float expOmegaZetaSin_Over_Alpha = expTerm * omegaZeta * sinTerm * invAlpha;

            pOutParams.M_posPosCoef = expCos + expOmegaZetaSin_Over_Alpha;
            pOutParams.M_posVelCoef = expSin * invAlpha;

            pOutParams.M_velPosCoef = -expSin * alpha - omegaZeta * expOmegaZetaSin_Over_Alpha;
            pOutParams.M_velVelCoef = expCos - expOmegaZetaSin_Over_Alpha;
        }

        else
        {
            float expTerm = Mathf.Exp(-angularFrequency * deltaTime);
            float timeExp = deltaTime * expTerm;
            float timeExpFreq = timeExp * angularFrequency;

            pOutParams.M_posPosCoef = timeExpFreq + expTerm;
            pOutParams.M_posVelCoef = timeExp;

            pOutParams.M_velPosCoef = -angularFrequency * timeExpFreq;
            pOutParams.M_velVelCoef = -timeExpFreq + expTerm;

        }
    }

    public void UpdateDampedSpringMotion(
        ref float pPos, ref float pVel,
        float equilibriumPos,
        tDampedSpringMotionParams param)
    {
        float oldPos = pPos - equilibriumPos;
        float oldVel = pVel;

        pPos = oldPos * param.M_posPosCoef + oldVel * param.M_posVelCoef + equilibriumPos;
        pVel = oldPos * param.M_velPosCoef + oldVel * param.M_velVelCoef;
    }

    public void Nudge(float velocity)
    {
        this.velocity = velocity;
    }
}
