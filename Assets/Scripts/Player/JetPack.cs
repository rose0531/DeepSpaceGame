using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(PlayerController))]
public class JetPack : MonoBehaviour {
    [SerializeField]
    private float jetPackForce;                  // The force applied to the jet pack.
    [SerializeField]
    private float fuelCost;                      // The amount of fuel used when activated.
    [SerializeField]
    private float fuelAmount;                    // The max amount of fuel stored.
    [SerializeField]
    private float refuelMultiplier;              // How fast the jet pack refuels.

    private bool activateJetPack;

    private float fuelAmountCounter;            // Counter for the fuel stored.

    //TODO: Make these modular in the future
    public Image fuelBar;                       // Reference to the fuel bar above the player's head.
    public GameObject fuelFumesEffect;          // Reference to the fuel fumes effect when the player is activating the jet pack.


    public PlayerController player;


    private void Awake()
    {
        // Multiply fuelAmount by 60 to scale up fuelAmount so we can slowly decrement fuelAmount by fuelCost every frame.
        fuelAmount *= 60;
        fuelAmountCounter = fuelAmount;

        // Hide fuel fumes until player activates jet pack.
        fuelFumesEffect.SetActive(false);
    }

    private void Update()
    {
        /*PROBLEM: Jump/HeldJump are triggered but activateJetPack/jump don't apply their force's sometimes?
          SOLUTION: This issue was caused by BoxCollider2D. Not sure why this component has problems.
                    Switched BoxCollider2D on the Tile object with four EdgeCollider2D components.
          */
         
        // Activate jetpack if user is holding jump and the player is not grounded.
        activateJetPack = player.input.HeldJump && !player.characterStats.IsGrounded;

        if (activateJetPack) Debug.Log("Jetpack activated");

        // If player is grounded then refuel his jetpack if needed.
        if (player.characterStats.IsGrounded)
        {
            StartCoroutine(Refuel());
        }
    }

    private void FixedUpdate()
    {
        if (activateJetPack && fuelAmountCounter > 0)
        {            
            // Decrement fuleAmountCounter by fuelCost.
            fuelAmountCounter -= fuelCost;

            // Update fuelBar to refelect current fuel amount.
            fuelBar.fillAmount = fuelAmountCounter / fuelAmount;

            //TODO: Maybe make this a SO Event?
            // Turn on fuel fumes effect.
            fuelFumesEffect.SetActive(true);

            // Add ascending force.
            if (player.Rb.velocity.y < 0)
            {
                // Apply the jetpack force to the character
                player.ApplyForce(0, -1 * player.Rb.velocity.y + jetPackForce + 5f);
            }
            // Add descending force.
            else
            {
                player.ApplyForce(0, jetPackForce);
            }
        }
        else
        {
            //TODO: Maybe make this a SO Event?
            // Turn off fuel fumes effect.
            fuelFumesEffect.SetActive(false);
        } 
    }

    private IEnumerator Refuel()
    {
        // Refuel the jet pack based on fuel cost and refule multiplier.
        if (fuelAmountCounter < fuelAmount)
            fuelAmountCounter += fuelCost * refuelMultiplier;

        // Fill fuel bar image.
        fuelBar.fillAmount += fuelCost / fuelAmount * refuelMultiplier;
        yield return null;
    }
}
