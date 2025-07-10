namespace PokemonGame.Systems.Inventory
{
    /// <summary>
    /// Defines a binding contract for UI components that display items from a specific inventory category.
    /// </summary>
    public interface IInventoryCategoryBind
    {
        /// <summary>
        /// Binds the given inventory category to the implementing UI component.
        /// </summary>
        /// <param name="category">The inventory category to bind and display.</param>
        void Bind(InventoryCategory category);
    }
}
