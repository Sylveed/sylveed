using Sylveed.SampleApp.Sample.Presentation.Devices;
using Sylveed.SampleApp.SampleApplication.Helpers;
using UnityEngine;
using UnityEngine.UI;

namespace Sylveed.SampleApp.SampleApplication.Monitors.DeviceProperties
{
    public class DevicePropertyView : ViewBase
    {
        readonly DevicePropertyController controller;

        [SerializeField]
        InputField deviceName;

        [SerializeField]
        Toggle isActive;

        [SerializeField]
        Button reset;

        string deviceId;

        public void Initialize(string deviceId)
        {
            this.deviceId = deviceId;
        }

        protected override void OnAwaked()
        {
            deviceName.onEndEdit.AddListener(value =>
            {
                controller.ChangeName(deviceId, value);
            });

            isActive.onValueChanged.AddListener(value =>
            {
                if (value)
                    controller.Activate(deviceId);
                else
                    controller.Deactivate(deviceId);
            });

            reset.onClick.AddListener(() =>
            {
                controller.Reset(deviceId);
            });
        }
    }
}