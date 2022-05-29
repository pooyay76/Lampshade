﻿using _0_Framework.Application;
using DiscountManagement.Application.Contracts.CustomerDiscountAgg;
using DiscountManagement.Domain.CustomerDiscountAgg;
using Framework.Application;
using System.Collections.Generic;
using System.Linq;

namespace DiscountManagement.Application
{
    public class CustomerDiscountApplication : ICustomerDiscountApplication
    {
        private readonly ICustomerDiscountRepository customerDiscountRepository;

        public CustomerDiscountApplication(ICustomerDiscountRepository customerDiscountRepository)
        {
            this.customerDiscountRepository = customerDiscountRepository;
        }

        public OperationResult Define(DefineCustomerDiscount command)
        {
            OperationResult operation = new();
            var x = command.StartDate.ToGeorgianDateTime();
            var data = new CustomerDiscount(command.ProductId, command.StartDate.ToGeorgianDateTime(),command.EndDate.ToGeorgianDateTime(), command.Reason, command.DiscountRate);
            if (customerDiscountRepository.Exists(x => x.StartDate == data.StartDate && x.EndDate == data.EndDate && x.Reason == data.Reason))
            {
                return operation.Failed(ApplicationMessages.DuplicatedMessage);
            }
            customerDiscountRepository.Create(data);
            return operation.Succeeded();
        }

        public OperationResult Edit(EditCustomerDiscount command)
        {
            OperationResult operation = new();
            var data = customerDiscountRepository.Get(command.Id);

            if (data == null)
                return operation.Failed(ApplicationMessages.NotFoundMessage);

            if (customerDiscountRepository.Exists(x => x.StartDate == command.StartDate.ToGeorgianDateTime() 
            && x.EndDate == command.EndDate.ToGeorgianDateTime() && x.Reason == command.Reason && x.Id != command.Id))
                return operation.Failed(ApplicationMessages.DuplicatedMessage);

            data.Edit(command.ProductId, command.StartDate.ToGeorgianDateTime(), command.EndDate.ToGeorgianDateTime(), command.Reason, command.DiscountRate);

            customerDiscountRepository.Update(data);

            return operation.Succeeded();
        }

        public EditCustomerDiscount EditGet(long id)
        {
            var data = customerDiscountRepository.Get(id);
            return new EditCustomerDiscount
            {
                DiscountRate = data.DiscountPercentage,
                EndDate = data.EndDate.ToFarsi().ToString(),
                Id = data.Id,
                ProductId = data.ProductId,
                Reason = data.Reason,
                StartDate = data.StartDate.ToFarsi().ToString()
            };
        }

        public List<CustomerDiscountViewModel> Search(CustomerDiscountSearchModel command)
        {
            return customerDiscountRepository.Search(command).ToList();
        }

    }
}
