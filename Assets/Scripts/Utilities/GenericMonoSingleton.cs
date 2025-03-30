using UnityEngine;

namespace Assets.Scripts.Utilities
{
    public class GenericMonoSingleton<T> : MonoBehaviour where T : GenericMonoSingleton<T>
    {
        private static T instance;

        public static T Instance { get { return instance; } }

        protected virtual void Awake()
        {
            if (instance == null)
            {
                instance = (T)this;
            }
            else
            {
                Destroy(gameObject);
                Debug.LogError("Singleton of " + (T)this + " is trying to create second instance.. ");
            }
        }
    }
}