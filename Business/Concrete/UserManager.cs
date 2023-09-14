using Business.Abstract;
using Business.BusinessAspect.Autofac;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class UserManager : IUserService
    {
        IUserDal _userDal;
        public UserManager(IUserDal userDal) {
            _userDal = userDal;
        }

        [ValidationAspect(typeof(UserValidator))]
        public IResult Add(User user)
        {
            try
            {
                _userDal.Add(user);
                return new SuccessResult(Messages.UserAdded);
            }
            catch (Exception ex)
            {

                return new ErrorDataResult<List<Car>>(ex.ToString());
            }
        }


        public IResult Delete(User user)
        {
            try
            {
                _userDal.Delete(user);
                return new SuccessResult(Messages.UserDeleted);
            }
            catch (Exception ex)
            {

                return new ErrorDataResult<List<Car>>(ex.ToString());
            }
        }

        public IDataResult<List<User>> GetAll()
        {
            try
            {
                var result = _userDal.GetAll();

                if (result == null)
                {
                    return new ErrorDataResult<List<User>>(Messages.NullData);
                }

                return new SuccessDataResult<List<User>>(result, Messages.UsersListed);
            }
            catch (Exception ex)
            {

                return new ErrorDataResult<List<User>>(ex.ToString());
            }
        }

        public IDataResult<User> GetByEmail(string email)
        {
            try
            {
                var result = _userDal.Get(u => u.Email==email);

                if (result == null)
                {
                    return new ErrorDataResult<User>(Messages.NullData);
                }

                return new SuccessDataResult<User>(result, Messages.UsersListed);
            }
            catch (Exception ex)
            {

                return new ErrorDataResult<User>(ex.ToString());
            }
        }

        public IDataResult<User> GetById(int id)
        {
            try
            {
                var result = _userDal.Get(u => u.UserId == id);

                if (result == null)
                {
                    return new ErrorDataResult<User>(Messages.NullData);
                }

                return new SuccessDataResult<User>(result, Messages.UsersListed);
            }
            catch (Exception ex)
            {

                return new ErrorDataResult<User>(ex.ToString());
            }
        }

        public IDataResult<List<OperationClaim>> GetClaims(User user)
        {
            try
            {
                var result = _userDal.GetClaims(user);

                if (result == null)
                {
                    return new ErrorDataResult<List<OperationClaim>>(Messages.NullData);
                }

                return new SuccessDataResult<List<OperationClaim>>(result, Messages.UserClaimsListed);

            }
            catch (Exception ex)
            {

                return new ErrorDataResult<List<OperationClaim>>(ex.ToString());
            }
        }

        [ValidationAspect(typeof(UserValidator))]
        public IResult Update(User user)
        {
            try
            {
                _userDal.Update(user);
                return new SuccessResult(Messages.UserUpdated);
            }
            catch (Exception ex)
            {

                return new ErrorDataResult<List<Car>>(ex.ToString());
            }
        }
    }
}
