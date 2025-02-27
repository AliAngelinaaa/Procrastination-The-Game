using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Energy : MonoBehaviour
{
    public static Energy Instance { get; private set; }

    [SerializeField] private float startingEnergy;
    public float currentEnergy; //{get; private set;}
    public PlayerMovement player;
    public bool playerAlive = true;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        Instance = this;

        currentEnergy = startingEnergy;
    }

    public void LoseEnergy(float _energy) {
        currentEnergy = Mathf.Clamp(currentEnergy - _energy, 0, startingEnergy);

        if(currentEnergy > 0) {
            player.Hurt();
        }
        else {
            player.Die();
            playerAlive = false;
        }
    }

    public void GainEnergy(float amount) {
        currentEnergy = Mathf.Clamp(currentEnergy + amount, 0, startingEnergy);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (currentEnergy <= 0 && playerAlive == false) {
            currentEnergy = startingEnergy;
            playerAlive = true;
            player.Restart();
        }
        if(player == null) {
            player = FindObjectOfType<PlayerMovement>();
        }
    }

    void noEnergyLeft() {
        GameManager.Instance.lose = true;
    }
}
