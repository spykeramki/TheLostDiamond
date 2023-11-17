using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [HideInInspector]
    public bool isPlayerDiving = false;

    private bool _isPlayerDead = false;

    private Vector3 _cameraOffsetPos;

    public Transform viewCam;

    public Transform submarine;

    public List<CollectableCtrl> collectables;

    [SerializeField]
    private PlayerCtrl playerCtrl;
    public PlayerCtrl PlayerCtrl
    {
        get {
                if (playerCtrl.gameObject.activeInHierarchy)
                {
                    return playerCtrl; 
                }
            else
            {
                return null;
            }
            }
    }

    private Transform _scubaDiverTransform;

    public bool IsPlayerDead
    {
        get { return _isPlayerDead; }
        set { _isPlayerDead = value; }
    }

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        _scubaDiverTransform = playerCtrl.gameObject.transform;
    }

    private void Start()
    {
        _cameraOffsetPos =  viewCam.position - _scubaDiverTransform.position;
        
    }

    private void LateUpdate()
    {
        if (isPlayerDiving)
        {
            viewCam.position = _scubaDiverTransform.position + _cameraOffsetPos;
        }
        else
        {
            viewCam.position = submarine.position + _cameraOffsetPos;
        }
    }

    public CollectableCtrl GetCollectableNearPlayer()
    {
        for (int i = 0; i<collectables.Count; i++)
        {
            if (collectables[i].gameObject.activeInHierarchy && Vector3.Distance(collectables[i].CollectableTransform.position, _scubaDiverTransform.position) < 20f)
            {
                return collectables[i];
            }
        }
        return null;
    }

    public void RemoveCollectableFromList(CollectableCtrl reqCollectable)
    {
        foreach(CollectableCtrl collectable in collectables)
        {
            if(GameObject.ReferenceEquals(reqCollectable.gameObject, collectable.gameObject))
            {
                collectables.Remove(collectable);
            }
        }
    }

    public void SetActivenessOfDependentCollectible(CollectableCtrl reqCollectable)
    {
        foreach (CollectableCtrl collectable in collectables)
        {
            if (collectable.CollectableToUnlock == reqCollectable.Type)
            {
                collectable.gameObject.SetActive(true);
            }
        }
    }


}
