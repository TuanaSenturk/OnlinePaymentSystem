using System;
using System.Collections.Generic;

namespace OnlinePaymentSystem.Models;

public partial class Account
{
    public int Id { get; set; }

    public string? Iban { get; set; }

    public decimal? Balance { get; set; }

    public virtual User IdNavigation { get; set; } = null!;

    public virtual ICollection<Transaction> TransactionReceiverAccounts { get; set; } = new List<Transaction>();

    public virtual ICollection<Transaction> TransactionSenderAccounts { get; set; } = new List<Transaction>();
}
