Prefab for Turn-Based Enemy:
Set a basic sprite as Enemy
When combat starts, match the sprite with whatever the player encountered in the main world.
Battle system sets stats and moves of the enemy accordingly.
Enemy has a state design pattern to help determine which attack it should use, currently only based on it's own health.
Basically, take whatever enemy the player encounters in the main world, create a copy of it, then BattleSystem sets it's stats
and manages it in combat.
Variables:
Max Health - at most 300
Current Health
Behavior - 1 (aggressive) or 2 (defensive)
