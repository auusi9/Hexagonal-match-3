using UnityEngine;

public class Block : MonoBehaviour
{
    public BlockType Type;
    
    
    public enum BlockType
    {
        Saloon = 0,
        ScifiBuilding = 1,
        Castle = 2,
        PetrolStation = 3,
        Windmill = 4
    }
}