namespace MonsterTamer.Shared.Interfaces
{
    /// <summary>
    /// Generic interface for components that can bind a data instance of type T.
    /// </summary>
    /// <typeparam name="T">The type of data to bind.</typeparam>
    public interface IBindable<T>
    {
        /// <summary>
        /// Binds the given data instance.
        /// </summary>
        /// <param name="data">The instance to bind.</param>
        void Bind(T data);
    }
}
