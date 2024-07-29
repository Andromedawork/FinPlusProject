namespace FinPlus.Domain.Offers
{
    using System.ComponentModel.DataAnnotations;

    public enum OfferStatus
    {
        // Оформлена заявка на оффер
        [Display(Name = "Ожидает")]
        InProgress = 0,

        // Лид одобрился партнеркой
        [Display(Name = "Одобрено")]
        Approved = 1,

        // Лид отклонился партнеркой
        [Display(Name = "Отклонено")]
        Declined = 2,

        // Отказано в получение оффера по заявке
        [Display(Name = "Отказано")]
        Refused = 3,

        // Лид встретился с курьером, получил оффер
        [Display(Name = "Получено")]
        Received = 4,

        // Лид совершил транзакцию по карте
        [Display(Name = "ЦД сделано")]
        TransactionMade = 5,

        // Лид отказался от оформления продукта
        [Display(Name = "Отказался")]
        Rejected = 6,

        // Лид скоро оформит заявку на оффер
        [Display(Name = "На будующее")]
        Future = 7,

        // У лида уже есть данный продукт
        [Display(Name = "Есть")]
        AlreadyHas = 8,

        // Любые проблемы в получении продукта
        [Display(Name = "Невозможно")]
        Impossible = 9,

        // Лид слился, либо украдена заявка
        [Display(Name = "Потерян")]
        Lost = 10,
    }
}
