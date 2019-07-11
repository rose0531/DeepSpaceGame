using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


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


    public CharacterController character;


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
        activateJetPack = character.input.HeldJump && !character.characterStats.IsGrounded;
    }

    private void FixedUpdate()
    {
        if (activateJetPack && fuelAmountCounter > 0)
        {            
            // Decrement fuleAmountCounter by fuelCost.
            fuelAmountCounter -= fuelCost;

            // Update fuelBar to refelect current fuel amount.
            fuelBar.fillAmount = fuelAmountCounter / fuelAmount;

            // Turn on fuel fumes effect.
            fuelFumesEffect.SetActive(true);

            // Add ascending force.
            if (character.Rb.velocity.y < 0)
            {
                // Apply the jetpack force to the character
                float yVelocity = character.Rb.velocity.y;
                character.ApplyForce(0, -1 * yVelocity + jetPackForce + 5f);
            }
            // Add descending force.
            else
            {
                character.ApplyForce(0, jetPackForce);
            }
        }
        else
        {
            // Turn off fuel fumes effect.
            fuelFumesEffect.SetActive(false);
        }

        if (character.characterStats.IsGrounded)
        {
            StartCoroutine(Refuel());
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
