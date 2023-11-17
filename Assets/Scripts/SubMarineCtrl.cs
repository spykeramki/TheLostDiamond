using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SubMarineCtrl : MonoBehaviour
{
    [SerializeField]
    private GameObject scubaDiver;
    [SerializeField]
    public GameObject releaseButtonGo;
    [SerializeField]
    public Button releaseButton;

    public float horizontalSpeed = 30f;
    public float verticalSpeed = 30f;

    private SpriteRenderer spriteRenderer;


    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        scubaDiver.SetActive(false);
        releaseButton.onClick.AddListener(() => ReleaseOrBoardDiver(true));
    }

    public void ReleaseOrBoardDiver(bool release)
    {
        scubaDiver.SetActive(release);
        releaseButtonGo.SetActive(!release);
        GameManager.instance.isPlayerDiving = release;
        scubaDiver.transform.localPosition = Vector3.zero;
    }
    void Update()
    {
        if (!GameManager.instance.isPlayerDiving)
        {
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
        }
    }

    private void MoveScubaDiverHorizontally(bool isForward)
    {
        int direction = isForward ? 1 : -1;
        spriteRenderer.flipX = !isForward;
        //transform.localScale = new Vector3(direction * scale.x, scale.y, scale.z); 
        transform.Translate(direction * horizontalSpeed * Time.deltaTime, 0f, 0f);
        //totalOceanBg.Translate(direction * horizontalSpeed * Time.deltaTime, 0f, 0f);
    }

    private void MoveScubaDiverVertically(bool isUpward)
    {
        int direction = isUpward ? 1 : -1;
        transform.Translate(0f, direction * verticalSpeed * Time.deltaTime, 0f);
        //totalOceanBg.Translate(0f, direction * verticalSpeed * Time.deltaTime, 0f);
    }
}
