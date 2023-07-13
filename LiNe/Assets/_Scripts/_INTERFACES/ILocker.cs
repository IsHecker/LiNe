namespace Assets.MAIN_INTERFACES.Shop_TAB
{
    public interface ILocker
    {
        void Unlock();
        int PriceToUnlock { get; }
    }
}
