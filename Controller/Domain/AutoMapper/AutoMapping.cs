﻿using AutoMapper;

namespace Domain.AutoMapper
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            new ClienteMapping();
            new FilmeMapping();
            new LocacaoMapping();
        }
    }
}
