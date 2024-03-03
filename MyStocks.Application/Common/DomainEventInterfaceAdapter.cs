using MediatR;
using MyStocks.Domain.Common.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyStocks.Application.Common;

public class Event<IDomainEvent> : INotification
{
//todo: verificar formas de melhorar essa transformação da classe
}