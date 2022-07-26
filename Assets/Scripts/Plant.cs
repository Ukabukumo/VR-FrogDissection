using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : MonoBehaviour
{
    float time = 0.01f;
    [SerializeField] int numberOfFlowers = 5;
    [SerializeField] int numberOfLeafs = 3;
    [SerializeField] float stemBend = 5f;
    [SerializeField] float stemSkew = 20f;
    [SerializeField] float plantScale = 0.02f;

    [SerializeField] private GameObject spawnPoint;
    [SerializeField] private Stem stemPref;
    [SerializeField] private Receptacle receptaclePref;
    [SerializeField] private Ovary ovaryPref;
    [SerializeField] private Style stylePref;
    [SerializeField] private Stigma stigmaPref;
    [SerializeField] private Petal petalPref;
    [SerializeField] private Sepal sepalPref;
    [SerializeField] private Leaf leafPref;
    [SerializeField] private Filament[] filamentPrefs;
    [SerializeField] private Anther[] antherPrefs;

    private List<List<Stem>> spawnedStems = new List<List<Stem>>();
    private List<Leaf> spawnedLeafs = new List<Leaf>();
    private List<Receptacle> spawnedReceptacles = new List<Receptacle>();
    private List<Ovary> spawnedOvaries = new List<Ovary>();
    private List<Style> spawnedStyles = new List<Style>();
    private List<Stigma> spawnedStigmas = new List<Stigma>();
    private List<List<Petal>> spawnedPetals = new List<List<Petal>>();
    private List<List<Sepal>> spawnedSepals = new List<List<Sepal>>();
    private List<List<Filament>> spawnedFilaments = new List<List<Filament>>();
    private List<List<Anther>> spawnedAnthers = new List<List<Anther>>();
    private List<GameObject> flowers = new List<GameObject>();

    public float Time
    {
        set { time = value; }
    }

    public void CreatePlant(string _plantCode, GameObject _plant)
    {
        GetComponent<PlantCode>().Code = _plantCode;

        CreateStems(_plant.transform);
        CreateLeafs();
        CreateFlowers();

        StartCoroutine(PlantGrowth());
    }

    private void CreateFlowers()
    {
        string _plantCode = GetComponent<PlantCode>().Code;

        for (int i = 0; i < numberOfFlowers; i++)
        {
            Transform _flower = new GameObject("Flower").transform;
            _flower.parent = spawnedStems[i][0].transform.parent.transform.parent;

            Receptacle _newReceptacle = Instantiate(receptaclePref, _flower);
            Ovary _newOvary = Instantiate(ovaryPref, _flower);
            _newOvary.transform.position = _newReceptacle.OvaryConnect.position -
                (_newOvary.Begin.position - _newOvary.transform.position);

            Style _newStyle = Instantiate(stylePref, _flower);
            _newStyle.transform.position = _newOvary.End.position -
                (_newStyle.Begin.position - _newStyle.transform.position);

            Stigma _newStigma = Instantiate(stigmaPref, _flower);
            _newStigma.transform.position = _newStyle.End.position -
                (_newStigma.Begin.position - _newStigma.transform.position);
            _newStigma.GetComponent<PlantCode>().Code = _plantCode;

            List<Petal> _petals = new List<Petal>();

            for (int j = 0; j < _newReceptacle.PetalConnects.Length; j++)
            {
                Petal _newPetal = Instantiate(petalPref, _flower);

                _newPetal.GetComponent<Renderer>().material = 
                    GetComponent<PlantMaterials>().GetPetalMat(_plantCode);
                _newPetal.transform.rotation *= Quaternion.Euler(0f, 0f, 90f * j);
                _newPetal.transform.position = _newReceptacle.PetalConnects[j].position -
                    (_newPetal.Begin.position - _newPetal.transform.position);
                _petals.Add(_newPetal);
            }

            List<Sepal> _sepals = new List<Sepal>();

            for (int j = 0; j < _newReceptacle.SepalConnects.Length; j++)
            {
                Sepal _newSepal = Instantiate(sepalPref, _flower);
                _newSepal.transform.rotation *= Quaternion.Euler(90f * j, 0f, 0f);
                _newSepal.transform.position = _newReceptacle.SepalConnects[j].position -
                    (_newSepal.Begin.position - _newSepal.transform.position);
                _sepals.Add(_newSepal);
            }

            List<Filament> _filaments = new List<Filament>();

            for (int j = 0; j < _newReceptacle.FilamentConnects.Length; j++)
            {
                int _rnd = new System.Random().Next(0, 4);
                Filament _newFilament = Instantiate(filamentPrefs[_rnd], _flower);
                _newFilament.transform.position = _newReceptacle.FilamentConnects[j].position -
                    (_newFilament.Begin.position - _newFilament.transform.position);
                _filaments.Add(_newFilament);
            }

            List<Anther> _anthers = new List<Anther>();

            for (int j = 0; j < _filaments.Count; j++)
            {
                int _rnd = new System.Random().Next(0, 2);
                Anther _newAnther = Instantiate(antherPrefs[_rnd], _flower);
                _newAnther.transform.position = _filaments[j].transform.position -
                    (_newAnther.Begin.position - _newAnther.transform.position);
                _newAnther.GetComponent<PlantCode>().Code = _plantCode;
                _anthers.Add(_newAnther);
            }

            spawnedReceptacles.Add(_newReceptacle);
            spawnedOvaries.Add(_newOvary);
            spawnedStyles.Add(_newStyle);
            spawnedStigmas.Add(_newStigma);
            spawnedPetals.Add(_petals);
            spawnedSepals.Add(_sepals);
            spawnedFilaments.Add(_filaments);
            spawnedAnthers.Add(_anthers);

            flowers.Add(_flower.gameObject);
        }
    }

    private IEnumerator PlantGrowth()
    {
        for (int i = 0; i < 100; i++)
        {
            StemsGrowth();
            LeafsGrowth();
            FlowersGrowth();

            if (time == 0f)
            {
                continue;
            }

            yield return new WaitForSeconds(time);
        }

        for (int i = 0; i < spawnedStyles.Count; i++)
        {
            spawnedStyles[i].AttachStigma(spawnedStigmas[i]);
            spawnedStigmas[i].GetComponent<Rigidbody>().isKinematic = false;
        }

        for (int i = 0; i < spawnedFilaments.Count; i++)
        {
            for (int j = 0; j < spawnedFilaments[i].Count; j++)
            {
                spawnedFilaments[i][j].AttachAnther(spawnedAnthers[i][j]);
                spawnedAnthers[i][j].GetComponent<Rigidbody>().isKinematic = false;
            }
        }    
    }

    private void CreateStems(Transform _plant)
    {
        for (int i = 0; i < numberOfFlowers + numberOfLeafs; i++)
        {
            Transform _parent;

            if (i < numberOfFlowers)
            {
                _parent = new GameObject("Flower_" + (i + 1)).transform;
            }

            else
            {
                _parent = new GameObject("Leaf_" + (i - numberOfFlowers + 1)).transform;
            }

            _parent.transform.parent = _plant;

            List<Stem> _stemParts = new List<Stem>();

            Transform _stem = new GameObject("Stem").transform;
            _stem.transform.parent = _parent;

            Stem _newStem = Instantiate(stemPref, _stem);

            Quaternion _rotation = Quaternion.Euler(Random.Range(-stemSkew, stemSkew), 
                Random.Range(0f, 360f), Random.Range(-stemSkew, stemSkew));
            _rotation *= _plant.transform.rotation * Quaternion.Euler(90f, 0f, 0f);
            _newStem.transform.rotation = _rotation;

            _newStem.transform.position = spawnPoint.transform.position - _newStem.Begin.position;

            _stemParts.Add(_newStem);
            spawnedStems.Add(_stemParts);
        }
    }

    private void CreateLeafs()
    {
        string _plantCode = GetComponent<PlantCode>().Code;

        for (int i = spawnedStems.Count - 1; i >= numberOfFlowers; i--)
        {
            Transform _parent = spawnedStems[i][0].transform.parent.transform.parent;

            Leaf _newLeaf = Instantiate(leafPref, _parent);

            Quaternion _rotation =
                Quaternion.Euler(Random.Range(-30f, 30f), Random.Range(-30f, 30f), Random.Range(-30f, 30f));

            _rotation *= spawnedStems[i][0].transform.rotation;
            _newLeaf.transform.rotation = _rotation;

            _newLeaf.transform.position =
                spawnedStems[i][0].End.position - (_newLeaf.Begin.position - _newLeaf.transform.position);

            spawnedLeafs.Add(_newLeaf);
        }
    }

    private void StemsGrowth()
    {
        int _nStems = spawnedStems.Count;

        for (int i = 0; i < _nStems; i++)
        {
            Transform _stem = spawnedStems[i][0].transform.parent;
            Stem _newStem = Instantiate(stemPref, _stem);
            Quaternion _rotation = Quaternion.Euler(Random.Range(-stemBend, stemBend), 
                Random.Range(-stemBend, stemBend), Random.Range(-stemBend, stemBend));
            int _last = spawnedStems[i].Count - 1;
            _rotation *= spawnedStems[i][_last].transform.rotation;
            _newStem.transform.rotation = _rotation;
            _newStem.transform.position =
                spawnedStems[i][_last].End.position - (_newStem.Begin.position - _newStem.transform.position);
            spawnedStems[i].Add(_newStem);
        }
    }

    private void LeafsGrowth()
    {
        for (int i = spawnedStems.Count - 1; i >= numberOfFlowers; i--)
        {
            Leaf _leaf = spawnedLeafs[spawnedStems.Count - i - 1];
            _leaf.transform.localScale *= 1f + plantScale;

            Vector3 _prevStemRot = -spawnedStems[i][spawnedStems[i].Count - 2].transform.rotation.eulerAngles;
            Vector3 _curStemRot = spawnedStems[i][spawnedStems[i].Count - 1].transform.rotation.eulerAngles;
            _leaf.transform.rotation =
                Quaternion.Euler(_prevStemRot + _leaf.transform.rotation.eulerAngles + _curStemRot);

            _leaf.transform.position = spawnedStems[i][spawnedStems[i].Count - 1].End.position -
                (_leaf.Begin.position - _leaf.transform.position);
        }
    }

    private void FlowersGrowth()
    {
        for (int i = 0; i < numberOfFlowers; i++)
        {
            flowers[i].transform.localScale *= 1f + plantScale;
            flowers[i].transform.rotation = spawnedStems[i][spawnedStems[i].Count - 1].transform.rotation;
            spawnedReceptacles[i].transform.position =
                spawnedStems[i][spawnedStems[i].Count - 1].End.position -
                (spawnedReceptacles[i].Begin.position - spawnedReceptacles[i].transform.position);

            spawnedOvaries[i].transform.position = spawnedReceptacles[i].OvaryConnect.position -
                (spawnedOvaries[i].Begin.position - spawnedOvaries[i].transform.position);

            spawnedStyles[i].transform.position = spawnedOvaries[i].End.position -
                (spawnedStyles[i].Begin.position - spawnedStyles[i].transform.position);

            spawnedStigmas[i].transform.position = spawnedStyles[i].End.position -
                (spawnedStigmas[i].Begin.position - spawnedStigmas[i].transform.position);

            for (int j = 0; j < spawnedReceptacles[i].PetalConnects.Length; j++)
            {
                spawnedPetals[i][j].transform.position = spawnedReceptacles[i].PetalConnects[j].position -
                    (spawnedPetals[i][j].Begin.position - spawnedPetals[i][j].transform.position);
            }

            for (int j = 0; j < spawnedReceptacles[i].SepalConnects.Length; j++)
            {
                spawnedSepals[i][j].transform.position = spawnedReceptacles[i].SepalConnects[j].position -
                    (spawnedSepals[i][j].Begin.position - spawnedSepals[i][j].transform.position);
            }

            for (int j = 0; j < spawnedReceptacles[i].FilamentConnects.Length; j++)
            {
                spawnedFilaments[i][j].transform.position =
                    spawnedReceptacles[i].FilamentConnects[j].position -
                    (spawnedFilaments[i][j].Begin.position - spawnedFilaments[i][j].transform.position);
            }

            for (int j = 0; j < spawnedAnthers[i].Count; j++)
            {
                spawnedAnthers[i][j].transform.position =
                    spawnedFilaments[i][j].End.position -
                    (spawnedAnthers[i][j].Begin.position - spawnedAnthers[i][j].transform.position);
            }
        }
    }

    private void OnTriggerEnter(Collider _other)
    {
        if (_other.tag == "Seeds")
        {
            string _plantCode = _other.GetComponent<PlantCode>().Code;
            CreatePlant(_plantCode, gameObject);
            Destroy(_other);
            GetComponent<SphereCollider>().enabled = false;
        }
    }
}

