
using UnityEngine;


public class background : MonoBehaviour
{
    [Range(-1f, 1f)]
    public float scrollspeed = 0.5f;
    public float scollaxis = 0;
    private float offset;
    private Material mat;
    void Start()
    {
        mat = GetComponent<Renderer>().material;

    }


    void Update()
    {


        float scollspeednow = 0;
        if (Input.GetAxisRaw("Horizontal") > 0)
        { scollspeednow = scrollspeed; }
        else if (Input.GetAxisRaw("Horizontal") < 0)
        { scollspeednow = scrollspeed * -1; }
        else
        {
            scollspeednow = 0.02f;
        }
        if (Mathf.Abs(Movement.xspeed) < 0.05)
        {
            scollspeednow = 0.02f;

        }
        offset += (Time.deltaTime * scollspeednow) / 10f;
        mat.SetTextureOffset("_MainTex", new Vector2(offset, 0));
    }
}
