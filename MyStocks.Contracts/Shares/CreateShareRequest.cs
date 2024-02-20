﻿using MediatR;
using MyStocks.Domain.Currencies;
using MyStocks.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyStocks.Contracts.Shares;
public record CreateShareRequest(
string code,
string name,
string? description,
string shareTypeCode,
string? currencyTypeCode
);
