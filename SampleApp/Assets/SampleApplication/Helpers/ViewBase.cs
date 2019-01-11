using UnityEngine;

namespace Sylveed.SampleApp.SampleApplication.Helpers
{
    public class ViewBase : MonoBehaviour
    {
        protected void Awake()
        {
            Injector.Inject(this);

            OnAwaked();
        }

        protected virtual void OnAwaked()
        {

        }
    }
}