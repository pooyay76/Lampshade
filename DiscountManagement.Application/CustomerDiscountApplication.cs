using DiscountManagement.Application.Contract;
using DiscountManagement.Domain.CustomerDiscountAgg;
using Framework.Application;
using System;
using System.Collections.Generic;

namespace DiscountManagement.Application
{
    public class CustomerDiscountApplication : ICustomerDiscountApplication
    {
        private readonly IDiscountRepository repository;

        public CustomerDiscountApplication(IDiscountRepository repository)
        {
            this.repository = repository;
        }

        public OperationResult Create(DefineCustomerDiscount command)
        {
            OperationResult operation = new();
            var data = new CustomerDiscount(command.ProductId,DateTime.Parse())
            if (repository.Exists(x=>x.StartDate == command.StartDate && x.EndDate == command.EndDate && x.Reason == command.Reason))
            {
                return operation.Failed(ApplicationMessages.NotFoundMessage);
            }
        }

        public OperationResult Edit(EditCustomerDiscount command)
        {
            throw new System.NotImplementedException();
        }

        public CustomerDiscountViewModel EditGet(long id)
        {
            throw new System.NotImplementedException();
        }

        public List<CustomerDiscountViewModel> Search(CustomerDiscountSearchModel query)
        {
            throw new System.NotImplementedException();
        }
    }
}
