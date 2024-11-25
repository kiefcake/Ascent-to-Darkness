using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class PlayerInventory : MonoBehaviour
{
    public int NumberOfOrbs { get; private set; }
    public UnityEvent<PlayerInventory> OnOrbCollected;

    public void OrbCollected()
    {
        //increment the number of orbs
        NumberOfOrbs++;
        OnOrbCollected.Invoke(this);

        //check if 20 orbs have been collected
        if (NumberOfOrbs >= 20)
        {
            //load the victory scene
            SceneManager.LoadScene("VictoryScene");
        }
    }
}
