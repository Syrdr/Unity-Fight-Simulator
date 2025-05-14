using UnityEngine;

public class DisappearWhenDead : MonoBehaviour
{
    [SerializeField] GameObject parent;
    private MoveCharacter parentMoveCharacter;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        parentMoveCharacter = parent.GetComponent<MoveCharacter>();
    }

    // Update is called once per frame
    void Update()
    {
        if(parentMoveCharacter.isDead)
        {
            gameObject.SetActive(false);
        }
    }
}
