using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;

namespace Sylveed.SampleApp.UseCase.Case2
{
    public class ChangeJointProperty : IUseCaseScope
    {
        public void Register(IUseCaseHolder holder)
        {
            holder.Register<Interactor, Response>();
        }

        public class Request : IRequest<Response>
        {
            public Guid JointId { get; }
            public int Position { get; }

            public Request(Guid jointId, int position)
            {
                JointId = jointId;
                Position = position;
            }
        }

        public class Response
        {
            public bool IsSucceed { get; }
            public int NewPosition { get; }

            public Response(bool isSucceed, int newPosition)
            {
                IsSucceed = isSucceed;
                NewPosition = newPosition;
            }
        }

        public class Interactor : IUseCase<Request, Response>
        {
            readonly ISomethingService somethingService;

            public Interactor(ISomethingService somethingService)
            {
                this.somethingService = somethingService;
            }

            public Response Handle(Request request)
            {
                throw new NotImplementedException();
            }
        }
    }

    public class SomethingController
    {
        readonly IUseCaseBus useCaseBus;

        public SomethingController(IUseCaseBus useCaseBus)
        {
            this.useCaseBus = useCaseBus;
        }

        public bool ChangeJointProperty(Guid jointId, int position)
        {
            var response = useCaseBus.Handle(
                new ChangeJointProperty.Request(jointId, position));

            return response.IsSucceed;
        }
    }

    public class App
    {
        static void Main(UseCaseConfigurator useCaseConfigurator)
        {
            useCaseConfigurator.RegisterScope<ChangeJointProperty>();
        }
    }

    public interface IUseCaseBus
    {
        TResponse Handle<TResponse>(IRequest<TResponse> request);
        //TResponse Handle<TRequest, TResponse>(TRequest request)
        //    where TRequest : IRequest<TResponse>;
    }

    public interface IUseCaseScope
    {
        void Register(IUseCaseHolder holder);
    }

    public interface IUseCaseHolder
    {
        void Register<TUseCase, TResponse>();
    }

    public interface IRequest<out TResponse>
    {

    }

    public interface IResponse<in TRequest>
    {

    }

    public interface IContainer
    {
        T Resolve<T>();
    }

    public interface IUseCaseContainer
    {
        //TUseCase Resolve<TUseCase>();
        IUseCase<TRequest, IResponse<TRequest>> Resolve<TRequest>();
    }

    public class UseCaseConfigurator
    {
        readonly IContainer container;
        readonly IUseCaseHolder holder;

        public void RegisterScope<TScope>() where TScope : IUseCaseScope
        {
            var scope = container.Resolve<TScope>();

            scope.Register(holder);
        }
    }

    //public class UseCaseBus : IUseCaseBus
    //{
    //    readonly IUseCaseContainer useCaseContainer;

    //    //public TResponse Handle<TRequest, TResponse>(TRequest request)
    //    //    where TRequest : IRequest<TResponse>
    //    //{

    //    //}

    //    public TResponse Handle<TResponse>(IRequest<TResponse> request)
    //    {

    //    }

    //    public TResponse Handle<TResponse>(IRequest<TResponse> request)
    //    {
    //        useCaseContainer.Resolve<ChangeJointProperty.Request>()
    //            .Handle(request);
    //    }
    //}

    public interface IUseCase<in TRequest, out TResponse>
    {
        TResponse Handle(TRequest request);
    }

    public interface ISomethingService
    {
            
    }
}

namespace Sylveed.SampleApp.UseCase.Case1
{
    public class MoveJoint2
    {
        readonly IJoistRepository joistRepository;
        readonly IDeckMatrixRepository deckMatrixRepository;
        readonly IJoistPresenter joistPresenter;
        readonly IDeckMatrixPreseter deckMatrixPreseter;
        readonly IEventRecorder eventRecorder;

        public MoveJoint2(IJoistRepository joistRepository, IDeckMatrixRepository deckMatrixRepository,
            IJoistPresenter joistPresenter,
            IDeckMatrixPreseter deckMatrixPreseter,
            IEventRecorder eventRecorder)
        {
            this.joistRepository = joistRepository;
            this.deckMatrixRepository = deckMatrixRepository;
            this.joistPresenter = joistPresenter;
            this.deckMatrixPreseter = deckMatrixPreseter;
            this.eventRecorder = eventRecorder;
        }

        public void Handle(Guid jointId, int position)
        {
            var joist = joistRepository.Find(jointId);
            var matrix = deckMatrixRepository.Find(jointId);
            
            var capture = Event.Capture(joist, matrix);

            joist.Move(position);
            matrix.MoveJoint(position);

            joistPresenter.Update(joist);
            deckMatrixPreseter.Update(matrix);

            eventRecorder.Record(capture(), Restore, Revert);
        }

