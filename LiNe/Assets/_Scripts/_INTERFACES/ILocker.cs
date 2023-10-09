namespace Assets.MAIN_INTERFACES.Shop_TAB
{
    public interface ILocker
    {
        void CheckIfCanUnlock();
        void Unlock();
        int PriceToUnlock { get; }
    }
}
