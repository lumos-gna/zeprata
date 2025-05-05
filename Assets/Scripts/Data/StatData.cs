

[System.Serializable]
public class StatData
{
    public float moveSpeed;

    public void ApplyTarget(bool isAdd, StatData targetData)
    {
        float delta = isAdd ? 1 : -1;

        moveSpeed += targetData.moveSpeed * delta;
    }
}