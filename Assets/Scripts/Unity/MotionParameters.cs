using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu(fileName = "Data", menuName = "My Game/Card Motion Parameters")]
public class MotionParameters : ScriptableObject
{
    // f, gamma, and r
    [SerializeField]
    public Vector3 parameters;
    // k1, k2, and k3
    [SerializeField]
    public Vector3 coefficients;
}

[CustomEditor(typeof(MotionParameters), false)]
class MotionParametersEditor : Editor
{
    private Vector3 GetCoefficients(Vector3 par)
    {
        float k1;
        float k2;
        float k3;

        float pif = Mathf.PI * par.x;

        k1 = par.y / pif;
        k2 = 1 / (4 * pif * pif);
        k3 = par.z * par.y / (2 * pif);

        return new Vector3(k1, k2, k3);
    }
    private Vector3 CoefficientsToParams(Vector3 coef)
    {
        Vector3 parameters = new Vector3();

        float twosqrtk2 = 2 * Mathf.Sqrt(coef.y);

        parameters.x = 1 / (Mathf.PI * twosqrtk2);
        parameters.y = coef.x / twosqrtk2;
        parameters.z = 2 * coef.z / coef.x;

        return parameters;
    }

    public override void OnInspectorGUI()
    {
        MotionParameters parameters = target as MotionParameters;

        Vector3 par = parameters.parameters;
        par.x = EditorGUILayout.FloatField("f", par.x);
        par.y = EditorGUILayout.FloatField("zeta", par.y);
        par.z = EditorGUILayout.FloatField("r", par.z);

        GUILayout.Space(30);


        if (par != parameters.parameters)
        {
            parameters.parameters = par;
            parameters.coefficients = GetCoefficients(par);

            //save the asset
            AssetDatabase.Refresh();
            EditorUtility.SetDirty(parameters);
            AssetDatabase.SaveAssets();
        }

        float setHeight = 12;
        EditorGUILayout.SelectableLabel("k1 = " + parameters.coefficients.x, GUILayout.Height(setHeight));
        EditorGUILayout.SelectableLabel("k2 = " + parameters.coefficients.y, GUILayout.Height(setHeight));
        EditorGUILayout.SelectableLabel("k3 = " + parameters.coefficients.z, GUILayout.Height(setHeight));
    }
}