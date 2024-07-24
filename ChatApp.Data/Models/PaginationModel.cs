﻿using Microsoft.Identity.Client;

namespace ChatApp.Data;

public class PaginationModel
{
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
    public string? Name { get; set; } = null;
}
