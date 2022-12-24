using UnityEngine;
using UnityEngine.UI;

public class OverlayAmmo : MonoBehaviour
{
    public Image mag;
    public CanvasGroup ammoHub;
    public Image hub;
    public GameObject ammoDisplay;

    [SerializeField]
    private Canvas canvas;

    [SerializeField]
    private GameObject rightHand;

    // Might not use group for this but will see as don't want my whole image backgroud to be seen otherwise i change in photoshop
    private short transparent = 0;
    private float full = 1;
    // TODO: will be an amount I use to get off player later
    private float magAmout = 5;
    
    private void Awake()
    {
        if (rightHand.transform.childCount == 0)
        {

            //ammoHub.alpha = 0;
            //Fades it differently but now in group though and children don't fade with it
            //hub.CrossFadeAlpha(0, 2f, false);
            
            
        }
        CreateMags();
    }

    // Update is called once per frame
    void Update()
    {
        if (rightHand.transform.childCount == 0 && ammoHub.alpha != 0)
        {
            // TODO: will make it fade in and out
            //Fade();
        }
    }

    private void Fade()
    {

        ammoHub.alpha = Mathf.MoveTowards(ammoHub.alpha, 0, 0.1f * Time.fixedDeltaTime);

        //fade to right doesn't work well
        //ammoHub.transform.position = new Vector3(Mathf.MoveTowards(ammoHub.transform.position.x, 500, 1f * Time.smoothDeltaTime), ammoHub.transform.position.y, 0f);
    }

    private void CreateMags() 
    { 

        // Might need to create for both mag and magAmmount
        for (int i = 0; i < magAmout; i++)
        {
            GameObject imgObject = new GameObject($"mag{i + 1}");

            RectTransform trans = imgObject.AddComponent<RectTransform>();
            imgObject.SetActive(true);
            trans.transform.SetParent(hub.transform);
            trans.localScale = Vector3.one;

            trans.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Bottom, 0, 0);

            trans.anchoredPosition = new Vector2(mag.rectTransform.anchoredPosition.x - 29f, 28.1f);
            trans.sizeDelta = new Vector2(24, 45);

            Image image = imgObject.AddComponent<Image>();
            image.sprite = mag.sprite;
        }
    }
    private void DecreaseMagAmmo() { }

    private void AmmoLow() { }
}
