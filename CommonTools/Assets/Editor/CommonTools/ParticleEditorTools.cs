using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace CommonTools
{
    public class ParticleEditorTools 
    {
        [MenuItem("CommonTools/Particle/修改粒子大小可缩放(选中物体)", false, 0)]
        static void SetPaticleScalingMode()
        {
            object[] objs = Selection.objects;
            for (int i = 0; i < objs.Length; i++)
            {
                GameObject obj = objs[i] as GameObject;
                ParticleSystem par1 = obj.transform.GetComponent<ParticleSystem>();
                if (par1 != null)
                {
                    par1.scalingMode = ParticleSystemScalingMode.Hierarchy;
                }
                Transform[] trans = obj.transform.GetComponentsInChildren<Transform>();
                foreach (var tran in trans)
                {
                    ParticleSystem par = tran.GetComponent<ParticleSystem>();
                    if (par != null)
                    {
                        par.scalingMode = ParticleSystemScalingMode.Hierarchy;
                    }
                }
            }
            AssetDatabase.SaveAssets();
        }
    }
}