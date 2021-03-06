/*
 * Filename: CharacterMovement2D.cs
 * Developer: Riley Doyle
 * Purpose: Controls player movement
 * 
 */


using UnityEngine;
using UnityEngine.Events;


/// <summary>
/// Member Variables
/// <list type = "bullet">
/// <item>animator</item>
/// <item>m_MovementSpeed</item>
/// <item>m_JumpForce</item>
/// <item>m_rigidbody</item>
/// </list>
/// </summary>
public class BossScenePlayer : MonoBehaviour
{

    public Animator animator;
    public float JumpForce = 1;

    public float m_MovementSpeed = 1;
    public float m_JumpForce = 1;
    private Rigidbody2D m_rigidbody;
    public bool m_Attack;


    /// <summary>
    /// Function finds the rigidnody of the player object its attached to
    /// </summary>
    private void Start()
    {
        m_rigidbody = GetComponent<Rigidbody2D>();
    }


    /// <summary>
    /// Controls the movement and actions of the character
    /// </summary>
    private void Update()
    {
        var movement = Input.GetAxis("Horizontal");
        transform.position += new Vector3(movement, 0, 0) * Time.deltaTime * m_MovementSpeed;

        animator.SetFloat("Speed", Mathf.Abs(movement));

        //attack animaton

        if (Input.GetButtonDown("attack"))
        {
            animator.SetTrigger("Attack");
        }

        if (Input.GetButtonDown("dodge"))
        {
            animator.SetTrigger("Dodge");
        }


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
/*
        //jump
        if (Input.GetButtonDown("Jump") && Mathf.Abs(m_rigidbody.velocity.y) < 0.001f)
        {
            //animator.SetBool("isJumping", true);
            m_rigidbody.AddForce(new Vector2(0, JumpForce), ForceMode2D.Impulse);
        
        }
*/    
    }


}
