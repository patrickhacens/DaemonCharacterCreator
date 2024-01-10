using DnetIndexedDb;
using Microsoft.JSInterop;

public class Db : IndexedDbInterop
{
    public const string PlayerStore = "Players";
    public event EventHandler CharacterListChanged;

    public void SendCharacterListChanged() => CharacterListChanged?.Invoke(this, EventArgs.Empty);
    public Db(IJSRuntime jsRuntime, IndexedDbOptions<Db> options)
    : base(jsRuntime, options)
    {
    }

}