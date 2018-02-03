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
                CommonTools.CommonEditorTools.ReplaceSelectPrefab(obj);
            }
            AssetDatabase.SaveAssets();
        }

        [MenuItem("CommonTools/Particle/删除粒子上的DefaultParticle")]
        static void DeleteDefaultParticleSystem()
        {

            object[] objs = Selection.objects;
            for (int i = 0; i < objs.Length; i++)
            {
                GameObject obj = objs[i] as GameObject;
                ParticleSystemRenderer par1 = obj.transform.GetComponent<ParticleSystemRenderer>();
                if (par1 != null)
                {
                    if (par1.sharedMaterial.name == "Default-Particle")
                    {
                        par1.sharedMaterial = null;
                        ParticleSystem s = par1.transform.GetComponent<ParticleSystem>();
                        GameObject.DestroyImmediate(s);
                    }
                }
                ParticleSystemRenderer[] parrenders = obj.transform.GetComponentsInChildren<ParticleSystemRenderer>(true);
                foreach (var par in parrenders)
                {
                    if (par.sharedMaterial != null && par.sharedMaterial.name == "Default-Particle")
                    {
                        par.sharedMaterial = null;
                        ParticleSystem s = par.transform.GetComponent<ParticleSystem>();
                        GameObject.DestroyImmediate(s);
                    }
                }
                CommonTools.CommonEditorTools.ReplaceSelectPrefab(obj);
            }
            AssetDatabase.SaveAssets();
        }
    }
}