using Sylveed.SampleApp.Sample.Presentation.Devices;
using Sylveed.SampleApp.Sample.Presentation.Monitors;
using Sylveed.SampleApp.SampleApplication.Helpers;
using UnityEngine;
using UnityEngine.UI;

namespace Sylveed.SampleApp.SampleApplication.Monitors.DeviceProperties
{
    public class DevicePropertyView : ViewBase, IDevicePropertyView
    {
        readonly DevicePropertyController controller;

        [SerializeField]
        InputField deviceName;

        [SerializeField]
        Toggle isActive;

        [SerializeField]
        Button reset;

        string deviceId;

        public bool Visible => gameObject.activeSelf;

        public void Show(string deviceId)
        {
            this.deviceId = deviceId;

            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }

        public void UpdateActive(bool value)
        {
            isActive.isOn = value;
        }

        public void UpdateName(string value)
        {
            deviceName.text = value;
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