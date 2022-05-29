using _0_Framework.Application;
using AccountManagement.Application.Contracts.AccountAgg;
using AccountManagement.Domain.AccountAgg;
using AutoMapper;
using Framework.Application;
using System.Collections.Generic;
using System.Linq;

namespace AccountManagement.Application
{
    public class AccountApplication : IAccountApplication
    {
        private readonly IAccountRepository accountRepository;
        private readonly IPasswordHasher passwordHasher;
        private readonly IMapper mapper;
        public AccountApplication(IAccountRepository accountRepository,IPasswordHasher passwordHasher,IMapper mapper)
        {
            this.mapper = mapper;
            this.accountRepository = accountRepository;
            this.passwordHasher = passwordHasher;
        }

        public OperationResult ChangePassword(ChangePassword command)
        {
            OperationResult operation = new();
            var target = accountRepository.Get(command.Id);
            if (command.Password != command.RepeatedPassword)
                return operation.Failed(ValidationMessages.PasswordsMismatchMessage);
            target.ChangePassword(passwordHasher.Hash(command.Password));
            accountRepository.Update(target);
            return operation.Succeeded();
        }

        public OperationResult Create(CreateAccount command)
        {
            OperationResult operation = new();
            if (accountRepository.Exists(x => x.PhoneNumber == command.PhoneNumber))
                return operation.Failed(ApplicationMessages.DuplicatedAccountMessage);
            if (accountRepository.Exists(x => x.Username == command.Username))
                return operation.Failed(ApplicationMessages.DuplicatedUsernameMessage);
            var target = new Account(command.FullName,
                                     command.Username,
                                     passwordHasher.Hash(command.Password),
                                     command.ProfilePicture,
                                     command.RoleId,
                                     command.PhoneNumber);
            accountRepository.Create(target);
            return operation.Succeeded();
        }

        public OperationResult Edit(EditAccount command)
        {
            OperationResult operation = new();
            var target = accountRepository.Get(command.Id);

            if (accountRepository.Exists(x => x.PhoneNumber == command.PhoneNumber && x.Id!=command.Id))
                return operation.Failed(ApplicationMessages.DuplicatedAccountMessage);
            if (accountRepository.Exists(x => x.Username == command.Username && x.Id != command.Id))
                return operation.Failed(ApplicationMessages.DuplicatedUsernameMessage);
            target.Edit(command.FullName,
                command.Username,
                command.ProfilePicture,
                command.RoleId,
                command.PhoneNumber);
            accountRepository.Update(target);
            return operation.Succeeded();
        }

        public EditAccount EditGet(long id)
        {
            var target = accountRepository.Get(id);
            if (target == null)
                return null;
            return mapper.Map<EditAccount>(target);
        }

        public List<AccountViewModel> Search(AccountSearchModel command)
        {
            return accountRepository.Search(command).Select(x => mapper.Map<AccountViewModel>(x)).ToList();
        }
    }
}
