using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(BoxCollider2D))]

public class Laundry: MonoBehaviour
{
    float speed = 0.1f;

    public void Initialize(Sprite _sprite) {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        sr.sprite = _sprite;
        GetComponent<BoxCollider2D>().isTrigger = true;
      }
    public virtual void OnCollect() {
        //collect coin
        speed = 10f;
        GetComponent<BoxCollider2D>().enabled = false;
        StartCoroutine(RemoveSelf());

    }

    public virtual void Animate() {
        transform.RotateAroundLocal(Vector3.up,speed);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.gameObject.tag == "Player")
        {
            OnCollect();
        }
    }
    IEnumerator RemoveSelf()
    {
        LaundryManager lm = transform.parent.GetComponent<LaundryManager>();
        lm.notifyStart();
        yield return new WaitForSeconds(0.5f);
        lm.UpdateLaundryCount(this);
        Destroy(this.gameObject);
    }

    //Handles Event System


}
