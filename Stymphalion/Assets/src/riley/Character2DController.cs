/*
 * Filename: Character2DController.cs
 * Developer: Riley Doyle
 * Purpose: Controls player movement
 * Attached to the player object
 */


using UnityEngine;
using UnityEngine.Events;


/// <summary>
/// Member Variables
/// <list type = "bullet">
/// <item>animator</item>
/// <item>m_MovementSpeed</item>
/// <item>m_rigidbody</item>
/// </list>
/// </summary>
public class CharacterMovement2D : MonoBehaviour
{



    public Animator animator;
    public float m_MovementSpeed = 1;
    public float m_JumpForce = 1;
    private Rigidbody2D m_rigidbody;

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
        //horizontal movement
        var movement = Input.GetAxis("Horizontal");
        transform.position += new Vector3(movement, 0, 0) * Time.deltaTime * m_MovementSpeed;

        //sets speed used in the Unity animator
        animator.SetFloat("Speed", Mathf.Abs(movement));

        //attack animaton

        //triggers attack animation
        if (Input.GetButtonDown("attack"))
        {
            animator.SetTrigger("Attack");
        }
        
        
        //flip the character when switching horizontal direction
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
        if (Input.GetButtonDown("Jump") && Mathf.Abs(_rigidbody.velocity.y) < 0.001f)
        {
            //animator.SetBool("isJumping", true);
            _rigidbody.AddForce(new Vector2(0, JumpForce), ForceMode2D.Impulse);
        
        }
*/    
        /*
                //Jump
                //Currently taken out because of problems with the animation
                if (Input.GetButtonDown("Jump") && Mathf.Abs(m_rigidbody.velocity.y) < 0.001f)
                {
                    //animator.SetBool("isJumping", true);
                    m_rigidbody.AddForce(new Vector2(0, JumpForce), ForceMode2D.Impulse);

                }
        */
    }


}