        void Restore(Event e)
        {
            var joist = joistRepository.Find(e.JoistId);
            var matrix = deckMatrixRepository.Find(e.DeckId);

            //joist.RestoreFromState(e.NewJoistState);
            //matrix.RestoreFromState(e.NewDeckMatrixState);

            joistPresenter.Update(joist);
            deckMatrixPreseter.Update(matrix);
        }

        void Revert(Event e)
        {
            var joist = joistRepository.Find(e.JoistId);
            var matrix = deckMatrixRepository.Find(e.DeckId);

            //joist.RestoreFromState(e.OldJoistState);
            //matrix.RestoreFromState(e.OldDeckMatrixState);

            joistPresenter.Update(joist);
            deckMatrixPreseter.Update(matrix);
        }
        
        class Event
        {
            public Guid JoistId { get; private set; }
            public Guid DeckId { get; private set; }
            public JoistState NewJoistState { get; private set; }
            public DeckMatrixState NewDeckMatrixState { get; private set; }
            public JoistState OldJoistState { get; private set; }
            public DeckMatrixState OldDeckMatrixState { get; private set; }

            public static Func<Event> Capture(IJoist joist, IDeckMatrix deckMatrix)
            {
                var e = new Event()
                {
                    JoistId = Guid.Empty,
                    DeckId = Guid.Empty,
                    OldJoistState = joist.SaveToState(),
                    OldDeckMatrixState = deckMatrix.SaveToState()
                };

                return () =>
                {
                    e.NewJoistState = joist.SaveToState();
                    e.NewDeckMatrixState = deckMatrix.SaveToState();
                    return e;
                };
            }
        }
    }

    public interface IMoveJoint2Presenter
    {
        void Select(IJoist target);
    }

    public interface IEventRecorder
    {
        void Record<TEvent>(
            TEvent e,
            Action<TEvent> restore,
            Action<TEvent> revert);
    }

    public class MoveJoint
    {
        readonly IJoistRepository joistRepository;
        readonly IDeckMatrixRepository deckMatrixRepository;

        public MoveJoint(
            IJoistRepository joistRepository, 
            IDeckMatrixRepository deckMatrixRepository)
        {
            this.joistRepository = joistRepository;
            this.deckMatrixRepository = deckMatrixRepository;
        }
        
        public Response Handle(Guid jointId, int position)
        {
            var joist = joistRepository.Find(jointId);

            var matrix = deckMatrixRepository.Find(jointId);
            
            var e = new EventRecording(joist, matrix);

            joist.Move(position);
            matrix.MoveJoint(position);

            e.Complete(UseCaseEventPublisher.Instance);

            return new Response(true, position);
        }

        public void Restore(Event e)
        {
            UseCaseEventPublisher.Publish(CommonEvents.Restore(e));
        }

        public void Revert(Event e)
        {
            UseCaseEventPublisher.Publish(CommonEvents.Revert(e));
        }

        public class Response
        {
            public bool IsSucceed { get; }
            public int NewPosition { get; }

            public Response(bool isSucceed, int newPosition)
            {
                IsSucceed = isSucceed;
                NewPosition = newPosition;
            }
        }
        
        public class Event
        {
            public Guid JoistId { get; private set; }
            public Guid DeckId { get; private set; }
            public JoistState NewJoistState { get; private set; }
            public DeckMatrixState NewDeckMatrixState { get; private set; }
            public JoistState OldJoistState { get; private set; }
            public DeckMatrixState OldDeckMatrixState { get; private set; }

            public Event(
                Guid joistId, 
                Guid deckId, 
                JoistState newJoistState,
                DeckMatrixState newDeckMatrixState, 
                JoistState oldJoistState,
                DeckMatrixState oldDeckMatrixState)
            {
                JoistId = joistId;
                DeckId = deckId;
                NewJoistState = newJoistState;
                NewDeckMatrixState = newDeckMatrixState;
                OldJoistState = oldJoistState;
                OldDeckMatrixState = oldDeckMatrixState;
            }
        }

        class EventRecording
        {
            readonly IJoist joist;
            readonly IDeckMatrix deckMatrix;

            readonly JoistState oldJoistState;
            readonly DeckMatrixState oldDeckMatrixState;

            public EventRecording(IJoist joist, IDeckMatrix deckMatrix)
            {
                this.joist = joist;
                this.deckMatrix = deckMatrix;

                oldJoistState = joist.SaveToState();
                oldDeckMatrixState = deckMatrix.SaveToState();
            }

