using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UniRx;

namespace Sylveed.SampleApp.UseCase.Modules
{
    namespace Domain
    {
        interface IModuleRepository
        {
            IModule Find(ModuleId id);
        }

        class ParameterKey
        {

        }

        class Parameter
        {
            public void SetValue(object value)
            {

            }
        }

        interface IModuleProperty<T>
        {
            T Value { get; }
            ParameterKey ParameterKey { get; }
            void SetRemoteValue(T newValue);
            void SetLocalValue(T newValue, IModuleNotificationService notificationService);
        }

        class ModuleId
        {

        }

        interface IModuleNotificationService
        {
            void UpdateRemote(ParameterKey key, object value);
        }

        interface IModule
        {
            ModuleId Id { get; }
            IModuleProperty<object> GetProperty(ParameterKey key);
            IModuleProperty<object>[] GetProperties();
            IModuleProperty<int> ScalingGain { get; }
            IModuleProperty<float> DynamicRange { get; }
        }
    }

    namespace UseCase
    {
        using Domain;

        class ChangeModuleParameter
        {
            readonly IModuleRepository moduleRepository;
            readonly IModuleNotificationService notificationService;
            readonly IModulePresenter modulePresenter;
        
            public void ScalingGain(ModuleId id, int value)
            {
                SetLocalValue(id, value, 
                    x => x.ScalingGain,
                    x => x.ScalingGain);
            }

            public void DynamicRange(ModuleId id, float value)
            {
                SetLocalValue(id, value, 
                    x => x.DynamicRange,
                    x => x.DynamicRange);
            }

            void SetLocalValue<T>(
                ModuleId id,
                T value,
                Func<IModule, IModuleProperty<T>> propertySelector,
                Func<IModulePresenter, Action<ModuleId, T>> presentationSelector)
            {
                var module = moduleRepository.Find(id);

                var property = propertySelector(module);

                property.SetLocalValue(value, notificationService);

                modulePresenter.Parameter(id, property.ParameterKey, value);
                presentationSelector(modulePresenter)(id, value);
            }
        }

        class UpdateModuleParameter
        {
            readonly IModuleRepository moduleRepository;
            readonly IModuleNotificationService notificationService;
            readonly IModulePresenter modulePresenter;

            public void Update(
                ModuleId moduleId,
                ParameterKey key,
                object value)
            {
                var module = moduleRepository.Find(moduleId);

                var property = module.GetProperty(key);

                property.SetRemoteValue(value);

                modulePresenter.Parameter(moduleId, key, value);
            }
        }

        class CopyModule
        {
            readonly IModuleRepository moduleRepository;
            readonly IModuleNotificationService notificationService;
            readonly IModulePresenter modulePresenter;
        
            public void Handle(ModuleId sourceId, ModuleId destinationId)
            {
                var source = moduleRepository.Find(sourceId);
                var destination = moduleRepository.Find(destinationId);

                var sourceProperties = source.GetProperties();
                var destinationProperties = destination.GetProperties();

                var presentationActions = new List<Action>();

                foreach (var x in sourceProperties
                    .Zip(destinationProperties, (a, b) => new { a, b }))
                {
                    var sourceProperty = x.a;
                    var destinationProperty = x.b;

                    if (destinationProperty.Value != sourceProperty.Value)
                    {
                        destinationProperty.SetLocalValue(sourceProperty.Value, notificationService);

                        presentationActions.Add(() =>
                        {
                            modulePresenter.Parameter(
                                destinationId, 
                                destinationProperty.ParameterKey, 
                                destinationProperty.Value);
                        });
                    }
                }

                foreach (var presentationAction in presentationActions)
                {
                    presentationAction();
                }
            }
        }

        interface IModulePresenter
        {
            void ScalingGain(ModuleId id, int value);
            void DynamicRange(ModuleId id, float value);

            void Parameter(
                ModuleId id,
                ParameterKey key,
                object value);
        }
    }

    namespace Application
    {
        using Domain;
        using UseCase;

        static class Converters
        {
            public static TwoWayConverter<float, decimal> FloatToDecimal { get; } =
                new TwoWayConverter<float, decimal>(x => (decimal)x, x => (float)x);

            public static TwoWayConverter<decimal, float> DecimalToFloat { get; } =
                new TwoWayConverter<decimal, float>(x => (float)x, x => (decimal)x);

            public class Through<T>
            {
                public static TwoWayConverter<T, T> Default =
                    new TwoWayConverter<T, T>(x => x, x => x);
            }
        }

        class TwoWayConverter<TIn, TOut>
        {
            readonly Converter<TIn, TOut> convert;
            readonly Converter<TOut, TIn> convertBack;

            public TwoWayConverter(Converter<TIn, TOut> convert, Converter<TOut, TIn> convertBack)
            {
                this.convert = convert;
                this.convertBack = convertBack;
            }

