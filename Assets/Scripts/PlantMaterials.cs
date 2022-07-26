using System.Collections.Generic;
using UnityEngine;

public class PlantMaterials : MonoBehaviour
{
    [SerializeField] private Material[] petalMats;
    [SerializeField] private Material[] leafMatsUp;
    [SerializeField] private Material[] leafMatsDown;

    private Dictionary<string, Material> petals;
    private Dictionary<string, Material> leafsUp;
    private Dictionary<string, Material> leafsDown;

    private void Awake()
    {
        petals = new Dictionary<string, Material>()
        {
            { "01",           petalMats[0] },
            { "10",           petalMats[1] },
            { "0110",         petalMats[2] },
            { "010110",       petalMats[3] },
            { "100110",       petalMats[4] },
            { "10010110",     petalMats[5] },
            { "01010110",     petalMats[6] },
            { "0110010110",   petalMats[7] },
            { "010110100110", petalMats[8] },
            { "0110100110",   petalMats[9] },
            { "10100110",     petalMats[10] },
            { "01100110",     petalMats[11] }
        };

        leafsUp = new Dictionary<string, Material>()
        {
            { "01",           petalMats[0] },
            { "10",           petalMats[1] },
            { "0110",         petalMats[2] },
            { "010110",       petalMats[3] },
            { "100110",       petalMats[4] },
            { "10010110",     petalMats[5] },
            { "01010110",     petalMats[6] },
            { "0110010110",   petalMats[7] },
            { "010110100110", petalMats[8] },
            { "0110100110",   petalMats[9] },
            { "10100110",     petalMats[10] },
            { "01100110",     petalMats[11] }
        };

        leafsDown = new Dictionary<string, Material>()
        {
            { "01",           petalMats[0] },
            { "10",           petalMats[1] },
            { "0110",         petalMats[2] },
            { "010110",       petalMats[3] },
            { "100110",       petalMats[4] },
            { "10010110",     petalMats[5] },
            { "01010110",     petalMats[6] },
            { "0110010110",   petalMats[7] },
            { "010110100110", petalMats[8] },
            { "0110100110",   petalMats[9] },
            { "10100110",     petalMats[10] },
            { "01100110",     petalMats[11] }
        };
    }

    public Material GetPetalMat(string _plantCode)
    {
        return petals[_plantCode];
    }

    public Material[] GetLeafMat(string _plantCode)
    {
        Material[] _leaf = new Material[2] { leafsUp[_plantCode], leafsDown[_plantCode] };
        return _leaf;
    }
}
