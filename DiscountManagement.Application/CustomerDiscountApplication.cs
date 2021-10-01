using _0_Framework.Application;
using DiscountManagement.Application.Contract;
using DiscountManagement.Domain.CustomerDiscountAgg;
using Framework.Application;
using System;
using System.Collections.Generic;

namespace DiscountManagement.Application
{
    public class CustomerDiscountApplication : ICustomerDiscountApplication
    {
        private readonly ICustomerDiscountRepository customerDiscountRepository;

        public CustomerDiscountApplication(ICustomerDiscountRepository repository)
        {
            this.customerDiscountRepository = repository;
        }

        public OperationResult Define(DefineCustomerDiscount command)
        {
            OperationResult operation = new();
            var data = new CustomerDiscount(command.ProductId, DateTime.Parse(command.StartDate), DateTime.Parse(command.EndDate), command.Reason, command.DiscountRate);
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
            if (customerDiscountRepository.Exists(x => x.StartDate == command.StartDate.ToGeorgianDateTime() && x.EndDate == command.EndDate.ToGeorgianDateTime() && x.Reason == command.Reason))
                return operation.Failed(ApplicationMessages.DuplicatedMessage);
            data.Edit(command.ProductId, DateTime.Parse(command.StartDate), DateTime.Parse(command.EndDate), command.Reason, command.DiscountRate);
            customerDiscountRepository.SaveChanges();
            return operation.Succeeded();
        }

        public EditCustomerDiscount EditGet(long id)
        {
            var data = customerDiscountRepository.Get(id);
            return new EditCustomerDiscount
            {
                Id = data.Id,
                ProductId = data.ProductId,
                Reason = data.Reason,
                DiscountRate = data.DiscountRate,
                StartDate = data.StartDate.ToString(),
                EndDate = data.EndDate.ToString(),
            };
        }

        public List<CustomerDiscountViewModel> Search(CustomerDiscountSearchModel command)
        {
            return customerDiscountRepository.Search(command);
        }

    }
}
