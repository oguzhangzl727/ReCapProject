using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Business;
using Core.Utilities.Helpers.FileHelper.Abstract;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Core.Utilities.Results;
using Business.BusinessAspect.Autofac;

namespace Business.Concrete
{
    public class CarImageManager : ICarImageService
    {
        ICarImageDal _carImageDal;
        IFileHelper _fileHelper;
        public CarImageManager(ICarImageDal carImageDal, IFileHelper fileHelper)
        {
            _carImageDal = carImageDal;
            _fileHelper = fileHelper;
        }


        [SecuredOperation("carimage.add,admin")]
        [ValidationAspect(typeof(CarImageValidator))]
        public IResult Add(IFormFile file, CarImage carImage)
        {
            var result = BusinessRules.Run(CheckIfCarImageLimit(carImage.CarId));
            if (result != null)
            {
                return result;
            }
            var resultofUpload = _fileHelper.Upload(file, PathConstant.ImagePath);
            if (!resultofUpload.Success)
            {
                return resultofUpload;
            }
            carImage.ImagePath = resultofUpload.Message;
            carImage.Date = DateTime.Now;
            _carImageDal.Add(carImage);
            return new SuccessResult(Messages.CarImageAdded);
        }


        [SecuredOperation("carimage.delete,admin")]
        public IResult Delete(CarImage carImage)
        {
            _carImageDal.Delete(carImage);
            return new SuccessResult(Messages.CarImageDeleted);

        }

        public IDataResult<List<CarImage>> GetAll()
        {
            return new SuccessDataResult<List<CarImage>>(_carImageDal.GetAll(),Messages.CarImagesListed);
        }

        public IDataResult<List<CarImage>> GetByCarId(int carId)
        {
            return new SuccessDataResult<List<CarImage>>(_carImageDal.GetAll(c=>c.CarId==carId), Messages.CarImagesListed);
        }

        public IDataResult<CarImage> GetByImageId(int id)
        {
            return new SuccessDataResult<CarImage>(_carImageDal.Get(c=>c.Id==id), Messages.CarImageListed);
        }



        [SecuredOperation("carimage.update,admin")]
        [ValidationAspect(typeof(CarImageValidator))]
        public IResult Update(IFormFile file, CarImage carImage)
        {
            _carImageDal.Update(carImage);
            return new SuccessResult(Messages.CarImageUpdated);
        }



        private IResult CheckIfCarImageLimit(int carId)
        {
            var result = _carImageDal.GetAll(carImage => carImage.CarId == carId);
            if (result.Count >= 5)
            {
                return new ErrorResult(Messages.CarImageLimitExceded);
            }
            return new SuccessResult();
        }


        private IDataResult<List<CarImage>> GetDefaultImage(int carId)
        {
            var defaultCarImage = new List<CarImage>();

            defaultCarImage.Add(new CarImage { CarId = carId, Date = DateTime.Now, ImagePath = "no-photos.png" });

            return new SuccessDataResult<List<CarImage>>(defaultCarImage);
        }

        private IResult CheckCarImageCount(int carId)
        {
            var result = _carImageDal.GetAll(carImage => carImage.CarId == carId).Count;
            if (result > 0)
            {
                return new SuccessResult();
            }
            return new ErrorResult();
        }
    }
}