            public void Complete(UseCaseEventPublisher publisher)
            {
                var e = new Event(
                    Guid.Empty, 
                    Guid.Empty, 
                    joist.SaveToState(),
                    deckMatrix.SaveToState(),
                    oldJoistState,
                    oldDeckMatrixState);

                publisher.PublishEvent(e);
            }
        }
    }
        
    public class ChangeJointProperty
    {
        readonly IJoistRepository joistRepository;

        public ChangeJointProperty(
            IJoistRepository joistRepository)
        {
            this.joistRepository = joistRepository;
        }
        
        public Response Handle(Guid jointId)
        {
            var joist = joistRepository.Find(jointId);

            var e = Event.Begin(joist);

            joist.ChangeProperty();

            UseCaseEventPublisher.Publish(e.Complete());

            return new Response(true);
        }

        public class Response
        {
            public bool IsSucceed { get; }

            public Response(bool isSucceed)
            {
                IsSucceed = isSucceed;
            }
        }
        
        public class Event
        {
            public JoistState NewJoistState { get; private set; }
            public JoistState OldJoistState { get; private set; }

            public static IEventRecording<Event> Begin(IJoist joist)
            {
                return new Context(joist);
            }

            class Context : IEventRecording<Event>
            {
                readonly IJoist joist;
                readonly Event target;

                public Context(IJoist joist)
                {
                    this.joist = joist;

                    target = new Event()
                    {
                        OldJoistState = joist.SaveToState()
                    };
                }

                public Event Complete()
                {
                    target.NewJoistState = joist.SaveToState();
                    return target;
                }
            }
        }
    }


    public static class CommonEvents
    {
        public static RestoreEvent<TEvent> Restore<TEvent>(TEvent e)
        {
            return new RestoreEvent<TEvent>(e);
        }

        public static RevertEvent<TEvent> Revert<TEvent>(TEvent e)
        {
            return new RevertEvent<TEvent>(e);
        }
    }
    public class RestoreEvent<TEvent>
    {
        public TEvent Event { get; }

        public RestoreEvent(TEvent @event)
        {
            Event = @event;
        }
    }

    public class RevertEvent<TEvent>
    {
        public TEvent Event { get; }

        public RevertEvent(TEvent @event)
        {
            Event = @event;
        }
    }

    public class SomethingController
    {
        readonly MoveJoint moveJoint;
        readonly ChangeJointProperty changeJointProperty;

        public SomethingController(MoveJoint moveJoint, ChangeJointProperty changeJointProperty)
        {
            this.moveJoint = moveJoint;
            this.changeJointProperty = changeJointProperty;
        }

        public bool ChangeJointProperty(Guid jointId)
        {
            var response = changeJointProperty.Handle(jointId);

            return response.IsSucceed;
        }

        public bool MoveJoint(Guid jointId, int position)
        {
            var response = moveJoint.Handle(jointId, position);

            return response.IsSucceed;
        }
    }

    public class MoveJointPresenter :
        IUseCaseEventPresenter<MoveJoint.Event>,
        IUseCaseEventPresenter<RestoreEvent<MoveJoint.Event>>,
        IUseCaseEventPresenter<RevertEvent<MoveJoint.Event>>
    {
        public void Handle(MoveJoint.Event e)
        {
            ViewManager.UpdateJoist(e.NewJoistState);
            ViewManager.UpdateDeckMatrix(e.NewDeckMatrixState);

            EventRecorder.Record(e);
        }

        public void Handle(RestoreEvent<MoveJoint.Event> restored)
        {
            var e = restored.Event;

            ViewManager.UpdateJoist(e.NewJoistState);
            ViewManager.UpdateDeckMatrix(e.NewDeckMatrixState);
        }

        public void Handle(RevertEvent<MoveJoint.Event> reverted)
        {
            var e = reverted.Event;

            ViewManager.UpdateJoist(e.OldJoistState);
            ViewManager.UpdateDeckMatrix(e.OldDeckMatrixState);
        }
    }

    public static class EventViewTree
    {
        static void Main()
        {
            Observable.Merge(
                    EventBus.Observe<MoveJoint.Event>()
                        .Select(x => x.NewJoistState),
                    EventBus.Observe<RestoreEvent<MoveJoint.Event>>()
                        .Select(x => x.Event.NewJoistState),
                    EventBus.Observe<RevertEvent<MoveJoint.Event>>()
                        .Select(x => x.Event.OldJoistState)
                )
                .Subscribe(state =>
                {
                    ViewManager.UpdateJoist(state);
                });
        }

        interface IEventPresenter
        {
            
        }