            public TOut Convert(TIn arg)
            {
                return convert(arg);
            }

            public TIn ConvertBack(TOut arg)
            {
                return convertBack(arg);
            }
        }

        class SettingModuleModel
        {
            readonly IModuleModelNotificationService notificationService;
            readonly ChangeModuleParameter changeModuleParameter;
            
            IModule module;
            
            bool suspendCommand = false;

            public ReactiveProperty<int> ScalingGain { get; }

            public ReactiveProperty<decimal> DynamicRange { get; }
            
            public void Initialize(IModule module)
            {
                this.module = module;

                BindProperty(DynamicRange,
                    x => x.DynamicRange,
                    x => x.DynamicRange,
                    Converters.FloatToDecimal);

                BindProperty(ScalingGain,
                    x => x.ScalingGain,
                    x => x.ScalingGain);
            }
            
            void BindProperty<T>(
                ReactiveProperty<T> localProperty,
                Func<IModule, IModuleProperty<T>> remotePropertySelector,
                Func<ChangeModuleParameter, Action<ModuleId, T>> useCaseSelector)
            {
                BindProperty(localProperty, remotePropertySelector, useCaseSelector, Converters.Through<T>.Default);
            }

            void BindProperty<TLocal, TRemote>(
                ReactiveProperty<TLocal> localProperty,
                Func<IModule, IModuleProperty<TRemote>> remotePropertySelector,
                Func<ChangeModuleParameter, Action<ModuleId, TRemote>> useCaseSelector,
                TwoWayConverter<TRemote, TLocal> converter)
            {
                BindPropertyOneWayToSource(localProperty, useCaseSelector, converter.ConvertBack);
                BindPropertyOneWay(remotePropertySelector, localProperty, converter.Convert);
            }
            
            void BindPropertyOneWay<TRemote, TLocal>(
                Func<IModule, IModuleProperty<TRemote>> remotePropertySelector,
                ReactiveProperty<TLocal> localProperty,
                Converter<TRemote, TLocal> converter)
            {
                var remoteProperty = remotePropertySelector(module);

                notificationService.Property(module.Id, remoteProperty, value =>
                {
                    suspendCommand = true;

                    localProperty.Value = converter(value);

                    suspendCommand = false;
                });
            }

            void BindPropertyOneWayToSource<TLocal, TRemote>(
                ReactiveProperty<TLocal> localProperty, 
                Func<ChangeModuleParameter, Action<ModuleId, TRemote>> useCaseSelector,
                Converter<TLocal, TRemote> converter)
            {
                SubscribeProperty<TLocal>(localProperty, value =>
                {
                    var useCaseMethod = useCaseSelector(changeModuleParameter);

                    useCaseMethod(module.Id, converter(value));
                });
            }

            void SubscribeProperty<T>(ReactiveProperty<T> property, Action<T> action)
            {
                property
                    .Where(_ => !suspendCommand)
                    .Subscribe(action);
            }
        }

        class CompareModuleModel
        {
            readonly IModuleModelNotificationService notificationService;
            readonly IModule module;

            public ReactiveDictionary<string, string> ParameterTable { get; }

            void Initialize()
            {
                UpdateParameters();

                notificationService.Parameter(module.Id, (parameterKey, value) =>
                {
                    UpdateParameters();
                });
            }

            void UpdateParameters()
            {

            }
        }

        //class ModulePresenter : IModulePresenter, IModuleModelNotificationService
        //{
        //    readonly Dictionary<ModuleId, Subject<float>> dynamicRanges =
        //        new Dictionary<ModuleId, Subject<float>>();

        //    public void DynamicRange(ModuleId id, float value)
        //    {
        //        notifyService.Update(id);
        //    }

        //    public IDisposable DynamicRange(ModuleId moduleId, Action<float> action)
        //    {
        //        dynamicRanges[moduleId] = new Subject<float>();
        //    }

        //    public void Parameter(ModuleId id, ParameterKey key, object value)
        //    {
        //        notifyService.Update(id);
        //    }

        //    public void ScalingGain(ModuleId id, int value)
        //    {
        //        notifyService.Update(id);
        //    }
        //}

        interface IModuleModelNotifyService
        {
            void Update(ModuleId moduleId);
        }

        interface IModuleModelNotificationService
        {
            IDisposable Property<T>(ModuleId moduleId, IModuleProperty<T> property, Action<T> action);
            IDisposable Parameter(ModuleId moduleId, Action<ParameterKey, object> action);
            IDisposable Updated(ModuleId moduleId, Action action);
        }
    }   

    namespace Others
    {
        class App
        {
            void Main()
            {
            }
        }
    }

    interface IContainer
    {
        T Resolve<T>();
    }
}