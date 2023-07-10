using System;
using System.Collections.Generic;

namespace API.Domains;

public partial class Transaction
{
    public string Id { get; set; } = null!;

    public string DebbitedAccount { get; set; } = null!;

    public string CreditedAccount { get; set; } = null!;

    public decimal Amount { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual Account CreditedAccountNavigation { get; set; } = null!;

    public virtual Account DebbitedAccountNavigation { get; set; } = null!;
}
