namespace ShadowExit.PlayerStateSystem
{
    public abstract class State<T>
    {
        protected T target;

        protected State(T target)
        {
            this.target = target;
        }

        public abstract void OnEnter();
        public abstract void OnUpdate();
        public abstract void OnFixedUpdate();
        public abstract void OnExit();
    }
}
