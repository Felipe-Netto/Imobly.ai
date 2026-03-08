namespace ImoblyAI.Api.Enums.Subscription;

public enum SubscriptionStatus
{
    // O usuário criou a conta mas ainda não assinou nada nem entrou no trial
    Inactive = 0,

    // Período de teste gratuito
    Trialing = 1,

    // Pagamento em dia e assinatura ativa
    Active = 2,

    // O pagamento falhou (cartão vencido, sem saldo). 
    // No Stripe isso é o "PastDue". Útil para mostrar um aviso: "Ops, verifique seu cartão!"
    PastDue = 3,

    // Ele cancelou, mas o mês ainda não acabou. Ele ainda deve ter acesso até o fim do ciclo.
    Canceled = 4,

    // A assinatura acabou de vez e o acesso foi cortado.
    Unpaid = 5
}