using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : MonoBehaviour
{
    [SerializeField] float time = 0.01f;
    [SerializeField] int numberOfFlowers = 5;
    [SerializeField] int numberOfLeafs = 3;
    [SerializeField] float stemBend = 5f;
    [SerializeField] float stemSkew = 20f;

    [SerializeField] private GameObject spawnPoint;
    [SerializeField] private Stem stemPref;
    [SerializeField] private Receptacle receptaclePref;
    [SerializeField] private Ovary ovaryPref;
    [SerializeField] private Style stylePref;
    [SerializeField] private Stigma stigmaPref;
    [SerializeField] private Petal petalPref;
    [SerializeField] private Sepal sepalPref;
    [SerializeField] private Filament[] filamentPrefs;
    [SerializeField] private Leaf[] leafPrefs;
    [SerializeField] private Anther[] antherPrefs;

    private List<List<Stem>> spawnedStems = new List<List<Stem>>();             // Сгенерированные стебли
    private List<Leaf> spawnedLeafs = new List<Leaf>();                         // Сгенерированные листья
    private List<Receptacle> spawnedReceptacles = new List<Receptacle>();       // Сгенерированные цветоложа
    private List<Ovary> spawnedOvaries = new List<Ovary>();                     // Сгенерированные завязи
    private List<Style> spawnedStyles = new List<Style>();                      // Сгенерированные столбики
    private List<Stigma> spawnedStigmas = new List<Stigma>();                   // Сгенерированные рыльца
    private List<List<Petal>> spawnedPetals = new List<List<Petal>>();          // Сгенерированные лепестки
    private List<List<Sepal>> spawnedSepals = new List<List<Sepal>>();          // Сгенерированные чашелистики
    private List<List<Filament>> spawnedFilaments = new List<List<Filament>>(); // Сгенерированные нити
    private List<List<Anther>> spawnedAnthers = new List<List<Anther>>();       // Сгенерированные пыльники

    private List<GameObject> flowers = new List<GameObject>();

    private void Start()
    {
        CreatePlant();
    }

    private void CreatePlant()
    {
        CreateStems();
        CreateLeafs();
        CreateFlowers();

        StartCoroutine(PlantGrowth());
    }

    private void CreateFlowers()
    {
        for (int i = 0; i < numberOfFlowers; i++)
        {
            // Хранилище для элементов цветка
            Transform _flower = new GameObject("Flower").transform;
            _flower.parent = spawnedStems[i][0].transform.parent.transform.parent;

            // Создаём новое цветоложе
            Receptacle _newReceptacle = Instantiate(receptaclePref, _flower);

            // Создаём новую завязь
            Ovary _newOvary = Instantiate(ovaryPref, _flower);

            // Устанавливаем завязь в определённую точку
            _newOvary.transform.position = _newReceptacle.OvaryConnect.position -
                (_newOvary.Begin.position - _newOvary.transform.position);

            // Создаём новый столбик
            Style _newStyle = Instantiate(stylePref, _flower);

            // Устанавливаем столбик в определённую точку
            _newStyle.transform.position = _newOvary.End.position -
                (_newStyle.Begin.position - _newStyle.transform.position);

            // Создаём новое рыльце
            Stigma _newStigma = Instantiate(stigmaPref, _flower);

            // Устанавливаем рыльце в определённую точку
            _newStigma.transform.position = _newStyle.End.position -
                (_newStigma.Begin.position - _newStigma.transform.position);

            // Лепестки
            List<Petal> _petals = new List<Petal>();

            for (int j = 0; j < _newReceptacle.PetalConnects.Length; j++)
            {
                // Создаём новый лепесток
                Petal _newPetal = Instantiate(petalPref, _flower);

                // Назначаем поворот в зависимости от позиции
                _newPetal.transform.rotation *= Quaternion.Euler(90f * j, 0f, 0f);

                // Устанавливаем лепесток в определённую точку
                _newPetal.transform.position = _newReceptacle.PetalConnects[j].position -
                    (_newPetal.Begin.position - _newPetal.transform.position);

                _petals.Add(_newPetal);
            }

            // Чашелистики
            List<Sepal> _sepals = new List<Sepal>();

            for (int j = 0; j < _newReceptacle.SepalConnects.Length; j++)
            {
                // Создаём новый чашелистик
                Sepal _newSepal = Instantiate(sepalPref, _flower);

                // Назначаем поворот в зависимости от позиции
                _newSepal.transform.rotation *= Quaternion.Euler(90f * j, 0f, 0f);

                // Устанавливаем чашелистик в определённую точку
                _newSepal.transform.position = _newReceptacle.SepalConnects[j].position -
                    (_newSepal.Begin.position - _newSepal.transform.position);

                _sepals.Add(_newSepal);
            }

            // Нити
            List<Filament> _filaments = new List<Filament>();

            for (int j = 0; j < _newReceptacle.FilamentConnects.Length; j++)
            {
                // Создаём новую нить
                int _rnd = new System.Random().Next(0, 4);
                Filament _newFilament = Instantiate(filamentPrefs[_rnd], _flower);

                // Устанавливаем нить в определённую точку
                _newFilament.transform.position = _newReceptacle.FilamentConnects[j].position -
                    (_newFilament.Begin.position - _newFilament.transform.position);

                _filaments.Add(_newFilament);
            }

            // Пыльники
            List<Anther> _anthers = new List<Anther>();

            for (int j = 0; j < _filaments.Count; j++)
            {
                // Создаём новый пыльник
                int _rnd = new System.Random().Next(0, 2);
                Anther _newAnther = Instantiate(antherPrefs[_rnd], _flower);

                // Устанавливаем пыльник в определённую точку
                _newAnther.transform.position = _filaments[j].transform.position -
                    (_newAnther.Begin.position - _newAnther.transform.position);

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

            yield return new WaitForSeconds(time);
        }

        for (int i = 0; i < spawnedStyles.Count; i++)
        {
            // Присоединяем рыльце к столбику пестика
            spawnedStyles[i].AttachStigma(spawnedStigmas[i]);

            // Делаем рыльце восприимчивым к гравитации
            spawnedStigmas[i].GetComponent<Rigidbody>().isKinematic = false;
        }

        for (int i = 0; i < spawnedFilaments.Count; i++)
        {
            for (int j = 0; j < spawnedFilaments[i].Count; j++)
            {
                // Присоединяем пыльник к тычиночной нити
                spawnedFilaments[i][j].AttachAnther(spawnedAnthers[i][j]);

                // Делаем пыльник восприимчивым к гравитации
                spawnedAnthers[i][j].GetComponent<Rigidbody>().isKinematic = false;
            }
        }    
    }

    private void CreateStems()
    {
        Transform _plant = new GameObject("Plant").transform;

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

            // Части стебля
            List<Stem> _stemParts = new List<Stem>();

            // Хранилище для частей стебля
            Transform _stem = new GameObject("Stem").transform;
            _stem.transform.parent = _parent;

            // Создаём новый стебель
            Stem _newStem = Instantiate(stemPref, _stem);

            // Выбираем случайный поворот стебля
            Quaternion _rotation = Quaternion.Euler(Random.Range(-stemSkew, stemSkew), 
                Random.Range(0f, 360f), Random.Range(-stemSkew, stemSkew));
            _newStem.transform.rotation = _rotation;

            // Устанавливаем стебель в определённую точку
            _newStem.transform.position = spawnPoint.transform.position - _newStem.Begin.position;

            _stemParts.Add(_newStem);
            spawnedStems.Add(_stemParts);
        }
    }

    private void CreateLeafs()
    {
        for (int i = spawnedStems.Count - 1; i >= numberOfFlowers; i--)
        {
            Transform _parent = spawnedStems[i][0].transform.parent.transform.parent;

            // Создаём новый лист
            int _rnd = new System.Random().Next(0, 5);
            Leaf _newLeaf = Instantiate(leafPrefs[_rnd], _parent);

            // Выбираем случайный поворот листа
            Quaternion _rotation =
                Quaternion.Euler(Random.Range(-30f, 30f), Random.Range(-30f, 30f), Random.Range(-30f, 30f));

            // Добавляем поворот стебля
            _rotation *= spawnedStems[i][0].transform.rotation;
            _newLeaf.transform.rotation = _rotation;

            // Устанавливаем лист в определённую точку
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
            // Хранилище для частей стебля
            Transform _stem = spawnedStems[i][0].transform.parent;

            // Создаём новый стебель
            Stem _newStem = Instantiate(stemPref, _stem);

            // Выбираем случайный поворот стебля
            Quaternion _rotation = Quaternion.Euler(Random.Range(-stemBend, stemBend), 
                Random.Range(-stemBend, stemBend), Random.Range(-stemBend, stemBend));

            // Добавляем поворот предыдущей части стебля
            int _last = spawnedStems[i].Count - 1;
            _rotation *= spawnedStems[i][_last].transform.rotation;

            _newStem.transform.rotation = _rotation;

            // Устанавливаем стебель в определённую точку
            _newStem.transform.position =
                spawnedStems[i][_last].End.position - (_newStem.Begin.position - _newStem.transform.position);

            spawnedStems[i].Add(_newStem);
        }
    }

    private void LeafsGrowth()
    {
        for (int i = spawnedStems.Count - 1; i >= numberOfFlowers; i--)
        {
            // Берём лист из списка
            Leaf _leaf = spawnedLeafs[spawnedStems.Count - i - 1];

            // Увеличиваем лист
            _leaf.transform.localScale *= 1.02f;

            // Назначаем поворот идентичный стеблю
            Vector3 _prevStemRot = -spawnedStems[i][spawnedStems[i].Count - 2].transform.rotation.eulerAngles;
            Vector3 _curStemRot = spawnedStems[i][spawnedStems[i].Count - 1].transform.rotation.eulerAngles;
            _leaf.transform.rotation =
                Quaternion.Euler(_prevStemRot + _leaf.transform.rotation.eulerAngles + _curStemRot);

            // Устанавливаем лист в определённую точку
            _leaf.transform.position = spawnedStems[i][spawnedStems[i].Count - 1].End.position -
                (_leaf.Begin.position - _leaf.transform.position);
        }
    }

    private void FlowersGrowth()
    {
        for (int i = 0; i < numberOfFlowers; i++)
        {
            // Увеличиваем цветок
            flowers[i].transform.localScale *= 1.015f;

            // Назначаем поворот идентичный стеблю
            flowers[i].transform.rotation = spawnedStems[i][spawnedStems[i].Count - 1].transform.rotation;

            // Устанавливаем цветоложе в определённую точку
            spawnedReceptacles[i].transform.position =
                spawnedStems[i][spawnedStems[i].Count - 1].End.position -
                (spawnedReceptacles[i].Begin.position - spawnedReceptacles[i].transform.position);

            // Устанавливаем завязь в определённую точку
            spawnedOvaries[i].transform.position = spawnedReceptacles[i].OvaryConnect.position -
                (spawnedOvaries[i].Begin.position - spawnedOvaries[i].transform.position);

            // Устанавливаем столбик в определённую точку
            spawnedStyles[i].transform.position = spawnedOvaries[i].End.position -
                (spawnedStyles[i].Begin.position - spawnedStyles[i].transform.position);

            // Устанавливаем рыльце в определённую точку
            spawnedStigmas[i].transform.position = spawnedStyles[i].End.position -
                (spawnedStigmas[i].Begin.position - spawnedStigmas[i].transform.position);

            for (int j = 0; j < spawnedReceptacles[i].PetalConnects.Length; j++)
            {
                // Устанавливаем лепесток в определённую точку
                spawnedPetals[i][j].transform.position = spawnedReceptacles[i].PetalConnects[j].position -
                    (spawnedPetals[i][j].Begin.position - spawnedPetals[i][j].transform.position);
            }

            for (int j = 0; j < spawnedReceptacles[i].SepalConnects.Length; j++)
            {
                // Устанавливаем чашелистик в определённую точку
                spawnedSepals[i][j].transform.position = spawnedReceptacles[i].SepalConnects[j].position -
                    (spawnedSepals[i][j].Begin.position - spawnedSepals[i][j].transform.position);
            }

            for (int j = 0; j < spawnedReceptacles[i].FilamentConnects.Length; j++)
            {
                // Устанавливаем нить в определённую точку
                spawnedFilaments[i][j].transform.position =
                    spawnedReceptacles[i].FilamentConnects[j].position -
                    (spawnedFilaments[i][j].Begin.position - spawnedFilaments[i][j].transform.position);
            }

            for (int j = 0; j < spawnedAnthers[i].Count; j++)
            {
                // Устанавливаем пыльник в определённую точку
                spawnedAnthers[i][j].transform.position =
                    spawnedFilaments[i][j].End.position -
                    (spawnedAnthers[i][j].Begin.position - spawnedAnthers[i][j].transform.position);
            }
        }
    }
}

