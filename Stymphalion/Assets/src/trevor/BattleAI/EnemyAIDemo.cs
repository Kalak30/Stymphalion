/*
 * Filename: EnemyAIDemo.cs
 * Developer: Trevor McGeary
 * Purpose: Contains the behavior for the EnemyAI, and returns the AI move to the battle system.
 * 
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// EnemyAI(Demo) Class. Contains information for how the enemy
/// makes it's decision for which move to use.
/// </summary>
public class EnemyAIDemo : MonoBehaviour
{
    /// <summary>
    /// State super class
    /// Gives states properties and values
    /// </summary>
    public abstract class State
    {
        //public UnitDataClass test_Data;

        protected Enemy enemy;
        protected float current_Health_State;
        protected float health_Percentage_State;

        public double lower_Limit;
        protected double upper_Limit;

        //Properties
        public Enemy Enemy
        {
            get { return enemy; }
            set { enemy = value; }
        }

        public float Current_Health_State
        {
            get { return current_Health_State; }
            set { current_Health_State = value; }
        }

        public float Health_Percentage_State
        {
            get { return health_Percentage_State; }
            set { current_Health_State = value; }
        }

        /// <summary>
        /// Function to cause the enemy to take damage and potentially change state.
        /// Takes a float "amount".
        /// </summary>
        /// <param name="amount"></param>
        public abstract void TakeDamage(float amount);

    }

    /// <summary>
    /// HighState. Subclass of State. State for when enemy has 75% or more of it's 
    /// health remaining. Can transition into MidState or LowState.
    /// </summary>
    public class HighState : State
    {
        //Constructors
        public HighState(State state) : this(state.Health_Percentage_State, state.Enemy, state.Current_Health_State)
        {

        }

        public HighState(float current_Health_State, Enemy enemy, float health_Percentage_State)
        {
            this.current_Health_State = current_Health_State;
            this.health_Percentage_State = health_Percentage_State;
            this.enemy = enemy;
            
            Initialize();

        }

        private void Initialize()
        {
            lower_Limit = .75;
            upper_Limit = 1;
            //StateChangeCheck();
        }

        public override void TakeDamage(float amount)
        {
            current_Health_State -= amount;
            health_Percentage_State = current_Health_State / enemy.max_Health;
            StateChangeCheck();
        }

        private void StateChangeCheck()
        {
            //current_Health_State = enemy.current_Health;
            Debug.Log("State: " + enemy.State.GetType().Name);
            //Debug.Log("Current Health: "+ current_Health_State);
            //Debug.Log("Max Health: " + enemy.max_Health);
            health_Percentage_State = current_Health_State / enemy.max_Health;
            Debug.Log("Percentage: " + health_Percentage_State);
            Debug.Log("Lower limit " + lower_Limit);
            if(health_Percentage_State < .50)
            {
                enemy.State = new LowState(this);
            }
            if(health_Percentage_State < lower_Limit && health_Percentage_State > .50)
            {
                
                //Debug.Log("Tried to change state");
                if(enemy == null)
                {
                    Debug.Log("I am null");
                }
                enemy.State = new MidState(this);
                Debug.Log("New state: " + enemy.State.GetType().Name);
            }
        }
    }

    /// <summary>
    /// MidState. Subclass of State. State for when enemy has less than 75% of it's health
    /// but more than 50% of it's health. Can transition into LowState.
    /// </summary>
    public class MidState : State
    {
        //Constructors
        public MidState(State state) : this(state.Health_Percentage_State, state.Enemy, state.Current_Health_State)
        {
        }

        public MidState(float health_Percentage_State, Enemy enemy, float current_Health_State)
        {
            this.current_Health_State = current_Health_State;
            this.health_Percentage_State = health_Percentage_State;
            this.enemy = enemy;
            Initialize();
            
        }

        private void Initialize()
        {
            lower_Limit = .50;
            upper_Limit = .75;
            StateChangeCheck();
        }

        public override void TakeDamage(float amount)
        {
            current_Health_State -= amount;
            health_Percentage_State = current_Health_State / enemy.max_Health;
            StateChangeCheck();
        }

        private void StateChangeCheck()
        {
            //current_Health_State = enemy.current_Health;
            Debug.Log("State: " + enemy.State.GetType().Name);
            //Debug.Log("Current Health: " + current_Health_State);
            //Debug.Log("Max Health: " + enemy.max_Health);
            health_Percentage_State = current_Health_State / enemy.max_Health;
            Debug.Log("Percentage: " + health_Percentage_State);
            Debug.Log("Lower limit " + lower_Limit);
            if (health_Percentage_State < lower_Limit)
            {
                enemy.State = new LowState(this);
                Debug.Log("New state: " + enemy.State.GetType().Name);
            }
        }
    }

    /// <summary>
    /// LowState. Subclass of State. State for when enemy has less than
    /// 50 % of it's health left. Cannot transition.
    /// </summary>
    public class LowState : State
    {
        //Constructors
        public LowState(State state) : this(state.Health_Percentage_State, state.Enemy, state.Current_Health_State)
        {

        }

        public LowState(float health_Percentage_State, Enemy enemy, float current_Health_State)
        {
            this.current_Health_State = current_Health_State;
            this.health_Percentage_State = health_Percentage_State;
            this.enemy = enemy;
            Initialize();
        }

        private void Initialize()
        {
            lower_Limit = 0;
            upper_Limit = .50;
            StateChangeCheck();
            //current_Health_State = enemy.current_Health;
           

        }

        public override void TakeDamage(float amount)
        {
            current_Health_State -= amount;
            health_Percentage_State = current_Health_State / enemy.max_Health;
            StateChangeCheck();
        }

        private void StateChangeCheck()
        {
            Debug.Log("State: " + enemy.State.GetType().Name);
            //Debug.Log("Current Health: " + current_Health_State);
            //Debug.Log("Max Health: " + enemy.max_Health);
            health_Percentage_State = current_Health_State / enemy.max_Health;
            Debug.Log("Percentage: " + health_Percentage_State);
            Debug.Log("Lower limit " + lower_Limit);
            if(health_Percentage_State < lower_Limit || current_Health_State < 0)
            {
                Debug.Log("Enemy died");
            }
        }


    }

    /// <summary>
    /// Enemy class. Has a state and values that are used for calculations in State. 
    /// </summary>
    public class Enemy
    {
        private State state;
        public float max_Health;
        public float current_Health;
        public float health_Percentage;
        //public int behavior;
        //public int enemy_Type;
        //public float player_Health;
        //public float player_Health_Percentage;

        /// <summary>
        /// Constructor. Takes a float x that corresponds to the max health of the enemy.
        /// Also creates a new State.
        /// </summary>
        /// <param name="x"></param>
        public Enemy(float x)
        {

            max_Health = x;
            current_Health = max_Health;
            //Debug.Log("Enemy health: " + current_Health);
            //Debug.Log("Enemy max: " + max_Health);
            this.state = new HighState(max_Health, this, 1);
        }

        public State State
        {
            get { return state; }
            set { state = value; }
        }

        /// <summary>
        /// Calls the function for the state to calculate the damage it's taken.
        /// Takes a float "amount". Called by EnemyAIDemo.DamageCalc(x), where x is an amount of damage.
        /// </summary>
        /// <param name="amount"></param>
        public void TakeDamage(float amount)
        {
            //Debug.Log("Enemy took: " + amount + "damage");
            //Dynamic binding. What state enemy points to could vary.
            state.TakeDamage(amount);

        }
      

    }

    public float max_Health;
    public float current_Health;
    public float health_Percentage;
    public int behavior;
    public int enemy_Type;
    public float player_Health;
    public float player_Health_Percentage;
    public Enemy enemy;
    private BattleSystem battle_System;

    // Start is called before the first frame update
    void Start()
    {
        

    }

    private void Awake()
    {
       
        battle_System = FindObjectOfType<BattleSystem>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    /// <summary>
    /// Returns the number corresponding to which attack the enemy will use.
    /// </summary>
    /// <returns></returns>
    public int EnemyMove()
    {        
        //Debug.Log(enemy.State.GetType().Name);
        behavior = battle_System.unitArray[0].behavior;
        player_Health = battle_System.unitArray[1].current_Health;
        player_Health_Percentage = player_Health / battle_System.unitArray[1].max_Health;
        player_Health_Percentage = battle_System.unitArray[1].current_Health / battle_System.unitArray[1].max_Health;
        current_Health = battle_System.unitArray[0].current_Health;
        health_Percentage = current_Health / max_Health;
        enemy.current_Health = current_Health;
        enemy.current_Health = battle_System.unitArray[0].current_Health;
        //Debug.Log("PHP: " + player_Health_Percentage);
        //behavior 1: aggressive behavior;
        if (behavior == 1)
        {
            //if(player_Health_Percentage >= .80 && health_Percentage >= .75)
            if(enemy.State is HighState)
            {
                
                //Debug.Log("Still in HighState: " + enemy.State.GetType().Name);
                //return 0;
            }
            
            if(enemy.State is MidState)
            {
                //Debug.Log("In MidState: " + enemy.State.GetType().Name);

            }
            if(enemy.State is LowState)
            {
                //Debug.Log("In LowState: " + enemy.State.GetType().Name);
            }
           
            if(player_Health_Percentage >= .80 && enemy.State is HighState)
            {
                //Debug.Log("Did this one");
                return 3;
            }
            
            else if(player_Health_Percentage >= .80 && enemy.State is MidState)
            {
                return 2;
            }
            else if (player_Health_Percentage < .80 && player_Health_Percentage >= .45 && enemy.State is HighState)
            {
                return 3;
            }
            else if (player_Health_Percentage < .80 && player_Health_Percentage >= .45 && enemy.State is MidState)
            {
                return 2;
            }
            else if (player_Health_Percentage < .45 && enemy.State is MidState)
            {
                //Debug.Log("Tried to do #5");
                return 3;
            }
            else if (player_Health_Percentage < .45 && enemy.State is LowState)
            {
                return 1;
            }
            else if (player_Health_Percentage <= .50 && enemy.State is LowState)
            {
                return 2;
            }
            else if (player_Health_Percentage > .50 && enemy.State is LowState)
            {
                return 3;
            }
        }
        //behavior 2: defensive behavior
        else if (behavior == 2)
        {
            if (player_Health_Percentage >= .80 && enemy.State is HighState)
            {
                return 2;
            }
            else if (player_Health_Percentage >= .80 && enemy.State is MidState)
            {
                return 0;
            }
            else if (player_Health_Percentage < .80 && player_Health_Percentage >= .45 && enemy.State is HighState)
            {
                return 3;
            }
            else if (player_Health_Percentage <= .80 && player_Health_Percentage >= .45 && enemy.State is MidState)
            {
                return 2;
            }
            else if (player_Health_Percentage < .45 && !(enemy.State is LowState))
            {
                return 1;
            }
            else if (player_Health_Percentage < .45 && enemy.State is LowState)
            {
                return 0;
            }
        }
        return 5;
    }
    /// <summary>
    /// Gives damage to the enemy, potentially changing it's state. Passes in a float x.
    /// Called in BattleSystem.cs, passing in a damage value.
    /// </summary>
    /// <param name="x"></param>
    public void DamageCalc(float x)
    {
        enemy.TakeDamage(x);
    }
    /// <summary>
    /// Creates a new enemy. Did this as a workaround to a problem, so that enemy can be public,
    /// and starts with max_Health assigned correctly. Called in BattleSystem.cs
    /// </summary>
    public void CreateEnemy()
    {
        enemy = new Enemy(battle_System.unitArray[0].max_Health);
    }
    
}