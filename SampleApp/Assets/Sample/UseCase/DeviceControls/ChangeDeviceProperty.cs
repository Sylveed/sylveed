using System;
using Sylveed.SampleApp.Sample.Application.Devices;
using Sylveed.SampleApp.Sample.Application.Dialogs;
using Sylveed.SampleApp.Sample.Domain.Devices;

namespace Sylveed.SampleApp.Sample.UseCase.DeviceControls
{
    public class ChangeDeviceProperty
    {
        readonly IDeviceRepository deviceRepository;
        readonly IDeviceControlService controlService;
        readonly IDeviceTransactionService deviceTransactionService;
        readonly IDevicePropertyPresenter devicePropertyPresenter;
        readonly IDialogPresenter dialogPresenter;

        public void ChangeName(DeviceId id, string name)
        {
            ChangeDevice(id, device =>
            {
                device.ChangeName(name, controlService);
            });

            devicePropertyPresenter.UpdateName(id, name);

            dialogPresenter.Message("Name changed.");
        }

        public void Activate(DeviceId id)
        {
            ChangeDevice(id, device =>
            {
                device.Activate(controlService);
            });

            devicePropertyPresenter.UpdateActive(id, true);

            dialogPresenter.Message("Activated.");
        }

        public void Deactivate(DeviceId id)
        {
            ChangeDevice(id, device =>
            {
                device.Deactivate(controlService);
            });
            
            devicePropertyPresenter.UpdateActive(id, false);

            dialogPresenter.Message("Deactivated.");
        }

        public void Reset(DeviceId id)
        {
            if (!dialogPresenter.Confirm("Are you sure reset this device?"))
                return;

            var newName = "No Name";

            ChangeDevice(id, device =>
            {
                device.ChangeName(newName, controlService);
                device.Deactivate(controlService);
            });

            devicePropertyPresenter.UpdateName(id, newName);
            devicePropertyPresenter.UpdateActive(id, false);

            dialogPresenter.Message("Device has reset.");
        }

        void ChangeDevice(DeviceId id, Action<Device> action)
        {
            using (var scope = deviceTransactionService.Begin(id))
            {
                var device = deviceRepository.Find(id);

                action(device);
                
                controlService.Save(id);

                scope.Commit();
            }
        }
    }
}