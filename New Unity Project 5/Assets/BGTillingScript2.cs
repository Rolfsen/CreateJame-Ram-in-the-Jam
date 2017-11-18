using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGTillingScript2 : MonoBehaviour {
    SpriteRenderer sprite;
    // Use this for initialization
    void Start () {
        // Get the current sprite with an unscaled size
        sprite = GetComponent<SpriteRenderer>();
        Vector2 spriteSize = new Vector2(sprite.bounds.size.x / (transform.localScale.x*10), sprite.bounds.size.y / (transform.localScale.y));

        // Generate a child prefab of the sprite renderer
        GameObject childPrefab = new GameObject();
        SpriteRenderer childSprite = childPrefab.AddComponent<SpriteRenderer>();
        childPrefab.transform.position = transform.position;
        childSprite.sprite = sprite.sprite;

        // Loop through and spit out repeated tiles
        GameObject child;
        for (int i = 1, l = (int)Mathf.Round(sprite.bounds.size.x); i < l; i++)
        {
            child = Instantiate(childPrefab) as GameObject;
            child.transform.position = transform.position - (new Vector3(spriteSize.x, 0, 0) * i);
            child.transform.parent = transform;
        }

        // Set the parent last on the prefab to prevent transform displacement
        childPrefab.transform.parent = transform;

        // Disable the currently existing sprite component since its now a repeated image
        sprite.enabled = false;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