        class JoistObserver
        {
            public void Register()
            {
                Streams()
                    .Merge()
                    .Subscribe(state =>
                    {
                        ViewManager.UpdateJoist(state);
                    });
            }

            IEnumerable<IObservable<JoistState>> Streams()
            {
                yield return EventBus.Observe<MoveJoint.Event>()
                    .Select(x => x.NewJoistState);
                yield return EventBus.Observe<RestoreEvent<MoveJoint.Event>>()
                    .Select(x => x.Event.NewJoistState);
                yield return EventBus.Observe<RevertEvent<MoveJoint.Event>>()
                    .Select(x => x.Event.OldJoistState);
            }
        }
    }

    public static class EventBus
    {
        public static IObservable<TEvent> Observe<TEvent>()
        {
            throw new NotImplementedException();
        }
    }
    public class ChangeJointPropertyPresenter : IUseCaseEventPresenter<ChangeJointProperty.Event>
    {
        public void Handle(ChangeJointProperty.Event e)
        {
            ViewManager.UpdateJoist(e.NewJoistState);
        }
    }

    public static class EventRecorder
    {
        static IContainer container;

        public static void Record<TEvent>(TEvent e)
        {
            RecorderCache<TEvent>.cache.Record(e);
        }

        public static void Register<TEvent>(Action<TEvent> restore, Action<TEvent> revert)
        {
            RecorderCache<TEvent>.cache = new AnonymousEventRecorder<TEvent>(restore, revert);
        }

        public static void Register<TEvent, TRecorder>()
            where TRecorder : IEventRecorder<TEvent>
        {
            RecorderCache<TEvent>.cache = container.Resolve<TRecorder>();
        }

        static class RecorderCache<TEvent>
        {
            public static IEventRecorder<TEvent> cache;
        }

        class AnonymousEventRecorder<TEvent> : IEventRecorder<TEvent>
        {
            readonly Action<TEvent> restore;
            readonly Action<TEvent> revert;

            public AnonymousEventRecorder(Action<TEvent> restore, Action<TEvent> revert)
            {
                this.restore = restore;
                this.revert = revert;
            }

            public void Record(TEvent e)
            {
                UndoManager.Add(
                    () => restore(e), 
                    () => revert(e));
            }
        }
    }

    class App
    {
        readonly MoveJoint moveJoint;

        void Main()
        {
            EventRecorder.Register<MoveJoint.Event, MoveJointEventRecorder>();

            EventRecorder.Register<MoveJoint.Event>(moveJoint.Restore, moveJoint.Revert);
        }
    }

    public class MoveJointEventRecorder : IEventRecorder<MoveJoint.Event>
    {
        readonly MoveJoint moveJoint;

        public void Record(MoveJoint.Event e)
        {
            UndoManager.Add(
                () => moveJoint.Restore(e),
                () => moveJoint.Revert(e));
        }
    }

    public interface IContainer
    {
        T Resolve<T>();
    }

    public interface IEventRecorder<TEvent>
    {
        void Record(TEvent e);
    }

    public interface IRevertableCommand
    {
        void Restore();
        void Revert();
    }

    public static class UndoManager
    {
        public static void Add(IRevertableCommand command)
        {

        }

        public static void Add(Action restore, Action revert)
        {

        }
    }

    public static class ViewManager
    {
        public static void UpdateJoist(JoistState state)
        {

        }

        public static void UpdateDeckMatrix(DeckMatrixState state)
        {

        }
    }

    public interface IUseCaseContainer
    {
        T Get<T>();
    }

    public interface IJoistRepository
    {
        IJoist Find(Guid id);
    }

    public interface IDeckMatrixRepository
    {
        IDeckMatrix Find(Guid id);
    }

    public interface IJoist
    {
        void Move(int position);
        void ChangeProperty();
        JoistState SaveToState();
    }

    public interface IDeckMatrix
    {
        void MoveJoint(int position);
        DeckMatrixState SaveToState();
    }

    public interface IJoistPresenter
    {
        void Update(IJoist joist);
    }

    public interface IDeckMatrixPreseter
    {
        void Update(IDeckMatrix matrix);
    }

    public class JoistState
    {

    }

    public class DeckMatrixState
    {

    }

    public class UseCaseEventPublisher
    {
        public static UseCaseEventPublisher Instance { get; }

        public void PublishEvent<T>(T e)
        {

        }

        public static void Publish<T>(T e)
        {

        }
    }

    public interface IUseCaseEventPresenter<TEvent>
    {
        void Handle(TEvent e);
    }

    public interface IEventRecording<TEvent>
    {
        TEvent Complete();
    }

}