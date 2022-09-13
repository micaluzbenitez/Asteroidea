namespace Toolbox.Pool
{
    public interface IPooledObject
    {
        /// <summary>
        /// Initialize the object in question with whatever is inside this function
        /// </summary>
        public void OnObjectSpawn();
    }
}