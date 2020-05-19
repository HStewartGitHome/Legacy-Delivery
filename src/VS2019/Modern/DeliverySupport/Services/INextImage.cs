namespace DeliverySupport.Services
{
    public interface INextImage
    {
        int CurrentCount { get; set; }
        int CurrentIndex { get; set; }

        string ImageAtCurrentIndex(int cardType);
        void Initialize(int cardType);
        string Next(int cardType);
        string Previous(int cardType);
        string TextAtCurrentIndex(int cardType);
        string TitleAtCurrentIndex(int cardType);
    }
}