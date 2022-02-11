using DiscountManagement.Application.Contracts;
using DiscountManagement.Application.Contracts.ColleagueDiscountAgg;
using DiscountManagement.Domain.ColleagueDiscountAgg;
using Framework.Application;
using System.Collections.Generic;
using System.Linq;


namespace DiscountManagement.Application
{
    public class ColleagueDiscountApplication : IColleagueDiscountApplication
    {
        private readonly IColleagueDiscountRepository colleagueDiscountRepository;

        public ColleagueDiscountApplication(IColleagueDiscountRepository colleagueDiscountRepository)
        {
            this.colleagueDiscountRepository = colleagueDiscountRepository;
        }


        public OperationResult Define(DefineColleagueDiscount command)
        {
            OperationResult operation = new();
            var data = new ColleagueDiscount(command.Name,command.ProductId,command.DiscountRate);
            if (colleagueDiscountRepository.Exists(x =>  x.Name == data.Name))
            {
                return operation.Failed(ApplicationMessages.DuplicatedMessage);
            }
            colleagueDiscountRepository.Create(data);
            return operation.Succeeded();
        }
       
        public OperationResult Edit(EditColleagueDiscount command)
        {
            OperationResult operation = new();
            var data = colleagueDiscountRepository.Get(command.Id);

            if (data == null)
                return operation.Failed(ApplicationMessages.NotFoundMessage);

            if (colleagueDiscountRepository.Exists(x => x.Name == command.Name && x.Id != command.Id))
                return operation.Failed(ApplicationMessages.DuplicatedMessage);

            data.Edit(command.Name,command.ProductId,command.DiscountRate);

            colleagueDiscountRepository.Update(data);

            return operation.Succeeded();
        }

        //doesnt fill products
        public EditColleagueDiscount EditGet(long id)
        {
            var data = colleagueDiscountRepository.Get(id);
            return new EditColleagueDiscount() {DiscountRate=data.DiscountRate,Name=data.Name,Id=data.Id,ProductId=data.ProductId };
        }

        public OperationResult Remove(long id)
        {
            OperationResult operation = new();
            var data = colleagueDiscountRepository.Get(id);

            if (data == null)
                return operation.Failed(ApplicationMessages.NotFoundMessage);
            data.Remove();
            colleagueDiscountRepository.Update(data);
            return operation.Succeeded();
            

        }

        public OperationResult Restore(long id)
        {
            OperationResult operation = new();
            var data = colleagueDiscountRepository.Get(id);

            if (data == null)
                return operation.Failed(ApplicationMessages.NotFoundMessage);
            data.Restore();
            colleagueDiscountRepository.Update(data);
            return operation.Succeeded();
        }

        public List<ColleagueDiscountViewModel> Search(ColleagueDiscountSearchModel command)
        {
            return colleagueDiscountRepository.Search(command).ToList();
        }


    }
}
