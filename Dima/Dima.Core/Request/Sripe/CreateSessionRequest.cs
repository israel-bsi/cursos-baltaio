﻿namespace Dima.Core.Request.Sripe;

public class CreateSessionRequest : Request
{
    public string OrderNumber { get; set; } = string.Empty;
    public string ProductTitle { get; set; } = string.Empty;
    public string ProductDescription { get; set; } = string.Empty;
    public long OrderTotal { get; set; }
}