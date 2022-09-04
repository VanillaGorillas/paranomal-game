public class CollectItem : Interactable
{
    protected override void Interact()
    {
        Destroy(gameObject);
    }
}
