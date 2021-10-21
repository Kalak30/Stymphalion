/*
 * FileName: ShopNPC.cs
 * Developer: Jon Kopf
 * Purpose: Provides abstraction for a ShopNPC
 */


/// <summary>
/// Provides an abstraction for a ShopNPC
/// </summary>
public class ShopNPC : NPC
{

    /// <summary>
    /// Handles what happens when player is interacting with this intractable.
    /// Is automatically called
    /// </summary>
    public override void TouchingInteractable()
    {
        throw new System.NotImplementedException();
    }
}