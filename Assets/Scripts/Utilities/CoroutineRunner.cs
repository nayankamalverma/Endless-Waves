using System.Collections;

namespace Assets.Scripts.Utilities
{
    public class CoroutineRunner : GenericMonoSingleton<CoroutineRunner>
    {
        public void RunCoroutine(IEnumerator coroutine)
        {
            StartCoroutine(coroutine);
        }
    }
}