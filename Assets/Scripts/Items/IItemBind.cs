namespace PokemonGame.Items
{
    /// <summary>
    /// Defines a contract for binding an item to a UI or logic component.
    /// </summary>
    public interface IItemBind
    {
        /// <summary>
        /// Binds the specified item to the implementing component.
        /// </summary>
        /// <param name="item">The item to bind.</param>
        void Bind(Item item);
    }
}
