Documentation For Player Prefab 
Riley Doyle

Overview:
The player prefab is made to make easy use of a player object that can already move and interact with enemies

Components:
-Transform:
	-Allows for the movement of the player object in the scene
-SpriteRenderer:
	-Makes for easy changes to the object's color
-BoxCollider2D:
	-Makes for collision detection in the scene
-RigidBody2D:
	-Allows physics to be aplied on the object
-CharacterMovement2D.cs:
	-Member Variables:
		-public static int m_maxHealth;
    		-int m_currentHealth;
	-Methods:
		-Start()
			-finds rigidbody of the object
		-Update()
			-controlls the movement of the object

-Player.cs:
	-Member Variables:
		-public Animator animator;
    		-public float m_MovementSpeed = 1;
    		-public float m_JumpForce = 1;
	-Methods:
		-Start()
			-sets max health to current health
		-GainHealth(int)
			-give the object health
		-TakeDamage(int)
			-lowers the health of the object
		-Die()
			-derenders player when healtn reaches zero
		-GetCurrentHealth()
			-returns current health
		-GetMaxHealth()
			-returns max health

-PlayerCombat.cs:
	-Member Variables:
		-public Transform m_attackPoint;
    		-public LayerMask m_enemyLayers;
    		-public float m_attackRange = 0.5f;
    		-public int m_attackDamage = 10;
	-Methods:
		-Update()
			-triggers attack on button down
		-Attack()
			-finds enemies that collide with the player's attack and applies damage
		-OnDrawGizmosSelected()
			-draws a wireframe in unity editor so the programmer can see the range of the objects attack
-Animator:
	-Unity object that allows for animation of the object