using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

namespace CommonTools
{
    public class AnimatorEditorTools
    {
        /// <summary>
        ///压缩动画，数值的精度改为3位
        ///去掉scale曲线
        /// </summary>
        public static void CompressAnimationClip(GameObject g)
        {
            List<AnimationClip> animationClipList = new List<AnimationClip>(AnimationUtility.GetAnimationClips(g));
            if (animationClipList.Count == 0)
            {
                AnimationClip[] objectList = UnityEngine.Object.FindObjectsOfType(typeof(AnimationClip)) as AnimationClip[];
                animationClipList.AddRange(objectList);
            }

            foreach (AnimationClip theAnimation in animationClipList)
            {
                try
                {
                    if (!g.name.Contains("mon_3@show1"))
                    {
                        //去除scale曲线
                        foreach (EditorCurveBinding theCurveBinding in AnimationUtility.GetCurveBindings(theAnimation))
                        {
                            string name = theCurveBinding.propertyName.ToLower();
                            if (name.Contains("scale"))
                            {
                                AnimationUtility.SetEditorCurve(theAnimation, theCurveBinding, null);
                            }
                        }
                    }

                    //浮点数精度压缩到f3
                    AnimationClipCurveData[] curves = null;
#pragma warning disable CS0618 // 类型或成员已过时
                    curves = AnimationUtility.GetAllCurves(theAnimation);
#pragma warning restore CS0618 // 类型或成员已过时
                    Keyframe key;
                    Keyframe[] keyFrames;
                    for (int ii = 0; ii < curves.Length; ++ii)
                    {
                        AnimationClipCurveData curveDate = curves[ii];
                        if (curveDate.curve == null || curveDate.curve.keys == null)
                        {
                            continue;
                        }
                        keyFrames = curveDate.curve.keys;
                        for (int i = 0; i < keyFrames.Length; i++)
                        {
                            key = keyFrames[i];
                            key.value = float.Parse(key.value.ToString("f4"));
                            key.inTangent = float.Parse(key.inTangent.ToString("f4"));
                            key.outTangent = float.Parse(key.outTangent.ToString("f4"));
                            keyFrames[i] = key;
                        }
                        curveDate.curve.keys = keyFrames;
                        theAnimation.SetCurve(curveDate.path, curveDate.type, curveDate.propertyName, curveDate.curve);
                    }
                }
                catch (System.Exception e)
                {
                    Debug.LogError(string.Format("CompressAnimationClip Failed !!! animationPath : {0} error: {1}", "  ", e));
                }
            }
        }
    }
}
