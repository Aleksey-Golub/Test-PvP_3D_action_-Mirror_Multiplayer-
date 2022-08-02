namespace Assets.CodeBase.Logic
{
    public struct Timer
    {
        public float Value { get; private set; }

        public void Tik(float delta) => Value += delta;

        public void Reset() => Value = 0;
    }
}
