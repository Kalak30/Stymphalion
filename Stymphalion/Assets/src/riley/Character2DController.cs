using UnityEngine;

public class Character2DController : MonoBehaviour
{
    public Animator animator;
    public float MovementSpeed = 1;
    public float JumpForce = 1;

    private Rigidbody2D _rigidbody;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        var movement = Input.GetAxis("Horizontal");
        transform.position += new Vector3(movement, 0, 0) * Time.deltaTime * MovementSpeed;

        animator.SetFloat("Speed", Mathf.Abs(movement));

        //flip the character
        Vector3 characterScale = transform.localScale;
        if (movement < 0)
        {
            characterScale.x = -1;
        }
        if (movement > 0)
        {
            characterScale.x = 1;
        }
        transform.localScale = characterScale;

        //jump
        if (Input.GetButtonDown("Jump") && Mathf.Abs(_rigidbody.velocity.y) < 0.001f)
        {
            animator.SetBool("IsJumping", true);
            _rigidbody.AddForce(new Vector2(0, JumpForce), ForceMode2D.Impulse);
        
        }
        if (Input.GetButtonUp("Jump") && Mathf.Abs(_rigidbody.velocity.y) > 0.001f)
        {
            animator.SetBool("IsJumping", false);
        }
    }

}
