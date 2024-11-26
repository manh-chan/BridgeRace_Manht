using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : ColorObject
{
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask stairLayer;
    [SerializeField] private LayerMask gateLayer;
    [SerializeField] private CharacterBrick charBrickPrefab;
    [SerializeField] private Transform brickHolder;
    [SerializeField] protected Transform charRotate;

    public int BrickCount => characterBricks.Count;
    public Animator anim;

    private string currentAnim = Constant.ANIM_IDLE;
    private Stage stage;
    private List<CharacterBrick> characterBricks = new List<CharacterBrick>();

    public Stage Stage { get => stage; set => stage = value; }

    public virtual void OnInit()
    {
        charRotate.rotation = Quaternion.identity;
        ClearBrick();
    }
    public Vector3 CheckGround(Vector3 nextPos)
    {
        RaycastHit hit;
        if (Physics.Raycast(nextPos, Vector3.down, out hit, 2f, groundLayer))
        {
            return hit.point + Vector3.up * 0.1f;
        }
        return TF.position;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, transform.position + charRotate.forward * 2f);
    }
    public bool CheckGate(Vector3 nextPos)
    {
        RaycastHit hit;
        Debug.DrawRay(nextPos, charRotate.forward * 0.2f, Color.red, 0.1f);

        if (Physics.Raycast(nextPos, charRotate.forward, out hit, 0.2f, gateLayer))
        {
            Gate gate = Cache.GetGate(hit.collider);
            Vector3 doorForward = gate.transform.forward;
            float dot = Vector3.Dot(doorForward, charRotate.forward);
           
            if (dot < 0) return false;
            else return true;
            
        }

        return true; 
    }
    public bool CanMove(Vector3 nextPoint)
    {
        bool isCanMove = true;
        RaycastHit hit;

        if (Physics.Raycast(nextPoint, Vector3.down, out hit, 0.25f, stairLayer))
        {
            Stair stair = Cache.GetStair(hit.collider);
            if (stair.colorType != colorType && characterBricks.Count > 0)
            {
                stair.ChangeColor(colorType);
                RemoveBrick();
                stage.NewBrick(colorType);
            }

            if (stair.colorType != colorType && characterBricks.Count == 0 && charRotate.forward.z > 0)
            {
                isCanMove = false;
            }
        }

        return isCanMove;
    }
    public void AddBrick()
    {
        CharacterBrick characterBrick = Instantiate(charBrickPrefab, brickHolder);
        characterBrick.ChangeColor(colorType);
        characterBrick.TF.localPosition = Vector3.up * 0.25f * characterBricks.Count;
        characterBricks.Add(characterBrick);
    }
    public void RemoveBrick()
    {
        if (characterBricks.Count > 0)
        {
            CharacterBrick playerBrick = characterBricks[characterBricks.Count - 1];
            characterBricks.RemoveAt(characterBricks.Count - 1);
            Destroy(playerBrick.gameObject);
        }
    }
    public void ClearBrick()
    {
        for (int i = 0; i < characterBricks.Count; i++)
        {
            Destroy(characterBricks[i].gameObject);
        }
        characterBricks.Clear();
    }
    public void ChangeAnim(string animName)
    {
        if (currentAnim != animName)
        {
            anim.ResetTrigger(currentAnim);
            currentAnim = animName;
            anim.SetTrigger(currentAnim);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Constant.TAG_BRICK))
        {
            Brick brick = Cache.GetBrick(other);
            if (brick.colorType == colorType)
            {
                brick.OnDespawn();
                AddBrick();
                Destroy(brick.gameObject);
            }
        }
    }
}
