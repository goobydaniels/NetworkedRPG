using System;

[Serializable]
public abstract class ItemInstance {
    public string id;

    public ItemInstance() {
        id = Guid.NewGuid().ToString();
    }
}
