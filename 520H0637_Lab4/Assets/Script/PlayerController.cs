using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Boundary
{
    public float xMin, xMax, zMin, zMax;
}

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float tilt;
    public Boundary bound;

    public GameObject Bullet;
    public Transform Shot;
    public float fireRate;
    private float timeRate;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Fire1") && Time.time>timeRate)
        {
            timeRate = Time.time + fireRate;
            Instantiate(Bullet, Shot.position, Shot.rotation);
        }
    }

    void FixedUpdate()
    {
        var horizontal = Input.GetAxis("Horizontal");
        var vertical = Input.GetAxis("Vertical");

        GetComponent<Rigidbody>().velocity = new Vector3(horizontal, 0, vertical)*speed;

        transform.position = new Vector3(
            Mathf.Clamp(transform.position.x, bound.xMin, bound.xMax),
            0,
            Mathf.Clamp(transform.position.z, bound.zMin, bound.zMax)
            );

        transform.rotation = Quaternion.Euler(new Vector3(0, 0, GetComponent<Rigidbody>().velocity.x * -tilt));
    }
}
