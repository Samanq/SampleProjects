﻿using MoqSample.Infrastructure.Services.Interfaces;

namespace MoqSample.Infrastructure.Services;

internal class CodeValidator : ICodeValidator
{
    public bool IsValid(int code)
    {
        return code > 10 && code < 20;
    }
}