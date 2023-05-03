using UnityEngine;

public class Jump : MonoBehaviour
{
    Rigidbody2D m_Rigidbody;
    readonly Vector2 force = new Vector2(50, 50);
    bool inAir;

    private void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Space) && !inAir)
        {
            inAir = true;
            m_Rigidbody.AddForce(force);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
            inAir = false;
    }
}