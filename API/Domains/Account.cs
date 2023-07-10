using System;
using System.Collections.Generic;

namespace API.Domains;

public partial class Account
{
    public string Id { get; set; } = null!;

    public string UserId { get; set; } = null!;

    public decimal Balance { get; set; }

    public virtual ICollection<Transaction> TransactionCreditedAccountNavigations { get; set; } = new List<Transaction>();

    public virtual ICollection<Transaction> TransactionDebbitedAccountNavigations { get; set; } = new List<Transaction>();

    public virtual User User { get; set; } = null!;
}
