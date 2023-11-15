using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loot : MonoBehaviour
{
    [SerializeField] private SpriteRenderer sr;
    [SerializeField] private BoxCollider2D thecollider;
    [SerializeField] private float moveSpeed;

    private Item item;
    public void Initalize(Item item)
    {
        this.item = item;
        sr.sprite = item.image;
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {

         bool canadd =   InVentorymannger.instance.AddItem(item);
            if (canadd)
            {
                StartCoroutine(MoveAndCollect(other.transform));
            }
        }
    }
    private IEnumerator MoveAndCollect(Transform target)
    {

        Destroy(thecollider);
        while (transform.position != target.position)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);

            yield return 0;
        }

       Destroy(gameObject);
    }


}
