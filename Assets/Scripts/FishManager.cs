using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishManager : MonoBehaviour
{
    public static FishManager instance;

    [SerializeField]
    private Vector3 fishRange;
    [SerializeField]
    private List<GameObject> fishPrefabList;
    [SerializeField]
    private Transform fishHolder;

    private Bounds _fishBounds;
    public Bounds FishBounds
    {
        get { return _fishBounds; }
    }

    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(instance);
        }
    }

    private void Start()
    {
        _fishBounds = new Bounds(this.transform.position, fishRange);

        for(int i = 0; i < 50; i++)
        {
            for (int j = 0; j< fishPrefabList.Count; j++)
            {
                Instantiate(fishPrefabList[j], new Vector3(Random.Range(_fishBounds.min.x, _fishBounds.max.x), Random.Range(_fishBounds.min.y, _fishBounds.max.y), 100f), Quaternion.identity, fishHolder);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
