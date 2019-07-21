using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JetPack : MonoBehaviour {

    [Range(0, 50)] [SerializeField] private float m_JetPackForce = 15f; // The force applied to the jet pack.
    [SerializeField] private float m_FuelAmount = 900;                  // The max amount of fuel stored.
    [SerializeField] private float m_FuelCost = 8f;                     // The amount of fuel used when activated.
    [SerializeField] private float m_RefuelMultiplier = 5f;             // How fast the jet pack refuels.

    private CharacterController controller; // Controller for moving the character.
    private bool m_HeldJump = false;        // Used for checking if jumped is being held.
    private float m_FuelAmountCounter = 0;  // Counter for the fuel stored.

    //TODO: Make these modular in the future
    //public Image fuelBar;                       // Reference to the fuel bar above the player's head.
    //public GameObject fuelFumesEffect;          // Reference to the fuel fumes effect when the player is activating the jet pack.


    private void Awake()
    {
        controller = GetComponent<CharacterController>();

        // Set fuel amount counter to the current fuel amount.
        m_FuelAmountCounter = m_FuelAmount;

        // Hide fuel fumes until player activates jet pack.
        //fuelFumesEffect.SetActive(false);
    }

    private void Update()
    {
        /*PROBLEM: heldJump is triggered but ActivateJetPack doesn't apply its force's sometimes?
          SOLUTION: This issue was caused by BoxCollider2D. Not sure why this component has problems.
                    Switched BoxCollider2D on the Tile object with four EdgeCollider2D components.
        */

        // Check if jump is being held down.
        if (InputManager.instance.Key("Jump"))
        {
            m_HeldJump = true;
        }

        // If character is grounded, then refuel the jetpack.
        if (controller.m_Grounded)
        {
            StartCoroutine(Refuel());
        }
    }


    private void FixedUpdate()
    {
        // If jump is being held down ...
        if (m_HeldJump)
        {
            // ... then activate the jetpack.
            m_HeldJump = false;
            ActivateJetpack();
        }
    }

    private void ActivateJetpack()
    {
        if (m_FuelAmountCounter > 0)
        {
            // Decrement m_FuleAmountCounter by m_FuelCost.
            m_FuelAmountCounter -= m_FuelCost;

            // Update fuelBar to refelect current m_FuelAmount.
            //fuelBar.fillAmount = m_FuelAmountCounter / m_FuelAmount;

            //TODO: Maybe make this a SO Event?
            // Turn on fuel fumes effect.
            //fuelFumesEffect.SetActive(true);

            // Add ascending force to current velocity.
            if (controller.Rb.velocity.y < 0)
                controller.Rb.AddForce(new Vector2(0, -controller.Rb.velocity.y + m_JetPackForce));
            // Add force to counteract descending velocity.
            else
                controller.Rb.AddForce(new Vector2(0, m_JetPackForce));
        }
        else
        {
            //TODO: Maybe make this a SO Event?
            // Turn off fuel fumes effect.
            //fuelFumesEffect.SetActive(false);
        }
    }

    private IEnumerator Refuel()
    {
        // Refuel the jet pack based on m_FuelCost and m_RefuleMultiplier.
        if (m_FuelAmountCounter < m_FuelAmount)
            m_FuelAmountCounter += m_FuelCost * m_RefuelMultiplier;

        // Fill fuel bar image.
        //fuelBar.fillAmount += m_FuelCost / m_FuelAmount * m_RefuelMultiplier;
        yield return null;
    }
}
