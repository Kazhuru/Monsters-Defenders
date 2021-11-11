using UnityEngine;

public class DefenderSpawner : MonoBehaviour
{
    [SerializeField] GameUnit knightPrefab;
    [SerializeField] GameUnit magePrefab;
    [SerializeField] GameUnit roguePrefab;
    [SerializeField] float correctZoneSpriteSize = 1.2f;
    [SerializeField] float incorrectZoneSpriteSize = 0.7f;

    private SpriteRenderer spriteRenderer;
    private GameStatus gameStatus;
    
    private bool spawnMode;
    private bool onCorrectSpawnZone;
    private string selectedDefender;
    private int currentSelectionCost;

    void Start()
    {
        spawnMode = false;
        onCorrectSpawnZone = false;
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        gameStatus = FindObjectOfType<GameStatus>();
        spriteRenderer.material.SetFloat("_GrayscaleAmount", 1f);
    }

    void Update()
    {
        if (spriteRenderer.sprite != null)
        {
            SetPositionToMouse();
            if (onCorrectSpawnZone)
            {
                gameObject.transform.localScale = new Vector2(correctZoneSpriteSize, correctZoneSpriteSize);
            }
            else
            {
                gameObject.transform.localScale = new Vector2(incorrectZoneSpriteSize, incorrectZoneSpriteSize);
            }
        }

        if(Input.GetMouseButtonUp(1))
        {
            RemoveSprite();
        }
    }

    private void SetPositionToMouse()
    {
        Vector2 vector = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = vector;
    }

    private void RemoveSprite()
    {
        spawnMode = false;
        spriteRenderer.sprite = null;
        currentSelectionCost = 0;
    }

    public void InstanceDefenderOnLine(Transform spawnPoint)
    {
        if (spawnMode)
        {
            switch (selectedDefender)
            {
                case "knight":
                    PurchaseDefender(spawnPoint, knightPrefab);
                    break;
                case "mage":
                    PurchaseDefender(spawnPoint, magePrefab);
                    break;
                case "rogue":
                    PurchaseDefender(spawnPoint, roguePrefab);
                    break;
            }
            RemoveSprite();
        }
    }

    private void PurchaseDefender(Transform spawnPoint, GameUnit defenderPrefab)
    {
        if(gameStatus.CurrentGold >= currentSelectionCost)
        {
            gameStatus.ReduceCurrentGold(currentSelectionCost);
            Instantiate(defenderPrefab, spawnPoint.position, Quaternion.identity);
        }
    }

    public void AttachSprite(GameObject previewObject)
    {
        spawnMode = true;
        AttachDefenderInfo attachDefenderInfo = previewObject.GetComponent<AttachDefenderInfo>();
        spriteRenderer.sprite = attachDefenderInfo.DefenderSprite;
        selectedDefender = attachDefenderInfo.DefenderName;
        currentSelectionCost = attachDefenderInfo.DefenderCost;
    }

    public void SaturateAttachedSprite()
    {
        onCorrectSpawnZone = true;
        spriteRenderer.material.SetFloat("_GrayscaleAmount", 0f);
    }

    public void DesaturateAttachedSprite()
    {
        onCorrectSpawnZone = false;
        spriteRenderer.material.SetFloat("_GrayscaleAmount", 1f);
    }
}
