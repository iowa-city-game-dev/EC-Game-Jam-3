using System.Collections;
using Managers;
using UnityEngine;

namespace Scripts
{
    [RequireComponent(typeof(SpriteRenderer))]
    [RequireComponent(typeof(BoxCollider2D))]
    public class Laundry : MonoBehaviour
    {
        float _speed = 0.1f;
        private LaundryManager _laundryManager;

        public void Initialize(Sprite sprite)
        {
            SpriteRenderer sr = GetComponent<SpriteRenderer>();
            sr.sprite = sprite;
            GetComponent<BoxCollider2D>().isTrigger = true;

            // We can do this here, so we move the expensive calls out of other palces
            _laundryManager = transform.parent.GetComponent<LaundryManager>();
        }

        public virtual void OnCollect()
        {
            //collect coin
            _speed = 10f;
            GetComponent<BoxCollider2D>().enabled = false;
            StartCoroutine(RemoveSelf());
        }

        public virtual void Animate()
        {
            transform.RotateAroundLocal(Vector3.up, _speed);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.transform.gameObject.tag == "Player")
            {
                OnCollect();
            }
        }

        private IEnumerator RemoveSelf()
        {
            _laundryManager.NotifyStart();
            yield return new WaitForSeconds(0.5f);
            _laundryManager.UpdateLaundryCount(this);
            Destroy(this.gameObject);
        }

        //Handles Event System
    }
}