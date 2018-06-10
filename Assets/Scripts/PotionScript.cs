using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PotionScript : MonoBehaviour {

    public List<GameObject> inputs;
    public List<GameObject> outputs;

    private SpriteRenderer spriteR;

    public Image image;


    // Use this for initialization
    void Start () {
        foreach (GameObject gameObject in inputs)
        {
            gameObject.SetActive(false);
        }

        foreach (GameObject gameObject in outputs)
        {
            gameObject.SetActive(false);
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnMouseDown()
    {
        foreach (GameObject gameObject in inputs)
        {
            gameObject.SetActive(true);
        }

        foreach (GameObject gameObject in outputs)
        {
            gameObject.SetActive(true);
            spriteR = gameObject.GetComponent<SpriteRenderer>();
            Sprite sprite = spriteR.sprite;
            Sprite newSprite = Sprite.Create(sprite.texture, sprite.rect, sprite.pivot);
            image.sprite = newSprite;
        }
    }


}
