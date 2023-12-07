namespace Assets.Scripts.Interface
{
    public class StartReady : BaseInterface
    {
        public static StartReady Instance;

        public void Awake()
        {
            Instance = this;
        }
    }
}
