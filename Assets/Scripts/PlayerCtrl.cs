using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCtrl : MonoBehaviour
{
    [SerializeField]
    private SubMarineCtrl subMarineCtrl;
    [SerializeField]
    public Transform oceansurface;
    [SerializeField]
    public GameObject boardButtonGo;
    [SerializeField]
    public Button boardButton;
    [SerializeField]
    public GameObject actionButtonGo;
    [SerializeField]
    public Button actionButton;
    [SerializeField]
    public GameObject collectibleInfo;
    [SerializeField]
    public TextMeshProUGUI infoText;

    private float _distanceOfPlayerFromSurface = 0f;
    public float DistanceOfPlayerFromSurface
    {
        get { return _distanceOfPlayerFromSurface; }
    }

    public float horizontalSpeed = 30f;
    public float verticalSpeed = 30f;

    private SpriteRenderer _spriteRenderer;
    private Transform _subMarineGo;
    private CollectableCtrl _collectableNearby;

    // Start is called before the first frame update
    void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        boardButton.onClick.AddListener(OnClickBoardBtn);
        actionButton.onClick.AddListener(OnClickActionButton);
        _subMarineGo = subMarineCtrl.gameObject.transform;
    }

    private void OnClickBoardBtn()
    {
        subMarineCtrl.ReleaseOrBoardDiver(false);
    }

    // Update is called once per frame
    void Update()
    {
        GameManager gameManager = GameManager.instance;
        if (gameManager.isPlayerDiving)
        {
            _distanceOfPlayerFromSurface = Vector3.Distance(new Vector3(0f, oceansurface.position.y, 0f), new Vector3(0f, transform.position.y, 0f));


            boardButtonGo.SetActive(Vector3.Distance(transform.position, _subMarineGo.position) < 30f);
            

            if (Input.GetKey(KeyCode.D))
            {
                MoveScubaDiverHorizontally(isForward: true);
            }
            if (Input.GetKey(KeyCode.A))
            {
                MoveScubaDiverHorizontally(isForward: false);
            }
            if (Input.GetKey(KeyCode.W))
            {
                MoveScubaDiverVertically(isUpward: true);
            }
            if (Input.GetKey(KeyCode.S))
            {
                MoveScubaDiverVertically(isUpward: false);
            }

            _collectableNearby = gameManager.GetCollectableNearPlayer();
            //Debug.Log(collectableNearby + "collectableNearby");
            if(_collectableNearby != null)
            {
                actionButtonGo.SetActive(!_collectableNearby.IsRestrictedToCollect);
                if (_collectableNearby.IsRestrictedToCollect)
                {
                    infoText.text = _collectableNearby.CollectableMessage;
                }
                else
                {
                    infoText.text = "...";
                }
                collectibleInfo.SetActive(_collectableNearby.IsRestrictedToCollect);
            }
            else
            {
                if(actionButtonGo.activeInHierarchy || collectibleInfo.activeInHierarchy)
                {
                    actionButtonGo.SetActive(false);
                    collectibleInfo.SetActive(false);
                }
            }
        }
    }

    private void MoveScubaDiverHorizontally(bool isForward)
    {
        int direction = isForward ? 1 : -1;
        _spriteRenderer.flipX = !isForward;
        //transform.localScale = new Vector3(direction * scale.x, scale.y, scale.z); 
        transform.Translate(direction  * horizontalSpeed * Time.deltaTime, 0f, 0f); 
        //totalOceanBg.Translate(direction * horizontalSpeed * Time.deltaTime, 0f, 0f);
    }

    private void MoveScubaDiverVertically(bool isUpward)
    {
        int direction = isUpward ? 1 : -1;
        transform.Translate(0f, direction * verticalSpeed * Time.deltaTime, 0f);
        //totalOceanBg.Translate(0f, direction * verticalSpeed * Time.deltaTime, 0f); 
    }

    private void OnClickActionButton()
    {
        InventoryCtrl.instance.AddItemToInventory(_collectableNearby);
        GameManager.instance.SetActivenessOfDependentCollectible(_collectableNearby);
        _collectableNearby.gameObject.SetActive(false);
        //GameManager.instance.RemoveCollectableFromList(_collectableNearby);
    }

}
