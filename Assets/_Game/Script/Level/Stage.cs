using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum ColorType
{
    Blue, Red, Green, Violet, Default
}
public class Stage : MonoBehaviour
{
    public Transform[] brickPoints;
    public List<Vector3> oldPosBrick = new List<Vector3>();
    public List<Brick> bricks = new List<Brick>();
    public Brick brickPrefab;
    public void OnInit()
    {
        for (int i = 0; i < brickPoints.Length; i++)
        {
            oldPosBrick.Add(brickPoints[i].position);
        }
    }
    public void InitColor(ColorType colorType)
    {
        int amount = brickPoints.Length / LevelManager.Instance.CharacterAmount;
        for (int i = 0; i < amount; i++)
        {
            NewBrick(colorType);
        }
    }
    public void NewBrick(ColorType colorType)
    {
        if (oldPosBrick.Count > 0)
        {
            int rand = Random.Range(0, oldPosBrick.Count);
            Brick brick = SimplePool.Spawn<Brick>(ObjectType.Brick, oldPosBrick[rand], Quaternion.identity);
            brick.stage = this;
            brick.ChangeColor(colorType);
            oldPosBrick.RemoveAt(rand);
            bricks.Add(brick);
        }
    }
    internal void RemoveBrick(Brick brick)
    {
        oldPosBrick.Add(brick.TF.position);
        bricks.Remove(brick);
    }
    public void ClearBrickColor(ColorType colorType)
    {
        for (int i = bricks.Count - 1; i >= 0; i--)
        {
            Brick brick = bricks[i];
            if (brick.colorType == colorType)
            {
                SimplePool.Despawn(brick);
                bricks.RemoveAt(i);
            }
        }
    }
    public void ClearAllBricks()
    {
        // Loại bỏ tất cả các viên gạch
        for (int i = bricks.Count - 1; i >= 0; i--)
        {
            SimplePool.Despawn(bricks[i]); // Trả gạch về pool
        }

        bricks.Clear();       // Xóa danh sách gạch
        oldPosBrick.Clear();  // Xóa vị trí cũ
    }

    internal Brick SeekBrickPoint(ColorType colorType)
    {
        Brick brick = null;

        for (int i = 0; i < bricks.Count; i++)
        {
            if (bricks[i].colorType == colorType)
            {
                brick = bricks[i];
                break;
            }
        }

        return brick;
    }

}
